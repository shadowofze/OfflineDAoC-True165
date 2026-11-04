---
schema_version: 1
class_name: "Minstrel"
class_id: 4
realm: "Albion"
primary_stats: ["Charisma", "Dexterity"]
default_personality: "Tactical"
---

# Class: Minstrel (Albion)

## Shared Class Capabilities
- **Armor Type**: Chain (absorb 27%), Leather, Cloth.
- **Weapon Slots**: One-Handed Slash or Thrust (Right Hand) + Small Shield (Left Hand), Instruments (Lute, Flute, Drum, Harp).
- **Core Specialization Lines**:
  - `Instruments`: Songs of Speed (Speed of the Realm), Songs of Mesmerization (AoE and single-target Mez), Instant Instrument Stun, Amnesia (CC dispel/memory wipe), Monster Chaming, Health and Power regeneration songs, Direct Damage Shouts.
  - `Slash`: Slashing weapon styles (Ruby Slash, Amethyst Slash, Diamond Slash) providing high-impact positional burst, bleeding, and attack speed debuffs.
  - `Thrust`: Thrusting weapon styles (Dragonfly, Basilisk, Viper) providing rear/side positional stuns and piercing burst.
  - `Stealth`: Concealment and stealth mobility.
- **Base Abilities**: Sprint, Evade 2, Shield Block, Instruments Pulsing Songs.
- **Group Mobility & CC Anchor**: Albion's premier speed driver and instant crowd controller.

---

## Specialization 1: Instruments Minstrel
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Instruments"
  min_point_ratio: 0.50
  spec_type_aliases: ["InstrumentMinstrel"]
```

### 1. Intended Role
Group Speed Driver, Primary Crowd Controller, and Charmed Pet Commander.

### 2. Intended Role Summary
The Instruments Minstrel is Albion's indispensable tactical pulse. It specializes in maximizing movement speed via pulsing Speed of the Realm songs, locking down enemy groups with area-of-effect and single-target instrument mesmerization, peeling attackers with instant instrument stuns, and controlling powerful charmed monster pets.

### 3. Combat Philosophy
Tactical mobility, tempo control, and instant disruption. The Instruments Minstrel dictates the terms of engagement: accelerating friendly positioning with speed songs, interrupting enemy lines with shouts, neutralizing reinforcements with instant mez, and locking down priority targets with stuns.

### 4. Mode: RvR
- **Speed Delivery**: Keep Speed of the Realm pulsing during movement; twist health or resist songs when entering combat range.
- **Instant Stun Peel**: Use instant Instrument Stun on enemy stealthers or assist-train tanks diving Albion's backline casters.
- **AoE Mesmerize**: Target incoming enemy groups with instrument mez to separate their frontline from their support.
- **Charm Pet Harass**: Command high-level charmed humanoid/beast pet to disrupt enemy healers.
- **SOS Authority**: Pop `AtlasOF_SOS` (Speed of Sound) when initiating an ambush or disengaging from an overwhelming enemy force.

### 5. Mode: PvP
- **Kiting & Stun**: Maintain distance with speed song, land instant stun, apply shouted damage, and strike with positional melee styles.
- **Charm Pressure**: Overwhelm opponents with charmed pet damage.

### 6a. Mode: PvE Standard
- **Pacing**: Drive party travel speed with instruments.
- **Add Lockdown**: Immediately mez roaming adds with instrument mez.
- **Melee Assist**: Engage the primary target with slash/thrust styles while songs pulse in the background.

### 6b. Mode: PvE Pet Pulling (Support & Peeler Role)
- **Role During Pet Pulling**: The Minstrel is **NOT the puller**. It acts as stationary speed, buff, and break-off peeler support.
- **Song Discipline**:
  - Run **Speed of the Realm** while travelling, but switch to **Health Regeneration** or **Resist songs** once anchored at camp.
  - Do NOT play charm songs on mobs in the pull pack.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused entirely on the pet while the pet's pulsing damage shield deals continuous damage.
  - Do NOT melee or stun the main pack while it is anchored to the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets group members, the Minstrel immediately casts **Instant Instrument Stun** or single Mez on that loose add, or engages it with melee styles to secure it.
- **The 85% Mob Health Burn Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to **85% health**, the Minstrel is authorized to engage the designated primary kill target with melee styles and DD shouts.
- **Strict Stationary Discipline**: Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards for casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT (CHARM PET LEVELING MASTER)**.
  - **Dungeon Suitability**: **SUPREME SOLO PET SURVIVABILITY**. The Minstrel is one of the strongest solo classes in DAoC because of the **Charm** song. In dungeons (Catacombs, Tomb of Mithra, Tepok's Mine), the Minstrel charms a powerful local monster (up to Yellow con), turning it into a disposable front-line tank! The Minstrel parks the Charmed pet in a corridor bottleneck, sends it to attack dungeon hostiles, and pulses health regen and battle songs from behind while the charmed pet absorbs all melee attacks. Zero interrupt risk for the Minstrel.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Charmed pet kills target rapidly with zero downtime.
  - **Secondary (Yellow)**: Standard target. Charmed pet holds aggro effortlessly.
  - **Restricted (Orange)**: Viable; assist pet with instant shouts and flank sword styles.
  - **Prohibited (Red / Purple)**: Avoid solo; charmed pet will die quickly against Purple.
- **Pull & Engagement Sequence**:
  - 1. Locate and Charm a local Yellow or High Blue humanoid/monster to serve as your pet tank.
  - 2. Position pet in corridor bottleneck.
  - 3. Opener: Command Charmed pet to attack target mob (/pet attack).
  - 4. Song Support: Pulse Health Regen song / Add Damage song on pet.
  - 5. Combat Assist: Fire instant DD shouts or step behind mob to land rear Slash/Thrust styles.
  - 6. If Charmed pet is about to die, break charm and let pet and mob kill each other, or re-charm.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind Charmed pet when Health < 50% or Endurance < 20%. Speed/Health songs accelerate recovery.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez shout on adds -> activate Speed Song -> Sprint away at maximum realm speed.

### 7. Offensive Priorities
1. Instant Instrument Stun: Priority 95 (Apply to loose adds or enemy casters)
2. Instrument Mesmerize: Priority 90 (Control secondary adds or enemy clusters)
3. Direct Damage Shout: Priority 75 (Cast on called target off cooldown)
4. Positional Melee Styles: Priority 70 (Execute on called target at 85% HP)

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100 (Emergency group disengage / burst)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Instant Stun on Pursuer: Priority 95 (Peel melee attacker off self or healer)
4. Song Twisting (Health / Resist): Priority 85 (Maintain defensive song uptime)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant stun/peel)
2. Enemy Caster / Healer in RvR: Priority 95 (For instant stun and pet harass)
3. Uncontrolled Enemy Group: Priority 90 (For AoE Mez)
4. Primary Called Target: Priority 75 (For melee DPS at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Midline / Flexible
- **Target Range**: 300â€“800 units (Able to dart into melee or peel backline)
- **Camp Anchor**: Stay stationed with the group during pet pulls.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Instant Instrument Stun: Priority 95
4. Speed / Health Songs: Priority 90
5. Instrument Mesmerize: Priority 85

### 12. Resource Management
- **Endurance Management**: Minstrels consume endurance through melee styles and sprint. Reserve 25% endurance for emergency escapes or stun styles.

### 13. What the AI SHOULD Do
- Keep Speed song active out of combat; switch to combat songs upon engagement.
- Stun loose break-off adds immediately during pet pulling to protect healers.
- Use instant crowd control to peel attackers off friendly casters.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never mez targets taking damage from the pet's damage shield (breaks mez).
- Never charge ahead and attack mobs while the pet puller is bringing them to camp.
- Never let defensive songs lapse during prolonged encounters.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Cast Instant Stun -> Engage with Melee.
- **Condition: Incoming Enemy Group in RvR**: Select Center -> Cast Instrument Mez.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Execute Positional Styles -> Cast Shouts.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_SOS`, sprint out of melee range, toggle instant stun on pursuer, call for heal.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20] (Speed of Sound - signature 15 pt active RA)
3. `AtlasOF_MasteryOfBlocking` (Rank 1-2) - 1 + 3 = 4 pts [Total: 24]
4. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 28]
5. `AtlasOF_SecondWind` (Rank 1) - 5 pts [Total: 33] (Endurance recovery)
6. `AtlasOF_AugStr` (Rank 1-2) - 1 + 3 = 4 pts [Total: 37]
7. `AtlasOF_AugDex` (Rank 1-2) - 1 + 3 = 4 pts [Total: 41]
8. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 45]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 49]
10. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Slash Combat Minstrel
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Slash"
  min_point_ratio: 0.50
  spec_type_aliases: ["SlashMinstrel"]
```

### 1. Intended Role
Melee Burst Specialist, Combat Skirmisher, and Secondary CC.

### 2. Intended Role Summary
The Slash Minstrel pairs chain armor durability with high-potency slashing weapon styles (Ruby Slash, Amethyst Slash) and core instrument speed songs. It acts as an aggressive frontline skirmisher, applying heavy burst damage while providing utility stuns.

### 3. Combat Philosophy
Aggressive melee pressure backed by song utility. The Slash Minstrel maneuvers to execute flank and rear positional slashing styles, stacking bleeds and slowing enemy attack speed.

### 4. Mode: RvR
- **Assist Train Melee**: Stick to the Main Assist's target and execute slash style sequences.
- **Caster Interruption**: Use instant stun and slash burst to dispatch enemy light-armor casters.
- **Positioning**: Frontline / Flank (in melee contact with called target).

### 5. Mode: PvP
- **Melee Brawling**: Exploit chain armor, parry/shield block, and positional slash styles to dominate physical duels.

### 6a. Mode: PvE Standard
- **Frontline Melee**: Engage the primary target with style rotations while pulsing songs in the background.

### 6b. Mode: PvE Pet Pulling (Melee Peeler & Striker Role)
- **Role During Pet Pulling**: The Slash Minstrel is **NOT the puller**. It acts as stationary add peeler and melee striker.
- **Song Discipline**: Run Speed to camp, switch to Health/Resist songs at camp anchor.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Do NOT charge out to meet or attack the incoming mob pack.
- **Loose Mob Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward group members, the Slash Minstrel steps in immediately, executes **Instant Instrument Stun** or positional slash styles, and holds that loose add.
- **THE 85% MOB HEALTH MELEE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Slash Minstrel engages the designated primary kill target within the cluster with full slashing styles and DD shouts!
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE BURST & PET UTILITY**. Slash spec provides heavy burst damage and positional side stuns (Amethyst Slash), allowing the Minstrel to fight effectively in corridors alongside a charmed pet or solo.
  - **Corridor Pull Isolation**: Pull single mobs to safe corridors; stun with Slash style.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in stun duration + slash combo.
  - **Secondary (Yellow)**: Standard target. Use charmed pet or land side stun combo.
  - **Restricted (Orange)**: Only engage with Charmed pet tanking.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Charm local dungeon mob or body pull target.
  - 2. Opener: Instant DD shout on contact.
  - 3. Melee Combo: Step to mob flank -> land Slash side stun.
  - 4. Finisher: High-damage Slash execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or End < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez shout -> Speed Song flee.

### 7. Offensive Priorities
1. Slashing Weapon Styles: Priority 95 (Execute on called target at 85% mob HP)
2. Instant Instrument Stun: Priority 90 (Apply to loose adds or enemy peelers)
3. Direct Damage Shouts: Priority 75
4. Instrument Mez: Priority 60 (Emergency add lockdown)

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100
2. Purge (AtlasOF_Purge): Priority 100
3. Instant Stun on Pursuer: Priority 95
4. Shield Block / Parry: Priority 85

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant stun and slash peel)
2. Primary Assist Target: Priority 90 (For slash DPS at 85% mob HP)
3. Enemy Caster / Healer in RvR: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline / Frontline
- **Target Range**: Melee contact (during 85% burn phase)
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Instant Instrument Stun: Priority 95
4. Slashing Styles: Priority 90
5. `AtlasOF_SecondWind`: Priority 80 (Endurance recovery)

### 12. Resource Management
- **Endurance**: Conserve 25% endurance for emergency sprint and stun styles.

### 13. What the AI SHOULD Do
- Peel loose break-off adds immediately during pet pulls with instant stun and slash styles.
- Engage the called target with full melee styles at the 85% health threshold.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Cast Instant Stun -> Slash Styles.
- **Condition: Mob Pack HP <= 85%**: Select Primary Assist Target -> Execute Slashing Styles.

### 16. Fallback Behavior
- **Low Endurance (< 15%)**: Activate `AtlasOF_SecondWind`, pause high-cost styles, rely on auto-attacks and shouts.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20]
3. `AtlasOF_SecondWind` (Rank 1) - 5 pts [Total: 25]
4. `AtlasOF_AugStr` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 35]
5. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 39]
6. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 43]
7. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
8. `AtlasOF_AugDex` (Rank 1-2) - 3 pts [Total: 50]

---

## Specialization 3: Thrust Combat Minstrel
- **role**: MeleeDPS
- **default_personality**: Opportunistic

```yaml
match_specs:
  primary_line: "Thrust"
  min_point_ratio: 0.50
  spec_type_aliases: ["ThrustMinstrel"]
```

### 1. Intended Role
Piercing Burst Stunner, Stealth Flanker, and Utility Disruptor.

### 2. Intended Role Summary
The Thrust Minstrel focuses on piercing style chains (Dragonfly, Basilisk) featuring long-duration rear and side stuns, high critical strike modifiers, speed utility, and shout burst.

### 3. Combat Philosophy
Positional execution. The Thrust Minstrel positions for positional stun styles to take opponents completely out of combat before executing follow-up burst chains.

### 4. Mode: RvR
- **Positional Stun Locking**: Execute rear/side thrust styles (Dragonfly) on enemy healers or casters to lock them in place.
- **Burst Finisher**: Follow stuns with high-damage piercing styles and DD shouts.
- **Positioning**: Flank / Backline.

### 5. Mode: PvP
- **Stun Locking**: Cycle instant instrument stuns and weapon thrust stuns to keep opponents perpetually disabled.

### 6a. Mode: PvE Standard
- **Positional Melee**: Maneuver behind the tank's target to land rear positional thrust styles.

### 6b. Mode: PvE Pet Pulling (Melee Peeler & Striker Role)
- **Role During Pet Pulling**: The Thrust Minstrel is **NOT the puller**. It acts as stationary add peeler and piercing striker.
- **Hold Fire on Pull**: Strictly wait at camp until the mobs arrive.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Do NOT attack the incoming mob pack in transit.
- **Loose Mob Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward group members, the Thrust Minstrel steps in immediately, lands **Instant Instrument Stun** or **Dragonfly**, and secures that loose add.
- **THE 85% MOB HEALTH MELEE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Thrust Minstrel engages the designated primary kill target within the cluster with full thrust styles!
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **FAST COMBAT & CHARM FLEXIBILITY**. Thrust styles provide fast swing recovery and evade counters in narrow dungeon corridors.
  - **Corridor Pull Isolation**: Pull single targets to hallways; utilize charmed pet or thrust styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills with minimal endurance loss.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with charmed pet.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Send charmed pet or pull mob to corridor.
  - 2. Opener: Instant shout + Thrust opener.
  - 3. Combo: Thrust positional / evade reaction combo.
  - 4. Finisher: Thrust execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or End < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez shout -> Speed Song flee.

### 7. Offensive Priorities
1. Thrust Weapon Styles: Priority 95 (Execute on called target at 85% mob HP)
2. Instant Instrument Stun: Priority 90 (Apply to loose adds or enemy peelers)
3. Direct Damage Shouts: Priority 75
4. Instrument Mez: Priority 60

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100
2. Purge (AtlasOF_Purge): Priority 100
3. Instant Stun on Pursuer: Priority 95
4. Shield Block / Evade: Priority 85

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant stun and thrust peel)
2. Primary Assist Target: Priority 90 (For thrust DPS at 85% mob HP)
3. Enemy Caster / Healer in RvR: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline / Flank
- **Target Range**: Melee contact (during 85% burn phase)
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Instant Instrument Stun: Priority 95
4. Thrust Weapon Styles: Priority 90
5. `AtlasOF_SecondWind`: Priority 80

### 12. Resource Management
- **Endurance**: Conserve 25% endurance for emergency sprint and stun styles.

### 13. What the AI SHOULD Do
- Peel loose break-off adds immediately during pet pulls with instant stun and thrust styles.
- Engage the called target with full melee styles at the 85% health threshold.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Cast Instant Stun -> Thrust Styles.
- **Condition: Mob Pack HP <= 85%**: Select Primary Assist Target -> Execute Thrust Styles.

### 16. Fallback Behavior
- **Low Endurance (< 15%)**: Activate `AtlasOF_SecondWind`, pause high-cost styles, rely on auto-attacks and shouts.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20]
3. `AtlasOF_SecondWind` (Rank 1) - 5 pts [Total: 25]
4. `AtlasOF_AugDex` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 35]
5. `AtlasOF_AugQui` (Rank 1-2) - 1 + 3 = 4 pts [Total: 39]
6. `AtlasOF_AugStr` (Rank 1-2) - 1 + 3 = 4 pts [Total: 43]
7. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
8. `AtlasOF_AugCon` (Rank 1-2) - 3 pts [Total: 50]
