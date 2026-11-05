---
schema_version: 1
class_name: "Warden"
class_id: 46
realm: "Hibernia"
primary_stats: ["Empathy", "Strength"]
default_personality: "Supportive"
---

# Class: Warden (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Scale (absorb 27%), Reinforced, Leather, Cloth.
- **Weapon Slots**: One-Handed Blades or Blunt (Right Hand) + Medium Shield (Left Hand).
- **Core Specialization Lines**:
  - `Nurture`: Pulsing Bladeturn (PBT - pulses every few seconds to absorb the next melee hit for all nearby group members), Group Damage Add buffs, baseline and spec stat buffs (Str/Con, Dex/Qui, Self-AF, Resists).
  - `Regrowth`: Baseline single heals, spec heals, combat resurrections, cures (Poison, Disease).
  - `Blades` / `Blunt`: Weapon combat styles providing positional stuns, snares, bleeds, and attack speed debuffs.
  - `Shield`: Shield blocking, Shield Stun styles (Slam), and Engage.
- **Base Abilities**: Sprint, Evade 1, Shield Block, Pulsing Bladeturn.
- **Hibernia's Protective Bulwark**: The hybrid protector uniting melee durability, pulsing bladeturn defense, group damage enhancement, and secondary healing.

---

## Specialization 1: Nurture Warden
- **role**: Buffer
- **default_personality**: Defensive

```yaml
match_specs:
  primary_line: "Nurture"
  min_point_ratio: 0.50
  spec_type_aliases: ["NurtureWarden"]
```

### 1. Intended Role
Pulsing Bladeturn Anchor, Group Buffer, Peeler Tank, and Secondary Healer.

### 2. Intended Role Summary
The Nurture Warden is Hibernia's premier protective hybrid. It specializes in maintaining continuous Pulsing Bladeturn (PBT) to nullify incoming enemy melee attacks across the entire group, buffing melee allies with high-potency Damage Adds, peeling attackers off casters with shield styles and roots, and assisting primary healers with triage.

### 3. Combat Philosophy
Defensive fortification and backline protection. The Nurture Warden anchors near the caster line, pulsing Bladeturn to shield allies from burst damage. When enemy assassins or tanks breach the formation, the Warden steps in with shield blocks, slam stuns, and roots, then sustains allies with secondary heals.

### 4. Mode: RvR
- **Pulsing Bladeturn (PBT)**: Maintain 100% continuous uptime of PBT. Nullifying the opening stealth strikes and melee styles of enemy trains is the Warden's paramount duty.
- **Backline Peeling**: Stand within 400 units of friendly Bards and Druids. Intercept incoming enemy melee with Slam (Shield Stun) and root.
- **Damage Add Maintenance**: Keep spec Damage Add running on friendly melee fighters.
- **Secondary Healing**: Spot-heal critical allies when primary healers are crowd-controlled or interrupted.

### 5. Mode: PvP
- **PBT Trading**: Leverage PBT and shield block to absorb enemy physical burst while chipping down opponents with weapon styles and baseline DD.
- **Self-Sustain**: Interleave heals between weapon swings.

### 6a. Mode: PvE Standard
- **PBT Uptime**: Ensure PBT is pulsing at all times during combat.
- **Peel & Taunt**: Peeling loose monsters off casters using shield stun and melee styles.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (PBT Anchor & Peeler Role)
- **Role During Pet Pulling**: The Warden is **NOT the puller**. It acts as stationary PBT protection, peeler, and secondary healer.
- **Continuous Pulsing Bladeturn (PBT) Discipline**:
  - The Warden **must keep Pulsing Bladeturn running at all times**. PBT is extraordinarily valuable during pet pulling because it continuously absorbs melee hits directed at the pet and party members!
- **Damage Add Upkeep**: Keep Damage Add active on all melee allies.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield deal continuous damage.
  - Do NOT taunt or melee the main pack while it is anchored to the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets group members, the Warden immediately steps forward, lands **Shield Stun (Slam)** or melee styles, and secures that loose mob to protect the group.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Warden **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Use the **best smallest heal** (`HealEfficient` / baseline heal) to minimize healing aggro. Escalate to a bigger heal only on massive spike damage.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Trigger**:
  - At ~85% mob health, engage the designated primary kill target with melee styles while maintaining PBT.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **PREMIER SOLO HYBRID SURVIVOR**. Wardens are among the absolute best solo classes in DAoC. Reinforced Scale armor, Large Shield block, self-pulsing Bludgeon/Melee Shield (PBT - Pulsing Bubble absorbs periodic melee hits), health regeneration chants, resistance buffs, and self-heals allow the Warden to grind continuously in dungeon corridors (Coruscating Mine, Muire Tomb, Treibh Caillte). The pulsing PBT bubble eliminates incoming hits, preventing damage and interrupts.
  - **Corridor Pull Isolation**: Pull single mobs with throwing weapon or body proximity into safe corridor corners; dispatch safely with Blades/Blunt and Shield styles.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop continuous grinding with virtually zero health loss.
  - **Secondary (Yellow)**: Standard 1-on-1 baseline target. PBT bubble absorbs mob attacks easily.
  - **Restricted (Orange)**: Fully viable; self-PBT and self-heals counter higher mob damage.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Maintain self-Nurture PBT bubble, Health Regen chant, and Spec AF/Resist buffs.
  - 2. Opener: Body pull single mob into safe corridor.
  - 3. Shield Contact: Block incoming strike -> execute block-reaction Shield Stun (Slam).
  - 4. Flank Combo: Step to mob's side during stun -> execute Blades/Blunt style combo.
  - 5. Self-Heal & Finish: Cast minor heal if needed; execute with heavy weapon finisher.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Wardens rarely drop below 50% HP, but sit when Mana < 20% or Endurance < 20%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Land Shield Slam on add -> cast instant baseline Root -> activate Sprint -> retreat to cleared zone.

### 7. Offensive Priorities
1. Shield Stun (Slam): Priority 95 (Apply to loose adds or enemy melee peelers)
2. Melee Style Chain: Priority 80 (Execute on called target at 85% mob HP)
3. Damage Add Buff on Allies: Priority 90 (Keep active on all melee fighters)
4. Baseline Nature DD: Priority 30 (Finishing low-health runners)

### 8. Defensive Priorities
1. Pulsing Bladeturn (PBT): Priority 100 (Maintain 100% continuous uptime)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Mastery of Concentration: Priority 90 (Power through interrupts for emergency triage)
4. Secondary Heal on Ally: Priority 85 (When ally HP < 45%)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For shield stun and melee peel)
2. Friendly Caster / Healer under attack: Priority 95 (For guard and peel)
3. Primary Assist Target: Priority 75 (For melee DPS at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Midline (Between frontline tanks and backline casters)
- **Target Range**: 300–600 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. Pulsing Bladeturn: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Shield Stun (Slam): Priority 95
4. Damage Add Buff: Priority 90
5. `AtlasOF_MasteryOfConcentration`: Priority 85

### 12. Resource Management
- **Mana Floor**: 25% (PBT consumes pulsing power; reserve mana for heals and PBT maintenance)
- **Endurance**: Conserved for shield stuns and peeling styles.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Keep Pulsing Bladeturn running 100% of the time during combat.
- Peel break-off adds with shield stun during pet pulls.
- Assist primary healers with spot heals when allies drop below 45% HP.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Pulsing Bladeturn drop during combat.
- Never charge ahead and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Slam (Shield Stun) -> Engage Melee.
- **Condition: Ally HP < 45%**: Select Ally -> Cast Secondary Heal.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Execute Melee Styles.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease direct damage casting and secondary heals, reserve remaining power exclusively for Pulsing Bladeturn upkeep.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfBlocking` (Rank 1-2) - 1 + 3 = 4 pts [Total: 24]
5. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 28]
6. `AtlasOF_WildHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 32]
7. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 36]
8. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 40]
9. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 44]
10. `AtlasOF_AugStr` (Rank 1-2) - 1 + 3 = 4 pts [Total: 48]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 49]
12. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Battle Warden
- **role**: MeleeDPS
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Blades"
  min_point_ratio: 0.40
  spec_type_aliases: ["BattleWarden"]
```

### 1. Intended Role
Frontline Hybrid Fighter, PBT Provider, and Melee Striker.

### 2. Intended Role Summary
The Battle Warden maximizes weapon style damage (Blades/Blunt) while keeping Pulsing Bladeturn active, serving as a frontline damage dealer with high defensive survivability and utility stuns.

### 3. Combat Philosophy
Offensive melee pressure fortified by continuous PBT. The Battle Warden fights directly alongside Hibernia's primary tanks, executing high-damage weapon styles while providing PBT protection to the entire frontline melee cluster.

### 4. Mode: RvR
- **Frontline Assist DPS**: Stick to the Main Assist's target and execute weapon styles (Blades/Blunt) to apply damage and slows.
- **PBT Uptime**: Maintain continuous Pulsing Bladeturn to protect frontline allies from opposing physical style bursts.
- **Peel Stuns**: Use Slam (Shield Stun) to peel enemy assist-train peelers.
- **Positioning**: Frontline / Midline (in melee contact with called target).

### 5. Mode: PvP
- **Melee Brawling**: Leverage Scale armor, shield block, and PBT to out-trade physical opponents in 1v1 and small-scale combat.

### 6a. Mode: PvE Standard
- **Frontline Melee**: Engage the primary target with style rotations while pulsing PBT.

### 6b. Mode: PvE Pet Pulling (Melee Peeler & Striker Role)
- **Role During Pet Pulling**: The Battle Warden is **NOT the puller**. It acts as stationary PBT protection, add peeler, and melee striker.
- **Continuous Pulsing Bladeturn (PBT)**: Maintain PBT active at all times during the pull and combat.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Do NOT charge out to meet or taunt the incoming mob pack.
- **Loose Mob Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward group members, the Battle Warden steps in immediately, executes **Slam (Shield Stun)** or positional melee styles, and holds that loose add.
- **THE 85% MOB HEALTH MELEE TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Battle Warden engages the designated primary kill target within the cluster with full melee styles!
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT**.
  - **Dungeon Suitability**: **HIGH MELEE BURST & PBT DEFENSE**. High weapon spec provides heavy style damage and block/parry reactions, cutting down single dungeon mobs quickly in narrow corridors.
  - **Corridor Pull Isolation**: Body pull single mobs to cleared corridor; slam-lock and execute.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Mobs die in 3 to 4 weapon styles.
  - **Secondary (Yellow)**: Standard baseline target. Block reactions trigger heavy counters.
  - **Restricted (Orange)**: Viable; PBT bubble mitigates incoming hits.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Body pull target into hallway.
  - 2. Opener: Shield Slam (stun).
  - 3. Combo: High-damage weapon style chain during stun.
  - 4. Finisher: Heavy execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Endurance < 20% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Slam stun -> Sprint away.

### 7. Offensive Priorities
1. Melee Weapon Styles: Priority 95 (Execute on called target at 85% mob HP)
2. Shield Stun (Slam): Priority 90 (Apply to loose adds or enemy peelers)
3. Damage Add Buff on Allies: Priority 85
4. Baseline Nature DD: Priority 30

### 8. Defensive Priorities
1. Pulsing Bladeturn (PBT): Priority 100 (Maintain 100% continuous uptime)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Shield Block: Priority 90
4. Secondary Heal on Ally: Priority 70 (Emergency triage)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For Slam and melee peel)
2. Primary Assist Target: Priority 90 (For melee DPS at 85% mob HP)
3. Friendly Healer under attack: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline / Frontline
- **Target Range**: Melee contact (during 85% burn phase)
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. Pulsing Bladeturn: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Melee Weapon Styles: Priority 90
4. Shield Stun (Slam): Priority 85
5. `AtlasOF_SecondWind`: Priority 80 (Endurance recovery)

### 12. Resource Management
- **Endurance Reserve**: Reserve 25% endurance for emergency Slam stuns and sprint.
- **Mana Floor**: 15% (Reserve power for PBT upkeep).

### 13. What the AI SHOULD Do
- Maintain continuous Pulsing Bladeturn.
- Intercept and peel loose break-off adds immediately during pet pulls.
- Engage the called target with full melee styles at the 85% health threshold.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Pulsing Bladeturn drop during combat.
- Never charge out and attack mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Slam (Shield Stun) -> Execute Styles.
- **Condition: Mob Pack HP <= 85%**: Select Primary Assist Target -> Execute Melee Weapon Styles.

### 16. Fallback Behavior
- **Low Endurance (< 15%)**: Activate `AtlasOF_SecondWind`, pause high-cost styles, rely on auto-attacks and PBT.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfBlocking` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 15]
3. `AtlasOF_MasteryOfParrying` (Rank 1-2) - 1 + 3 = 4 pts [Total: 19]
4. `AtlasOF_AugStr` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 29]
5. `AtlasOF_SecondWind` (Rank 1) - 5 pts [Total: 34]
6. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_AugDex` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42]
8. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 47]
9. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 48]
10. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 2 pts [Total: 50]

---

## Specialization 3: Regrowth Warden
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Regrowth"
  min_point_ratio: 0.50
  spec_type_aliases: ["RegrowthWarden"]
```

### 1. Intended Role
Healer Hybrid Anchor, PBT Sustainer, and Triage Specialist.

### 2. Intended Role Summary
The Regrowth Warden pairs high-tier Regrowth healing (48 Regrowth) with high Nurture Pulsing Bladeturn (45 Nurture), acting as a resilient secondary group healing engine protected by physical shielding.

### 3. Combat Philosophy
Life support protected by physical shielding. The Regrowth Warden focuses primarily on ally survival, cycling major heals and group heals while maintaining PBT to shield the entire party from incoming melee damage.

### 4. Mode: RvR
- **Primary Group Triage**: Prioritize critical allies with spec heals; cure disease and poison rapidly.
- **PBT Defense**: Maintain continuous PBT to shield friendly casters from melee interruptions.
- **Positioning**: 1350 units back.

### 5. Mode: PvP
- **Self-Sustain**: Use major heals while PBT absorbs physical attacks.

### 6a. Mode: PvE Standard
- **Dedicated Healing**: Manage party triage while keeping PBT active.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (Healer & PBT Role)
- **Role During Pet Pulling**: The Regrowth Warden is **NOT the puller**. It acts as stationary PBT sustainer and smart pet healer.
- **Continuous Pulsing Bladeturn (PBT)**: Keep PBT active 100% of the time.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold heals mid-pull while the pet is returning to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, tanks attack it immediately. The Regrowth Warden spot-heals allies engaging that loose add.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Regrowth Warden **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - **Smart Healing (Best Smallest Heal)**: Cast the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet takes massive spike damage, cast a bigger spec heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, maintain PBT and group heal triage on party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **UNBREAKABLE HEALING SUSTAIN**. High Regrowth spec ensures the Warden can fully restore health instantly while maintaining solid scale and shield defense in dungeon rooms.
  - **Corridor Pull Isolation**: Pull single mobs to corridor; sustain through combat.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady grinding with zero risk of death.
  - **Secondary (Yellow)**: Standard target. Self-heals ensure victory.
  - **Restricted (Orange)**: Manageable; monitor mana pool during self-heals.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pull mob into hallway.
  - 2. Engage with weapon style chain.
  - 3. Cast spec direct heal if Health drops < 60%.
  - 4. Weapon finisher.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Instant self-heal / Shield Slam -> Sprint retreat.

### 7. Offensive Priorities
1. Baseline Nature DD: Priority 30 (Finishing low-health runners)
2. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Pulsing Bladeturn (PBT): Priority 100 (Keep active continuously)
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
- **Target Range**: 1300–1450 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. Pulsing Bladeturn: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. `AtlasOF_MasteryOfConcentration`: Priority 95
4. Spec Major Heal (`HealBig`): Priority 90
5. `HealEfficient` / Minor Heal: Priority 85
6. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Mana Floor**: 25% (PBT + heals drain power; reserve mana for emergency heals)
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Keep Pulsing Bladeturn running continuously.
- Heal the pet using smallest heal after mobs lose ~5% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Pulsing Bladeturn drop during combat.
- Never heal the pet mid-pull before mobs reach camp and register 5% health loss.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Ally HP < 35%**: Select Ally -> Cast `HealBig`.
- **Condition: Pet HP < 75% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.
- **Condition: 3+ Allies HP < 70%**: Cast Group Heal.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cast only `HealEfficient` on critical allies, reserve remaining power for PBT upkeep.

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
