---
schema_version: 1
class_name: "Runemaster"
class_id: 29
realm: "Midgard"
primary_stats: ["Piety", "Dexterity"]
default_personality: "Tactical"
---

# Class: Runemaster (Midgard)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Darkness`: Midgard's premier Point-Blank Area of Effect (PBAoE) Darkness Bomb (massive energy area damage), Darkness Direct Damage (DD), Darkness resistance debuff, and single-target Snare.
  - `Runecarving`: Heavy Cold Bolt (extreme-range artillery), Cold Direct Damage (DD), Cold resistance debuff, and Rune Damage Shields (castable on self, allies, and pets).
  - `Suppression`: Long-duration Area of Effect (AoE) Root, Single Root, Nearsight (reduces enemy spell and archery range by up to 70%), Suppression Direct Damage, and Rune of Grounding (deploys stationary ward that absorbs incoming magical spells).
- **Base Abilities**: Sprint, Quick Cast.
- **Midgard's Master of Runic Power**: The Runemaster channels ancient Norse runes to obliterate clustered enemies with devastating darkness pulses, snipe targets with frost bolts from extreme range, or blind opposing casters with crippling nearsight.

---

## Specialization 1: Darkness Runemaster
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Darkness"
  min_point_ratio: 0.50
  spec_type_aliases: ["DarkRune"]
```

### 1. Intended Role
Point-Blank Area of Effect (PBAoE) Darkness Bomber, Area Finisher, Darkness Direct Damage Burst Nuker, and Midline Cleaver.

### 2. Intended Role Summary
The Darkness Runemaster is Midgard's premier area devastation caster. Armed with crushing Point-Blank Area of Effect (PBAoE) Darkness spells, it vaporizes tightly clustered enemy packs, tears through opposing resistance barriers with Darkness debuffs, and cleans up survivors with high-impact single-target Darkness DD.

### 3. Combat Philosophy
Close-quarters area devastation and coordinated explosive burst. In RvR, the Darkness Runemaster pushes alongside Midgard's heavy frontline (Warriors, Savages, Berserkers), pops `Mastery of Concentration`, and detonates clustered enemies with rapid PBAoE pulses. In PvE pet pulling, it steps into point-blank range once the pull pack is anchored at 85% health and eradicates the cluster in seconds.

### 4. Mode: RvR
- **PBAoE Darkness Cleave**: Coordinate with Midgard's melee assist train; activate `Mastery of Concentration` or `Static Tempest` and detonate rapid PBAoE Darkness waves into clustered enemy zergs.
- **Darkness Debuff & Assist**: Cast Darkness resistance debuff on high-priority tanks or casters; assist fellow Midgard casters with Darkness DD.
- **Snare Defense**: Apply Darkness Snare to enemy peelers pursuing Midgard Shamans or Healers.
- **Positioning**: 1200–1350 units back during initial approach; collapse into PBAoE range under frontline cover.

### 5. Mode: PvP
- **Burst & Kite**: Open with Snare -> Quick Cast Darkness DD -> Pop `Mastery of Concentration` if closed upon and finish with PBAoE Darkness pulses.

### 6a. Mode: PvE Standard
- **Multi-Mob Bombing**: Unleash PBAoE Darkness pulses once Midgard tanks establish solid threat on multi-mob pulls.

### 6b. Mode: PvE Pet Pulling (The Darkness PBAoE Melting Role)
- **Role During Pet Pulling**: The Darkness Runemaster is **NOT the puller**. It acts as the **primary PBAoE area finisher**.
- **Mobs Stay on the Pet**:
  - Allow the pet puller's pet (Spiritmaster or Bonedancer) to absorb the pull and let its pulsing damage shield establish baseline aggro.
  - Strictly hold fire while mobs are in transit to camp.
- **Break-Off Add Protection**:
  - If any mob breaks off from the pet toward the group, cast **Darkness Snare** or baseline root on that loose add to freeze it away from the healers.
- **THE 85% MOB HEALTH PBAoE DARKNESS TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Darkness Runemaster activates:
  - Step into point-blank range of the cluster centered on the pet.
  - Unleash continuous, rapid **PBAoE Darkness pulses** to melt the entire pull pack down in seconds!
- **Strict Stationary Discipline on Pull**:
  - Remain strictly stationed at the camp anchor during the pull phase. Do NOT run away or flee backwards for range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Gotar, Raumarik, Uppland, Myrkwood). Open northern expanses afford essential 1500–2000 unit clear kiting lanes and safe pre-cast distances.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons (Spindelhalla, Cursed Tomb, Varv) have narrow winding corridors (<800 unit sightlines), low ceilings, and dense patrol links. Because physical hits cause 100% spell interrupts and Runemasters wear light cloth armor without a persistent tank pet, soloing in dungeons results in immediate interrupts and death. **Only enter dungeons when grouped with a dedicated chain tank and healer**.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Primary grinding target. Target melts under 2 to 3 Darkness nukes with zero spell resists.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Requires 100% Mana and clear backward retreat path.
  - **Restricted (Orange)**: Engage only if QuickCast is ready and wide kiting circle is clear of roaming adds.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; resists cause immediate gap-close and lethal melee beatdown.
- **Pull & Engagement Sequence**:
  - 1. Establish 1500-unit casting distance with clear open terrain behind the bot.
  - 2. Opener: Cast longest-range Darkness Direct Damage nuke.
  - 3. Secondary: Cast baseline Cold/Darkness Snare as mob closes within 1000 units.
  - 4. Kiting Cast: Step backward 300 units; cast secondary Darkness DD.
  - 5. Finisher: QuickCast nuke or instant baseline DD before mob enters melee swing range.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest immediately when Mana < 25% or Health < 50% post-combat.
  - **Safety Check**: Scan 1000 units for wandering snow bears or wolves before sitting; reposition into cleared clearings.
  - **Resume Threshold**: Stand and resume hunting only when Mana >= 85% and Health >= 90%.
- **Emergency Escape Protocol**:
  - On add or early melee close: QuickCast baseline Root / Darkness Snare -> 180° turn -> Sprint retreat to open safe zone.

### 7. Offensive Priorities
1. PBAoE Darkness Pulses: Priority 100 (Unleash into mob cluster anchored on pet at 85% HP)
2. Darkness Snare: Priority 85 (Apply to charging melee or loose break-off adds)
3. Darkness Resistance Debuff: Priority 80 (Cast on primary target before burst)
4. Spec Darkness DD: Priority 70 (Finishing single targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE casting)
3. Darkness Snare on Melee Pursuer: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Bomb)
2. Loose Break-off Add: Priority 90 (For Darkness Snare / baseline Root)
3. Primary Called Target: Priority 75 (For Darkness DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **PBAoE Range**: 0–350 units from the mob cluster centered on the pet (during 85% burn phase)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE bomb if under attack)
3. Spec PBAoE Darkness Spell: Priority 90 (At 85% mob health)
4. Darkness Snare: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **High Mana Consumption**: PBAoE drains significant power.
- **PBAoE Throttling**: Stop PBAoE when mana drops below 20% or when only 1 mob remains; switch to single-target Darkness DD.
- **MCL Timing**: Activate `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Step into point-blank range of the pet-anchored cluster at 85% HP and unleash PBAoE Darkness waves.
- Snare or root loose break-off adds to protect group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast PBAoE while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek range.
- Never spam PBAoE when only a single low-health monster remains.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Darkness Pulses.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Darkness Snare / Root.
- **Condition: Single Target Remaining**: Select Target -> Cast Darkness DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease PBAoE casting immediately, withdraw to midline, cast baseline DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Runecarving Runemaster
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Runecarving"
  min_point_ratio: 0.50
  spec_type_aliases: ["RuneRune"]
```

### 1. Intended Role
Extreme-Range Cold Bolt Sniper, Cold Direct Damage Burst Nuker, Cold Resistance Debuffer, and Rune Shield Provider.

### 2. Intended Role Summary
The Runecarving Runemaster is Midgard's premier long-range magical artillery. It opens combat from extreme distances with devastating Cold Bolts, applies crippling Cold resistance debuffs to focus targets, incinerates enemies with heavy Cold DD, and shields allies with protective Rune Damage Shields.

### 3. Combat Philosophy
Maximum distance elimination and burst execution. The Runecarving Runemaster sets up at extreme range (1875+ units), snipes high-value enemy targets before they can close distance, and finishes softened enemies with rapid Cold DD rotations.

### 4. Mode: RvR
- **Bolt Opening**: Open from maximum range (1875 units) on enemy cloth casters, healers, or archers with Cold Bolt.
- **Cold Debuff & Nuke**: Apply Cold resistance debuff to the assist target, then follow with rapid spec Cold DD nukes.
- **Rune Damage Shields**: Keep Rune Damage Shields active on Midgard tanks and melee peelers.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Bolt & Burst**: Cold Bolt opener -> Quick Cast Cold DD -> Cold Debuff -> Follow-up Cold DD to delete target.

### 6a. Mode: PvE Standard
- **Single-Target Sniper**: Cold Bolt opener on isolated targets; assist main tank with Cold Debuff -> Cold DD rotation.

### 6b. Mode: PvE Pet Pulling (Cold Sniper & Rune Shield Support)
- **Role During Pet Pulling**: The Runecarving Runemaster is **NOT the puller**. It acts as **stationary single-target burst artillery**.
- **Rune Shield Buffing**:
  - Prior to the pull, cast **Rune Damage Shield** onto the pet puller's pet! This stacks additional damage shield retribution against attacking mobs.
- **Hold Fire on Pull**: Strictly hold all bolts, debuffs, and DD spells while the pet puller brings the mobs back to camp.
- **Break-Off Add Protection**:
  - If any mob breaks away from the pet toward the group, cast **Cold DD** or baseline root on that loose add to quickly eliminate it.
- **THE 85% MOB HEALTH BURN TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Runecarving Runemaster activates:
  - Cast **Cold Resistance Debuff** on the primary focus mob in the cluster.
  - Follow immediately with heavy **spec Cold Direct Damage** nukes to incinerate the target!
- **Strict Stationary Discipline**:
  - Stay put at the camp anchor alongside the group. Do NOT run away or seek range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Gotar, Myrkwood, East Svealand). Open fields allow full utilization of ground-targeted Runes and long-range bolt/nuke openers.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Narrow dungeon corridors obstruct Ground-Targeted Area of Effect (GTAoE) runes and prevent necessary kiting distance. Pure cloth armor and zero tank pet make solo dungeon grinding non-viable. Group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: High turnover; runes and fire nukes destroy target before it approaches.
  - **Secondary (Yellow)**: Standard solo target. Lay fire rune trap in path of charging mob.
  - **Restricted (Orange)**: Only engage with QuickCast and full mana pool.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1500 units) in open field.
  - 2. Opener: Lay Rune trap on ground between bot and mob -> cast long-range Fire Bolt / Nuke.
  - 3. Trap Trigger: Mob charges across rune trap, triggering heavy fire detonation.
  - 4. Finisher: Cast follow-up Fire DD to vaporize target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Root -> Sprint retreat.

### 7. Offensive Priorities
1. Cold Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Cold Direct Damage: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Cold Bolt: Priority 80 (Long-range opener in RvR / PvE)
4. Baseline Root on Break-Off Add: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Rune Damage Shield: Priority 90 (Self/Ally/Pet buff)
3. Mastery of Concentration: Priority 85
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening healers: Priority 100 (For Cold DD / Root)
2. Primary Assist Kill Target: Priority 90 (For Cold Debuff -> Cold DD)
3. Enemy Caster / Healer: Priority 80 (For Cold Bolt in RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000–1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Rune Damage Shield: Priority 90 (Pre-buff on pet and group)
3. Cold Resistance Debuff: Priority 85
4. Spec Cold DD: Priority 80
5. `AtlasOF_MCL`: Priority 75

### 12. Resource Management
- **Bolt Power Efficiency**: Cold Bolts deal immense damage per point of power.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Pre-buff the puller's pet with Rune Damage Shield prior to pulls.
- Hold all damage spells while the pet is bringing mobs to camp.
- Cast Cold Resistance Debuff on focus targets once the pack hits 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast Cold Bolt or Cold DD while mobs are in transit to camp.
- Never run away backwards out of camp to seek casting range.
- Never cast Cold Bolt at point-blank range (bolts require range and clear trajectory).

### 15. Target Selection Matrix
- **Condition: Add breaks toward healer**: Select Add -> Cast Cold DD / Root.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Target -> Cast Cold Debuff -> Spam Cold DD.
- **Condition: Combat Start (RvR & Max Range)**: Select Target -> Cast Cold Bolt.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease debuffing; cast only baseline Cold DD on designated focus target.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Suppression Runemaster
- **role**: CrowdControlCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Suppression"
  min_point_ratio: 0.50
  spec_type_aliases: ["SuppRune"]
```

### 1. Intended Role
Midgard Crowd Control (Area & Single Root), Nearsight Caster Neutralizer, Suppression Direct Damage Nuker, and Rune of Grounding Warder.

### 2. Intended Role Summary
The Suppression Runemaster controls the tempo of battle through devastating mobility denial and range suppression. It immobilizes enemy forces with Area of Effect and Single Roots, cripples enemy casters and archers with Nearsight (reducing their effective range by up to 70%), deploys Runes of Grounding to absorb enemy spells, and chips away at priority targets with Suppression DD.

### 3. Combat Philosophy
Range suppression and area containment. The Suppression Runemaster blinds opposing casters with Nearsight, locks incoming melee trains in place with AoE Roots, sets protective spell-absorbing wards, and coordinates with Midgard nukers to eliminate isolated targets.

### 4. Mode: RvR
- **Nearsight Suppression**: Blanket enemy casters and archers with Nearsight from 1500 units away, forcing them into dangerous close range to cast.
- **Area Root Lockdown**: Cast AoE Root across charging enemy melee trains to isolate them from their support.
- **Rune of Grounding**: Deploy `Rune of Grounding` at the Midgard support anchor to absorb incoming enemy spell bombardment.
- **Positioning**: 1350 units back in the Midgard caster line.

### 5. Mode: PvP
- **Nearsight & Root**: Cast Nearsight on caster opponents -> Root -> Backpedal -> Suppression DD rotation.

### 6a. Mode: PvE Standard
- **Add Lockdown**: Root secondary adds on large pulls; Nearsight enemy caster mobs to force them into melee camp range; assist DPS with Suppression DD.

### 6b. Mode: PvE Pet Pulling (Crowd Control & Caster Mob Neutralization)
- **Role During Pet Pulling**: The Suppression Runemaster is **NOT the puller**. It acts as the **crowd controller and caster-mob neutralizer**.
- **Nearsight Caster Mobs**:
  - If ranged or caster mobs are pulled by the pet, **cast Nearsight on them**! This forces enemy caster mobs to run directly into melee range of the pet's pulsing damage shield.
- **Hold Direct Damage During Pull**:
  - Strictly hold fire on all direct damage while the pet is bringing mobs to camp.
- **Break-Off Add Protection (Immediate Root)**:
  - If any mob breaks away from the pet toward the group healers, **immediately cast Single Root or AoE Root** onto that loose add!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Suppression Runemaster assists by casting **Suppression DD** into the primary focus target.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor. Never flee backwards out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Raumarik, Gotar). Open zones allow full exploitation of Pulsing Bludgeon/Melee Shield (PBT) and cold snare kiting.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Despite self-PBT absorbing periodic melee hits, tight corridors and rapid patrol links quickly overwhelm the PBT cycle and cause fatal cast interrupts. Join a group for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: PBT absorbs virtually all melee hits while cold nukes shatter mob.
  - **Secondary (Yellow)**: Standard target. Maintain permanent Cold Snare while channeling Suppression nukes.
  - **Restricted (Orange)**: Engage only with self-PBT running and full mana pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure Suppression Pulsing Bludgeon/Melee Shield (PBT) is active.
  - 2. Opener: Cast Spec Cold Snare / Nuke from 1500 units.
  - 3. Kiting: Walk backward while channeling Suppression DD; PBT absorbs stray contact hits.
  - 4. Finisher: Rapid cold nuke to finish target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Drop to sit when Mana < 25%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast AoE Snare/Root -> Sprint retreat.

### 7. Offensive Priorities
1. Single / AoE Root on Loose Break-Off Add: Priority 100
2. Nearsight on Enemy Casters / Archers (RvR): Priority 95
3. Suppression Direct Damage on Focus Target: Priority 85
4. Rune of Grounding Deployment: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95
3. Single Root on Melee Attacker: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Root)
2. Enemy Primary Casters / Archers: Priority 90 (For Nearsight in RvR)
3. Primary Focus Target: Priority 80 (For Suppression DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000–1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. AoE / Single Root: Priority 95 (On loose adds or melee trains)
3. Nearsight: Priority 90 (On ranged casters)
4. Suppression Direct Damage: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Power Efficiency**: Roots and Nearsight are power-friendly; manage power during prolonged Suppression DD casting.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Cast Nearsight on enemy caster mobs during pulls to pull them into melee range of the pet.
- Root any loose add that breaks away from the pet toward group healers.
- Deploy Rune of Grounding to protect Midgard casters from enemy magical bombardment.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast damage spells while mobs are in transit to camp.
- Never flee backwards out of camp when mobs arrive at the pet.
- Never break existing roots on secondary mobs with direct damage.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Cast Single Root.
- **Condition: Enemy caster mob at range**: Select Mob -> Cast Nearsight.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Focus Target -> Cast Suppression DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease direct damage entirely; conserve power exclusively for emergency peel roots on group healers.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfTheArt` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]
