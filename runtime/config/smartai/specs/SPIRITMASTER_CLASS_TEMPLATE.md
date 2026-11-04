---
schema_version: 1
class_name: "Spiritmaster"
class_id: 27
realm: "Midgard"
primary_stats: ["Piety", "Dexterity"]
default_personality: "Tactical"
---

# Class: Spiritmaster (Midgard)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Summoning`: Summon undead spirit pets (Spirit Warrior, Spirit Archer, Spirit Mage), pet pulsing damage shield, pet baseline/spec buffs, pet heal/mend, pet offensive command suite (`/pet attack`, `/pet follow`, `/pet stay`, `/pet passive`, `/pet return`).
  - `Darkness`: Baseline and spec Cold direct damage (DD), long-range snare (Cold slow), Lifedrain, Cold resistance debuff, Darkness DoT.
  - `Suppression`: Area-of-Effect Mesmerize (AoE Mez), single-target Mesmerize, single-target and AoE Root, Strength/Constitution stat debuffs, and Point-Blank Area of Effect (PBAoE) Cold Bomb.
- **Base Abilities**: Sprint, Quick Cast.
- **Summoned Undead Spirit**: Summons an obedient spirit companion that acts as the primary tank during pet pulling or as an offensive disruptor in PvP/RvR.

---

## Specialization 1: Summoning Spiritmaster
- **role**: PetPuller
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Summoning"
  min_point_ratio: 0.50
  spec_type_aliases: ["SummSpirit"]
```

### 1. Intended Role
Dedicated PvE Pet Puller, Pet Tank Anchor, and Undead Commander.

### 2. Intended Role Summary
The Summoning Spiritmaster is Midgard's premier pet-pulling specialist. It utilizes an expendable undead spirit pet to body-pull monster camps back to a secure stationary group, maintains a continuous pulsing damage shield on the pet, and manages pet passive/return states to ensure the group never takes unmanageable aggro.

### 3. Combat Philosophy
Methodical control and safe pacing. The Summoning Spiritmaster initiates combat strictly through the summoned pet, using proximity body-pulling. The pet serves as the initial damage sponge while the pulsing damage shield burns down the pack. Once threat is established and mob health reaches 85%, group AoE casters melt the cluster down.

### 4. Mode: RvR
- **Pet Disruption**: Send spirit pet to interrupt high-priority enemy healers or crowd-controllers (Bards, Sorcerers).
- **Pet Buffing**: Keep spec damage shields, haste, and stat buffs active on the pet to maximize melee harassment.
- **Active RAs**: Trigger `AtlasOF_WhipOfEncouragement` or `AtlasOF_ExcitedFrenzy` to turn the spirit pet into a lethal assassin against light-armor casters.
- **Positioning**: 1350–1500 units behind frontline; stay anchored near friendly peelers.

### 5. Mode: PvP
- **Pet Pressure**: Command pet to attack enemy archers or casters to push back cast bars continuously.
- **Self-Defense**: Use `Quick Cast` + baseline Darkness DD or snare if pressured by enemy melee.
- **Tether Management**: Keep pet within 2000 units to avoid despawn.

### 6a. Mode: PvE Standard
- **Pet Tanking**: Command pet to engage the primary target with taunts and damage shield active.
- **Pacing**: Rest to regenerate mana between pulls when power drops below 40%.
- **Pet Sustain**: Cast pet heals between combat cycles to keep the spirit at full health.

### 6b. Mode: PvE Pet Pulling (Primary Specialty)
- **Dedicated Puller Responsibilities**:
  - The Summoning Spiritmaster is the **designated group puller**.
  - **Pet Mode Toggle**: Always set pet to **Passive** (`/pet passive`) before sending it toward the mob encampment. Ensure `Guard` mode is strictly disabled.
  - **Proximity (Body) Pulling**: Command pet to move near the target pack (`/pet go` or proximity approach) without attacking unless necessary.
  - **The Return Command**: The moment the mob pack registers the pet (aggros), immediately hit `/pet return` or `/pet follow` to bring the pet sprinting directly back to camp.
- **Continuous Pulsing Damage Shield**:
  - The Spiritmaster **must keep its pulsing damage shield running on the pet at all times**. This shield is the primary source of initial threat and damage.
- **Mobs Stay on the Pet (No Melee Taunting)**:
  - The mobs must stay focused entirely on the pet while the damage shield deals continuous damage.
  - Tanks and melee DPS do NOT attempt to taunt or peel the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Spiritmaster can spot-snare or direct the pet if safe.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Healers in the group ARE allowed to heal the pet, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Group healers cast their **best smallest heal** (`HealEfficient`), escalating to a bigger heal only on massive spike damage.
  - Pre-cast Health Regeneration / HoTs from Shaman stay running on the pet.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - The entire group remains firmly anchored at camp. Group members do **NOT** run away or kite backwards for casting range.
  - The Spiritmaster brings the mobs directly into the parked group.
- **The 85% Mob Health AoE Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, group AoE casters (Cave Shamans, Runemasters) and the Spiritmaster are authorized to unleash AoE spells to melt the cluster.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SOLO DUNGEON GRINDER**. The Summoning Spiritmaster is Midgard's premier pet class. In dungeons (Spindelhalla, Cursed Tomb, Varv), the Spiritmaster uses the summoned Spirit Warrior as a **mobile door-block and corridor threat anchor**. The bot parks the pet at corridor bottlenecks or room entryways (/pet stay / /pet guard), commands the pet to attack, and stands safely 600 units behind in the hallway. The pet absorbs all melee attacks and holds threat, allowing the Spiritmaster to cast debuffs, damage shields, and spirit nukes with zero risk of interrupt.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop pet grinding with minimal pet health loss.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Spirit pet holds aggro effortlessly.
  - **Restricted (Orange)**: Fully viable; keep pet buffed with AF/Str buffs and assist with spirit nukes.
  - **Prohibited (Red / Purple)**: Avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Position Spirit Warrior at corridor bottleneck or room doorway.
  - 2. Opener: Command pet to /pet attack target or proximity pull mob to pet.
  - 3. Threat Lock: Mob strikes pet, taking damage from Spiritmaster's pulsing damage shield.
  - 4. Debuff & Assist: Apply Spirit Resistance Debuff; cast Summoning DD from 600 units behind pet.
  - 5. Pet executes target with enhanced melee styles.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind parked pet when Mana < 20% or Health < 50%.
  - **Safety Check**: Keep pet on defensive guard mode to intercept any pathing patrols while resting.
  - **Resume Threshold**: Stand when Mana >= 85% and Pet Health >= 90%.
- **Emergency Escape Protocol**:
  - If add spawns: Command pet to intercept add, cast QuickCast baseline Root on add, and retreat into cleared corridor.

### 7. Offensive Priorities
1. Pet Pulsing Damage Shield: Priority 100 (Keep running on pet continuously)
2. Pet Return / Pull Command: Priority 95 (Recall pet the instant mobs aggro)
3. Darkness DD / Snare: Priority 40 (Cast only at 85% mob health threshold or on break-off adds)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Pet Heal / Transfer: Priority 85 (Sustain pet when taking heavy damage)
3. Self-Cast Base Buffs: Priority 80 (Keep active at all times)
4. Quick Cast: Priority 75 (When interrupted by loose add)

### 9. Target Priorities
1. Pulled Mob Pack (via Pet Body Pull): Priority 100
2. Loose Break-Off Add threatening group: Priority 90
3. Primary Kill Target (at 85% HP threshold): Priority 75

### 10. Positioning Preferences
- **Anchor**: Group Camp Center / Backline
- **Target Range**: 1200–1400 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Pulsing Damage Shield: Priority 100
2. `/pet return` / `/pet follow`: Priority 95
3. `AtlasOF_Purge`: Priority 90
4. `AtlasOF_MCL`: Priority 85 (Power recovery when mana < 25%)
5. Pet Heal: Priority 80

### 12. Resource Management
- **Mana Floor**: 30% (Reserve power for re-summoning pet and refreshing damage shield)
- **Pet Re-Summon**: If pet dies during a pull, do NOT immediately stand still and cast a long re-summon if you have aggro; run to tanks first.

### 13. What the AI SHOULD Do
- Set pet to Passive before pulling; recall pet immediately upon mob aggro.
- Maintain continuous pulsing damage shield on the pet.
- Bring the pulled pack directly into the parked group.
- Protect group members by allowing tanks to engage break-off adds.

### 14. What the AI SHOULD NOT Do
- Never leave pet on Aggressive or Defensive during pulling (causes lethal links/adds).
- Never break camp anchor or run away backwards to gain range.
- Never attack with pet on pull when a simple proximity body pull is sufficient.
- Never let the pulsing damage shield expire during pet tanking.

### 15. Target Selection Matrix
- **Condition: Pull Phase Initiated**: Target Camp -> Send Pet (Passive) -> On Aggro -> Issue `/pet return`.
- **Condition: Mob Pack at Pet & HP <= 85%**: Target Clustered Mob -> Cast AoE / DD.
- **Condition: Mob breaks off toward ally**: Select Break-off Add -> Cast Snare / Call for Tank Peel.

### 16. Fallback Behavior
- **Pet Dies Mid-Fight**: Target nearest tank, sprint to melee line, activate `Quick Cast` + Re-summon once aggro is peeling.
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from offensive nuking, conserve power for damage shield upkeep.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_WildMinion` (Rank 1) - 1 pt [Total: 6] (Pet critical hit chance)
3. `AtlasOF_WildMinion` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_WildMinion` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
6. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
7. `AtlasOF_WhipOfEncouragement` (Rank 1) - 5 pts [Total: 35] (Active pet combat buff)
8. `AtlasOF_ExcitedFrenzy` (Rank 1) - 5 pts [Total: 40] (Active pet attack speed surge)
9. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 41]
10. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 44]
11. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 45]
12. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 48]
13. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
14. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Darkness Spiritmaster
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Darkness"
  min_point_ratio: 0.50
  spec_type_aliases: ["DarkSpirit"]
```

### 1. Intended Role
Primary Ranged Nuker, Cold Snare Artillery, and Lifedrain Specialist.

### 2. Intended Role Summary
The Darkness Spiritmaster is a high-damage ranged caster specializing in maximum Cold direct damage nukes, long-duration snares, lifedrain recovery, and Cold magic debuffs. It does NOT serve as a pet puller; instead, it acts as Midgard's heavy ranged artillery, sniping targets called by the main assist.

### 3. Combat Philosophy
Lethal ranged burst and target suppression. The Darkness Spiritmaster uses long-range snares to disable enemy mobility, stacks Cold debuffs, and hammers priority targets with high-yield Cold DDs. In RvR, it functions as a primary kill-shot caster; in PvE, it delivers decisive single-target damage.

### 4. Mode: RvR
- **Target Assassination**: Assist Midgard's Main Assist (MA) by focusing high-yield Cold DD nukes on called targets.
- **Snare Control**: Apply long-range Cold Snare to enemy melee peelers and tanks charging the caster line.
- **Debuff Coordination**: Cast Cold resistance debuff prior to burst cycles on high-health targets.
- **Positioning**: 1350–1500 units back. Maintain clear lines of fire while staying behind friendly shield tanks.

### 5. Mode: PvP
- **Kite & Drain**: Apply Cold snare, kite backwards, and use Lifedrain to recover health while sustaining damage.
- **Quick Cast Nuke**: Deploy `Quick Cast` + Cold DD to secure lethal finishes against retreating opponents.

### 6a. Mode: PvE Standard
- **Assist Train DPS**: Assist main tank on primary kill target.
- **Spell Rotation**: Snare (if target runs) -> Cold Debuff -> Cold DD nukes until target dies.
- **Mana Conservation**: Switch to baseline DD when target drops below 15% health.

### 6b. Mode: PvE Pet Pulling (Non-Puller Role)
- **Role During Pet Pulling**: The Darkness Spiritmaster is **NOT the puller**. It acts as stationary ranged artillery.
- **Hold Fire on Pull**: Strictly hold all direct damage, snares, and lifedrains while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, the Darkness Spiritmaster can immediately assist in bursting down that loose add.
- **The 85% Mob Health AoE / Burn Trigger**: Once the pet's pulsing damage shield whittles the mob pack down to **85% health**, unleash maximum single-target Cold DD nukes on the designated primary kill target within the cluster.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **HIGH SOLO SURVIVABILITY VIA PET ANCHOR**. Using the Spirit pet as a front-line corridor wall, the Darkness Spiritmaster casts heavy Darkness DD nukes and debuffs safely from behind the pet in narrow dungeon rooms.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid kills; Darkness nukes finish mob in seconds while pet tanks.
  - **Secondary (Yellow)**: Standard target. Pet absorbs hits while caster nukes.
  - **Restricted (Orange)**: Viable; debuff Darkness resistance before bursting.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Park pet at doorway.
  - 2. Opener: Send pet to attack.
  - 3. Debuff: Apply Darkness Resistance Debuff.
  - 4. Nuke: Cast heavy Darkness DD nukes from safe distance behind pet.
  - 5. Finisher: Rapid Darkness DD to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 25%.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - Pet intercepts add -> QuickCast Root -> Sprint retreat.

### 7. Offensive Priorities
1. Cold Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Cold Direct Damage: Priority 85 (Primary nuke rotation)
3. Cold Snare: Priority 80 (Apply to charging melee or runners)
4. Lifedrain: Priority 70 (When self HP < 60% during combat)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 90 (Power through interrupts to cast lifedrain or snare)
3. Lifedrain on Target: Priority 80 (Recover lost hit points)
4. Quick Cast: Priority 75 (Emergency instant nuke / snare)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 90
3. Enemy Caster / Healer in RvR: Priority 85
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 90
3. `AtlasOF_MCL`: Priority 85 (When mana < 20%)
4. Spec Cold DD: Priority 80
5. Cold Snare: Priority 75

### 12. Resource Management
- **Mana Reserve Floor**: 20%
- **Efficiency Threshold**: Cast baseline DD instead of high-cost spec DD on targets below 15% HP.

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Apply Cold snare to enemy frontline tanks charging the group.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.
- Never use lifedrain when at full health and mana-starved.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Cold Debuff -> Cast Spec DD.
- **Condition: Self HP < 50% & In Combat**: Target Enemy -> Cast Lifedrain.
- **Condition: Enemy Melee approaching < 600 range**: Target Enemy -> Cast Cold Snare.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting high-cost spec DD, switch to baseline DD or let pet melee.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21] (Spell critical chance)
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31] (Direct damage boost)
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 35] (Cast speed boost)
10. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AugDex` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 3: Suppression Spiritmaster
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Suppression"
  min_point_ratio: 0.50
  spec_type_aliases: ["SuppSpirit"]
```

### 1. Intended Role
Primary Crowd Controller, Point-Blank AoE (PBAoE) Bomb Specialist, and Debuffer.

### 2. Intended Role Summary
The Suppression Spiritmaster is Midgard's premier area control and PBAoE devastation caster. It specializes in wide-area crowd control (AoE Mesmerize, AoE Root, single-target Mez), stat debuffing, and casting devastating Point-Blank Area of Effect (PBAoE) Cold spells. It does NOT pull with pets; in PvE pet pulling, it acts as the primary PBAoE bomb that obliterates the pack once anchored on the pet!

### 3. Combat Philosophy
Decisive area lockdown and burst annihilation. In RvR, the Suppression Spiritmaster neutralizes enemy zergs with AoE Mesmerize and roots. In PvE group pulling, it holds fire until the mob cluster is anchored around the pet, then steps in and obliterates the pack with rapid PBAoE pulses.

### 4. Mode: RvR
- **AoE Crowd Control**: Open engagements with maximum-range AoE Mesmerize on clustered enemy groups.
- **Root Lockdown**: Apply AoE Root to incoming enemy melee trains to peel them off friendly casters.
- **PBAoE Bombing**: Coordinate with friendly tanks; when enemy groups collapse, pop `Mastery of Concentration` and unleash rapid PBAoE pulses.
- **Positioning**: 1350 units back for initial CC; collapse to PBAoE range only under protected cover.

### 5. Mode: PvP
- **Mez & Reset**: Land single-target or AoE Mez to force enemy `Purge` or reset combat pacing.
- **Root & Kite**: Keep enemy fighters rooted while casting DD or setting up PBAoE.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Mez or single Mez on any extra monster groups that aggro onto the party.
- **PBAoE Execution**: In multi-mob encounters, cast PBAoE pulses once tanks have secured aggro.

### 6b. Mode: PvE Pet Pulling (The PBAoE Melting Role)
- **Role During Pet Pulling**: The Suppression Spiritmaster is **NOT the puller**. It is the **primary AoE / PBAoE finisher**.
- **Mobs Stay on the Pet**: Let the pet absorb the pull and let the pet's pulsing damage shield establish baseline threat. Do NOT cast spells or CC while mobs are running back to camp.
- **Break-Off Add Protection**: If any loose mob peels off from the pet toward the group, cast single-target Root or Mez on that loose mob to protect the group.
- **THE 85% MOB HEALTH PBAoE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Suppression Spiritmaster initiates the **PBAoE Bomb**:
  - Move into point-blank range of the cluster anchored on the pet.
  - Unleash continuous, rapid **PBAoE Cold pulses** to melt the entire pack down in seconds!
- **Strict Stationary Discipline on Pull**: Remain strictly stationed at camp during the pull phase. Do NOT run away or flee backwards to achieve range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT CONTROL & PBAoE SAFETY**. With the Spirit pet anchoring corridor aggro, the Suppression Spiritmaster can safely utilize cold debuffs, lifetaps, and PBAoE pulses without fear of melee interrupt.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast multi-mob or single-mob clearing around pet.
  - **Secondary (Yellow)**: Standard target. Pet tanks while Suppression spells drain mob.
  - **Restricted (Orange)**: Viable with full pet buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Park pet at doorway.
  - 2. Opener: Send pet to engage mob.
  - 3. Debuff: Apply Suppression debuff.
  - 4. Burst: Cast Suppression DD / PBAoE pulse onto cluster at pet.
  - 5. Pet finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast AoE Snare -> Pet distracts -> Sprint away.

### 7. Offensive Priorities
1. PBAoE Cold Bomb: Priority 100 (Unleash into mob cluster anchored on pet at 85% HP)
2. AoE Mesmerize: Priority 95 (Open RvR / multi-add encounters)
3. AoE / Single Root: Priority 90 (Lock down charging adds or break-offs)
4. Suppression DD: Priority 60 (Single target finishing nuke)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE / CC casting)
3. Emergency Root on Melee Attacker: Priority 90
4. Self-Cast Buffs: Priority 75

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Bomb)
2. Uncontrolled Enemy Group in RvR: Priority 95 (For AoE Mez)
3. Loose Break-off Add: Priority 90 (For Root / Mez)
4. Called Primary Target: Priority 75

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **PBAoE Range**: 0–350 units from the mob cluster centered on the pet (during burn phase)
- **Pull Phase**: Stationary at camp anchor; never flee backwards for range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE bomb if under melee attack)
3. Spec PBAoE Cold Spell: Priority 90 (At 85% mob health)
4. AoE Mesmerize: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **High Mana Consumption**: PBAoE consumes massive power. Monitor mana closely.
- **PBAoE Throttling**: Stop PBAoE when mana drops below 20% or when only 1 mob remains; switch to single-target DD.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Step into point-blank range of the pet-anchored cluster at 85% HP and unleash PBAoE.
- Mez roaming adds in PvE and open RvR encounters with AoE Mez.
- Protect group members by rooting loose break-off adds.

### 14. What the AI SHOULD NOT Do
- Never cast PBAoE or Mez while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.
- Never cast AoE Mez on targets already taking damage from the pulsing damage shield (breaks Mez).
- Never spam PBAoE when only a single low-health monster remains.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Pulses.
- **Condition: Uncontrolled Multiple Enemies in RvR**: Select Center of Cluster -> Cast AoE Mez.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Root / Mez.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease PBAoE casting immediately, withdraw to backline, cast single-target DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15] (Crucial for uninterruptible PBAoE bombs)
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
