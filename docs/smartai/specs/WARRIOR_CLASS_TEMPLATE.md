---
schema_version: 1
class_name: "Warrior"
class_id: 22
realm: "Midgard"
primary_stats: ["Strength", "Constitution", "Dexterity"]
default_personality: "Defensive"
---

# Class: Warrior (Midgard)

## Shared Class Capabilities
- **Armor Type**: Chain (Level 20+), Studded (1–19).
- **Weapon Slots**: One-Handed Sword, Axe, Hammer; Two-Handed Sword, Axe, Hammer; Medium and Large Shields; Thrown Weapons (Throwing Axes, Throwing Hammers).
- **Core Specialization Lines**:
  - `Shields`: High-tier shield styles (Slam: 9-second stun), high block chance with Large Shields, and critical party defense abilities (`Guard`, `Protect`, `Intercept`).
  - `Weapon Lines` (`Sword`, `Axe`, `Hammer`): Weapon styles featuring devastating stuns, snares, bleeds, and attack bonuses (applicable to both 1H and 2H variants).
  - `Parry`: Midgard's highest passive melee deflection rate.
  - `Thrown Weapons`: Ranged physical missile attacks.
- **Base Abilities**: Sprint, Taunt, Engage, Protect, Intercept.
- **Midgard's Iron Vanguard**: Clad in heavy chainmail, the Warrior is Midgard's premier frontline protector, capable of absorbing immense physical punishment, locking down foes with 9-second shield stuns, and defending Midgard's healers with unwavering resolve.

---

## Specialization 1: Shield Main Tank Warrior
- **role**: DefensiveTank
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Shields"
  fallback: true   # an Axe or Hammer Warrior matches no line here; owner 2026-11-05 run 4
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Primary Midgard Main Tank, 9-Second Shield Slam Anchor, Healer Bodyguard (`Guard 3`/`Protect`), Maximum Block Fortress, and Midline Protector.

### 2. Intended Role Summary
The Shield Main Tank Warrior is the defensive cornerstone of Midgard combat groups. Specializing to 50 in Shields alongside 50 in base weapons (Sword, Axe, or Hammer), it provides maximum block rates with a Large Shield, permanent `Guard` coverage over Midgard's primary Healers and Shamans, and reliable 9-second Shield Slams to completely shut down enemy attackers.

### 3. Combat Philosophy
Immovable protection and total lockdown. The Shield Warrior anchors directly beside Midgard's support core, blocks incoming strikes aimed at squishy healers, slams threatening melee and stealthers with 9-second stuns, and absorbs physical pressure.

### 4. Mode: RvR
- **Dedicated Healer Guard**: Anchor beside Midgard's primary Healer or Shaman; maintain active `Guard` and `Protect` to absorb incoming melee strikes.
- **9-Second Shield Slam Peeling**: When enemy stealthers (Infiltrators, Nightshades) or heavy melee reach Midgard support, slam them with a 9-second shield stun.
- **Frontline Screening**: Screen against charging enemy zergs, soaking incoming CC and physical burst.
- **Positioning**: Tight backline / Midline shield wall (within 150–200 units of main Healer).

### 5. Mode: PvP
- **Shield Stun & Attrition**: Slam stun opponent (9s) -> Move to side/rear for weapon style chain -> Block with large shield -> Outlast opponent through relentless defense and parry.

### 6a. Mode: PvE Standard
- **Main Tank**: Taunt and hold boss mobs; guard primary healer; slam and stun secondary adds.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Break-Off Peeler Role)
- **Role During Pet Pulling**: The Warrior is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off mob peeler**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - Taunting mobs off the pet ruins pull efficiency and splits mob positioning.
- **Active Guard & Protect on Healer**:
  - Set **active `Guard` and `Protect` on the group's primary Healer/Shaman** before the pull begins.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the Healer or casters, **the Warrior steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or taunting strike!
  - Lock down and destroy that specific loose add while keeping the Healer 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Warrior assists the group:
  - Engage the primary focus target in the cluster with one-handed weapon styles.
  - Continue maintaining guard coverage over the Healer.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Heavy chain armor, large shield block rates, and the 9-second Slam stun make the Shield Warrior extremely durable in confined dungeon spaces (Spindelhalla, Cursed Tomb, Varv). Melee attacks suffer zero cast interrupts.
  - **Corridor Pull Isolation**: **Never charge into an uncleared room or in active patrol paths**. Use Throwing Axes or proximity body-pulling to isolate a single mob 500 units backward into a previously cleared corridor or room.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Negligible health loss, consistent block triggers for reactionary shield stuns (Slam), low endurance consumption.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Slam stun neutralizes mob for 9 seconds.
  - **Restricted (Orange)**: Engage only with full endurance and Engage / IP ready.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; miss rate exhausts endurance and results in death.
- **Pull & Engagement Sequence**:
  - 1. Pull single target with Throwing Axe into cleared corridor bottleneck.
  - 2. Opener: Land Shield Slam (9-second stun) on contact.
  - 3. Flank Combo: Step to mob's side/rear during stun -> execute Sword/Axe/Hammer style combo.
  - 4. Block Counter: Block incoming recovery strike -> execute block-reaction counter-style.
  - 5. Finisher: High-damage execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest in cleared corridor corner when Health < 50% or Endurance < 20%.
  - **Safety Check**: Check cleared corridor for patrol spawns before sitting.
  - **Resume Threshold**: Stand when Health >= 90% and Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add links: Shield Slam stun primary add -> activate Sprint -> retreat into cleared dungeon corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. One-Handed Taunt Style on Break-Off Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Thrown Weapon on Fleeing Target: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Healer: Priority 100 (Never let drop)
2. Protect on Primary Healer: Priority 95
3. Large Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Healer: Priority 100 (For Shield Slam)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Melee DPS)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 150–200 units of guarded Healer)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard / Protect: Priority 100 (Active on Healer)
2. Shield Slam Style: Priority 95 (On loose add)
3. One-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Large Shield Block: Priority 80

### 12. Resource Management
- **Endurance**: 1H styles and Shield Slams are endurance-friendly; maintain endurance reserve for peel emergencies.

### 13. What the AI SHOULD Do
- Maintain active `Guard` and `Protect` on the primary Healer/Shaman.
- Intercept and Shield Slam any loose mob that breaks away from the pet toward healers.
- Hold position strictly at the camp anchor during pet pulling.
- Engage the cluster with weapon styles once mobs reach 85% health.

### 14. What the AI SHOULD NOT Do
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never charge out of camp to meet incoming mobs while they are in transit.
- Never abandon the guarded Healer to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Healer**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Healer Safe**: Select Primary Focus Target -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Enter Engage / Block defense stance, rely on Healer heals and chain/shield defense.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Crucial CC reduction)
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25] (Maximum Large Shield block rate)
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

## Specialization 2: Two-Handed Warrior
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Sword"  # Or Axe / Hammer (Two-Handed)
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Heavy Two-Handed Physical Striker, High-Parry Frontliner, Positional Executioner, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Two-Handed Warrior trades a shield for the devastating kinetic impact of Midgard's massive two-handed Great Swords, Great Axes, and Great Hammers. Combined with near-maximum Parry (49 Parry), it deflects incoming melee blows with weapon parries while dishing out bone-crushing physical burst against enemy targets.

### 3. Combat Philosophy
Ferocious two-handed aggression and weapon deflection. The Two-Handed Warrior charges into combat, parries counter-attacks with weapon mastery, and delivers heavy two-handed style chains to delete opposing casters and flankers.

### 4. Mode: RvR
- **Heavy Melee Burst**: Charge alongside Midgard Berserkers and Savages; execute rear and side positional styles on priority targets.
- **Weapon Parry Defense**: Deflect incoming melee attacks with massive 49 Parry rating.
- **Peel Enforcement**: Obliterate enemy peelers with massive two-handed damage.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Crush & Deflect**: Close distance -> Land heavy 2H positional style chain -> Parry enemy retaliation -> Finish with heavy damage executioners.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-damage two-handed weapon style chains.

### 6b. Mode: PvE Pet Pulling (Melee Enforcer & Break-Off Destroyer)
- **Role During Pet Pulling**: The Two-Handed Warrior is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Fire on Pull**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **immediately engage the loose add with heavy Two-Handed styles**!
  - The massive burst of a 2H weapon will quickly obliterate the loose add before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Two-Handed Warrior steps forward into melee:
  - Engage the primary focus target in the cluster with heavy **Two-Handed weapon styles**!
- **Strict Stationary Discipline**:
  - Remain stationed at camp during the pull phase. Never chase mobs out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE BURST SURVIVABILITY**. Devastating two-handed weapon style damage cuts down single mobs quickly in corridors, minimizing exposure to wandering patrols.
  - **Corridor Pull Isolation**: Throwing axe pull single mobs back to cleared hallways.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 to 4 heavy two-handed swings.
  - **Secondary (Yellow)**: Standard target. Parry reactions trigger heavy counter-attacks.
  - **Restricted (Orange)**: Only engage with full endurance pool.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Throwing axe pull mob into safe hallway.
  - 2. Opener: Heavy two-handed opening style.
  - 3. Parry Counter: Parry incoming strike -> execute two-handed riposte chain.
  - 4. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Switch to 1H + Shield -> Slam stun add -> Sprint to safety.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Heavy Two-Handed Style Chain on Focus Target (at 85% HP): Priority 90
3. Thrown Weapon on Fleeing / Ranged Target: Priority 80
4. Positional Stun / Snare Style: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. High Parry Deflection: Priority 85
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
3. `AtlasOF_Determination`: Priority 85
4. Positional Stun Style: Priority 80
5. `AtlasOF_Toughness`: Priority 75

### 12. Resource Management
- **Endurance**: Heavy 2H styles drain endurance quickly; manage endurance and coordinate with group Skald for endurance chants.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.
- Utilize high parry rate to survive melee counter-attacks.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, parry, and call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfParrying` (Rank 3) - 6 pts [Total: 25] (Maximum parry deflection rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Boosts 2H melee damage)
11. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Hybrid Weapon & Shield Warrior
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Sword"  # Or Axe / Hammer
  secondary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Balanced Frontline Brawler, 42 Shield Slam Peeler, High-Parry Off-Tank, and Healer Bodyguard.

### 2. Intended Role Summary
The Hybrid Weapon & Shield Warrior strikes the perfect equilibrium between physical offense and group defense. Combining 50 in primary weapon specialization with 42 Shield and 39 Parry, it possesses the lethal 9-second Shield Slam, high block and parry rates, reliable style damage, and the versatility to guard healers or lead melee charges.

### 3. Combat Philosophy
Fluid tactical adaptation. The Hybrid Warrior can stand firm on the backline protecting healers with shield stuns and blocks, or transition seamlessly into an aggressive melee brawler pushing enemy lines.

### 4. Mode: RvR
- **Healer Protection & Peel**: Guard Midgard healers; slam enemy rushers with 9-second Shield Slams.
- **Melee Assist Transition**: Once backline is secure, push forward with the melee train, delivering high-damage weapon style chains.
- **Positioning**: Flexible midline (200–300 units ahead of Healer).

### 5. Mode: PvP
- **Slam & Chain**: Shield Slam (9s stun) -> Positional weapon style chain -> Block and parry opponent counter-strikes.

### 6a. Mode: PvE Standard
- **Versatile Tanking**: Off-tank secondary adds with shield slam; guard healer; assist on focus target with weapon styles.

### 6b. Mode: PvE Pet Pulling (Balanced Healer Guard & Peeler Role)
- **Role During Pet Pulling**: The Hybrid Warrior is **NOT the puller**. It acts as **stationary healer bodyguard and break-off peeler**.
- **Guard Primary Healer**:
  - Set **active `Guard` and `Protect` on the primary Healer/Shaman** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the Healer at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Shield Slam Peeling)**:
  - If any mob breaks away from the pet toward the Healer, **the Warrior immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Destroy the stunned add while keeping the Healer 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Warrior assists DPS on the primary focus target with weapon styles, remaining within peeling distance of the Healer.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **BALANCED PHYSICAL MASTERY**. Combines high shield defense with solid one-handed weapon style damage for reliable corridor clearing.
  - **Corridor Pull Isolation**: Pull single mobs to corridor corners; slam-lock and dispatch.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast, safe grinding with minimal health loss.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Viable with Slam stun.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Shield Slam (stun).
  - 3. Combo: One-handed style positional combo.
  - 4. Finisher: Block-reaction counter-style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint retreat.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. One-Handed Taunt Style on Break-Off Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Thrown Weapon on Fleeing Target: Priority 70

### 8. Defensive Priorities
1. Guard on Primary Healer: Priority 100 (Keep active 100% of time)
2. Protect on Primary Healer: Priority 95
3. Shield Block & Parry Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Healer: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Healer Side (within 150–200 units of guarded Healer)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Healer)
2. Shield Slam Style: Priority 95 (On loose add)
3. Weapon Styles: Priority 90 (At 85% mob health)
4. `AtlasOF_Purge`: Priority 85
5. Protect Ability: Priority 80

### 12. Resource Management
- **Endurance**: Balanced style rotation maintains strong endurance reserves.

### 13. What the AI SHOULD Do
- Keep Guard and Protect active on the primary Healer at all times.
- Hold position at camp beside the Healer during pet pulling.
- Shield slam and stun any loose add that breaks toward the healer.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the Healer to chase DPS far from camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never let Guard drop from the primary Healer.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Healer**: Select Add -> Execute Shield Slam Stun (9s) -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Healer Safe**: Select Primary Focus Target -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Activate Engage / Block defense, rely on Healer heals and combined block/parry defense.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 25]
8. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfParrying` (Rank 3) - 6 pts [Total: 35]
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
