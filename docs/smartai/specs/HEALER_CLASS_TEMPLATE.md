---
schema_version: 1
class_name: "Healer"
class_id: 26
realm: "Midgard"
primary_stats: ["Piety", "Constitution"]
default_personality: "Supportive"
---

# Class: Healer (Midgard)

## Shared Class Capabilities
- **Armor Type**: Chain (absorb 27%), Leather, Cloth.
- **Weapon Slots**: One-Handed Hammer (Right Hand) + Small Round Shield (Left Hand), or Two-Handed Hammer.
- **Core Specialization Lines**:
  - `Pacification`: Midgard's premier crowd control: Area-of-Effect Mesmerize (AoE Mez), single-target Mesmerize, Area-of-Effect Stun (AoE Stun), single-target Stun, Amnesia (wipe attack list), and CC clears.
  - `Mending`: Baseline and spec single-target heals, group heals, spread heals, combat resurrections, cures (Poison, Disease, Blindness, Nearsight).
  - `Augmentation`: Celerity (drastic combat melee attack speed acceleration buff), baseline/spec stat buffs (Str/Con, Dex/Qui, Acuity, AF, Resists), Damage Add buffs.
- **Base Abilities**: Sprint, Evade 1, Shield Block.
- **Midgard's Lifeline & Master Controller**: The powerhouse hybrid uniting instantaneous wide-area crowd control, group triage, and combat acceleration.

---

## Specialization 1: Pacification Healer
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Pacification"
  min_point_ratio: 0.50
  spec_type_aliases: ["PacHealer"]
```

### 1. Intended Role
Primary Crowd Controller, Instant Area Stunner, and Triage Anchor.

### 2. Intended Role Summary
The Pacification Healer is Midgard's ultimate tactical crowd control authority. It specializes in shutting down entire enemy battlegroups with maximum-range Area-of-Effect Mesmerize, halting melee assist trains with AoE Stuns, clearing memory with Amnesia, and providing vital secondary heal triage.

### 3. Combat Philosophy
Decisive group neutralization and protection. The Pac Healer dictates combat flow by locking down enemy forces before they reach the Midgard lines, preventing counter-attacks with AoE Stuns, and sustaining allies with baseline heals.

### 4. Mode: RvR
- **Opening CC**: Cast maximum-range AoE Mesmerize on clustered enemy groups.
- **AoE Stun Lockdown**: Land AoE Stun on charging enemy melee trains to leave them vulnerable to Midgard's assist train.
- **Amnesia**: Cycle Amnesia against enemy casters to disrupt continuous cast bars.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Stun & Mez Control**: Cycle single-target Stun and Mez to create distance, reset fights, or secure kills.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Mez or single Mez on any extra monster groups that aggro onto the party.
- **Secondary Healing**: Assist the primary healer when multiple allies take damage.

### 6b. Mode: PvE Pet Pulling (Crowd Control & Triage Role)
- **Role During Pet Pulling**: The Healer is **NOT the puller**. It acts as stationary crowd control, peeler, and spot-heal support.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire and CC while mobs are in transit to camp.
- **Break-Off Add Protection**:
  - If any loose mob breaks off from the pet toward the group, cast **Single Mez** or **Single Stun** on that loose add immediately to protect the group.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Healer **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Use the **best smallest heal** (`HealEfficient` / baseline heal) to minimize healing aggro. Escalate to a bigger heal only on massive spike damage.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, when AoE casters begin blasting the cluster, maintain group heal triage on party members.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Preferred**.
  - **Dungeon Suitability**: **CAUTIOUS / NOT RECOMMENDED SOLO**. Excellent crowd control (Mez, Stun, Root) allows neutralizing adds, but low direct damage makes solo killing slow. In tight dungeon corridors, roaming adds can interrupt casts. Best to solo in open fields or entrance camps. **Join a group with tanks and casters for dungeon expeditions**.
- **Target Con Selection**:
  - **Optimal (Blue / Low Blue)**: Mez / Stun locks target while baseline smite or melee chips health away.
  - **Secondary (Yellow)**: Only engage with full mana pool.
  - **Restricted (Orange)**: Strictly avoid solo.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units in open terrain.
  - 2. Opener: Long-range Single-Target Stun / Mez on target.
  - 3. Attack: Cast baseline DD or engage in melee with Hammer and Shield.
  - 4. Self-Heal: Cast HealEfficient if Health drops < 65%.
  - 5. Finisher: Hammer style or baseline DD.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit after every 1-2 kills when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 90%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Instant AoE Mez / Stun -> Sprint retreat to safety.

### 7. Offensive Priorities
1. AoE Mesmerize: Priority 100 (Open RvR / multi-add encounters)
2. AoE / Single Stun: Priority 95 (Lock down charging melee or loose adds)
3. Amnesia: Priority 70 (Disrupt enemy caster lines)
4. Melee Hammer Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible CC / heals)
3. Emergency Spot Heal: Priority 85 (When ally HP < 45%)
4. Cure Disease / Poison: Priority 80

### 9. Target Priorities
1. Uncontrolled Enemy Group in RvR: Priority 100 (For AoE Mez)
2. Loose Break-off Add: Priority 95 (For Single Mez / Stun)
3. Friendly Ally under attack: Priority 90 (For CC peel / heal)
4. Lowest Effective HP% Ally: Priority 80 (For secondary heal)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. AoE Mesmerize: Priority 90
4. AoE / Single Stun: Priority 85
5. `AtlasOF_MCL`: Priority 80 (When mana < 20%)

### 12. Resource Management
- **Mana Floor**: 25% (Reserve power for emergency CC and critical triage)

### 13. What the AI SHOULD Do
- Open RvR battles with AoE Mesmerize.
- Peel break-off adds with single Mez or Stun during pet pulls.
- Assist with spot heals when party members drop below 45% HP.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never mez targets taking damage from the pulsing damage shield (breaks mez).
- Never run away backwards out of camp to seek max range.
- Never engage in melee combat; stay focused on CC and heals.

### 15. Target Selection Matrix
- **Condition: Uncontrolled Multiple Enemies in RvR**: Select Center of Cluster -> Cast AoE Mez.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Mez / Stun.
- **Condition: Ally HP < 45%**: Select Ally -> Cast Baseline Heal.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, conserve power exclusively for single-target CC and emergency heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfTheArt` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Cast speed boost)
5. `AtlasOF_WildHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42]
8. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 46]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 50]

---

## Specialization 2: Mending Healer
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Mending"
  min_point_ratio: 0.50
  spec_type_aliases: ["MendHealer"]
```

### 1. Intended Role
Dedicated Primary Group Healer, Group Cleanse Anchor, and Triage Specialist.

### 2. Intended Role Summary
The Mending Healer is Midgard's dedicated life-support anchor. It specializes in maximizing major single-target heals, group heals, spread heals, combat resurrections, and cleansing diseases and poisons to keep all 8 group members alive under maximum enemy pressure.

### 3. Combat Philosophy
Uncompromising ally survival through rapid heal triage. The Mending Healer prioritizes effective ally health percentages, proactively cleanses healing-debuffing diseases, maintains backline safety, and deploys `Mastery of Concentration` to power through enemy interrupts.

### 4. Mode: RvR
- **Primary Group Triage**: Prioritize frontline tanks (Warriors, Berserkers) and friendly crowd controllers under assist train focus.
- **Disease & Poison Cleansing**: Rapidly cast `Cure Disease` on friendly tanks and healers to remove the 50% healing received penalty.
- **Interrupt Protection**: Trigger `Mastery of Concentration` when collapsed upon by enemy stealthers or melee trains to ensure uninterrupted group triage.
- **Positioning**: 1350–1500 units back; spread away from Shamans and Runemasters to avoid clustered enemy CC.

### 5. Mode: PvP
- **Pillar Play & Kiting**: Break enemy caster lines of sight between cast cycles.
- **Counter-Interrupt**: Use the fastest heal available when pressured by enemy pets or fast melee attacks (Healers have no Quick Cast).

### 6a. Mode: PvE Standard
- **Aggro Management**: Hold major heals for 1.5 seconds on pull until the frontline tank lands initial taunt to prevent pulling healing aggro.
- **Pacing**: Monitor group mana. Call for rest when party mana drops below 35%.
- **Emergency Triage**: Execute group heal if 3+ party members drop below 70% health.

### 6b. Mode: PvE Pet Pulling (Dedicated Triage Anchor Role)
- **Role During Pet Pulling**: The Mending Healer is **NOT the puller**. It acts as stationary triage anchor and smart pet healer.
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused entirely on the pet while the pet owner's pulsing damage shield deals continuous damage.
  - Do NOT cast heals mid-pull while the pet is returning to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, tanks engage it immediately. The Mending Healer spot-heals allies engaging that loose add.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Mending Healer **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - **Smart Healing (Best Smallest Heal)**: Cast the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet takes massive spike damage (heavy multi-mob spike or rapid health drop), cast a bigger spec heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, when AoE casters begin blasting the cluster, maintain stationary group triage on party members.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Preferred**.
  - **Dungeon Suitability**: **POOR SOLO / GROUP ESSENTIAL**. Pure healing spec provides zero offensive enhancements. Soloing is exceptionally slow and mana-intensive. Group with tanks and DPS for all leveling.
- **Target Con Selection**:
  - **Optimal (Blue / Low Blue)**: Only con soloable without mana starvation.
  - **Secondary (Yellow)**: Avoid solo.
  - **Restricted (Orange)**: Prohibited solo.
  - **Prohibited (Red / Purple)**: Prohibited solo.
- **Pull & Engagement Sequence**:
  - 1. Baseline DD pull from range.
  - 2. Melee strike with One-Handed Hammer.
  - 3. Self-heal continuously.
  - 4. Finisher: Melee strike.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 35%.
  - **Resume Threshold**: Stand when Mana >= 90%.
- **Emergency Escape Protocol**:
  - Baseline Mez add -> Sprint retreat.

### 7. Offensive Priorities
1. Baseline Hammer Attack: Priority 0 (Strictly disabled)
2. Direct Damage: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 95 (15 seconds uninterruptible healing)
3. Cure Disease: Priority 90 (Cleanse tanks immediately)
4. Heal Triage (Lowest HP% Ally): Priority 90
5. Self-Cast Buffs: Priority 80

### 9. Target Priorities
1. Lowest Effective HP% Ally: Priority 100
2. Friendly Tank afflicted with Disease: Priority 95
3. Group Pet (at 5% mob HP loss threshold): Priority 85
4. Friendly Crowd Controller under attack: Priority 90

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Spec Major Heal (`HealBig`): Priority 90 (Target HP < 50%)
4. `HealEfficient` / Minor Heal: Priority 85 (Target HP 50%–85%)
5. Group Heal: Priority 80 (3+ allies < 70% HP)
6. `AtlasOF_MCL`: Priority 75 (When mana < 20%)

### 12. Resource Management
- **Mana Reserve Floor**: 25% (Reserve power for emergency group heals and resurrections)
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient`
  - Target HP 35%–70%: `HealBig`
  - Target HP < 35%: `Mastery of Concentration` + `HealBig`
  - 3+ members < 70%: `HealGroup`

### 13. What the AI SHOULD Do
- Heal the pet using smallest heal after mobs lose ~5% health.
- Cleanse Disease on tanks immediately to restore 100% healing effectiveness.
- Deploy `Mastery of Concentration` under melee pressure.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast heals on the pet mid-pull before the mobs arrive at camp and lose 5% health.
- Never run away backwards out of camp to seek max range.
- Never engage in melee combat or cast offensive spells.

### 15. Target Selection Matrix
- **Condition: Ally HP < 35%**: Select Ally -> Cast `HealBig` (Escalate to MoC if interrupted).
- **Condition: Ally Afflicted with Disease**: Select Ally -> Cast `Cure Disease`.
- **Condition: 3+ Allies HP < 70%**: Cast `HealGroup`.
- **Condition: Pet HP < 75% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cast only `HealEfficient` on critical allies, announce low power.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildHealing` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Heal crits)
5. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42]
8. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 46]
9. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 50]

---

## Specialization 3: Augmentation Healer
- **role**: Buffer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Augmentation"
  min_point_ratio: 0.50
  spec_type_aliases: ["AugHealer"]
```

### 1. Intended Role
Celerity Buffer, Group Enhancer, and Secondary Healer.

### 2. Intended Role Summary
The Augmentation Healer is Midgard's premier melee combat accelerator. It specializes in maximizing Celerity (dramatic combat melee attack speed acceleration), maintaining high-tier baseline and spec stat buffs (Str/Con, Dex/Qui, AF, Resists), Damage Add buffs, and providing secondary heal triage.

### 3. Combat Philosophy
Combat acceleration and group enhancement. The Aug Healer empowers Midgard's heavy frontline (Warriors, Berserkers, Savages) to attack at blinding speeds while ensuring the group has maximum physical and magical defense.

### 4. Mode: RvR
- **Celerity Maintenance**: Keep Celerity active on friendly melee trains to maximize style pressure and burst.
- **Damage Add & Buffs**: Ensure all 8 group members have spec AF, Str/Con, and Damage Add active.
- **Secondary Healing**: Assist the primary healer with baseline heals on backline squishies and frontline tanks.
- **Positioning**: Midline / Backline (1100–1350 units).

### 5. Mode: PvP
- **Buff Preservation**: Keep buffs and Celerity running on partner fighters to win DPS trades.

### 6a. Mode: PvE Standard
- **Celerity Uptime**: Cast Celerity on pull so tanks and melee DPS burn down targets rapidly.
- **Secondary Heals**: Interleave heals when primary healer is resting or under pressure.

### 6b. Mode: PvE Pet Pulling (Buffer & Secondary Healer Role)
- **Role During Pet Pulling**: The Aug Healer is **NOT the puller**. It acts as stationary buffer, secondary healer, and peeler support.
- **Buff & Celerity Discipline**:
  - Keep Celerity and Damage Add active on group melee fighters.
  - Pre-cast stat buffs and AF buffs on the pet prior to the pull.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Do NOT cast spells or heals on mobs in transit.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, tanks attack it immediately with Celerity-boosted melee. The Aug Healer spot-heals allies engaging that loose add.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Aug Healer **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Cast the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while minimizing healing aggro.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, when AoE casters begin blasting the cluster, maintain Celerity and secondary heal triage on party members.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Moderately Viable**.
  - **Dungeon Suitability**: **MODERATE SOLO MELEE SURVIVABILITY**. Self-spec buffs (Celerity, Haste, Str/Con, Dex/Quick) provide strong melee swing speed and defense in chain armor. Can solo single Blue/Yellow mobs in dungeon entrance hallways.
  - **Corridor Pull Isolation**: Baseline DD pull into hallway; melee down with Celerity/Haste.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast melee kills under Celerity buff.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Avoid solo.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-buff self with Augmentation buffs and Celerity.
  - 2. Baseline DD pull from doorway.
  - 3. Melee Combat: Rapid Hammer strikes under Celerity buff.
  - 4. Self-Heal: Minor baseline heal if Health < 60%.
  - 5. Finisher: Hammer style execution.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Baseline Mez add -> Sprint retreat.

### 7. Offensive Priorities
1. Celerity Buff on Melee Allies: Priority 100 (Keep active on all frontline fighters)
2. Damage Add Buff on Melee Allies: Priority 95
3. Baseline Hammer Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Group Base/Spec Buffs (AF, Str/Con, Resists): Priority 95
3. Mastery of Concentration: Priority 90
4. Secondary Heal on Ally: Priority 85 (When ally HP < 50%)

### 9. Target Priorities
1. Friendly Melee Allies (for Celerity & Damage Add): Priority 100
2. Lowest Effective HP% Ally: Priority 90 (For secondary heal)
3. Group Pet (at 5% mob HP loss threshold): Priority 80

### 10. Positioning Preferences
- **Anchor**: Midline / Backline
- **Target Range**: 1100–1350 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Celerity Buff: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Damage Add Buff: Priority 95
4. Baseline `HealEfficient` / `HealBig`: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Mana Floor**: 25% (Reserve power for Celerity recasting and emergency heals)

### 13. What the AI SHOULD Do
- Keep Celerity active on melee fighters continuously.
- Re-apply buffs promptly after resurrections.
- Heal the pet using smallest heal after mobs lose ~5% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let Celerity drop during melee engagements.
- Never cast heals on the pet mid-pull before the mobs arrive at camp and lose 5% health.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Ally Celerity Expired**: Select Melee Ally -> Cast Celerity.
- **Condition: Ally HP < 50%**: Select Lowest Ally HP% -> Cast Baseline Heal.
- **Condition: Pet HP < 70% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, conserve power exclusively for Celerity upkeep on the main tank.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30]
5. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_Toughness` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42]
8. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 46]
9. `AtlasOF_AugCon` (Rank 1-2) - 1 + 3 = 4 pts [Total: 50]
