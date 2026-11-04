---
schema_version: 1
class_name: "Armsman"
class_id: 2
realm: "Albion"
primary_stats: ["Strength", "Constitution", "Dexterity"]
default_personality: "Defensive"
---

# Class: Armsman (Albion)

## Shared Class Capabilities
- **Armor Type**: Full Plate (Level 20+), Chain (1â€“19).
- **Weapon Slots**: One-Handed Slashing, Thrusting, Crushing; Polearms (Albion's signature long-reach heavy weapons); Two-Handed Weapons; Large Shields; Heavy Crossbows.
- **Core Specialization Lines**:
  - `Polearm`: Devastating long-reach weapon style chains featuring crippling rear/side stuns, snares, and immense physical burst damage.
  - `Shields`: High-tier shield styles (Slam: 9-second stun), high block chance with Large Shields, and vital party protection abilities (`Guard`, `Protect`, `Intercept`).
  - `Weapon Lines` (`Slash`, `Thrust`, `Crush`): One-handed weapon specialization delivering dependable baseline damage and opening styles.
  - `Two-Handed`: Heavy two-handed slashing, thrusting, and crushing weapons.
  - `Parry` & `Crossbow`: Passive melee deflection and heavy ranged physical missile fire.
- **Base Abilities**: Sprint, Taunt, Engage, Protect, Intercept.
- **Albion's Master of Arms**: Clad in impenetrable full plate armor, the Armsman wields the widest arsenal of weapons in the realm, capable of absorbing enormous punishment behind a tower shield or cleaving foes from reach with deadly polearms.

---

## Specialization 1: Polearm Hybrid Armsman
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "PolearmWeapon"
  min_point_ratio: 0.50
  spec_type_aliases: ["PolearmArmsman", "TwoHandHybrid"]
```

### 1. Intended Role
Reach Weapon Striker, 9-Second Shield Slam Peeler, Healer Bodyguard (`Guard`/`Protect`), Crossbow Sniper, and Hybrid Frontliner.

### 2. Intended Role Summary
The Polearm Hybrid Armsman is Albion's most feared combat specialist. Combining 50 Polearm with 42 Shield, it fluidly switches between a tower shield for 9-second Shield Slams and healer guarding, and a massive Polearm to deliver devastating positional reach strikes, stuns, and high physical burst against priority targets.

### 3. Combat Philosophy
Fluid defense and crushing counter-offense. The Polearm Armsman anchors close to Albion's primary healers with shield in hand, intercepts advancing enemies with 9-second shield stuns, and swaps to Polearm to execute targets with heavy reach style chains.

### 4. Mode: RvR
- **Crossbow Opener**: Open combat with heavy Crossbow shots onto enemy casters or support from 1800 units away.
- **Shield Slam & Guard**: Protect Albion's primary Cleric with active `Guard` and `Protect`; slam incoming enemy melee rushers or stealthers with 9-second Shield Slams.
- **Polearm Execution**: Swap to Polearm; execute rear or side positional styles on stunned targets to delete them rapidly.
- **Positioning**: Midline anchor (300 units ahead of Cleric, within immediate peeling range).

### 5. Mode: PvP
- **Slam & Polearm Chain**: Shield Slam (9s stun) -> Swap to Polearm -> Maneuver to rear -> Execute positional Polearm chain -> Finish with Crossbow shot if opponent flees.

### 6a. Mode: PvE Standard
- **Off-Tank / Main Tank**: Guard primary healer; hold secondary adds with shield slam; assist on focus target with heavy Polearm styles.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Break-Off Peeler Role)
- **Role During Pet Pulling**: The Armsman is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off mob peeler**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - Taunting mobs off the pet ruins pull efficiency and splits mob positioning.
- **Active Guard & Protect on Healer**:
  - Set **active `Guard` and `Protect` on the group's primary Cleric** before the pull begins.
- **Crossbow Discipline**:
  - Do NOT fire the crossbow at mobs while the pet is in transit (premature ranged attacks pull mobs off the pet).
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the Cleric or casters, **the Armsman steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or taunting strike!
  - Swap to Polearm to quickly obliterate that specific loose add while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Armsman assists the group:
  - Engage the primary focus target in the cluster with heavy **Polearm styles**!
  - Continue maintaining guard coverage over the Cleric.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Full plate armor, Crossbow pulling capability, and devastating Polearm style chains make the Armsman an exceptional solo dungeon fighter.
  - **Corridor Pull Isolation**: **Never charge into an uncleared room**. Stand in a cleared corridor bottleneck, equip Crossbow, and fire a single bolt at an isolated mob to pull it 600 units backward into the safe corridor. Switch immediately to Polearm as the mob closes distance.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 3 to 4 Polearm styles kill the target rapidly with low endurance consumption.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Parry reaction styles inflict massive damage.
  - **Restricted (Orange)**: Engage only with full endurance and defensive cooldowns available.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Equip Crossbow; locate isolated Blue/Yellow mob from doorway.
  - 2. Opener: Fire Crossbow bolt to initiate pull; immediately swap to Polearm.
  - 3. Intercept: As mob enters melee range, execute Polearm front stun or opening style.
  - 4. Style Chain: Follow up with high-damage Polearm positional/reaction combo.
  - 5. Finisher: Execution style to finish mob before second patrol paths near.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit in cleared corridor corner when Health < 50% or Endurance < 25%.
  - **Safety Check**: Verify line of sight down corridor is clear of patrolling mobs.
  - **Resume Threshold**: Stand when Health >= 90% and Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add joins: Swap to 1H + Shield -> Shield Slam stun add -> Sprint retreat down cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Polearm Style Chain on Loose Add: Priority 95
3. Polearm Weapon Styles on Focus Target (at 85% HP): Priority 85
4. Crossbow Shot on Fleeing / Ranged Target: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Never let drop)
2. Protect on Primary Cleric: Priority 95
3. Purge (AtlasOF_Purge): Priority 90
4. Large Shield Block Stance: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Cleric: Priority 100 (For Shield Slam)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Polearm Burst)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 200 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard / Protect: Priority 100 (Active on Cleric)
2. Shield Slam Style: Priority 95 (On loose add)
3. Polearm Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Endurance Management**: Polearm styles consume high endurance; pace style chains and coordinate with group Paladin/Minstrel for endurance recovery.

### 13. What the AI SHOULD Do
- Maintain active `Guard` and `Protect` on the primary Cleric.
- Intercept and Shield Slam any loose mob that breaks away from the pet toward healers.
- Hold position strictly at the camp anchor during pet pulling.
- Engage the cluster with heavy Polearm styles once mobs reach 85% health.

### 14. What the AI SHOULD NOT Do
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never charge out of camp to meet incoming mobs while they are in transit.
- Never fire the crossbow at mobs while the pet is still pulling them to camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Cleric**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Swap to Polearm -> Finish Add.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute Polearm Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip Large Shield, enter Engage / Block defense stance, rely on Cleric heals and plate armor.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration)
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Large Shield block rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Polearm damage boost)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: One-Hand & Shield Main Tank Armsman
- **role**: DefensiveTank
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: ["OneHandAndShieldArmsman", "OneHandAndShield"]
```

### 1. Intended Role
Primary Main Tank, Maximum Shield Defense (Shield 50), Unbreakable Healer Fortress (`Guard 3`/`Protect`), and 9-Second Shield Slam Anchor.

### 2. Intended Role Summary
The One-Hand and Shield Main Tank Armsman is the ultimate defensive bastion of Albion. With 50 in Shields and 50 in one-handed Slash, Thrust, or Crush, this build boasts the highest block chance possible with a Large Shield, provides permanent `Guard` coverage over group healers, and shuts down enemy attackers with reliable 9-second Shield Slams.

### 3. Combat Philosophy
Total physical mitigation and immovable area control. The Main Tank Armsman exists to absorb damage, block attacks aimed at squishy support, and lock threatening enemies in place with continuous shield stuns.

### 4. Mode: RvR
- **Maximum Cleric Protection**: Anchor beside the primary Cleric with active `Guard` and `Protect`; block incoming strikes for both self and the guarded ally.
- **Shield Slam Peeling**: Deliver 9-second Shield Slams onto enemy melee rushers and stealthers attempting to disrupt Albion casters.
- **Crowd Control Absorption**: Soak incoming physical damage and CC, leveraging high Determination to recover rapidly.
- **Positioning**: Tight backline / Midline shield wall (within 150 units of main Cleric).

### 5. Mode: PvP
- **Attrition Wall**: Shield Slam (9s stun) -> Position for side/rear 1H styles -> Block incoming melee attacks -> Grind down opponents behind plate and shield.

### 6a. Mode: PvE Standard
- **Main Tank**: Taunt and hold boss mobs; guard primary healer; slam and stun secondary adds.

### 6b. Mode: PvE Pet Pulling (Dedicated Healer Fortress Role)
- **Role During Pet Pulling**: The Main Tank Armsman is **NOT the puller**. It acts as the **dedicated camp bodyguard and healer protector**.
- **Guard Primary Cleric**:
  - Set **active `Guard` and `Protect` on the primary Cleric** before the pull.
- **Hold Position Beside Healer**:
  - Remain strictly stationed beside the Cleric at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Instant Shield Slam)**:
  - If any mob breaks away from the pet toward the Cleric, **the Armsman immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Hold the stunned mob in place and destroy it while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Armsman assists DPS on the primary focus target with one-handed weapon styles, remaining within peeling distance of the Cleric.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT PHYSICAL RESILIENCE**. Plate armor + Large Shield block rates ensure minimal health loss when grinding in tight corridors.
  - **Corridor Pull Isolation**: Use Crossbow to pull single hostiles out of densely packed dungeon rooms into safe hallway choke points.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Grinds mobs with near 100% block rates and negligible damage taken.
  - **Secondary (Yellow)**: Standard target. Slam stun allows easy positional style follow-up.
  - **Restricted (Orange)**: Viable; lock down with Shield Slam.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Crossbow pull mob into cleared corridor.
  - 2. Opener: Land Shield Slam (stun) on contact.
  - 3. Combo: Step to flank during stun; execute Crush/Slash/Thrust style combo.
  - 4. Defense: Block incoming recovery swings and execute block-reaction counter-styles.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint away to dungeon exit.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. One-Handed Taunt Style on Break-Off Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Crossbow Shot on Fleeing Target: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Keep active 100% of time)
2. Protect on Primary Cleric: Priority 95
3. Large Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Cleric: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For 1H Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Tight Backline (within 100â€“150 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never leave the Cleric's side.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Cleric)
2. Shield Slam Style: Priority 95 (On loose add)
3. One-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Protect Ability: Priority 80

### 12. Resource Management
- **Endurance**: 1H styles are endurance-efficient; manage stamina during extended engagements.

### 13. What the AI SHOULD Do
- Keep Guard and Protect active on the primary Cleric at all times.
- Hold position at camp beside the Cleric during pet pulling.
- Shield slam and stun any loose add that breaks toward the healer.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the Cleric to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Cleric.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Cleric**: Select Add -> Execute Shield Slam Stun (9s) -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute 1H Weapon Styles.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Activate Engage / Block defense, rely on Cleric heals and shield block.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Maximum shield block rate)
8. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Pure Two-Hand / Polearm Armsman
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Two_Handed"
  min_point_ratio: 0.50
  spec_type_aliases: ["PurePolearmTwoHandArmsman", "TwoHanded"]
```

### 1. Intended Role
Heavy Two-Handed Physical Striker, Crossbow Sniper, Reach Finisher, and High-Burst Frontline Enforcer.

### 2. Intended Role Summary
The Pure Two-Hand / Polearm Armsman dedicates all training to maximum offensive lethality. By specializing to 50 in Polearms or Two-Handed weapons alongside 50 in base weapons, it delivers the highest single-hit physical damage in Albion's heavy armor arsenal, backed by long-range Crossbow sniping.

### 3. Combat Philosophy
Overwhelming physical force and reach dominance. The Two-Hand Armsman opens engagements with armor-piercing crossbow bolts, charges priority targets alongside the melee assist train, and deletes opponents with crushing two-handed style executions.

### 4. Mode: RvR
- **Crossbow Volley**: Fire heavy crossbow bolts from 1800 units onto enemy squishies before lines collide.
- **Melee Assist Burst**: Push into the enemy backline; execute two-handed positional styles on called targets.
- **Peel Enforcement**: Obliterate enemy peelers with massive two-handed burst damage.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Burst & Snipe**: Open with Crossbow shot -> Close distance -> Deliver high-damage 2H positional style chain -> Execute fleeing targets with crossbow.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-damage two-handed weapon style chains.

### 6b. Mode: PvE Pet Pulling (Melee Enforcer & Break-Off Destroyer)
- **Role During Pet Pulling**: The Two-Hand Armsman is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Fire on Pull**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Do NOT shoot the crossbow while the pet is in transit.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **immediately engage the loose add with heavy Two-Handed/Polearm styles**!
  - The massive burst of a 2H weapon will quickly obliterate the loose add before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Two-Hand Armsman steps forward into melee:
  - Engage the primary focus target in the cluster with heavy **Two-Handed/Polearm weapon styles**!
- **Strict Stationary Discipline**:
  - Remain stationed at camp during the pull phase. Never chase mobs out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH BURST MELEE SURVIVABILITY**. High damage per swing shortens combat duration in corridors, reducing exposure to wandering patrols.
  - **Corridor Pull Isolation**: Crossbow pull single mobs back to dead-end hallways.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 heavy swings.
  - **Secondary (Yellow)**: Standard target. Parry counters deal devastating burst.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Crossbow pull target into corridor.
  - 2. Swap to Two-Hander/Polearm.
  - 3. Opener: Heavy opening style or parry reaction counter.
  - 4. Finisher: High-damage execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Heavy Two-Handed Style Chain on Focus Target (at 85% HP): Priority 90
3. Crossbow Shot on Ranged / Fleeing Target: Priority 80
4. Positional Stun / Snare Style: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense Stance: Priority 85
4. First Aid (AtlasOF_FirstAid): Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For 2H DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Two-Handed Weapon Styles: Priority 90 (At 85% mob health)
3. Crossbow Shot: Priority 85 (Opening RvR)
4. Positional Stun Style: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Heavy 2H styles drain endurance quickly; manage endurance and coordinate with group Paladin/Minstrel.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Open RvR combat with heavy crossbow fire.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never fire the crossbow at mobs while the pet is in transit.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Cleric, parry, and call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts 2H melee damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
