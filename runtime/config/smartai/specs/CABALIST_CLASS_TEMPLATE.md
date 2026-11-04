---
schema_version: 1
class_name: "Cabalist"
class_id: 13
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Cabalist (Albion)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Matter Magic`: Summon Simulacrum pet (Amber, Jade, Ruby, Sapphire, Emerald), pet pulsing damage shield, pet baseline/spec buffs, pet heal, matter damage-over-time (DoT), matter targeted area-of-effect (AoE) DD, and matter single-target DD.
  - `Body Magic`: High-speed Body Lifedrain, Body resistance debuff, single-target Root, single-target Body DD, and pet physical attribute enhancements.
  - `Spirit Magic`: Area-of-Effect Mesmerize (AoE Mez), single-target Mesmerize, single-target and AoE Disease, Spirit direct damage (DD), and nearsight debuffs.
- **Base Abilities**: Sprint, Quick Cast.
- **Summoned Simulacrum Companion**: Commands a magical golem pet capable of physical melee tanking, spellcasting, or disrupting enemy backline support via `/pet attack`, `/pet follow`, `/pet stay`, `/pet passive`, and `/pet return`.

---

## Specialization 1: Matter Cabalist
- **role**: PetPuller
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Matter_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["MatterCab"]
```

### 1. Intended Role
Dedicated PvE Pet Puller, Pet Tank Anchor, and Matter DoT Specialist.

### 2. Intended Role Summary
The Matter Cabalist is Albion's premier pet-pulling and persistent damage specialist. It utilizes its summoned Amber Simulacrum pet to body-pull monster camps safely into a secure stationary group, maintains a continuous pulsing damage shield on the pet, and unleashes stacking Matter DoTs and targeted AoE spells to melt the pack once anchored.

### 3. Combat Philosophy
Methodical pull control and persistent area damage. The Matter Cabalist uses its golem as a high-threat damage sponge. The pulsing damage shield does continuous damage to all attacking monsters. Once the pack is anchored at camp and reaches 85% health, the Cabalist blankets the cluster in Matter DoTs and targeted AoE spells.

### 4. Mode: RvR
- **Simulacrum Disruption**: Send Simulacrum to pressure enemy healers and crowd controllers.
- **Active RAs**: Pop `AtlasOF_Juggernaut` to transform the Simulacrum into a towering combat engine capable of shredding enemy casters.
- **DoT Pressure**: Blanket enemy backline casters in long-duration Matter DoTs to break stealth and cause cast interrupts.
- **Positioning**: 1350–1500 units back behind friendly frontline shield tanks.

### 5. Mode: PvP
- **Pet Harass & DoT**: Command Simulacrum to stick to opponent while applying Matter DoTs and baseline DD.
- **Self-Defense**: Use `Quick Cast` + root or DD if pressured by melee.

### 6a. Mode: PvE Standard
- **Pet Tanking**: Command Simulacrum to engage primary target with pulsing damage shield active.
- **Pet Sustain**: Heal Simulacrum between combat cycles to enter each pull at full health.

### 6b. Mode: PvE Pet Pulling (Primary Specialty)
- **Dedicated Puller Responsibilities**:
  - The Matter Cabalist is the **designated group puller**.
  - **Pet Mode Toggle**: Always set Simulacrum to **Passive** (`/pet passive`) before sending it toward the mob encampment. Ensure `Guard` mode is strictly disabled.
  - **Proximity (Body) Pulling**: Command Simulacrum to move near the target pack (`/pet go` or proximity approach) without attacking unless necessary.
  - **The Return Command**: The moment the mob pack registers the Simulacrum (aggros), immediately hit `/pet return` or `/pet follow` to bring the pet sprinting directly back to camp.
- **Continuous Pulsing Damage Shield**:
  - The Cabalist **must keep its pulsing damage shield running on the Simulacrum at all times**. This shield is the primary source of initial threat and damage.
- **Mobs Stay on the Pet (No Melee Taunting)**:
  - The mobs must stay focused entirely on the pet while the damage shield deals continuous damage.
  - Tanks and melee DPS do NOT attempt to taunt or peel the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Cabalist continues managing the main pack.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Healers in the group (Clerics/Friars) ARE allowed to heal the pet, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Group healers cast their **best smallest heal** (`HealEfficient`), escalating to a bigger heal only on massive spike damage.
  - Pre-cast Health Regeneration / HoTs from Friars stay running on the pet.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - The entire group remains firmly anchored at camp. Group members do **NOT** run away or kite backwards for casting range.
  - The Cabalist brings the mobs directly into the parked group.
- **The 85% Mob Health AoE Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Matter Cabalist unleashes its **Matter DoTs and Targeted AoE spells** directly into the cluster centered on the pet to melt the pull down!

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. The Matter Cabalist is a premier solo dungeon class (Tomb of Mithra, Catacombs, Tepok's Mine). The summoned Amber Golem acts as an immovable corridor door-block and threat anchor. The Cabalist positions the pet at narrow doorways or corridor intersections (/pet stay / /pet guard), sends the pet to pull or engage, and stands safely 600 units behind in the hallway. The pet absorbs all melee attacks, preventing cast interrupts while the Cabalist applies Matter DoTs and debuffs in total safety.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Continuous non-stop pet grinding with near-zero pet health loss.
  - **Secondary (Yellow)**: Standard baseline solo target. Amber Golem easily holds aggro throughout combat.
  - **Restricted (Orange)**: Highly viable; pre-buff pet with AF/Str buffs, apply Matter DoTs, and spot-heal pet if needed.
  - **Prohibited (Red / Purple)**: Avoid solo unless pet is buffed to cap and escape path is cleared.
- **Pull & Engagement Sequence**:
  - 1. Park Amber Golem at corridor chokepoint or doorway.
  - 2. Opener: Command pet to /pet attack target or proximity body-pull mob back to the parked pet.
  - 3. Threat Anchor: Wait until mob strikes pet and pet lands initial taunt hit.
  - 4. DoT & Debuff: Apply Matter Resistance Debuff followed by primary Matter DoT.
  - 5. Sustain: Cast pulsing damage shield on pet; cast minor pet heal if pet drops below 60% HP.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind parked pet when Mana < 20% or Health < 50%.
  - **Safety Check**: Keep pet on Defensive guard mode while sitting to intercept any roaming patrols.
  - **Resume Threshold**: Stand when Mana >= 80% and Pet Health >= 90%.
- **Emergency Escape Protocol**:
  - If add spawns: Command Amber Golem to taunt add, cast QuickCast baseline Root on add, and retreat into cleared corridor.

### 7. Offensive Priorities
1. Pet Pulsing Damage Shield: Priority 100 (Keep running on pet continuously)
2. Pet Return / Pull Command: Priority 95 (Recall pet the instant mobs aggro)
3. Matter DoT / Targeted AoE: Priority 90 (Unleash at 85% mob health threshold on pet cluster)
4. Baseline Matter DD: Priority 40 (Finishing single runners)
5. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible casting)
3. Pet Heal: Priority 80 (Sustain Simulacrum between pull cycles)
4. Quick Cast: Priority 75 (Emergency instant cast)

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For Targeted AoE / DoTs)
2. Pulled Mob Pack (via Pet Body Pull): Priority 95
3. Loose Break-off Add: Priority 85 (For tank peel / spot baseline DD)

### 10. Positioning Preferences
- **Anchor**: Group Camp Center / Backline
- **Target Range**: 1200–1400 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Pulsing Damage Shield: Priority 100
2. `/pet return` / `/pet follow`: Priority 95
3. `AtlasOF_Purge`: Priority 90
4. Matter DoTs / Targeted AoE: Priority 85 (At 85% mob health)
5. `AtlasOF_MCL`: Priority 80 (Power recovery when mana < 25%)

### 12. Resource Management
- **Mana Floor**: 25% (Reserve power for re-summoning pet and refreshing damage shield)
- **DoT Efficiency**: Avoid re-casting DoTs on targets that already have full-duration DoTs ticking.

### 13. What the AI SHOULD Do
- Set pet to Passive before pulling; recall pet immediately upon mob aggro.
- Maintain continuous pulsing damage shield on the pet.
- Bring the pulled pack directly into the parked group.
- Blanket the cluster with Matter DoTs and targeted AoE once mobs hit 85% health.

### 14. What the AI SHOULD NOT Do
- Never leave pet on Aggressive or Defensive during pulling (causes lethal links/adds).
- Never break camp anchor or run away backwards to gain range.
- Never attack with pet on pull when a simple proximity body pull is sufficient.
- Never cast DoTs or targeted AoE while the pet is still in transit pulling mobs to camp.

### 15. Target Selection Matrix
- **Condition: Pull Phase Initiated**: Target Camp -> Send Pet (Passive) -> On Aggro -> Issue `/pet return`.
- **Condition: Mob Pack at Pet & HP <= 85%**: Target Clustered Mob -> Cast Matter DoT -> Cast Targeted AoE.
- **Condition: Mob breaks off toward ally**: Select Break-off Add -> Call for Tank Peel.

### 16. Fallback Behavior
- **Pet Dies Mid-Fight**: Sprint to melee line, activate `Quick Cast` + Re-summon once aggro stabilizes onto tanks.
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from AoE DoTs, conserve power for damage shield upkeep.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_WildMinion` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_WildMinion` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_WildMinion` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_Juggernaut` (Rank 1) - 5 pts [Total: 20] (Active pet mega-buff)
6. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 25]
7. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 35]
8. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 36]
9. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 39]
10. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 40]
11. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 43]
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 44]
13. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 47]
14. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 48]
15. `AtlasOF_AugCon` (Rank 1) - 1 pt [Total: 49]
16. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Body Cabalist
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Body_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["BodyCab"]
```

### 1. Intended Role
Primary Single-Target Burst Nuker, Body Debuffer, and Lifedrain Duelist.

### 2. Intended Role Summary
The Body Cabalist is Albion's premier high-burst drain caster. It specializes in rapid-cast Body Lifedrain, Body resistance debuffs, single-target roots, and pet combat enhancements. It does NOT serve as a pet puller; instead, it acts as the primary kill-shot caster for Albion's assist train and a formidable 1v1 duelist.

### 3. Combat Philosophy
Lethal burst execution and self-sustain. The Body Cabalist strips enemy body resistances and hammers targets with rapid, high-yield Body Lifedrains that restore health while inflicting massive damage. In RvR, it eliminates high-value targets; in PvE, it delivers focused single-target burst DPS.

### 4. Mode: RvR
- **Target Assassination**: Coordinate with Albion's Main Assist (MA); apply Body resistance debuff and burst called targets with rapid Lifedrain.
- **Root Lockdown**: Cast single-target Body root on enemy melee peelers charging the caster line.
- **Pet Harassment**: Command Simulacrum to attack enemy healers to push back cast times.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Duelist Dominance**: Strip body resistance, cast single root to create distance, and cycle Lifedrain to sustain full health while burning the opponent.
- **Quick Cast Nuke**: Deploy `Quick Cast` + Lifedrain to secure finishes.

### 6a. Mode: PvE Standard
- **Assist Train DPS**: Assist main tank on primary kill target with Body Debuff -> Lifedrain rotation.
- **Root Peel**: Root un-taunted adds charging backline support.

### 6b. Mode: PvE Pet Pulling (Non-Puller Role)
- **Role During Pet Pulling**: The Body Cabalist is **NOT the puller**. It acts as stationary single-target artillery.
- **Hold Fire on Pull**: Strictly hold all direct damage, debuffs, and lifedrains while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, immediately cast single Body root or burst it down with Lifedrain.
- **The 85% Mob Health Burn Trigger**: Once the pet's pulsing damage shield whittles the mob pack down to **85% health**, unleash maximum single-target Body Debuff + Lifedrain on the designated primary kill target within the cluster.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Using the Jade/Amber Golem as a front-line corridor wall, the Body Cabalist casts Body Lifetaps and Body Debuffs from complete safety behind the pet in narrow dungeon rooms. Zero cast interrupts from incoming mobs.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fastest leveling speed. Mob melts under pet attacks and Body Lifetaps.
  - **Secondary (Yellow)**: Standard target. Lifetaps maintain Cabalist health while pet absorbs incoming damage.
  - **Restricted (Orange)**: Viable; debuff Body resistance before lifetapping.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position Golem in corridor chokepoint.
  - 2. Opener: Send Golem to attack; mob anchors threat on pet.
  - 3. Debuff: Apply Body Resistance Debuff on target.
  - 4. Lifetap Burst: Cast Body Lifetaps safely from 600 units behind pet.
  - 5. Pet finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%. Lifetaps ensure Cabalist HP remains high.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Command pet to hold add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Body Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Body Lifedrain: Priority 85 (Primary nuke rotation on MA target)
3. Single-Target Body Root: Priority 80 (Apply to charging melee or break-offs)
4. Baseline Matter DD: Priority 50 (Finishing low-health targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Single-Target Root on Attacker: Priority 90 (Root melee pursuer)
3. Lifedrain on Target: Priority 85 (Recover lost hit points)
4. Quick Cast: Priority 80 (Emergency instant root / drain)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 95 (For Root lockdown)
3. Enemy Caster / Healer in RvR: Priority 90
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Body Resistance Debuff: Priority 95
3. Spec Body Lifedrain: Priority 90
4. Single-Target Body Root: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Efficiency Threshold**: Switch to baseline DD when target HP < 15%.

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Root loose break-off adds to protect group members.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.
- Never cast single-target root on a target currently taking melee damage.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Body Debuff -> Cast Spec Lifedrain.
- **Condition: Loose Add threatening healer**: Target Add -> Cast Single Body Root.
- **Condition: Self HP < 50% & In Combat**: Target Enemy -> Cast Lifedrain.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting high-cost spec Lifedrain, switch to baseline DD or let pet melee.

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
9. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AugDex` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 3: Spirit Cabalist
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Spirit_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["SpiritCab"]
```

### 1. Intended Role
Primary Crowd Controller, AoE Mesmerize Specialist, and Disease Caster.

### 2. Intended Role Summary
The Spirit Cabalist is Albion's premier crowd-control and debuffing caster alongside the Sorcerer. It specializes in Area-of-Effect Mesmerize (AoE Mez), single-target Mesmerize, single/AoE Disease, Spirit direct damage nukes, and nearsight debuffs. It does NOT serve as a pet puller; in PvE pet pulling, it locks down roaming adds and bursts the pack once anchored at 85% health!

### 3. Combat Philosophy
Wide-area neutralization and tactical control. The Spirit Cabalist neutralizes incoming enemy waves with AoE Mesmerize, blankets enemy healers in Disease (halving their healing effectiveness), and applies nearsight debuffs to enemy archers and casters. In RvR, it secures group tempo; in PvE, it provides flawless crowd control.

### 4. Mode: RvR
- **AoE Mesmerize Opening**: Open engagements with maximum-range AoE Mesmerize on clustered enemy groups.
- **Disease Blanket**: Cast Disease onto enemy healers and tanks to cut incoming heals by 50% and reveal stealthers.
- **Nearsight Debuffing**: Apply nearsight to enemy ranged snipers (Rangers, Scouts, Hunters) and casters to shut down their effective casting range.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Mez & Reset**: Land single-target Mez to force enemy `Purge` or reset combat distance.
- **Disease & Nuke**: Apply Disease -> Cast Spirit DD while keeping pet attacking the opponent.

### 6a. Mode: PvE Standard
- **Add Lockdown**: Immediately cast AoE Mez or single Mez on any extra monster groups that aggro onto the party.
- **Assist DPS**: Assist main tank on called target with Spirit DD.

### 6b. Mode: PvE Pet Pulling (Crowd Control & Finisher Role)
- **Role During Pet Pulling**: The Spirit Cabalist is **NOT the puller**. It acts as stationary crowd control and burst support.
- **Mobs Stay on the Pet**: Let the pet absorb the pull and let the pulsing damage shield establish baseline threat. Do NOT cast Mez or spells on mobs in transit.
- **Break-Off Add Protection**: If any loose mob peels off from the pet toward the group, cast single-target Root or Mez on that loose mob to protect the group.
- **The 85% Mob Health Trigger**: Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Spirit Cabalist is authorized to cast **Disease and Spirit DD nukes** into the cluster centered on the pet.
- **Strict Stationary Discipline on Pull**: Remain strictly stationed at camp during the pull phase. Do NOT run away or flee backwards to achieve range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO SURVIVABILITY**. Spirit Cabalists use the Ruby Golem (caster pet) or Amber Golem (tank pet) alongside AoE Spirit debuffs and Spirit DD nukes. In dungeons, parking the pet in the doorway allows safe corridor bombardment.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Golem and Spirit nukes kill targets in seconds.
  - **Secondary (Yellow)**: Standard target. Spirit debuff followed by heavy Spirit DD.
  - **Restricted (Orange)**: Viable with full pet buffs and debuffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Set Golem at bottleneck entry.
  - 2. Opener: Send Golem to engage target.
  - 3. Debuff & Nuke: Cast Spirit Resistance Debuff followed by Spec Spirit DD.
  - 4. Finisher: Rapid Spirit DD to execute target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 25%.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Root on pursuer -> Pet distracts -> Sprint retreat.

### 7. Offensive Priorities
1. AoE Mesmerize: Priority 100 (Open RvR / multi-add encounters)
2. Single / AoE Disease: Priority 90 (Apply to enemy healers/tanks)
3. Spirit Direct Damage: Priority 85 (Primary nuke rotation at 85% mob health)
4. Nearsight Debuff: Priority 75 (Apply to enemy casters/archers)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible CC casting)
3. Emergency Single Mez on Attacker: Priority 90
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Uncontrolled Enemy Group in RvR: Priority 100 (For AoE Mez)
2. Enemy Primary Healer / Support: Priority 95 (For Disease)
3. Loose Break-off Add: Priority 90 (For Single Mez / Root)
4. Primary Called Target: Priority 80 (For Spirit DD)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. AoE Mesmerize: Priority 90
4. Single / AoE Disease: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Mez Discipline**: Never re-cast AoE Mez on targets already mesmerized or taking damage.

### 13. What the AI SHOULD Do
- Open RvR engagements with maximum-range AoE Mesmerize.
- Cast Disease on enemy healers to cut healing received by 50%.
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast AoE Mez on targets already taking damage from the pulsing damage shield (breaks Mez).
- Never cast spells on mobs while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Uncontrolled Multiple Enemies in RvR**: Select Center of Cluster -> Cast AoE Mez.
- **Condition: Enemy Healer without Disease**: Select Healer -> Cast Disease.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Mez / Root.
- **Condition: Primary Target HP <= 85%**: Select Primary Target -> Cast Spirit DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting offensive DD, reserve power exclusively for emergency single Mez and roots.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15] (Crucial for uninterruptible CC)
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
