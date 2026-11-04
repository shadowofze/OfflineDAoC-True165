---
schema_version: 1
class_name: "Champion"
class_id: 45
realm: "Hibernia"
primary_stats: ["Strength", "Intelligence", "Constitution", "Dexterity"]
default_personality: "Tactical"
---

# Class: Champion (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Scale (Level 20+), Reinforced (1â€“19).
- **Weapon Slots**: One-Handed Blades, Blunt, Piercing; Large Weapons (heavy two-handed weapons); Medium Shields.
- **Core Specialization Lines**:
  - `Valor`: Crippling debuff magic combined with instant Direct Damage (DD) shouts, instant Snares, Strength/Constitution debuffs, Dexterity/Quickness debuffs, and melee haste debuffs that cripple enemy physical combat effectiveness.
  - `Large Weapons`: Heavy two-handed weapon style chains delivering crushing kinetic burst.
  - `Weapon Lines` (`Blades`, `Blunt`, `Piercing`): One-handed weapon specialization.
  - `Shields`: High-tier shield styles (Slam: 9-second stun), shield blocking, and healer protection (`Guard`).
  - `Parry`: Passive melee deflection.
- **Base Abilities**: Sprint, Taunt, Engage.
- **Hibernia's Debuffing Vanguard**: Clad in heavy scale armor, the Champion weakens enemy champions through devastating stat-stripping magic before crushing them with shield stuns and two-handed weaponry.

---

## Specialization 1: Valor Shield Peeler Champion
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Valor"
  min_point_ratio: 0.50
  spec_type_aliases: ["ValorShieldChampion", "OneHandAndShield"]
```

### 1. Intended Role
Debuffing Frontline Defender, 9-Second Shield Slam Peeler, Stat Stripper, Healer Bodyguard (`Guard`), and Instant DD Shouter.

### 2. Intended Role Summary
The Valor Shield Peeler Champion is Hibernia's premier hybrid disrupter. Combining 50 Valor with 42 Shield and 39 base weapon, it shuts down enemy offense by stripping massive amounts of Strength, Constitution, Dexterity, and Quickness with instant debuffs, locks down rushers with 9-second Shield Slams, and guards Hibernia's primary healers.

### 3. Combat Philosophy
Debilitation and physical lockdown. The Champion casts instant debuffs to weaken enemy attack speed and damage, slams melee rushers with 9-second shield stuns, guards the backline, and finishes targets with instant DD shouts.

### 4. Mode: RvR
- **Instant Stat Debuffs**: Cast Strength/Constitution and Dexterity/Quickness debuffs onto enemy melee rushers and assist targets.
- **Dedicated Healer Guard**: Anchor beside Hibernian Bards/Druids; maintain active `Guard` to absorb incoming melee strikes.
- **Shield Slam & Snare**: Slam incoming stealthers or melee with a 9-second shield stun; apply instant Valor Snare to fleeing targets.
- **Positioning**: Midline anchor (200â€“300 units ahead of Bard/Druid).

### 5. Mode: PvP
- **Debuff & Slam**: Instant Str/Con debuff -> Instant Dex/Qui debuff -> Shield Slam (9s stun) -> Positional 1H style chain -> Instant DD shout finisher.

### 6a. Mode: PvE Standard
- **Off-Tank / Debuffer**: Debuff primary boss mobs to reduce their damage and attack speed; slam secondary adds; guard healer.

### 6b. Mode: PvE Pet Pulling (Debuffing Bodyguard & Shield Peeler Role)
- **Role During Pet Pulling**: The Champion is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off peeler**.
- **Crucial Rule: NO TAUNTING OR CASTING ON THE MAIN PACK**:
  - **Do NOT cast debuffs or taunt mobs centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Active Guard on Healer**:
  - Set **active `Guard` on the primary Bard or Druid** before the pull begins.
- **Hold Debuffs and Melee During Transit**:
  - Strictly hold all spells, debuffs, and melee swings while the pet is bringing mobs to camp.
- **Break-Off Add Protection (Shield Slam & Debuff Peel)**:
  - If any mob breaks away from the pet toward the support, **the Champion steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or **Instant Valor Debuff/DD**!
  - Destroy that specific loose add while keeping the support core 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Champion assists the group:
  - Cast **Valor Debuffs** on the primary focus target in the cluster, fire **Instant DD Shouts**, and engage with one-handed weapon styles!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT HYBRID CONTROL SURVIVABILITY**. Reinforced Scale armor, Shield block with Slam stun, instant Valor magic debuffs (Str/Con and Dex/Quick debuffs), and instant DD shouts make the Champion exceptionally strong in dungeon hallways. Magic shouts cannot be interrupted by melee swings.
  - **Corridor Pull Isolation**: Use ranged Valor magic pull from doorway to pull an isolated mob into a cleared corridor bottleneck.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Valor debuffs cripple mob; weapon styles finish it with zero damage taken.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Slam stun followed by debuff and melee burst.
  - **Restricted (Orange)**: Viable; debuff mob strength/dexterity to severely lower its attack power.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull target with ranged Valor debuff/shout into safe corridor.
  - 2. Opener: Shield Slam (stun) on contact.
  - 3. Magic Debuff: Cast instant Valor Str/Con debuff during stun.
  - 4. Melee Combo: Step to flank; execute weapon style chain.
  - 5. Finisher: Instant magic shout or block-reaction style to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20% or Mana < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Shield Slam stun add -> instant magic snare -> Sprint retreat down cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Instant Valor Stat Debuffs on Focus Target (at 85% HP): Priority 95
3. Instant DD Shout on Cooldown: Priority 90
4. One-Handed Weapon Style Chain on Focus Target: Priority 85

### 8. Defensive Priorities
1. Guard on Primary Bard/Druid: Priority 100 (Never let drop)
2. Protect on Primary Bard/Druid: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Support: Priority 100 (For Shield Slam)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Debuffs & Melee DPS)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 150â€“200 units of guarded Bard/Druid)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Bard/Druid)
2. Shield Slam Style: Priority 95 (On loose add)
3. Instant Valor Debuffs: Priority 90 (On primary target)
4. Instant DD Shout: Priority 85
5. `AtlasOF_Purge`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Valor spells consume mana; monitor mana pool and pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Maintain active `Guard` on the primary Bard/Druid at all times.
- Hold all debuffs and melee attacks while mobs are in transit to camp.
- Intercept and Shield Slam any loose mob that breaks toward healers.
- Apply Valor debuffs to primary focus targets once mobs reach 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast debuffs or spells on mobs while the pet is in transit to camp.
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never abandon the guarded support to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Support**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Cast Instant Valor Debuff.
- **Condition: Mob Pack at Pet & HP <= 85% & Support Safe**: Select Primary Focus Target -> Cast Valor Debuffs -> Execute 1H Weapon Styles -> Fire Instant DD.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Equip shield, enter block defense, apply haste debuff to attacker, call for Druid heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10] (Restores power for Valor debuffs/shouts)
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 30] (Shield block rate)
9. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 40] (Style damage)
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Large Weapons Shock Champion
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Large_Weapons"
  min_point_ratio: 0.50
  spec_type_aliases: ["LargeWeaponChampion", "TwoHanded"]
```

### 1. Intended Role
Two-Handed Heavy Kinetic Striker, Valor Debuff Executioner, Frontline Shock Enforcer, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Large Weapons Shock Champion combines massive two-handed blunt and slashing implements with 50 Valor debuff magic. By stripping enemy defenses with instant debuffs and following up with bone-shattering two-handed style chains, it delivers devastating physical burst against priority targets.

### 3. Combat Philosophy
Debuff-assisted kinetic annihilation. The Large Weapons Champion strips target armor, strength, and attack speed with instant Valor magic, closes the distance, and crushes opponents with heavy two-handed style chains.

### 4. Mode: RvR
- **Debuff Shock Assist**: Charge with Hibernian assist train; cast instant Valor debuffs onto called targets, then execute heavy two-handed styles.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Debuff & Crush**: Instant Str/Con debuff -> Instant Snare -> Close distance -> Positional Large Weapon style chain -> Instant DD shout.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with debuffs and high-damage two-handed style chains.

### 6b. Mode: PvE Pet Pulling (2H Enforcer Role)
- **Role During Pet Pulling**: The Large Weapon Champion is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Champion immediately engages it**:
  - Intercept the loose add and shred it with debuffs and heavy Two-Handed styles!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Champion assists DPS on the primary focus target with Valor debuffs and Large Weapon style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH HYBRID BURST DAMAGE**. Two-handed Large Weapon styles combined with instant Valor magic nukes vaporize single mobs in corridors.
  - **Corridor Pull Isolation**: Ranged magic pull into cleared room; execute two-handed styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 two-handed swings + magic shout.
  - **Secondary (Yellow)**: Standard target. Parry reactions trigger heavy counter-attacks.
  - **Restricted (Orange)**: Only engage with full endurance and mana.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ranged magic pull into hallway.
  - 2. Opener: Heavy two-handed opening style.
  - 3. Shout Weave: Weave instant Valor DD shout between weapon swings.
  - 4. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Switch to Shield -> Slam stun -> Sprint away.

### 7. Offensive Priorities
1. Large Weapon Style Burst & Debuff on Loose Break-Off Add: Priority 100
2. Instant Valor Debuffs on Focus Target (at 85% HP): Priority 95
3. Heavy Large Weapon Style Chain on Focus Target: Priority 90
4. Instant DD Shout on Cooldown: Priority 85

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For 2H DPS & Debuffs)
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Instant Valor Debuffs: Priority 95
3. Large Weapon Styles: Priority 90 (At 85% mob health)
4. Instant DD Shout: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Balance power usage between debuffs and two-handed styles; pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Apply Valor debuffs before executing heavy two-handed style chains.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never cast debuffs into the pull pack before mobs arrive at the pet.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Cast Valor Debuff -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Cast Valor Debuffs -> Execute Large Weapon Style Chain.

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
8. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 30] (Large Weapon damage)
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

## Specialization 3: High Weapon Debuffer Champion
- **role**: MeleeDPS
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Blades"  # Or Blunt / Piercing
  min_point_ratio: 0.50
  spec_type_aliases: ["HighWeaponShieldChampion", "OneHandHybrid"]
```

### 1. Intended Role
Primary Weapon Duelist, Valor Debuff Striker, Moderate Shield Peeler, and Frontline Brawler.

### 2. Intended Role Summary
The High Weapon Debuffer Champion maximizes single-handed weapon specialization (50 Blades, Blunt, or Piercing) while retaining 50 Valor debuff mastery and 28 Shield. It delivers maximum-damage one-handed style chains, strips enemy combat stats with instant debuffs, and provides defensive shield blocks and stuns.

### 3. Combat Philosophy
Debuff-empowered single-weapon execution. The High Weapon Champion weakens enemy offensive output with Valor debuffs and methodically dismantles opponents with high-accuracy weapon styles.

### 4. Mode: RvR
- **Frontline Assist Brawler**: Push into frontline collisions; apply Valor debuffs to high-threat enemy fighters while executing 50-spec weapon style chains.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Debuff & Duel**: Debuff target stats -> Shield stun -> Execute weapon style chain -> Finish with instant DD shouts.

### 6a. Mode: PvE Standard
- **Melee DPS & Debuff**: Debuff boss mobs; assist main tank with high-damage weapon styles.

### 6b. Mode: PvE Pet Pulling (Debuff Brawler Role)
- **Role During Pet Pulling**: The High Weapon Champion is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Champion immediately engages it**:
  - Intercept the loose add and shred it with debuffs and weapon styles!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Champion assists DPS on the primary focus target with Valor debuffs and weapon styles.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **BALANCED HYBRID SUSTAIN**. High weapon skill backed by continuous stat debuffs and scale armor.
  - **Corridor Pull Isolation**: Magic pull into hallway; melee down.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with full debuffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Magic debuff pull from doorway.
  - 2. Weapon style opening chain on contact.
  - 3. Point-blank magic shout weave.
  - 4. Execution style to finish.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when resources < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant magic snare -> Sprint retreat.

### 7. Offensive Priorities
1. Weapon Style Burst & Debuff on Loose Break-Off Add: Priority 100
2. Instant Valor Debuffs on Focus Target (at 85% HP): Priority 95
3. Weapon Style Chain on Focus Target: Priority 90
4. Instant DD Shout on Cooldown: Priority 85

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Shield Block Stance: Priority 90
3. Determination CC Reduction: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Weapon Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS & Debuffs)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Instant Valor Debuffs: Priority 95
3. Weapon Styles: Priority 90 (At 85% mob health)
4. Instant DD Shout: Priority 85
5. Shield Block: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Balance power usage between debuffs and weapon styles; pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Apply Valor debuffs before executing weapon style chains.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never cast debuffs into the pull pack before mobs arrive at the pet.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Melee Style -> Cast Valor Debuff -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Cast Valor Debuffs -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Druid, enter shield block stance, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10]
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 30] (Style damage)
9. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 34] (Shield block rate)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 35]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 38]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 39]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 42]
15. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 43]
16. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 46]
17. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 47]
18. `AtlasOF_LongWind` (Rank 1) - 3 pts [Total: 50]
