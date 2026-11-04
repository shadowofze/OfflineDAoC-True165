---
schema_version: 1
class_name: "Berserker"
class_id: 31
realm: "Midgard"
primary_stats: ["Strength", "Quickness", "Constitution"]
default_personality: "Aggressive"
---

# Class: Berserker (Midgard)

## Shared Class Capabilities
- **Armor Type**: Studded (Level 20+), Leather (1â€“19).
- **Weapon Slots**: Dual Wield via Left Axe (Sword, Axe, or Hammer in main hand, Left Axe in offhand).
- **Core Specialization Lines**:
  - `Left Axe`: Midgard's dual-wield combat mastery, delivering vicious two-axe style chains, side/rear stuns, armor-piercing bleeds, and relentless offhand double-strikes.
  - `Weapon Lines` (`Sword`, `Axe`, `Hammer`): Main hand weapon specialization determining primary weapon damage and style combos.
  - `Parry`: Melee attack deflection.
- **Base Abilities**: Sprint, Berserk / Vendo Form (transform into a ferocious bear/beast, granting guaranteed critical style strikes and massive damage at the cost of taking increased damage).
- **Midgard's Dual-Axe Frenzy**: The Berserker is a terrifying whirlwind of flying axes, diving into combat with wild abandon, ripping through enemy armor with Left Axe style chains, and transforming into the fearsome Vendo Form to slaughter priority targets in seconds.

---

## Specialization 1: Left Axe Blender Berserker
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Left_Axe"
  min_point_ratio: 0.50
  spec_type_aliases: ["LeftAxeBerserker", "LeftAxe"]
```

### 1. Intended Role
Primary Dual-Axe Striker, Positional Flanker (Rear/Side Stuns & Bleeds), High-Sustained Physical DPS, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Left Axe Blender Berserker is Midgard's premier physical meat-grinder. By maximizing Left Axe to 50 alongside 50 in primary weapon specialization (Sword, Axe, or Hammer), it attains near-guaranteed offhand double-swings, devastating positional style chains, and relentless melee pressure.

### 3. Combat Philosophy
Ferocious offensive pressure and flanking execution. The Berserker flanks engaged targets, lands brutal side/rear Left Axe style chains, and overwhelms opposing casters and healers with extreme swing volume.

### 4. Mode: RvR
- **Melee Assist Train**: Charge with Midgard's heavy frontline; focus called enemy casters or healers with Left Axe style chains.
- **Positional Execution**: Maneuver to rear or side angles to land high-damage stuns and bleeds.
- **Peel Enforcement**: Intercept enemy stealthers attacking Midgard Shamans or Healers; shred them with dual-axe burst.
- **Positioning**: Flank / Melee assist train (tight on called target).

### 5. Mode: PvP
- **Flank & Shred**: Close distance -> Evade counter-attack -> Maneuver for side/rear Left Axe style chain -> Land stun -> Execute with double-axe finishers.

### 6a. Mode: PvE Standard
- **Flanking Melee DPS**: Assist main tank on designated targets; position behind mobs to execute rear style chains without taking front parry/block checks.

### 6b. Mode: PvE Pet Pulling (Flanking Blender & Break-Off Interceptor Role)
- **Role During Pet Pulling**: The Berserker is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the healers or casters, **the Berserker steps in immediately**:
  - Engage the loose add with fast Left Axe weapon styles!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Berserker steps into melee:
  - Maneuver to the flank or rear of the designated primary focus target in the cluster.
  - Unleash rapid **Left Axe style chains** to carve through the pack one mob at a time!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **VERY HIGH SOLO DPS SURVIVABILITY**. Dual Left Axe styles deliver immense sustained physical DPS, cutting down dungeon mobs in seconds. Studded armor and parry defense keep the Berserker protected in corridor fights.
  - **Corridor Pull Isolation**: Use throwing axes or proximity pulling to draw single mobs out of crowded dungeon rooms into cleared hallways. Position mob against corridor wall to execute side and rear style chains.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fastest XP per hour; targets melt in under 5 seconds with minimal endurance drain.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Left Axe styles overpower mob quickly.
  - **Restricted (Orange)**: Engage only with Berserk / Vendo Form off cooldown.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Throwing axe pull single mob into cleared hallway.
  - 2. Opener: Left Axe anytime opening style or parry reaction counter.
  - 3. Stun & Flank: Step to side -> land Left Axe side stun style (Doublestrike).
  - 4. Left Axe Burst: Unleash Left Axe bleed and high-damage combo.
  - 5. Finisher: Rapid dual axe execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit after every 2-3 kills when Health < 50% or Endurance < 25%.
  - **Safety Check**: Check corridor behind bot for wandering patrols before sitting.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Side-stun add with Left Axe style -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Left Axe Style Burst on Loose Break-Off Add: Priority 100
2. Positional Rear / Side Left Axe Style Chain on Focus Target (at 85% HP): Priority 90
3. Anytime Left Axe Style Chain: Priority 85
4. Flanking Bleed / Stun Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense Stance: Priority 85
4. First Aid (AtlasOF_FirstAid): Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Left Axe Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Flanking DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target (avoids target block/parry)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Left Axe Weapon Styles: Priority 95 (At 85% mob health)
3. Positional Stun Style: Priority 90
4. `AtlasOF_Determination`: Priority 85
5. `AtlasOF_Toughness`: Priority 80

### 12. Resource Management
- **Endurance**: Left Axe consumes moderate endurance; rely on group Skald for endurance chants during sustained engagements.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Position on the rear or flank of focus targets to maximize Left Axe style hit rates.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never attack mobs from the front if flank or rear positioning is accessible.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Left Axe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Flank/Rear -> Execute Left Axe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, enter parry stance, and call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration)
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts melee damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: High Parry Berserker
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Left_Axe"
  min_point_ratio: 0.50
  spec_type_aliases: ["ParryBerserker", "LeftAxe"]
```

### 1. Intended Role
Dual-Axe Frontline Duelist, High-Parry Deflection Blender, Sustained Melee DPS, and Break-Off Interceptor.

### 2. Intended Role Summary
The High Parry Berserker balances raw dual-axe slaughter with exceptional defensive weapon deflection. By training 50 Left Axe alongside 44 base weapon and 37 Parry, it deflects a large portion of enemy melee counter-attacks with weapon parries while dishing out relentless dual-axe style chains.

### 3. Combat Philosophy
Defensive deflection fueling offensive counter-strikes. The High Parry Berserker absorbs frontline punishment with parries and retaliates immediately with high-damage Left Axe style counters.

### 4. Mode: RvR
- **Frontline Assist Brawler**: Push into frontline scrums; deflect melee retaliation with high parry while shredding called targets with Left Axe styles.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Duel & Deflect**: Parry enemy strikes -> Counter with high-damage Left Axe style chains -> Outlast opponent through deflection and attack speed.

### 6a. Mode: PvE Standard
- **Melee DPS**: Assist main tank on designated targets; parry frontal attacks if holding secondary aggro.

### 6b. Mode: PvE Pet Pulling (Duelist Peeler Role)
- **Role During Pet Pulling**: The High Parry Berserker is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Berserker immediately engages it**:
  - Intercept the loose add and shred it with Left Axe style chains while parrying its attacks!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Berserker assists DPS on the primary focus target with Left Axe style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT DEFENSIVE RIPOSTE**. High parry spec deflects mob strikes and triggers devastating Left Axe counter-styles in narrow hallways.
  - **Corridor Pull Isolation**: Pull single mobs into hallways; parry and riposte.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Effortless grinding with minimal health loss.
  - **Secondary (Yellow)**: Standard baseline target. Parry reactions trigger heavy counters.
  - **Restricted (Orange)**: Viable; high parry counters mob attack advantage.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Defensive stance / Parry reaction opener.
  - 3. Counter-Strike: Trigger Left Axe riposte chain on parry deflection.
  - 4. Finisher: Execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Stun style -> Sprint retreat.

### 7. Offensive Priorities
1. Left Axe Style Burst on Loose Break-Off Add: Priority 100
2. Left Axe Style Chain on Focus Target (at 85% HP): Priority 90
3. Positional Side / Rear Left Axe Style: Priority 85
4. Parry Reactionary Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. High Parry Deflection Stance: Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Left Axe Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Left Axe Weapon Styles: Priority 90 (At 85% mob health)
3. Parry Reactionary Styles: Priority 85
4. `AtlasOF_Determination`: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Pace style usage and rely on group Skald for endurance recovery.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Exploit parry reactionary styles upon deflecting enemy strikes.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Left Axe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Left Axe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, enter parry defense, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfParrying` (Rank 3) - 6 pts [Total: 25] (Maximum weapon parry rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Vendo Burst Frenzy Berserker
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Left_Axe"
  min_point_ratio: 0.50
  spec_type_aliases: ["VendoBurstBerserker", "LeftAxe"]
```

### 1. Intended Role
Vendo Form Shock Assassin, Critical Burst Striker, Priority Target Executioner, and Break-Off Add Vaporizer.

### 2. Intended Role Summary
The Vendo Burst Frenzy Berserker specializes in explosive shock warfare. By combining 50 Left Axe and 50 base weapon with timely activation of Vendo Form (Berserk), it guarantees 100% critical style strikes and massive damage amplification, annihilating priority enemy casters and support in a matter of seconds.

### 3. Combat Philosophy
All-or-nothing shock lethality. The Vendo Berserker identifies high-value targets, triggers Vendo Form upon closing distance, and deletes enemies in 2â€“3 strikes before defensive reactions can form.

### 4. Mode: RvR
- **Vendo Shock Charge**: Trigger Vendo Form when closing within melee range of enemy primary healers or cloth casters; land guaranteed critical Left Axe styles.
- **Burst Assist**: Vaporize called targets before enemy support can land heals.
- **Positioning**: Flank / Priority target dive.

### 5. Mode: PvP
- **Vendo Annihilation**: Close distance -> Pop Vendo Form -> Land guaranteed critical Left Axe positional chain -> Obliterate opponent in seconds.

### 6a. Mode: PvE Standard
- **Elite Mob Burn**: Activate Vendo Form on high-health boss or elite mobs to rapidly melt them down.

### 6b. Mode: PvE Pet Pulling (Burst Vaporizer Role)
- **Role During Pet Pulling**: The Vendo Berserker is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Berserker immediately engages it**:
  - Intercept the loose add and shred it with Left Axe style chains!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Berserker steps in:
  - Engage the primary focus target in the cluster with Left Axe style chains.
  - On tough high-health targets, pop **Vendo Form** to vaporize the focus mob immediately!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **BURST DISPATCH SPECIALIST**. Berserk mode turns every hit into a guaranteed critical strike, allowing the Berserker to vaporize tough single mobs in corridors before patrols arrive.
  - **Corridor Pull Isolation**: Pull single targets to cleared corners; trigger burst.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Instant kill turnover.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Prime target for Berserk cooldown window.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull target into safe hallway.
  - 2. Opener: Activate Berserk if high Yellow/Orange con.
  - 3. Frenzy Combo: Land high-damage Left Axe styles with 100% critical rate.
  - 4. Finisher: Left Axe execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Left Axe Style Burst on Loose Break-Off Add: Priority 100
2. Vendo Form Activation on High-Threat Target (at 85% HP): Priority 95
3. Left Axe Style Chain on Focus Target: Priority 90
4. Positional Stun / Bleed Style: Priority 85

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. First Aid (AtlasOF_FirstAid): Priority 85
4. Parry Defense: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Left Axe Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Vendo Burst)
3. Enemy Primary Healers / Casters: Priority 85 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Vendo Form (Berserk): Priority 95 (On priority target)
3. Left Axe Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Determination`: Priority 85
5. `AtlasOF_Toughness`: Priority 80

### 12. Resource Management
- **Vendo Vulnerability**: Vendo Form increases damage taken; ensure group Healer has line of sight and is ready to heal during frenzy.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Activate Vendo Form on high-priority targets to secure instant kills.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never pop Vendo Form when health is below 40% (avoids suicide).
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Left Axe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Trigger Vendo Form -> Execute Left Axe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Cancel Vendo Form if active, fall back toward Healer, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts style damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
