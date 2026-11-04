---
schema_version: 1
class_name: "Hunter"
class_id: 25
realm: "Midgard"
primary_stats: ["Dexterity", "Quickness", "Strength"]
default_personality: "Tactical"
---

# Class: Hunter (Midgard)

## Shared Class Capabilities
- **Armor Type**: Studded (Level 20+), Leather (1â€“19).
- **Weapon Slots**: Composite Bow (Midgard's heavy recurve bow); Two-Handed Spear; One-Handed Sword.
- **Core Specialization Lines**:
  - `Composite Bow`: Rapid-fire long-range physical archery (Critical Shot, Power Shot, Rapid Fire, Point Blank Shot).
  - `Spear`: Heavy two-handed reach weapon line delivering long-reach stuns, bleeds, and high physical piercing burst.
  - `Beastcraft`: Nature summoning and companion magic, summoning an autonomous canine wolf/dog pet, self-buffs (strength, dexterity, armor factor), and pet stat enhancements.
  - `Stealth`: Concealed movement and camouflage.
- **Base Abilities**: Sprint, Stealth, Shoot, Trueshot.
- **Midgard's Wilderness Tracker**: Armed with a composite bow, a lethal hunting spear, and a loyal wolf companion, the Hunter stalks prey through snow and shadow, striking from range before impaling victims in close-quarters melee.

---

## Specialization 1: Spear Hunter
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Spear"
  min_point_ratio: 0.50
  spec_type_aliases: ["SpearHunter", "TwoHanded"]
```

### 1. Intended Role
Two-Handed Spear Striker, Canine Pet Harasser, Long-Reach Stunner, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Spear Hunter is Midgard's premier melee-archer hybrid. Combining 50 Spear with 39 Beastcraft, 35 Stealth, and 25 Composite Bow, it commands a vicious wolf companion pet, snipes targets from concealment, and transitions into devastating close-quarters combat with long-reach spear stuns and crushing physical burst.

### 3. Combat Philosophy
Long-reach melee assault and pet harassment. The Spear Hunter commands its pet to interrupt casters, closes distance with stealth, and executes targets with two-handed spear style chains.

### 4. Mode: RvR
- **Spear Ambush**: Open from stealth on priority enemy casters/healers with positional spear stuns; command wolf pet to attack casters to disrupt casting channels.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Pet & Spear**: Command wolf pet to attack -> Close in with spear -> Positional rear/side stun -> Execute high-damage spear chain.

### 6a. Mode: PvE Standard
- **Melee DPS & Pet Off-Tank**: Assist main tank on designated targets with spear styles; use wolf pet to off-tank stray adds.

### 6b. Mode: PvE Pet Pulling (Spear Peeler & Wolf Discipline Role)
- **Role During Pet Pulling**: The Hunter is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: WOLF PET ON PASSIVE AT CAMP**:
  - **Set the wolf companion pet strictly to PASSIVE at camp while the pull is underway!**
  - If the wolf attacks mobs while the pet puller is bringing them to camp, it will break mob pathing and pull aggro off the main pet!
- **Do NOT Shoot Arrows During Transit**:
  - Hold all bow shots while the pet is traveling to camp with the pull pack.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Hunter immediately steps in**:
  - Command the wolf pet to attack the loose add and engage it with heavy **Spear styles**!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Hunter steps in:
  - Command the wolf to attack the primary focus target and unleash heavy **Spear style chains**!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT MELEE-STEALTH SURVIVABILITY**. Reinforced Spear styles with rear stuns, high parry defense, and Stealth allow the Spear Hunter to infiltrate dungeon rooms and dispatch isolated targets safely in corridor corners.
  - **Corridor Pull Isolation**: Locate solitary target; open from stealth with spear stun or bow pull into cleared hallway.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Target dies in spear stun duration + positional combo.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Spear parry counters deal heavy damage.
  - **Restricted (Orange)**: Only engage with full endurance and active buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Infiltrate from stealth behind solitary target in dungeon room.
  - 2. Opener: Execute Spear rear stun opener or short bow pull into corridor.
  - 3. Spear Combo: Step to flank/rear during stun -> execute Spear style chain.
  - 4. Parry Counter: Parry incoming attack -> execute heavy spear counter-style.
  - 5. Finisher: Spear execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit in stealth when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Spear stun add -> Sprint retreat -> enter Stealth.

### 7. Offensive Priorities
1. Spear Style Burst & Wolf Attack on Loose Break-Off Add: Priority 100
2. Heavy Spear Style Chain on Focus Target (at 85% HP): Priority 90
3. Positional Rear / Side Spear Stun Style: Priority 85
4. Composite Bow Opener: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense Stance: Priority 85
4. First Aid (AtlasOF_FirstAid): Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Spear Burst / Pet Attack)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Spear DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Spear Weapon Styles: Priority 90 (At 85% mob health)
3. Wolf Pet Attack Command: Priority 85
4. Positional Stun Style: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Endurance**: Heavy 2H spear styles consume endurance; pace style chains and rely on Skald endurance chants.

### 13. What the AI SHOULD Do
- Keep the wolf pet on Passive at camp during pet pulling.
- Hold melee attacks and bow shots until the mob pack hits 85% health at camp.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let the wolf pet charge incoming mobs while they are traveling to camp.
- Never shoot arrows at mobs in transit.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Command Wolf Attack -> Charge Spear Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Command Wolf Attack -> Execute Spear Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, parry, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration)
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Spear damage)
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

## Specialization 2: Composite Bow Sniper Hunter
- **role**: RangedPhysicalDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "CompositeBow"
  min_point_ratio: 0.50
  spec_type_aliases: ["SniperHunter", "Ranged"]
```

### 1. Intended Role
Extreme-Range Composite Bow Sniper, Rapid Archery Artillery, Wolf Scout, and Single-Target Eliminator.

### 2. Intended Role Summary
The Composite Bow Sniper Hunter is Midgard's premier ranged assassin. Taking Composite Bow to 50 alongside 35 Beastcraft, 35 Stealth, and 35 weapon, it snipes enemy casters and support from 1875+ units, commands its wolf to track stealthers, and finishes targets with Rapid Fire volleys.

### 3. Combat Philosophy
Ranged sniping and pet screening. The Sniper Hunter establishes a concealed firing position at extreme range, picks off squishy targets with high-velocity arrows, and uses its wolf pet to screen against counter-attackers.

### 4. Mode: RvR
- **Caster Elimination**: Snipe enemy cloth casters and primary healers from maximum range with Critical Shot and Trueshot.
- **Positioning**: Elevated backline vantage point (1600â€“1875 units).

### 5. Mode: PvP
- **Snipe & Kite**: Critical Shot opener -> Trueshot -> Command wolf to intercept -> Rapid Fire -> Finish target.

### 6a. Mode: PvE Standard
- **Ranged DPS**: Snipe priority mobs; assist tank on called targets.

### 6b. Mode: PvE Pet Pulling (Sniper Role)
- **Role During Pet Pulling**: The Sniper Hunter is **NOT the puller**. It acts as **stationary ranged artillery**.
- **Wolf on Passive**: Keep wolf pet strictly on Passive at camp during the pull.
- **Hold Fire on Pull**: Strictly hold all bow shots while the pet is pulling mobs to camp.
- **Break-Off Add Defense**: If an add breaks off toward healers, shoot it with Rapid Fire or engage with melee styles.
- **THE 85% MOB HEALTH TRIGGER**: Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, open fire with heavy Composite Bow shots on the primary focus target!
- **Strict Stationary Discipline**: Stay put at the camp anchor with the group.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Gotar, Uppland, Raumarik). Open snowy plains provide the essential 1800â€“2000 unit clear sightlines required for Critical Shot openers, Pin/Snare arrows, and outdoor kiting.
  - **Dungeon Suitability**: **RESTRICTED / AVOID SOLO**. Confined dungeon corridors eliminate maximum bow range advantage and force immediate melee combat where a pure archer is at a severe disadvantage. Stay outdoors unless grouped.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 2 to 3 bow shots kill target before it reaches melee distance.
  - **Secondary (Yellow)**: Standard target. Open with Critical Shot, apply Pin/Snare arrow, step back and finish.
  - **Restricted (Orange)**: Only engage with full distance and clear outdoor retreat lane.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1800â€“2000 units) from stealth in open field.
  - 2. Opener: Critical Shot from stealth.
  - 3. Snare: Follow with Pin / Snare Arrow as target charges.
  - 4. Rapid Shots: Fire standard arrows while target crawls forward.
  - 5. Finisher: Quick melee strike or final arrow.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when Endurance >= 90%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Pin Arrow add -> turn 180Â° -> Sprint retreat -> enter Stealth.

### 7. Offensive Priorities
1. Rapid Fire / Melee on Loose Break-Off Add: Priority 100
2. Composite Bow Archery on Focus Target (at 85% HP): Priority 90
3. Critical Shot / Trueshot Opener (RvR): Priority 85
4. Power Shot on Armored Target: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Wolf Intercept Stance: Priority 90
3. Sprint Disengage: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 85 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Midline (1000â€“1200 units from mob cluster)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Composite Bow Archery: Priority 90 (At 85% mob health)
3. `AtlasOF_Trueshot`: Priority 85
4. Rapid Fire: Priority 80
5. `AtlasOF_FalconsEye`: Priority 75

### 12. Resource Management
- **Endurance**: Pace bow shots to avoid endurance depletion.

### 13. What the AI SHOULD Do
- Hold bow fire until the mob pack hits 85% health at camp.
- Keep the wolf pet on Passive at camp during pet pulling.
- Intercept any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never shoot arrows at mobs while they are in transit to camp.
- Never let the wolf pet attack mobs in transit.
- Never flee backwards out of camp to seek bow range.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Rapid Fire / Command Wolf Attack.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Fire Composite Bow Archery.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Command wolf to peel, fall back toward Healer, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_FalconsEye` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_FalconsEye` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_FalconsEye` (Rank 3) - 6 pts [Total: 15] (Archery critical hit chance)
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts bow damage)
8. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Beastcraft Master Hunter
- **role**: PetCommander
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Beastcraft"
  min_point_ratio: 0.50
  spec_type_aliases: ["BeastcraftHunter", "TwoHanded"]
```

### 1. Intended Role
Canine Companion Commander, Beast Buff Enhancer, Two-Handed Spear Brawler, and Add Interceptor.

### 2. Intended Role Summary
The Beastcraft Master Hunter pushes pet synergy to the maximum. By training 49 Beastcraft alongside 44 Spear and 35 Stealth, it commands a fully empowered, high-health wolf companion that deals heavy damage and disrupts enemy casters, while the Hunter fights alongside it with heavy spear style chains.

### 3. Combat Philosophy
Coordinated hunter-and-hound assault. The Hunter buffs its wolf companion with high-tier Beastcraft spells, commands it to tie down casters or peel attackers, and joins the fray with high-damage spear strikes.

### 4. Mode: RvR
- **Wolf Caster Lockdown**: Send empowered wolf pet to relentlessly attack enemy primary healers or casters, shutting down casting channels.
- **Spear Assist**: Strike targets engaged by the wolf with heavy spear styles.
- **Positioning**: Flank / Support screen.

### 5. Mode: PvP
- **Dual Pressure**: Wolf attacks opponent -> Hunter lands spear stuns -> Overpower target with combined physical burst.

### 6a. Mode: PvE Standard
- **Pet Off-Tank & Melee DPS**: Wolf tanks secondary mob; Hunter assists main tank with spear styles.

### 6b. Mode: PvE Pet Pulling (Beastcraft Peeler Role)
- **Role During Pet Pulling**: The Beastcraft Hunter is **NOT the puller**. It acts as **stationary pet support and break-off mob destroyer**.
- **Wolf on Passive at Camp**: Keep the wolf pet strictly on Passive at camp during pull transit.
- **Break-Off Add Interception**: If an add breaks toward healers, command the empowered wolf to attack it while the Hunter spears it down!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, command the wolf onto the focus target and execute spear styles.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (PET TANK EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO PET SURVIVABILITY**. The Beastcraft Hunter summons a permanent wolf/dog companion. In dungeons (Spindelhalla, Cursed Tomb), the pet acts as a front-line door-block and threat anchor in corridors, absorbing melee hits while the Hunter snipes with the Composite Bow or assists with Spear styles from behind the pet.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop pet grinding with zero downtime.
  - **Secondary (Yellow)**: Standard baseline target. Pet easily holds aggro.
  - **Restricted (Orange)**: Viable; buff pet with Beastcraft buffs and assist with bow shots.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-buff pet with Beastcraft stat and AF buffs; position pet at doorway.
  - 2. Opener: Command pet to attack target mob (/pet attack).
  - 3. Ranged Assist: Fire Composite Bow arrows into target while pet holds threat.
  - 4. Melee Finisher: Step in with Spear to execute target as it nears 20% HP.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Command pet to hold add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Spear Style Burst & Wolf Command on Loose Break-Off Add: Priority 100
2. Heavy Spear Style Chain on Focus Target (at 85% HP): Priority 90
3. Beastcraft Pet Buff Maintenance: Priority 85
4. Positional Spear Stun Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Wolf Intercept Command: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Spear Weapon Styles: Priority 90 (At 85% mob health)
3. Wolf Pet Attack Command: Priority 85
4. Beastcraft Buffs: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Endurance & Pet Mana**: Monitor endurance usage and maintain pet buffs prior to combat.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Keep the wolf pet on Passive at camp during pet pulling.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never let the wolf pet charge incoming mobs during transit.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Command Wolf Attack -> Charge Spear Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Command Wolf Attack -> Execute Spear Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Command wolf to peel, fall back toward Healer, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Spear damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
