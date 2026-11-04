---
schema_version: 1
class_name: "Mercenary"
class_id: 11
realm: "Albion"
primary_stats: ["Strength", "Dexterity", "Constitution"]
default_personality: "Aggressive"
---

# Class: Mercenary (Albion)

## Shared Class Capabilities
- **Armor Type**: Chain (Level 20+), Studded (1â€“19).
- **Weapon Slots**: Dual Wield (One-Handed Slashing, Thrusting, Crushing in main and offhand); Small and Medium Shields.
- **Core Specialization Lines**:
  - `Dual Wield`: Albion's premier dual-weapon mastery line, unlocking lethal offhand strike chances, crushing side and rear positional style chains, stuns, and bleeds.
  - `Weapon Lines` (`Slash`, `Thrust`, `Crush`): Base weapon specialization dictating primary hand damage, opening styles, and defensive modifiers.
  - `Shields`: Defensive shield utility including Slam (9-second stun), shield blocking, and healer protection (`Guard`).
  - `Parry`: High passive melee attack deflection.
- **Base Abilities**: Sprint, Taunt, Flank, Dirty Tricks.
- **Albion's Dual-Wield Blender**: The Mercenary shreds through opposing ranks in a whirlwind of steel, delivering rapid two-weapon strikes from flanking angles, locking down enemy support with positional stuns, and shredding armor with relentless physical assault.

---

## Specialization 1: Pure Dual Wield Mercenary
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Dual_Wield"
  min_point_ratio: 0.50
  spec_type_aliases: ["DualWieldMercenary", "DualWield"]
```

### 1. Intended Role
Primary Dual-Wield Striker, Positional Flanker (Rear/Side Stuns & Bleeds), High-Sustained Physical DPS, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Pure Dual Wield Mercenary is Albion's ultimate melee damage engine. By taking Dual Wield to 50 alongside 50 in primary weapon specialization (Slash, Thrust, or Crush), it maximizes offhand swing chance and executes high-tier positional style chains, delivering relentless physical DPS and crippling enemy targets with positional stuns.

### 3. Combat Philosophy
Aggressive flanking and relentless attack speed. The Mercenary maneuvers to the flanks and rear of engaged targets, lands high-damage positional styles, and overwhelms opponents through dual-wield swing volume.

### 4. Mode: RvR
- **Melee Assist Train**: Charge alongside Albion Armsmen and Paladins; target called enemy casters or healers.
- **Positional Execution**: Maneuver to rear or side angles to execute positional stuns and bleeds.
- **Peel Enforcement**: Intercept enemy stealthers attacking Albion casters; shred them with dual-wield burst.
- **Positioning**: Flank / Melee assist train (tight on called target).

### 5. Mode: PvP
- **Flank & Shred**: Close distance -> Evade counter-strike -> Maneuver for side/rear positional style chain -> Land stun -> Execute with high-damage dual-wield finishers.

### 6a. Mode: PvE Standard
- **Flanking Melee DPS**: Assist main tank on designated targets; position behind mobs to execute rear style chains without taking front parry/block checks.

### 6b. Mode: PvE Pet Pulling (Flanking Shredder & Break-Off Interceptor Role)
- **Role During Pet Pulling**: The Mercenary is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the healers or casters, **the Mercenary steps in immediately**:
  - Engage the loose add with fast dual-wield weapon styles!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Mercenary steps into melee:
  - Maneuver to the flank or rear of the designated primary focus target in the cluster.
  - Unleash rapid **Dual Wield style chains** to carve through the pack one mob at a time!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **VERY HIGH SOLO SURVIVABILITY**. Chain armor, dual-wield parry defenses, and high sustained melee DPS allow Mercenaries to dispatch mobs rapidly in dungeon corridors.
  - **Corridor Pull Isolation**: Use throwing weapons or proximity pulling to draw single mobs out of crowded rooms into cleared hallway corridors. Position mob against corridor wall to execute flank and rear style chains.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fastest XP per hour; mobs melt under dual-wield swing speed with minimal endurance drain.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Parry reactions trigger heavy dual-wield counter-styles.
  - **Restricted (Orange)**: Engage only with Dirty Tricks and defensive cooldowns ready.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Proximity pull single mob into cleared hallway.
  - 2. Opener: Frontal style or parry reaction style on contact.
  - 3. Stun & Flank: Land positional side/rear style combo (Flank / dual stun styles).
  - 4. Burst: Unleash dual-wield bleed and damage chain.
  - 5. Finisher: Rapid dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit after every 2-3 kills when Health < 50% or Endurance < 25%.
  - **Safety Check**: Check corridor behind bot for wandering patrols before sitting.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Use Dirty Tricks / stun style on add -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Dual Wield Style Burst on Loose Break-Off Add: Priority 100
2. Positional Rear / Side Style Chain on Focus Target (at 85% HP): Priority 90
3. Anytime Dual Wield Style Chain: Priority 85
4. Flanking Bleed / Stun Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense Stance: Priority 85
4. First Aid (AtlasOF_FirstAid): Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Dual Wield Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Flanking DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target (avoids target block/parry)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Dual Wield Weapon Styles: Priority 95 (At 85% mob health)
3. Positional Stun Style: Priority 90
4. `AtlasOF_Determination`: Priority 85
5. `AtlasOF_Toughness`: Priority 80

### 12. Resource Management
- **Endurance Management**: Dual-wielding consumes endurance rapidly; pace style chains and rely on group Paladin Endosong.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Position on the rear or flank of focus targets to maximize dual-wield style hit rates.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never attack mobs from the front if flank or rear positioning is accessible.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Dual Wield Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Flank/Rear -> Execute Dual Wield Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Cleric, enter parry stance, and call for heals.

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

## Specialization 2: Shield Flanker Mercenary
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Dual_Wield"
  min_point_ratio: 0.50
  spec_type_aliases: ["ShieldFlankerMercenary", "DualWieldAndShield"]
```

### 1. Intended Role
Dual-Wield Peeler, 42 Shield Slam Stunner, Healer Guard Bodyguard, and Flexible Flanker.

### 2. Intended Role Summary
The Shield Flanker Mercenary merges dual-wield offense with heavy shield crowd control. By training 50 Dual Wield, 42 Shield, and 39 base weapon, it carries a medium shield for 9-second Shield Slams and healer `Guard` duties, while retaining the capability to draw two weapons and shred targets with high-damage style chains.

### 3. Combat Philosophy
Adaptive peeling and counter-striking. The Shield Flanker guards Albion's backline with shield stuns, intercepts threatening peelers, and swaps to dual weapons to execute stunned foes.

### 4. Mode: RvR
- **Healer Protection & Peel**: Guard Albion Clerics; land 9-second Shield Slams on enemy stealthers and melee rushers.
- **Dual-Wield Execution**: Once peel target is stunned, swap to dual wield and burst them down.
- **Positioning**: Midline anchor (200â€“300 units ahead of Cleric).

### 5. Mode: PvP
- **Slam & Shred**: Shield Slam (9s stun) -> Swap to dual wield -> Positional style chain -> Destroy opponent before stun expires.

### 6a. Mode: PvE Standard
- **Off-Tank / Peeler**: Guard healer; slam loose adds with shield; switch to dual wield to burn down focus targets.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Shield Peeler Role)
- **Role During Pet Pulling**: The Shield Mercenary is **NOT the puller**. It acts as **stationary healer bodyguard and break-off peeler**.
- **Guard Primary Cleric**:
  - Set **active `Guard` on the primary Cleric** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the Cleric at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Shield Slam Peeling)**:
  - If any mob breaks away from the pet toward the Cleric, **the Mercenary immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Destroy the stunned add while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Mercenary assists DPS on the primary focus target with dual-wield style chains, remaining within peeling distance of the Cleric.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT CONTROL & SURVIVABILITY**. Equipping a shield provides access to Shield Slam stun, allowing safe lockdown of mobs in tight corridors before swapping to dual-wield.
  - **Corridor Pull Isolation**: Pull single targets to corridor corners; slam-lock and flank.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Zero-risk grinding with shield stuns.
  - **Secondary (Yellow)**: Standard target. Slam stun ensures full positional combo lands.
  - **Restricted (Orange)**: Viable; long stun duration mitigates incoming damage.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Proximity pull target to safe corridor.
  - 2. Opener: Shield Slam (stun).
  - 3. Swap: Swap to Dual Wield offhand; step to mob's side/rear during stun.
  - 4. Burst: Land full dual-wield positional style chain.
  - 5. Finisher: Dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Dual Wield Weapon Styles on Focus Target (at 85% HP): Priority 90
3. Positional Side / Rear Dual Wield Style: Priority 85
4. Taunt Style on Break-Off Mob: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Keep active 100% of time)
2. Protect on Primary Cleric: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Cleric: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For Dual Wield DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Healer Side (within 150â€“200 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Cleric)
2. Shield Slam Style: Priority 95 (On loose add)
3. Dual Wield Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Shield Block: Priority 80

### 12. Resource Management
- **Endurance**: Balanced style rotation maintains strong endurance reserves.

### 13. What the AI SHOULD Do
- Keep Guard active on the primary Cleric at all times.
- Hold position at camp beside the Cleric during pet pulling.
- Shield slam and stun any loose add that breaks toward the healer.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the Cleric to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Cleric.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Cleric**: Select Add -> Execute Shield Slam Stun (9s) -> Swap to Dual Wield -> Finish Add.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute Dual Wield Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, enter block stance, rely on Cleric heals.

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
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Melee style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: High Parry Duelist Mercenary
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Dual_Wield"
  min_point_ratio: 0.50
  spec_type_aliases: ["ParryDuelistMercenary", "DualWield"]
```

### 1. Intended Role
Dual-Wield Melee Duelist, High-Parry Deflection Frontliner, Sustained Blender, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The High Parry Duelist Mercenary emphasizes personal survivability in close-quarters melee combat. By combining 50 Dual Wield with 44 base weapon and 38 Parry, it deflects a large volume of enemy melee strikes with weapon parries while delivering high-damage dual-wield style chains to dismantle opposing fighters.

### 3. Combat Philosophy
Defensive weapon deflection combined with offensive dual-wield execution. The Duelist absorbs incoming physical blows with parries and immediately retaliates with high-damage style counters.

### 4. Mode: RvR
- **Melee Assist Brawler**: Engage in frontline collisions; deflect enemy melee strikes with high parry while assisting on priority targets.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Duel & Deflect**: Parry enemy strikes -> Counter with high-damage dual-wield style chains -> Outlast opponent through deflection.

### 6a. Mode: PvE Standard
- **Melee DPS**: Assist main tank on designated targets; parry frontal attacks if holding secondary aggro.

### 6b. Mode: PvE Pet Pulling (Duelist Peeler Role)
- **Role During Pet Pulling**: The Duelist Mercenary is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Duelist immediately engages it**:
  - Intercept the loose add and shred it with dual-wield style chains while parrying its attacks!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Duelist assists DPS on the primary focus target with dual-wield style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH DUELING EFFICIENCY**. Maximum parry spec turns defensive weapon deflections into instant high-damage counter-strikes in narrow hallways.
  - **Corridor Pull Isolation**: Pull single mobs into hallways; parry and riposte.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Consistent parry chains cut down mobs effortlessly.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Defensive stance / Parry reaction opener.
  - 3. Counter-Strike: Trigger dual-wield riposte chain on parry deflection.
  - 4. Finisher: High-damage dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Dual Wield Style Burst on Loose Break-Off Add: Priority 100
2. Dual Wield Style Chain on Focus Target (at 85% HP): Priority 90
3. Positional Side / Rear Dual Wield Style: Priority 85
4. Parry Reactionary Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. High Parry Deflection Stance: Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Dual Wield Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Dual Wield Weapon Styles: Priority 90 (At 85% mob health)
3. Parry Reactionary Styles: Priority 85
4. `AtlasOF_Determination`: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Pace dual-wield style usage and rely on Paladin Endosong.

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
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Dual Wield Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Dual Wield Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Cleric, enter parry defense, call for heals.

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
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Boosts style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
