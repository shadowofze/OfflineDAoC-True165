<#
.SYNOPSIS
    Compares bot fight logs of server runs: the old bot AI ("SmartAI off") against SmartAI ("SmartAI on").

.DESCRIPTION
    Reads the fight recorder's files (runtime/logs/smart_ai_groups and smart_ai_solo, subfolders included), one run
    per server start (the files' yyyyMMdd-HHmmss prefix), and reports per run and per AI:
      fights, kills, deaths, deaths per fight, share of fights with a death, kills per log-hour,
      fight length, quiet time between fights, caster melee starts, swing position, style use,
      spells landed and heals, and the recorder's flags.
    It writes an HTML page and the same tables as Markdown (for a pull request; -MdOut, default next to the HTML
    page as fight-compare.md), and prints a short summary.

    Turn recording on from the server start in both kinds of run, so they are recorded the same way:
    state-165/smartai-world.json  "settings": { "recordFightsFromStart": true }
    (or type /smartai debugfights in game).

    Only lines the recorder writes for both AIs are compared. SmartAI's decision lines ("thinks", "Legacy") and its
    "spec bans melee" flag have no old-AI counterpart; caster melee is counted for the pure caster classes only.
    Old AI / SmartAI is read from each log file's first line, never from the folder name.

.EXAMPLE
    powershell -ExecutionPolicy Bypass -File Compare-FightLogs.ps1
    powershell -ExecutionPolicy Bypass -File Compare-FightLogs.ps1 -Runs 20261106-090111,20261106-100057 -Open
#>
param(
    [string]$LogsDir = 'C:\ODTestMP\runtime\logs',
    [string[]]$Runs,
    [int]$MinFights = 20,
    [string]$HtmlOut,
    [string]$MdOut,
    [string]$CsvOut,
    [switch]$Open
)

# "powershell -File" hands "-Runs a,b" over as one string: split it here.
$Runs = @($Runs | ForEach-Object { $_ -split '[,; ]+' } | Where-Object { $_ })
if (-not $HtmlOut) { $HtmlOut = Join-Path $LogsDir 'fight-compare.html' }
if (-not $MdOut) { $MdOut = [IO.Path]::ChangeExtension($HtmlOut, '.md') }

$casters = @('Wizard','Theurgist','Sorcerer','Cabalist','Eldritch','Enchanter','Mentalist','Animist','Runemaster',
             'Spiritmaster','Bonedancer','Mage','Magician','Elementalist','Forester','Mystic')
$reHead   = [regex]'^=== Fight \d+ \| (?<t>[\d-]+ [\d:]+) \| (?<s>\d+)s \| .*?kills (?<k>\d+)/(?<e>\d+), deaths (?<d>\d+), lowest health (?<low>\d+)%'
$reMember = [regex]'^  (?<n>[^ |*]+)\*? \| (?<kind>\w+) \| (?<cls>[A-Za-z ]+?) \d+ \|'
$reDies   = [regex]'^\s+[+-][\d.]+\s+! (?<n>\S+) dies'
$reGap    = [regex]'^Before: (?:! )?(?<g>\d+)s since the last fight'
$reSwings = [regex]'swings (?<n>\d+) \(front (?<f>\d+)/side (?<si>\d+)/back (?<b>\d+)\)(?:, styles (?<st>[^|]+))?'
$reSpells = [regex]'spells (?<n>\d+) landed \(heals (?<h>\d+)\)(?:, (?<nf>\d+) not finished)?'
$reShots  = [regex]'\| shots (?<n>\d+)'
$reMelee  = [regex]'\((?<cls>[A-Za-z ]+)\) started melee (?<n>\d+)x'
$reNum    = [regex]'(\d+)(?=\s*(,|$))'

$sumKeys = 'Files','Fights','Kills','Deaths','FightsWithDeath','FightSec','GroupSec','Gaps','GapSec','LowSum','CasterMelee',
           'Swings','Front','Side','Back','Styled','Spells','Heals','NotFinished','Shots'
function New-Stats {
    $s = [ordered]@{}; foreach ($k in $sumKeys) { $s[$k] = 0.0 }
    $s.Flags = @{}; $s.DeathsByClass = @{}; $s.FightsByClass = @{}; $s.Runs = 0; $s.Start = $null; $s.End = $null
    return $s
}
function Add-Stats($into, $from) {
    foreach ($k in $sumKeys) { $into[$k] += $from[$k] }
    foreach ($h in 'Flags','DeathsByClass','FightsByClass') { foreach ($e in $from[$h].GetEnumerator()) { $into[$h][$e.Key] = $e.Value + [double]$into[$h][$e.Key] } }
    $into.Runs += 1
}

# Each metric: label, how it is worked out, which way is better ('low', 'high' or '' for neither), and a note.
$metrics = @(
    @{ L='Fights';                    F={ param($s) $s.Fights };                                        B='';     N='complete fights recorded' }
    @{ L='Kills';                     F={ param($s) $s.Kills };                                         B='';     N='' }
    @{ L='Deaths';                    F={ param($s) $s.Deaths };                                        B='';     N='bot and player deaths in those fights' }
    @{ L='Deaths per fight';          F={ param($s) $s.Deaths / $s.Fights };                            B='low';  N=''; D=3 }
    @{ L='Fights with a death %';     F={ param($s) 100 * $s.FightsWithDeath / $s.Fights };             B='low';  N='' }
    @{ L='Kills per death';           F={ param($s) if ($s.Deaths) { $s.Kills / $s.Deaths } else { $s.Kills } }; B='high'; N='' }
    @{ L='Kills per log-hour';        F={ param($s) $s.Kills / [math]::Max(1/60, $s.GroupSec / 3600) }; B='high'; N='per group (or solo bot), from its first to its last fight' }
    @{ L='Average fight (s)';         F={ param($s) $s.FightSec / $s.Fights };                          B='low';  N='' }
    @{ L='Average gap between fights (s)'; F={ param($s) if ($s.Gaps) { $s.GapSec / $s.Gaps } else { 0 } }; B='low'; N='quiet time before a pull' }
    @{ L='Average lowest health %';   F={ param($s) $s.LowSum / $s.Fights };                            B='high'; N='lowest member health in a fight' }
    @{ L='Caster melee starts per fight'; F={ param($s) $s.CasterMelee / $s.Fights };                   B='low';  N='pure caster classes only'; D=3 }
    @{ L='Swings from side or back %'; F={ param($s) 100 * ($s.Side + $s.Back) / [math]::Max(1, $s.Swings) }; B='high'; N='' }
    @{ L='Swings with a style %';     F={ param($s) 100 * $s.Styled / [math]::Max(1, $s.Swings) };      B='high'; N='' }
    @{ L='Spells landed per fight';   F={ param($s) $s.Spells / $s.Fights };                            B='';     N='' }
    @{ L='Heals per fight';           F={ param($s) $s.Heals / $s.Fights };                             B='';     N='' }
    @{ L='Unfinished casts per fight'; F={ param($s) $s.NotFinished / $s.Fights };                      B='low';  N='' }
    @{ L='Bow shots per fight';       F={ param($s) $s.Shots / $s.Fights };                             B='';     N='' }
    @{ L='Members who did nothing, per fight'; F={ param($s) [double]$s.Flags['did nothing (no cast, no attack)'] / $s.Fights }; B='low'; N='no cast, no attack'; D=3 }
    @{ L='Missed positional styles per fight'; F={ param($s) [double]$s.Flags['swung from the side/back but used no positional style'] / $s.Fights }; B='low'; N=''; D=3 }
    @{ L='Missed reactive styles per fight'; F={ param($s) [double](($s.Flags.GetEnumerator() | Where-Object { $_.Key -like 'missed * reactive*' } | Measure-Object Value -Sum).Sum) / $s.Fights }; B='low'; N=''; D=3 }
)

function Get-Value($m, $s) { $v = & $m.F $s; if ($null -eq $v) { 0 } else { [double]$v } }
function Format-Value($m, $v) {
    $d = if ($m.D) { $m.D } elseif ($v -ge 100 -or $m.L -in 'Fights','Kills','Deaths') { 0 } else { 1 }
    if ($m.L -in 'Fights','Kills','Deaths') { return ('{0:N0}' -f $v) }
    return ('{0:N' + $d + '}') -f $v
}

# ---- read the logs
$dirs = @('smart_ai_groups','smart_ai_solo') | ForEach-Object { Join-Path $LogsDir $_ } | Where-Object { Test-Path $_ }
# Subfolders are read too (e.g. smart_ai_groups\group run 1 smartai off). A run is still told apart by the server
# start in each file's name, and old AI / SmartAI by each file's own header, never by the folder's name.
$files = $dirs | ForEach-Object { Get-ChildItem $_ -Filter '*.log' -Recurse -File } | Where-Object { $_.Name -match '^\d{8}-\d{6}_' }
$byRun = $files | Group-Object { $_.Name.Substring(0,15) } | Where-Object { $Runs.Count -eq 0 -or $Runs -contains $_.Name } | Sort-Object Name

$results = @()
foreach ($run in $byRun) {
    foreach ($kind in 'group','solo') {
        $set = $run.Group | Where-Object { ($_.FullName -like '*\smart_ai_solo\*') -eq ($kind -eq 'solo') }
        if (-not $set) { continue }
        Write-Host "Reading $($run.Name) $kind ($($set.Count) files)..."
        $s = New-Stats; $ai = '?'
        foreach ($file in $set) {
            $s.Files++
            $members = @{}; $first = $null; $last = $null
            foreach ($line in [IO.File]::ReadLines($file.FullName)) {
                if ($ai -eq '?' -and $line -match 'SmartAI (on|off)\.') { $ai = if ($Matches[1] -eq 'on') { 'SmartAI' } else { 'Old AI' } }
                $m = $reHead.Match($line)
                if ($m.Success) {
                    $members = @{}
                    $t = [datetime]::ParseExact($m.Groups['t'].Value, 'yyyy-MM-dd HH:mm:ss', $null); $sec = [int]$m.Groups['s'].Value
                    if (-not $first) { $first = $t }; $last = $t.AddSeconds($sec)
                    if (-not $s.Start -or $t -lt $s.Start) { $s.Start = $t }
                    if (-not $s.End -or $last -gt $s.End) { $s.End = $last }
                    $d = [int]$m.Groups['d'].Value
                    $s.Fights++; $s.Kills += [int]$m.Groups['k'].Value; $s.Deaths += $d; $s.FightSec += $sec
                    $s.LowSum += [int]$m.Groups['low'].Value
                    if ($d -gt 0) { $s.FightsWithDeath++ }
                    continue
                }
                if ($line.StartsWith('Before:')) { $g = $reGap.Match($line); if ($g.Success) { $s.Gaps++; $s.GapSec += [int]$g.Groups['g'].Value }; continue }
                $m = $reMember.Match($line)
                if ($m.Success -and $line.Contains(' hp ')) {
                    $c = $m.Groups['cls'].Value.Trim(); $members[$m.Groups['n'].Value] = $c
                    $s.FightsByClass[$c] = 1 + [double]$s.FightsByClass[$c]
                    continue
                }
                $m = $reDies.Match($line)
                if ($m.Success -and $members.ContainsKey($m.Groups['n'].Value)) { $c = $members[$m.Groups['n'].Value]; $s.DeathsByClass[$c] = 1 + [double]$s.DeathsByClass[$c]; continue }
                if ($line.StartsWith('Flags:')) {
                    foreach ($part in $line.Substring(6).Split(';')) {
                        $p = $part.Trim(); if (-not $p) { continue }
                        $mm = $reMelee.Match($p)
                        if ($mm.Success) { if ($casters -contains $mm.Groups['cls'].Value) { $s.CasterMelee += [int]$mm.Groups['n'].Value }; $key = 'caster/spec started melee' }
                        else { $key = ($p -replace '^\S+ ', '' -replace '\d+', 'N') }
                        $s.Flags[$key] = 1 + [double]$s.Flags[$key]
                    }
                    continue
                }
                if ($line.StartsWith('  ') -and $line.Contains(': ')) {
                    $m = $reSwings.Match($line)
                    if ($m.Success) {
                        $s.Swings += [int]$m.Groups['n'].Value; $s.Front += [int]$m.Groups['f'].Value; $s.Side += [int]$m.Groups['si'].Value; $s.Back += [int]$m.Groups['b'].Value
                        if ($m.Groups['st'].Success) { foreach ($x in $reNum.Matches($m.Groups['st'].Value)) { $s.Styled += [int]$x.Groups[1].Value } }
                    }
                    $m = $reSpells.Match($line)
                    if ($m.Success) { $s.Spells += [int]$m.Groups['n'].Value; $s.Heals += [int]$m.Groups['h'].Value; if ($m.Groups['nf'].Success) { $s.NotFinished += [int]$m.Groups['nf'].Value } }
                    $m = $reShots.Match($line)
                    if ($m.Success) { $s.Shots += [int]$m.Groups['n'].Value }
                }
            }
            if ($first) { $s.GroupSec += ($last - $first).TotalSeconds }
        }
        if ($s.Fights -lt $MinFights) { continue }
        $s.Runs = 1
        $results += [pscustomobject]@{ Run = $run.Name; AI = $ai; Kind = $kind; Stats = $s
            Folder = (($set | ForEach-Object { $_.Directory.Name } | Sort-Object -Unique) -join ', ') }
    }
}
if (-not $results) { Write-Output "No run with $MinFights+ fights in $LogsDir$(if ($Runs.Count) { " for runs $($Runs -join ', ')" })."; return }

# ---- per AI totals (the runs' raw counts added up, so every rate is over all their fights)
$totals = @{}
foreach ($r in $results) {
    $key = "$($r.Kind)|$($r.AI)"
    if (-not $totals.ContainsKey($key)) { $totals[$key] = New-Stats; $totals[$key].Runs = 0 }
    Add-Stats $totals[$key] $r.Stats
}

# ---- console summary
foreach ($kind in 'group','solo') {
    foreach ($ai in 'Old AI','SmartAI','?') {
        $t = $totals["$kind|$ai"]; if (-not $t) { continue }
        $parts = foreach ($m in $metrics[3,4,6,7,8,10,11,12,17]) { "$($m.L.ToLower()) $(Format-Value $m (Get-Value $m $t))" }
        Write-Output ("{0,-5} {1,-7} runs {2}, fights {3:N0}: {4}" -f $kind, $ai, $t.Runs, $t.Fights, ($parts -join ', '))
    }
}

# ---- HTML
function Enc([string]$s) { [System.Net.WebUtility]::HtmlEncode($s) }
function Get-Change($m, $old, $new) {
    if ($old -eq 0 -and $new -eq 0) { return @('', '') }
    $pct = if ($old -ne 0) { 100 * ($new - $old) / [math]::Abs($old) } else { 100 }
    $text = '{0}{1:N0}%' -f $(if ($pct -ge 0) { '+' } else { '' }), $pct
    if (-not $m.B -or [math]::Abs($pct) -lt 2) { return @($text, 'same') }
    $better = ($m.B -eq 'low' -and $new -lt $old) -or ($m.B -eq 'high' -and $new -gt $old)
    return @($text, $(if ($better) { 'better' } else { 'worse' }))
}
function Get-ClassTable($statsList, $names) {
    $classes = @{}
    foreach ($st in $statsList) { foreach ($e in $st.DeathsByClass.GetEnumerator()) { $classes[$e.Key] = $e.Value + [double]$classes[$e.Key] } }
    $top = $classes.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 15
    if (-not $top) { return '<p class="note">No member deaths.</p>' }
    $h = '<table><thead><tr><th>Class</th>' + (($names | ForEach-Object { "<th>$(Enc $_)<br><span class='sub'>deaths / per 100 fights in</span></th>" }) -join '') + '</tr></thead><tbody>'
    foreach ($c in $top) {
        $h += "<tr><td>$(Enc $c.Key)</td>"
        foreach ($st in $statsList) {
            $d = [double]$st.DeathsByClass[$c.Key]; $f = [double]$st.FightsByClass[$c.Key]
            $rate = if ($f) { '{0:N1}' -f (100 * $d / $f) } else { '-' }
            $h += "<td>$('{0:N0}' -f $d) <span class='sub'>/ $rate</span></td>"
        }
        $h += '</tr>'
    }
    return $h + '</tbody></table>'
}

$sections = foreach ($kind in 'group','solo') {
    $kr = @($results | Where-Object Kind -eq $kind)
    if (-not $kr) { continue }
    $title = if ($kind -eq 'group') { 'Group fights' } else { 'Solo fights (a bot fighting alone)' }
    $html = "<section><h2>$title</h2>"
    $old = $totals["$kind|Old AI"]; $new = $totals["$kind|SmartAI"]
    if ($old -and $new) {
        $html += "<h3>Old AI against SmartAI</h3><p class='note'>All runs of each AI added together: old AI $($old.Runs) run(s), $('{0:N0}' -f $old.Fights) fights; SmartAI $($new.Runs) run(s), $('{0:N0}' -f $new.Fights) fights. Change is SmartAI against the old AI; green is better, red is worse.</p>"
        $html += '<table class="cmp"><thead><tr><th>Measure</th><th>Old AI</th><th>SmartAI</th><th>Change</th></tr></thead><tbody>'
        foreach ($m in $metrics) {
            $a = Get-Value $m $old; $b = Get-Value $m $new
            $ch = if ($m.L -in 'Fights','Kills','Deaths') { @('', '') } else { Get-Change $m $a $b }
            $html += "<tr><td>$(Enc $m.L)$(if ($m.N) { "<br><span class='sub'>$(Enc $m.N)</span>" })</td><td>$(Format-Value $m $a)</td><td>$(Format-Value $m $b)</td><td class='$($ch[1])'>$($ch[0])</td></tr>"
        }
        $html += '</tbody></table>'
        $html += '<h3>Member deaths by class</h3>' + (Get-ClassTable @($old, $new) @('Old AI', 'SmartAI'))
    }
    else {
        $html += "<p class='note'>Only $(($kr | Select-Object -ExpandProperty AI -Unique) -join ', ') runs here, so there is no old AI against SmartAI table yet.</p>"
    }
    $html += '<h3>Every run side by side</h3><p class="note">Bold is the best run on that line.</p><table class="runs"><thead><tr><th>Measure</th>'
    foreach ($r in $kr) {
        $span = if ($r.Stats.Start) { '{0:HH:mm}-{1:HH:mm}' -f $r.Stats.Start, $r.Stats.End } else { '' }
        $html += "<th class='$(if ($r.AI -eq 'SmartAI') { 'ai-smart' } else { 'ai-old' })'>$(Enc $r.AI)<br><span class='sub'>$(Enc $r.Folder)<br>$($r.Run) $span, $($r.Stats.Files) logs</span></th>"
    }
    $html += '</tr></thead><tbody>'
    foreach ($m in $metrics) {
        $vals = @($kr | ForEach-Object { Get-Value $m $_.Stats })
        $best = if ($m.B -eq 'low') { ($vals | Measure-Object -Minimum).Minimum } elseif ($m.B -eq 'high') { ($vals | Measure-Object -Maximum).Maximum } else { $null }
        $html += "<tr><td>$(Enc $m.L)</td>"
        foreach ($v in $vals) { $html += "<td$(if ($null -ne $best -and $kr.Count -gt 1 -and $v -eq $best) { ' class=best' })>$(Format-Value $m $v)</td>" }
        $html += '</tr>'
    }
    $html += '</tbody></table>'
    $html += '<h3>Member deaths by class, per run</h3>' + (Get-ClassTable @($kr | ForEach-Object { $_.Stats }) @($kr | ForEach-Object { "$($_.AI) $($_.Run)" }))
    $html + '</section>'
}

$page = @"
<!DOCTYPE html>
<html lang="en"><head><meta charset="utf-8"><title>Old AI against SmartAI: fight logs</title>
<style>
 body { font: 14px/1.45 "Segoe UI", Arial, sans-serif; margin: 24px auto; max-width: 1200px; padding: 0 16px; color: #1d2329; background: #f6f7f9; }
 h1 { font-size: 22px; margin: 0 0 4px; } h2 { font-size: 18px; margin: 32px 0 8px; border-bottom: 2px solid #d5dbe1; padding-bottom: 4px; }
 h3 { font-size: 15px; margin: 20px 0 6px; } .note, .sub { color: #5b6672; } .sub { font-size: 12px; font-weight: normal; }
 section { background: #fff; border: 1px solid #e1e5ea; border-radius: 8px; padding: 4px 20px 20px; margin-bottom: 20px; }
 table { border-collapse: collapse; width: 100%; margin: 6px 0 12px; font-variant-numeric: tabular-nums; }
 th, td { border-bottom: 1px solid #e8ebef; padding: 6px 10px; text-align: right; vertical-align: top; }
 th:first-child, td:first-child { text-align: left; } thead th { background: #eef1f4; position: sticky; top: 0; }
 table.cmp { max-width: 760px; } tbody tr:hover { background: #f8fafc; }
 .better { color: #0a7a3d; font-weight: 600; } .worse { color: #b42318; font-weight: 600; } .same { color: #5b6672; }
 td.best { font-weight: 700; } th.ai-smart { border-top: 3px solid #2f6fdb; } th.ai-old { border-top: 3px solid #8a94a0; }
</style></head><body>
<h1>Old AI against SmartAI: fight logs</h1>
<p class="note">Made $(Get-Date -Format 'yyyy-MM-dd HH:mm') from $(Enc $LogsDir). Runs: $(($results | Select-Object -ExpandProperty Run -Unique) -join ', '). Runs with fewer than $MinFights fights are left out.
Caster melee counts pure caster classes only. SmartAI's own decision lines have no old-AI counterpart and are not compared. Bots keep their levels between runs, so later runs start a little stronger.</p>
$($sections -join "`n")
</body></html>
"@
[IO.File]::WriteAllText($HtmlOut, $page, (New-Object System.Text.UTF8Encoding $false))
Write-Output "HTML report: $HtmlOut"
if ($CsvOut) {
    $results | ForEach-Object { $r = $_; $o = [ordered]@{ Run = $r.Run; AI = $r.AI; Kind = $r.Kind; Folder = $r.Folder }
        foreach ($m in $metrics) { $o[$m.L] = [math]::Round((Get-Value $m $r.Stats), 3) }; [pscustomobject]$o } |
        Export-Csv -NoTypeInformation -Path $CsvOut
    Write-Output "CSV: $CsvOut"
}

# ---- Markdown (for a pull request: GitHub renders the tables). Plain text only - no emoji, and one line per
# paragraph, because GitHub keeps every line break in a pull request's description.
function MdEsc([string]$s) { $s.Replace('|', '\|') }
function Get-MdClassTable($statsList, $names) {
    $classes = @{}
    foreach ($st in $statsList) { foreach ($e in $st.DeathsByClass.GetEnumerator()) { $classes[$e.Key] = $e.Value + [double]$classes[$e.Key] } }
    $top = $classes.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 15
    if (-not $top) { return @('No member deaths.', '') }
    $lines = @(('| Class | ' + (($names | ForEach-Object { "$(MdEsc $_) deaths (per 100 fights in)" }) -join ' | ') + ' |'),
               ('|---|' + ('---:|' * $names.Count)))
    foreach ($c in $top) {
        $cells = foreach ($st in $statsList) {
            $d = [double]$st.DeathsByClass[$c.Key]; $f = [double]$st.FightsByClass[$c.Key]
            '{0:N0} ({1})' -f $d, $(if ($f) { '{0:N1}' -f (100 * $d / $f) } else { '-' })
        }
        $lines += "| $(MdEsc $c.Key) | $($cells -join ' | ') |"
    }
    return $lines + ''
}
$md = @('# Old AI against SmartAI: fight logs', '',
        ("Made $(Get-Date -Format 'yyyy-MM-dd HH:mm') from the fight recorder's logs. Runs: $(($results | Select-Object -ExpandProperty Run -Unique) -join ', '). Runs with fewer than $MinFights fights are left out. " +
         "Caster melee counts pure caster classes only. Bots keep their levels between runs, so later runs start a little stronger."), '')
foreach ($kind in 'group','solo') {
    $kr = @($results | Where-Object Kind -eq $kind)
    if (-not $kr) { continue }
    $md += $(if ($kind -eq 'group') { '## Group fights' } else { '## Solo fights (a bot fighting alone)' }), ''
    $old = $totals["$kind|Old AI"]; $new = $totals["$kind|SmartAI"]
    if ($old -and $new) {
        $md += '### Old AI against SmartAI', '',
               "All runs of each AI added together: old AI $($old.Runs) run(s), $('{0:N0}' -f $old.Fights) fights; SmartAI $($new.Runs) run(s), $('{0:N0}' -f $new.Fights) fights. Change is SmartAI against the old AI (""better"" and ""worse"" say which way SmartAI moved).", '',
               '| Measure | Old AI | SmartAI | Change |', '|---|---:|---:|---:|'
        foreach ($m in $metrics) {
            $a = Get-Value $m $old; $b = Get-Value $m $new
            $ch = if ($m.L -in 'Fights','Kills','Deaths') { @('', '') } else { Get-Change $m $a $b }
            $mark = switch ($ch[1]) { 'better' { ' better' } 'worse' { ' worse' } default { '' } }
            $md += "| $(MdEsc $m.L) | $(Format-Value $m $a) | $(Format-Value $m $b) | $($ch[0])$mark |"
        }
        $md += '', '### Member deaths by class', ''
        $md += Get-MdClassTable @($old, $new) @('Old AI', 'SmartAI')
    }
    $md += '### Every run side by side', '', 'Bold is the best run on that line.', '',
           ('| Measure | ' + (($kr | ForEach-Object { "$($_.AI) $($_.Run)" + $(if ($_.Folder -notmatch '^smart_ai_') { " ($(MdEsc $_.Folder))" }) }) -join ' | ') + ' |'),
           ('|---|' + ('---:|' * $kr.Count))
    foreach ($m in $metrics) {
        $vals = @($kr | ForEach-Object { Get-Value $m $_.Stats })
        $best = if ($m.B -eq 'low') { ($vals | Measure-Object -Minimum).Minimum } elseif ($m.B -eq 'high') { ($vals | Measure-Object -Maximum).Maximum } else { $null }
        $md += "| $(MdEsc $m.L) | " + (($vals | ForEach-Object { $t = Format-Value $m $_; if ($null -ne $best -and $kr.Count -gt 1 -and $_ -eq $best) { "**$t**" } else { $t } }) -join ' | ') + ' |'
    }
    $md += ''
}
[IO.File]::WriteAllText($MdOut, ($md -join "`r`n"), (New-Object System.Text.UTF8Encoding $false))
Write-Output "Markdown report: $MdOut"
if ($Open) { Start-Process $HtmlOut }
