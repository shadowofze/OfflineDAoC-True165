---
schema_version: 1
class_name: "Nightshade"
class_id: 49
realm: "Hibernia"
primary_stats: ["Dexterity", "Quickness", "Intelligence"]
default_personality: "Aggressive"
---

# Class: Nightshade (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Leather.
- **Weapon Slots**: Dual Wield via Celtic Dual (Blades or Piercing in main hand and offhand).
- **Core Specialization Lines**:
  - `Critical Strike`: Stealth assassination mastery line, unlocking lethal stealth-opening strikes (Perforate Artery, Dragonfang, Shadow's Edge), high-tier stuns, bleeds, and critical executioners.
  - `Envenom`: Lethal weapon-coating poisons including Damage-over-Time (DoT) poisons, stat-debuffing poisons, and movement-snaring poisons.
  - `Celtic Dual`: Twin-blade melee style chains delivering offhand double-strikes, stuns, and bleeds.
  - `Weapon Lines` (`Piercing`, `Blades`): Base weapon specialization dictating damage type and baseline style openers.
  - `Stealth` & `Magic`: Concealed movement and baseline magical direct damage spells, instant DD casts, and self-stealth enhancements.
- **Base Abilities**: Sprint, Stealth, Evade, Vanish.
- **Hibernia's Magical Assassin**: Weaving dark fae magic with lethal venom and twin blades, the Nightshade strikes from the shadows with catastrophic Perforate Artery ambushes, finishing targets with magical blasts and piercing combos.

---

## Specialization 1: Perforate Artery Assassin Nightshade
- **role**: StealthMeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Critical_Strike"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Stealth Ambush Assassin, Perforate Artery Opener, Poison Debuffer, High-Burst Finisher, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Perforate Artery Assassin Nightshade is Hibernia's premier shadow executioner. Combining 44 Critical Strike with 39 Piercing, 37 Envenom, and 37 Stealth, it stalks priority enemy casters and support in stealth, unleashes a devastating Perforate Artery ambush from behind, applies crippling venoms, and weaves magical DD spells into critical style chains to delete targets before they can react.

### 3. Combat Philosophy
Lethal stealth ambush and hybrid magical execution. The Nightshade stalks isolated support targets, opens with maximum-damage stealth styles, applies lethal poisons, and weaves magical direct damage into melee combos.

### 4. Mode: RvR
- **Stealth Ambush**: Stalk enemy casters and healers from stealth; open from the rear with Perforate Artery -> Dragonfang chain -> Envenom poisons -> Magical DD finisher.
- **Target Elimination**: Delete high-value support targets; use `Vanish` if overwhelmed.
- **Positioning**: Stealth backline infiltration.

### 5. Mode: PvP
- **Ambush & Burst**: Stealth rear opener (Perforate Artery) -> Apply poison DoT/debuff -> Chain into Critical Strike finishers -> Instant magical DD.

### 6a. Mode: PvE Standard
- **Flanking Melee DPS**: Position behind mobs to execute positional style chains; apply venom to elite targets.

### 6b. Mode: PvE Pet Pulling (Venomous Peeler Role)
- **Role During Pet Pulling**: The Nightshade is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO AMBUSHING MOBS DURING TRANSIT**:
  - **Do NOT attack or poison mobs while the pet is in transit to camp!**
  - The mobs **must stay on the pet** so the pulsing damage shield deals continuous retribution damage.
  - Attacking mobs in transit breaks pull geometry and pulls aggro off the pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Nightshade steps in immediately**:
  - Intercept the loose add and burst it down with venomous style chains and magical DD!
  - Protect the backline from harm.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Nightshade steps into melee:
  - Maneuver behind the designated primary focus target in the cluster.
  - Unleash heavy **Critical Strike / Piercing style chains** to eliminate targets one by one!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT FOR ISOLATED TARGETING**. Stealth allows the Nightshade to navigate through crowded dungeon rooms (Coruscating Mine, Muire Tomb, Treibh Caillte) and slip past hostile multi-mob spawns undetected. The Nightshade stalks solitary Blue/Yellow mobs, opens with stealth stuns (Perforate Artery), applies envenomed debuffs, and bursts the target down in seconds with Celtic Dual styles and Shadow Magic DD before stealthing again.
  - **Corridor Pull Isolation**: Never engage a mob linked to a cluster. Infiltrate to the rear of the room or locate solitary corridor patrols. Open from stealth behind the mob.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Perforate Artery removes 60%+ HP; mob dies during initial stun duration before landing a single hit.
  - **Secondary (Yellow)**: Standard 1-on-1 assassination target. PA opener + venom DoTs ensure rapid kill.
  - **Restricted (Orange)**: Only engage with Vanish / Purge ready and full endurance.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; high evade/miss rates break opener and cause death.
- **Pull & Engagement Sequence**:
  - 1. Enter Stealth; apply lethal and stat-debuff venoms to dual piercing blades.
  - 2. Stalk target; maneuver directly into the rear arc while in stealth.
  - 3. Opener: Execute Perforate Artery from stealth (heavy damage + stun).
  - 4. Follow-up: Immediately execute Backstab II during stun duration.
  - 5. Dual Burst & Magic: Chain Celtic Dual styles; weave instant Shadow Magic DD during weapon delay.
  - 6. Reset: Re-enter Stealth immediately post-combat.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest in Stealth in a safe corner when Health < 50% or Endurance < 25%.
  - **Safety Check**: Verify stealth detection radius before resting.
  - **Resume Threshold**: Stand and re-apply poisons when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add links or opener fails: Trigger Vanish RA or side-stun add -> cast instant Shadow Magic root -> Sprint away into shadows -> enter Stealth.

### 7. Offensive Priorities
1. Critical Strike / Venom Burst on Loose Break-Off Add: Priority 100
2. Positional Rear Style Chain on Focus Target (at 85% HP): Priority 90
3. Perforate Artery Opener from Stealth (RvR): Priority 85
4. Envenom Poison Application: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Vanish (AtlasOF_Vanish): Priority 95 (Emergency stealth escape)
3. Advanced Evade Stance: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Melee Burst)
2. Primary Focus Target (at 85% HP): Priority 90 (For Critical Strike DPS)
3. Enemy Light Armor Caster / Support (RvR): Priority 85

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Directly behind target (rear flank)
- **Pull Phase**: Stationary at camp anchor; never charge out.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_Vanish`: Priority 95 (Emergency combat drop)
3. Critical Strike Weapon Styles: Priority 90 (At 85% mob health)
4. Envenom Poisons: Priority 85
5. `AtlasOF_MasteryOfPain`: Priority 80

### 12. Resource Management
- **Poison Supplies**: Ensure weapon poisons are pre-applied before engagements.
- **Endurance & Mana**: Pace style chains and weave magical DDs.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Pre-apply Envenom poisons to weapons.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never attack mobs while they are in transit to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never attack targets from the front when rear positioning is accessible.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Rear -> Execute Critical Strike Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, enter stealth, fall back toward Druid, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Vanish` (Rank 1) - 10 pts [Total: 15] (Essential stealth disengage)
3. `AtlasOF_MasteryOfStealth` (Rank 1) - 1 pt [Total: 16]
4. `AtlasOF_MasteryOfStealth` (Rank 2) - 3 pts [Total: 19]
5. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 20]
6. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 23]
7. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 29] (Style damage)
8. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 30]
9. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 33]
10. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 39] (Melee critical chance)
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 40]
12. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 43]
13. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 44]
14. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 47]
15. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 48]
16. `AtlasOF_LongWind` (Rank 1) - 2 pts [Total: 50]

---

## Specialization 2: Celtic Dual Venom Blender Nightshade
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Celtic_Dual"
  min_point_ratio: 0.50
  spec_type_aliases: ["DualWield"]
```

### 1. Intended Role
Dual-Blade Venomous Blender, High-Evade Skirmisher, Sustained Poison Striker, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Celtic Dual Venom Blender Nightshade trades stealth opening burst for open-combat twin-blade ferocity. Combining 50 Celtic Dual with 39 base weapon and 34 Envenom, it continually applies debilitating poisons through rapid Celtic Dual style chains, dodging counter-attacks with Advanced Evade.

### 3. Combat Philosophy
Continuous poison application and agile dual-blade assault. The Blender Nightshade maneuvers around targets, overwhelms them with twin strikes, and stacks crippling poisons.

### 4. Mode: RvR
- **Melee Assist Striker**: Charge with Hibernian assist train; execute Celtic Dual style chains on priority targets; poison enemy casters and support.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Flank & Poison**: Close distance -> Apply poison debuffs -> Positional Celtic Dual style chain -> Overwhelm target with swing speed.

### 6a. Mode: PvE Standard
- **Flanking DPS**: Assist tank on focus target; poison elite mobs.

### 6b. Mode: PvE Pet Pulling (Blender Peeler Role)
- **Role During Pet Pulling**: The Blender Nightshade is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with Celtic Dual style chains!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with Celtic Dual styles.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH MELEE DPS SURVIVABILITY**. High dual-wield swing speed, Envenom DoTs, and evade-reaction styles allow rapid single-mob grinding in dungeon hallways.
  - **Corridor Pull Isolation**: Ambush solitary targets from stealth; flank and blend.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills (<6 seconds) with zero downtime.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Manageable with full weapon venoms active.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Stealth behind solitary target with poisoned blades.
  - 2. Opener: Stealth stun / opening style.
  - 3. Dual Style Combo: Execute Celtic Dual bleed and flank chain.
  - 4. Evade Reaction: Deflect mob strike and trigger evade-reaction riposte.
  - 5. Re-stealth post-kill.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Stun style -> Sprint -> Stealth vanish.

### 7. Offensive Priorities
1. Celtic Dual Style Burst on Loose Break-Off Add: Priority 100
2. Celtic Dual Style Chain on Focus Target (at 85% HP): Priority 90
3. Envenom Poison Application: Priority 85
4. Positional Flank / Rear Style: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Vanish (AtlasOF_Vanish): Priority 95
3. Evade Defense Stance: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_Vanish`: Priority 95
3. Celtic Dual Weapon Styles: Priority 90 (At 85% mob health)
4. Envenom Poisons: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Endurance**: Pace Celtic Dual style usage and rely on group Bard for endurance recovery.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never neglect weapon poison applications.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Celtic Dual Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Celtic Dual Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, fall back toward Druid, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Vanish` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
4. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
5. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Celtic Dual style damage)
6. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
7. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
8. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
12. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
13. `AtlasOF_MasteryOfStealth` (Rank 1) - 1 pt [Total: 44]
14. `AtlasOF_MasteryOfStealth` (Rank 2) - 3 pts [Total: 47]
15. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 48]
16. `AtlasOF_LongWind` (Rank 1) - 2 pts [Total: 50]

---

## Specialization 3: Shadow Strike Caster-Assassin Nightshade
- **role**: HybridDamage
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Critical_Strike"
  secondary_line: "Nightshade_Magic"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Magical Assassin, Critical Strike Ambusher, Baseline Spell Nuker, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Shadow Strike Caster-Assassin Nightshade blends stealth critical strikes with magical ranged direct damage. By training 39 Critical Strike, 39 base weapon, 37 Envenom, and 23 Celtic Dual, it opens with stealth ambushes, weaves baseline magical DDs into style recoveries, and picks off fleeing targets with spells.

### 3. Combat Philosophy
Hybrid physical-magical assassination. The Shadow Strike Nightshade ambushes from stealth and weaves magical direct damage spells between blade strikes to overwhelm target defenses.

### 4. Mode: RvR
- **Hybrid Ambush**: Open from stealth on priority casters; weave magical DD spells into Critical Strike combos; finish fleeing enemies with ranged spells.
- **Positioning**: Stealth flank / Infiltration.

### 5. Mode: PvP
- **Spell & Strike**: Stealth opener -> Poison debuff -> Critical Strike chain -> Quick Cast magical DD finisher.

### 6a. Mode: PvE Standard
- **Hybrid DPS**: Position behind mobs to execute positional style chains; weave spells between swings.

### 6b. Mode: PvE Pet Pulling (Hybrid Peeler Role)
- **Role During Pet Pulling**: The Shadow Strike Nightshade is **NOT the puller**. It acts as **stationary melee/spell burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with Critical Strike styles and magical DD!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with style chains and spells.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **MAGIC-MELEE AMBUSH SPECIALIST**. Opens with long-range Shadow Magic DD or stealth rear strike, weaving magic nukes into melee combat in corridors.
  - **Corridor Pull Isolation**: Magic pull solitary targets into cleared corridor; execute with blades.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid assassination.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Engage only with full cooldowns.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Stealth behind target.
  - 2. Opener: Stealth rear strike or point-blank Shadow DD.
  - 3. Style Chain: Celtic Dual style combo.
  - 4. Finisher: Instant Shadow DD to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Vanish RA -> Sprint away.

### 7. Offensive Priorities
1. Critical Strike / Spell Burst on Loose Break-Off Add: Priority 100
2. Critical Strike Style Chain on Focus Target (at 85% HP): Priority 90
3. Baseline Magical DD on Cooldown: Priority 85
4. Envenom Poison Application: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Vanish (AtlasOF_Vanish): Priority 95
3. Evade Defense Stance: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 80 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Directly behind target
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_Vanish`: Priority 95
3. Critical Strike Weapon Styles: Priority 90 (At 85% mob health)
4. Magical Direct Damage: Priority 85
5. `AtlasOF_MasteryOfPain`: Priority 80

### 12. Resource Management
- **Mana & Endurance**: Balance power usage between spells and style chains.

### 13. What the AI SHOULD Do
- Hold attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Weave magical DD spells between blade strikes.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never exhaust mana completely on spells.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Melee Style -> Cast Magical DD -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Rear -> Execute Style Chain -> Weave Spell.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, fall back toward Druid, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Vanish` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
4. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
5. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25]
6. `AtlasOF_MasteryOfPain` (Rank 1) - 1 pt [Total: 26]
7. `AtlasOF_MasteryOfPain` (Rank 2) - 3 pts [Total: 29]
8. `AtlasOF_MasteryOfPain` (Rank 3) - 6 pts [Total: 35] (Melee critical chance)
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 36]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 39]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 40]
12. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 43]
13. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 44]
14. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 47] (Spell damage)
15. `AtlasOF_FirstAid` (Rank 1) - 1 pt [Total: 48]
16. `AtlasOF_LongWind` (Rank 1) - 2 pts [Total: 50]
