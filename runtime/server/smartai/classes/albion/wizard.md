---
schema_version: 1
class_name: "Wizard"
class_id: 7
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Aggressive"
---

# Class: Wizard (Albion)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Cold Magic`: Point-Blank Area of Effect (PBAoE) Ice Bomb (crushing area damage), Cold direct damage (DD), long-range Cold Snare, and Cold resistance debuffs.
  - `Fire Magic`: High-velocity Fire Bolt, high-damage Fire DD, Fire resistance debuff, and Fire damage-over-time (DoT).
  - `Earth Magic`: Earth/Matter direct damage (DD), Area-of-Effect Root, single-target Root, and self/ally damage shields.
- **Base Abilities**: Sprint, Quick Cast.
- **Albion's Elemental Engine**: The quintessential heavy artillery caster, capable of obliterating groups with PBAoE ice waves or sniping individual targets with long-range firebolts.

---

## Specialization 1: Ice Wizard
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Cold_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["IceWiz"]
```

### 1. Intended Role
Point-Blank Area of Effect (PBAoE) Ice Bomber, Cold Snare Artillery, and Area Finisher.

### 2. Intended Role Summary
The Ice Wizard is Albion's premier area devastation caster. It specializes in casting crushing Point-Blank Area of Effect (PBAoE) Ice spells, slowing incoming enemy forces with long-range Cold Snares, stripping Cold resistances, and delivering high-impact Cold direct damage.

### 3. Combat Philosophy
Close-quarters area devastation and mobility denial. In RvR, the Ice Wizard positions under tank protection to drop rapid PBAoE bombs on enemy zergs. In PvE pet pulling, it steps into point-blank range once the pack is anchored at 85% health and vaporizes the cluster.

### 4. Mode: RvR
- **PBAoE Bombing**: Coordinate with Albion's frontline tanks (Paladins, Armsmen); activate `Mastery of Concentration` and unleash rapid PBAoE Ice pulses into clustered enemy groups.
- **Snare Peel**: Apply long-range Cold Snare to enemy melee peelers charging the caster line.
- **Positioning**: 1350 units back during approach; collapse into PBAoE range only with guard or bubble protection.

### 5. Mode: PvP
- **Kite & Snare**: Apply Cold snare, create separation, and finish opponents with Cold DD and `Quick Cast`.

### 6a. Mode: PvE Standard
- **Multi-Mob Cleave**: Unleash PBAoE pulses once tanks establish threat on multi-mob pulls.

### 6b. Mode: PvE Pet Pulling (The Ice PBAoE Melting Role)
- **Role During Pet Pulling**: The Ice Wizard is **NOT the puller**. It acts as the **primary PBAoE area finisher**.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire while mobs are in transit to camp.
- **Break-Off Add Protection**:
  - If any mob breaks off from the pet toward the group, cast **Cold Snare** or baseline root on that loose add, or assist in burning it down.
- **THE 85% MOB HEALTH PBAoE ICE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Ice Wizard initiates the **Ice Bomb**:
  - Move into point-blank range of the cluster centered on the pet.
  - Unleash continuous, rapid **PBAoE Cold pulses** to melt the entire pull down in seconds!
- **Strict Stationary Discipline on Pull**:
  - Remain strictly stationed at camp during the pull phase. Do NOT run away or flee backwards to achieve range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Salisbury Plains, Campacorentin Station, Black Mountains North, Dartmoor). Open zones provide crucial 1500–2000 unit kiting lanes, predictable mob pathing, and maximum spell pre-cast distance.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons (Tomb of Mithra, Catacombs, Tepok's Mine) feature narrow corridors (<800 unit sightlines), low ceilings, and tight patrolling spawns. Because physical hits cause 100% spell interrupts and Ice Wizards wear fragile cloth armor without a persistent tank pet, soloing in dungeons results in immediate interrupts and death. **Only enter dungeons when grouped with a dedicated plate tank and primary healer**.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Primary grinding targets. Guarantees near-zero spell resists, rapid kill turnaround (<8 seconds), and minimal downtime.
  - **Secondary (Yellow)**: Single-target baseline when starting at 100% Mana. Open at maximum 1500 range with cold snare/nuke.
  - **Restricted (Orange)**: Only engage if QuickCast is available and environment allows a 1500-unit straight retreat lane.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; spell resists result in immediate melee gap-close and death.
- **Pull & Engagement Sequence**:
  - 1. Establish maximum casting distance (1500 units) in clear open terrain with an unobstructed backward kiting lane.
  - 2. Opener: Cast longest-range Ice Nuke / Ice Snare from max range.
  - 3. Secondary: Re-apply cold snare if mob closes within 800 units; kite backward while casting instant/fast Ice DD.
  - 4. Finisher: Cast point-blank cold pulse or QuickCast Ice Nuke if target reaches melee range.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Drop to sit/rest immediately when Mana < 25% or Health < 50% post-combat.
  - **Safety Check**: Scan 1000 units for wandering hostiles before sitting. If a patrol approaches within 600 units, stand and reposition 500 units away into cleared territory.
  - **Resume Threshold**: Stand and resume hunting only when Mana >= 85% and Health >= 90%.
- **Emergency Escape Protocol**:
  - If multiple hostiles link or Health drops < 30%: Trigger QuickCast + AoE Cold Root / Snare, turn 180°, activate Sprint, and flee toward open guarded zones or bindstone.

### 7. Offensive Priorities
1. PBAoE Ice Pulses: Priority 100 (Unleash into mob cluster anchored on pet at 85% HP)
2. Cold Snare: Priority 85 (Apply to charging melee or loose adds)
3. Cold Resistance Debuff: Priority 80 (Cast on primary target before burst)
4. Spec Cold DD: Priority 70 (Finishing single targets)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible PBAoE casting)
3. Cold Snare on Melee Attacker: Priority 90
4. Quick Cast: Priority 75

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For PBAoE Bomb)
2. Loose Break-off Add: Priority 90 (For Cold Snare / nuke)
3. Primary Called Target: Priority 75 (For Cold DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center
- **PBAoE Range**: 0–350 units from the mob cluster centered on the pet (during burn phase)
- **Pull Phase**: Stationary at camp anchor; never flee backwards for range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95 (Pop before PBAoE bomb if under attack)
3. Spec PBAoE Ice Spell: Priority 90 (At 85% mob health)
4. Cold Snare: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **High Mana Consumption**: PBAoE drains significant power.
- **PBAoE Throttling**: Stop PBAoE when mana drops below 20% or when only 1 mob remains; switch to single-target Cold DD.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Step into point-blank range of the pet-anchored cluster at 85% HP and unleash PBAoE.
- Snare loose break-off adds to protect group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast PBAoE while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.
- Never spam PBAoE when only a single low-health monster remains.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85%**: Move to Pet -> Cast PBAoE Ice Pulses.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Cold Snare -> Call for Peel.
- **Condition: Single Target Remaining**: Select Target -> Cast Cold DD.

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

## Specialization 2: Fire Wizard
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Fire_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["FireWiz"]
```

### 1. Intended Role
Long-Range Bolt Artillery, Fire DD Burst Nuker, and Debuffer.

### 2. Intended Role Summary
The Fire Wizard delivers massive long-range damage via Fire Bolts, Fire Direct Damage, Fire resistance debuffs, and `Volcanic Pillar`, acting as Albion's premier single-target sniper.

### 3. Combat Philosophy
Extreme range elimination and high-velocity burst. The Fire Wizard opens engagements from maximum distance with devastating Fire Bolts, strips enemy fire resistance with spec debuffs, and unleashes rapid Fire DDs to vaporize priority targets.

### 4. Mode: RvR
- **Bolt Opening**: Open on high-value enemy casters, healers, or archers with Fire Bolt from 1800 units.
- **Fire Debuff Assist**: Cast Fire resistance debuff on the Main Assist's target, then burst with spec Fire DD.
- **Volcanic Pillar**: Drop `AtlasOF_VolcanicPillar` on clustered enemy groups or siege engines.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Bolt & Burst**: Open with Fire Bolt -> Debuff -> `Quick Cast` + Fire DD -> Finish with follow-up nukes before target closes distance.

### 6a. Mode: PvE Standard
- **Assist DPS**: Assist main tank on designated target with Fire Debuff -> Fire DD rotation.

### 6b. Mode: PvE Pet Pulling (Single-Target Sniper Role)
- **Role During Pet Pulling**: The Fire Wizard is **NOT the puller**. It acts as stationary single-target artillery.
- **Hold Fire on Pull**: Strictly hold all direct damage, bolts, and debuffs while the pet puller brings the mob pack to camp. Do NOT cast spells on mobs in transit.
- **Break-Off Add Defense**: If any loose mob breaks off from the pet and threatens group members, immediately cast Fire DD to incinerate that loose add.
- **THE 85% MOB HEALTH BURN TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Fire Wizard unleashes spec **Fire Debuff + Fire DD nukes** on the designated primary kill target within the cluster!
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Salisbury Plains, Avalon Marsh, Black Mountains South). Wide-open terrain allows full exploitation of maximum-range fire artillery before enemies can close to melee.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Narrow dungeon corridors prevent maintaining 1500-unit casting range. Cloth armor and high cast interrupt rates make solo dungeon grinding non-viable. Must group with a tank and healer for dungeon crawl expeditions.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Maximum XP turnover per mana pool. Targets die within 2 to 3 spec Fire nukes before reaching the wizard.
  - **Secondary (Yellow)**: Standard 1-on-1 solo target. Requires 100% Mana and unobstructed line of sight.
  - **Restricted (Orange)**: Engage only with QuickCast ready to counter spell interrupts.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; high resist rates will drain mana and lead to fatal melee interrupts.
- **Pull & Engagement Sequence**:
  - 1. Position at extreme range (1500 units) with clear line of sight.
  - 2. Opener: Cast Spec Fire Resistance Debuff followed immediately by heavy Fire Direct Damage nuke.
  - 3. Mid-Flight Cast: Pre-queue second Fire DD while first projectile travels.
  - 4. Finisher: Cast baseline Fire DD or QuickCast nuke to vaporize target before it lands a physical swing.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest when Mana < 30% or Health < 50%.
  - **Safety Check**: Verify no roaming predators within 1000 units before sitting; reposition into cleared clearings.
  - **Resume Threshold**: Resume combat only when Mana >= 90% and Health >= 90%.
- **Emergency Escape Protocol**:
  - If add links or mob closes distance unexpectedly: QuickCast baseline Root, 180° turn, activate Sprint, and disengage.

### 7. Offensive Priorities
1. Fire Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Fire Direct Damage: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Fire Bolt: Priority 80 (Open RvR / single target opener)
4. Volcanic Pillar (AtlasOF_VolcanicPillar): Priority 75 (Deploy on clustered enemies)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 90 (Power through interrupts to cast Fire DD)
3. Quick Cast: Priority 80 (Emergency instant nuke)
4. Cold Snare: Priority 75 (Slow charging melee attacker)

### 9. Target Priorities
1. Main Assist's Called Target: Priority 100
2. Break-off Add threatening healers: Priority 95 (For rapid incineration)
3. Enemy Caster / Healer in RvR: Priority 90
4. Low Health Runner (< 20% HP): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Fire Resistance Debuff: Priority 95
3. Spec Fire DD: Priority 90
4. `AtlasOF_VolcanicPillar`: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 20%
- **Bolt Conservation**: Do not cast Fire Bolt once targets enter melee range (long cast time).

### 13. What the AI SHOULD Do
- Follow the Main Assist's target calls strictly.
- Hold all offensive spells during pet pulling until mobs reach the 85% HP threshold.
- Incinerate loose break-off adds immediately to protect healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never initiate pulls or attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Main Assist Target Active & (HP <= 85% or Break-off)**: Target MA's Target -> Cast Fire Debuff -> Cast Spec Fire DD.
- **Condition: Loose Add threatening healer**: Target Add -> Cast Spec Fire DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop casting high-cost spec Fire DD, switch to baseline DD.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Spell crit chance)
5. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_VolcanicPillar` (Rank 1) - 5 pts [Total: 43] (Active AoE fire strike)
8. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
9. `AtlasOF_Serenity` (Rank 1-2) - 3 pts [Total: 50]

---

## Specialization 3: Earth Wizard
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Earth_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["EarthWiz"]
```

### 1. Intended Role
AoE Root Controller, Matter DD Nuker, and Defensive Buffer.

### 2. Intended Role Summary
The Earth Wizard specializes in Area-of-Effect Roots to separate enemy formations, Matter direct damage nukes, Matter resistance debuffs, and party damage shields.

### 3. Combat Philosophy
Battlefield containment and physical mitigation. The Earth Wizard immobilizes incoming threats with AoE Roots, softens them with Matter debuffs, and unloads heavy Matter DDs.

### 4. Mode: RvR
- **AoE Root Control**: Cast maximum-range AoE Root on charging enemy melee trains to peel them off the Albion backline.
- **Matter DD Burst**: Assist the assist train with Matter DD nukes on called targets.
- **Damage Shield**: Keep damage shield active on friendly frontline tanks.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Root & Nuke**: Pin opponent with AoE/single Root, step out of melee reach, and burn them down with Matter DD.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Root on roaming adds that aggro during combat.
- **Damage Shield Uptime**: Keep damage shield active on main tank.

### 6b. Mode: PvE Pet Pulling (Root Controller & Peeler Role)
- **Role During Pet Pulling**: The Earth Wizard is **NOT the puller**. It acts as stationary root controller and burst support.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire and do NOT cast roots while mobs are in transit to camp.
- **Break-Off Add Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward the group, cast **Single Root** or **AoE Root** on that loose add immediately to protect group members.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Earth Wizard casts spec **Matter DD** into the designated primary kill target within the cluster.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Pennineway, Dartmoor foothills, Salisbury Plains). Earth Wizards utilize heavy root-and-nuke rotations that demand open spatial clearance.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Close dungeon quarters make root kiting hazardous due to tight corridor walls, roaming links, and immediate melee interrupts. Join a group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Optimal farming con. Root holds reliably with zero break-on-damage resists.
  - **Secondary (Yellow)**: Standard target. Earth root locks target in place while Earth Matter DD drains health.
  - **Restricted (Orange)**: Only engage if baseline root and QuickCast are off cooldown.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; root resists guarantee lethal melee engagement.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1500 units).
  - 2. Opener: Cast Long-Duration Earth Root on target.
  - 3. Turret Rotation: Step backward 300 units, apply Matter Resistance Debuff, and cast heavy Matter DD nukes while target is immobilized.
  - 4. Re-Root / Finisher: If root breaks early, re-cast fast root or finish with instant matter damage.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Safety Check**: Clear immediate 800-unit perimeter of roaming mobs before resting.
  - **Resume Threshold**: Stand when Mana >= 85% and Health >= 90%.
- **Emergency Escape Protocol**:
  - On multiple adds: QuickCast + AoE Earth Root -> Sprint to safety.

### 7. Offensive Priorities
1. AoE / Single Root: Priority 95 (Lock down loose adds or charging enemy melee)
2. Matter Resistance Debuff: Priority 85 (Apply to primary target)
3. Spec Matter Direct Damage: Priority 80 (Nuke called target at 85% mob HP)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Root on Attacker: Priority 95 (Peel melee attacker immediately)
3. Mastery of Concentration: Priority 90 (Power through interrupts to cast AoE root)
4. Damage Shield on Tank: Priority 85

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant Root peel)
2. Charging Enemy Melee Train in RvR: Priority 95 (For AoE Root)
3. Primary Assist Target: Priority 75 (For Matter DD at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. AoE / Single Root: Priority 95
3. `AtlasOF_MasteryOfConcentration`: Priority 90
4. Spec Matter DD: Priority 80
5. `AtlasOF_MCL`: Priority 75

### 12. Resource Management
- **Mana Floor**: 20% (Reserve power for emergency roots and damage shield upkeep)

### 13. What the AI SHOULD Do
- Root incoming enemy melee trains in RvR with AoE Root.
- Peel loose break-off adds immediately during pet pulling with roots.
- Cast Matter DD on the called target once mobs reach 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never root targets currently taking heavy melee damage (breaks root).
- Never cast roots or nukes while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Root.
- **Condition: Enemy Melee Train Charging**: Select Center -> Cast AoE Root.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Cast Matter DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease offensive DD, reserve power exclusively for emergency roots and damage shield.

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
