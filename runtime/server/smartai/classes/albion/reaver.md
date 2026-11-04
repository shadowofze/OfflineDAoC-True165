---
schema_version: 1
class_name: "Reaver"
class_id: 19
realm: "Albion"
primary_stats: ["Strength", "Piety", "Constitution", "Dexterity"]
default_personality: "Aggressive"
---

# Class: Reaver (Albion)

## Shared Class Capabilities
- **Armor Type**: Chain (Level 20+), Studded (1–19).
- **Weapon Slots**: Flexible Weapons (Albion's signature spiked flails and whips); One-Handed Slashing, Thrusting, Crushing; Medium and Large Shields.
- **Core Specialization Lines**:
  - `Soulrending`: Dark magic featuring high-damage Soul Lifedrains (steals enemy health), instant Area of Effect (AoE) DoTs, instant Direct Damage shouts, damage debuffs, and necrotic damage shields.
  - `Flexible`: Flail weapon specialization delivering deadly bleed chains, positional stuns, snares, and defense bonuses.
  - `Shields`: High-tier shield styles (Slam: 9-second stun), shield blocking, and defensive party protection abilities (`Guard`).
  - `Parry`: Passive melee deflection.
- **Base Abilities**: Sprint, Taunt, Engage.
- **Albion's Dark Knight**: Clad in dark chainmail and wielding spiked flails, the Reaver combines ferocious physical style damage with soul-draining necrotic spells, crippling opponents with 9-second shield stuns while siphoning their life force into its own health pool.

---

## Specialization 1: Soulrending Flexible Reaver
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Soulrending"
  secondary_line: "Flexible"
  min_point_ratio: 0.50
  spec_type_aliases: ["OneHandAndShield"]
```

### 1. Intended Role
Soulrending Flail Knight, 9-Second Shield Slam Peeler, Lifedrain Self-Sustainer, Healer Bodyguard (`Guard`), and Flexible Melee Finisher.

### 2. Intended Role Summary
The Soulrending Flexible Reaver is Albion's premier dark hybrid defender. Combining 50 Soulrending with 42 Flexible and 42 Shield, it delivers reliable 9-second Shield Slams, protects Albion's primary healers via `Guard`, inflicts heavy bleed damage with flail styles, and finishes enemies while healing itself with instant soul lifetaps.

### 3. Combat Philosophy
Attrition through physical lockdown and lifetap recovery. The Reaver guards Albion's support core, intercepts attackers with 9-second shield stuns, applies flail bleeds, and sustains its own health pool through relentless soul lifetaps.

### 4. Mode: RvR
- **Healer Protection & Peel**: Anchor beside Albion Clerics; maintain active `Guard` to absorb incoming melee strikes.
- **Shield Slam & Soul Tap**: Slam enemy stealthers or melee with a 9-second shield stun, then immediately siphon their health with Soul Lifedrain.
- **AoE Soul DoT**: Drop instant AoE Soul DoT onto enemy assist clusters to unstealth hidden enemies and pressure healer power.
- **Positioning**: Midline anchor (200–300 units ahead of Cleric).

### 5. Mode: PvP
- **Slam & Drain**: Shield Slam (9s stun) -> Positional Flexible bleed chain -> Cast Soul Lifedrain -> Outlast opponent through self-healing burst.

### 6a. Mode: PvE Standard
- **Off-Tank / Sustained DPS**: Hold secondary adds with shield slam; guard healer; assist on focus target with flail styles and lifetaps.

### 6b. Mode: PvE Pet Pulling (Healer Bodyguard & Lifetap Peeler Role)
- **Role During Pet Pulling**: The Reaver is **NOT the puller and NOT the main aggro holder**. It acts as the **stationary camp anchor, healer bodyguard, and break-off peeler**.
- **Crucial Rule: NO TAUNTING OR CASTING ON THE MAIN PACK**:
  - **Do NOT cast AoE DoTs or taunt mobs centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
  - AoE DoTs on incoming mobs will immediately pull the entire pack off the pet onto the Reaver!
- **Active Guard on Healer**:
  - Set **active `Guard` on the primary Cleric** before the pull begins.
- **Hold Spells and Melee During Transit**:
  - Strictly hold all lifetaps, DoTs, and melee swings while the pet is bringing mobs to camp.
- **Break-Off Add Protection (Shield Slam & Lifetap Peel)**:
  - If any mob breaks away from the pet toward the Cleric or casters, **the Reaver steps in immediately**:
  - Hit the loose add with a **Shield Slam (9-second stun)** or **Soul Lifedrain**!
  - Destroy that specific loose add while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Reaver assists the group:
  - Engage the primary focus target in the cluster with **Flexible flail styles** and cast **Soul Lifedrain** to accelerate the kill!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Chain armor, Flexible whip styles with rear/side snares and stuns, pulsing Soulrending damage auras, and ranged lifetaps make the Reaver exceptionally effective in dungeon corridors (Catacombs, Tomb of Mithra).
  - **Corridor Pull Isolation**: Use ranged Soulrending Lifetap from doorway to pull an isolated mob into a cleared corridor. The lifetap provides initial threat and health buffering before melee engagement.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Soulrending auras and lifetaps keep Reaver at 100% HP throughout combat.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Lifetaps offset incoming melee damage.
  - **Restricted (Orange)**: Viable; activate Soulrending damage auras and debuffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Activate Soulrending damage aura on self.
  - 2. Opener: Cast ranged Soulrending Lifetap on target from 1000 units to initiate pull.
  - 3. Melee Contact: Execute Flexible opening style (Leviathan / flexible chain).
  - 4. Stun / Bleed: Land positional flexible stun or bleed style.
  - 5. Finisher: Instant lifetap or flexible execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20% or Endurance < 25%. Lifetaps keep Health high.
  - **Resume Threshold**: Stand when Mana >= 85% and Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Cast flexible whip snare / instant lifetap on add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Soul Lifedrain on Loose Add / Focus Target: Priority 95
3. Flexible Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Instant AoE Soul DoT on Clustered Pack (at 85% HP): Priority 80

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Never let drop)
2. Soul Lifedrain for Self-Heal: Priority 95 (When own HP drops below 70%)
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening Cleric: Priority 100 (For Shield Slam / Lifetap)
2. Primary Assist Kill Target (at 85% HP): Priority 85 (For Flail DPS & Lifetaps)
3. Enemy Melee Attacking Backline (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Tight Backline / Camp Center (within 150–200 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never pursue mobs outside camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Cleric)
2. Shield Slam Style: Priority 95 (On loose add)
3. Soul Lifedrain: Priority 90 (Nuke & self-heal)
4. Flexible Weapon Styles: Priority 85 (At 85% mob health)
5. `AtlasOF_Purge`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Soul spells consume mana; monitor power pool and pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Maintain active `Guard` on the primary Cleric at all times.
- Hold all spells and melee attacks while mobs are in transit to camp.
- Intercept and Shield Slam any loose mob that breaks toward healers.
- Cast Soul Lifedrain to recover health when taking damage.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast AoE Soul DoT while the pet is still pulling mobs to camp.
- Never taunt or attempt to peel the main mob cluster off the puller's pet.
- Never abandon the guarded Cleric to chase DPS far from camp.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward Cleric**: Select Add -> Move into Melee -> Execute Shield Slam (9s Stun) -> Cast Soul Lifedrain.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute Flexible Style Chain -> Cast Lifedrain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Cast instant Soul Lifedrain, enter shield block defense, rely on Cleric heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 10] (Restores mana for soul lifedrains)
3. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 11]
4. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 14]
5. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 20]
6. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 21]
7. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 24]
8. `AtlasOF_MasteryOfBlock` (Rank 3) - 6 pts [Total: 30] (Shield block rate)
9. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 34]
11. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 40] (Flexible weapon style damage)
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 41]
13. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 44]
14. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
15. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
17. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Weapon & Shield Flail Brawler Reaver
- **role**: DefensiveTank
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Flexible"  # Or Slash / Thrust / Crush
  secondary_line: "Shields"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Flail Melee Striker, 9-Second Shield Slam Peeler, Healer Bodyguard (`Guard`), and Frontline Brawler.

### 2. Intended Role Summary
The Weapon & Shield Flail Brawler Reaver maximizes physical melee style lethality while retaining dependable shield crowd control. By taking base weapons (Flexible or Slash/Thrust/Crush) to 50 alongside 42 Shield and 41 Soulrending, it delivers crushing style damage, guaranteed 9-second Shield Slams, and solid soul lifetap support.

### 3. Combat Philosophy
Balanced melee brawling and defensive peeling. The Flail Brawler locks down threats with shield stuns, punishes targets with heavy flail styles, and uses lifetaps to maintain personal combat endurance.

### 4. Mode: RvR
- **Frontline Assist & Peel**: Provide active `Guard` over Albion healers; slam enemy rushers with 9-second Shield Slams; execute called targets with 50-spec flail styles.
- **Positioning**: Midline / Frontline junction (200–300 units ahead of Cleric).

### 5. Mode: PvP
- **Slam & Flail**: Shield Slam (9s stun) -> Positional flail style chain -> Finish with Soul Lifedrain.

### 6a. Mode: PvE Standard
- **Off-Tank**: Guard healer; slam loose adds; assist on focus target with heavy flail styles.

### 6b. Mode: PvE Pet Pulling (Flail Peeler Role)
- **Role During Pet Pulling**: The Flail Brawler is **NOT the puller**. It acts as **stationary healer bodyguard and break-off peeler**.
- **Guard Primary Cleric**:
  - Set **active `Guard` on the primary Cleric** before the pull begins.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the Cleric at camp. Do NOT run out into the field.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Protection (Shield Slam Peeling)**:
  - If any mob breaks away from the pet toward the Cleric, **the Reaver immediately slams it**:
  - Land a **Shield Slam (9-second stun)** directly on the break-off mob!
  - Destroy the stunned add while keeping the Cleric 100% safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Reaver assists DPS on the primary focus target with heavy flail styles.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH DEFENSIVE SURVIVABILITY**. Large Shield blocks and Slam stuns combined with Soulrending auras provide total lockdown in tight corridors.
  - **Corridor Pull Isolation**: Ranged lifetap pull into cleared corridor corner; slam-lock and whip.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Negligible health loss grinding.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Viable with Slam stun.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ranged lifetap pull into safe corridor.
  - 2. Opener: Shield Slam (stun).
  - 3. Combo: Step to flank; execute Flexible whip style chain.
  - 4. Aura & Finisher: Soul aura ticks + execution style finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Endurance < 20% or Mana < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add -> Sprint retreat.

### 7. Offensive Priorities
1. Shield Slam Stun on Loose Break-Off Add: Priority 100
2. Heavy Flexible Weapon Styles on Focus Target (at 85% HP): Priority 90
3. Soul Lifedrain on Injured Target: Priority 85
4. Positional Bleed Style: Priority 80

### 8. Defensive Priorities
1. Guard on Primary Cleric: Priority 100 (Keep active 100% of time)
2. Protect on Primary Cleric: Priority 95
3. Shield Block Stance: Priority 90
4. Purge (AtlasOF_Purge): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded Cleric: Priority 100 (For Shield Slam)
2. Primary Focus Target (at 85% HP): Priority 85 (For Flail DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Healer Side (within 150–200 units of guarded Cleric)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main Cleric)
2. Shield Slam Style: Priority 95 (On loose add)
3. Flexible Weapon Styles: Priority 90 (At 85% mob health)
4. Soul Lifedrain: Priority 85
5. `AtlasOF_Purge`: Priority 80

### 12. Resource Management
- **Endurance & Mana**: Balanced flail style rotation and occasional lifetaps maintain high sustainability.

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
- **Condition: Loose add breaks toward Cleric**: Select Add -> Execute Shield Slam Stun (9s) -> Flail Style Chain.
- **Condition: Mob Pack at Pet & HP <= 85% & Cleric Safe**: Select Primary Focus Target -> Execute Flail Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Cast instant Soul Lifedrain, enter shield block defense, call for Cleric heals.

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
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Full Soulrending Striker Reaver
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Soulrending"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Maximum Soulrending Burst Nuker, Heavy Flail Executioner, Necrotic Damage Dealer, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Full Soulrending Striker Reaver pushes necrotic magical burst to the limit. By taking Soulrending to 50 alongside 50 in primary weapon specialization, it delivers maximum-damage Soul Lifedrains, high-potency necrotic procs, devastating flail style chains, and lethal physical/magical hybrid burst.

### 3. Combat Philosophy
Aggressive necrotic annihilation. The Striker Reaver overwhelms priority targets with heavy flail style chains while weaving high-damage soul lifetaps to delete opponents and keep its own health topped off.

### 4. Mode: RvR
- **Melee Shock Assist**: Charge with the Albion melee assist train; weave 50-spec Soul Lifedrains directly into flail style chains onto called targets.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Necrotic Burst**: Close distance -> Flexible style chain -> Cast Soul Lifedrain -> Repeat lifedrain to burn target while sustaining full health.

### 6a. Mode: PvE Standard
- **Hybrid Melee DPS**: Assist main tank on designated targets with high-damage flail styles and soul lifetaps.

### 6b. Mode: PvE Pet Pulling (Necrotic Enforcer Role)
- **Role During Pet Pulling**: The Striker Reaver is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Reaver immediately engages it**:
  - Intercept the loose add and shred it with flail style chains and Soul Lifedrain!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Reaver assists DPS on the primary focus target with flail style chains and lifetaps.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MAGIC-MELEE HYBRID BURST**. Heavy pulsing damage auras and high-damage lifetaps quickly dispatch single mobs in narrow corridors.
  - **Corridor Pull Isolation**: Ranged lifetap pull back to cleared rooms.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills with high health retention.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Only engage with full mana pool for continuous lifetaps.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure maximum Soulrending aura is active.
  - 2. Opener: Ranged lifetap pull.
  - 3. Melee: Flexible melee style chain as aura pulses continuous damage.
  - 4. Finisher: Point-blank lifetap execution.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Resume Threshold**: Resume at Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Instant lifetap -> Sprint retreat to safety.

### 7. Offensive Priorities
1. Flail Style Burst & Lifedrain on Loose Break-Off Add: Priority 100
2. Flexible Style Chain on Focus Target (at 85% HP): Priority 90
3. Soul Lifedrain on Cooldown: Priority 85
4. Instant AoE Soul DoT on Clustered Pack (at 85% HP): Priority 80

### 8. Defensive Priorities
1. Soul Lifedrain for Self-Heal: Priority 100 (When HP < 70%)
2. Purge (AtlasOF_Purge): Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Flail Burst / Lifetap)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS & Lifetaps)
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Soul Lifedrain: Priority 90 (Nuke & self-heal)
3. Flexible Weapon Styles: Priority 85 (At 85% mob health)
4. `AtlasOF_MCL`: Priority 80
5. `AtlasOF_Determination`: Priority 75

### 12. Resource Management
- **Mana & Endurance**: Balance power usage between lifetaps and weapon styles; pop `AtlasOF_MCL` when mana drops below 25%.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Cast Soul Lifedrain to recover health when taking damage.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Flail Melee Style -> Cast Soul Lifedrain -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Flail Style Chain -> Cast Lifedrain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Cast instant Soul Lifedrain, fall back toward Cleric, call for heals.

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
9. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
10. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34] (Lifedrain damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 35]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 38]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 39]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 42]
15. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 43]
16. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 46]
17. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 47]
18. `AtlasOF_LongWind` (Rank 1) - 3 pts [Total: 50]
