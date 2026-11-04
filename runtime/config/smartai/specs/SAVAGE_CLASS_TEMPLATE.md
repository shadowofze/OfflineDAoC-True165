---
schema_version: 1
class_name: "Savage"
class_id: 32
realm: "Midgard"
primary_stats: ["Strength", "Quickness", "Constitution"]
default_personality: "Aggressive"
---

# Class: Savage (Midgard)

## Shared Class Capabilities
- **Armor Type**: Studded (Level 20+), Leather (1â€“19).
- **Weapon Slots**: Hand-to-Hand (dual spiked fist claws with innate dual-wield mechanics); Two-Handed Sword, Axe, Hammer; One-Handed Sword, Axe, Hammer.
- **Core Specialization Lines**:
  - `Savagery`: Unique health-sacrificing self-buffs! The Savage sacrifices portions of current health to activate game-breaking combat enhancements: extreme melee Haste buffs, high-tier melee Damage Add buffs, Evade chance buffs, and Melee Critical hit buffs.
  - `Hand to Hand`: Fist and claw combat style chains featuring devastating side/rear stuns, bleeds, and rapid double-claw strikes.
  - `Weapon Lines` (`Sword`, `Axe`, `Hammer`): Single-hand and two-handed weapon specialization.
  - `Parry`: Passive melee attack deflection.
- **Base Abilities**: Sprint, Taunt, Dirty Tricks.
- **Midgard's Primal Fury**: Sacrificing their own life essence to fuel relentless ferocity, the Savage unleashes a blistering flurry of claw strikes, shredding foes under extreme haste and raw damage adds.

---

## Specialization 1: Hand-to-Hand Claw Savage
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Hand_To_Hand"
  min_point_ratio: 0.50
  spec_type_aliases: ["HandToHandSavage", "DualWield"]
```

### 1. Intended Role
Primary Claw Striker, Maximum Attack Speed Blender, Positional Flanker, and Break-Off Add Shredder.

### 2. Intended Role Summary
The Hand-to-Hand Claw Savage is the fastest-hitting melee combatant in Dark Age of Camelot. Combining 50 Hand-to-Hand with 49 Savagery, it activates self-haste and damage adds that transform dual claw attacks into a blinding storm of physical strikes, locking down targets with positional claw stuns and melting health bars.

### 3. Combat Philosophy
Hyper-speed melee onslaught. The Savage activates Savagery buffs prior to combat, closes the distance, maneuvers to the flanks, and buries opponents under a relentless barrage of claw strikes.

### 4. Mode: RvR
- **Savagery Pre-Buffing**: Sacrifice health to activate Savagery Haste, Damage Add, and Evade buffs before colliding with enemy forces.
- **Melee Assist Train**: Charge with Midgard's heavy frontline; target called enemy casters or healers with rapid claw style chains.
- **Positional Execution**: Maneuver to rear or side angles to land high-damage claw stuns.
- **Peel Enforcement**: Intercept enemy stealthers attacking Midgard healers; shred them under extreme haste.
- **Positioning**: Flank / Melee assist train (tight on called target).

### 5. Mode: PvP
- **Haste & Claw**: Activate Savagery buffs -> Close distance -> Maneuver for side/rear claw chain -> Land stun -> Unleash max-speed claw strikes to execute target before stun ends.

### 6a. Mode: PvE Standard
- **High-Velocity Melee DPS**: Assist main tank on designated targets; position behind mobs to execute rear style chains; manage health sacrifices with healer heals.

### 6b. Mode: PvE Pet Pulling (Claw Shredder & Break-Off Interceptor Role)
- **Role During Pet Pulling**: The Savage is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO TAUNTING THE MAIN PACK**:
  - **Do NOT taunt or pull aggro from the mob pack centered on the pet!**
  - The mobs **must stay on the pet** so the pet puller's pulsing damage shield deals continuous retribution damage.
- **Health Sacrifice Awareness**:
  - Activate Savagery buffs **at camp** before or during pull arrival.
  - Rely on Shaman/Healer/Friar HoTs on self to recover health spent on Savagery buffs.
- **Hold Melee Attacks During Transit**:
  - Do NOT charge out into the field to meet incoming mobs.
  - Remain strictly stationed at camp alongside the group until the pull pack arrives.
- **Break-Off Add Protection (Top Priority Peeler)**:
  - If any mob breaks away from the pet toward the healers or casters, **the Savage steps in immediately**:
  - Engage the loose add with rapid Hand-to-Hand claw style chains!
  - Destroy that specific loose add rapidly before it harms group casters.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Savage steps into melee:
  - Maneuver to the flank or rear of the designated primary focus target in the cluster.
  - Unleash blinding-speed **Hand-to-Hand claw chains** to carve through the pack one mob at a time!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **VERY HIGH SOLO DPS SURVIVABILITY**. Studded armor, Hand-to-Hand claw weapon style speed, self-haste and HP buffs, and quadruple-hit strike procs allow Savages to rip through single mobs in dungeon corridors.
  - **Corridor Pull Isolation**: Pull single mobs with throwing weapons into cleared hallway corridors; position mob against corridor wall for flank styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid kill turnover; mob dies in seconds from quadruple-hit claw procs.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline.
  - **Restricted (Orange)**: Engage only with active self-buffs (Haste / HP Buff) and full endurance.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-buff self with Savagery HP buff and Haste buff (sacrificing small health pool).
  - 2. Pull: Throwing weapon pull into cleared hallway.
  - 3. Opener: Hand-to-Hand opening style on contact.
  - 4. Stun & Bleed: Land positional side stun or claw bleed combo.
  - 5. Finisher: Quadruple-hit execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Savagery self-buffs consume health on cast; sit to rest when Health < 50% or Endurance < 25%.
  - **Safety Check**: Check corridor for roaming patrols before sitting.
  - **Resume Threshold**: Resume hunting at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Land claw side-stun on add -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Hand-to-Hand Style Burst on Loose Break-Off Add: Priority 100
2. Positional Rear / Side Claw Style Chain on Focus Target (at 85% HP): Priority 90
3. Savagery Haste / Damage Add Maintenance: Priority 85
4. Flanking Stun / Bleed Claw Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. First Aid (AtlasOF_FirstAid): Priority 85
4. Evade Defense Stance: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Claw Burst)
2. Primary Assist Focus Target (at 85% HP): Priority 90 (For Flanking Claw DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 80

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Rear or side flank of primary target
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Savagery Haste Buff: Priority 95 (Keep active)
3. Hand-to-Hand Weapon Styles: Priority 90 (At 85% mob health)
4. Savagery Damage Add: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Health Management**: Savagery buffs drain health; avoid refreshing buffs when health drops below 50%.

### 13. What the AI SHOULD Do
- Maintain Savagery Haste and Damage Add buffs.
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never refresh Savagery buffs when current health is under 40% (avoids suicide).
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Claw Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Flank/Rear -> Execute Hand-to-Hand Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Cease refreshing Savagery buffs, fall back toward Healer, pop First Aid, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15] (Reduces CC duration)
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts claw damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Two-Handed Savage
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Two_Handed"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandedSavage", "TwoHanded"]
```

### 1. Intended Role
Two-Handed Heavy Kinetic Striker, Hasted 2H Executioner, Frontline Shock Enforcer, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Two-Handed Savage combines the crushing impact of Midgard's massive Great Swords, Great Axes, and Great Hammers with the insane attack speed and damage adds of Savagery. Wielding a two-handed weapon at speeds typically reserved for dual-wielders, it delivers terrifying physical burst against enemy targets.

### 3. Combat Philosophy
Heavy weapon velocity. The Two-Handed Savage activates Savagery haste, closes distance, and crushes targets with rapid-fire two-handed style executions.

### 4. Mode: RvR
- **Hasted 2H Assist**: Push into enemy lines; execute two-handed styles with blazing haste onto called targets.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Shock Burst**: Pre-buff Savagery haste -> Close distance -> Positional 2H style chain -> Overpower target before they can react.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Assist main tank on designated targets with high-speed two-handed style chains.

### 6b. Mode: PvE Pet Pulling (2H Enforcer Role)
- **Role During Pet Pulling**: The Two-Handed Savage is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Savage immediately engages it**:
  - Intercept the loose add and shred it with hasted Two-Handed styles!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Savage assists DPS on the primary focus target with heavy 2H style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE CRUSH/SLASH BURST**. Two-handed weapon styles combined with Savagery haste buffs cut down single mobs rapidly in corridors.
  - **Corridor Pull Isolation**: Pull single mobs to safe hallways.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 to 4 heavy swings.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Only engage with full endurance and active buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Throwing weapon pull into hallway.
  - 2. Opener: Heavy two-handed opening style.
  - 3. Savagery haste accelerates two-handed swing speed.
  - 4. Finisher: Heavy two-handed execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Heavy Two-Handed Style Chain on Focus Target (at 85% HP): Priority 90
3. Savagery Haste / Damage Add Maintenance: Priority 85
4. Positional Stun Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Determination CC Reduction: Priority 95
3. Parry Defense: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For 2H Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For 2H DPS)
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Savagery Haste: Priority 95
3. Two-Handed Weapon Styles: Priority 90 (At 85% mob health)
4. Savagery Damage Add: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Health Management**: Monitor health sacrifices and pace Savagery buff refreshing.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain Savagery haste to maximize 2H swing speed.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never refresh buffs when health is below 40%.
- Never taunt the primary mob cluster off the puller's pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, parry, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Boosts 2H style damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: High Parry Savage
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Hand_To_Hand"
  min_point_ratio: 0.50
  spec_type_aliases: ["ParrySavage", "DualWield"]
```

### 1. Intended Role
Defensive Claw Striker, Weapon Deflection Blender, Sustained Flanker, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The High Parry Savage balances high-speed claw strikes with increased defensive weapon parrying. By training 39 Hand-to-Hand, 49 Savagery, and 20 Parry, it retains the lethal Savagery haste and damage adds while providing reliable weapon deflection against return physical strikes.

### 3. Combat Philosophy
Defensive weapon deflection combined with hyper-speed counter-attacks. The High Parry Savage deflects enemy melee strikes and retaliates with rapid claw combos.

### 4. Mode: RvR
- **Melee Assist Brawler**: Engage in frontline collisions; deflect enemy melee strikes with parry while shredding called targets with claw styles.
- **Positioning**: Forward frontline / Melee assist train.

### 5. Mode: PvP
- **Duel & Deflect**: Parry enemy strikes -> Counter with high-speed claw combos -> Outlast opponent through deflection and attack speed.

### 6a. Mode: PvE Standard
- **Melee DPS**: Assist main tank on designated targets; parry frontal attacks if holding secondary aggro.

### 6b. Mode: PvE Pet Pulling (Duelist Peeler Role)
- **Role During Pet Pulling**: The High Parry Savage is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**:
  - Remain strictly stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**:
  - Let the mobs stay on the pet. Do NOT taunt or pull threat off the pet puller's pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Savage immediately engages it**:
  - Intercept the loose add and shred it with claw style chains while parrying its attacks!
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Savage assists DPS on the primary focus target with claw style chains.
- **Strict Stationary Discipline**:
  - Remain strictly stationed at camp. Never chase mobs outside camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH DEFENSIVE RIPOSTE EFFICIENCY**. High parry spec deflects incoming hits and triggers claw counter-styles in tight corridors.
  - **Corridor Pull Isolation**: Pull single mobs to corridor; parry and counter.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, consistent grinding.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with full buffs.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Opener: Defensive stance / Parry reaction opener.
  - 3. Counter-Strike: Trigger claw riposte chain on parry deflection.
  - 4. Finisher: High-damage claw execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Sprint retreat to safety.

### 7. Offensive Priorities
1. Claw Style Burst on Loose Break-Off Add: Priority 100
2. Hand-to-Hand Style Chain on Focus Target (at 85% HP): Priority 90
3. Savagery Haste / Damage Add Maintenance: Priority 85
4. Parry Reactionary Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Parry Deflection Stance: Priority 95
3. Determination CC Reduction: Priority 90
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Claw Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Melee DPS)
3. Enemy Melee Attacking Backline: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Savagery Haste: Priority 95
3. Hand-to-Hand Weapon Styles: Priority 90 (At 85% mob health)
4. Parry Reactionary Styles: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Health Management**: Monitor health sacrifices and pace Savagery buff refreshing.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Exploit parry reactionary styles upon deflecting enemy strikes.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never refresh buffs when health is below 40%.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Claw Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Hand-to-Hand Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Fall back toward Healer, parry, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Determination` (Rank 1) - 1 pt [Total: 6]
3. `AtlasOF_Determination` (Rank 2) - 3 pts [Total: 9]
4. `AtlasOF_Determination` (Rank 3) - 6 pts [Total: 15]
5. `AtlasOF_MasteryOfParrying` (Rank 1) - 1 pt [Total: 16]
6. `AtlasOF_MasteryOfParrying` (Rank 2) - 3 pts [Total: 19]
7. `AtlasOF_MasteryOfParrying` (Rank 3) - 6 pts [Total: 25] (Weapon parry rate)
8. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 26]
9. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 29]
10. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 35] (Style damage)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
15. `AtlasOF_Toughness` (Rank 3) - 6 pts [Total: 49]
16. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 50]
