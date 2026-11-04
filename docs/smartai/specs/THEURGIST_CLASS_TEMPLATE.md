---
schema_version: 1
class_name: "Theurgist"
class_id: 5
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Theurgist (Albion)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Wind Magic`: Air Elemental Minion Summons (swift melee elementals that sprint to target, stun on contact, and relentlessly interrupt casting), Wind Direct Damage, baseline Pulsing Bladeturn (PBT), and Nearsight.
  - `Cold Magic`: Ice Elemental Minion Summons (ranged elementals that pelt enemies with cold damage and snares), Cold Direct Damage, Cold Area-of-Effect Snare, and Ice Bolts.
  - `Earth Magic`: Earth Elemental Minion Summons (durable, high-hitpoint elementals that taunt and absorb hits), Pulse Bladeturn (PBT up to 4â€“6 second pulse rate at 50 Earth), Earth Direct Damage, and Area of Effect Root.
- **Base Abilities**: Sprint, Quick Cast.
- **Albion's Elemental Conjurer**: The Theurgist overwhelms opposing forces by manifesting autonomous elementals that interrupt, snare, and harass enemies while shielding allies behind impenetrable Pulsing Bladeturn barriers.

---

## Specialization 1: Air Theurgist
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Wind_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["AirTheur"]
```

### 1. Intended Role
Anti-Caster / Anti-Healer Disruption Specialist, Air Elemental Swarm Conjurer, Wind Direct Damage Nuker, and Midline Buffer.

### 2. Intended Role Summary
The Air Theurgist is Albion's premier caster-suppression engine. It specializes in summoning high-velocity Air Elementals that sprint across the battlefield, stun enemy casters and healers on contact, and deliver non-stop melee interrupts. Backed by Wind Direct Damage and baseline Pulsing Bladeturn, the Air Theurgist paralyzes enemy casting backlines.

### 3. Combat Philosophy
Maximum disruption through elemental swarms. The Air Theurgist identifies high-value enemy casting targets and floods them with Air Elementals, rendering them completely incapable of completing spell casts, while maintaining defensive Bladeturn for the group.

### 4. Mode: RvR
- **Caster Suppression Swarm**: Spam Air Elementals (2â€“4 minions) onto enemy primary healers (Druids, Healers, Shamans) and heavy nukers (Eldritches, Runemasters) from 1500 units away.
- **Wind DD Burst**: Once enemy casters are disrupted and unable to cast, assist Albion nukers with rapid Wind DD strikes.
- **Pulse Bladeturn Maintenance**: Keep baseline Pulse Bladeturn running continuously to mitigate incoming physical attacks on Albion squishies.
- **Positioning**: 1350â€“1500 units back in the caster core.

### 5. Mode: PvP
- **Swarm & Overwhelm**: Summon 2 Air Elementals -> Stun on impact -> Quick Cast Wind DD -> Summon 3rd Elemental -> Finish target with Wind DD while opponent is locked down.

### 6a. Mode: PvE Standard
- **Caster Mob Neutralization**: Summon Air Elementals onto enemy caster mobs to cancel their spells; assist main tank with Wind DD.

### 6b. Mode: PvE Pet Pulling (Air Disruption & Add Peel Role)
- **Role During Pet Pulling**: The Theurgist is **NOT the puller**. It acts as **stationary elemental support and add interceptor**.
- **Hold Summons During Pull Phase**:
  - Strictly hold fire and do **NOT summon any elementals** while the pet puller is bringing mobs back to camp.
  - Premature elementals will attack the incoming pull pack, split mob pathing, and pull aggro off the main pet!
- **Break-Off Add Protection (Elemental Peel)**:
  - If any mob breaks away from the pet toward group healers or casters, **immediately summon an Air Elemental onto that loose add**!
  - The Air Elemental will sprint to the add, stun it, and take physical aggro away from the healer while the Theurgist nukes it with Wind DD.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the mob pack down to **85% health**, the Air Theurgist unleashes:
  - Cast **Wind Direct Damage** into the primary kill target within the cluster.
  - If a tough elite or high-threat mob is in the pack, summon 1â€“2 Air Elementals directly onto that elite to accelerate its demise!
- **Strict Stationary Discipline**:
  - Stay put at the camp anchor. Never flee backwards out of camp for range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Salisbury Plains, Dartmoor, Black Mountains). Open world affords unobstructed 1500-unit casting space to spawn Air Elemental swarms.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons feature narrow corridors and low ceilings that severely impair summoned pet pathing and trigger linked patrol adds. Without a dedicated tank, incoming melee hits cause 100% spell interrupts, preventing pet summoning. Must group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 2 Air Elementals stun-lock and kill the mob before it takes a single step.
  - **Secondary (Yellow)**: Standard target. Summon 3 Air Elementals; continuous stun procs prevent mob from closing distance.
  - **Restricted (Orange)**: Engage only with full mana (summon 4-5 Air Elementals).
  - **Prohibited (Red / Purple)**: Strictly avoid solo; resist rates cause elementals to fail and mob overwhelms caster.
- **Pull & Engagement Sequence**:
  - 1. Stand at 1500 units range in open field.
  - 2. Opener: Cast Air Elemental summon on target.
  - 3. Swarm Escalation: Rapidly cast 2 additional Air Elementals while first pet flies to target and lands stun proc.
  - 4. Finisher: Assist elemental swarm with baseline Wind DD nuke to execute target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30%. Spawning multiple elementals consumes significant mana.
  - **Safety Check**: Scan 1000 units for wandering patrols before sitting.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Cast QuickCast Air Stun / Root on add -> Sprint away while summoning an Air Elemental to distract pursuers.

### 7. Offensive Priorities
1. Air Elemental Summon on Loose Break-Off Add: Priority 100
2. Air Elemental Spam on Enemy Primary Healer/Caster (RvR): Priority 95
3. Wind Direct Damage on Primary Assist Target: Priority 85
4. Nearsight on Enemy Archer / Ranged Caster: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Pulse Bladeturn (PBT): Priority 95 (Ensure pulsing bubble is active at all times)
3. Air Elemental on Melee Attacker: Priority 90 (Stun melee attacker chasing self/healer)
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening healers: Priority 100 (For Air Pet Summon / Nuke)
2. Enemy Primary Healers / Casters: Priority 90 (For Air Pet Spam in RvR)
3. Primary Focus Target: Priority 80 (For Wind DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Pulse Bladeturn: Priority 95 (Keep running continuously)
3. Air Elemental Summon: Priority 90 (On loose add or priority enemy caster)
4. Wind Direct Damage: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Elemental Mana Consumption**: Summoning multiple elementals consumes significant power.
- **Throttling**: Limit elemental spam to 2â€“3 active pets in PvE.
- **MCL Timing**: Activate `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Keep Pulsing Bladeturn active on the group at all times.
- Hold all elemental summons and direct damage while the pet puller is bringing mobs to camp.
- Summon an Air Elemental instantly onto any loose mob that breaks toward healers.
- Assist with Wind DD on the pet's cluster once mobs hit 85% health.

### 14. What the AI SHOULD NOT Do
- Never summon elementals while mobs are in transit to camp (it disrupts pull geometry).
- Never spam 5+ elementals in PvE when mana is under 40% (avoids power exhaustion).
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Summon Air Elemental -> Cast Wind DD.
- **Condition: Enemy healer casting big heal (RvR)**: Select Healer -> Summon 2 Air Elementals.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Focus Target -> Cast Wind DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease summoning elementals; conserve power strictly for maintaining baseline Pulsing Bladeturn.

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

## Specialization 2: Ice Theurgist
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Cold_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["IceTheur"]
```

### 1. Intended Role
Ice Elemental Ranged Artillery, Cold Direct Damage Nuker, Area-of-Effect Snare Controller, and Kiter.

### 2. Intended Role Summary
The Ice Theurgist commands freezing Cold Magic, conjuring autonomous Ice Elementals that bombard enemy ranks from range with cold missiles and snares. Armed with Cold Direct Damage, heavy Cold Bolts, and Area-of-Effect Cold Snares, the Ice Theurgist cripples enemy mobility and shreds opposing forces from afar.

### 3. Combat Philosophy
Ranged bombardment and movement denial. The Ice Theurgist slows advancing enemies with AoE Cold Snares, deploys Ice Elementals to pepper targets with ranged spells, and executes targets with Cold DD.

### 4. Mode: RvR
- **Area Snare Barrier**: Cast AoE Cold Snare across enemy approach paths to break up charging melee zergs.
- **Ice Elemental Turrets**: Summon 2â€“3 Ice Elementals that establish stationary ranged fire onto enemy lines.
- **Cold DD Burst**: Coordinate with Albion assist train to drop heavy Cold DD on snared targets.
- **Positioning**: 1350â€“1500 units back.

### 5. Mode: PvP
- **Snare & Snipe**: Cast AoE/Single Cold Snare -> Summon Ice Elemental -> Backpedal -> Quick Cast Cold DD -> Ice Bolt finisher.

### 6a. Mode: PvE Standard
- **Snare & Burst**: Snare roaming adds; summon Ice Elementals to add ranged DPS on focus targets; assist with Cold DD.

### 6b. Mode: PvE Pet Pulling (Cold Snare & Ranged Elemental Support)
- **Role During Pet Pulling**: The Ice Theurgist is **NOT the puller**. It acts as **ranged artillery and add controller**.
- **Hold Fire on Pull**: Strictly hold all spells and pet summons while the pet puller is bringing the mobs to camp.
- **Break-Off Add Protection**:
  - If any mob breaks off from the pet toward the group, cast **Cold Snare** on it immediately and summon an Ice Elemental to peel it.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Ice Theurgist unleashes:
  - Cast **Cold Direct Damage** on the primary focus target in the cluster.
  - Summon 1â€“2 Ice Elementals to provide continuous ranged fire into the cluster!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor. Never flee backwards out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Campacorentin, Dartmoor foothills). Open terrain allows full use of Ice Elemental snare-kiting and cold nukes.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Tight dungeon geometry and lack of kiting distance make solo play fatal. Group with a tank and healer for dungeon content.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Ice Elemental snares target while Ice DD nukes shatter it safely at range.
  - **Secondary (Yellow)**: Standard target. Summon 2 Ice Elementals to maintain permanent snare on target.
  - **Restricted (Orange)**: Only engage with full mana pool and QuickCast available.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Max range setup (1500 units).
  - 2. Opener: Cast Ice Elemental summon on target.
  - 3. Slow & Nuke: As Ice Elemental snares target, channel Ice DD nukes into the crawling hostile.
  - 4. Finisher: Fast Ice DD or cold pulse to finish target before it reaches melee distance.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Drop to sit when Mana < 25% or Health < 50%.
  - **Resume Threshold**: Resume hunting at Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - QuickCast AoE Root -> 180Â° turn and Sprint retreat.

### 7. Offensive Priorities
1. Cold Snare on Loose Break-Off Add: Priority 100
2. AoE Cold Snare on Advancing Enemy Train (RvR): Priority 95
3. Cold Direct Damage on Focus Target: Priority 85
4. Ice Elemental Summon on Focus Target: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95
3. Cold Snare on Melee Pursuer: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group: Priority 100 (For Cold Snare / Nuke)
2. Primary Assist Kill Target: Priority 90 (For Cold DD / Ice Elemental)
3. Clustered Melee Forces: Priority 80 (For AoE Cold Snare)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. AoE Cold Snare: Priority 95
3. Cold Direct Damage: Priority 85
4. Ice Elemental Summon: Priority 80
5. `AtlasOF_MCL`: Priority 75

### 12. Resource Management
- **Mana Throttling**: Ice Elementals and Cold DD consume moderate mana; pop `AtlasOF_MCL` when below 25% power.

### 13. What the AI SHOULD Do
- Snare any mob that breaks off from the pet during pet pulling.
- Hold all elemental summons until the pull pack hits 85% health at camp.
- Maintain stationary camp discipline during pet pulling.
- Cast AoE Cold Snare to protect group casters from charging melee.

### 14. What the AI SHOULD NOT Do
- Never summon Ice Elementals during the pull phase before mobs arrive.
- Never flee backwards out of camp when mobs reach the pet.
- Never let Cold Snare drop on a loose add attacking a group healer.

### 15. Target Selection Matrix
- **Condition: Add breaks toward healer**: Select Add -> Cast Cold Snare -> Summon Ice Pet.
- **Condition: Charging enemy train (RvR)**: Target Center -> Cast AoE Cold Snare.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Focus Target -> Cast Cold DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease summoning elementals; rely on baseline Cold DD and save power for emergency snares.

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

## Specialization 3: Earth Theurgist
- **role**: DefensiveBuffer
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Earth_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: ["EarthTheur"]
```

### 1. Intended Role
Master of Pulsing Bladeturn (PBT 50), Earth Tank Elemental Conjurer, Area-of-Effect Root Controller, and Group Shield Anchor.

### 2. Intended Role Summary
The Earth Theurgist is Albion's ultimate defensive shield. By specializing heavily in Earth Magic (up to 50 Earth), it provides the fastest-pulsing, highest-tier Pulsing Bladeturn (PBT) in the game, refreshing melee-absorption bubbles across the entire group every 4â€“6 seconds. Combined with durable, taunting Earth Elementals and Area-of-Effect Roots, the Earth Theurgist provides supreme group defense.

### 3. Combat Philosophy
Impenetrable physical mitigation and area crowd control. The Earth Theurgist ensures Pulsing Bladeturn never lapses, pins enemy melee forces with Area Roots, conjures rock-hard Earth Elementals to absorb heavy hits, and chips away at targets with Earth DD.

### 4. Mode: RvR
- **Maximum PBT Coverage**: Maintain 50 Earth Pulsing Bladeturn at all times; this pulse negates an enormous volume of enemy melee and archer burst.
- **Area Root Lockdown**: Cast AoE Root on charging enemy frontline tanks to leave them stranded while Albion's casters destroy their backline.
- **Earth Elemental Walls**: Summon Earth Elementals to screen against enemy stealthers and absorb hits from hard-hitting melee.
- **Positioning**: 1200â€“1350 units back, securely in the center of the Albion support core.

### 5. Mode: PvP
- **PBT Defense & Root**: Keep PBT active -> Cast Single/AoE Root -> Summon Earth Elemental to tank opponent -> Cast Earth DD from safety.

### 6a. Mode: PvE Standard
- **PBT Group Defense**: Provide continuous PBT; summon Earth Elementals as off-tanks on multi-mob pulls; assist DPS with Earth DD.

### 6b. Mode: PvE Pet Pulling (High-Tier PBT & Earth Minion Off-Tanking)
- **Role During Pet Pulling**: The Earth Theurgist is **NOT the puller**. It acts as the **group's defensive PBT anchor and break-off interceptor**.
- **Continuous Pulsing Bladeturn (PBT)**:
  - **Keep Pulsing Bladeturn active throughout the entire pull and combat cycle**.
  - PBT shields the pet puller from stray ranged or melee hits during the pull, and absorbs hits for group members if any mob turns toward camp.
- **Hold Summons and Nukes During Pull Phase**:
  - Strictly hold fire and do NOT summon Earth Elementals while the pet is pulling mobs to camp.
- **Break-Off Add Protection (Earth Minion Taunt)**:
  - If any mob breaks away from the pet toward the healers, **immediately summon an Earth Elemental onto that loose add** or cast **Single Root**!
  - The Earth Elemental will engage and taunt the add, keeping group healers completely safe.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the mob cluster down to **85% health**, the Earth Theurgist begins casting **Earth Direct Damage** into the primary focus target.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor. Never flee backwards out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Prioritized** (Avalon Marsh, Cornwall). Open fields maximize Earth Elemental tanking distance and Earth PBT defense.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Although Earth Elementals have high melee durability, their temporary lifespan and slow summon time make dungeon corridor encounters hazardous when patrols add. Group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Earth Elemental holds threat while Earth Theurgist nukes down target.
  - **Secondary (Yellow)**: Standard target. Summon 2 Earth Elementals to absorb hits while self-PBT absorbs stray strikes.
  - **Restricted (Orange)**: Engage only with full mana and active Earth PBT running.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-cast Earth Pulsing Bludgeon/Melee Shield (PBT) on self.
  - 2. Opener: Summon Earth Elemental directly onto target from 1500 units.
  - 3. Mid-Fight: Summon second Earth Elemental; cast Earth Matter DD from behind elemental frontline.
  - 4. Finisher: Earth DD to finish target as elementals pummel mob.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Summon Earth Elemental on add to intercept -> Sprint away to safety.

### 7. Offensive Priorities
1. Earth Elemental Summon on Loose Break-Off Add: Priority 100
2. AoE Root on Advancing Enemy Melee (RvR): Priority 95
3. Earth Direct Damage on Focus Target: Priority 85
4. Single-Target Root: Priority 75

### 8. Defensive Priorities
1. Pulsing Bladeturn (PBT): Priority 100 (Keep running 100% of the time)
2. Purge (AtlasOF_Purge): Priority 95
3. Mastery of Concentration: Priority 90
4. Single Root on Attacking Melee: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening healers: Priority 100 (For Earth Pet / Root)
2. Primary Focus Target: Priority 85 (For Earth DD at 85% HP)
3. Clustered Melee Forces: Priority 80 (For AoE Root in RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1200 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. Pulse Bladeturn: Priority 100 (Never let drop)
2. `AtlasOF_Purge`: Priority 95
3. Earth Elemental Summon: Priority 90 (On loose break-off adds)
4. Earth Direct Damage: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **PBT Efficiency**: Pulsing Bladeturn consumes recurring power per pulse; prioritize power conservation.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 30% to prevent PBT from dropping.

### 13. What the AI SHOULD Do
- Keep 50 Earth Pulsing Bladeturn active at all times.
- Hold all elemental summons and nukes during the pull phase until the mob pack hits 85% health.
- Summon an Earth Elemental to intercept and taunt any mob that breaks off toward healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Pulsing Bladeturn drop or expire during combat.
- Never summon Earth Elementals while mobs are in transit to camp.
- Never flee backwards out of camp when mobs arrive.

### 15. Target Selection Matrix
- **Condition: PBT Not Active**: Cast Pulsing Bladeturn immediately.
- **Condition: Loose add breaks toward healer**: Select Add -> Summon Earth Elemental -> Cast Root.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Focus Target -> Cast Earth DD.

### 16. Fallback Behavior
- **Low Mana (< 20%)**: Cease all direct damage and elemental summons immediately; dedicate all remaining power exclusively to maintaining Pulsing Bladeturn.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_Serenity` (Rank 3) - 6 pts [Total: 30] (Essential for sustaining 50 PBT pulse)
7. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_Toughness` (Rank 1) - 3 pts [Total: 50]
