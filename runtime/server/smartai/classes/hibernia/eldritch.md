---
schema_version: 1
class_name: "Eldritch"
class_id: 40
realm: "Hibernia"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Aggressive"
---

# Class: Eldritch (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Mana`: Point-Blank Area of Effect (PBAoE) Mana Bomb (crushing area annihilation), long-range Nearsight debuffs, and area Snares.
  - `Void`: Long-range Cold Bolt, Matter direct damage (DD), Cold resistance debuffs, and single-target Roots.
  - `Light`: Area-of-Effect Mesmerize (AoE Mez), single-target Stun, Energy direct damage (DD), and single-target Disease.
- **Base Abilities**: Sprint, Quick Cast.
- **Hibernia's Arcane Destruction Engine**: The premier pure destructive caster, capable of obliterating clustered groups with PBAoE mana waves or executing long-range bolt snipes.

---

## Specialization 1: Mana Eldritch
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Mana"
  min_point_ratio: 0.50
  spec_type_aliases: ["ManaEld"]
```

### 1. Intended Role
PBAoE Mana Bomber, Nearsight Artillery, and Area Annihilation Specialist.

### 2. Intended Role Summary
The Mana Eldritch is Hibernia's premier area annihilation caster. It specializes in casting devastating Point-Blank Area of Effect (PBAoE) Mana pulses that incinerate clustered enemies, shutting down enemy ranged archers and casters with long-range Nearsight debuffs, and controlling movement with area snares.

### 3. Combat Philosophy
Decisive area devastation and sight denial. In RvR, the Mana Eldritch nearsights opposing casters before collapsing into PBAoE range under tank protection. In PvE pet pulling, it steps into point-blank range once the mob pack reaches 85% health and vaporizes the cluster.

### 4. Mode: RvR
- **PBAoE Bombing**: Coordinate with friendly frontline tanks; activate `Mastery of Concentration` and unleash rapid PBAoE Mana pulses into clustered enemy forces.
- **Nearsight Suppression**: Apply maximum-range Nearsight onto enemy archers (Scouts, Rangers, Hunters) and casters to cripple their effective range.
- **Positioning**: 1350 units back during approach; collapse into PBAoE range only under guard or bubble protection.

### 5. Mode: PvP
- **Nearsight & Kite**: Shut down enemy ranged distance with nearsight, apply snares, and burst with DD and PBAoE.

### 6a. Mode: PvE Standard
- **Multi-Mob Cleave**: Unleash PBAoE pulses once tanks establish threat on multi-mob pulls.

### 6b. Mode: PvE Pet Pulling (The Mana PBAoE Melting Role)
- **Role During Pet Pulling**: The Mana Eldritch is **NOT the puller**. It acts as the **primary PBAoE area finisher**.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire while mobs are in transit to camp.
- **Break-Off Add Protection**:
  - If any mob breaks off from the pet toward the group, cast **Single Stun** (from Light baseline) or baseline root on that loose add, or assist in burning it down.
- **THE 85% MOB HEALTH PBAoE MANA TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Mana Eldritch initiates the **Mana Bomb**:
  - Move into point-blank range of the cluster centered on the pet.
  - Unleash continuous, rapid **PBAoE Mana pulses** to melt the entire pull down in seconds!
- **Strict Stationary Discipline on Pull**:
  - Remain strictly stationed at camp during the pull phase. Do NOT run away or flee backwards to achieve range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Shannon Estuary, Connacht, Bog of Cullen, Silvermine Mountains). Wide open fields allow the Mana Eldritch to execute long-range PBAoE / snare kiting rotations safely.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons (Coruscating Mine, Muire Tomb, Treibh Caillte) feature narrow corridors (<800 unit sightlines), low ceilings, and dense patrol links. Because physical hits cause 100% spell interrupts and Eldritches wear light cloth armor without a persistent tank pet, soloing in dungeons results in immediate interrupts and death. **Only enter dungeons when grouped with a dedicated scale tank and healer**.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Primary grinding target. Target dies in 2 to 3 Mana nukes before closing distance.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Requires 100% Mana and unobstructed retreat path.
  - **Restricted (Orange)**: Engage only if QuickCast is ready and open kiting room is clear of roaming adds.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; spell resists result in instant melee interrupt and death.
- **Pull & Engagement Sequence**:
  - 1. Establish 1500-unit casting distance with clear open terrain behind the bot.
  - 2. Opener: Cast longest-range Mana Direct Damage nuke / Mana Snare.
  - 3. Secondary: Re-apply cold snare if mob closes within 800 units; kite backward while casting instant/fast Mana DD.
  - 4. Finisher: Cast point-blank Mana pulse or QuickCast nuke if target reaches melee range.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest immediately when Mana < 25% or Health < 50% post-combat.
  - **Safety Check**: Scan 1000 units for wandering spraggons or moor bugs before sitting; reposition into cleared clearings.
  - **Resume Threshold**: Stand and resume hunting only when Mana >= 85% and Health >= 90%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Root / Snare -> 180° turn -> Sprint retreat to open safe zone.

### 7. Offensive Priorities
1. PBAoE Mana Pulses: Priority 100 (Unleash into mob cluster anchored on pet at 85% HP)
2. Nearsight Debuff: Priority 90 (Cast on enemy casters/archers in RvR)
3. Area / Single Snare: Priority 80 (Apply to charging melee or loose adds)
4. Baseline Light DD: Priority 60 (Finishing single targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE casting)
3. Single Stun / Root on Attacker: Priority 90
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Bomb)
2. Enemy Caster / Archer Line in RvR: Priority 95 (For Nearsight debuff)
3. Loose Break-off Add: Priority 90 (For Stun / nuke)
4. Primary Called Target: Priority 75

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **PBAoE Range**: 0–350 units from the mob cluster centered on the pet (during burn phase)
- **Pull Phase**: Stationary at camp anchor; never flee backwards for range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE bomb if under attack)
3. Spec PBAoE Mana Spell: Priority 90 (At 85% mob health)
4. Nearsight Debuff: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **High Mana Consumption**: PBAoE drains significant power.
- **PBAoE Throttling**: Stop PBAoE when mana drops below 20% or when only 1 mob remains; switch to baseline DD.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Step into point-blank range of the pet-anchored cluster at 85% HP and unleash PBAoE.
- Nearsight enemy casters and archers in RvR.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast PBAoE while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.
- Never spam PBAoE when only a single low-health monster remains.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Mana Pulses.
- **Condition: Enemy Caster without Nearsight**: Target Caster -> Cast Nearsight.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Stun / Root.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease PBAoE casting immediately, withdraw to backline, cast baseline DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15] (Crucial for uninterruptible PBAoE)
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 25]
7. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 28]
8. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 34]
9. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 2: Void Eldritch
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Void"
  min_point_ratio: 0.50
  spec_type_aliases: ["VoidEld"]
```

### 1. Intended Role
Cold Bolt Artillery, Matter DD Sniper, and Cold Debuffer.

### 2. Intended Role Summary
The Void Eldritch delivers extreme single-target damage through high-velocity Cold Bolts, Matter direct damage nukes, Cold resistance debuffs, and `CastThroughRoot`, acting as Hibernia's premier single-target sniper.

### 3. Combat Philosophy
Long-range bolt assassination and heavy single-target nuking. The Void Eldritch opens from 1800 units with Cold Bolts, strips Cold resistance with spec debuffs, and hammers targets with rapid Matter DDs.

### 4. Mode: RvR
- **Bolt Opening**: Open on high-value enemy casters, healers, or archers with Cold Bolt from 1800 units.
- **Cold Debuff Assist**: Cast Cold resistance debuff on the Main Assist's target, then burst with spec DD.
- **Cast Through Root**: Activate `AtlasOF_CastThroughRoot` to nuke rooted targets without breaking their root state!
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Bolt & Burst**: Open with Cold Bolt -> Cold Debuff -> `Quick Cast` + Matter DD -> Root opponent if they close distance.

### 6a. Mode: PvE Standard
- **Assist DPS**: Assist main tank on designated target with Cold Debuff -> Matter DD rotation.

### 6b. Mode: PvE Pet Pulling (Single-Target Sniper Role)
- **Role During Pet Pulling**: The Void Eldritch is **NOT the puller**. It acts as stationary single-target artillery.
- **Hold Fire on Pull**: Strictly hold all direct damage, bolts, and debuffs while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, immediately cast Matter DD or single root to secure that loose add.
- **THE 85% MOB HEALTH BURN TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Void Eldritch unleashes spec **Cold Debuff + Matter DD nukes** on the designated primary kill target within the cluster!
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Shannon Estuary, Bog of Cullen). Open fields allow full exploitation of Void Bolt and Void Direct Damage nukes from extreme range.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Narrow dungeon corridors obstruct long-range bolt trajectories and prevent maintaining safe distance. Pure cloth armor and high interrupt rates make solo dungeon crawling non-viable. Group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Void Bolt + 1 Void DD nuke eliminates target before it takes 3 steps.
  - **Secondary (Yellow)**: Standard solo target. Open with Void Bolt, apply Void Resistance Debuff, finish with Void DD.
  - **Restricted (Orange)**: Only engage with QuickCast and full mana pool.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1800 units for Bolt, 1500 units for DD) in open terrain.
  - 2. Opener: Cast Void Bolt from maximum range.
  - 3. Mid-Flight Cast: Cast Void Resistance Debuff while bolt travels.
  - 4. Heavy Nuke: Cast Spec Void DD nuke into debuffed target.
  - 5. Finisher: Quick baseline DD before mob enters melee swing range.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Root -> Sprint retreat.

### 7. Offensive Priorities
1. Cold Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Matter Direct Damage: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Cold Bolt: Priority 80 (Open RvR / single target opener)
4. Single-Target Root: Priority 75 (Peel loose adds or charging melee)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Single Root on Attacker: Priority 95 (Root melee pursuer)
3. Mastery of Concentration: Priority 90 (Power through interrupts)
4. Quick Cast: Priority 80 (Emergency instant nuke / root)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 95 (For rapid incineration or root)
3. Enemy Caster / Healer in RvR: Priority 90
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Cold Resistance Debuff: Priority 95
3. Spec Matter DD: Priority 90
4. `AtlasOF_CastThroughRoot`: Priority 85 (When nuking rooted enemies)
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Bolt Conservation**: Do not cast Cold Bolt once targets enter melee range.

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Root loose break-off adds immediately to protect healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Cold Debuff -> Cast Spec Matter DD.
- **Condition: Loose Add threatening healer**: Target Add -> Cast Single Root.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting high-cost spec DD, switch to baseline DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Spell crit chance)
5. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_CastThroughRoot` (Rank 1) - 5 pts [Total: 43] (Active root-nuking ability)
8. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
9. `AtlasOF_Serenity` (Rank 1-2) - 3 pts [Total: 50]

---

## Specialization 3: Light Eldritch
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Light"
  min_point_ratio: 0.50
  spec_type_aliases: ["LightEld"]
```

### 1. Intended Role
AoE Mesmerize Controller, Energy DD Nuker, and Stun Specialist.

### 2. Intended Role Summary
The Light Eldritch specializes in wide-area crowd control through AoE Mesmerize, single-target Stuns, Disease, and Energy direct damage.

### 3. Combat Philosophy
Tactical area control and energy execution. The Light Eldritch locks down incoming forces with AoE Mez, debuffs enemy healing received by 50% with Disease, and executes priority targets with Energy DDs.

### 4. Mode: RvR
- **AoE Mesmerize Opening**: Cast maximum-range AoE Mesmerize on clustered enemy groups.
- **Disease Blanket**: Cast Disease on enemy healers to cut healing received by 50%.
- **Energy DD Burst**: Assist the assist train with Energy DD nukes on called targets.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Mez & Stun Control**: Cycle AoE/single Mez and single Stun to create distance and burn with Energy DD.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Mez or single Mez on roaming adds that aggro during combat.
- **Assist DPS**: Burn down primary target with Energy DD.

### 6b. Mode: PvE Pet Pulling (Crowd Control & Peeler Role)
- **Role During Pet Pulling**: The Light Eldritch is **NOT the puller**. It acts as stationary crowd control and burst support.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire and do NOT cast Mez while mobs are in transit to camp.
- **Break-Off Add Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward the group, cast **Single Stun** or **Single Mez** on that loose add immediately to protect group members.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Light Eldritch casts spec **Energy DD** into the designated primary kill target within the cluster.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Connacht, Silvermine Mountains). Open terrain allows full use of Light single-target and area Mezzes followed by disease and DD nukes.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Tight corridors make crowd-control kiting risky due to tight walls and roaming link patrols. Must group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Target neutralized in mez/snare and shattered with Light DD.
  - **Secondary (Yellow)**: Standard target. Open with Light Mez, debuff, and nuke.
  - **Restricted (Orange)**: Engage only with QuickCast available.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1500 units).
  - 2. Opener: Cast Single-Target Light Mez on target.
  - 3. Debuff: Apply Light debuff / disease while target sleeps.
  - 4. Nuke Rotation: Cast heavy Light DD; finish with fast baseline nuke as mob closes distance.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast AoE Light Mez -> Sprint retreat to safety.

### 7. Offensive Priorities
1. AoE Mesmerize: Priority 100 (Open RvR / multi-add encounters)
2. Single-Target Stun: Priority 95 (Apply to loose adds or enemy peelers)
3. Single / AoE Disease: Priority 90 (Apply to enemy healers)
4. Spec Energy Direct Damage: Priority 80 (Nuke called target at 85% mob HP)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Single Stun on Attacker: Priority 95 (Stun melee pursuer immediately)
3. Mastery of Concentration: Priority 90 (Power through interrupts to cast AoE Mez)
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant Stun / Mez peel)
2. Uncontrolled Enemy Group in RvR: Priority 95 (For AoE Mez)
3. Enemy Primary Healer in RvR: Priority 90 (For Disease)
4. Primary Assist Target: Priority 75 (For Energy DD at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. AoE Mesmerize: Priority 95
3. Single-Target Stun: Priority 90
4. `AtlasOF_MasteryOfConcentration`: Priority 85
5. Spec Energy DD: Priority 80

### 12. Resource Management
- **Mana Floor**: 20% (Reserve power for emergency CC and stuns)

### 13. What the AI SHOULD Do
- Open RvR engagements with maximum-range AoE Mesmerize.
- Peel loose break-off adds immediately during pet pulling with stuns or mez.
- Cast Energy DD on the called target once mobs reach 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never mez targets currently taking damage from the pulsing damage shield (breaks mez).
- Never cast Mez or nukes while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Stun / Mez.
- **Condition: Uncontrolled Multiple Enemies in RvR**: Select Center -> Cast AoE Mez.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Cast Energy DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease offensive DD, reserve power exclusively for emergency single-target CC.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfTheArt` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Cast speed boost)
5. `AtlasOF_WildPower` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 48]
8. `AtlasOF_AugAcuity` (Rank 1-2) - 2 pts [Total: 50]
