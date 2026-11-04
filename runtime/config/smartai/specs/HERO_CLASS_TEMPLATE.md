---
schema_version: 1
class_name: "Hero"
class_id: 44
realm: "Hibernia"
primary_stats: ["Strength", "Constitution", "Dexterity"]
default_personality: "Defensive"
---

# Class: Hero (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Scale (Level 20+), Reinforced (1–19).
- **Weapon Slots**: One-Handed Blades, Blunt, Piercing; Celtic Spear (Hibernia's long-reach piercing two-hander); Large Weapons (heavy two-handed crushing and slashing weapons); Medium and Large Shields.
- **Core Specialization Lines**:
  - `Shields`: High-tier shield styles (Slam: 9-second stun), high block chance with Large Shields, and critical party defense abilities (`Guard`, `Protect`, `Intercept`).
  - `Celtic Spear`: Hibernia's premier reach weapon line, delivering crippling positional stuns, snares, bleeds, and high piercing burst.
  - `Large Weapons`: Heavy two-handed blunt and slashing weapon style chains delivering immense kinetic burst.
  - `Weapon Lines` (`Blades`, `Blunt`, `Piercing`): One-handed weapon specialization delivering dependable baseline damage.
  - `Parry`: Passive melee attack deflection.
- **Base Abilities**: Sprint, Taunt, Engage, Protect, Intercept, Stag Form (morph into a giant stag with immense hit points and crowd control resistance).
- **Hibernia's Living Fortress**: Clad in interlocking scale mail, the Hero is the unbreakable champion of the realm, capable of absorbing catastrophic damage behind a tower shield, locking foes in place with 9-second shield stuns, or impaling enemies from reach with the lethal Celtic Spear.

---

## Specialization 1: Celtic Spear Hybrid Hero
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Celtic_Spear"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandHybrid"]
```

### 1. Intended Role
Reach Weapon Striker, 9-Second Shield Slam Peeler, Healer Bodyguard (`Guard`/`Protect`), Celtic Spear Burst Finisher, and Hybrid Frontliner.

### 2. Intended Role Summary
The Celtic Spear Hybrid Hero is Hibernia's premier frontline combatant. Combining 50 Celtic Spear with 42 Shield and 39 base weapon (Piercing/Blades/Blunt), it fluidly alternates between a tower shield for 9-second Shield Slams and healer guarding, and a deadly Celtic Spear to deliver devastating positional reach strikes, stuns, and high piercing burst against priority targets.

### 3. Combat Philosophy
Fluid tactical defense and reach execution. The Celtic Spear Hero anchors close to Hibernia's primary support (Bards and Druids) with shield ready, intercepts incoming attackers with 9-second shield stuns, and swaps to Celtic Spear to dispatch targets with long-reach style chains.

### 4. Mode: RvR
- **Stag Form Resilience**: Activate Stag Form upon entering major collisions to gain massive bonus health and shrug off enemy crowd control.
- **Dedicated Healer Guard**: Anchor directly beside the primary Bard or Druid; maintain active `Guard` and `Protect` to absorb incoming melee strikes.
- **Shield Slam & Spear Swap**: Slam incoming enemy stealthers or heavy melee with a 9-second shield stun, then immediately swap to Celtic Spear to execute positional rear/side stuns and bleeds.
- **Positioning**: Midline anchor (200–300 units ahead of Bard/Druid).

### 5. Mode: PvP
- **Slam & Spear Chain**: Shield Slam (9s stun) -> Swap to Celtic Spear -> Maneuver to rear -> Execute positional spear chain -> Finish with high-damage executioners.

### 6a. Mode: PvE Standard
- **Off-Tank / Main Tank**: Guard primary healer; hold secondary adds with shield slam; assist on focus target with heavy Celtic Spear styles.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Break-Off Peeler Role)
- **Role During Pet Pulling**: The Hero is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off mob peeler**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - Taunting mobs off the pet ruins pull efficiency and splits mob positioning.
- **Active Guard & Protect on Healer**:
  - Set **active `Guard` and `Protect` on the group's primary Bard or Druid** before the pull begins.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the Bard or Druid, **the Hero steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or taunting strike!
  - Swap to Celtic Spear to quickly obliterate that specific loose add while keeping the support core 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Hero assists the group:
  - Engage the primary focus target in the cluster with heavy **Celtic Spear styles**!
  - Continue maintaining guard coverage over the Bard or Druid.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Reinforced Scale armor, massive health pool (highest HP in Hibernia), Celtic Spear styles with rear stuns, and high parry make the Hero exceptionally durable in dungeon corridors (Coruscating Mine, Muire Tomb, Treibh Caillte). Melee attacks suffer zero cast interrupts.
  - **Corridor Pull Isolation**: **Never charge into an uncleared room**. Stand in a cleared corridor bottleneck, equip Short Bow or throwing spear, and hit an isolated mob to pull it 500 units backward into the safe corridor. Switch immediately to Celtic Spear as the mob closes distance.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 3 to 4 Celtic Spear styles kill the target rapidly with low endurance consumption.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Parry reactions trigger heavy counter-attack styles.
  - **Restricted (Orange)**: Engage only with full endurance and Stag Form / IP available.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; miss rate exhausts endurance and results in death.
- **Pull & Engagement Sequence**:
  - 1. Equip bow/thrown spear; locate isolated Blue/Yellow mob from doorway.
  - 2. Opener: Fire arrow/spear to initiate pull; immediately swap to Celtic Spear.
  - 3. Intercept: As mob enters melee range, execute Celtic Spear front style or parry opener.
  - 4. Style Chain: Follow up with high-damage Celtic Spear positional/reaction combo.
  - 5. Finisher: Execution style to finish mob before second patrol paths near.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest in cleared corridor corner when Health < 50% or Endurance < 25%.
  - **Safety Check**: Check cleared corridor for patrol spawns before sitting.
  - **Resume Threshold**: Stand when Health >= 90% and Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add links: Swap to 1H + Shield -> Shield Slam stun add -> activate Sprint -> retreat into cleared dungeon corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Celtic Spear Style Chain on Loose Add: Priority 95
3. Celtic Spear Weapon Styles on Focus Target (at 85% HP): Priority 85
4. Positional Rear / Side Stun Spear Style: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Bard/Druid: Priority 100 (Never let drop)
2. Protect on Primary Bard/Druid: Priority 95
3. Purge (AtlasOF_Purge): Priority 90
4. Large Shield Block Stance: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Bard/Druid: Priority 100 (For Shield Slam)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Spear Burst)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 200 units of guarded Bard/Druid)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard / Protect: Priority 100 (Active on Bard/Druid)
2. Shield Slam Style: Priority 95 (On loose add)
3. Celtic Spear Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Large Shield Block: Priority 80

### 12. Resource Management
- **Endurance**: Celtic Spear styles consume moderate endurance; pace style chains and coordinate with group Bard for endurance songs.

### 13. What the AI SHOULD Do
- Maintain active `Guard` and `Protect` on the primary Bard/Druid.
- Intercept and Shield Slam any loose mob that breaks away from the pet toward healers.
- Hold position strictly at the camp anchor during pet pulling.
- Engage the cluster with heavy Celtic Spear styles once mobs reach 85% health.

### 14. What the AI SHOULD NOT Do
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never charge out of camp to meet incoming mobs while they are in transit.
- Never abandon the guarded support to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Bard/Druid**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Swap to Celtic Spear -> Finish Add.
- **Condition: Mob Pack at Pet & HP <= 85% & Support Safe**: Select Primary Focus Target -> Execute Celtic Spear Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip Large Shield, enter Engage / Block defense stance, activate Stag Form if available, rely on Druid heals.

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
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Celtic Spear damage boost)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: One-Hand & Shield Main Tank Hero
- **role**: DefensiveTank
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: ["OneHandAndShield"]
```

### 1. Intended Role
Primary Hibernian Main Tank, Maximum Shield Defense (Shield 50), Unbreakable Healer Fortress (`Guard 3`/`Protect`), and 9-Second Shield Slam Anchor.

### 2. Intended Role Summary
The One-Hand and Shield Main Tank Hero is the ultimate defensive bastion of Hibernia. With 50 in Shields and 50 in base weapons (Blades, Blunt, or Piercing), this build boasts maximum block rates with a Large Shield, provides permanent `Guard` coverage over Hibernia's primary Bards and Druids, and completely neutralizes enemy attackers with guaranteed 9-second Shield Slams.

### 3. Combat Philosophy
Total physical mitigation and immovable area control. The Main Tank Hero exists to absorb damage, block attacks aimed at squishy support, and lock threatening enemies in place with continuous shield stuns.

### 4. Mode: RvR
- **Maximum Support Protection**: Anchor beside the primary Bard or Druid with active `Guard` and `Protect`; block incoming strikes for both self and the guarded ally.
- **Shield Slam Peeling**: Deliver 9-second Shield Slams onto enemy melee rushers and stealthers attempting to disrupt Hibernian casters.
- **Crowd Control Absorption**: Soak incoming physical damage and CC, leveraging Stag Form and Determination to recover rapidly.
- **Positioning**: Tight backline / Midline shield wall (within 150 units of main Bard/Druid).

### 5. Mode: PvP
- **Attrition Wall**: Shield Slam (9s stun) -> Position for side/rear 1H styles -> Block incoming melee attacks -> Grind down opponents behind scale armor and shield.

### 6a. Mode: PvE Standard
- **Main Tank**: Taunt and hold boss mobs; guard primary healer; slam and stun secondary adds.

### 6b. Mode: PvE Pet Pulling (Dedicated Support Fortress Role)
- **Role During Pet Pulling**: The Main Tank Hero is **NOT the puller**. It acts as the **dedicated camp bodyguard and healer protector**.
- **Guard Primary Support**:
  - Set **active `Guard` and `Protect` on the primary Bard or Druid** before the pull.
- **Hold Position Beside Healer**:
  - Remain strictly stationed beside the support at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Instant Shield Slam)**:
  - If any mob breaks away from the pet toward the Bard or Druid, **the Hero immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Hold the stunned mob in place and destroy it while keeping support 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Hero assists DPS on the primary focus target with one-handed weapon styles, remaining within peeling distance of the support core.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **ULTIMATE PHYSICAL RESILIENCE**. Scale armor + Large Shield block rates ensure minimal health loss when grinding in tight corridors, with 9-second Slam stuns.
  - **Corridor Pull Isolation**: Use bow to pull single hostiles out of densely packed dungeon rooms into safe hallway choke points.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Grinds mobs with near 100% block rates and negligible damage taken.
  - **Secondary (Yellow)**: Standard target. Slam stun allows easy positional style follow-up.
  - **Restricted (Orange)**: Viable; lock down with Shield Slam.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Bow pull mob into cleared corridor.
  - 2. Opener: Land Shield Slam (9-second stun) on contact.
  - 3. Combo: Step to flank during stun; execute Blades/Blunt/Piercing style combo.
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
4. Positional 1H Style on Focus Target: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Bard/Druid: Priority 100 (Keep active 100% of time)
2. Protect on Primary Bard/Druid: Priority 95
3. Large Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Support: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For 1H Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Tight Backline (within 100–150 units of guarded Bard/Druid)
- **Pull Phase**: Stationary at camp anchor; never leave the support's side.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Bard/Druid)
2. Shield Slam Style: Priority 95 (On loose add)
3. One-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Protect Ability: Priority 80

### 12. Resource Management
- **Endurance**: 1H styles are endurance-efficient; manage stamina during extended engagements.

### 13. What the AI SHOULD Do
- Keep Guard and Protect active on the primary Bard/Druid at all times.
- Hold position at camp beside the healer during pet pulling.
- Shield slam and stun any loose add that breaks toward the support.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the support to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Bard/Druid.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Support**: Select Add -> Execute Shield Slam Stun (9s) -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Support Safe**: Select Primary Focus Target -> Execute 1H Weapon Styles.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Activate Engage / Block defense, activate Stag Form, rely on Druid heals and shield block.

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

## Specialization 3: Large Weapons Hybrid Hero
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Large_Weapons"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Heavy Two-Handed Physical Striker, 42 Shield Slam Peeler, Crushing Burst Finisher, and Frontline Enforcer.

### 2. Intended Role Summary
The Large Weapons Hybrid Hero wields massive two-handed blunt and slashing implements, delivering bone-shattering kinetic burst to priority targets. Backed by 42 Shield and 39 base weapon, it retains the 9-second Shield Slam, solid block rates, and the versatility to guard healers or smash through enemy frontlines with crushing two-handed styles.

### 3. Combat Philosophy
Crushing kinetic impact backed by defensive versatility. The Large Weapon Hero can stand firm on the backline protecting healers with shield stuns and blocks, or transition seamlessly into an aggressive two-handed melee brawler crushing opposing ranks.

### 4. Mode: RvR
- **Heavy Melee Burst**: Charge alongside Hibernian Blademasters and Champions; execute heavy two-handed styles on called targets.
- **Shield Slam & Swap**: Slam incoming enemy rushers with a 9-second shield stun, then swap to Large Weapons to finish them.
- **Positioning**: Flexible midline / Forward melee flank (200–300 units ahead of Bard).

### 5. Mode: PvP
- **Slam & Crush**: Shield Slam (9s stun) -> Swap to Large Weapon -> Position for side/rear style chain -> Overpower target with massive two-handed burst.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS & Off-Tank**: Guard healer; slam loose adds; assist on focus target with heavy Large Weapon styles.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Break-Off Destroyer)
- **Role During Pet Pulling**: The Large Weapon Hero is **NOT the puller**. It acts as **stationary healer bodyguard and break-off mob destroyer**.
- **Guard Primary Support**:
  - Set **active `Guard` and `Protect` on the primary Bard or Druid** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the healer at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the support, **the Hero immediately intercepts it**:
  - Hit the loose add with a **Shield Slam (9-second stun)**, swap to Large Weapons, and quickly destroy the add!
  - Keep the support core completely safe from harm.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Hero assists DPS on the primary focus target with heavy Large Weapon styles, remaining within peeling distance of the support.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE BURST SURVIVABILITY**. Massive two-handed Large Weapon style damage cuts down single mobs quickly in corridors.
  - **Corridor Pull Isolation**: Bow pull single mobs back to dead-end hallways.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 heavy swings.
  - **Secondary (Yellow)**: Standard target. Parry reactions trigger heavy counters.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Bow pull target into corridor.
  - 2. Swap to Large Weapon.
  - 3. Opener: Heavy opening style or parry reaction counter.
  - 4. Finisher: High-damage execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Heavy Large Weapon Style Chain on Loose Add: Priority 95
3. Large Weapon Styles on Focus Target (at 85% HP): Priority 85
4. Positional Stun / Snare Style: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Bard/Druid: Priority 100 (Keep active 100% of time)
2. Protect on Primary Bard/Druid: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Support: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For 2H Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Support Side (within 150–200 units of guarded Bard/Druid)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Bard/Druid)
2. Shield Slam Style: Priority 95 (On loose add)
3. Large Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Protect Ability: Priority 80

### 12. Resource Management
- **Endurance**: Heavy 2H styles consume endurance; pace style chains and coordinate with group Bard for endurance songs.

### 13. What the AI SHOULD Do
- Keep Guard and Protect active on the primary Bard/Druid at all times.
- Hold position at camp beside the healer during pet pulling.
- Shield slam and stun any loose add that breaks toward the support.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the support to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Bard/Druid.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Support**: Select Add -> Execute Shield Slam Stun (9s) -> Swap to Large Weapon -> Destroy Add.
- **Condition: Mob Pack at Pet & HP <= 85% & Support Safe**: Select Primary Focus Target -> Execute Large Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, enter Engage / Block defense, activate Stag Form, rely on Druid heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25]
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Boosts Large Weapon damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
