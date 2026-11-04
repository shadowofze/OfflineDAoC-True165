---
schema_version: 1
class_name: "Skald"
class_id: 24
realm: "Midgard"
primary_stats: ["Charisma", "Strength", "Constitution"]
default_personality: "Aggressive"
---

# Class: Skald (Midgard)

## Shared Class Capabilities
- **Armor Type**: Chain (Level 20+), Studded (1â€“19).
- **Weapon Slots**: One-Handed Sword, Axe, Hammer; Two-Handed Sword, Axe, Hammer; Small and Medium Shields.
- **Core Specialization Lines**:
  - `Battlesongs`: Midgard's premier Group Speed (Speed of the Sound / Song Speed 5), Group Melee Damage Add Song, Instant Direct Damage Shouts, Instant Area-of-Effect Mesmerize Song, and Combat Health Regeneration Song.
  - `Weapon Specialization` (`Sword`, `Axe`, or `Hammer`): High-damage melee weapon style chains featuring positional stuns, snares, and bleeds (usable with 1H or 2H weapons).
  - `Parry`: Passive melee attack deflection.
- **Base Abilities**: Sprint.
- **Midgard's War Chant Skald**: The battle skald leads Midgard warbands into combat with blazing realm speed, disrupts enemy formations with instant shouts and mez songs, amplifies melee damage with pulsing war chants, and smashes foes in close-quarters combat.

---

## Specialization 1: Pure Battlesongs Skald
- **role**: HybridSupport
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Battlesongs"
  min_point_ratio: 0.50
  spec_type_aliases: ["SongSkald", "Mid"]
```

### 1. Intended Role
Group Speed Leader (Speed 5), Damage Add Engine, Instant AoE Mez Disrupter, Instant DD Shout Striker, and Dedicated Backline Peeler.

### 2. Intended Role Summary
The Pure Battlesongs Skald is the tactical backbone of Midgard group mobility and offensive pacing. By taking Battlesongs to 50, it provides maximum movement speed out of combat, the highest-tier pulsing Damage Add song to amplify Midgard's physical strike forces, instant AoE Mesmerize songs to interrupt enemy charges, and instant DD shouts that bypass casting timers.

### 3. Combat Philosophy
Song management, disruption, and rapid backline peeling. The Skald positions between Midgard's heavy frontline and squishy support midline, twisting songs to maintain damage output, disrupting enemy casters with instant shouts, and intercepting enemy attackers charging Midgard healers.

### 4. Mode: RvR
- **Speed Delivery**: Provide maximum group speed out of combat; pop `Speed of Sound` (SOS) on contact to break enemy snares and close the gap.
- **Instant AoE Mez**: Drop instant AoE Mesmerize song on the enemy frontline or support cluster to break casting and stall charges.
- **Damage Add Twisting**: Maintain pulsing Damage Add song to grant bonus melee damage to all Midgard fighters.
- **Instant DD Shouts**: Fire instant DD shouts on cooldown against enemy casters to interrupt critical casts.
- **Peel Duty**: Intercept enemy stealthers (Infiltrators, Nightshades) charging Midgard healers; apply stunning melee styles.
- **Positioning**: Flexible midline (400â€“600 units ahead of healers, 400 units behind main tanks).

### 5. Mode: PvP
- **Engage & Disrupt**: Open with instant Mez song -> Close distance with speed -> Land rear/side positional weapon stun -> Unleash instant DD shouts -> Finish with weapon style chain.

### 6a. Mode: PvE Standard
- **Song Support**: Run speed song out of combat; switch to Damage Add song in combat; engage main assist target with weapon styles; peel loose adds off healers.

### 6b. Mode: PvE Pet Pulling (Damage Add & Primary Add Peeler Role)
- **Role During Pet Pulling**: The Skald is **NOT the pet puller**. It acts as **stationary chant support and primary backline peeler**.
- **Song Management During Pull**:
  - **Run Damage Add Song or Health Regen Song** at camp.
  - Do NOT run speed song during combat (speed can cause pathing desyncs).
- **Strict Melee Discipline (Hold Attacks on Pull)**:
  - **Do NOT charge out to meet incoming mobs!**
  - Hold all melee attacks and instant shouts while the pet is bringing mobs to camp.
  - Allow mobs to reach the pet and cluster around the pulsing damage shield.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward group healers or casters, **the Skald is the designated frontline peeler**:
  - Immediately fire an **Instant DD Shout** onto the loose add to pull aggro.
  - Step in and engage the loose mob in melee with **stunning or snaring weapon styles** to hold it securely away from the healers!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Skald assists the group:
  - Engage the primary focus target within the cluster using melee weapon styles.
  - Continue monitoring healer safety and peel any subsequent loose adds immediately.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor alongside the group. Do NOT run away or pursue mobs far from camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **PREMIER SOLO HYBRID SURVIVOR**. Skalds are elite solo grinders in DAoC. Heavy chain armor, pulsing Damage Add song, Health Regen song, continuous Speed Song, and instant Battlesong shouts (DD and Mez) allow the Skald to dominate in dungeon corridors (Spindelhalla, Cursed Tomb). **Battlesong shouts are completely instant and suffer ZERO cast interrupts from melee attacks**, making the Skald immune to caster corridor hazards.
  - **Corridor Pull Isolation**: Use ranged instant DD shout or throwing weapon to pull an isolated mob into a cleared corridor. Shouts provide instant damage without interrupting movement.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Instant shout + 2 weapon swings kill target with zero downtime.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Skald out-damages the mob easily.
  - **Restricted (Orange)**: Fully viable; use instant Mez shout to bandage/rest or reset combat.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Maintain pulsing Damage Add song and Health Regen song.
  - 2. Opener: Fire instant Battlesong DD shout on target from 1500 units to initiate pull.
  - 3. Melee Contact: Execute Two-Handed / One-Handed opening style as mob reaches melee.
  - 4. Instant Shout Weave: Fire secondary instant DD shout between weapon swing timers.
  - 5. Finisher: High-damage weapon execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest when Health < 50% or Endurance < 20%. Songs accelerate recovery.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Fire instant Battlesong AoE Mez shout on adds -> activate Speed Song -> Sprint away at maximum realm speed.

### 7. Offensive Priorities
1. Instant DD Shout on Loose Break-Off Add: Priority 100
2. Stunning / Snaring Melee Style on Loose Add: Priority 95
3. Instant AoE Mez Song (RvR Opener / Interrupt): Priority 90
4. Melee Weapon Style Chain on Focus Target (at 85% HP): Priority 85
5. Instant DD Shout on Focus Target on Cooldown: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Speed of Sound (AtlasOF_SpeedOfSound): Priority 95 (Group CC/snare break)
3. Ignore Pain (AtlasOF_IgnorePain): Priority 90 (Emergency self-heal)
4. Peel Threat from Group Healer: Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Shout / Melee Peel)
2. Enemy Casters / Healers: Priority 90 (For Instant Shouts in RvR)
3. Primary Focus Target: Priority 80 (For Melee DPS at 85% HP)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Midline (between healers and pet anchor)
- **Pull Phase**: Stationary at camp anchor; never charge out into the field.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_SpeedOfSound`: Priority 95 (RvR escape / engage)
3. Instant DD Shout: Priority 90 (On loose add or caster interrupt)
4. Damage Add Song: Priority 85 (Keep pulsing in combat)
5. `AtlasOF_IgnorePain`: Priority 80

### 12. Resource Management
- **Endurance Management**: Melee styles consume endurance; use efficient opening styles and manage endurance during long fights.
- **Instant Shout Mana**: Shouts consume small amounts of power; maintain sufficient power to always have shouts ready for interrupts.

### 13. What the AI SHOULD Do
- Run Damage Add song continuously during combat.
- Hold all melee swings and shouts while the pet puller is bringing mobs to camp.
- Immediately fire an instant shout and melee stun onto any loose mob that breaks toward healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to hit mobs while they are traveling to camp.
- Never run speed song during combat pulls (causes mob warping).
- Never ignore a loose add attacking a group healer to chase primary target DPS.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Fire Instant DD Shout -> Engage in Melee with Stun Style.
- **Condition: Enemy caster casting in RvR**: Select Caster -> Fire Instant DD Shout.
- **Condition: Mob Pack at Pet & HP <= 85% & Healers Safe**: Select Focus Target -> Execute Weapon Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_IgnorePain`, activate defensive parry stance, fall back toward healer protection.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SpeedOfSound` (Rank 1) - 10 pts [Total: 15] (Group CC break and maximum speed burst)
3. `AtlasOF_IgnorePain` (Rank 1) - 5 pts [Total: 20] (Emergency 50% instant heal)
4. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 44]
12. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Two-Handed Skald
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Sword"  # Or Axe / Hammer
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandSkald", "TwoHanded"]
```

### 1. Intended Role
Heavy Two-Handed Melee Striker, High-Burst Finisher, Instant DD Shout Nuker, and Offensive Enforcer.

### 2. Intended Role Summary
The Two-Handed Skald trades pure song mastery for immense physical burst damage. Wielding massive two-handed Great Swords, Great Axes, or Great Hammers, it delivers crushing positional strikes, pairs them with off-global instant DD shouts, and maintains high-tier song speed and damage adds.

### 3. Combat Philosophy
Aggressive target elimination and burst shock. The Two-Handed Skald charges priority targets, lands heavy two-handed opening styles, weaves instant shouts into swing recovery timers, and deletes squishy cloth casters in seconds.

### 4. Mode: RvR
- **Burst Assist Train**: Charge alongside Midgard's heavy frontline (Warriors, Savages, Berserkers); target called cloth casters or healers with two-handed burst styles and instant shouts.
- **Instant Shout Burst**: Weave instant DD shouts directly between two-handed weapon swings.
- **Positioning**: Forward frontline / Flank.

### 5. Mode: PvP
- **Shock & Shatter**: Close distance -> Instant Mez -> Position for 2H rear/side style -> Deliver massive physical burst -> Instant DD shout finisher.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-damage two-handed weapon style chains.

### 6b. Mode: PvE Pet Pulling (Melee Enforcer & Break-Off Destroyer)
- **Role During Pet Pulling**: The Two-Handed Skald is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Fire on Pull**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **immediately engage the loose add with Two-Handed styles and instant shouts**!
  - The massive burst of a 2H weapon will quickly obliterate the loose add before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Two-Handed Skald steps forward into melee:
  - Engage the primary focus target in the cluster with heavy **Two-Handed weapon styles**!
- **Strict Stationary Discipline**:
  - Remain stationed at camp during the pull phase. Never chase mobs out of camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **MASSIVE BURST DAMAGE IN CORRIDORS**. Heavy Two-Handed weapon styles combined with pulsing Damage Add song and instant shouts cut down single dungeon mobs in 3 swings.
  - **Corridor Pull Isolation**: Instant shout pull into cleared hallway; two-handed burst down.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: 2 to 3 swings kill target.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Viable; instant shouts bypass mob defenses.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Instant shout pull from doorway.
  - 2. Opener: Heavy two-handed opening style.
  - 3. Shout Weave: Weave instant DD shout during weapon delay.
  - 4. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez shout -> Speed Song flee.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Instant DD Shout on Loose Add / Focus Target: Priority 95
3. Two-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Instant AoE Mez Song: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Speed of Sound (AtlasOF_SpeedOfSound): Priority 95
3. Ignore Pain (AtlasOF_IgnorePain): Priority 90
4. Parry Defense Stance: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst)
2. Primary Assist Focus Target: Priority 90 (For 2H DPS at 85% HP)
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet puller anchor)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_SpeedOfSound`: Priority 95
3. Two-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. Instant DD Shout: Priority 85
5. `AtlasOF_IgnorePain`: Priority 80

### 12. Resource Management
- **Endurance Conservation**: 2H styles drain endurance quickly; cycle endurance-efficient styles during prolonged combat.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Weave instant shouts between two-handed melee swings.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never exhaust 100% endurance without saving enough for peel styles.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Fire Instant Shout.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_IgnorePain`, parry, and call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SpeedOfSound` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_IgnorePain` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_LongWind` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: One-Hand & Shield Skald
- **role**: DefensiveTankSupport
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Sword"  # Or Axe / Hammer with Shield
  min_point_ratio: 0.50
  spec_type_aliases: ["OneHandAndShieldSkald", "OneHandAndShield"]
```

### 1. Intended Role
Dedicated Healer Bodyguard, Shield Slam Stunner, Guard / Protect Provider, and Defensive Peeler.

### 2. Intended Role Summary
The One-Hand and Shield Skald combines solid melee resilience with active party defense. Equipped with a medium shield and one-handed weapon, it locks down enemy attackers with shield slam stuns, guards squishy healers with active `Guard` and `Protect`, pulses Battlesong damage adds, and neutralizes break-off threats with instant shouts.

### 3. Combat Philosophy
Protective shielding and crowd control peeling. The One-Hand and Shield Skald anchors directly beside Midgard's primary healer, intercepting incoming melee rushers with high-duration shield stuns and absorbing physical attacks.

### 4. Mode: RvR
- **Healer Bodyguard**: Anchor beside Midgard's primary healer (Healer or Shaman); maintain active `Guard` on that healer to absorb physical hits.
- **Shield Slam Peeling**: Use shield styles (slam/stun) to stun enemy melee peelers attacking the backline for up to 9 seconds.
- **Battlesong Chants**: Keep Damage Add song running to buff frontline fighters while defending the rear.
- **Positioning**: Tight backline (within 200 units of primary healer).

### 5. Mode: PvP
- **Shield Stun & Attrition**: Shield slam stun opponent -> Position for side/rear weapon style chain -> Instant DD shout -> Block incoming strikes with shield.

### 6a. Mode: PvE Standard
- **Off-Tank & Guard**: Guard the main healer; off-tank secondary adds with shield block; assist DPS on primary targets.

### 6b. Mode: PvE Pet Pulling (Dedicated Healer Bodyguard & Shield Peeler)
- **Role During Pet Pulling**: The Shield Skald is **NOT the puller**. It acts as the **dedicated camp bodyguard and healer protector**.
- **Guard Primary Healer**:
  - **Set active `Guard` on the group's primary healer** prior to the pull!
- **Hold Fire on Pull**:
  - Remain strictly stationed beside the group healer at camp. Do NOT run out to attack mobs in transit.
- **Break-Off Add Protection (Shield Slam Peeling)**:
  - If any mob breaks away from the pet toward the healers, **the Shield Skald immediately intercepts the mob**:
  - Hit the loose add with an **Instant DD Shout** to secure threat.
  - Land a **Shield Slam (Stun)** directly on the add, locking it down completely away from the healer!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Shield Skald assists DPS on the primary focus target with one-handed weapon styles, while remaining within peeling distance of the healer.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp beside the healer. Never abandon the bodyguard post.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH DEFENSIVE SURVIVABILITY**. Large Shield blocks and Slam stuns combined with instant shouts provide total lockdown in tight corridors.
  - **Corridor Pull Isolation**: Pull single targets to hallway; slam-lock and dispatch.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding with near-zero damage taken.
  - **Secondary (Yellow)**: Standard target. Slam stun ensures safe combat.
  - **Restricted (Orange)**: Viable with Slam stun.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Instant shout pull into corridor.
  - 2. Opener: Shield Slam (stun).
  - 3. Shout & Style: Instant DD shout during stun + weapon style combo.
  - 4. Finisher: Block-reaction counter-style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun add / Instant Mez -> Speed Song flee.

### 7. Offensive Priorities
1. Instant DD Shout on Loose Break-Off Add: Priority 100
2. Shield Slam Stun on Loose Add: Priority 95
3. One-Handed Weapon Style Chain on Focus Target (at 85% HP): Priority 85
4. Instant DD Shout on Cooldown: Priority 80

### 8. Defensive Priorities
1. Guard on Primary Healer: Priority 100 (Keep active at all times)
2. Purge (AtlasOF_Purge): Priority 95
3. Shield Block Stance: Priority 90
4. Ignore Pain (AtlasOF_IgnorePain): Priority 85

### 9. Target Priorities
1. Loose Break-off Mob threatening guarded healer: Priority 100 (For Shield Slam)
2. Primary Assist Focus Target: Priority 85 (For 1H DPS at 85% HP)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Tight Backline (within 200 units of guarded primary healer)
- **Pull Phase**: Stationary at camp anchor; never leave the healer's side.

### 11. Ability Usage Priorities
1. Guard Ability: Priority 100 (Targeted on main healer)
2. `AtlasOF_Purge`: Priority 95
3. Shield Slam Style: Priority 90 (On loose add)
4. Instant DD Shout: Priority 85
5. `AtlasOF_IgnorePain`: Priority 80

### 12. Resource Management
- **Shield Endurance**: Shield slam styles consume moderate endurance; conserve endurance specifically for peeling emergencies.

### 13. What the AI SHOULD Do
- Keep Guard active on the primary healer at all times.
- Hold position at camp beside the healer during pet pulling.
- Shield slam and stun any loose add that breaks toward the healer.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never abandon the healer to chase DPS targets far from camp.
- Never run speed song during combat pulls.
- Never let Guard drop from the primary healer.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Fire Instant Shout -> Execute Shield Slam Stun.
- **Condition: Mob Pack at Pet & HP <= 85% & Healer Safe**: Select Primary Focus Target -> Execute 1H Weapon Styles.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_IgnorePain`, enter maximum shield block defense, call for peel.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SpeedOfSound` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_IgnorePain` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_AvoidanceOfMagic` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfBlock` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_MasteryOfBlock` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_LongWind` (Rank 1) - 3 pts [Total: 50]
