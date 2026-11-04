---
schema_version: 1
class_name: "Necromancer"
class_id: 12
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Necromancer (Albion)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Death Servant`: Undead servant pet summoning (Greater Zombie, Skeletal Minion, Abomination), pet pulsing damage shield, pet taunts, pet combat buffs, servant mending/heals, and pet command suite (`/pet attack`, `/pet follow`, `/pet stay`, `/pet passive`, `/pet return`).
  - `Deathsight`: Baseline and spec Cold direct damage (DD), life-tap / lifedrain, long-range snare, strength/constitution transfer, and power transfer to allies.
  - `Painworking`: Stacking damage-over-time (DoT) spells, Point-Blank Area of Effect (PBAoE) decay/rot, magic resistance debuffs, and damage chants.
- **Base Abilities**: Sprint, Quick Cast, Shade Form (caster transforms into an invulnerable shade while controlling the physical undead servant).
- **Physical Undead Servant**: The undead servant acts as the tangible physical avatar in the game world, executing spells, melee attacks, or tanking.

---

## Specialization 1: Death Servant Necromancer
- **role**: PetPuller
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Death_Servant"
  min_point_ratio: 0.50
  spec_type_aliases: ["DeathServantNecro"]
```

### 1. Intended Role
Dedicated PvE Pet Puller, Undead Servant Tank Anchor, and Pet Commander.

### 2. Intended Role Summary
The Death Servant Necromancer is Albion's premier pet-pulling specialist. Through its summoned Greater Zombie / Abomination servant, it performs safe proximity body-pulling back to a secure stationary group, maintains a continuous pulsing damage shield on the servant, and manages passive/return states to ensure zero stray threat is transferred to group members.

### 3. Combat Philosophy
Methodical pull mechanics and unbreakable pet threat. The Death Servant Necromancer uses the physical servant as the frontline damage sponge. The pulsing damage shield inflicts steady damage on all attacking monsters. Once the pack is anchored at camp and reaches 85% health, group AoE casters melt the cluster down.

### 4. Mode: RvR
- **Servant Harassment**: Send Greater Zombie servant to pressure enemy healers and crowd controllers (Bards, Healers, Druids).
- **Servant Buffing**: Keep spec damage shields, haste, and AF buffs running on the servant.
- **Active RAs**: Trigger `AtlasOF_StrikeTheSoul` or `AtlasOF_WildMinion` to maximize servant melee damage output.
- **Positioning**: Keep Shade form tucked securely behind friendly frontline shield tanks.

### 5. Mode: PvP
- **Servant Pressure**: Command servant to relentlessly attack enemy archers and casters to cause pushback.
- **Servant Sustain**: Interleave servant heals to keep the pet alive against enemy burst.

### 6a. Mode: PvE Standard
- **Servant Tanking**: Command servant to engage the primary target with taunts and damage shield active.
- **Servant Sustain**: Heal servant between combat cycles to enter each pull at full health.

### 6b. Mode: PvE Pet Pulling (Primary Specialty)
- **Dedicated Puller Responsibilities**:
  - The Death Servant Necromancer is the **designated group puller**.
  - **Pet Mode Toggle**: Always set servant to **Passive** (`/pet passive`) before sending it toward the mob encampment. Ensure `Guard` mode is strictly disabled.
  - **Proximity (Body) Pulling**: Command servant to move near the target pack (`/pet go` or proximity approach) without attacking unless necessary.
  - **The Return Command**: The moment the mob pack registers the servant (aggros), immediately hit `/pet return` or `/pet follow` to bring the servant sprinting directly back to camp.
- **Continuous Pulsing Damage Shield**:
  - The Necromancer **must keep its pulsing damage shield running on the servant at all times**. This shield is the primary source of initial threat and damage.
- **Mobs Stay on the Pet (No Melee Taunting)**:
  - The mobs must stay focused entirely on the servant while the damage shield deals continuous damage.
  - Tanks and melee DPS do NOT attempt to taunt or peel the main pack off the servant.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the servant and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Necromancer continues managing the main pack.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Healers in the group (Clerics/Friars) ARE allowed to heal the servant, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Group healers cast their **best smallest heal** (`HealEfficient`), escalating to a bigger heal only on massive spike damage.
  - Pre-cast Health Regeneration / HoTs from Friars stay running on the servant.
- **Group Stays Put (Strict Stationary Discipline â€” No Running for Range)**:
  - The entire group remains firmly anchored at camp. Group members do **NOT** run away or kite backwards for casting range.
  - The Necromancer brings the mobs directly into the parked group.
- **The 85% Mob Health AoE Trigger**:
  - Once the servant's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, group AoE casters (Smite Clerics, Wizards, Cabalists) and the Necromancer are authorized to unleash AoE spells to melt the cluster.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **SUPREME SOLO DUNGEON GRINDER**. The Necromancer is widely regarded as the ultimate solo class in DAoC. The Necromancer Shade is completely immune to physical attacks and interrupts, while the summoned Greater Abomination pet acts as an impenetrable front-line tank. In dungeons (Catacombs, Tomb of Mithra, Tepok's Mine), the Necromancer positions the Abomination in doorways or corridor choke points. The pet absorbs all damage, while the Shade casts damage shields, pet heals, and melee enhancements with zero risk of interrupt.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Infinite sustained grinding with zero downtime.
  - **Secondary (Yellow)**: Effortless 1-on-1 solo baseline. Pet health barely dips.
  - **Restricted (Orange)**: Highly manageable; keep Pulsing Damage Shield running on pet and cast minor pet heal.
  - **Prohibited (Red / Purple)**: Red cons are soloable with active pet buffs; avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure Death Servant Pulsing Damage Shield and AF buffs are active on Abomination pet.
  - 2. Opener: Send Abomination pet to attack target mob (/pet attack).
  - 3. Threat Lock: Mob attacks pet, taking continuous damage from pulsing damage shield.
  - 4. Sustain: Cast Spec Pet Heal if pet health drops < 70%.
  - 5. Pet executes target with enhanced melee styles.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Necromancers rarely need to sit due to high mana efficiency, but if Mana < 20%, rest behind pet.
  - **Safety Check**: Shade is invulnerable; ensure pet is stationed defensively at doorway.
  - **Resume Threshold**: Resume at Mana >= 80%.
- **Emergency Escape Protocol**:
  - If overwhelmed by high-density room adds: Command pet to hold ground, cast pet sacrifice/defensive chant, and walk Shade safely out of dungeon to de-spawn and re-summon.

### 7. Offensive Priorities
1. Servant Pulsing Damage Shield: Priority 100 (Keep running on servant continuously)
2. Servant Return / Pull Command: Priority 95 (Recall servant the instant mobs aggro)
3. Baseline Deathsight DD: Priority 40 (Cast only at 85% mob health threshold or on break-off adds)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Servant Heal / Mend: Priority 85 (Sustain servant when taking heavy damage)
3. Self-Cast Base Buffs: Priority 80 (Keep active at all times)
4. Quick Cast: Priority 75 (Emergency instant heal / cast)

### 9. Target Priorities
1. Pulled Mob Pack (via Servant Body Pull): Priority 100
2. Loose Break-Off Add threatening group: Priority 90
3. Primary Kill Target (at 85% HP threshold): Priority 75

### 10. Positioning Preferences
- **Anchor**: Group Camp Center / Backline
- **Target Range**: 1200â€“1400 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Pulsing Damage Shield: Priority 100
2. `/pet return` / `/pet follow`: Priority 95
3. `AtlasOF_Purge`: Priority 90
4. Servant Heal: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery when mana < 25%)

### 12. Resource Management
- **Mana Floor**: 30% (Reserve power for re-summoning servant and refreshing damage shield)
- **Servant Re-Summon**: If servant dies mid-pull, move to melee tank line before recasting.

### 13. What the AI SHOULD Do
- Set servant to Passive before pulling; recall servant immediately upon mob aggro.
- Maintain continuous pulsing damage shield on the servant.
- Bring the pulled pack directly into the parked group.
- Protect group members by allowing tanks to engage break-off adds.

### 14. What the AI SHOULD NOT Do
- Never leave servant on Aggressive or Defensive during pulling (causes lethal links/adds).
- Never break camp anchor or run away backwards to gain range.
- Never attack with servant on pull when a simple proximity body pull is sufficient.
- Never let the pulsing damage shield expire during pet tanking.

### 15. Target Selection Matrix
- **Condition: Pull Phase Initiated**: Target Camp -> Send Servant (Passive) -> On Aggro -> Issue `/pet return`.
- **Condition: Mob Pack at Servant & HP <= 85%**: Target Clustered Mob -> Cast Baseline DD / Assist DPS.
- **Condition: Mob breaks off toward ally**: Select Break-off Add -> Call for Tank Peel.

### 16. Fallback Behavior
- **Servant Dies Mid-Fight**: Move shade toward tanks, activate `Quick Cast` + Re-summon once aggro stabilizes.
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from offensive DD, conserve power for damage shield upkeep.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_WildMinion` (Rank 1) - 1 pt [Total: 6] (Servant critical hit chance)
3. `AtlasOF_WildMinion` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_WildMinion` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
6. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
7. `AtlasOF_StrikeTheSoul` (Rank 1) - 5 pts [Total: 35] (Active servant strike ability)
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

## Specialization 2: Deathsight Necromancer
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Deathsight"
  min_point_ratio: 0.50
  spec_type_aliases: ["DeathsightNecro"]
```

### 1. Intended Role
Primary Ranged Nuker, Lifedrain Artillery, and Power Battery.

### 2. Intended Role Summary
The Deathsight Necromancer is Albion's premier drain-tank and direct damage caster. It specializes in high-yield Cold direct damage nukes, life-stealing lifedrain spells, long-range snares, and power transfer to sustain friendly group healers. It does NOT serve as a pet puller; instead, it acts as heavy ranged artillery and a power battery.

### 3. Combat Philosophy
Unrelenting attrition and self-sustain. The Deathsight Necromancer siphons life from enemy targets to replenish its servant's health while hammering priority targets with Cold DDs. In RvR, it provides impenetrable drain-tanking; in PvE, it delivers steady single-target DPS and powers group healers with mana transfers.

### 4. Mode: RvR
- **Target Assassination**: Assist Albion's Main Assist (MA) by focusing high-yield Cold DD nukes on called targets.
- **Snare Control**: Apply long-range Cold Snare to enemy melee peelers charging the backline.
- **Lifedrain Sustain**: Use Lifedrain to recover servant health when focused by enemy ranged attackers.
- **Power Transfer**: Transfer excess power to friendly Clerics or Sorcerers when their mana drops below 30%.
- **Positioning**: 1350â€“1500 units back.

### 5. Mode: PvP
- **Drain Tanking**: Keep Lifedrain cycling continuously against opponents while servant melees.
- **Snare & Kite**: Apply Cold snare to keep enemy melee fighters at bay.

### 6a. Mode: PvE Standard
- **Assist Train DPS**: Assist main tank on primary kill target.
- **Power Support**: Transfer power to group healer during prolonged encounters.

### 6b. Mode: PvE Pet Pulling (Non-Puller Role)
- **Role During Pet Pulling**: The Deathsight Necromancer is **NOT the puller**. It acts as stationary ranged artillery and power battery.
- **Hold Fire on Pull**: Strictly hold all direct damage, snares, and lifedrains while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, immediately assist in bursting down that loose add.
- **The 85% Mob Health Burn Trigger**: Once the pet's pulsing damage shield whittles the mob pack down to **85% health**, unleash maximum single-target Cold DD nukes on the designated primary kill target within the cluster.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **SUPREME SOLO DUNGEON GRINDER**. The Deathsight Necromancer channels massive baseline and spec lifetaps through the Abomination pet. Because the Shade cannot be hit or interrupted, tight dungeon corridors provide ideal line-of-sight funnels where the pet pins the mob against a wall while the caster drains its life force.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 2 Lifetaps + pet hits finish the mob with zero mana strain.
  - **Secondary (Yellow)**: Standard baseline solo target.
  - **Restricted (Orange)**: Fully viable; lifetap replenishes pet/caster health while draining target.
  - **Prohibited (Red / Purple)**: Avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Position Abomination in corridor.
  - 2. Opener: Send pet to attack.
  - 3. Lifetap Channel: Cast Deathsight Lifetap through pet line-of-sight.
  - 4. Finisher: Continuous lifetap drains target while keeping pet topped off.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%. Lifetaps keep health at 100%.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - Root add through pet tether -> Walk Shade back into cleared hallway.

### 7. Offensive Priorities
1. Spec Cold Direct Damage: Priority 90 (Primary nuke rotation on MA target)
2. Cold Snare: Priority 85 (Apply to charging melee or runners)
3. Lifedrain: Priority 80 (When servant HP < 65% during combat)
4. Baseline DD: Priority 50 (Finishing low-health targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Lifedrain on Target: Priority 90 (Recover lost hit points)
3. Power Transfer to Healer: Priority 85 (When friendly Cleric mana < 25%)
4. Quick Cast: Priority 75 (Emergency instant nuke / drain)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 90
3. Enemy Caster / Healer in RvR: Priority 85
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300â€“1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Spec Cold DD: Priority 90
3. Cold Snare: Priority 85
4. Power Transfer to Cleric: Priority 80
5. `AtlasOF_MCL`: Priority 75 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Power Transfer Reserve**: Do not transfer mana to allies if self mana is below 35%.

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Transfer power to friendly group healers when self mana is abundant.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.
- Never drain mana onto allies when self is out of power.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Spec Cold DD.
- **Condition: Friendly Cleric Mana < 25% & Self Mana > 50%**: Target Cleric -> Cast Power Transfer.
- **Condition: Enemy Melee approaching < 600 range**: Target Enemy -> Cast Cold Snare.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, refrain from power transfers, cast baseline DD or let servant melee.

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

## Specialization 3: Painworking Necromancer
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Painworking"
  min_point_ratio: 0.50
  spec_type_aliases: ["PainworkingNecro"]
```

### 1. Intended Role
Area Decay Specialist, Stacking DoT Pressure Caster, and PBAoE Finisher.

### 2. Intended Role Summary
The Painworking Necromancer is Albion's premier area decay and damage-over-time master. It specializes in stacking multi-line DoTs, resistance debuffs, damage chants, and Point-Blank Area of Effect (PBAoE) decay pulses. It does NOT pull with pets; in PvE pet pulling, it acts as an AoE decay finisher that melts the cluster at 85% health!

### 3. Combat Philosophy
Mass attrition and decay. The Painworking Necromancer blankets enemy clusters in stacking DoTs and decay auras, eroding their health bars and causing constant spell pushback. In RvR, it suppresses enemy caster lines with persistent damage; in PvE pet pulling, it unleashes devastating AoE decay into the pet-anchored pack.

### 4. Mode: RvR
- **Multi-DoT Blanket**: Cast spec DoTs on multiple enemy healers and casters to disrupt their cast bars and push back triage.
- **PBAoE Collapse**: If enemy melee trains collapse onto the Albion lines, activate `Mastery of Concentration` and unleash rapid PBAoE decay pulses.
- **Magic Resistance Shearing**: Cast spec debuffs to strip enemy magic resistances before AoE bombardment.
- **Positioning**: 1350 units back.

### 5. Mode: PvP
- **DoT Kiting**: Apply stacking DoTs, slow with snare, and kite while the decay melts the opponent's health bar.
- **PBAoE Defense**: Use PBAoE decay pulses when pressured in close-quarters melee.

### 6a. Mode: PvE Standard
- **Multi-Mob DPS**: Apply DoTs to secondary targets while bursting the primary target with PBAoE or DD.

### 6b. Mode: PvE Pet Pulling (The Decay Melting Role)
- **Role During Pet Pulling**: The Painworking Necromancer is **NOT the puller**. It acts as the **primary AoE decay finisher**.
- **Mobs Stay on the Pet**: Let the pet absorb the pull and let the pulsing damage shield establish baseline threat. Do NOT cast DoTs or spells on mobs in transit.
- **Break-Off Add Protection**: If any loose mob peels off from the pet toward the group, cast single-target DoT and assist in securing it.
- **THE 85% MOB HEALTH AoE / PBAoE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Painworking Necromancer initiates the **Decay Burn**:
  - Cast **AoE DoTs** and step into point-blank range of the cluster centered on the pet.
  - Unleash continuous **PBAoE decay pulses** to melt the entire pack down in seconds!
- **Strict Stationary Discipline on Pull**: Remain strictly stationed at camp during the pull phase. Do NOT run away or flee backwards to achieve range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **SUPREME SOLO DUNGEON GRINDER**. Painworking Necromancers utilize stacked DoTs and area affliction spells through their pet. In dungeon corridors, parking the pet in front of a mob cluster allows DoTs to melt hostiles while the Shade remains completely safe from interrupts.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid DoT kill rotation on single or linked targets.
  - **Secondary (Yellow)**: Standard target. Stack primary and secondary DoTs on pet's target.
  - **Restricted (Orange)**: Manageable; apply full DoT stack and let pet tank.
  - **Prohibited (Red / Purple)**: Avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Send Abomination to engage target at doorway.
  - 2. Opener: Apply primary Painworking DoT on target.
  - 3. Secondary DoT: Apply secondary DoT stack; pet maintains aggro.
  - 4. Finisher: Pet melee styles finish off DoT-weakened target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 20%.
  - **Resume Threshold**: Resume at Mana >= 80%.
- **Emergency Escape Protocol**:
  - Command pet to hold adds -> Shade walks safely back to entrance.

### 7. Offensive Priorities
1. Spec PBAoE Decay Pulses: Priority 100 (Unleash into mob cluster anchored on pet at 85% HP)
2. Stacking Spec DoTs: Priority 90 (Apply to primary targets and enemy casters)
3. Magic Resistance Debuff: Priority 85 (Strip resistances before AoE burn)
4. Baseline Cold DD: Priority 60 (Finishing single runners)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE / DoT casting)
3. Self-Cast Buffs: Priority 75
4. Quick Cast: Priority 70

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Decay)
2. Enemy Caster / Healer Line in RvR: Priority 95 (For DoT interrupt spread)
3. Loose Break-off Add: Priority 90
4. Primary Called Target: Priority 75

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **PBAoE Range**: 0â€“350 units from the mob cluster centered on the pet (during burn phase)
- **Pull Phase**: Stationary at camp anchor; never flee backwards for range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE decay if under attack)
3. Spec PBAoE Decay Spell: Priority 90 (At 85% mob health)
4. Stacking DoTs: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **High Mana Consumption**: PBAoE decay consumes significant mana.
- **PBAoE Throttling**: Stop PBAoE when mana drops below 20% or when only 1 mob remains; switch to single-target DoT/DD.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Unleash AoE DoTs and step into PBAoE decay range at 85% mob health to melt the cluster.
- Blanket enemy caster lines with DoTs in RvR to disrupt their casting.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast DoTs or PBAoE while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.
- Never spam PBAoE when only a single low-health monster remains.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Decay Pulses.
- **Condition: Enemy Caster without DoT**: Target Caster -> Cast Spec DoT.
- **Condition: Add breaks off toward healer**: Select Add -> Cast DoT -> Call for Peel.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease PBAoE casting immediately, withdraw to backline, cast single-target DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15] (Crucial for uninterruptible PBAoE decay)
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
