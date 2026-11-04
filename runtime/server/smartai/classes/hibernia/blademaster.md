---
schema_version: 1
class_name: "Blademaster"
class_id: 43
realm: "Hibernia"
primary_stats: ["Strength", "Dexterity", "Constitution"]
default_personality: "Aggressive"
---

# Class: Blademaster (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Reinforced (Level 20+), Leather (1–19).
- **Weapon Slots**: Dual Wield via Celtic Dual (Blades, Blunt, or Piercing in main hand and offhand); Small and Medium Shields.
- **Core Specialization Lines**:
  - `Celtic Dual`: Hibernia's dual-wield combat mastery, delivering high-frequency offhand double-strikes, side/rear stuns, armor-cleaving bleeds, and defensive style chains.
  - `Weapon Lines` (`Blades`, `Blunt`, `Piercing`): Base weapon specialization dictating damage type, opening chains, and armor penetration.
  - `Shields`: Defensive shield utility including Slam (9-second stun), shield blocking, and healer protection (`Guard`).
  - `Parry` & `Evade`: Advanced Triple Evade (highest passive melee evasion in Hibernia) combined with weapon parries.
- **Base Abilities**: Sprint, Taunt, Advanced Evade, Flank.
- **Hibernia's Graceful Whirlwind**: Dancing across the battlefield with unparalleled agility, the Blademaster evades incoming strikes with ease, slicing through armor with twin blades and executing lethal positional combos from enemy flanks.

---

## Specialization 1: Pure Celtic Dual Blademaster
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Celtic_Dual"
  min_point_ratio: 0.50
  spec_type_aliases: ["DualWield"]
```

### 1. Intended Role
Primary Dual-Wield Striker, Positional Flanker (Side/Rear Stuns & Bleeds), High-Evade Skirmisher, and Break-Off Add Shredder.

### 2. Intended Role Summary
The Pure Celtic Dual Blademaster is Hibernia's ultimate melee damage engine. Combining 50 Celtic Dual with 50 in primary weapon specialization (Blades or Blunt) and high Advanced Evade, it delivers non-stop twin-weapon strikes, dodges enemy physical attacks with high evasion rates, and locks down targets with positional Celtic Dual stuns.

### 3. Combat Philosophy
Agile evasion and relentless flanking assault. The Blademaster evades frontal attacks with Advanced Evade, maneuvers to the flanks and rear of enemy targets, and dismantles opponents with rapid Celtic Dual style chains.

### 4. Mode: RvR
- **Melee Assist Train**: Charge alongside Hibernian Heroes and Champions; target called enemy casters or healers with rapid twin-weapon style chains.
- **Positional Execution**: Maneuver to rear or side angles to execute high-damage stuns and bleeds.
- **Peel Enforcement**: Intercept enemy stealthers attacking Hibernian Bards or Druids; shred them with dual-blade burst.
- **Positioning**: Flank / Melee assist train (tight on called target).

### 5. Mode: PvP
- **Evade & Shred**: Close distance -> Evade opponent's opening strike -> Execute evade reactionary or positional Celtic Dual chain -> Land stun -> Finish with dual-wield executioners.

### 6a. Mode: PvE Standard
- **Flanking Melee DPS**: Assist main tank on designated targets; position behind mobs to execute rear style chains without taking front parry/block checks.

### 6b. Mode: PvE Pet Pulling (Flanking Blender & Break-Off Interceptor Role)
- **Role During Pet Pulling**: The Blademaster is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the healers or casters, **the Blademaster steps in immediately**:
  - Engage the loose add with rapid Celtic Dual weapon styles!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Blademaster steps into melee:
  - Maneuver to the flank or rear of the designated primary focus target in the cluster.
  - Unleash rapid **Celtic Dual style chains** to carve through the pack one mob at a time!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **VERY HIGH SOLO DPS SURVIVABILITY**. Reinforced Scale armor, triple evasion bonuses (Advanced Evade), and high sustained Celtic Dual style damage allow Blademasters to dispatch dungeon mobs rapidly.
  - **Corridor Pull Isolation**: Use throwing weapons or proximity pulling to draw single mobs out of crowded dungeon rooms into cleared hallways. Position mob against corridor wall to execute side and rear Celtic Dual styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fastest XP per hour; targets melt under dual swing speed with minimal endurance drain.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Evade reactions trigger heavy Celtic Dual counter-styles.
  - **Restricted (Orange)**: Engage only with Triple Wield / evasion cooldowns ready.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Proximity pull single mob into cleared hallway.
  - 2. Opener: Celtic Dual opening style or evade reaction counter on contact.
  - 3. Stun & Flank: Step to side -> land Celtic Dual side stun style.
  - 4. Burst: Unleash Celtic Dual bleed and high-damage combo during stun.
  - 5. Finisher: Rapid dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit after every 2-3 kills when Health < 50% or Endurance < 25%.
  - **Safety Check**: Check corridor behind bot for wandering patrols before sitting.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Side-stun add with Celtic Dual style -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Celtic Dual Style Burst on Loose Break-Off Add: Priority 100
2. Positional Rear / Side Celtic Dual Style Chain on Focus Target (at 85% HP): Priority 90
3. Evade Reactionary Celtic Dual Style: Priority 85
4. Flanking Bleed / Stun Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Advanced Evade Reaction Stance: Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid (AtlasOF_FirstAid): Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Celtic Dual Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Flanking DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Celtic Dual Weapon Styles: Priority 95 (At 85% mob health)
3. Positional Stun Style: Priority 90
4. `AtlasOF_Determination`: Priority 85
5. `AtlasOF_Toughness`: Priority 80

### 12. Resource Management
- **Endurance**: Celtic Dual consumes moderate endurance; rely on group Bard for endurance songs.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Exploit evade reactionary styles upon dodging enemy strikes.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never attack mobs from the front if flank or rear positioning is accessible.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Celtic Dual Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Flank/Rear -> Execute Celtic Dual Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, rely on high evade, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration)
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

---

## Specialization 2: Shield Flanker Blademaster
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Celtic_Dual"
  secondary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: ["DualWieldAndShield"]
```

### 1. Intended Role
Celtic Dual Peeler, 42 Shield Slam Stunner, Healer Guard Bodyguard, and Flexible Flanker.

### 2. Intended Role Summary
The Shield Flanker Blademaster merges dual-wield lethality with heavy shield crowd control. By training 50 Celtic Dual, 42 Shield, and 39 base weapon, it carries a medium shield for 9-second Shield Slams and healer `Guard` duties, while retaining the capability to draw twin weapons and shred targets with high-damage style chains.

### 3. Combat Philosophy
Adaptive peeling and counter-striking. The Shield Flanker guards Hibernia's backline with shield stuns, intercepts threatening peelers, and swaps to dual weapons to execute stunned foes.

### 4. Mode: RvR
- **Healer Protection & Peel**: Guard Hibernian Bards/Druids; land 9-second Shield Slams on enemy rushers and stealthers.
- **Celtic Dual Execution**: Once peel target is stunned, swap to dual wield and burst them down.
- **Positioning**: Midline anchor (200–300 units ahead of Bard/Druid).

### 5. Mode: PvP
- **Slam & Shred**: Shield Slam (9s stun) -> Swap to dual wield -> Positional style chain -> Destroy opponent before stun expires.

### 6a. Mode: PvE Standard
- **Off-Tank / Peeler**: Guard healer; slam loose adds with shield; switch to dual wield to burn down focus targets.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Shield Peeler Role)
- **Role During Pet Pulling**: The Shield Blademaster is **NOT the puller**. It acts as **stationary healer bodyguard and break-off peeler**.
- **Guard Primary Support**:
  - Set **active `Guard` on the primary Bard or Druid** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the support at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Shield Slam Peeling)**:
  - If any mob breaks away from the pet toward the support, **the Blademaster immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Destroy the stunned add while keeping the support core 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Blademaster assists DPS on the primary focus target with dual-wield style chains, remaining within peeling distance of the support.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT CONTROL & SURVIVABILITY**. Equipping a shield provides access to Shield Slam stun, allowing safe lockdown of mobs in tight corridors before swapping to Celtic Dual.
  - **Corridor Pull Isolation**: Pull single targets to corridor corners; slam-lock and flank.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Zero-risk grinding with shield stuns.
  - **Secondary (Yellow)**: Standard target. Slam stun ensures full positional combo lands.
  - **Restricted (Orange)**: Viable; long stun duration mitigates incoming damage.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Proximity pull target to safe corridor.
  - 2. Opener: Shield Slam (stun).
  - 3. Swap: Swap to Celtic Dual offhand; step to mob's side/rear during stun.
  - 4. Burst: Land full Celtic Dual positional style chain.
  - 5. Finisher: Dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Celtic Dual Weapon Styles on Focus Target (at 85% HP): Priority 90
3. Positional Side / Rear Celtic Dual Style: Priority 85
4. Taunt Style on Break-Off Mob: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Bard/Druid: Priority 100 (Keep active 100% of time)
2. Protect on Primary Bard/Druid: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Support: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For Dual Wield DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Support Side (within 150–200 units of guarded Bard/Druid)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Bard/Druid)
2. Shield Slam Style: Priority 95 (On loose add)
3. Celtic Dual Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Shield Block: Priority 80

### 12. Resource Management
- **Endurance**: Balanced style rotation maintains strong endurance reserves.

### 13. What the AI SHOULD Do
- Keep Guard active on the primary Bard/Druid at all times.
- Hold position at camp beside the healer during pet pulling.
- Shield slam and stun any loose add that breaks toward the support.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the support to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Bard/Druid.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Support**: Select Add -> Execute Shield Slam Stun (9s) -> Swap to Celtic Dual -> Finish Add.
- **Condition: Mob Pack at Pet & HP <= 85% & Support Safe**: Select Primary Focus Target -> Execute Celtic Dual Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, enter block stance, rely on Druid heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Boosts shield block rate)
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

## Specialization 3: Piercing Dual Blademaster
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Celtic_Dual"
  secondary_line: "Piercing"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Armor-Piercing Dual Striker, High-Burst Flanker, Chain/Scale Penetrator, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Piercing Dual Blademaster specializes in armor penetration. By pairing 50 Celtic Dual with 50 Piercing, it bypasses heavy chain and scale armor resistances, delivering devastating piercing positional strikes and critical style chains against high-armor frontline tanks and casters alike.

### 3. Combat Philosophy
Armor-piercing lethality. The Piercing Blademaster maneuvers behind heavily armored opponents, lands armor-piercing rear style chains, and deletes targets with high-penetration twin piercing strikes.

### 4. Mode: RvR
- **Heavy Armor Shredding**: Target high-armor enemy tanks (Armsmen, Paladins, Warriors); bypass armor with piercing damage type and positional stuns.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Armor Pierce Burst**: Evade counter-strike -> Maneuver to rear -> Execute piercing Celtic Dual chain -> Shred target through armor.

### 6a. Mode: PvE Standard
- **Piercing Melee DPS**: Assist main tank on designated targets; exploit piercing damage vulnerabilities on monster types.

### 6b. Mode: PvE Pet Pulling (Armor-Piercing Peeler Role)
- **Role During Pet Pulling**: The Piercing Blademaster is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Blademaster immediately engages it**:
  - Intercept the loose add and shred it with piercing style chains!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Blademaster assists DPS on the primary focus target with piercing Celtic Dual style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH PIERCING SPEED SURVIVABILITY**. Piercing weapon styles offer high attack speed and low endurance drain for prolonged corridor grinding.
  - **Corridor Pull Isolation**: Pull single mobs into hallways; evade and counter-strike.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Consistent evade chains cut down mobs effortlessly.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Piercing anytime opening style.
  - 3. Evade Counter: Evade deflection triggers high-damage riposte style.
  - 4. Finisher: Rapid piercing execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Piercing Celtic Dual Style Burst on Loose Break-Off Add: Priority 100
2. Piercing Style Chain on Focus Target (at 85% HP): Priority 90
3. Positional Rear Stun Style: Priority 85
4. Evade Reactionary Piercing Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Advanced Evade Reaction Stance: Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Piercing Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS)
3. Heavy Armor Targets (RvR): Priority 80 (For Armor Piercing)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear flank of primary target
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Celtic Dual Piercing Styles: Priority 90 (At 85% mob health)
3. Positional Stun Style: Priority 85
4. `AtlasOF_Determination`: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Pace style usage and rely on group Bard for endurance recovery.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Target the rear of armored enemies to land armor-piercing style chains.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Piercing Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Rear -> Execute Piercing Celtic Dual Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, rely on high evade, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Style damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
