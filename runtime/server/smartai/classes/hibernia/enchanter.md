---
schema_version: 1
class_name: "Enchanter"
class_id: 41
realm: "Hibernia"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Enchanter (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Mana`: Summon Mana companion (Underhill Ally), pet pulsing damage shield, pet baseline/spec buffs, pet heal, and devastation Point-Blank Area of Effect (PBAoE) Mana Bomb.
  - `Light`: Baseline and spec Heat direct damage (DD), single-target Stun, Heat resistance debuff, Light baseline DD.
  - `Enchantments`: Summon Underhill Guardian / Sentinel, pet Combat Speed buff, Group Damage Add buff, Charm (monster control), Banish (anti-root/snare cleanse), and Mana transfer.
- **Base Abilities**: Sprint, Quick Cast.
- **Summoned Underhill Companion**: Commands a magical Underhill pet capable of melee attack, tanking, or caster disruption via `/pet attack`, `/pet follow`, `/pet stay`, `/pet passive`, and `/pet return`.

---

## Specialization 1: Mana Enchanter
- **role**: PetPuller
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Mana"
  min_point_ratio: 0.50
  spec_type_aliases: ["ManaEnchanter"]
```

### 1. Intended Role
Dedicated PvE Pet Puller, Pet Tank Anchor, and PBAoE Bomb Specialist.

### 2. Intended Role Summary
The Mana Enchanter is Hibernia's premier pet-pulling and area-annihilation caster. It utilizes its summoned Underhill pet to body-pull monster camps into a secure stationary group, maintains a continuous pulsing damage shield on the pet, and unleashes crushing Point-Blank Area of Effect (PBAoE) pulses to melt the pack once anchored.

### 3. Combat Philosophy
Methodical pull mechanics paired with rapid area devastation. The Mana Enchanter initiates pulls safely through pet proximity body-pulling on passive mode. Once the mobs arrive at camp and their health drops to 85% from the damage shield, the Enchanter steps into point-blank range and incinerates the entire cluster with rapid PBAoE pulses.

### 4. Mode: RvR
- **PBAoE Bombing**: Coordinate with Hibernian tanks; when enemy melee trains or caster lines cluster, pop `Mastery of Concentration` and blast them with rapid Mana PBAoE.
- **Pet Disruption**: Send Underhill pet to disrupt enemy healers or primary crowd-controllers (Healers, Sorcerers).
- **Positioning**: 1350 units back during approach; collapse into PBAoE range only under guard/bubble protection.

### 5. Mode: PvP
- **Pet Harassment**: Command pet to pressure enemy casters and archers continuously.
- **PBAoE Defense**: If collapsed upon by stealthers or melee, activate `Quick Cast` + PBAoE or root to peel.

### 6a. Mode: PvE Standard
- **Multi-Mob Control**: Let pet tank initial contact, then assist tanks with PBAoE or baseline DD.
- **Mana Discipline**: Rest between pulls to regenerate power when below 35%.

### 6b. Mode: PvE Pet Pulling (Primary Specialty)
- **Dedicated Puller Responsibilities**:
  - The Mana Enchanter is the **designated group puller**.
  - **Pet Mode Toggle**: Always set pet to **Passive** (`/pet passive`) before sending it toward the mob encampment. Ensure `Guard` mode is strictly disabled.
  - **Proximity (Body) Pulling**: Command pet to move near the target pack (`/pet go` or proximity approach) without attacking unless necessary.
  - **The Return Command**: The moment the mob pack registers the pet (aggros), immediately hit `/pet return` or `/pet follow` to bring the pet sprinting directly back to camp.
- **Continuous Pulsing Damage Shield**:
  - The Enchanter **must keep its pulsing damage shield running on the pet at all times**. This shield is the primary source of initial threat and damage.
- **Mobs Stay on the Pet (No Melee Taunting)**:
  - The mobs must stay focused entirely on the pet while the damage shield deals continuous damage.
  - Tanks and melee DPS do NOT attempt to taunt or peel the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Enchanter continues managing the main pack.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Healers in the group (Druids/Bard) ARE allowed to heal the pet, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Group healers cast their **best smallest heal** (`HealEfficient`), escalating to a bigger heal only on massive spike damage.
  - Pre-cast Health Regeneration / HoTs from Druids stay running on the pet.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - The entire group remains firmly anchored at camp. Group members do **NOT** run away or kite backwards for casting range.
  - The Enchanter brings the mobs directly into the parked group.
- **The 85% Mob Health PBAoE Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Mana Enchanter steps into point-blank range of the cluster centered on the pet and unleashes continuous **Mana PBAoE pulses** to melt the entire pull down!

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO DUNGEON GRINDER**. The Enchanter is Hibernia's premier pet class. In dungeons (Coruscating Mine, Muire Tomb, Treibh Caillte), the Enchanter uses the summoned Underhill Stalker/Ally as a **mobile door-block and corridor threat anchor**. The bot parks the pet at corridor bottlenecks or room entryways (/pet stay / /pet guard), commands the pet to attack, and stands safely 600 units behind in the hallway. The pet absorbs all melee attacks and holds threat, allowing the Enchanter to cast debuffs, pulsing damage shields, and Mana PBAoE/DD with zero risk of interrupt.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop pet grinding with minimal pet health loss.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Underhill pet holds aggro effortlessly.
  - **Restricted (Orange)**: Fully viable; keep pet buffed with damage shield and assist with Mana DD.
  - **Prohibited (Red / Purple)**: Avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Position Underhill pet at corridor bottleneck or room doorway.
  - 2. Opener: Command pet to /pet attack target or proximity pull mob to pet.
  - 3. Threat Lock: Mob strikes pet, taking continuous damage from Enchanter's pulsing damage shield.
  - 4. Assist: Cast Mana Resistance Debuff; cast Mana DD from 600 units behind pet.
  - 5. Pet executes target with enhanced melee attacks.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind parked pet when Mana < 20% or Health < 50%.
  - **Safety Check**: Keep pet on defensive guard mode to intercept any pathing patrols while resting.
  - **Resume Threshold**: Stand when Mana >= 85% and Pet Health >= 90%.
- **Emergency Escape Protocol**:
  - If add spawns: Command pet to intercept add, cast QuickCast baseline Stun/Root on add, and retreat into cleared corridor.

### 7. Offensive Priorities
1. Pet Pulsing Damage Shield: Priority 100 (Keep running on pet continuously)
2. Pet Return / Pull Command: Priority 95 (Recall pet the instant mobs aggro)
3. Mana PBAoE Bomb: Priority 90 (Unleash at 85% mob health threshold on pet cluster)
4. Baseline Light DD: Priority 40 (Finishing single runners)
5. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE casting)
3. Pet Heal: Priority 80 (Sustain pet between pull cycles)
4. Quick Cast: Priority 75 (Emergency instant cast)

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Bomb)
2. Pulled Mob Pack (via Pet Body Pull): Priority 95
3. Loose Break-off Add: Priority 85 (For tank peel / spot baseline DD)

### 10. Positioning Preferences
- **Anchor**: Group Camp Center
- **PBAoE Range**: 0–350 units from the mob cluster centered on the pet (during burn phase)
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Pulsing Damage Shield: Priority 100
2. `/pet return` / `/pet follow`: Priority 95
3. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE bomb if under melee attack)
4. Spec PBAoE Mana Spell: Priority 90 (At 85% mob health)
5. `AtlasOF_MCL`: Priority 85 (Power recovery when mana < 25%)

### 12. Resource Management
- **High Mana Consumption**: PBAoE drains significant power. Monitor mana pools closely.
- **PBAoE Cutoff**: Cease PBAoE when mana drops below 20% or when only a single mob remains; switch to baseline DD.

### 13. What the AI SHOULD Do
- Set pet to Passive before pulling; recall pet immediately upon mob aggro.
- Maintain continuous pulsing damage shield on the pet.
- Bring the pulled pack directly into the parked group.
- Trigger PBAoE bomb decisively once mobs hit the 85% health threshold.

### 14. What the AI SHOULD NOT Do
- Never leave pet on Aggressive or Defensive during pulling (causes lethal links/adds).
- Never break camp anchor or run away backwards to gain range.
- Never attack with pet on pull when a simple proximity body pull is sufficient.
- Never cast PBAoE while the pet is still in transit pulling mobs to camp.

### 15. Target Selection Matrix
- **Condition: Pull Phase Initiated**: Target Camp -> Send Pet (Passive) -> On Aggro -> Issue `/pet return`.
- **Condition: Mob Pack at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Pulses.
- **Condition: Mob breaks off toward ally**: Call for Tank Peel -> Cast Baseline DD if safe.

### 16. Fallback Behavior
- **Pet Dies Mid-Fight**: Sprint to melee line, activate `Quick Cast` + Re-summon once aggro is peeling onto tanks.
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease PBAoE, withdraw to backline, cast single-target DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15] (Crucial for uninterruptible PBAoE)
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21] (Spell critical chance)
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 35] (Cast speed boost)
10. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 2: Light Enchanter
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Light"
  min_point_ratio: 0.50
  spec_type_aliases: ["LightEnchanter"]
```

### 1. Intended Role
Primary Ranged Nuker, Heat Debuff Specialist, and Single-Target Stunner.

### 2. Intended Role Summary
The Light Enchanter is Hibernia's premier single-target heat artillery. It specializes in high-yield Heat direct damage nukes, long-range single-target Stuns, and Heat resistance debuffs. It does NOT serve as a pet puller; instead, it acts as the primary kill-shot caster for Hibernia's assist train.

### 3. Combat Philosophy
Lethal single-target burst and stun coordination. The Light Enchanter locks down priority targets with long-range stuns, strips their heat resistance with debuffs, and obliterates them with high-speed Heat DDs. In RvR, it executes high-value enemy casters and healers; in PvE, it provides focused single-target DPS.

### 4. Mode: RvR
- **Stun Setup**: Open on high-value enemy casters, healers, or melee peelers with single-target Heat Stun.
- **Heat Debuff Assist**: Cast Heat debuff on the Main Assist's target, then burst with spec Heat DD.
- **Pet Harass**: Send Underhill pet to pressure enemy backline support.
- **Positioning**: 1350–1500 units back behind frontline tanks.

### 5. Mode: PvP
- **Stun & Burn**: Land Heat Stun -> Debuff -> `Quick Cast` + Heat DD -> Finish with follow-up nukes before stun expires.
- **Pet Distraction**: Command pet to intercept enemy archers or stealthers.

### 6a. Mode: PvE Standard
- **Assist Train DPS**: Assist main tank on designated target.
- **Stun Peel**: Stun runaway mobs or adds charging the healer line.

### 6b. Mode: PvE Pet Pulling (Non-Puller Role)
- **Role During Pet Pulling**: The Light Enchanter is **NOT the puller**. It acts as stationary single-target artillery.
- **Hold Fire on Pull**: Strictly hold all direct damage, debuffs, and stuns while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, immediately cast Heat Stun on the loose mob and assist in bursting it down.
- **The 85% Mob Health Burn Trigger**: Once the pet's pulsing damage shield whittles the mob pack down to **85% health**, unleash maximum single-target Heat DD nukes on the designated primary kill target within the cluster.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **HIGH SOLO SURVIVABILITY VIA PET ANCHOR**. Using the Underhill pet as a front-line corridor wall, the Light Enchanter casts heavy Light DD nukes and debuffs safely from behind the pet in narrow dungeon rooms.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid kills; Light nukes finish mob in seconds while pet tanks.
  - **Secondary (Yellow)**: Standard target. Pet absorbs hits while caster nukes.
  - **Restricted (Orange)**: Viable; debuff Light resistance before bursting.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Park Underhill pet at doorway.
  - 2. Opener: Send pet to attack.
  - 3. Debuff: Apply Light Resistance Debuff.
  - 4. Nuke: Cast heavy Light DD nukes from safe distance behind pet.
  - 5. Finisher: Rapid Light DD to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 25%.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - Pet intercepts add -> QuickCast Stun -> Sprint retreat.

### 7. Offensive Priorities
1. Heat Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Heat Direct Damage: Priority 85 (Primary nuke rotation)
3. Single-Target Heat Stun: Priority 80 (Apply to priority targets or break-off adds)
4. Baseline Light DD: Priority 50 (Finishing low-health targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Single-Target Stun on Attacker: Priority 90 (Stun melee pursuer)
3. Mastery of Concentration: Priority 85 (Power through interrupts to secure nuke)
4. Quick Cast: Priority 80 (Emergency instant stun / nuke)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 95 (For Stun lockdown)
3. Enemy Caster / Healer in RvR: Priority 90
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Single-Target Heat Stun: Priority 95
3. Heat Debuff: Priority 90
4. Spec Heat DD: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Efficiency Threshold**: Switch to baseline DD when target HP < 15%.

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Stun loose break-off adds to protect group members.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.
- Never cast single-target stun on a target with stun immunity.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Heat Debuff -> Cast Spec Heat DD.
- **Condition: Loose Add threatening healer**: Target Add -> Cast Single Heat Stun.
- **Condition: Target HP < 15%**: Target Enemy -> Cast Baseline DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting high-cost spec DD, switch to baseline DD or let pet melee.

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

## Specialization 3: Enchantment Enchanter
- **role**: PetMaster
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Enchantments"
  min_point_ratio: 0.50
  spec_type_aliases: ["EnchantmentEnchanter"]
```

### 1. Intended Role
Underhill Pet Master, Group Damage Buffer, and Utility Support Specialist.

### 2. Intended Role Summary
The Enchantment Enchanter is Hibernia's dedicated pet commander and group enhancer. It specializes in summoning high-tier Underhill Guardian and Sentinel pets, buffing group melee damage with Damage Add spells, accelerating combat attack speed, managing charmed monster pets, cleansing roots with Banish, and transferring mana to support casters. It does NOT pull with pets; in PvE pet pulling, it provides vital group damage buffs and pet flanker support!

### 3. Combat Philosophy
Group empowerment and pet micro-management. The Enchantment Enchanter turns its summoned pet into a lethal combatant while enriching the group's physical combat output with high-potency Damage Add buffs. In RvR, it disrupts enemy casters with pet harassment; in PvE, it maximizes group melee efficiency.

### 4. Mode: RvR
- **Group Damage Add**: Maintain spec Damage Add on all Hibernian melee fighters (Blademasters, Heroes, Champions).
- **Pet Harassment**: Command Underhill pet to hunt enemy healers and crowd controllers to cause spell interrupts.
- **Banish Cleanse**: Cast Banish to cleanse enemy roots and snares off friendly frontline tanks.
- **Positioning**: 1200–1400 units back.

### 5. Mode: PvP
- **Pet Dominance**: Buff pet with combat speed and damage add; command pet to stick to opponent while providing baseline DD support.
- **Charm Control**: In open environments, charm wild beasts to fight alongside the summoned pet.

### 6a. Mode: PvE Standard
- **Buff Maintenance**: Keep Damage Add active on group tanks.
- **Pet Support**: Command pet to attack the primary target.

### 6b. Mode: PvE Pet Pulling (Support & Flanker Role)
- **Role During Pet Pulling**: The Enchantment Enchanter is **NOT the designated puller**. It acts as stationary buff and DPS support.
- **Group Damage Buffing**: Maintain spec Damage Add on all melee group members prior to and during pulls.
- **Hold Pet on Pull**: Keep summoned pet on **Passive / Guard** near camp while the pet puller brings the mob pack in. Do NOT send pet out to meet the pack.
- **Break-Off Add Defense**: If any loose mob peels off from the main pack toward the group, command the Underhill pet to engage the loose add immediately to protect the group.
- **The 85% Mob Health Trigger**: Once the mob pack reaches **85% health** at the camp anchor, command the Underhill pet to attack the primary assist target and assist with baseline DD.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT PET MELEE ENHANCEMENT**. Enchantment spec provides maximum pet melee buffs (Pet Haste, Str/Con, Damage Add, AF). In dungeon corridors, the buffed Underhill pet becomes an elite killing machine that tears through single mobs with zero damage taken by the caster.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast, effortless pet grinding.
  - **Secondary (Yellow)**: Standard target. Buffed pet crushes mob alone.
  - **Restricted (Orange)**: Fully viable with fully buffed pet.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Cast full suite of Enchantment pet buffs on Underhill pet.
  - 2. Opener: Send pet to engage mob at doorway.
  - 3. Support: Cast baseline DD or pet damage add buffs.
  - 4. Pet finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Pet intercepts add -> Sprint away to safety.

### 7. Offensive Priorities
1. Group Damage Add Buff: Priority 100 (Keep active on all melee allies)
2. Underhill Pet Attack: Priority 85 (Target primary mob or break-off add)
3. Baseline Light DD: Priority 60 (Cast at 85% mob health threshold)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Banish (Root / Snare Cleanse): Priority 90 (Cleanse incapacitated friendly tanks)
3. Pet Guard on Healer: Priority 85 (Protect group healer from stealthers)
4. Self-Cast Buffs: Priority 80

### 9. Target Priorities
1. Friendly Melee Allies (for Damage Add): Priority 100
2. Loose Break-off Add: Priority 90 (Send pet to intercept)
3. Primary Kill Target (at 85% HP threshold): Priority 80

### 10. Positioning Preferences
- **Anchor**: Midline / Backline
- **Target Range**: 1100–1350 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Group Damage Add Buff: Priority 95
3. Banish Root Cleanse: Priority 90
4. Underhill Pet Commands: Priority 85
5. `AtlasOF_BrilliantAura`: Priority 80 (Active pet buff)

### 12. Resource Management
- **Mana Reserve Floor**: 25% (Reserve power for Damage Add upkeep and Banish cleanses)
- **Mana Transfer**: If mana > 70% and friendly healer < 25%, transfer power to healer.

### 13. What the AI SHOULD Do
- Keep Damage Add active on group fighters at all times.
- Cleanse roots off friendly frontline tanks with Banish.
- Use Underhill pet to intercept break-off adds during pet pulling.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Damage Add drop on frontline melee.
- Never send pet out to attack mobs during transit while the puller is returning to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Ally Damage Add Expired**: Select Melee Ally -> Cast Damage Add.
- **Condition: Ally Rooted / Snared**: Select Ally -> Cast Banish.
- **Condition: Loose Add threatening healer**: Target Add -> Issue `/pet attack`.
- **Condition: Mob Pack HP <= 85%**: Target Primary Kill Target -> Send Pet -> Cast Baseline DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from baseline DD, conserve power exclusively for Damage Add and Banish.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_WildMinion` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_WildMinion` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_WildMinion` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_BrilliantAura` (Rank 1) - 5 pts [Total: 20] (Active pet combat enhancement)
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
