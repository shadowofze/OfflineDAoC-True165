---
schema_version: 1
class_name: "Ranger"
class_id: 50
realm: "Hibernia"
primary_stats: ["Dexterity", "Quickness", "Strength"]
default_personality: "Tactical"
---

# Class: Ranger (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Reinforced (Level 20+), Leather (1â€“19).
- **Weapon Slots**: Recurve Bow (Hibernia's rapid-firing composite bow); Dual Wield via Celtic Dual (Blades or Piercing in main hand and offhand).
- **Core Specialization Lines**:
  - `Recurve Bow`: High-velocity physical missile archery (Critical Shot, Power Shot, Rapid Fire, Point Blank Shot).
  - `Pathfinding`: Self-enhancement nature magic, featuring self Damage Adds, self Haste buffs, self Armor Factor (AF) buffs, and self movement speed enhancements.
  - `Celtic Dual`: Twin-blade melee style chains delivering offhand double-strikes, stuns, and bleeds.
  - `Weapon Lines` (`Piercing`, `Blades`): Base weapon specialization.
  - `Stealth`: Concealed movement and camouflage.
- **Base Abilities**: Sprint, Stealth, Shoot, Trueshot.
- **Hibernia's Guardian of the Woods**: Combining the swift lethality of the recurve bow with twin-blade Celtic Dual mastery and self-enhancing Pathfinding magic, the Ranger snipes foes from afar before slicing through survivors in close-quarters combat.

---

## Specialization 1: Recurve Bow Sniper Ranger
- **role**: RangedPhysicalDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "RecurveBow"
  min_point_ratio: 0.50
  spec_type_aliases: ["SniperRanger", "Ranged"]
```

### 1. Intended Role
Extreme-Range Recurve Bow Sniper, Rapid Missile Artillery, Pathfinding Buff Enhancer, and Single-Target Eliminator.

### 2. Intended Role Summary
The Recurve Bow Sniper Ranger is Hibernia's premier long-range physical killer. By training 45 Recurve Bow alongside 40 Pathfinding and 35 Stealth, it bolsters itself with high-tier self-buffs (haste, damage adds, AF), sets up in concealed vantage points, and eliminates enemy casters and support from 1875+ units with Critical Shot and Trueshot.

### 3. Combat Philosophy
Concealed long-range sniping supported by nature buffs. The Ranger activates Pathfinding self-buffs, snipes priority targets from maximum range, and uses speed to reposition or kite.

### 4. Mode: RvR
- **Caster Sniping**: Target enemy cloth casters and primary healers from 1875+ units with Critical Shot and Trueshot.
- **Pathfinding Buffs**: Maintain self AF, Damage Add, and Haste buffs at all times.
- **Positioning**: Elevated backline vantage point (1600â€“1875 units).

### 5. Mode: PvP
- **Snipe & Kite**: Critical Shot opener -> Trueshot -> Rapid Fire -> Swap to Celtic Dual if closed upon -> Finish with twin-blade styles.

### 6a. Mode: PvE Standard
- **Ranged DPS**: Snipe priority mobs from range; assist tank on called targets.

### 6b. Mode: PvE Pet Pulling (Sniper Role)
- **Role During Pet Pulling**: The Sniper Ranger is **NOT the puller**. It acts as **stationary ranged artillery**.
- **Pathfinding Pre-Buffing**: Keep self AF, Damage Add, and Haste buffs active at camp.
- **Hold Fire on Pull**: Strictly hold all bow shots while the pet is pulling mobs to camp.
- **Break-Off Add Defense**: If an add breaks off toward healers, shoot it with Rapid Fire or engage with Celtic Dual styles.
- **THE 85% MOB HEALTH TRIGGER**: Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, open fire with heavy Recurve Bow shots on the primary focus target!
- **Strict Stationary Discipline**: Stay put at the camp anchor with the group.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Shannon Estuary, Connacht, Bog of Cullen). Open green expanses provide the essential 1800â€“2000 unit clear sightlines required for Critical Shot openers, Pin/Snare arrows, and outdoor kiting.
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
  - 5. Finisher: Quick Celtic Dual strike if target reaches melee, or final arrow.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when Endurance >= 90%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Pin Arrow add -> turn 180Â° -> Sprint retreat -> enter Stealth.

### 7. Offensive Priorities
1. Rapid Fire / Celtic Dual on Loose Break-Off Add: Priority 100
2. Recurve Bow Archery on Focus Target (at 85% HP): Priority 90
3. Critical Shot / Trueshot Opener (RvR): Priority 85
4. Pathfinding Self-Buff Maintenance: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Pathfinding AF Self-Buff: Priority 95
3. Evade Defense Stance: Priority 85
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
2. Recurve Bow Archery: Priority 90 (At 85% mob health)
3. `AtlasOF_Trueshot`: Priority 85
4. Rapid Fire: Priority 80
5. `AtlasOF_FalconsEye`: Priority 75

### 12. Resource Management
- **Endurance & Mana**: Pathfinding buffs consume mana; bow shots consume endurance. Pace attacks.

### 13. What the AI SHOULD Do
- Hold bow fire until the mob pack hits 85% health at camp.
- Maintain Pathfinding self-buffs prior to combat.
- Intercept any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never shoot arrows at mobs while they are in transit to camp.
- Never flee backwards out of camp to seek bow range.
- Never break stealth prematurely before an RvR engagement.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Rapid Fire / Celtic Dual Melee.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Fire Recurve Bow Archery.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, rely on evade, call for heals.

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

## Specialization 2: Pathfinding Dual Blender Ranger
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Celtic_Dual"
  min_point_ratio: 0.50
  spec_type_aliases: ["PathfindingBlenderRanger", "DualWield"]
```

### 1. Intended Role
Hasted Dual-Blade Striker, Pathfinding Buffed Flanker, Sustained Melee DPS, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Pathfinding Dual Blender Ranger brings close-quarters ferocity to the woodsman archetype. By pairing 50 Celtic Dual with 40 Pathfinding, it enters melee combat heavily fortified with self-haste, bonus damage adds, and barkskin AF, shredding targets with rapid twin-blade style chains.

### 3. Combat Philosophy
Self-buffed dual-blade brawling. The Blender Ranger activates Pathfinding combat enhancements, closes the distance, and overwhelms targets with high-speed Celtic Dual style chains.

### 4. Mode: RvR
- **Melee Assist Striker**: Charge with Hibernian assist train; execute Celtic Dual style chains on priority targets under self-haste and damage adds.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Haste & Shred**: Pre-buff Pathfinding haste/damage add -> Close distance -> Positional Celtic Dual chain -> Finish target with rapid strikes.

### 6a. Mode: PvE Standard
- **Flanking DPS**: Assist tank on focus target; maintain self-buffs for maximum damage.

### 6b. Mode: PvE Pet Pulling (Blender Peeler Role)
- **Role During Pet Pulling**: The Blender Ranger is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with Celtic Dual style chains!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with Celtic Dual styles.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT HYBRID MELEE SURVIVABILITY**. Self-Pathfinding buffs (Damage Add buff, Self-AF, Str/Dex buffs, Self-Heal over Time) combined with Celtic Dual weapon styles allow the Ranger to fight effectively in dungeon corridors after softening targets with short-range bow pulls.
  - **Corridor Pull Isolation**: Short bow pull into cleared hallway; Celtic Dual flank and blend.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills; self-HoT and damage add buff keep downtime low.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline.
  - **Restricted (Orange)**: Only engage with active self-buffs and full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure Pathfinding self-buffs, damage add, and HoT are running.
  - 2. Opener: Short bow pull from doorway.
  - 3. Swap: Swap to Celtic Dual weapons as mob closes distance.
  - 4. Style Chain: Celtic Dual opening style -> flank combo.
  - 5. Finisher: Rapid dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit in stealth when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Pin Arrow add / Celtic Dual stun -> Sprint retreat -> enter Stealth.

### 7. Offensive Priorities
1. Celtic Dual Style Burst on Loose Break-Off Add: Priority 100
2. Celtic Dual Style Chain on Focus Target (at 85% HP): Priority 90
3. Pathfinding Haste / Damage Add Maintenance: Priority 85
4. Positional Flank / Rear Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Pathfinding AF Self-Buff: Priority 95
3. Evade Defense Stance: Priority 85
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
2. Celtic Dual Weapon Styles: Priority 90 (At 85% mob health)
3. Pathfinding Buffs: Priority 85
4. `AtlasOF_Determination`: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Pace Celtic Dual style usage and rely on group Bard for endurance recovery.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain Pathfinding haste and damage add buffs.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never enter melee without Pathfinding buffs active.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Celtic Dual Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Celtic Dual Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, rely on evade, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Celtic Dual style damage)
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

## Specialization 3: Balanced Woodsman Ranger
- **role**: HybridDamage
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Pathfinding"
  min_point_ratio: 0.50
  spec_type_aliases: ["BalancedScoutRanger", "Hybrid"]
```

### 1. Intended Role
Versatile Archer-Duelist, Bow Harasser, Twin-Blade Peeler, and Pathfinding Buffer.

### 2. Intended Role Summary
The Balanced Woodsman Ranger achieves total operational flexibility by training 40 Pathfinding, 35 Recurve Bow, 35 base weapon, 31 Celtic Dual, and 35 Stealth. It snipes incoming targets from range, transitions into hasted dual-wield melee when engaged, and provides agile backline defense.

### 3. Combat Philosophy
Adaptive hybrid warfare. The Balanced Ranger opens from range with archery, closes to melee when advantageous, and leverages Pathfinding magic to maintain superiority in both domains.

### 4. Mode: RvR
- **Adaptive Skirmishing**: Harass enemy lines with archery; switch to twin blades to peel enemy stealthers off Hibernian support.
- **Positioning**: Midline anchor (200â€“400 units from support).

### 5. Mode: PvP
- **Shoot & Slice**: Bow opener -> Close in -> Positional Celtic Dual style chain -> Finish with Point Blank Shot.

### 6a. Mode: PvE Standard
- **Hybrid DPS**: Open with bow; engage in melee on tank's target; maintain self-buffs.

### 6b. Mode: PvE Pet Pulling (Hybrid Peeler Role)
- **Role During Pet Pulling**: The Balanced Ranger is **NOT the puller**. It acts as **stationary ranged/melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with Celtic Dual styles!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with bow shots or melee styles.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **BALANCED ARCHERY & DUAL BLADES**. Uses stealth to infiltrate dungeon rooms, pulls solitary targets with bow, finishes with dual blades.
  - **Corridor Pull Isolation**: Short bow pull into safe corridor; melee finish.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Avoid solo.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Bow pull from stealth at corridor doorway.
  - 2. Swap to dual blades on contact.
  - 3. Positional style chain.
  - 4. Finisher: Dual execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit in stealth when resources < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat -> Stealth vanish.

### 7. Offensive Priorities
1. Celtic Dual Style Burst on Loose Break-Off Add: Priority 100
2. Recurve Bow / Celtic Dual on Focus Target (at 85% HP): Priority 90
3. Pathfinding Self-Buff Maintenance: Priority 85
4. Positional Stun / Bleed Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Pathfinding AF Self-Buff: Priority 95
3. Evade Defense Stance: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Midline (within 200 units of support)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Recurve Bow / Celtic Dual Styles: Priority 90 (At 85% mob health)
3. Pathfinding Buffs: Priority 85
4. `AtlasOF_Determination`: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance & Mana**: Balanced ability usage prevents resource exhaustion.

### 13. What the AI SHOULD Do
- Hold attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain Pathfinding self-buffs.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never shoot arrows at mobs in transit.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Celtic Dual Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Celtic Dual / Bow Attack.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, rely on evade, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_FalconsEye` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_FalconsEye` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_FalconsEye` (Rank 3) - 6 pts [Total: 15] (Crits)
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Damage)
8. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
