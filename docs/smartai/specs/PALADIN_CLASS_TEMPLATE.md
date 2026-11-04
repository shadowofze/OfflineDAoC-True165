---
schema_version: 1
class_name: "Paladin"
class_id: 1
realm: "Albion"
primary_stats: ["Strength", "Constitution", "Piety"]
default_personality: "Defensive"
---

# Class: Paladin (Albion)

## Shared Class Capabilities
- **Armor Type**: Full Plate (Level 20+), Chain (1â€“19).
- **Weapon Slots**: One-Handed Slashing, Thrusting, Crushing; Two-Handed Weapons; Medium and Large Shields.
- **Core Specialization Lines**:
  - `Chants`: Albion's holy war chants featuring Pulsing Endosong (pulsing endurance reduction chant that keeps group endurance full!), Pulsing Armor Factor (AF) chant, Pulsing Health Regeneration chant, Pulsing Damage Shield chant (retribution damage against melee attackers), and minor self-healing spells.
  - `Shields`: High-tier shield styles (Slam: 9-second stun), high block chance, and defensive party protection abilities (`Guard`, `Protect`, `Intercept`).
  - `Weapon Lines` (`Slash`, `Thrust`, `Crush`): One-handed weapon styles delivering bleeds, snares, and defense bonuses.
  - `Two-Handed`: Heavy two-handed weapons delivering high physical burst.
  - `Parry`: Passive melee deflection.
- **Base Abilities**: Sprint, Taunt, Engage.
- **Albion's Holy Knight**: The Paladin anchors Albion formations in impenetrable plate armor, pulsing essential endurance and armor chants to empower allies while shielding healers behind a massive wall of steel and 9-second shield stuns.

---

## Specialization 1: One-Hand & Shield Paladin
- **role**: DefensiveTank
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Chants"
  min_point_ratio: 0.50
  spec_type_aliases: ["OneHandAndShieldPaladin", "OneHandAndShield"]
```

### 1. Intended Role
Albion Group Anchor, Endosong Provider, 9-Second Shield Slam Peeler, Healer Bodyguard (`Guard`/`Protect`), and Plate Frontliner.

### 2. Intended Role Summary
The One-Hand and Shield Paladin is the defensive heartbeat of Albion groups. Combining high Chants (48â€“50) with 42 Shield, it provides the essential Pulsing Endosong that enables infinite sprinting and style execution for all party members, maintains pulsing AF buffs, locks down enemy attackers with 9-second Shield Slams, and protects vulnerable casters and healers via `Guard` and `Protect`.

### 3. Combat Philosophy
Steadfast protection and endurance logistics. The Paladin prioritizes the survival of Albion's support core above personal damage. It maintains chants continuously, stays within bodyguard range of the primary cleric, intercepts charging melee with Shield Slam stuns, and absorbs incoming blows.

### 4. Mode: RvR
- **Endosong Maintenance**: Keep Pulsing Endosong running 100% of the time so Albion's melee fighters and casters never exhaust endurance.
- **Dedicated Healer Guard**: Anchor directly beside the primary Cleric; maintain active `Guard` to absorb incoming melee strikes.
- **Shield Slam Peeling**: When enemy stealthers (Shadowblades, Nightshades) or heavy melee reach Albion support, slam them with a 9-second shield stun.
- **Pulsing AF Chant**: Twist Pulsing AF chant to maximize group armor mitigation against physical damage.
- **Positioning**: Tight midline (within 200â€“300 units of the main Cleric).

### 5. Mode: PvP
- **Shield Stun & Attrition**: Slam stun opponent (9s) -> Move to side/rear for weapon style chain -> Block with large shield -> Outlast opponent through self-heals and pulsing chants.

### 6a. Mode: PvE Standard
- **Main Tank / Off-Tank**: Hold threat on primary targets with taunt styles; keep Endosong active; guard the primary healer; slam secondary adds that target casters.

### 6b. Mode: PvE Pet Pulling (Endosong Support & Healer Bodyguard Role)
- **Role During Pet Pulling**: The Paladin is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, chant provider, and break-off peeler**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - Melee taunts on the main pack disrupt pull efficiency and split mob aggro.
- **Active Guard & Protect on Healer**:
  - Prior to the pull, set **active `Guard` and `Protect` on the group's primary Cleric/Healer**.
- **Chant Management at Camp**:
  - Keep **Pulsing Endosong** active at all times so casters can sprint and healers never gas out.
  - Run **Pulsing AF Chant** or **Health Regen Chant** to buffer the party.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the Cleric or casters, **the Paladin steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or taunting weapon style!
  - Lock down and destroy that specific loose add to keep the backline 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Paladin assists the group:
  - Engage the designated focus target within the cluster using one-handed weapon styles.
  - Maintain bodyguard positioning near the Cleric while swinging.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Heavy plate armor, large shield block rates, and self-pulsing AF and healing chants make the Paladin exceptionally durable in confined dungeon spaces (Tomb of Mithra, Catacombs, Tepok's Mine). Melee attacks suffer zero cast interrupts.
  - **Corridor Pull Isolation**: **Never fight inside an uncleared room or in intersecting patrol corridors**. Use proximity body-pulling or throwing weapons to pull an isolated single mob 500 units backward into a previously cleared dead-end corridor or safe room. This prevents roaming patrols from adding mid-combat.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Minimal miss rate, low endurance consumption, consistent block triggers for reactionary shield stuns (Brutalize / Slam).
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Paladin out-lasts the mob with defense and health chants.
  - **Restricted (Orange)**: Engage only with full endurance, active chants, and Engage / IP off cooldown.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; high miss rate exhausts endurance and results in death.
- **Pull & Engagement Sequence**:
  - 1. Pre-combat: Ensure End Regen Chant, AF Chant, and Health Regen Chant are running.
  - 2. Pull: Body-pull single target back into cleared corridor bottleneck.
  - 3. Opener: Block incoming strike -> execute Block-reaction Shield Stun (Slam / Brutalize).
  - 4. Style Rotation: Step to side/flank while mob is stunned -> execute Slash/Crush style chain.
  - 5. Finisher: Taunt style / high-damage style to finish target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest when Health < 50% or Endurance < 20%. Chants accelerate recovery.
  - **Safety Check**: Check cleared corridor for patrol spawns before sitting.
  - **Resume Threshold**: Resume hunting at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add links: Shield Slam stun the primary add -> activate Sprint -> retreat into cleared dungeon corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. One-Handed Taunt Style on Break-Off Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Self-Heal on Injured Self/Ally: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Never let drop)
2. Protect on Primary Cleric: Priority 95
3. Purge (AtlasOF_Purge): Priority 90
4. Large Shield Block Stance: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening group Cleric: Priority 100 (For Shield Slam)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Melee DPS)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 200 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Pulsing Endosong: Priority 100 (Keep running continuously)
2. Guard / Protect: Priority 95 (Active on Cleric)
3. Shield Slam Style: Priority 90 (On loose add)
4. `AtlasOF_Purge`: Priority 85
5. Pulsing AF Chant: Priority 80

### 12. Resource Management
- **Endurance Independence**: Pulsing Endosong ensures the Paladin and party have virtually infinite endurance.
- **Chant Mana**: Chants consume small mana ticks; monitor mana to ensure chants do not lapse.

### 13. What the AI SHOULD Do
- Keep Pulsing Endosong running 100% of the time.
- Maintain active `Guard` and `Protect` on the group's primary Cleric.
- Intercept and Shield Slam any loose mob that breaks away from the pet toward healers.
- Hold position strictly at the camp anchor during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never charge out of camp to meet incoming mobs while they are in transit.
- Never abandon the guarded Cleric to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Cleric**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Enter Engage / Block defense stance, cast self-heal, rely on Cleric heals and shield block.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration significantly)
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Boosts shield block rate)
8. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 30]
11. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 33]
12. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Two-Hand Hybrid Paladin
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Two_Handed"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandHybridPaladin", "TwoHanded"]
```

### 1. Intended Role
Heavy Two-Handed Melee Striker, Endosong Provider, Offensive Enforcer, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Two-Hand Hybrid Paladin trades a shield for the crushing physical burst of a two-handed Great Sword, Great Hammer, or Polearm, while maintaining high Chants (48) to fuel the group with Pulsing Endosong and AF buffs. It functions as an offensive frontline enforcer capable of shattering enemy ranks with heavy two-handed styles.

### 3. Combat Philosophy
Aggressive melee pressure backed by holy chants. The Two-Hand Hybrid Paladin pulses essential group endurance, joins the Albion melee assist train to eliminate priority targets with heavy two-handed styles, and intercepts loose threats with devastating two-handed burst.

### 4. Mode: RvR
- **Heavy Melee Burst**: Charge alongside Albion Armsmen and Mercenaries; deliver two-handed positional styles (rear/side stuns and bleeds) onto called targets.
- **Endosong Twisting**: Maintain Pulsing Endosong continuously to feed Albion's assist train endless combat endurance.
- **Caster Peel**: Intercept enemy peelers charging Albion casters; use heavy two-handed damage to force them to retreat or die.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Heavy Pressure**: Close distance -> Land positional two-handed style -> Chain into high-damage executioners -> Sustain through self-heals and Endosong.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-damage two-handed style chains; pulse Endosong for the party.

### 6b. Mode: PvE Pet Pulling (Endosong & Break-Off Destroyer Role)
- **Role During Pet Pulling**: The Two-Hand Paladin is **NOT the puller**. It acts as **stationary chant support and break-off mob destroyer**.
- **Do NOT Taunt Main Pack**:
  - Allow the pet puller's pet to hold the incoming mob pack. Do NOT attempt to take aggro from the pet.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out to meet incoming mobs. Remain strictly stationed at camp with the group.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **immediately engage the loose add with heavy Two-Handed weapon styles**!
  - Two-handed burst will quickly annihilate the loose mob before it threatens group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Two-Hand Paladin steps forward into melee:
  - Engage the primary focus target in the cluster with heavy **Two-Handed style chains**!
- **Strict Stationary Discipline**:
  - Remain stationed at camp during the pull phase. Never chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **VERY HIGH SOLO SURVIVABILITY**. Two-handed weapon styles deal heavy burst damage, cutting down single mobs quickly in dungeon corridors, supported by heavy plate armor and self-chants.
  - **Corridor Pull Isolation**: Pull single mobs back into cleared hallways; avoid engaging near uncleared spawns.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 4 to 5 two-handed style hits with minimal health loss.
  - **Secondary (Yellow)**: Standard target. Parry reactions trigger heavy counter-attack styles.
  - **Restricted (Orange)**: Only engage with full endurance and self-buff chants running.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure chants are active.
  - 2. Pull: Proximity pull mob into safe hallway.
  - 3. Opener: Parry reaction style or high-damage two-handed anytime style.
  - 4. Style Combo: Execute two-handed damage chain to burst target down.
  - 5. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Switch to 1H + Shield -> Shield Slam add -> Sprint to safety.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Two-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 90
3. Self-Heal on Critical Health: Priority 75
4. Taunt Style on Break-Off Mob: Priority 70

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense: Priority 85
4. Self-Heal: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For 2H DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. Pulsing Endosong: Priority 100 (Keep running continuously)
2. `AtlasOF_Purge`: Priority 95
3. Two-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. Pulsing AF Chant: Priority 85
5. Self-Heal: Priority 80

### 12. Resource Management
- **Endurance**: Endosong keeps endurance topped off, enabling continuous high-cost two-handed styles.

### 13. What the AI SHOULD Do
- Run Pulsing Endosong continuously during combat.
- Hold melee attacks until the mob pack hits 85% health at camp.
- Intercept and burst down any loose mob that breaks toward healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Withdraw slightly toward Cleric, cast self-heal, rely on parry defense.

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

---

## Specialization 3: Full Shield Bodyguard Paladin
- **role**: DefensiveTank
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: ["FullShieldPaladin", "ShieldBodyguardPaladin"]
```

### 1. Intended Role
Maximum Shield Defense (Shield 50), Unbreakable Healer Bodyguard (`Guard 3` / `Protect`), Maximum Block Rate, and 9-Second Shield Slam Anchor.

### 2. Intended Role Summary
The Full Shield Bodyguard Paladin pushes defensive capability to its absolute limit by specializing to 50 in Shields. Wielding a massive Large Shield in full plate armor, it boasts maximum block rates, provides the highest-tier `Guard` and `Protect` to completely neutralize physical attacks on Albion's primary healer, delivers guaranteed 9-second Shield Slams, and pulses essential holy chants.

### 3. Combat Philosophy
Impenetrable physical barrier. The Full Shield Bodyguard Paladin acts as a living fortress dedicated to the survival of the group Cleric. It intercepts every physical threat, absorbs blows with high-tier block and `Guard`, and controls enemies with 9-second shield stuns.

### 4. Mode: RvR
- **Maximum Cleric Guard**: Maintain 50-spec `Guard` on the primary Cleric; block incoming attacks for both self and the guarded ally.
- **9-Second Shield Slam Lockdown**: Apply 9-second Shield Slams to high-threat enemy melee rushers and stealthers attacking Albion's backline.
- **Pulsing Chants**: Maintain Pulsing Endosong and AF chants while standing firm beside the support core.
- **Positioning**: Zero-distance anchor (within 100â€“150 units of guarded Cleric).

### 5. Mode: PvP
- **Shield Fortress**: Shield slam stun (9s) -> Rotate for positional styles -> Block incoming attacks -> Outlast through defensive absorption.

### 6a. Mode: PvE Standard
- **Main Tanking & Bodyguarding**: Lock threat on boss/primary mobs; guard squishy casters; slam and stun secondary adds.

### 6b. Mode: PvE Pet Pulling (Dedicated Healer Fortress & Shield Peeler)
- **Role During Pet Pulling**: The Full Shield Paladin is **NOT the puller**. It acts as the **dedicated camp bodyguard and healer protector**.
- **Guard Primary Cleric**:
  - **Set active `Guard` and `Protect` on the primary Cleric** before the pull.
- **Hold Position Beside Healer**:
  - Remain strictly stationed beside the Cleric at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Instant Shield Slam)**:
  - If any mob breaks away from the pet toward the Cleric, **the Paladin immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Hold the stunned mob in place and destroy it while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Paladin assists DPS on the primary focus target with one-handed weapon styles, remaining within peeling distance of the Cleric.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **ULTIMATE PHYSICAL RESILIENCE**. Maximum shield spec provides 9-second Slam stuns and near-impenetrable block defense in narrow corridors.
  - **Corridor Pull Isolation**: Pull single mobs into corridor corners; slam-lock and dispatch safely.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Zero-damage grinding; shield blocks virtually all incoming attacks.
  - **Secondary (Yellow)**: Standard baseline solo target.
  - **Restricted (Orange)**: Viable due to long Slam stun duration.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull target into doorway.
  - 2. Opener: Immediate Shield Slam (9-second stun).
  - 3. Flank Rotation: Walk to mob's side during stun -> execute positional weapon styles.
  - 4. Finisher: Block-reaction counter style to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. One-Handed Taunt Style on Break-Off Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Self-Heal: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Keep active 100% of time)
2. Protect on Primary Cleric: Priority 95
3. Large Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Cleric: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Tight Backline (within 100â€“150 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never leave the Cleric's side.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Cleric)
2. Pulsing Endosong: Priority 95
3. Shield Slam Style: Priority 90 (On loose add)
4. `AtlasOF_Purge`: Priority 85
5. Pulsing AF Chant: Priority 80

### 12. Resource Management
- **Endurance**: Endosong ensures full endurance at all times.

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
- **Low Health (< 30%)**: Activate Engage / Block defense, cast self-heal, rely on Cleric heals and shield block.

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
