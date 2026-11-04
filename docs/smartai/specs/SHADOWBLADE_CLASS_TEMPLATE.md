---
schema_version: 1
class_name: "Shadowblade"
class_id: 23
realm: "Midgard"
primary_stats: ["Strength", "Dexterity", "Quickness"]
default_personality: "Aggressive"
---

# Class: Shadowblade (Midgard)

## Shared Class Capabilities
- **Armor Type**: Leather.
- **Weapon Slots**: Dual Wield via Left Axe (Sword, Axe, or Hammer in main hand, Left Axe in offhand); Two-Handed Sword, Axe, Hammer.
- **Core Specialization Lines**:
  - `Critical Strike`: Stealth assassination mastery line, delivering lethal stealth-opening strikes (Perforate Artery, Dragonfang, Shadow's Edge), high-tier stuns, bleeds, and critical executioners.
  - `Envenom`: Deadly weapon poisons including Damage-over-Time (DoT) poisons, stat-debuffing poisons, and movement-snaring poisons.
  - `Left Axe`: Midgard's dual-axe style chains and offhand double-strike mechanics.
  - `Weapon Lines` (`Sword`, `Axe`, `Hammer`): Main hand / two-handed weapon specialization.
  - `Stealth`: Concealed movement, silent stalking, and ambush positioning.
- **Base Abilities**: Sprint, Stealth, Evade, Vanish.
- **Midgard's Shadow Assassin**: Stalking through the frozen mist in complete silence, the Shadowblade ambushes unsuspecting victims from behind, delivering catastrophic critical strikes with dual axes or a massive two-handed great weapon coated in lethal venoms.

---

## Specialization 1: Left Axe Assassin Shadowblade
- **role**: StealthMeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Critical_Strike"
  min_point_ratio: 0.50
  spec_type_aliases: ["LeftAxeShadowblade", "CriticalStrike"]
```

### 1. Intended Role
Dual-Axe Stealth Ambush Assassin, Perforate Artery Opener, Poison Debuffer, High-Burst Finisher, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Left Axe Assassin Shadowblade is Midgard's premier stealth killer. Combining 44 Critical Strike with 39 Left Axe, 36 Envenom, and 35 Stealth, it stalks enemy casters and support in stealth, unleashes a devastating Perforate Artery ambush from behind, applies crippling venoms, and follows up with rapid dual-axe style chains to delete targets before they can escape.

### 3. Combat Philosophy
Lethal stealth ambush and dual-axe execution. The Shadowblade stalks isolated support targets, opens with maximum-damage stealth styles, applies lethal poisons, and carves through opponents with Left Axe combos.

### 4. Mode: RvR
- **Stealth Ambush**: Stalk enemy casters and healers from stealth; open from the rear with Perforate Artery -> Dragonfang chain -> Envenom poisons -> Left Axe double-strikes.
- **Target Elimination**: Delete high-value support targets; use `Vanish` if overwhelmed.
- **Positioning**: Stealth backline infiltration.

### 5. Mode: PvP
- **Ambush & Burst**: Stealth rear opener (Perforate Artery) -> Apply poison DoT/debuff -> Chain into Left Axe finishers.

### 6a. Mode: PvE Standard
- **Flanking Melee DPS**: Position behind mobs to execute positional style chains; apply venom to elite targets.

### 6b. Mode: PvE Pet Pulling (Venomous Peeler Role)
- **Role During Pet Pulling**: The Shadowblade is **NOT the puller and NOT the aggro holder**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Crucial Rule: NO AMBUSHING MOBS DURING TRANSIT**:
  - **Do NOT attack or poison mobs while the pet is in transit to camp!**
  - The mobs **must stay on the pet** so the pulsing damage shield deals continuous retribution damage.
  - Attacking mobs in transit breaks pull geometry and pulls aggro off the pet.
- **Break-Off Add Elimination**:
  - If any mob breaks away from the pet toward the healers, **the Shadowblade steps in immediately**:
  - Intercept the loose add and burst it down with Left Axe style chains and poisons!
  - Protect the backline from harm.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the pet's pulsing damage shield brings the mob cluster down to **85% health**, the Shadowblade steps into melee:
  - Maneuver behind the designated primary focus target in the cluster.
  - Unleash heavy **Critical Strike / Left Axe style chains** to eliminate targets one by one!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor with the group. Do **NOT** run away or chase mobs out into the field.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **EXCELLENT FOR ISOLATED TARGETING**. Stealth allows the Shadowblade to navigate through crowded dungeon rooms (Spindelhalla, Cursed Tomb) and slip past hostile multi-mob spawns undetected. The Shadowblade stalks solitary Blue/Yellow mobs, opens with stealth stuns, applies envenomed debuffs, and bursts the target down in seconds with dual Left Axe styles before stealthing again.
  - **Corridor Pull Isolation**: Never engage a mob linked to a cluster. Infiltrate to the rear of the room or locate solitary corridor patrols. Open from stealth behind the mob.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Stealth opener removes 60%+ HP; mob dies during initial stun duration before landing a single hit.
  - **Secondary (Yellow)**: Standard 1-on-1 assassination target. PA/Backstab opener + venom DoTs ensure rapid kill.
  - **Restricted (Orange)**: Only engage with Vanish / Purge ready and full endurance.
  - **Prohibited (Red / Purple)**: Strictly avoid solo; high evade/miss rates break opener and cause death.
- **Pull & Engagement Sequence**:
  - 1. Enter Stealth; apply lethal and stat-debuff venoms to dual axes.
  - 2. Stalk target; maneuver directly into the rear arc while in stealth.
  - 3. Opener: Execute Stealth rear stun opener (Perforate Artery / Garrote).
  - 4. Left Axe Burst: Immediately follow with Left Axe style chain (Doublestrike) during stun.
  - 5. Dual Burst: Chain dual-axe styles and let envenom DoTs finish target.
  - 6. Reset: Re-enter Stealth immediately post-combat.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest in Stealth in a safe corner when Health < 50% or Endurance < 25%.
  - **Safety Check**: Verify stealth detection radius before resting.
  - **Resume Threshold**: Stand and re-apply poisons when Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - If add links or opener fails: Trigger Vanish RA or side-stun add -> Sprint away into shadows -> enter Stealth.

### 7. Offensive Priorities
1. Left Axe / Venom Burst on Loose Break-Off Add: Priority 100
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
4. Left Axe Weapon Styles: Priority 85
5. Envenom Poisons: Priority 80

### 12. Resource Management
- **Poison Supplies**: Ensure weapon poisons are pre-applied before engagements.
- **Endurance**: Pace Critical Strike chains to avoid endurance exhaustion.

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
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, enter stealth, fall back toward Healer, call for heals.

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

## Specialization 2: Two-Handed Ambush Assassin Shadowblade
- **role**: StealthMeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Critical_Strike"
  min_point_ratio: 0.50
  spec_type_aliases: ["TwoHandedShadowblade", "TwoHanded"]
```

### 1. Intended Role
Two-Handed Stealth Ambush Assassin, 2H Perforate Artery Executioner, Massive Kinetic Burst Striker, and Break-Off Add Destroyer.

### 2. Intended Role Summary
The Two-Handed Ambush Assassin Shadowblade is unique among assassins, wielding a massive two-handed Great Sword or Great Axe to deliver catastrophic Perforate Artery ambushes. By combining 44 Critical Strike with 44 Two-Handed weapon specialization, 38 Envenom, and 38 Stealth, it delivers the highest single-hit stealth ambush damage in Dark Age of Camelot, often one-shotting enemy casters before they realize they are under attack.

### 3. Combat Philosophy
Single-hit annihilation. The 2H Shadowblade stalks targets from stealth, positions behind them, and lands a massive two-handed Perforate Artery strike, instantly destroying or critically crippling the target.

### 4. Mode: RvR
- **2H Stealth Ambush**: Open from stealth on priority enemy cloth casters with a two-handed Perforate Artery -> Dragonfang chain; finish immediately with 2H styles.
- **Positioning**: Stealth backline infiltration.

### 5. Mode: PvP
- **One-Shot Ambush**: Stealth rear opener (2H Perforate Artery) -> 2H follow-up style -> Target eliminated in 2 swings.

### 6a. Mode: PvE Standard
- **Heavy Melee DPS**: Position behind mobs to execute heavy 2H style chains.

### 6b. Mode: PvE Pet Pulling (2H Executioner Peeler Role)
- **Role During Pet Pulling**: The 2H Shadowblade is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with heavy Two-Handed styles!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with heavy 2H style chains.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **HIGH BURST AMBUSH SURVIVABILITY**. Opening with a massive Two-Handed stealth strike delivers enormous burst damage, chopping down isolated dungeon targets in 2 swings.
  - **Corridor Pull Isolation**: Ambush solitary targets from stealth; finish with heavy two-handed styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mob dies in 2 to 3 swings.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Manageable with full weapon venoms active.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Stealth behind solitary target with envenomed two-handed blade/axe.
  - 2. Opener: Stealth rear strike from two-handed weapon.
  - 3. Two-Handed Chain: Execute high-damage two-handed style combo.
  - 4. Finisher: Poison tick + heavy execution style.
  - 5. Re-stealth post-kill.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Health < 50% or Endurance < 20%.
  - **Resume Threshold**: Resume at Health >= 90%, Endurance >= 85%.
- **Emergency Escape Protocol**:
  - Stun style -> Sprint -> Stealth vanish.

### 7. Offensive Priorities
1. Two-Handed Style Burst on Loose Break-Off Add: Priority 100
2. Heavy 2H Style Chain on Focus Target (at 85% HP): Priority 90
3. 2H Perforate Artery Opener from Stealth: Priority 85
4. Envenom Poison Application: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Vanish (AtlasOF_Vanish): Priority 95
3. Parry Defense: Priority 85
4. First Aid: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100
2. Primary Focus Target (at 85% HP): Priority 90
3. Enemy Light Armor Caster / Support: Priority 85 (In RvR)

### 10. Positioning Preferences
- **Anchor**: Camp Center / Melee Flank (adjacent to pet anchor)
- **Combat Angle**: Directly behind target
- **Pull Phase**: Stationary at camp anchor; never leave the camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_Vanish`: Priority 95
3. Two-Handed Critical Strike Styles: Priority 90 (At 85% mob health)
4. Envenom Poisons: Priority 85
5. `AtlasOF_MasteryOfArms`: Priority 80

### 12. Resource Management
- **Endurance**: Heavy 2H styles consume endurance; pace attacks and rely on Skald endurance chants.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers with heavy 2H styles.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never attack mobs while they are in transit to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never attack from the front when rear positioning is accessible.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge 2H Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Move to Rear -> Execute 2H Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, fall back toward Healer, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Vanish` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
4. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
5. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (2H style damage)
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

## Specialization 3: Pure Axe Blender Shadowblade
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Left_Axe"
  min_point_ratio: 0.50
  spec_type_aliases: ["PureAxeBlenderShadowblade", "LeftAxe"]
```

### 1. Intended Role
Dual-Axe Venomous Blender, Sustained Flanker, Poison Striker, and Break-Off Add Interceptor.

### 2. Intended Role Summary
The Pure Axe Blender Shadowblade emphasizes open-melee dual-axe combat over stealth ambushes. By taking Left Axe to 50 alongside 36 Envenom and 36 Stealth, it continuously applies lethal poisons through non-stop double-axe strikes, shredding opponents in sustained melee.

### 3. Combat Philosophy
Continuous dual-axe pressure and poison application. The Blender maneuvers to flanks, overwhelms targets with Left Axe double-strikes, and stacks lethal venoms.

### 4. Mode: RvR
- **Melee Assist Striker**: Charge with Midgard assist train; execute Left Axe style chains on priority targets; poison enemy casters and support.
- **Positioning**: Flank / Melee assist train.

### 5. Mode: PvP
- **Flank & Poison**: Close distance -> Apply poison debuffs -> Positional Left Axe style chain -> Overwhelm target with swing speed.

### 6a. Mode: PvE Standard
- **Flanking DPS**: Assist tank on focus target; poison elite mobs.

### 6b. Mode: PvE Pet Pulling (Blender Peeler Role)
- **Role During Pet Pulling**: The Blender Shadowblade is **NOT the puller**. It acts as **stationary melee burst and break-off mob destroyer**.
- **Hold Position at Camp**: Remain stationed beside the group at camp during the pull.
- **Do NOT Taunt Main Mob Pack**: Let the mobs stay on the pet.
- **Break-Off Add Elimination**: If an add breaks toward healers, intercept it immediately with Left Axe style chains!
- **THE 85% MOB HEALTH TRIGGER**: Once the mob pack hits 85% health at camp, engage the focus target with Left Axe styles.
- **Strict Stationary Discipline**: Stay strictly stationed at camp.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **FAST COMBAT & POISON SUSTAIN**. High dual axe attack speed and venom debuffs allow continuous clearing of solitary hallway mobs.
  - **Corridor Pull Isolation**: Ambush targets from stealth; flank and blend.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Rapid assassination.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Engage only with full cooldowns.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Stealth into room.
  - 2. Opener: Stealth opener on target.
  - 3. Style Chain: Dual axe flank/rear combo.
  - 4. Finisher: Execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Rest in stealth when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Vanish RA -> Sprint away.

### 7. Offensive Priorities
1. Left Axe Style Burst on Loose Break-Off Add: Priority 100
2. Left Axe Style Chain on Focus Target (at 85% HP): Priority 90
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
3. Left Axe Weapon Styles: Priority 90 (At 85% mob health)
4. Envenom Poisons: Priority 85
5. `AtlasOF_Determination`: Priority 80

### 12. Resource Management
- **Endurance**: Pace Left Axe style usage and rely on Skald endurance chants.

### 13. What the AI SHOULD Do
- Hold melee attacks until the mob pack arrives at camp and drops to 85% health.
- Intercept and burst down any loose add that breaks toward group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out of camp to attack mobs while they are traveling to camp.
- Never taunt the primary mob cluster off the puller's pet.
- Never neglect weapon poison applications.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Charge Left Axe Melee Style -> Eliminate Add.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Execute Left Axe Style Chain.

### 16. Fallback Behavior
- **Low Health (< 30%)**: Pop `AtlasOF_Vanish`, fall back toward Healer, call for heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Vanish` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MasteryOfArms` (Rank 1) - 1 pt [Total: 16]
4. `AtlasOF_MasteryOfArms` (Rank 2) - 3 pts [Total: 19]
5. `AtlasOF_MasteryOfArms` (Rank 3) - 6 pts [Total: 25] (Left Axe style damage)
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
