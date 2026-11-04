---
schema_version: 1
class_name: "Thane"
class_id: 21
realm: "Midgard"
primary_stats: ["Strength", "Piety", "Constitution"]
default_personality: "Tactical"
---

# Class: Thane (Midgard)

## Shared Class Capabilities
- **Armor Type**: Chain (Level 20+), Studded (1â€“19).
- **Weapon Slots**: One-Handed Sword, Axe, Hammer; Two-Handed Sword, Axe, Hammer; Medium Shields.
- **Core Specialization Lines**:
  - `Stormcalling`: Thor's divine lightning magic, featuring long-range Direct Damage lightning bolts, instant point-blank lightning blasts, Area of Effect (AoE) lightning shouts, and personal static damage shields.
  - `Shields`: High-tier shield styles (Slam: 9-second stun), medium shield block, and defensive party protection abilities (`Guard`, `Protect`).
  - `Weapon Lines` (`Sword`, `Axe`, `Hammer`): Heavy melee styles delivering bleeds, snares, and physical burst.
  - `Parry`: Passive melee deflection.
- **Base Abilities**: Sprint, Taunt, Engage.
- **Midgard's Thunder Warrior**: The Thane channels the wrath of Thor, striking foes with crackling lightning from afar while standing resolute in chain armor, ready to slam opponents with a 9-second shield stun or smash them with storm-empowered weapons.

---

## Specialization 1: Stormcalling Shield Thane
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Stormcalling"
  min_point_ratio: 0.50
  spec_type_aliases: ["StormShieldThane", "OneHandAndShield"]
```

### 1. Intended Role
Thunder Tank, Lightning Artillery, 9-Second Shield Slam Peeler, Healer Bodyguard (`Guard`/`Protect`), and Chain Frontliner.

### 2. Intended Role Summary
The Stormcalling Shield Thane is Midgard's premier magical-hybrid frontline defender. Combining 50 Stormcalling with 42 Shield and 39 in base weapons, it possesses maximum lightning burst (heavy bolts, instant DD shouts, and AoE storms), reliable 9-second Shield Slams, solid block rates, and the capability to guard Midgard's healers while raining lightning upon enemy ranks.

### 3. Combat Philosophy
Ranged lightning harassment paired with impenetrable backline peeling. The Thane softens targets from range with lightning bolts, anchors beside Midgard's support core, intercepts melee rushers with 9-second shield stuns, and weaves instant lightning blasts into melee combat.

### 4. Mode: RvR
- **Lightning Artillery**: Cast long-range lightning bolts onto enemy casters or support before lines clash (1500 units).
- **Dedicated Healer Guard**: Anchor beside the primary Healer or Shaman; maintain active `Guard` to absorb physical strikes.
- **Shield Slam & Instant Blast**: Slam incoming enemy stealthers or melee with a 9-second shield stun, then immediately blast them with an instant lightning shout.
- **Static Damage Shield**: Keep personal static damage shield active to punish attackers.
- **Positioning**: Midline anchor (200â€“300 units ahead of Healer).

### 5. Mode: PvP
- **Storm & Slam**: Lightning bolt opener -> Shield Slam (9s stun) -> Instant lightning blast -> Positional melee style chain -> Finish with follow-up instant DD.

### 6a. Mode: PvE Standard
- **Off-Tank / Caster Harasser**: Pull loose mobs with lightning; slam secondary adds that threaten casters; guard primary healer; assist on focus target with melee styles.

### 6b. Mode: PvE Pet Pulling (Lightning Peel & Healer Bodyguard Role)
- **Role During Pet Pulling**: The Thane is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off peeler**.
- **Crucial Rule: NO TAUNTING OR NUKING THE MAIN PACK**:
  - **Do NOT cast lightning bolts or taunt mobs centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - Lightning spells or melee taunts on the main pack pull threat off the pet and split the cluster.
- **Active Guard & Protect on Healer**:
  - Set **active `Guard` and `Protect` on the group's primary Healer/Shaman** before the pull begins.
- **Hold Lightning During Transit**:
  - Strictly hold fire on all lightning bolts and shouts while the pet is bringing mobs to camp.
- **Break-Off Add Protection (Lightning & Slam Peel)**:
  - If any mob breaks away from the pet toward the Healer or casters, **the Thane steps in immediately**:
  - Hit the loose add with an **Instant Lightning Blast** or **Shield Slam (9-second stun)**!
  - Lock down and destroy that specific loose add while keeping the Healer 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Thane assists the group:
  - Unleash **instant lightning shouts** and engage the primary focus target with one-handed weapon styles!
  - Continue maintaining guard coverage over the Healer.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO HYBRID SURVIVABILITY**. Chain armor, Shield block defense with Slam stun, self-AF and Str buffs, and instant Stormcalling lightning shouts give the Thane exceptional versatility in dungeon corridors (Spindelhalla, Cursed Tomb). Lightning shouts cannot be interrupted by physical hits, allowing the Thane to finish fleeing or charging mobs reliably.
  - **Corridor Pull Isolation**: Use ranged Lightning Bolt / Shout from doorway to pull an isolated mob into a cleared corridor bottleneck.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Lightning shouts + Hammer/Axe styles eliminate mob with zero downtime.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Slam stun followed by lightning burst.
  - **Restricted (Orange)**: Engage only with full endurance and active self-buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure self-spec AF and Str buffs are active.
  - 2. Opener: Cast ranged Stormcalling Lightning Bolt on target to initiate pull.
  - 3. Contact: Land Shield Slam (stun) as mob closes into melee range.
  - 4. Instant Burst: Fire instant Lightning Shout point-blank during stun.
  - 5. Melee Combo: Execute Hammer/Axe style chain to execute target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20% or Health < 50% or Endurance < 20%.
  - **Safety Check**: Check cleared hallway before sitting.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Shield Slam stun primary add -> fire instant Lightning Shout -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Instant Lightning Blast / Shield Slam on Loose Break-Off Add: Priority 100
2. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
3. Instant Lightning Shout on Cooldown: Priority 80
4. Lightning Bolt on Ranged Target (Opening RvR): Priority 75

### 8. Defensive Priorities
1. Guard on Primary Healer: Priority 100 (Never let drop)
2. Protect on Primary Healer: Priority 95
3. Purge (AtlasOF_Purge): Priority 90
4. Medium Shield Block Stance: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Healer: Priority 100 (For Shield Slam / Instant Blast)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Melee DPS & Shouts)
3. Enemy Casters / Support (RvR): Priority 80 (For Lightning Bolts)

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 200 units of guarded Healer)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard / Protect: Priority 100 (Active on Healer)
2. Shield Slam Style: Priority 95 (On loose add)
3. Instant Lightning Blast: Priority 90 (Instant interrupt / burst)
4. `AtlasOF_Purge`: Priority 85
5. Weapon Styles: Priority 80 (At 85% mob health)

### 12. Resource Management
- **Mana for Lightning**: Lightning spells and instant shouts consume mana; monitor mana pool and avoid spamming bolts in PvE pet pulling.
- **MCL Usage**: Pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Maintain active `Guard` and `Protect` on the primary Healer/Shaman.
- Intercept and Shield Slam any loose mob that breaks away from the pet toward healers.
- Hold position strictly at the camp anchor during pet pulling.
- Engage the cluster with instant lightning and weapon styles once mobs reach 85% health.

### 14. What the AI SHOULD NOT Do
- Never cast lightning bolts into the pull pack while mobs are in transit to camp.
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never abandon the guarded Healer to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Healer**: Select Add -> Fire Instant Lightning Blast -> Move into Melee -> Execute Shield Slam (9s Stun).
- **Condition: Mob Pack at Pet & HP <= 85% & Healer Safe**: Select Primary Focus Target -> Fire Instant Shout -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease casting lightning bolts; rely exclusively on one-handed weapon styles, shield block, and emergency instant shouts.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10] (Restores power for lightning shouts)
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 30] (Boosts shield block rate)
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 40]
12. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Two-Handed Thunder Thane
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Stormcalling"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandedThane", "TwoHanded"]
```

### 1. Intended Role
Two-Handed Thunder Striker, Heavy Lightning Burst Nuker, Frontline Shock Enforcer, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Two-Handed Thunder Thane channels the full destructive power of Thor through massive two-handed Great Swords, Great Axes, and Great Hammers. Backed by 50 Stormcalling, it pairs crushing two-handed physical styles with devastating off-global instant lightning blasts, vaporizing opposing targets in a flurry of steel and thunder.

### 3. Combat Philosophy
Aggressive shock warfare. The Two-Handed Thane closes distance under lightning bombardment, strikes opponents with heavy two-handed style chains, weaves instant lightning blasts into swing intervals, and overpowers priority targets with combined physical and magical burst.

### 4. Mode: RvR
- **Lightning Opener**: Fire lightning bolts into enemy casters while closing distance.
- **Melee Shock Assist**: Push with Midgard's frontline assist train; weave instant lightning blasts between two-handed weapon swings onto called targets.
- **Peel Enforcement**: Obliterate enemy peelers with massive two-handed burst and instant thunder damage.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Thunder Shock**: Lightning bolt opener -> Close distance -> Positional 2H weapon style chain -> Instant lightning blast -> Execute with follow-up 2H strikes.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-damage two-handed weapon style chains and instant lightning shouts.

### 6b. Mode: PvE Pet Pulling (Melee Enforcer & Break-Off Destroyer)
- **Role During Pet Pulling**: The Two-Handed Thane is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Fire on Pull**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Do NOT cast lightning while the pet is in transit.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **immediately engage the loose add with heavy Two-Handed styles and instant lightning blasts**!
  - The combined burst of a 2H weapon and lightning will quickly obliterate the loose add before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Two-Handed Thane steps forward into melee:
  - Engage the primary focus target in the cluster with heavy **Two-Handed weapon styles** and instant shouts!
- **Strict Stationary Discipline**:
  - Remain stationed at camp during the pull phase. Never chase mobs out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH HYBRID BURST DAMAGE**. Two-handed weapon burst combined with instant lightning shouts vaporizes single mobs in corridors.
  - **Corridor Pull Isolation**: Ranged lightning pull into cleared room; execute two-handed styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 two-handed swings + lightning shout.
  - **Secondary (Yellow)**: Standard target. Parry reactions trigger heavy counter-attacks.
  - **Restricted (Orange)**: Only engage with full endurance and mana.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ranged lightning bolt pull into hallway.
  - 2. Opener: Heavy two-handed opening style.
  - 3. Instant Shout: Weave instant Lightning Shout between weapon swings.
  - 4. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Switch to Shield -> Slam stun -> Sprint away.

### 7. Offensive Priorities
1. Two-Handed Style Burst & Instant Lightning on Loose Break-Off Add: Priority 100
2. Heavy Two-Handed Style Chain on Focus Target (at 85% HP): Priority 90
3. Instant Lightning Blast on Cooldown: Priority 85
4. Lightning Bolt on Ranged Target (Opening RvR): Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense Stance: Priority 85
4. Static Damage Shield: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst / Lightning)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For 2H DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Two-Handed Weapon Styles: Priority 90 (At 85% mob health)
3. Instant Lightning Blast: Priority 85
4. `AtlasOF_MCL`: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Endurance & Mana**: Weaving 2H styles and lightning consumes both endurance and mana; balance power usage and pop `AtlasOF_MCL` when below 25% mana.

### 13. What the AI SHOULD Do
- Hold melee attacks and spells until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Weave instant lightning blasts between two-handed melee swings.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never cast lightning into the pull pack before mobs arrive at the pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Fire Instant Lightning -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain -> Weave Instant Blast.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, parry, and call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10]
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 30] (Boosts 2H melee damage)
9. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 35]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 38]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 39]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 42]
15. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 43]
16. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 46]
17. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 47]
18. `AtlasOF_LongWind` (Rank 1) - 3 pts [Total: 50]

---

## Specialization 3: Battle Thane Hybrid
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Stormcalling"
  min_point_ratio: 0.50
  spec_type_aliases: ["BattleThane", "OneHandHybrid"]
```

### 1. Intended Role
Balanced Frontline Brawler, Lightning Interceptor, Shield Block Peeler, and Healer Bodyguard.

### 2. Intended Role Summary
The Battle Thane Hybrid strikes a balanced chord between Thor's storm magic, reliable weapon style execution, and medium shield defense. With 48 Stormcalling, 44 base weapon, and 35 Shield, it delivers high style damage, dependable shield blocks and stuns, instant lightning blasts, and solid bodyguard support for Midgard's healers.

### 3. Combat Philosophy
Adaptive frontline flexibility. The Battle Thane can hold position guarding healers with shield blocks and instant blasts, or step into the melee fray to assist damage dealers with heavy style chains and lightning bursts.

### 4. Mode: RvR
- **Healer Protection & Peel**: Guard Midgard healers; block incoming strikes; peel enemy rushers with shield stuns and instant lightning blasts.
- **Melee Assist Burst**: Deliver high-damage weapon styles on called targets while weaving instant lightning shouts.
- **Positioning**: Flexible midline (200â€“300 units ahead of Healer).

### 5. Mode: PvP
- **Shield Stun & Blast**: Shield stun opponent -> Instant lightning blast -> Execute weapon style chain -> Block counter-attacks.

### 6a. Mode: PvE Standard
- **Versatile Tanking**: Off-tank secondary adds with shield; guard healer; assist on focus target with weapon styles and lightning.

### 6b. Mode: PvE Pet Pulling (Balanced Healer Guard & Peeler Role)
- **Role During Pet Pulling**: The Battle Thane is **NOT the puller**. It acts as **stationary healer bodyguard and break-off peeler**.
- **Guard Primary Healer**:
  - Set **active `Guard` and `Protect` on the primary Healer/Shaman** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the Healer at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Shield Stun & Blast)**:
  - If any mob breaks away from the pet toward the Healer, **the Thane immediately intercepts it**:
  - Hit the loose add with an **Instant Lightning Blast** or **Shield Stun**!
  - Destroy the stunned add while keeping the Healer 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Thane assists DPS on the primary focus target with weapon styles and instant shouts, remaining within peeling distance of the Healer.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **BALANCED HYBRID SUSTAIN**. High melee weapon skill backed by Stormcalling procs and chain armor.
  - **Corridor Pull Isolation**: Lightning pull into hallway; melee down.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with full buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Lightning shout pull from doorway.
  - 2. Weapon style opening chain on contact.
  - 3. Point-blank lightning shout weave.
  - 4. Execution style to finish.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when resources < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant shout -> Sprint retreat.

### 7. Offensive Priorities
1. Shield Stun / Instant Lightning on Loose Break-Off Add: Priority 100
2. Weapon Style Chain on Focus Target (at 85% HP): Priority 90
3. Instant Lightning Blast on Cooldown: Priority 85
4. Static Damage Shield Maintenance: Priority 75

### 8. Defensive Priorities
1. Guard on Primary Healer: Priority 100 (Keep active 100% of time)
2. Protect on Primary Healer: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Healer: Priority 100 (For Shield Stun / Blast)
2. Primary Focus Target (at 85% HP): Priority 85 (For Melee DPS & Shouts)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Healer Side (within 150â€“200 units of guarded Healer)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Healer)
2. Shield Stun Style: Priority 95 (On loose add)
3. Instant Lightning Blast: Priority 90
4. Weapon Styles: Priority 85 (At 85% mob health)
5. `AtlasOF_Purge`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Balance power usage between instant blasts and weapon styles; pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Keep Guard and Protect active on the primary Healer at all times.
- Hold position at camp beside the Healer during pet pulling.
- Shield stun and blast any loose add that breaks toward the healer.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the Healer to chase DPS far from camp.
- Never cast lightning into the pull pack before mobs arrive at the pet.
- Never let Guard drop from the primary Healer.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Healer**: Select Add -> Fire Instant Lightning -> Execute Shield Stun -> Taunt Style.
- **Condition: Mob Pack at Pet & HP <= 85% & Healer Safe**: Select Primary Focus Target -> Execute Weapon Style Chain -> Fire Instant Blast.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Activate Engage / Block defense, rely on Healer heals and shield block.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10]
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 30]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 40]
12. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]
