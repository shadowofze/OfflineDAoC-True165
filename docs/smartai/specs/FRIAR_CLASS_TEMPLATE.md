---
schema_version: 1
class_name: "Friar"
class_id: 10
realm: "Albion"
primary_stats: ["Piety", "Dexterity"]
default_personality: "Supportive"
---

# Class: Friar (Albion)

## Shared Class Capabilities
- **Armor Type**: Leather (absorb 10%).
- **Weapon Slots**: Two-Handed Quarterstaff.
- **Core Specialization Lines**:
  - `Enhancement`: Heal-over-Time (HoT - Holy Aura / Divine Regeneration), Armor Factor (AF), Strength/Constitution, Dexterity/Quickness, elemental/magic resistance buffs, and self-haste buffs.
  - `Rejuvenation`: Baseline and spec single-target heals, group heals, combat resurrections, cures (Poison, Disease).
  - `Staff`: Two-handed quarterstaff weapon styles (Counterstrike, Friar's Friend, Banish, Excommunicate) providing evade-reactive stuns, side stuns, attack speed debuffs, and high physical burst.
  - `Parry`: Weapon parry defense.
- **Base Abilities**: Sprint, Evade 4, Quarterstaff Mastery.
- **Albion's Agile Support Striker**: The versatile monk hybrid uniting rapid HoT sustain, high-tier group buffs, evasive staff combat, and secondary/primary healing.

---

## Specialization 1: Enhancement Friar
- **role**: Buffer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Enhancement"
  min_point_ratio: 0.50
  spec_type_aliases: ["EnhanceFriar"]
```

### 1. Intended Role
Heal-over-Time (HoT) Provider, Premier Group Buffer, and Staff Skirmisher.

### 2. Intended Role Summary
The Enhancement Friar is Albion's premier health-regeneration and defensive enhancement specialist. It specializes in maintaining continuous Heal-over-Time (HoT) buffs on tanks and pets, maximizing party Armor Factor (AF) and magic resistances, peeling loose enemies with evasive staff stuns, and spot-healing allies.

### 3. Combat Philosophy
Continuous health buffering and agile defense. The Enhancement Friar layers HoTs and buffs before combat, then maneuvers along the frontline and midline to absorb attacks through high evade/parry, landing staff stuns on peelers and keeping allies topped off with heals.

### 4. Mode: RvR
- **HoT Buffering**: Maintain spec Heal-over-Time (HoT) on friendly frontline tanks (Armsman, Paladin) and friendly casters taking fire.
- **Buff Maintenance**: Keep spec AF, Str/Con, Dex/Qui, and Resists active on all 8 group members.
- **Evade Peeling**: Intercept enemy stealthers targeting the Cleric line; utilize Evade 4 + Counterstrike (Evade Stun) to lock down attackers.
- **Positioning**: Midline (400â€“600 units from casters).

### 5. Mode: PvP
- **Staff Dueling**: Trade blows with high Evade 4 and Parry; land evade stuns; cycle HoT and baseline heals to outlast opponents.

### 6a. Mode: PvE Standard
- **HoT Tank Support**: Keep HoT ticking on the main tank throughout the fight.
- **Melee Assist**: Flank the primary target with staff styles while maintaining buffs.

### 6b. Mode: PvE Pet Pulling (HoT Anchor & Peeler Role)
- **Role During Pet Pulling**: The Friar is **NOT the puller**. It acts as stationary HoT provider, peeler, and secondary healer.
- **The Core Pet HoT Rule (CRITICAL MECHANIC)**:
  - **Heal-over-Time Spells from a Friar ARE explicitly allowed and MUST always stay running on the pet!**
  - Prior to each pull, cast your spec **Heal-over-Time (HoT)** buff on the pet. This HoT sustains the pet without pulling transit aggro.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pet owner's pulsing damage shield deals continuous damage.
  - Do NOT melee or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, the Friar steps forward immediately, executes **Quarterstaff styles (Evade Stun / Counterstrike)**, and secures that loose mob to protect the group.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Direct heals: The Friar **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Use the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while generating minimal healing aggro. Escalate to a bigger heal only on massive spike damage.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Trigger**:
  - At ~85% mob health, engage the designated primary kill target with quarterstaff styles while keeping HoTs ticking.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **PREMIER SOLO DUNGEON SURVIVOR**. Friars are legendary solo grinders in DAoC. With high staff defense (evade/parry), spec AF buffs, health regeneration chants, endurance reduction buffs, and self-heals, the Friar thrives in confined dungeon corridors (Catacombs, Tomb of Mithra, Tepok's Mine). Zero cast-interrupt vulnerability once engaged in melee, and self-heals can be cast between swings or during staff stuns.
  - **Corridor Pull Isolation**: Pull single mobs with thrown weapon or body proximity into safe corridor corners; dispatch safely with staff styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop continuous grinding with virtually zero downtime.
  - **Secondary (Yellow)**: Standard 1-on-1 baseline target. Friar out-sustains the mob easily.
  - **Restricted (Orange)**: Fully viable; self-buffs and staff evade styles counter higher mob attack power.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Maintain self-spec AF, Str/Con, Dex/Quick, and Health Regen buffs.
  - 2. Opener: Body pull single mob into safe corridor.
  - 3. Staff Combo: Land staff opener -> trigger evade-reaction staff style combo.
  - 4. Staff Stun: Execute Staff side/flank stun or parry stun.
  - 5. Self-Heal & Finish: Cast minor heal if needed; execute with heavy staff finisher.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Friars rarely drop below 50% HP, but sit when Mana < 20% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Land Staff stun on add -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Evade Stun (Counterstrike): Priority 95 (Apply to loose adds or enemy peelers)
2. Quarterstaff Melee Chain: Priority 80 (Execute on called target at 85% mob HP)
3. Baseline Smite DD: Priority 30 (Finishing low-health runners)

### 8. Defensive Priorities
1. Heal-over-Time (HoT) on Pet & Allies: Priority 100 (Keep running continuously)
2. Group Base/Spec Buffs (AF, Str/Con, Resists): Priority 95
3. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
4. Mastery of Concentration: Priority 90 (Power through interrupts for emergency heals)
5. Secondary Heal on Ally: Priority 85 (When ally HP < 45%)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For staff stun and melee peel)
2. Friendly Caster / Healer under attack: Priority 95 (For evade peel)
3. Primary Assist Target: Priority 75 (For staff DPS at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Midline (Between frontline tanks and backline casters)
- **Target Range**: 300â€“600 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. Spec Heal-over-Time (HoT): Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Counterstrike (Evade Stun): Priority 95
4. `AtlasOF_MasteryOfConcentration`: Priority 90
5. `AtlasOF_MCL`: Priority 85 (Power recovery)

### 12. Resource Management
- **Mana Floor**: 20% (HoTs consume steady mana; reserve power for heals and re-buffing)
- **Endurance**: Quarterstaff styles consume low endurance; reserve 20% for sprint and evade styles.

### 13. What the AI SHOULD Do
- Keep Heal-over-Time (HoT) active on the pet 100% of the time during pet pulls.
- Peel break-off adds with quarterstaff styles during pet pulls.
- Assist primary healers with spot heals when allies drop below 45% HP.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let HoT expire on the tank or pet.
- Never charge ahead and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Execute Counterstrike (Evade Stun).
- **Condition: Ally HP < 45%**: Select Ally -> Cast Secondary Heal.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Execute Staff Styles.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease offensive smiting, conserve mana exclusively for maintaining HoTs on critical targets.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 24]
5. `AtlasOF_WildHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 28]
6. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 32]
7. `AtlasOF_AugDex` (Rank 1-2) - 1 + 3 = 4 pts [Total: 36] (Evade boost)
8. `AtlasOF_AugQui` (Rank 1-2) - 1 + 3 = 4 pts [Total: 40]
9. `AtlasOF_AugStr` (Rank 1-2) - 1 + 3 = 4 pts [Total: 44]
10. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 48]
11. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
12. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Staff Friar
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Staff"
  min_point_ratio: 0.50
  spec_type_aliases: ["StaffFriar"]
```

### 1. Intended Role
Frontline Quarterstaff Striker, Evade Duelist, and Stun Executioner.

### 2. Intended Role Summary
The Staff Friar maximizes two-handed quarterstaff styles (50 Staff) for high-impact physical burst, capitalizing on Evade 4 and high Parry to dominate melee skirmishes, lock down targets with evade stuns, and eliminate high-value enemy casters.

### 3. Combat Philosophy
Aggressive evasive dueling and melee execution. The Staff Friar dances through enemy attacks using Evade 4, landing Counterstrike stuns and high-damage follow-ups while maintaining self-buffs.

### 4. Mode: RvR
- **Assist Train Melee**: Stick to the Main Assist's target and execute quarterstaff style sequences.
- **Caster Interruption**: Use high mobility and evade stuns to eliminate enemy backline casters.
- **Positioning**: Frontline / Flank.

### 5. Mode: PvP
- **Evade Dueling**: Exploit Evade 4 and reactive styles to disable opponents in close-quarters combat.

### 6a. Mode: PvE Standard
- **Melee DPS**: Execute staff style sequences on the main target.

### 6b. Mode: PvE Pet Pulling (Melee Peeler & Striker Role)
- **Role During Pet Pulling**: The Staff Friar is **NOT the puller**. It acts as stationary add peeler and melee striker.
- **Hold Fire on Pull**: Strictly wait at camp until the mobs arrive. Do NOT run out to meet the pull.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Do NOT melee or taunt the main pack off the pet.
- **Loose Mob Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward group members, the Staff Friar steps in immediately, lands **Counterstrike (Evade Stun)** or positional staff styles, and holds that loose add.
- **THE 85% MOB HEALTH MELEE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Staff Friar engages the designated primary kill target within the cluster with full quarterstaff styles!
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **HIGH MELEE BURST & EVASION**. Pure Staff spec provides maximum staff style damage, double evasion bonuses, and devastating reactionary counter-strikes in narrow hallways.
  - **Corridor Pull Isolation**: Body pull single mobs to cleared corridor; evade and counter-strike.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 to 4 staff styles.
  - **Secondary (Yellow)**: Standard baseline target. Evade reactions trigger heavy staff counters.
  - **Restricted (Orange)**: Viable; high evasion mitigates incoming hits.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Body pull target into hallway.
  - 2. Opener: Staff anytime opening style.
  - 3. Evade Counter: Evade incoming swing -> execute high-damage evade riposte style.
  - 4. Finisher: Heavy staff execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Staff stun -> Sprint away.

### 7. Offensive Priorities
1. Quarterstaff Melee Styles: Priority 95 (Execute on called target at 85% mob HP)
2. Evade Stun (Counterstrike): Priority 90 (Apply to loose adds or enemy peelers)
3. Baseline Smite DD: Priority 30

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Evade 4 / Parry: Priority 95
3. Self-Cast Buffs: Priority 85
4. Secondary Heal on Ally: Priority 70 (Emergency spot triage)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For staff stun and melee peel)
2. Primary Assist Target: Priority 90 (For staff DPS at 85% mob HP)
3. Enemy Caster / Healer in RvR: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline / Frontline
- **Target Range**: Melee contact (during 85% burn phase)
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Quarterstaff Styles: Priority 95
3. Counterstrike (Evade Stun): Priority 90
4. `AtlasOF_SecondWind`: Priority 80 (Endurance recovery)

### 12. Resource Management
- **Endurance**: Conserve 20% endurance for emergency sprint and evade stuns.

### 13. What the AI SHOULD Do
- Peel loose break-off adds immediately during pet pulls with staff stuns.
- Engage the called target with full melee styles at the 85% health threshold.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never charge out and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Execute Counterstrike (Evade Stun).
- **Condition: Mob Pack HP <= 85%**: Select Primary Assist Target -> Execute Staff Styles.

### 16. Fallback Behavior
- **Low Endurance (< 15%)**: Activate `AtlasOF_SecondWind`, rely on auto-attacks and high evade.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfParrying` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 15]
3. `AtlasOF_AugDex` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 25] (Evade boost)
4. `AtlasOF_AugStr` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 35]
5. `AtlasOF_SecondWind` (Rank 1) - 5 pts [Total: 40]
6. `AtlasOF_AugQui` (Rank 1-2) - 1 + 3 = 4 pts [Total: 44]
7. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 48]
8. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 49]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Rejuvenation Friar
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Rejuvenation"
  min_point_ratio: 0.50
  spec_type_aliases: ["RejuvFriar"]
```

### 1. Intended Role
Dedicated Group Healer Friar, Triage Anchor, and HoT Sustainer.

### 2. Intended Role Summary
The Rejuvenation Friar pairs major single-target and group heals (44 Rejuvenation) with pulsing HoTs and defensive quarterstaff defense, acting as a resilient dedicated group healer.

### 3. Combat Philosophy
Life support protected by evasion. The Rejuvenation Friar focuses primarily on ally survival, cycling major heals and group heals while using high evade and staff parry to survive melee dives.

### 4. Mode: RvR
- **Primary Group Triage**: Prioritize critical allies with spec heals; cure disease and poison rapidly.
- **Interrupt Resistance**: Use `Mastery of Concentration` when collapsed upon to ensure unbroken heals.
- **Positioning**: 1350 units back.

### 5. Mode: PvP
- **Self-Sustain**: Use major heals while Evade 4 absorbs physical attacks.

### 6a. Mode: PvE Standard
- **Dedicated Healing**: Manage party triage while keeping HoTs active.

### 6b. Mode: PvE Pet Pulling (Dedicated Healer & HoT Role)
- **Role During Pet Pulling**: The Rejuvenation Friar is **NOT the puller**. It acts as stationary triage healer and HoT sustainer.
- **The Core Pet HoT Rule (CRITICAL MECHANIC)**:
  - **Heal-over-Time Spells from a Friar ARE explicitly allowed and MUST always stay running on the pet!**
  - Keep spec HoT active on the pet at all times.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold direct heals mid-pull while the pet is returning to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, tanks attack it immediately. The Rejuv Friar spot-heals allies engaging that loose add.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Rejuv Friar **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - **Smart Healing (Best Smallest Heal)**: Cast the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet takes massive spike damage, cast a bigger spec heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, maintain group heal triage on party members.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **UNBREAKABLE HEALING SUSTAIN**. High Rejuvenation spec ensures the Friar can fully restore health instantly while maintaining solid staff defense in dungeon rooms.
  - **Corridor Pull Isolation**: Pull single mobs to corridor; sustain through combat.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding with zero risk of death.
  - **Secondary (Yellow)**: Standard target. Self-heals ensure victory.
  - **Restricted (Orange)**: Manageable; monitor mana pool during self-heals.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Engage with Staff style chain.
  - 3. Cast spec direct heal if Health drops < 60%.
  - 4. Staff finisher.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25%.
  - **Resume Threshold**: Resume at Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Instant self-heal / staff stun -> Sprint retreat.

### 7. Offensive Priorities
1. Baseline Staff Attack: Priority 0 (Strictly disabled)
2. Direct Damage: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Heal-over-Time (HoT) on Pet & Allies: Priority 100 (Keep running continuously)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Mastery of Concentration: Priority 95 (15 seconds uninterruptible healing)
4. Heal Triage (Lowest HP% Ally): Priority 90
5. Cure Disease / Poison: Priority 85

### 9. Target Priorities
1. Lowest Effective HP% Ally: Priority 100
2. Group Pet (at 5% mob HP loss threshold): Priority 85
3. Loose Break-off Add: Priority 80 (For spot-heal on tank)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300â€“1450 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. Spec Heal-over-Time (HoT): Priority 100
2. `AtlasOF_Purge`: Priority 100
3. `AtlasOF_MasteryOfConcentration`: Priority 95
4. Spec Major Heal (`HealBig`): Priority 90
5. `HealEfficient` / Minor Heal: Priority 85
6. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Mana Floor**: 25% (Reserve power for emergency triage and resurrections)

### 13. What the AI SHOULD Do
- Keep HoT active on the pet 100% of the time.
- Heal the pet using smallest heal after mobs lose ~5% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never heal the pet mid-pull before mobs reach camp and register 5% health loss.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Ally HP < 35%**: Select Ally -> Cast `HealBig`.
- **Condition: Pet HP < 75% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.
- **Condition: 3+ Allies HP < 70%**: Cast Group Heal.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cast only `HealEfficient` on critical allies, reserve remaining power for HoTs.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildHealing` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30]
5. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 44]
7. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 48]
8. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 2 pts [Total: 50]
