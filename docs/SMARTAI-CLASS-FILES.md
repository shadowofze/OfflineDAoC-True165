# SmartAI class files: how to create and edit them

SmartAI decides what a companion or gamebot does from a **class file**: one text file per class that describes each
of the class's specializations. Edit a file and the bots of that class change how they fight. You don't need to
build anything or restart the game.

This guide explains where the files are, what the engine reads from them, and how to check your changes. For what
SmartAI does and how to turn it on, see [SMARTAI.md](SMARTAI.md).

**What a class file can and can't change**

- It changes the bots' *choices*: heal or attack, which heal, when to save power, whether to melee, which realm
  abilities to use and when, and which specialization a bot plays.
- It can't change combat numbers (damage, heal amounts, costs, ranges, reuse timers) or the rules the bots follow.
  It can't give a bot a spell, style or ability it hasn't trained, or make SmartAI use an action it doesn't know yet.

## Where the files are

In your game folder:

```
runtime\server\smartai\classes\albion\cleric.md
runtime\server\smartai\classes\midgard\healer.md
runtime\server\smartai\classes\hibernia\druid.md
...
```

There is one file for each of the 39 classes and each of the 15 base classes (`fighter.md`, `albionrogue.md`,
`viking.md`, `guardian.md`...), in a folder for its realm. These are the files the server reads.

- Open them in any text editor (Notepad, Notepad++, VS Code). Save them as **UTF-8**.
- **Make a copy before you edit**, for example `cleric.md.bak`, so you can go back. A backup ending in `.md` is
  read as a second Cleric file and refused, so don't name it `cleric-old.md`.
- The file and folder names don't matter to the engine; the class is set inside the file. A file in the wrong realm
  folder only gives a warning.
- **Updates:** before the downloader replaces files, it checks that they are unchanged. If a new version updates a
  class file you edited, it reports that files were changed on this PC. Keep your edited copy somewhere safe and put
  the original back before you update.

**For developers:** the repository keeps the same files in three places, and they must stay identical:
`runtime/server/smartai/classes/<realm>/<class>.md` (the live copy),
`runtime/config/smartai/specs/<CLASS>_CLASS_TEMPLATE.md` and `docs/smartai/specs/<CLASS>_CLASS_TEMPLATE.md`.

## How to edit: change, reload, check

SmartAI must be on: tick **Use SmartAI** in the launcher's Options tab before you start the server.

1. **Edit** the class file and save it. The server can be running.
2. In the game, type **`/smartai reload`**. The server reads every class file again and answers with a summary such
   as `54 class(es), 148 spec(s) from 54 file(s); 0 error(s), 2 warning(s)`, followed by the first problems it found.
   - With **errors**, it keeps the files it was using, so nothing breaks mid-game. Fix the errors and reload.
   - `/smartai reload force` loads every file without errors. A class whose file has errors then uses the normal bot
     AI until you fix it.
   - Warnings don't stop a file from loading, but read them: they usually mean part of your edit is ignored.
3. **Target a bot** of that class and type **`/smartai spec`**. It shows what the engine took from your file:
   - which specialization the bot plays, and why;
   - every priority line it understood, as `Triage 100: 9.1 Lowest Effective HP% ...` with its condition;
   - the heal bands, the group heal rule and the realm abilities;
   - whether melee is allowed, and the mana floor.

   A line you wrote that isn't in this list is ignored by the engine.
4. **Watch it fight**, then type **`/smartai debug`** on the bot to see its last decisions.

Every `/smartai` command and its answer are saved to `runtime\logs\smartai-session-<server start>.log`. The full
list of problems is also in the server log.

`/smartai` is for GMs, and for players while the server property `smart_ai_player_tools` is on (the default).

## What a class file looks like

A class file is a Markdown text file. This outline shows its parts; `albion\cleric.md` is a complete example.

````markdown
---
schema_version: 1
class_name: "Cleric"
class_id: 6
realm: "Albion"
primary_stats: ["Piety", "Constitution"]
default_personality: "Supportive"
---

# Class: Cleric (Albion)

## Shared Class Capabilities
- Free text about the class: armor, weapons, lines. Not read by the engine.

---

## Specialization 1: Rejuvenation Cleric
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Rejuvenation"
  min_point_ratio: 0.50
  spec_type_aliases: ["RejuvCleric"]
```

### 1. Intended Role
### 2. Intended Role Summary
...
### 17. Realm Ability Progression Order

---

## Specialization 2: Enhancement Cleric
...
````

The rules of the layout:

- The file starts with the **front matter**: the lines between the first two `---` lines.
- `## Specialization N: Name` starts a specialization. The number is optional, but the **name must be unique** in
  the file.
- Any other `## ` heading is class-level text, and the engine doesn't use it.
- Inside a specialization, the `- **role**:` and `- **default_personality**:` lines and the ```` ```yaml ```` block
  come **before** the first `###` section.
- Sections are `### <number>. <title>`. The engine finds them by **number** (`1` to `17`, plus `6a`, `6b`, `6c`); the
  titles are for people. Only section 14's title matters (see [Section 14](#section-14-must-not-and-melee)).

## The front matter

| Key | What it is |
|---|---|
| `schema_version` | Always `1`. |
| `class_name` | The class's name, in English, as the game spells it (`Cleric`, `Runemaster`, `Bonedancer`). |
| `class_id` | The game's number for the class. It must be that class: `class_id 6 is Cleric, not 'Friar'` is an error. Copy it from the existing file. |
| `realm` | `Albion`, `Midgard` or `Hibernia`. |
| `primary_stats` | Text for people; not used. |
| `default_personality` | The personality for specializations that don't set their own (see [Personality](#role-and-personality)). |

Each class may have only one file. A second file for the same `class_id` is an error.

## Which specialization a bot plays

Each specialization has a ```` ```yaml match_specs ```` block that tells the engine when a bot plays it:

```yaml
match_specs:
  primary_line: "Rejuvenation"      # the spec line that defines this specialization
  min_point_ratio: 0.50             # share of the points that line must hold (above 0, at most 1)
  spec_type_aliases: ["RejuvCleric"]  # gamebots only: the bot spec types that mean this specialization
  secondary_line: "Shields"         # optional: tells apart specs that share a primary_line
  secondary_min_level: 36           # optional: the secondary line's level range
  secondary_max_level: 50           # optional
  fallback: true                    # optional: play this spec when nothing below decides
```

**How the engine picks:**

1. It adds up the points each bot has put into each specialization's `primary_line`. A line at level L holds
   L×(L+1)/2 − 1 points; level 1 is free.
2. The line with the most points wins, if it holds at least `min_point_ratio` of the points in all the file's primary
   lines.
3. If several specializations share that primary line, `secondary_line` decides:
   - first, a specialization whose secondary level range holds the bot's level in that line;
   - then, the one whose secondary line has the most points;
   - then, the one with no `secondary_line` (the "pure" spec).
4. If the winner is below its ratio: a **gamebot** whose rolled spec type is in a `spec_type_aliases` list plays that
   specialization; otherwise the bot plays its most-trained line anyway.
5. If nothing is trained past level 1, or two lines tie, the bot plays the first specialization marked
   `fallback: true`. With none, it uses the normal bot AI. The base-class files mark their first specialization this
   way, because a level 1-5 bot often has two lines at the same level.

**Hired companions** play only what their player trained. Their rolled spec type is never used.

Spell line names can be written with spaces or underscores (`Body Magic` or `Body_Magic`). An unknown line is an
error. `spec_type_aliases` must be bot spec type names such as `RejuvCleric`, `SmiteCleric`, `FireWiz` or
`BodyCab`; an unknown name is a warning and never matches.

Example from the Thane file. Three specializations share Stormcalling: Shields 36 and up is the Shield Thane,
Shields 2 to 35 the Battle Hybrid, and a Thane with no Shields the pure Stormcalling spec (no `secondary_line`):

```yaml
match_specs:
  primary_line: "Stormcalling"
  secondary_line: "Shields"
  secondary_min_level: 36   # Shields above 35: the Shield Thane
  min_point_ratio: 0.50
```

```yaml
match_specs:
  primary_line: "Stormcalling"
  secondary_line: "Shields"
  secondary_min_level: 2    # some Shields, 35 or less: the Battle Hybrid
  secondary_max_level: 35
  min_point_ratio: 0.50
```

A `#` after a space starts a comment in the yaml block.

Two specializations with the same primary line and no `secondary_line`, or with overlapping secondary level ranges,
get a warning: only the first can ever be chosen.

## Role and personality

```
- **role**: PrimaryHealer
- **default_personality**: Supportive
```

**role** is required. You can write any name, but certain words in it change behaviour:

| The role contains | Effect |
|---|---|
| `Healer` | The bot counts as a healer: it reports "healer under attack", "heavy group damage" and "healer low on power" to its group, and other bots treat it as the group's healer. |
| `Tank`, `Support`, `Buffer`, `Hybrid`, `CrowdControl`, `Peel` or `Guard` (and not `Healer`) | The bot peels: it goes to help when the group's healer is attacked. |

The roles in the current files are PrimaryHealer, DefensiveTank, MeleeDPS, StealthMeleeDPS, OffensiveCaster,
CasterDps, CrowdControl, CrowdControlCaster, Buffer, DefensiveBuffer, HybridSupport, HybridDamage,
RangedPhysicalDPS, PetPuller, PetCommander, PetMaster and DefensiveTankSupport.

**default_personality** is one of `Auto`, `Aggressive`, `Defensive`, `Tactical`, `Opportunistic`, `Supportive` or
`Conservative`. It applies when the player hasn't set one with `/smartai personality`. If it's missing, the class's
front matter default applies. [SMARTAI.md](SMARTAI.md#personalities) says what each one does.

## The 17 sections

Every specialization must have every section: `1` to `5`, `6a`, `6b`, `6c` and `7` to `17`. A missing section is an
error, so the class falls back to the normal bot AI. Most sections are descriptions for people; the engine reads
only some of them:

| Section | Title in the current files | Read by the engine |
|---|---|---|
| 1–3 | Intended Role, Role Summary, Combat Philosophy | No |
| 4, 5 | Mode: RvR, Mode: PvP | No |
| 6a, 6b, 6c | PvE Standard, PvE Pet Pulling, Solo Leveling & Dungeon | No |
| **7** | Offensive Priorities | **Yes**: priority lines |
| **8** | Defensive Priorities | **Yes**: priority lines |
| **9** | Target Priorities | **Yes**: the friendly lines (whom to heal) |
| 10 | Positioning Preferences | No |
| **11** | Ability Usage Priorities | **Yes**: realm abilities, Guard and Protect |
| **12** | Resource Management | **Yes**: mana floor, sitting between heals, heal thresholds |
| 13 | What the AI SHOULD Do | No |
| **14** | What the AI MUST NOT Do | **Yes**: a melee ban |
| 15 | Group Responsibilities (or Target Selection Matrix) | No |
| 16 | Situational Responses (or Fallback Behavior) | No |
| 17 | Realm Ability Progression Order | Checked, but not used yet |

The "No" sections still matter: they say how the class should be played, and they are the plan for what SmartAI
learns next. Keep them accurate.

## Priority lines (sections 7, 8, 9)

Each numbered line in sections 7, 8 and 9 is one rule:

```
1. Direct Damage (Smite): Priority 10 (Only if all group members > 90% HP, group mana > 60%, and target has < 15% HP)
```

The parts:

- **`1.`** a number, then a space.
- **A name**, which says *what* to do. The engine reads it for keywords; see below.
- **`: Priority N`**, the importance. Higher wins. **`Priority 0` turns the action off.**
- **`(condition)`**, optional: *when* the rule applies. See [Conditions](#conditions).

A numbered line without `Priority N` is a warning. Lines that start with `-` are notes and are not rules.

### What the name maps to

The engine has four kinds of action. It finds the kind from words in the name, checked in this order:

| The name contains | Action |
|---|---|
| `potion`, `cooldown`, `shield block`, `stance` | None: ignored (potions and stances aren't driven by SmartAI yet) |
| `melee`, `style`, `weapon` | **Full offense**: melee and styles (plus the bot's normal attack spells) |
| `heal`, `cure`, `triage`, `resurrect`, `rez` | **Triage**: heals, cures, resurrections |
| `buff`, `bubble`, `absorb`, `regen`, `armor factor` | **Support**: buffs and defensive spells |
| `damage`, `nuke`, `smite`, `dd`, `dot`, `bolt`, `blast`, `stun`, `mez`, `root`, `snare`, `debuff`, `lifetap`, `aoe`, `spell` | **Offensive spells**, no melee |
| anything else | None: ignored |

In **section 9 (Target Priorities)** only friendly lines count. A line with `lowest`, or with `group member` or `ally`
together with `hp`, `health`, `disease` or `mez`, is a triage rule. Enemy target lines are ignored; the group's
target choice handles those.

The words are matched anywhere, even inside other words: `dd` is also found in "add", and `dot` in "dotted". If a
line lands in the wrong action, reword it, then check with `/smartai spec`.

### How a bot chooses

On each combat think:

1. For each action, the engine takes the **highest-priority line whose condition holds now**. An action with no such
   line, or only lines at priority 0, is not a choice this think.
2. Some actions can never be chosen at a given moment: no spells while silenced, no attack on a mesmerized or rooted
   target (nobody breaks a mez), and no melee for a spec that bans it.
3. Each choice gets a score: its priority, plus these bonuses:

   | When | Score change |
   |---|---|
   | Triage, someone in the group is below 40% health | +40 |
   | Triage, someone is below 65% health | +20 |
   | Triage, the group called emergency healing | +30 |
   | Triage, against players with someone below 65% | +10 |
   | Personality, unless someone is below 40% | ×0.80 to ×1.25 |
   | Offensive spells, below the [mana floor](#section-12-mana-floor-sitting-between-heals-and-heal-thresholds) | ÷4 |
   | The same action as the last think (within 1.5 seconds) | +15 |

4. The highest score is tried first. If it has nothing to do (nobody to heal, no spell ready), the next is tried.
5. If nothing acts: a spec with [Sit Between Heals](#section-12-mana-floor-sitting-between-heals-and-heal-thresholds)
   may sit; otherwise a support bot does its normal support turn, a spec that bans melee holds still, and any other
   bot uses its normal AI for that think.

A spec that has any triage rule heals only through its triage rules. A spec with no triage rule keeps the normal
bot habit of healing while attacking.

### Conditions

The condition in brackets is split into parts at `,`, `;` and the word `and`. **Every part must hold.** Within a
part, `or` means either side may hold. The engine understands these parts:

| Write | Means |
|---|---|
| `group HP > 80%`, `all group members above 90%`, `ally HP < 50%` | The **lowest** health in the group is above or below N%. |
| `mana > 50%`, `mana drops below 25%`, `< 35% power` | The bot's **own** power is above or below N% (even if you write "group mana"). |
| `target has < 15% HP`, `target above 60%` | The bot's target's health is above or below N%. |
| `in combat`, `out of combat` | The bot is or isn't fighting. |
| `silenced`, `out of mana`, `out of power`, `OOM` | Silenced, or below 10% power. |
| `another healer is healing`, `other healer` | Another group member is healing. |
| `no other healer`, `only healer`, `sole healer`, `no healer` | Nobody else in the group is healing. |

For a number, the engine looks for `>`, `above` or `over` (or `<`, `below`, `under`) **before** the number, and for
the subject (`mana`/`power`, `group`/`ally`/`allies`/`member`, `target`) in the few words around it.

**A part the engine doesn't understand is skipped**, and it never blocks the rule. `(Only when the main tank is
taunting)` is therefore the same as no condition at all. In particular, the bot's **own** health ("self HP < 45%")
is not understood. `/smartai spec` shows the condition text next to each rule it understood.

## Section 11: realm abilities, Guard and Protect

Section 11 uses the same line format. Two kinds of line do something:

**Realm abilities.** A line whose name includes the ability's key in backticks fires that ability:

```
1. `Purge` (AtlasOF_Purge): Priority 100 (Use immediately if stunned/mezzed while any ally is < 50% HP)
```

The key must be written exactly, with the same capital letters. SmartAI can fire these abilities, each only when its
own situation applies **and** the line's condition holds:

| Key | Fires when |
|---|---|
| `AtlasOF_Purge`, `AtlasOF_PurgeReduced` | The bot is stunned or mesmerized. |
| `AtlasOF_IgnorePain`, `AtlasOF_IgnorePainTank` | In combat, below 50% health. |
| `AtlasOF_SecondWind` | In combat, below 25% endurance. |
| `AtlasOF_FirstAid` | Out of combat, below 75% health. |
| `AtlasOF_MCL` (Mystic Crystal Lore) | Out of combat, below 75% power. |
| `AtlasOF_MasteryOfConcentration` | A caster in combat that is being hit or has a melee enemy on it. |
| `AtlasOF_BunkerOfFaith` | In combat, with two members below 65% or one below 40%. |

Abilities are tried in priority order, at most one per second. The bot must have the ability (a companion gets the
ones its player chose) and it must be ready. Any other realm ability in section 11 is ignored.

**Guard and Protect on the healer.** A line with the word `Guard` or `Protect` that names a healer, in its name or
its condition, makes a tank keep that ability on the group's healer. The healer words are healer, cleric, druid,
shaman, friar, bard and priest:

```
1. Guard on Primary Cleric: Priority 100 (Never let drop)
2. Protect on Primary Cleric: Priority 95
```

This works in sections 7, 8, 9 and 11, for any line above priority 0.

## Section 12: mana floor, sitting between heals and heal thresholds

**Mana floor.** A bullet whose bold label contains `Mana Floor`, with a percentage:

```
- **Conservative Mana Floor**: 30% (Never exhaust mana on non-essential spells)
```

Below this much power, offensive spells score a quarter of their value, so heals and buffs come first. Heals and
buffs are never held back by it.

**Sitting between heals.** A bullet whose bold label contains `Sit Between Heals`, with a percentage:

```
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)
```

In a fight against monsters (never against players), a bot with this line sits down to regain power faster when
nothing in its spec has anything to do: no heal, cure, buff or spell. All of these must hold:

- the bot and every living group member near it are above the percentage;
- its power is not full;
- no enemy is targeting it, and it hasn't been hit in the last 4 seconds;
- it isn't casting, attacking or moving;
- the group's leader is standing still within 800 units.

It stands up to cast, attack or move, when an enemy hits it, and when any of these stops holding. After standing it
waits 2.5 seconds before it sits again. Sitting gives the player's sitting regeneration and nothing more; like a
sitting player, it can't evade, parry or block while seated. Without the line, the bot never sits in a fight. The
Mentalist, Druid, Healer, Shaman, Cleric, Friar, Warden and Bard files use 90% in every specialization except the
melee ones (Staff Friar, Battle Warden).

**Heal thresholds.** Bullets that name heal kinds in backticks, with a health range, tell a healer which heal to use:

```
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient` (Greater Heal)
  - Target HP 35%–70%: `HealBig` (Major Heal)
  - Target HP < 35%: `HealInstant` + `HealBig`
  - 3+ members < 70%: `HealSpread` / `HealGroup`
```

- **A range** `70%–85%` covers health from 70% up to just under 85%. Use an en dash, an em dash or a hyphen.
- **`< 35%`** (or `below` / `under 35%`) covers 0 up to just under 35%.
- **`3+ members < 70%`** is the group heal rule: when that many members are under that health, the group heal comes
  first.
- **Several heals in one line** are tried in order; the next is used if the first isn't ready.
- Separate two thresholds in one bullet with `;`.

The heal kinds are:

| Kind | Meaning |
|---|---|
| `HealEfficient` | The best-value single heal |
| `HealBig` | The biggest single heal |
| `HealInstant` | An instant heal |
| `HealGroup` (or `HealSpread`) | A group heal |
| `HealInstantGroup` | An instant group heal |
| `HealOverTime` | A heal over time |

When no band fits, or none of its heals is ready, the bot uses the normal heal choice. Health above every band
(here 85% and up) gets no special heal choice.

## Section 14: MUST NOT and melee

If section 14's title contains **MUST NOT** (for example `### 14. What the AI MUST NOT Do`), a bullet that bans melee
stops the specialization from ever meleeing:

```
- MUST NOT engage in melee combat under any circumstances.
```

A bullet counts as a ban when it contains "engage in melee", "melee combat" or "melee attack", and doesn't mention a
mez. `MUST NOT break mezzed targets with melee swings` is not a ban. A `Priority 0` melee line in section 7 bans melee
too:

```
2. Melee Attack: Priority 0 (Strictly disabled)
```

With a title like "SHOULD NOT", the bullets are only notes. The other MUST NOT rules are descriptions for people.
SmartAI's own hard rules (never break a mez, no spells while silenced) always apply.

## Section 17: realm ability order

Lines in this form are read and checked:

```
1. **Purge 1** (`AtlasOF_Purge`, Level 1): Essential CC cleanse.
```

An unknown ability key is a warning. The engine doesn't use this order yet: gamebots train their realm abilities as
before, and a companion's player picks its abilities.

## Recipes

**Make a healer heal earlier.** In section 12, raise the bands, and move the band below so no gap opens between
them. The Cleric's

```
  - Target HP 70%–85%: `HealEfficient` (Greater Heal)
  - Target HP 35%–70%: `HealBig` (Major Heal)
```

becomes

```
  - Target HP 75%–90%: `HealEfficient` (Greater Heal)
  - Target HP 35%–75%: `HealBig` (Major Heal)
```

and the small heal starts below 90% health instead of below 85%. Keep the backticks around the heal kinds.

**Make a hybrid stop meleeing.** In section 7, set its melee line to `Priority 0`, or add
`- MUST NOT engage in melee combat.` to a section 14 titled "What the AI MUST NOT Do".

**Make a caster save more power for emergencies.** Raise the mana floor in section 12, or add a power condition to
its damage line: `1. Single-Target Nuke: Priority 90 (mana > 40%)`.

**Make a support spec attack only when the group is healthy.**
`1. Direct Damage: Priority 60 (group HP > 80%, mana > 50%, another healer is healing)`.

**Use a realm ability sooner.** Add or raise its line in section 11, and loosen its condition. The ability's own
situation in the table above still has to apply.

**Add a specialization.**

1. Copy a whole `## Specialization` block from the same file, from the `##` heading down to the next `---`.
2. Give it a new name and its own `match_specs`. If it shares a `primary_line` with another, give it a
   `secondary_line`, or a secondary level range that doesn't overlap.
3. Rewrite the sections. Keep every section number.
4. Reload. On a bot trained that way, check with `/smartai spec` that it is matched "by trained points".

**Start a class file from scratch.** All 39 classes and the 15 base classes already have one, so copy the existing file and change it. If
you do write a new one, copy the front matter of the old file (the `class_id` must be right) and remove the old
file. Two files for one class is an error.

## Messages and what they mean

| Message | Fix |
|---|---|
| `missing front matter (the file must start with '---')` | The first line of the file must be `---`. |
| `class_id N is X, not 'Y'` | `class_id` and `class_name` don't agree. Copy them from the original file. |
| `'Name' is missing section 6c` | Add the section, even if it has only one line of text. |
| `'Name' has no '- **role**:' line` | Add the role line under the `## Specialization` heading, before the first `###`. |
| `'Name' has no ```yaml match_specs``` block` | Add the block before the first `###` section. |
| `unknown spec line 'X'` | Use the line's name as the game spells it (trainer window). |
| `min_point_ratio must be above 0 and at most 1` | Use a number such as `0.45`, with a dot. |
| `section 7 line has no 'Priority N'` (warning) | Write the line as `N. Name: Priority P (condition)`. |
| `... share primary_line ... with no secondary_line` (warning) | Only the first can be chosen; give the other a `secondary_line`. |
| `spec_type_aliases 'X' is not a bot spec type` (warning) | Fix the name, or remove it. |
| `unknown realm ability 'X'` (warning) | Check the key's spelling. |
| `repaired N garbled character(s)` (warning) | Save the file as UTF-8. |
| `class X is already defined by Y` | There are two files for one class; remove one (a backup copy ending in `.md` counts). |

## Questions

**My change does nothing.** Did `/smartai reload` say "the previous registry was kept"? Then there were errors.
If it loaded, check `/smartai spec` on the bot: is the bot playing the specialization you edited, and is your line
in the list? A line missing from the list either didn't match a keyword or doesn't have `Priority N`.

**A bot uses the normal AI.** `/smartai spec` says why: no class file loaded for its class, nothing trained past
level 1, or a tie between two lines.

**Will the file break my game?** No. A file with errors is refused, and its class keeps the normal bot AI. If SmartAI
itself hits a problem with a bot, that bot uses the normal AI for a while, and repeated problems turn SmartAI off for
the session.
