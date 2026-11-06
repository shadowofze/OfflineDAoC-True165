<#
.SYNOPSIS
    Compares bot fight logs of server runs: the old bot AI ("SmartAI off") against SmartAI ("SmartAI on").

.DESCRIPTION
    Reads the fight recorder's files (runtime/logs/smart_ai_groups and smart_ai_solo), one run per server start
    (the files' yyyyMMdd-HHmmss prefix), and prints per run and per AI:
      fights, kills, deaths, deaths per fight, share of fights with a death, kills per group-hour,
      fight length, quiet time between fights, caster melee starts, swing position, style use,
      spells landed and heals, and the recorder's flags.
    Turn recording on from the server start in both kinds of run, so they are recorded the same way:
    state-165/smartai-world.json  "settings": { "recordFightsFromStart": true }
    (or type /smartai debugfights in game).

    Only lines the recorder writes for both AIs are compared. SmartAI's decision lines ("thinks", "Legacy") and its
    "spec bans melee" flag have no old-AI counterpart; caster melee is counted for the pure caster classes only.

.EXAMPLE
    powershell -File Compare-FightLogs.ps1 -LogsDir C:\ODTestMP\runtime\logs
    powershell -File Compare-FightLogs.ps1 -Runs 20261106-090000,20261106-120000 -MinFights 50
#>
param(
    [string]$LogsDir = 'C:\ODTestMP\runtime\logs',
    [string[]]$Runs,
    [int]$MinFights = 20,
    [string]$CsvOut
)

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

function New-Stats { [ordered]@{ Files=0; Fights=0; Kills=0; Deaths=0; FightsWithDeath=0; FightSec=0; GroupSec=0.0; Gaps=0; GapSec=0
    LowSum=0; CasterMelee=0; Swings=0; Front=0; Side=0; Back=0; Styled=0; Spells=0; Heals=0; NotFinished=0; Shots=0
    Flags=@{}; DeathsByClass=@{} } }

$dirs = @('smart_ai_groups','smart_ai_solo') | ForEach-Object { Join-Path $LogsDir $_ } | Where-Object { Test-Path $_ }
$files = $dirs | ForEach-Object { Get-ChildItem $_ -Filter '*.log' }
$byRun = $files | Group-Object { $_.Name.Substring(0,15) } | Where-Object { -not $Runs -or $Runs -contains $_.Name } | Sort-Object Name

$rows = @()
foreach ($run in $byRun) {
    foreach ($kind in 'group','solo') {
        $set = $run.Group | Where-Object { ($_.DirectoryName -like '*smart_ai_solo') -eq ($kind -eq 'solo') }
        if (-not $set) { continue }
        $s = New-Stats; $ai = '?'
        foreach ($file in $set) {
            $s.Files++
            $members = @{}; $first = $null; $last = $null
            foreach ($line in [IO.File]::ReadLines($file.FullName)) {
                if ($ai -eq '?' -and $line -match 'SmartAI (on|off)\.') { $ai = if ($Matches[1] -eq 'on') { 'SmartAI' } else { 'old AI' } }
                $m = $reHead.Match($line)
                if ($m.Success) {
                    $members = @{}
                    $t = [datetime]::ParseExact($m.Groups['t'].Value, 'yyyy-MM-dd HH:mm:ss', $null); $sec = [int]$m.Groups['s'].Value
                    if (-not $first) { $first = $t }; $last = $t.AddSeconds($sec)
                    $d = [int]$m.Groups['d'].Value
                    $s.Fights++; $s.Kills += [int]$m.Groups['k'].Value; $s.Deaths += $d; $s.FightSec += $sec
                    $s.LowSum += [int]$m.Groups['low'].Value
                    if ($d -gt 0) { $s.FightsWithDeath++ }
                    continue
                }
                if ($line.StartsWith('Before:')) { $g = $reGap.Match($line); if ($g.Success) { $s.Gaps++; $s.GapSec += [int]$g.Groups['g'].Value }; continue }
                $m = $reMember.Match($line)
                if ($m.Success -and $line.Contains(' hp ')) { $members[$m.Groups['n'].Value] = $m.Groups['cls'].Value.Trim(); continue }
                $m = $reDies.Match($line)
                if ($m.Success -and $members.ContainsKey($m.Groups['n'].Value)) { $c = $members[$m.Groups['n'].Value]; $s.DeathsByClass[$c] = 1 + [int]$s.DeathsByClass[$c]; continue }
                if ($line.StartsWith('Flags:')) {
                    foreach ($part in $line.Substring(6).Split(';')) {
                        $p = $part.Trim(); if (-not $p) { continue }
                        $mm = $reMelee.Match($p)
                        if ($mm.Success) { if ($casters -contains $mm.Groups['cls'].Value) { $s.CasterMelee += [int]$mm.Groups['n'].Value }; $key = 'caster/spec started melee' }
                        else { $key = ($p -replace '^\S+ ', '' -replace '\d+', 'N') }
                        $s.Flags[$key] = 1 + [int]$s.Flags[$key]
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
        $f = [double]$s.Fights; $sw = [math]::Max(1, $s.Swings); $gh = [math]::Max(1/60, $s.GroupSec / 3600)
        $rows += [pscustomobject]@{
            Run = $run.Name; AI = $ai; Kind = $kind; Logs = $s.Files; Fights = $s.Fights; Kills = $s.Kills; Deaths = $s.Deaths
            'Deaths/fight' = [math]::Round($s.Deaths / $f, 3)
            'Fights w/ death %' = [math]::Round(100 * $s.FightsWithDeath / $f, 1)
            'Kills/death' = if ($s.Deaths) { [math]::Round($s.Kills / $s.Deaths, 1) } else { $s.Kills }
            'Kills/log-hour' = [math]::Round($s.Kills / $gh, 1)
            'Avg fight s' = [math]::Round($s.FightSec / $f, 1)
            'Avg gap s' = if ($s.Gaps) { [math]::Round($s.GapSec / $s.Gaps, 1) } else { 0 }
            'Avg lowest hp %' = [math]::Round($s.LowSum / $f, 1)
            'Caster melee/fight' = [math]::Round($s.CasterMelee / $f, 3)
            'Swings side+back %' = [math]::Round(100 * ($s.Side + $s.Back) / $sw, 1)
            'Styled swings %' = [math]::Round(100 * $s.Styled / $sw, 1)
            'Spells/fight' = [math]::Round($s.Spells / $f, 2)
            'Heals/fight' = [math]::Round($s.Heals / $f, 2)
            'Unfinished casts/fight' = [math]::Round($s.NotFinished / $f, 2)
            'Shots/fight' = [math]::Round($s.Shots / $f, 2)
            'Did nothing/fight' = [math]::Round([int]$s.Flags['did nothing (no cast, no attack)'] / $f, 3)
            'Missed positional/fight' = [math]::Round([int]$s.Flags['swung from the side/back but used no positional style'] / $f, 3)
            'Missed reactive/fight' = [math]::Round(($s.Flags.GetEnumerator() | Where-Object { $_.Key -like 'missed * reactive*' } | Measure-Object Value -Sum).Sum / $f, 3)
            'Deaths by class' = (($s.DeathsByClass.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 6 | ForEach-Object { "$($_.Key) $($_.Value)" }) -join ', ')
        }
    }
}

if (-not $rows) { Write-Output "No run with $MinFights+ fights in $LogsDir."; return }
Write-Output "Per run:"
$rows | Format-List | Out-String -Width 200 | Write-Output

Write-Output "Per AI (all runs together, weighted by fights):"
foreach ($kind in 'group','solo') {
    $byAi = $rows | Where-Object Kind -eq $kind | Group-Object AI
    foreach ($a in $byAi) {
        $r = $a.Group; $f = ($r | Measure-Object Fights -Sum).Sum
        $w = { param($col) [math]::Round((($r | ForEach-Object { $_.$col * $_.Fights }) | Measure-Object -Sum).Sum / $f, 3) }
        Write-Output ("{0,-5} {1,-8} runs {2}, fights {3}: deaths/fight {4}, fights w/ death {5}%, kills/log-hour {6}, fight {7}s, gap {8}s, caster melee/fight {9}, side+back {10}%, styled {11}%, did nothing/fight {12}" -f `
            $kind, $a.Name, $r.Count, $f, (& $w 'Deaths/fight'), (& $w 'Fights w/ death %'), (& $w 'Kills/log-hour'), (& $w 'Avg fight s'),
            (& $w 'Avg gap s'), (& $w 'Caster melee/fight'), (& $w 'Swings side+back %'), (& $w 'Styled swings %'), (& $w 'Did nothing/fight'))
    }
}
if ($CsvOut) { $rows | Export-Csv -NoTypeInformation -Path $CsvOut; Write-Output "Written: $CsvOut" }
