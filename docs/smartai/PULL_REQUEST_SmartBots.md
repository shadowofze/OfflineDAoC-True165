# SmartAI: a smarter brain for gamebots and hired companions

## Summary

This branch adds **SmartAI**, an opt-in decision layer that sits on top of the existing bot AI. It is for autonomous
gamebots and hired companions. With SmartAI on, each bot thinks with a spec written for its class: when to heal, buff,
nuke, melee, use a realm ability, peel for the healer or sit for power. Groups share what they see, choose camps they
can win at, travel together and learn which places kill them. When SmartAI has nothing better to do, the old bot AI
takes over, so nothing is lost.

It is switched on and off with **Use SmartAI** in the launcher. Off, the server runs exactly as before.

**Results from the first test runs.** The two runs were played back to back on the same world. Run 1 used the old AI
and run 2 used SmartAI; the full report is at the end.

| | Old AI | SmartAI | Change |
|---|---:|---:|---:|
| Group deaths per fight | 0.511 | 0.136 | **-73%** |
| Group kills per death | 2.0 | 8.7 | **4.3x** |
| Group kills per hour | 44.1 | 62.7 | **+42%** |
| Group fights with a death | 24.6% | 9.8% | **-60%** |
| Casters meleeing, per group fight | 1.88 | 0.45 | **-76%** |
| Group swings with a style | 41% | 55% | **+34%** |
| Solo deaths per fight | 0.129 | 0.089 | **-31%** |
| Solo kills per death | 5.8 | 8.8 | **+51%** |

These numbers come from one run of each AI. The bots kept their levels between runs, so run 2 started a little
stronger. More alternating runs are being recorded to confirm them.

## What SmartAI adds

### Thinking per class and spec
- **Class files** in `runtime/server/smartai/classes/<realm>/` cover all 39 classes. Base classes (Fighter, Mage,
  Mystic, Viking...) have files too, and fallback specs catch unusual builds. Each spec gives:
  - the role;
  - spell priorities;
  - heal thresholds;
  - whether melee is banned;
  - when to sit between heals;
  - a secondary line.

  `docs/SMARTAI-CLASS-FILES.md` explains how to write them; `/smartai reload` loads edits without a restart.
- **A decision engine** scores four kinds of action each think (heal, buff, offensive spells, full offense). The best
  one runs; when none applies, the old AI acts.
- **Personalities** (aggressive, defensive, tactical, supportive...) vary how bots of the same class play.
  `/smartai personality` sets a companion's.

### Healing, casting and power
- Heal thresholds per spec. Healers and casters rest below 90% power and **sit between heals** when the group is
  healthy.
- An Enhancement Cleric is not left as the group's only healer.
- **Casters stop meleeing:**
  - they wait for a spell to recharge instead of drawing a weapon;
  - they pick the strongest ready bolt or nuke instead of a random one;
  - casts are no longer refused or toggled every think while the caster is in melee.

  Caster melee per group fight fell from 1.88 to 0.45.
- **QuickCast** for list casters under pressure, and for Mentalist heals.
- **Solo caster kiting:** root (or stun) the monster, back off along a planned escape path clear of other aggressive
  monsters, then nuke.
- **Ice Theurgists** spread their ice pets.

### Melee, styles and abilities
- **Positional styles:**
  - grouped melee bots move to the monster's side or back when they have a style for it (flanking), with fixes for
    rubber-banding;
  - SmartAI picks a positional style it is in place for ahead of the queued or anytime style.
- **Stealth openers:** Infiltrators, Nightshades and Shadowblades hide and walk in to open on monsters with Backstab,
  Perforate Artery and the other stealth openers. Before, they only hid for RvR ambushes.
- **Class abilities:** Triple Wield, Stag, Flurry, Dirty Tricks, Berserk (with a health gate), Engage against archers,
  Guard and Protect on the healer, and interrupts on enemy casters.
- **Realm abilities:** Purge, Ignore Pain, Second Wind, First Aid, MCL, Mastery of Concentration and Bunker of Faith.
- **Archers** keep their bow drawn instead of cancelling the draw to try a spell every think.

### Groups and the world
- **Group blackboard:** members share targets and threats; callouts; arbitration of who does what.
- **Camp choice:**
  - camps are searched by area, using generated zone and dungeon level ranges (`state-165/smartai-world.json`,
    editable);
  - pulls are banded by con and gear score;
  - crowded camps are avoided.
- **Learning** (`state-165/smartai-learning.json`): deaths and stalls lower a camp's or area's score, and fade over
  time. A party that stands at its camp with nothing pulled moves on.
- **Travel:**
  - parties meet at the formation town and bind there;
  - they travel together and regroup at a safe spot;
  - members walking alone keep clear of aggressive monsters;
  - long walks are routed around dangerous areas (A* over the zone).
- **Solo bots:**
  - rest before travelling;
  - restart from their bind after a death;
  - a death-loop breaker stops a bot dying in the same spot again and again;
  - a learned per-spec con ceiling keeps weak specs off monsters they can't kill.
- **Pet pulling** (`/petpull`) for companions with pets.

### Diagnostics
- `/smartai status | spec | group | debug`. Players can use these while `smart_ai_player_tools` is on.
- A session log of every `/smartai` command and its answer.
- **The fight recorder** (`/smartai debugfights`, or `recordFightsFromStart` in `smartai-world.json`) writes each
  finished group or solo fight to `runtime/logs/smart_ai_groups` and `smart_ai_solo`:
  - the group's makeup;
  - the gap before the pull;
  - a timeline;
  - a summary per member, with flags.

  It records the old AI the same way, which is what makes the comparison below possible. Writes are batched, every 30
  seconds per file.
- `development-source/tools/smartai/Compare-FightLogs.ps1` turns those logs into an old-AI-against-SmartAI report
  (HTML and Markdown).
- 10-minute counter lines in the server log: `SMARTAI_FLANK`, `SMARTAI_ROUTE`, `SMARTAI_KITE`,
  `SMARTAI_STEALTH_OPENER`, `SMARTAI_CAMP_STALL`.

## Fixes that apply with SmartAI off too
- Monsters vanished when a hired or invited companion struck first, and healer bots vanished when their group broke
  up. Both are fixed.
- Bots are interrupted by their own melee the same way players are (3 seconds). This follows rule 4: bots live by the
  player rules.
- Stuck vendor walks are abandoned after 120 seconds. "Release" stays released, which ends a 15-minute party freeze at
  Lough Derg.
- Arrow restocking no longer retries every few seconds with nothing bought. It logs why
  (`BOT_RESTOCK_FAILED_165`: no coin, bag full) and waits 10 minutes.
- The game-loop watchdog names the step and the object a stuck worker is on, to find the cause of a server hang.

## Project rules
- No combat numbers, monster stats, XP or drops changed. SmartAI chooses between actions the game already allows, with
  the same cast times, ranges, endurance and power costs as a player.
- Bots follow player rules: no bot-only shortcuts. Positional and stealth styles use the game's own `StyleProcessor`
  checks.
- No Trials of Atlantis or later abilities. Realm abilities are the 1.65 set.
- Opt-in: with Use SmartAI off, only the fixes listed above change anything.

## Testing
- **Automated:** 3,343 NUnit tests pass (`dotnet test Tests/Tests.csproj`). They include tests for:
  - the class file parser and validator;
  - the decision engine;
  - flanking, kiting and routes;
  - the fight recorder;
  - the run 8 fixes.
- **In game:** about ten long server runs on a test copy, each reviewed through the fight logs and the server log.
  Each run's findings fed the next round of fixes; the run notes are kept outside the repository. The first old AI
  against SmartAI comparison is below. More alternating runs are being recorded.
- **Not yet checked in game:**
  - the run 8 fixes (stealth openers, bow draws, travel rest, restock);
  - Engage against archers;
  - the Bunker of Faith range (it has none, like the player handler).

## Known open items
- Group deaths are still highest among Vikings, Mystics and Seers.
- Some Warden and Valewalker specs still hand about a third of their thinks to the old AI.
- Solo bots still swing mostly from the front: flanking is a group behaviour.
- Not built yet:
  - a solo and dungeon strategy;
  - the rest of the group blackboard;
  - `bot-ai-delay.json` integration.

## How to try it
1. Tick **Use SmartAI** in the launcher, then START SERVER.
2. To compare with the old AI:
   - set `"recordFightsFromStart": true` in `runtime/server/state-165/smartai-world.json`;
   - alternate runs with the box unticked and ticked;
   - run `Compare-FightLogs.ps1 -Runs <run>,<run>`.

---

## Appendix: old AI against SmartAI, runs 1 and 2

Made 2026-11-06 11:53 from the fight recorder's logs. Runs: 20261106-090111, 20261106-100057. Runs with fewer than 20 fights are left out.
Caster melee counts pure caster classes only. Bots keep their levels between runs, so later runs start a little stronger.

### Group fights

#### Old AI against SmartAI

All runs of each AI added together: old AI 1 run(s), 788 fights; SmartAI 1 run(s), 1,276 fights. Change is SmartAI against the old AI (✅ better, ❌ worse).

| Measure | Old AI | SmartAI | Change |
|---|---:|---:|---:|
| Fights | 788 | 1,276 |  |
| Kills | 795 | 1,507 |  |
| Deaths | 403 | 174 |  |
| Deaths per fight | 0.511 | 0.136 | ✅ -73% |
| Fights with a death % | 24.6 | 9.8 | ✅ -60% |
| Kills per death | 2.0 | 8.7 | ✅ +339% |
| Kills per log-hour | 44.1 | 62.7 | ✅ +42% |
| Average fight (s) | 14.7 | 11.2 | ✅ -24% |
| Average gap between fights (s) | 71.1 | 57.6 | ✅ -19% |
| Average lowest health % | 57.4 | 71.4 | ✅ +24% |
| Caster melee starts per fight | 1.879 | 0.449 | ✅ -76% |
| Swings from side or back % | 25.0 | 33.4 | ✅ +33% |
| Swings with a style % | 41.0 | 55.0 | ✅ +34% |
| Spells landed per fight | 9.0 | 6.2 | -31% |
| Heals per fight | 2.3 | 1.1 | -51% |
| Unfinished casts per fight | 0.9 | 1.1 | ❌ +12% |
| Bow shots per fight | 0.2 | 0.1 | -75% |
| Members who did nothing, per fight | 1.385 | 1.170 | ✅ -15% |
| Missed positional styles per fight | 0.077 | 0.011 | ✅ -86% |
| Missed reactive styles per fight | 0.069 | 0.044 | ✅ -36% |

#### Member deaths by class

| Class | Old AI deaths (per 100 fights in) | SmartAI deaths (per 100 fights in) |
|---|---:|---:|
| Mystic | 27 (20.8) | 10 (10.9) |
| Viking | 27 (9.0) | 7 (12.1) |
| Fighter | 18 (27.7) | 11 (14.5) |
| Paladin | 19 (7.3) | 8 (3.0) |
| Mage | 6 (60.0) | 21 (20.4) |
| Scout | 10 (8.5) | 12 (5.8) |
| Seer | 16 (13.8) | 4 (3.3) |
| Reaver | 9 (8.7) | 9 (3.7) |
| Naturalist | 13 (25.5) | 5 (10.6) |
| Theurgist | 9 (8.4) | 7 (4.9) |
| Hero | 13 (30.2) | 3 (2.9) |
| Rogue | 12 (18.5) | 4 (1.8) |
| Druid | 5 (3.1) | 10 (4.8) |
| Guardian | 13 (16.5) | 2 (3.2) |
| Bard | 11 (7.2) | 3 (1.2) |

#### Every run side by side

Bold is the best run on that line.

| Measure | Old AI 20261106-090111 (group run 1 smartai off) | SmartAI 20261106-100057 (group run 2 smartai on) |
|---|---:|---:|
| Fights | 788 | 1,276 |
| Kills | 795 | 1,507 |
| Deaths | 403 | 174 |
| Deaths per fight | 0.511 | **0.136** |
| Fights with a death % | 24.6 | **9.8** |
| Kills per death | 2.0 | **8.7** |
| Kills per log-hour | 44.1 | **62.7** |
| Average fight (s) | 14.7 | **11.2** |
| Average gap between fights (s) | 71.1 | **57.6** |
| Average lowest health % | 57.4 | **71.4** |
| Caster melee starts per fight | 1.879 | **0.449** |
| Swings from side or back % | 25.0 | **33.4** |
| Swings with a style % | 41.0 | **55.0** |
| Spells landed per fight | 9.0 | 6.2 |
| Heals per fight | 2.3 | 1.1 |
| Unfinished casts per fight | **0.9** | 1.1 |
| Bow shots per fight | 0.2 | 0.1 |
| Members who did nothing, per fight | 1.385 | **1.170** |
| Missed positional styles per fight | 0.077 | **0.011** |
| Missed reactive styles per fight | 0.069 | **0.044** |

### Solo fights (a bot fighting alone)

#### Old AI against SmartAI

All runs of each AI added together: old AI 1 run(s), 21,291 fights; SmartAI 1 run(s), 23,402 fights. Change is SmartAI against the old AI (✅ better, ❌ worse).

| Measure | Old AI | SmartAI | Change |
|---|---:|---:|---:|
| Fights | 21,291 | 23,402 |  |
| Kills | 15,966 | 18,302 |  |
| Deaths | 2,756 | 2,087 |  |
| Deaths per fight | 0.129 | 0.089 | ✅ -31% |
| Fights with a death % | 12.9 | 8.9 | ✅ -31% |
| Kills per death | 5.8 | 8.8 | ✅ +51% |
| Kills per log-hour | 31.8 | 34.4 | ✅ +8% |
| Average fight (s) | 14.2 | 14.9 | ❌ +5% |
| Average gap between fights (s) | 72.2 | 68.3 | ✅ -5% |
| Average lowest health % | 72.5 | 74.8 | ✅ +3% |
| Caster melee starts per fight | 0.246 | 0.300 | ❌ +22% |
| Swings from side or back % | 8.5 | 7.0 | ❌ -17% |
| Swings with a style % | 27.4 | 30.1 | ✅ +10% |
| Spells landed per fight | 1.3 | 1.5 | +16% |
| Heals per fight | 0.0 | 0.0 | +15% |
| Unfinished casts per fight | 0.3 | 0.3 | ❌ +12% |
| Bow shots per fight | 0.0 | 0.1 | +19% |
| Members who did nothing, per fight | 0.051 | 0.049 | ✅ -4% |
| Missed positional styles per fight | 0.009 | 0.003 | ✅ -70% |
| Missed reactive styles per fight | 0.033 | 0.034 | ❌ +4% |

#### Member deaths by class

| Class | Old AI deaths (per 100 fights in) | SmartAI deaths (per 100 fights in) |
|---|---:|---:|
| Nightshade | 92 (34.5) | 96 (25.2) |
| Eldritch | 100 (12.8) | 85 (11.4) |
| Scout | 93 (43.5) | 90 (29.2) |
| Blademaster | 93 (39.6) | 85 (37.8) |
| Forester | 169 (57.9) | 6 (2.2) |
| Naturalist | 85 (18.7) | 77 (10.0) |
| Ranger | 72 (38.9) | 81 (43.5) |
| Acolyte | 141 (49.1) | 8 (5.6) |
| Rogue | 78 (7.1) | 71 (8.3) |
| Warden | 88 (46.8) | 60 (37.3) |
| Hero | 72 (29.4) | 68 (38.9) |
| Infiltrator | 73 (51.4) | 64 (32.7) |
| Wizard | 77 (20.6) | 53 (7.1) |
| Guardian | 40 (7.2) | 78 (15.1) |
| Mystic | 60 (7.3) | 56 (7.3) |

#### Every run side by side

Bold is the best run on that line.

| Measure | Old AI 20261106-090111 (solo run 1 smartai off) | SmartAI 20261106-100057 (solo run 2 smartai on) |
|---|---:|---:|
| Fights | 21,291 | 23,402 |
| Kills | 15,966 | 18,302 |
| Deaths | 2,756 | 2,087 |
| Deaths per fight | 0.129 | **0.089** |
| Fights with a death % | 12.9 | **8.9** |
| Kills per death | 5.8 | **8.8** |
| Kills per log-hour | 31.8 | **34.4** |
| Average fight (s) | **14.2** | 14.9 |
| Average gap between fights (s) | 72.2 | **68.3** |
| Average lowest health % | 72.5 | **74.8** |
| Caster melee starts per fight | **0.246** | 0.300 |
| Swings from side or back % | **8.5** | 7.0 |
| Swings with a style % | 27.4 | **30.1** |
| Spells landed per fight | 1.3 | 1.5 |
| Heals per fight | 0.0 | 0.0 |
| Unfinished casts per fight | **0.3** | 0.3 |
| Bow shots per fight | 0.0 | 0.1 |
| Members who did nothing, per fight | 0.051 | **0.049** |
| Missed positional styles per fight | 0.009 | **0.003** |
| Missed reactive styles per fight | **0.033** | 0.034 |
