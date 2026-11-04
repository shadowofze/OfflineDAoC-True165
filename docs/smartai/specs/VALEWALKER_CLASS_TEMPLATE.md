---
schema_version: 1
class_name: "Valewalker"
class_id: 56
realm: "Hibernia"
primary_stats: ["Intelligence", "Strength", "Constitution", "Dexterity"]
default_personality: "Aggressive"
---

# Class: Valewalker (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Cloth (imbued with self-buffed Arboreal Barkskin armor factor buffs that elevate physical mitigation to scale armor equivalents).
- **Weapon Slots**: Two-Handed Scythe (Hibernia's lethal two-handed curved harvesting scythe).
- **Core Specialization Lines**:
  - `Scythe`: Heavy two-handed scythe style chains delivering slashing, crushing, and cold damage, featuring rear/side stuns, snares, bleeds, and high physical burst.
  - `Arboreal Path`: Nature's combat sorcery, featuring Arboreal Barkskin (massive self-AF buff), offensive spell procs (weapon lifetaps, cold damage procs), Area of Effect (AoE) Roots, single roots, and instant nature DoTs.
  - `Parry`: Passive two-handed weapon deflection.
- **Base Abilities**: Sprint, Quick Cast.
- **Hibernia's Scythe-Wielding Reaper**: Channeling the raw power of the forest through a deadly two-handed scythe, the Valewalker clothes itself in impenetrable barkskin, siphoning life from foes with magical weapon procs while cleaving through enemy lines with sweeping harvesting strikes.

---

## Specialization 1: Arboreal Scythe Lifetapper Valewalker
- **role**: MeleeDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Arboreal_Path"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHanded"]
```

### 1. Intended Role
Nature Hybrid Striker, Barkskin Defense Tank, Lifetap Weapon Proccer, AoE Root Peeler, and Scythe Cleaver.

### 2. Intended Role Summary
The Arboreal Scythe Lifetapper Valewalker is Hibernia's premier hybrid melee-sorcerer. Combining 50 Arboreal Path with 39 Scythe, it operates under maximum Barkskin physical protection, imbues its scythe with high-potency lifetap procs that heal the Valewalker on hit, roots enemy peelers with AoE Roots, and delivers heavy two-handed scythe styles.

### 3. Combat Philosophy
Self-sustaining scythe combat and area control. The Valewalker keeps Barkskin and offensive procs active, controls enemy movement with roots, and sustains its health pool through continuous lifetapping scythe strikes.

### 4. Mode: RvR
- **Barkskin & Procs**: Keep 50-spec Barkskin AF and lifetap procs active at all times.
- **AoE Root Control**: Cast AoE Root to pin advancing enemy melee zergs before lines clash.
- **Melee Assist Burst**: Dive with Hibernia's frontline; execute scythe styles on called targets while lifetap procs sustain health under fire.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Root & Harvest**: Cast Root -> Close distance -> Apply scythe bleed and positional styles -> Siphon life with procs -> Outlast opponent through self-healing damage.

### 6a. Mode: PvE Standard
- **Self-Sustaining DPS**: Assist main tank on designated targets; rely on Barkskin and lifetap procs to absorb stray hits without draining healer mana.

### 6b. Mode: PvE Pet Pulling (Lifetap Scythe & Root Peeler Role)
- **Role During Pet Pulling**: The Valewalker is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst, lifetap peeler, and break-off mob destroyer**.
- **Crucial Rule: NO TAUNTING OR CASTING ROOTS ON THE MAIN PACK**:
  - **Do NOT cast AoE Root or spells onto the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Pre-Buffing at Camp**:
  - Keep **Barkskin AF buff** and **Lifetap Weapon Procs** active at all times.
- **Hold Melee Attacks and Spells During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Root & Scythe Peel)**:
  - If any mob breaks away from the pet toward the healers or casters, **the Valewalker steps in immediately**:
  - Cast **Single Root** or engage the loose add with heavy Scythe style chains!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Valewalker steps into melee:
  - Engage the primary focus target in the cluster with heavy **Scythe style chains**!
  - Lifetap procs will trigger on every swing, siphoning health and rapidly eliminating targets.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT MELEE LIFETAP SURVIVABILITY**. Reinforced Scythe styles with side stuns, Arboreal magic absorb buffs (granting high physical damage absorption to cloth armor), and built-in Scythe lifetap procs give the Valewalker exceptional sustain in dungeon corridors (Coruscating Mine, Treibh Caillte). Scythe lifetaps heal the Valewalker continuously while in melee combat, with zero cast interrupts.
  - **Corridor Pull Isolation**: Use ranged Arboreal DD from doorway to pull an isolated mob into a cleared corridor.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Scythe lifetaps keep Valewalker at 100% HP throughout combat.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Lifetap procs offset incoming melee damage.
  - **Restricted (Orange)**: Viable; ensure self-absorb buff and damage procs are active.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Maintain self-Arboreal Absorb buff and Scythe Damage Proc buff.
  - 2. Opener: Cast ranged Arboreal DD on target from 1000 units to initiate pull.
  - 3. Melee Contact: Execute Scythe opening style as mob closes distance.
  - 4. Stun & Flank: Step to side -> land Scythe side stun style (Foxfire / positional stun).
  - 5. Lifetap Combo: Channel Scythe styles; lifetap procs replenish health.
  - 6. Finisher: Heavy Scythe execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20% or Endurance < 25%. Scythe lifetaps keep Health high.
  - **Resume Threshold**: Stand when Mana >= 85% and Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Land Scythe side-stun on add -> cast instant Arboreal root -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Scythe Style Burst on Loose Break-Off Add: Priority 100
2. Heavy Scythe Style Chain on Focus Target (at 85% HP): Priority 90
3. Barkskin AF & Lifetap Proc Maintenance: Priority 85
4. AoE Root on Advancing Enemy Melee (RvR): Priority 80

### 8. Defensive Priorities
1. Barkskin AF Self-Buff: Priority 100 (Never let drop)
2. Purge (AtlasOF_Purge): Priority 95
3. Determination CC Reduction: Priority 90
4. Single Root on Attacking Melee: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Scythe Burst / Root)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Scythe DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. Barkskin AF Buff: Priority 100 (Pre-buff before combat)
2. `AtlasOF_Purge`: Priority 95
3. Scythe Weapon Styles: Priority 90 (At 85% mob health)
4. Lifetap Weapon Proc: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Barkskin and procs consume mana; monitor power pool and pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Keep Barkskin AF buff and lifetap procs running continuously.
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never cast AoE Root on the mob cluster around the pet during pull transit.
- Never enter combat without Barkskin AF active (cloth armor without Barkskin is lethal).

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Scythe Melee Style -> Cast Single Root -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Scythe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, cast defensive root on pursuer, rely on lifetap procs and parry.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10] (Power for Barkskin and weapon procs)
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20] (Reduces CC duration)
6. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 30] (Scythe style damage)
9. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 40] (Melee critical chance)
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Pure Scythe Executioner Valewalker
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Scythe"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Maximum Scythe Physical Striker, Positional Executioner (Rear Stuns & Bleeds), High-Burst Finisher, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Pure Scythe Executioner Valewalker maximizes two-handed scythe style lethality. With 50 Scythe and 43 Arboreal Path, it executes high-tier positional style chains that stun, snare, and inflict catastrophic slashing damage on priority targets, backed by dependable Barkskin protection and cold damage procs.

### 3. Combat Philosophy
Overwhelming physical scythe burst. The Executioner maneuvers behind enemy targets, lands lethal positional rear stuns, and shreds opponents with high-damage style chains.

### 4. Mode: RvR
- **Positional Scythe Assist**: Charge with Hibernia's melee assist train; execute rear positional stuns on called enemy casters or healers.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Rear Stun & Harvest**: Maneuver behind opponent -> Execute rear scythe stun -> Chain into high-damage executioners -> Finish before stun expires.

### 6a. Mode: PvE Standard
- **High-Burst Melee DPS**: Assist main tank on designated targets; execute positional scythe chains from behind mobs.

### 6b. Mode: PvE Pet Pulling (Scythe Enforcer Role)
- **Role During Pet Pulling**: The Pure Scythe Valewalker is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Valewalker immediately engages it**:
  - Intercept the loose add and shred it with heavy Scythe style chains!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Valewalker assists DPS on the primary focus target with 50-spec Scythe style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **MASSIVE MELEE BURST DAMAGE**. Pure Scythe spec maximizes two-handed scythe style damage, slicing through single mobs in corridors in 3 to 4 swings.
  - **Corridor Pull Isolation**: Ranged DD pull into safe hallway; scythe burst down.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 heavy swings.
  - **Secondary (Yellow)**: Standard baseline target. Parry reactions trigger heavy counters.
  - **Restricted (Orange)**: Viable with active absorb buff.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ranged DD pull from doorway.
  - 2. Opener: Scythe anytime opening style.
  - 3. Side Stun: Step to flank during combat -> execute Scythe stun.
  - 4. Finisher: Heavy Scythe execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Scythe stun -> Sprint away.

### 7. Offensive Priorities
1. Scythe Style Burst on Loose Break-Off Add: Priority 100
2. Heavy Scythe Style Chain on Focus Target (at 85% HP): Priority 90
3. Positional Rear Stun Style: Priority 85
4. Barkskin AF & Proc Maintenance: Priority 80

### 8. Defensive Priorities
1. Barkskin AF Self-Buff: Priority 100 (Keep active)
2. Purge (AtlasOF_Purge): Priority 95
3. Determination CC Reduction: Priority 90
4. Parry Defense: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Scythe Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Scythe DPS)
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear flank of primary target
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Barkskin AF Buff: Priority 100
2. Scythe Weapon Styles: Priority 90 (At 85% mob health)
3. Positional Stun Style: Priority 85
4. `AtlasOF_Purge`: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Endurance**: Heavy 2H scythe styles consume endurance; pace style chains and coordinate with group Bard.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Execute rear positional stuns on focus targets.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never enter combat without Barkskin active.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Scythe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Rear -> Execute Scythe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, parry, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10]
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 30] (Scythe damage)
9. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 40] (Melee critical chance)
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: High Parry Scythe Duelist Valewalker
- **role**: MeleeDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Scythe"
  secondary_line: "Parry"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Defensive Scythe Striker, Weapon Deflection Frontliner, Sustained Duelist, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The High Parry Scythe Duelist Valewalker emphasizes physical survival in melee scrums. By combining 50 Scythe with 26 Parry and 34 Arboreal Path, it deflects a large portion of enemy melee strikes with weapon parries while delivering high-damage scythe style chains to dismantle opposing fighters under Barkskin protection.

### 3. Combat Philosophy
Defensive weapon deflection combined with scythe counter-attacks. The High Parry Valewalker absorbs frontline pressure with parries and immediately retaliates with high-damage scythe combos.

### 4. Mode: RvR
- **Melee Assist Brawler**: Engage in frontline collisions; deflect enemy melee strikes with parry while shredding called targets with scythe styles.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Duel & Deflect**: Parry enemy strikes -> Counter with high-damage scythe combos -> Outlast opponent through deflection and Barkskin armor.

### 6a. Mode: PvE Standard
- **Melee DPS**: Assist main tank on designated targets; parry frontal attacks if holding secondary aggro.

### 6b. Mode: PvE Pet Pulling (Duelist Peeler Role)
- **Role During Pet Pulling**: The High Parry Valewalker is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Valewalker immediately engages it**:
  - Intercept the loose add and shred it with scythe style chains while parrying its attacks!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Valewalker assists DPS on the primary focus target with scythe style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH DEFENSIVE RIPOSTE EFFICIENCY**. High parry spec deflects incoming hits and triggers scythe counter-styles in tight corridors.
  - **Corridor Pull Isolation**: Pull single mobs to corridor; parry and counter.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, consistent grinding.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with full buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Defensive stance / Parry reaction opener.
  - 3. Counter-Strike: Trigger Scythe riposte chain on parry deflection.
  - 4. Finisher: High-damage Scythe execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Scythe Style Burst on Loose Break-Off Add: Priority 100
2. Scythe Style Chain on Focus Target (at 85% HP): Priority 90
3. Parry Reactionary Style: Priority 85
4. Barkskin AF & Proc Maintenance: Priority 80

### 8. Defensive Priorities
1. Barkskin AF Self-Buff: Priority 100
2. Parry Deflection Stance: Priority 95
3. Purge (AtlasOF_Purge): Priority 90
4. Determination CC Reduction: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Scythe Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Barkskin AF Buff: Priority 100
2. Scythe Weapon Styles: Priority 90 (At 85% mob health)
3. Parry Reactionary Styles: Priority 85
4. `AtlasOF_Purge`: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Endurance**: Pace style usage and rely on group Bard for endurance recovery.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Exploit parry reactionary styles upon deflecting enemy strikes.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never enter combat without Barkskin active.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Scythe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Scythe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, parry, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfParrying` (Rank 3) - 6 pts [Total: 25] (Weapon parry rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
