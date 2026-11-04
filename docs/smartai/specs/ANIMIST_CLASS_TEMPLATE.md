---
schema_version: 1
class_name: "Animist"
class_id: 55
realm: "Hibernia"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Animist (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Creeping Path`: Stationary plant turret spires (Turret Garden - Fire Ferns, Briar Spores), long-range single and AoE Roots, Snares, and nature Damage-Over-Time (DoT).
  - `Arboreal Path`: High-damage Direct Damage (DD) nukes, Ground-Target Area of Effect (GTAoE) bombardment spells, and pet bombardier summons.
  - `Verdant Path`: Summon Main Guardian Tree Pet, pulsing damage shield buffs, pet mending, and Point-Blank Area of Effect (PBAoE) plant auras.
- **Base Abilities**: Sprint, Quick Cast.
- **Hibernia's Plant Commander**: Master of stationary turret fortresses and devastating ground-targeted bombardment.

---

## Specialization 1: Creeping Animist
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Creeping_Path"
  min_point_ratio: 0.50
  spec_type_aliases: ["CreepingAnimist"]
```

### 1. Intended Role
Turret Garden Master, Area Denial Specialist, and Root Controller.

### 2. Intended Role Summary
The Creeping Animist creates impenetrable defensive zones by planting a network of stationary plant turrets (the "Turret Garden"). These turrets fire continuous volleys of direct damage, snares, and roots at any enemy entering the kill zone, completely shutting down choke points and incoming charges.

### 3. Combat Philosophy
Zone control and automated suppression. The Creeping Animist anchors at camp, sets up turret networks around the group perimeter, and uses long-range roots to hold enemies inside the turret kill zone.

### 4. Mode: RvR
- **Turret Nesting**: Plant 4â€“8 stationary fire-turrets in choke points (milegates, bridges, keep doors) to create lethal kill zones.
- **Root Containment**: Apply long-range Roots and Snares to keep enemy fighters trapped within turret range.
- **Positioning**: 1350 units back behind the turret perimeter.

### 5. Mode: PvP
- **Turret Kiting**: Plant turrets, kite around them, and use roots to hold opponents while turrets burn them down.

### 6a. Mode: PvE Standard
- **Perimeter Defense**: Plant turrets at camp to assist in burning down incoming pulls.

### 6b. Mode: PvE Pet Pulling (Turret Defense & Burn Role)
- **Role During Pet Pulling**: The Animist is **NOT the designated puller**. It acts as stationary camp defense and turret artillery.
- **Camp Turret Placement**:
  - Prior to the pull, summon 2â€“3 stationary turrets tightly anchored around the camp center.
  - Do NOT cast turrets along the pull path; turrets must remain focused inside the camp kill zone.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the puller's pet while the damage shield does damage.
  - Hold fire and refrain from planting additional turrets while mobs are in transit.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, cast **Single Root** or let camp turrets auto-target the break-off add to protect group members.
- **The 85% Mob Health AoE Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Creeping Animist and its turret garden unleash maximum fire into the cluster to accelerate the burn.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **SUPREME TURRET DEFENSE IN CORRIDORS**. The Creeping Animist summons static turret mushrooms (wisps) that blast enemies with continuous magic artillery. In dungeons (Coruscating Mine, Treibh Caillte, Muire Tomb), the Animist plants a "turret forest" of 3-5 wisps inside a doorway or corridor bottleneck. Any mob that approaches is melted within 3 seconds by concentrated turret crossfire before it can reach the Animist. Zero risk of melee interrupts.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Turrets destroy mobs almost instantaneously.
  - **Secondary (Yellow)**: Standard 1-on-1 baseline. Turret cluster kills mob in 3 volleys.
  - **Restricted (Orange)**: Fully viable; plant 5 turrets and snare mob as it approaches.
  - **Prohibited (Red / Purple)**: Avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Plant 3-4 Creeping Turrets (wisps) in a clustered semi-circle at a corridor bottleneck or room threshold.
  - 2. Opener: Cast baseline Creeping Snare/DD from max range to pull mob toward the turret kill zone.
  - 3. Crossfire: Mob enters corridor bottleneck; all turrets automatically focus fire simultaneously.
  - 4. Finisher: Mob vaporizes under concentrated turret bolts.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit safely behind turret cluster when Mana < 20% or Health < 50%.
  - **Safety Check**: Turrets automatically engage any roaming patrols that approach the camp.
  - **Resume Threshold**: Stand when Mana >= 80%.
- **Emergency Escape Protocol**:
  - If overwhelmed: Plant instant wisp -> cast baseline Tangler/Root -> Sprint retreat down cleared corridor.

### 7. Offensive Priorities
1. Plant Turret Spire: Priority 95 (Maintain turret perimeter at camp)
2. Long-Range Single / AoE Root: Priority 90 (Trap loose adds or incoming enemy melee)
3. Nature DoT: Priority 80 (Apply to primary kill target)
4. Baseline DD: Priority 60 (Cast at 85% mob health threshold)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible turret casting)
3. Root on Attacker: Priority 90 (Root melee pursuer)
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For root lockdown)
2. Clustered Mob Pack around Pet (at 85% HP): Priority 90 (For turret focus)
3. Enemy Caster / Healer in RvR: Priority 85

### 10. Positioning Preferences
- **Anchor**: Group Camp Center
- **Target Range**: 1200â€“1400 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Plant Turret Spire: Priority 90
4. Single / AoE Root: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 25% (Turrets consume steady power; monitor mana pools)

### 13. What the AI SHOULD Do
- Establish turret perimeter at camp before combat.
- Root loose break-off adds during pet pulling to protect healers.
- Focus turret fire on pet cluster once mobs hit 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never plant turrets along the pull path (causes accidental adds/links).
- Never run away backwards out of camp to seek max range.
- Never cast roots on targets taking heavy melee damage.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Root.
- **Condition: Mob Pack HP <= 85%**: Direct Turrets -> Cast Nature DoT -> Baseline DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease planting new turrets, rely on existing garden and baseline spells.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildMinion` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Turret crits)
5. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42]
8. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 46]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 50]

---

## Specialization 2: Arboreal Animist
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Arboreal_Path"
  min_point_ratio: 0.50
  spec_type_aliases: ["ArborealAnimist"]
```

### 1. Intended Role
GTAoE Bomber, Heavy Direct Damage Artillery, and Siege Destroyer.

### 2. Intended Role Summary
The Arboreal Animist specializes in massive Ground-Targeted Area of Effect (GTAoE) bombardment spells and high-yield direct damage nukes, providing devastating area firepower against clustered targets and siege lines.

### 3. Combat Philosophy
Long-range artillery bombardment into enemy clusters. The Arboreal Animist drops continuous GTAoE bombardment zones onto enemy formations, forcing them out of cover while sniping called targets with heavy Arboreal DDs.

### 4. Mode: RvR
- **GTAoE Bombardment**: Drop continuous GTAoE bombardment on keep ramps, doors, bridge choke points, and enemy caster clusters.
- **Arboreal DD Burst**: Assist the assist train with heavy Arboreal DD nukes on called targets.
- **Positioning**: 1350â€“1500 units back.

### 5. Mode: PvP
- **GTAoE Traps**: Drop GTAoE on opponent's escape path, root, and burn with Arboreal DD.

### 6a. Mode: PvE Standard
- **Area Bombardment**: Drop GTAoE on grouped monster pulls once tanks establish aggro.

### 6b. Mode: PvE Pet Pulling (GTAoE Artillery Role)
- **Role During Pet Pulling**: The Arboreal Animist is **NOT the puller**. It acts as stationary GTAoE artillery and single-target sniper.
- **Hold Fire on Pull**: Strictly hold all GTAoE and DD spells while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet toward the group, cast Arboreal DD to incinerate that loose add immediately.
- **THE 85% MOB HEALTH GTAoE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Arboreal Animist drops continuous **GTAoE Bombardment spells** directly centered on the mob cluster anchored to the pet!
  - Follow up with spec Arboreal DD on the designated primary kill target.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **HIGH DIRECT DAMAGE TURRET DISPATCH**. Arboreal Animists utilize heavy direct damage turrets and personal nukes. In dungeon corridors, placing 2 Arboreal turrets at a doorway creates an impenetrable sniper chokepoint.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid kills; high turret damage downs mob instantly.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Viable with full turret cluster.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Plant 2-3 Arboreal turrets in hallway bottleneck.
  - 2. Opener: Baseline DD pull into turret range.
  - 3. Turret Burst: Turrets unleash heavy single-target bolts.
  - 4. Finisher: Personal Arboreal DD finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind turrets when Mana < 25%.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - Turrets distract add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. GTAoE Bombardment: Priority 100 (Drop on mob cluster anchored to pet at 85% HP)
2. Spec Arboreal Direct Damage: Priority 85 (Nuke primary target at 85% mob HP)
3. Baseline Root: Priority 75 (Peel loose adds)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible GTAoE casting)
3. Root on Attacker: Priority 90
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For GTAoE Bombardment)
2. Loose Break-off Add: Priority 95 (For rapid incineration)
3. Primary Assist Target: Priority 80 (For Arboreal DD)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350â€“1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. GTAoE Bombardment: Priority 95 (At 85% mob health)
3. `AtlasOF_MasteryOfConcentration`: Priority 90
4. Spec Arboreal DD: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Mana Floor**: 20%
- **GTAoE Throttling**: Stop GTAoE when mana drops below 20% or when only 1 mob remains; switch to single-target DD.

### 13. What the AI SHOULD Do
- Drop continuous GTAoE bombardment on the pet cluster at 85% mob health.
- Hold fire completely while mobs are in transit to camp.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast GTAoE while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Target Center of Cluster -> Cast GTAoE Bombardment.
- **Condition: Loose Add threatening healer**: Target Add -> Cast Arboreal DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease GTAoE casting, withdraw to backline, cast baseline DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Spell crit chance)
5. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_AugAcuity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 48]
8. `AtlasOF_Serenity` (Rank 1-2) - 2 pts [Total: 50]

---

## Specialization 3: Verdant Animist
- **role**: Buffer
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Verdant_Path"
  min_point_ratio: 0.50
  spec_type_aliases: ["VerdantAnimist"]
```

### 1. Intended Role
Guardian Tree Tank Master, Damage Shield Provider, and PBAoE Aura Specialist.

### 2. Intended Role Summary
The Verdant Animist commands a sturdy Guardian Tree pet, provides high-potency pulsing damage shields, sustains the pet with mending spells, and unleashes plant PBAoE damage pulses.

### 3. Combat Philosophy
Living fortification and tank support. The Verdant Animist uses the Guardian Tree as a physical damage sponge, layers pulsing damage shields, and provides defensive peel.

### 4. Mode: RvR
- **Guardian Pet Harass**: Command Guardian Tree pet to intercept and peel enemy frontline melee trains.
- **Damage Shield Uptime**: Maintain pulsing damage shields on friendly melee allies.
- **Positioning**: 1200â€“1400 units back.

### 5. Mode: PvP
- **Tree Pet Dueling**: Let Guardian Tree absorb hits while casting damage shields and baseline DD.

### 6a. Mode: PvE Standard
- **Pet Tanking**: Command Guardian Tree to engage and tank primary targets.

### 6b. Mode: PvE Pet Pulling (Support & Break-Off Tank Role)
- **Role During Pet Pulling**: The Verdant Animist is **NOT the puller**. It acts as stationary camp defense and break-off add interceptor.
- **Damage Shield Buffs**: Maintain damage shield active on self and group members.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the puller's pet while the damage shield deals continuous damage.
  - Do NOT send Guardian Tree out to meet the pull pack in transit.
- **Loose Mob Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the puller's pet toward the group, command the **Guardian Tree pet to attack and taunt that loose add immediately** to protect group members.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the mob cluster reaches **85% health**, command Guardian Tree pet to assist on the primary kill target while casting baseline DD.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT PET TANK & SUSTAIN**. Verdant Animists summon a durable Forest Guardian pet alongside healing/buffing wisps. In dungeons, the Guardian acts as a dedicated door-block while healing wisps keep the pet topped off.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Zero-damage grinding; Guardian pet absorbs all hits.
  - **Secondary (Yellow)**: Standard target. Guardian tanks while Animist assists.
  - **Restricted (Orange)**: Viable due to healing wisp sustain.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Summon Forest Guardian pet and plant 1 healing wisp.
  - 2. Opener: Send Guardian to engage mob at doorway.
  - 3. Support: Cast Verdant buffs / DD while wisp heals Guardian.
  - 4. Finisher: Guardian finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Guardian intercepts add -> Sprint away to safety.

### 7. Offensive Priorities
1. Guardian Tree Pet Attack: Priority 95 (Engage loose add or primary target at 85% mob HP)
2. Pulsing Damage Shield: Priority 90 (Keep active on pet and allies)
3. Baseline DD: Priority 60 (Cast at 85% mob health threshold)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Pet Mending / Heal: Priority 90 (Sustain Guardian Tree pet)
3. Mastery of Concentration: Priority 85
4. Self-Cast Buffs: Priority 80

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (Command Guardian Tree to intercept)
2. Primary Assist Target: Priority 85 (At 85% mob HP)
3. Friendly Healer under attack: Priority 80

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **Target Range**: 1100â€“1350 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Guardian Tree Pet Commands: Priority 95
3. Pulsing Damage Shield: Priority 90
4. Pet Mending: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Mana Floor**: 25% (Reserve power for re-summoning pet and mending)

### 13. What the AI SHOULD Do
- Command Guardian Tree pet to intercept break-off adds immediately during pet pulls.
- Maintain damage shields on friendly party members.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never send Guardian Tree pet out along the pull path while the puller is returning to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Target Add -> Issue `/pet attack`.
- **Condition: Mob Pack HP <= 85%**: Target Primary Assist Target -> Direct Pet -> Cast Baseline DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from baseline DD, conserve power exclusively for pet mending.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_WildMinion` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 15] (Pet crits)
3. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 25]
4. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 30]
5. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 40]
6. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 44]
7. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 48]
8. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 2 pts [Total: 50]
