---
schema_version: 1
class_name: "Scout"
class_id: 3
realm: "Albion"
primary_stats: ["Dexterity", "Strength", "Constitution"]
default_personality: "Tactical"
---

# Class: Scout (Albion)

## Shared Class Capabilities
- **Armor Type**: Studded (Level 20+), Leather (1â€“19).
- **Weapon Slots**: Longbow (Albion's massive long-range bow); One-Handed Slashing and Thrusting; Medium and Large Shields (unique among archers!).
- **Core Specialization Lines**:
  - `Longbow`: Extreme-range physical missile archery (Critical Shot, Power Shot, Rapid Fire, Point Blank Shot).
  - `Shields`: High-tier shield styles (Slam: 9-second stun), shield blocking with Large Shields, and defensive utility (`Engage`).
  - `Weapon Lines` (`Thrust`, `Slash`): One-handed weapon specialization for close-quarters melee combat.
  - `Stealth`: Concealed movement and camouflage.
- **Base Abilities**: Sprint, Stealth, Shoot, Trueshot.
- **Albion's Master Marksman**: Equipped with a devastating longbow and a heavy shield, the Scout snipes targets from extreme range, stuns pursuers with a 9-second shield slam, and escapes unseen into the shadows.

---

## Specialization 1: Shield Slam Archer Scout
- **role**: RangedPhysicalDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Longbow"
  min_point_ratio: 0.50
  spec_type_aliases: ["ShieldArcherScout", "OneHandAndShield"]
```

### 1. Intended Role
Extreme-Range Longbow Sniper, 9-Second Shield Slam Peeler, Stealth Scout, and Self-Reliant Archer.

### 2. Intended Role Summary
The Shield Slam Archer Scout is Albion's iconic "Slam Scout." By combining 44 Longbow with 42 Shield, 29 Thrust/Slash, and 35 Stealth, it delivers heavy long-range archery fire while possessing the deadly 9-second Shield Slam, allowing it to stun any melee enemy that closes the gap, back up, and fire Point Blank Shots into their face.

### 3. Combat Philosophy
Ranged elimination backed by close-quarters crowd control. The Scout snipes targets from concealment, falls back to camp, slams aggressive rushers with a 9-second shield stun, and executes them with point-blank archery.

### 4. Mode: RvR
- **Extreme-Range Sniping**: Engage enemy casters and support from 1875+ units with Critical Shot and Trueshot.
- **Slam Peel**: When enemy stealthers attack Albion support or close in, slam them with a 9-second shield stun, step back, and fire point-blank arrows.
- **Scouting**: Provide advance reconnaissance from stealth.
- **Positioning**: Elevated backline (1500â€“1800 units from enemy forces).

### 5. Mode: PvP
- **Snipe & Slam**: Critical Shot opener -> Trueshot -> Swap to Shield -> Shield Slam (9s stun) -> Backpedal 5 paces -> Point Blank Shot -> Finish with melee.

### 6a. Mode: PvE Standard
- **Ranged Puller / DPS**: Open on high-value targets with longbow; switch to shield and weapon if engaged in melee.

### 6b. Mode: PvE Pet Pulling (Archery Artillery & Break-Off Slam Peeler Role)
- **Role During Pet Pulling**: The Scout is **NOT the puller**. It acts as **stationary archery artillery and break-off mob peeler**.
- **Crucial Rule: NO SHOOTING MOBS DURING TRANSIT**:
  - **Do NOT fire arrows at mobs while the pet is in transit to camp!**
  - The mobs **must stay on the pet** so the pulsing damage shield deals continuous retribution damage.
  - Shooting arrows at mobs in transit pulls threat off the pet onto the Scout!
- **Break-Off Add Protection (9-Second Shield Slam)**:
  - If any mob breaks away from the pet toward the healers, **the Scout steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)**!
  - Hold the stunned mob in place and destroy it while keeping group healers 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Scout opens fire:
  - Unleash rapid **Longbow shots (Rapid Fire / Point Blank Shot)** into the primary focus target in the cluster!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or seek max range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outdoor Open-World Leveling Preferred; Dungeons Viable with Stealth Isolation**.
  - **Dungeon Suitability**: **MODERATE / TACTICAL SOLO**. Dungeons lack 2000-unit sightlines for long-range archery, reducing opening snipe damage. However, Stealth allows the Scout to infiltrate past linked mob packs to reach solitary targets in corridor corners. The Shield Slam spec provides a 9-second melee stun, neutralizing any mob that closes distance in tight hallways before finishing with Thrust styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Bow shot removes 50% HP; Shield Slam + Thrust styles finish with zero damage taken.
  - **Secondary (Yellow)**: Standard 1-on-1 target. Bow opener followed by Shield Slam lockdown.
  - **Restricted (Orange)**: Engage only with Sure Shot / Critical Shot available and full endurance.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Stealth into position at maximum corridor sightline (600â€“800 units) from target.
  - 2. Opener: Fire Longbow shot (Critical Shot / standard heavy arrow) from stealth.
  - 3. Weapon Swap: Immediately swap to Shield and One-Handed Thrust weapon as mob charges.
  - 4. Stun Lockdown: Land Shield Slam as mob enters melee range (9-second stun).
  - 5. Finisher: Step to flank/rear during stun -> execute Thrust style chain to execute mob.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Enter Stealth before sitting to rest in cleared corridor corners when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand and re-stealth when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Land Shield Slam on add -> activate Sprint -> retreat into stealth or exit dungeon.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Longbow Archery on Focus Target (at 85% HP): Priority 90
3. Critical Shot / Trueshot Opener (RvR): Priority 85
4. Point Blank Shot on Stunned Target: Priority 80

### 8. Defensive Priorities
1. Shield Slam on Melee Pursuer: Priority 100
2. Purge (AtlasOF_Purge): Priority 95
3. Large Shield Block Stance: Priority 90
4. Stealth Disengage: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 90 (For Archery DPS)
3. Enemy Light Armor Caster / Support: Priority 85 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Healer Side (within 150â€“200 units of group healer)
- **Pull Phase**: Stationary at camp anchor; never retreat or shoot early.

### 11. Ability Usage Priorities
1. Shield Slam Style: Priority 100 (On loose add)
2. `AtlasOF_Purge`: Priority 95
3. Longbow Archery: Priority 90 (At 85% mob health)
4. `AtlasOF_Trueshot`: Priority 85 (RvR opening burst)
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Endurance**: Archery draw and Shield Slams consume endurance; pace shots during prolonged combat.

### 13. What the AI SHOULD Do
- Hold all bow shots while the pet is bringing mobs to camp.
- Intercept and Shield Slam any loose mob that breaks toward healers.
- Fire Longbow arrows into the focus target once mobs hit 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never shoot arrows at incoming mobs while the pet is in transit.
- Never run away backwards out of camp to seek bow range.
- Never let a loose mob reach group healers without attempting a Shield Slam.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Equip Shield -> Execute Shield Slam (9s Stun).
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Fire Longbow Archery.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, enter block defense, fall back toward Cleric, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_FalconsEye` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_FalconsEye` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_FalconsEye` (Rank 3) - 6 pts [Total: 15] (Archery critical hit chance)
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Shield block rate)
8. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Pure Longbow Sniper Scout
- **role**: RangedPhysicalDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Longbow"
  min_point_ratio: 0.50
  spec_type_aliases: ["SniperScout", "Ranged"]
```

### 1. Intended Role
Extreme-Range Longbow Artillery, Single-Target Eliminator, Cloth Caster Killer, and Stealth Ambush Sniper.

### 2. Intended Role Summary
The Pure Longbow Sniper Scout dedicates 50 points to Longbow, maximizing arrow flight speed, damage, and critical shot multipliers. Striking from 1875+ units, it eliminates enemy casters and support before they can react, utilizing camouflage and distance as its primary defenses.

### 3. Combat Philosophy
Maximum distance elimination. The Sniper Scout sets up in elevated vantage points, delivers catastrophic opening shots with Critical Shot and Trueshot, and finishes targets from range.

### 4. Mode: RvR
- **Caster Elimination**: Target enemy primary healers and cloth nukers from maximum range; coordinate opening volleys with fellow archers.
- **Positioning**: Extreme range vantage point (1600â€“1875 units).

### 5. Mode: PvP
- **Snipe & Kite**: Critical Shot opener -> Trueshot -> Rapid Fire -> Kite with sprint if closed upon.

### 6a. Mode: PvE Standard
- **Long-Range Artillery**: Snipe priority mobs from range; assist tank on called targets.

### 6b. Mode: PvE Pet Pulling (Sniper Role)
- **Role During Pet Pulling**: The Sniper Scout is **NOT the puller**. It acts as **stationary ranged artillery**.
- **Hold Fire on Pull**: Strictly hold all bow shots while the pet is pulling mobs to camp.
- **Break-Off Add Defense**: If an add breaks off toward healers, shoot it with Rapid Fire or engage with melee styles.
- **THE 85% MOB HEALTH TRIGGER**: Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, open fire with heavy Longbow shots on the primary focus target!
- **Strict Stationary Discipline**: Stay put at the camp anchor with the group.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Salisbury Plains, Avalon Marsh, Dartmoor). Open zones provide the essential 1800â€“2000 unit clear sightlines required for Critical Shot openers, Pin/Snare shots, and outdoor kiting.
  - **Dungeon Suitability**: **RESTRICTED / AVOID SOLO**. Confined dungeon corridors eliminate maximum bow range advantage and force immediate melee combat where a pure archer is at a severe disadvantage. Stay outdoors unless grouped.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 2 to 3 bow shots kill target before it reaches melee distance.
  - **Secondary (Yellow)**: Standard target. Open with Critical Shot, apply Pin/Snare shot, step back and finish.
  - **Restricted (Orange)**: Only engage with full distance and clear outdoor retreat lane.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1800â€“2000 units) from stealth in open field.
  - 2. Opener: Critical Shot from stealth.
  - 3. Snare: Follow with Pin / Snare Arrow as target charges.
  - 4. Rapid Shots: Fire standard arrows while target crawls forward.
  - 5. Finisher: Quick melee thrust if target reaches melee, or final arrow.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when Endurance >= 90%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Pin Arrow add -> turn 180Â° -> Sprint retreat -> enter Stealth.

### 7. Offensive Priorities
1. Rapid Fire / Melee on Loose Break-Off Add: Priority 100
2. Longbow Archery on Focus Target (at 85% HP): Priority 90
3. Critical Shot / Trueshot Opener (RvR): Priority 85
4. Power Shot on Armored Target: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Shield Block Stance: Priority 85
3. Sprint Disengage: Priority 80
4. First Aid: Priority 75

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 85 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Midline (1000â€“1200 units from mob cluster)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Longbow Archery: Priority 90 (At 85% mob health)
3. `AtlasOF_Trueshot`: Priority 85
4. Rapid Fire: Priority 80
5. `AtlasOF_FalconsEye`: Priority 75

### 12. Resource Management
- **Endurance**: Pace bow shots to avoid endurance depletion.

### 13. What the AI SHOULD Do
- Hold bow fire until the mob pack hits 85% health at camp.
- Intercept any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never shoot arrows at mobs while they are in transit to camp.
- Never flee backwards out of camp to seek bow range.
- Never break stealth prematurely before an RvR engagement.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Rapid Fire / Melee Strike.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Fire Longbow Archery.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, fall back toward Cleric, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_FalconsEye` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_FalconsEye` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_FalconsEye` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts bow damage)
8. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Melee Hunter Scout
- **role**: MeleeDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Thrust"  # Or SlashingWeapon
  min_point_ratio: 0.50
  spec_type_aliases: ["MeleeHunterScout", "OneHandAndShield"]
```

### 1. Intended Role
Melee Skirmisher, Shield Peeler, Stealth Ambusher, and Archery Harasser.

### 2. Intended Role Summary
The Melee Hunter Scout emphasizes one-handed weapon prowess (44 Thrust/Slash) backed by 35 Shield, 35 Longbow, and 36 Stealth. It opens engagements with stealth archery snipes, transitions into close-quarters melee combat, and provides reliable shield stuns and blocks.

### 3. Combat Philosophy
Ambusher and close-quarters brawler. The Melee Scout softens targets from stealth with archery, closes distance, and dismantles opponents with weapon styles and shield blocks.

### 4. Mode: RvR
- **Ambush & Engage**: Open with bow snipe from stealth -> Close distance -> Execute melee style chain with shield defense.
- **Positioning**: Flank / Support guard.

### 5. Mode: PvP
- **Snipe & Melee**: Bow opener -> Close in -> Shield stun -> Positional melee style chain.

### 6a. Mode: PvE Standard
- **Melee DPS & Peeler**: Assist tank on focus target; slam secondary adds with shield.

### 6b. Mode: PvE Pet Pulling (Melee Peeler Role)
- **Role During Pet Pulling**: The Melee Scout is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with shield stun and weapon styles!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with weapon styles or bow shots.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE-HYBRID SURVIVABILITY**. Reinforced Thrust and Shield spec allow the Melee Scout to fight effectively in tight corridors after softening targets with short-range bow pulls.
  - **Corridor Pull Isolation**: Short-range bow pull into cleared hallway; shield stun and thrust.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Effortless single-mob farming.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Short bow pull from doorway.
  - 2. Swap to Shield + Thrust weapon.
  - 3. Opener: Shield stun on contact.
  - 4. Style Chain: Thrust positional combo.
  - 5. Finisher: Thrust execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit in stealth when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Shield stun add -> Sprint retreat.

### 7. Offensive Priorities
1. Shield Stun / Weapon Style on Loose Break-Off Add: Priority 100
2. Melee Weapon Styles on Focus Target (at 85% HP): Priority 90
3. Longbow Archery Opener: Priority 85
4. Positional Bleed Style: Priority 80

### 8. Defensive Priorities
1. Shield Block Stance: Priority 100
2. Purge (AtlasOF_Purge): Priority 95
3. Determination CC Reduction: Priority 90
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
2. Shield Stun Style: Priority 95
3. Melee Weapon Styles: Priority 90 (At 85% mob health)
4. Longbow Archery: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Endurance**: Balanced style rotation maintains strong endurance reserves.

### 13. What the AI SHOULD Do
- Hold melee attacks and bow shots until the mob pack hits 85% health at camp.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never abandon the healer to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Move into Melee -> Execute Shield Stun -> Weapon Styles.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Melee Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Enter shield block defense, fall back toward Cleric, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Shield block rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
