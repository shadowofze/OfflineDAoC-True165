---
schema_version: 1
class_name: "Sorcerer"
class_id: 8
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Sorcerer (Albion)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Mind Magic`: Premier Albion Crowd Control (Area of Effect Mesmerize, Single-Target Mesmerize, Area of Effect Root, Single-Target Root), Pet Charm (control humanoid and undead creatures), Amnesia (instant target break / interrupt), and Mind Direct Damage.
  - `Body Magic`: High-impact Body Lifedrain (deals damage while restoring health to caster), Body Resistance Debuff, single-target Root, and baseline CC.
  - `Matter Magic`: Area of Effect Damage-over-Time (AoE DoT), Matter Direct Damage, Matter Resistance Debuff, and baseline CC.
- **Base Abilities**: Sprint, Quick Cast.
- **Albion's Master of Mental Dominion**: The Sorcerer dictates the battlefield through unmatched crowd control, debuffs, pet manipulation, and lethal spell artillery.

---

## Specialization 1: Mind Sorcerer
- **role**: CrowdControlCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Mind_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["MindSorc"]
```

### 1. Intended Role
Primary Group Crowd Control (AoE Mez / AoE Root), Pet Charmer, Caster Disruptor (Amnesia), and Tactical Midline Anchor.

### 2. Intended Role Summary
The Mind Sorcerer is the tactical linchpin of Albion group combat. It specializes in locking down entire enemy warbands with long-range Area of Effect Mesmerize, anchoring charging melee trains with Area and Single Roots, disrupting opposing casters and healers with instant Amnesia, and commanding charmed NPC pets to wreak havoc on enemy ranks.

### 3. Combat Philosophy
Total battlefield pacification and target isolation. The Mind Sorcerer initiates engagements by neutralizing non-primary targets through long-range Mesmerize, coordinates with Albion's assist train to isolate targets one by one, and uses Amnesia to completely cancel enemy casting chains.

### 4. Mode: RvR
- **Opener**: Cast AoE Mesmerize (`Mind Magic`) from maximum range (1500 units) onto clustered enemy support and caster lines.
- **Re-Mez & Peel**: Monitor Mez timers; re-mez targets broken early by stray damage. Apply Single Root or AoE Root to enemy melee trains charging Albion's support midline.
- **Amnesia Disruption**: Cast Amnesia onto enemy primary healers or casting nukers during critical spell channels to reset their targets and cancel casts.
- **Pet Charm**: Maintain a charmed pet (guard or enemy humanoid/undead) set to attack enemy casters to disrupt casting concentration.
- **Positioning**: 1350â€“1500 units behind frontline tanks, slightly offset from group healers.

### 5. Mode: PvP
- **Control & Isolate**: Open with Mesmerize -> Command charmed pet to attack -> Apply Root -> Cast Mind DD while maintaining distance. Use `Quick Cast` if pressured.

### 6a. Mode: PvE Standard
- **Crowd Control**: Mesmerize secondary and tertiary adds on incoming pulls; keep non-focus targets locked down until the main assist switches to them.
- **Charmed Tank/DPS**: Command charmed mob to off-tank loose mobs or add damage to the primary target.

### 6b. Mode: PvE Pet Pulling (Crowd Control & Break-Off Lockdown)
- **Role During Pet Pulling**: The Sorcerer is **NOT the puller**. It acts as the **camp crowd controller and add lockdown specialist**.
- **Mobs Stay on the Pet**:
  - Allow the designated pet puller's pet to secure proximity threat and let the pulsing damage shield establish baseline aggro.
  - Strictly hold all AoE Mesmerize, DD, and debuffs while mobs are traveling to camp.
- **Break-Off Add Protection (Top Priority)**:
  - If any mob breaks off from the pet toward the group healers or casters, the Mind Sorcerer **immediately casts Single Root or Single Mez** onto that loose add.
  - Never allow loose mobs to reach the group healers.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to approximately **85% health**, the Mind Sorcerer assists the group by casting **Mind DD** on the designated focus mob.
  - Do NOT cast AoE Mez into the cluster at 85% HP, as AoE damage from PBAoE and GTAoE casters will instantly break it.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor. Never flee backwards or run away to seek range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Prioritized** (Avalon Marsh, Salisbury Plains, Campacorentin). Open fields allow full crowd-control kiting and long-range charm manipulation.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO (Unless utilizing Charmed Mob as Tank)**. Standard soloing in tight corridors without a tank is prohibited due to high interrupt risk. However, if a suitable Yellow/Blue humanoid or animal mob is present inside the dungeon entrance, a Mind Sorcerer may Charm it to act as an expendable front-line pet door-block; otherwise, stay outdoors unless grouped.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mez holds reliably; target neutralized while Charmed pet attacks.
  - **Secondary (Yellow)**: Standard solo target. Maintain single-target Mez, debuff, and let pet hold threat.
  - **Restricted (Orange)**: Only engage with Charmed pet actively buffed and positioned between caster and mob.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; mez resists result in rapid death.
- **Pull & Engagement Sequence**:
  - 1. Ensure Charmed pet is controlled and buffed with Damage Shield / Stat buffs.
  - 2. Opener: Long-range Single-Target Mez on target from 1500 units.
  - 3. Pet Engagement: Command Charmed pet to attack sleeping target; mob awakens focused entirely on the pet.
  - 4. Caster Support: Cast Mind DD and debuffs from 1000 units while pet holds aggro.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind Charmed pet when Mana < 25% or Health < 50%.
  - **Safety Check**: Monitor Charm duration pulse timer; re-cast Charm before sitting if duration is low.
  - **Resume Threshold**: Resume hunting at Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - If Charm breaks or adds link: Cast Instant/QuickCast AoE Mez -> Sprint retreat to open safe zone.

### 7. Offensive Priorities
1. Single Root / Mez on Loose Break-Off Add: Priority 100
2. AoE Mesmerize on Uncontrolled Enemy Groups (RvR): Priority 95
3. Amnesia on Enemy Caster / Healer: Priority 85
4. Mind Direct Damage on Focus Target: Priority 70

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse)
2. Mastery of Concentration: Priority 95 (Cast uninterrupted CC under heavy pressure)
3. Single Root on Enemy Melee Attacker: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Root/Mez)
2. Enemy Primary Healers / Casters: Priority 90 (For Mez/Amnesia in RvR)
3. Main Assist Focus Target: Priority 75 (For Mind DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. AoE Mesmerize: Priority 90 (RvR Opener)
4. Amnesia: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **Power Conservation**: Crowd control spells are power-efficient, but repeated AoE Mez and Mind DD spam drain reserves.
- **MCL Usage**: Pop `AtlasOF_MCL` when power drops below 25%. Cease DD spam and prioritize purely CC if power is below 15%.

### 13. What the AI SHOULD Do
- Open RvR engagements with long-range AoE Mesmerize on enemy backlines.
- Instantly Root any loose mob that breaks away from the pet during PvE pet pulling.
- Cast Amnesia to cancel enemy healer casts and interrupt enemy nukers.
- Maintain stationary discipline at the camp anchor during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast AoE Mesmerize on the mob cluster after the 85% AoE burn has begun (it will instantly break).
- Never cast spells on incoming mobs while the pet is still in transit pulling them to camp.
- Never run backwards away from camp seeking range when mobs arrive.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Cast Single Root / Mez.
- **Condition: Incoming un-mezed enemy group (RvR)**: Select Center Target -> Cast AoE Mez.
- **Condition: Enemy healer casting big heal**: Select Healer -> Cast Amnesia.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Cast Mind DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease all direct damage; conserve mana exclusively for emergency peel roots on healers.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfTheArt` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Body Sorcerer
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Body_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["BodySorc"]
```

### 1. Intended Role
Body Lifedrain Burst Nuker, Body Resistance Debuffer, Secondary CC Peeler, and Self-Sustaining Artillery.

### 2. Intended Role Summary
The Body Sorcerer is Albion's self-healing offensive powerhouse. Specializing in Body Magic, it applies massive Body resistance debuffs, fires high-damage Lifedrains that siphon enemy life directly into the Sorcerer's health pool, and locks down threatening melee with baseline crowd control.

### 3. Combat Philosophy
Sustained offensive pressure with built-in survivability. The Body Sorcerer softens targets with resistance debuffs, coordinates with Cabalists and Necromancers in the "Body Assist Train", and neutralizes incoming damage through relentless lifetaps.

### 4. Mode: RvR
- **Body Debuff Train**: Coordinate with Albion Body casters; apply Body Resistance Debuff (-40% / -50%) to the primary target, then unleash spec Body Lifedrains.
- **Self-Sustain**: Use Lifedrain to recover from enemy archer or caster poke without taxing Albion healer mana pools.
- **Peel Support**: Apply baseline Root or Mez to enemy peelers pursuing Albion support.
- **Positioning**: 1200â€“1350 units back in the caster line.

### 5. Mode: PvP
- **Debuff & Drain**: Open with Root -> Body Resistance Debuff -> Quick Cast Lifedrain -> Repeat Lifedrain to burn target while keeping own HP at 100%.

### 6a. Mode: PvE Standard
- **Single-Target Burst**: Debuff Body resistance on main tank's target -> Burst with Body Lifedrain -> Assist on subsequent targets.

### 6b. Mode: PvE Pet Pulling (Body Debuff & Lifedrain Sniper Role)
- **Role During Pet Pulling**: The Body Sorcerer is **NOT the puller**. It acts as **stationary single-target burst artillery**.
- **Hold Fire During Transit**: Strictly hold all debuffs and lifetaps while the pet puller brings the mobs to camp. Do NOT cast on mobs in motion.
- **Break-Off Add Protection**:
  - If a loose mob breaks off from the pet toward the group, cast **Single Root** or immediately burn it down with **Body Lifedrain**.
- **THE 85% MOB HEALTH BURN TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Body Sorcerer activates:
  - Cast **Body Resistance Debuff** on the primary focus target in the cluster.
  - Follow immediately with rapid **spec Body Lifedrain** casts to rapidly eliminate targets one by one!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do NOT run away or flee backwards for range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Black Mountains, Salisbury Plains). Open areas permit maximum kiting distance while lifetaps and Body DD whittle targets down.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons feature narrow bottlenecks where body casters cannot kite. Physical interrupts lock out lifetaps, leading to swift demise. Group with a tank and healer for dungeon crawling.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Lifetap returns 100% value; mob dies before reaching melee range.
  - **Secondary (Yellow)**: Standard target. Open with Body Debuff + Root, follow with continuous Lifetaps.
  - **Restricted (Orange)**: Only engage with QuickCast ready and full mana pool.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; high spell resists negate lifetap healing.
- **Pull & Engagement Sequence**:
  - 1. Establish 1500-unit casting range with clear retreat lane.
  - 2. Opener: Cast Body Resistance Debuff followed immediately by baseline Root.
  - 3. Lifetap Leech: Step back to 1400 units and channel heavy Body Lifetap nukes, recovering any lost health while damaging the target.
  - 4. Finisher: QuickCast Body DD or instant lifetap to execute target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Drop to sit when Mana < 25%. Lifetaps keep Health high, so Mana is the primary recovery bottleneck.
  - **Safety Check**: Verify 1000-unit clear radius before sitting.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - On add or early root break: QuickCast Root / Mez -> Sprint away to cleared terrain.

### 7. Offensive Priorities
1. Body Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Body Lifedrain: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Single Root on Break-Off Add: Priority 80
4. Baseline Mesmerize: Priority 70

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95 (Cast Lifedrains uninterrupted while focused)
3. Body Lifedrain for Self-Heal: Priority 90 (When own HP drops below 70%)
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group: Priority 100 (For Root / Drain)
2. Primary Assist Kill Target: Priority 90 (For Body Debuff -> Lifedrain)
3. Enemy Light Armor Caster / Support: Priority 75

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from mob cluster)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Body Resistance Debuff: Priority 90
4. Spec Body Lifedrain: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Self-Healing Efficiency**: Lifedrains generate both damage and health, drastically reducing healer strain.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Apply Body Resistance Debuff before nuking focus targets.
- Use Lifedrain to recover self-health when taking ranged or caster damage.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast lifetaps on incoming mobs while the pet is still in transit pulling them to camp.
- Never run away backwards out of camp to seek casting range.
- Never spam debuffs on secondary targets while the primary target is still alive.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Root -> Drain.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Target -> Cast Body Debuff -> Spam Lifedrain.
- **Condition: Own HP < 60%**: Select Current Target -> Cast Quick Cast Lifedrain.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease debuffing; cast only baseline lifetaps when strictly needed for self-preservation.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Matter Sorcerer
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Matter_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["MatterSorc"]
```

### 1. Intended Role
Area Damage-over-Time (AoE DoT) Bombardier, Matter Direct Damage Nuker, Stealth Disrupter, and Pressure Caster.

### 2. Intended Role Summary
The Matter Sorcerer specializes in spreading debilitating Area of Effect Damage-over-Time (AoE DoT) afflictions across entire enemy groups. This pressure prevents enemies from dropping combat or stealthed movement, drains healer power reserves over extended engagements, and softens targets for Matter Direct Damage follow-ups.

### 3. Combat Philosophy
Attrition through sustained area damage. The Matter Sorcerer blankets opposing forces in heavy ticking DoTs, uses baseline crowd control to peel, and finishes softened enemies with Matter DD.

### 4. Mode: RvR
- **AoE DoT Carpet**: Cast AoE Matter DoT on clustered enemy formations, choke points, and keep doors to unstealth hidden enemies and disrupt casting.
- **Matter DD Follow-up**: Assist Albion nukers with Matter Direct Damage against debuffed targets.
- **Peel Defense**: Use baseline Mez or Root to halt advancing enemy peelers.
- **Positioning**: 1350 units back, maximizing AoE spell reach.

### 5. Mode: PvP
- **DoT & Kite**: Apply Matter DoT -> Cast Single Root -> Maintain distance -> Fire Matter DD while DoT ticks.

### 6a. Mode: PvE Standard
- **Multi-Mob Cleave**: Apply AoE Matter DoT across multi-mob pulls once the tank establishes threat; burst primary targets with Matter DD.

### 6b. Mode: PvE Pet Pulling (The AoE DoT Acceleration Role)
- **Role During Pet Pulling**: The Matter Sorcerer is **NOT the puller**. It acts as the **AoE DoT pressure specialist**.
- **Hold Fire on Pull**:
  - Strictly hold fire while the pet puller is bringing the mobs back to camp.
  - Do NOT apply DoTs while mobs are in transit (DoT ticks generate intense aggro that pulls mobs off the pet!).
- **Break-Off Add Protection**:
  - If any mob breaks away from the pet toward the group, cast **Single Root** to freeze it in place.
- **THE 85% MOB HEALTH AoE DoT TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Matter Sorcerer unleashes:
  - Cast **AoE Matter DoT** directly onto the cluster anchored around the pet!
  - Follow up with **Matter DD** on the primary focus target within the pack.
  - The AoE DoT will tick down all mobs simultaneously while the damage shield continues melting them!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor alongside the group. Do NOT run away or seek max range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Avalon Marsh, Cornwall). Open terrain allows multi-DoT kiting rotations across wide circular paths.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Tight dungeon corridors trap the DoT kiter against walls and into linked monster packs. Solo dungeon leveling is prohibited; group with a tank and healer.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Matter DoTs kill target over duration with zero risk of mob reaching caster.
  - **Secondary (Yellow)**: Standard target. Apply full DoT stack, snare, and kite.
  - **Restricted (Orange)**: Only engage with full mana and large open kiting circle.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; DoT resists waste mana and mob speed overwhelms kiting.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units with wide open circular kiting space.
  - 2. Opener: Cast Primary Matter DoT followed immediately by Secondary DoT.
  - 3. Snare & Kite: Apply Matter Snare, turn and jog along a wide circular arc while DoTs tick down mob HP.
  - 4. Finisher: Cast Matter DD or let final DoT tick execute the target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Safety Check**: Ensure no wandering mobs along kiting path before sitting.
  - **Resume Threshold**: Resume at Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - QuickCast AoE Mez -> Sprint retreat to safety.

### 7. Offensive Priorities
1. AoE Matter DoT on Mob Cluster (at 85% HP): Priority 95
2. Single Root on Break-Off Add: Priority 90
3. Matter Direct Damage on Focus Target: Priority 85
4. Single-Target Matter DoT: Priority 70

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95
3. Baseline Root on Melee Attacker: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Mob Cluster at Pet (at 85% HP): Priority 100 (For AoE DoT)
2. Loose Break-off Mob threatening group: Priority 95 (For Single Root)
3. Primary Focus Target: Priority 80 (For Matter DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Spec AoE Matter DoT: Priority 90 (At 85% mob health)
4. Matter Direct Damage: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **AoE DoT Efficiency**: AoE DoTs provide exceptionally high damage per mana point.
- **Power Recovery**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Hold all DoTs and damage spells until the mob pack hits 85% health at camp.
- Cast AoE Matter DoT onto the anchored mob cluster at 85% health to accelerate cleave speed.
- Root any loose mob that breaks away from the pet toward the healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast AoE DoT or single DoTs while the pet is still pulling mobs to camp.
- Never flee backwards out of camp to seek casting range.
- Never recast AoE DoT while the existing DoT is still ticking on the targets.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85% & No DoT Active**: Cast AoE Matter DoT.
- **Condition: Loose add breaks toward healer**: Select Add -> Cast Single Root.
- **Condition: Mob Cluster at Pet & DoT Active**: Select Primary Focus Target -> Cast Matter DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease direct damage; rely on existing ticking DoTs and save power for emergency roots.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]
