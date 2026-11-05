---
schema_version: 1
class_name: "Mentalist"
class_id: 42
realm: "Hibernia"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Mentalist (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Light`: High-damage Energy direct damage (DD), single-target Stun, Heal-over-Time (HoT - Divine Restoration), and baseline single-target heals.
  - `Mana`: Area-of-Effect Damage-Over-Time (AoE DoT), Group Power Regeneration buffs, Power Transfers (mana battery), and Heat/Energy DD.
  - `Mentalism`: Area-of-Effect Root, single-target Root, Area Confusion/Charm, and Mentalism DD.
- **Base Abilities**: Sprint, Quick Cast.
- **Hibernia's Psionic Master**: The versatile caster uniting heavy energy burst, stun control, power replenishment, area decay, and continuous HoT sustain.

---

## Specialization 1: Light Mentalist
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Light"
  min_point_ratio: 0.50
  spec_type_aliases: ["LightMenta"]
```

### 1. Intended Role
Energy DD Nuker, Single-Target Stunner, and HoT Provider.

### 2. Intended Role Summary
The Light Mentalist is Hibernia's premier single-target psionic burst and sustain caster. It specializes in high-yield Energy direct damage, long-range single-target Stuns, baseline triage healing, and maintaining continuous Heal-over-Time (HoT) spells on tanks and pets.

### 3. Combat Philosophy
Lethal burst coordination and continuous health regeneration. The Light Mentalist locks down enemy targets with stuns, unloads high-speed Energy DDs, and keeps allies buffered with HoTs.

### 4. Mode: RvR
- **Stun Setup**: Open on high-value enemy casters, healers, or melee peelers with single-target Stun.
- **Energy Burst**: Assist Hibernia's assist train with spec Energy DD nukes.
- **HoT Buffering**: Maintain HoT on frontline fighters taking fire.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Stun & Burn**: Land Stun -> `Quick Cast` + Energy DD -> Finish with follow-up nukes while HoT restores self health.

### 6a. Mode: PvE Standard
- **Assist DPS**: Assist main tank on designated target.
- **HoT Sustain**: Keep HoT running on main tank.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (HoT Provider & Sniper Role)
- **Role During Pet Pulling**: The Mentalist is **NOT the puller**. It acts as stationary HoT support, stun peeler, and sniper.
- **The Core Pet HoT Rule (CRITICAL MECHANIC)**:
  - **Heal-over-Time Spells from a Mentalist ARE explicitly allowed and MUST always stay running on the pet!**
  - Prior to each pull, cast your spec **Heal-over-Time (HoT)** buff on the pet. This HoT sustains the pet without pulling transit aggro.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold fire while mobs are in transit to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, cast **Single Stun** or **Root** on that loose add immediately to protect the group.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - Direct heals: The Mentalist **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Use the **best smallest heal** (`HealEfficient` / baseline heal) to minimize healing aggro. Escalate to a bigger heal only on massive spike damage.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Trigger**:
  - Once mobs reach **85% health**, unleash spec Energy DD nukes on the designated primary kill target.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Shannon Estuary, Connacht, Bog of Cullen). Open fields permit full execution of long-range Light Direct Damage and stun kiting.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Dungeons (Coruscating Mine, Muire Tomb, Treibh Caillte) have narrow hallways where cloth casters cannot maintain distance. Any physical hit causes 100% cast interrupts, making solo dungeon grinding lethal. **Only enter dungeons when grouped with a tank and healer**.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Light Stun + 2 Light DD nukes finish target with zero health lost.
  - **Secondary (Yellow)**: Standard 1-on-1 solo baseline. Requires full mana pool.
  - **Restricted (Orange)**: Engage only with QuickCast and full mana pool.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units in clear open field.
  - 2. Opener: Cast Light Stun from range to lock target in place.
  - 3. Nuke: Cast heavy Spec Light DD nuke into stunned target.
  - 4. Secondary: Cast second Light DD as stun breaks.
  - 5. Finisher: Rapid baseline DD or instant HoT if health dips.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Safety Check**: Clear 1000-unit perimeter before sitting.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - QuickCast Light Stun / baseline Root -> Sprint retreat to safe zone.

### 7. Offensive Priorities
1. Single-Target Stun: Priority 95 (Apply to loose adds or enemy casters)
2. Spec Energy Direct Damage: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Baseline Light DD: Priority 50 (Finishing low-health targets)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Heal-over-Time (HoT) on Pet & Allies: Priority 100 (Keep running continuously)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Single Stun on Attacker: Priority 90 (Stun melee pursuer)
4. Mastery of Concentration: Priority 85 (Power through interrupts)
5. Secondary Heal on Ally: Priority 80 (When ally HP < 40%)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For Single Stun / Root)
2. Enemy Caster / Healer in RvR: Priority 90 (For Stun and Energy DD)
3. Primary Assist Target: Priority 80 (For Energy DD at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. Spec Heal-over-Time (HoT): Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Single-Target Stun: Priority 95
4. Spec Energy DD: Priority 85
5. `AtlasOF_MCL`: Priority 80 (Power recovery)

### 12. Resource Management
- **Mana Floor**: 20%
- **HoT Efficiency**: Refresh HoT only when 1–2 ticks remain.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Keep Heal-over-Time (HoT) active on the pet 100% of the time during pet pulls.
- Stun loose break-off adds during pet pulling to protect healers.
- Follow the Main Assist's target calls at the 85% health threshold.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let HoT expire on the tank or pet.
- Never cast offensive spells on mobs while the pet puller is bringing them to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Loose Add threatening healer**: Select Add -> Cast Single Stun.
- **Condition: Ally HP < 40%**: Select Ally -> Cast Baseline Heal.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Cast Spec Energy DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease offensive nuking, conserve mana exclusively for HoT upkeep and emergency stuns.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Spell crit chance)
5. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_WildHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 42] (HoT crits)
8. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 46]
9. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 50]

---

## Specialization 2: Mana Mentalist
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Mana"
  min_point_ratio: 0.50
  spec_type_aliases: ["ManaMenta"]
```

### 1. Intended Role
AoE DoT Specialist, Power Battery, and Mana Transfer Provider.

### 2. Intended Role Summary
The Mana Mentalist is Hibernia's premier mana sustain and wide-area damage specialist. It specializes in spreading high-damage Area-of-Effect Damage-Over-Time (AoE DoT) spells across clustered enemies, maintaining pulsing group Power Regeneration buffs, and directly transferring power to supply friendly healers and casters.

### 3. Combat Philosophy
Group resource sustainability paired with wide-area attrition. The Mana Mentalist ensures Hibernia's support casters never run out of mana while blanketing enemy formations in stacking AoE DoTs that disrupt casting, drain health, and break stealth across massive areas.

### 4. Mode: RvR
- **AoE DoT Spread**: Blanket enemy caster and healer lines with AoE DoT to cause constant cast pushback and force enemy healers into panic triage.
- **Power Battery**: Continuously monitor friendly Druid, Bard, and Eldritch mana pools. Transfer power whenever an ally drops below 35% mana.
- **Heat/Energy DD Support**: Focus single-target DD on called targets once AoE DoTs are ticking.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **DoT Kiting**: Apply AoE/single DoT, use baseline root to maintain distance, and drain opponent while transferring power if paired with a partner.

### 6a. Mode: PvE Standard
- **Multi-Mob AoE DoT**: Blanket multi-mob pulls in spec AoE DoTs once tanks establish aggro.
- **Power Transfer**: Top off the party healer's mana during prolonged dungeon encounters.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (AoE DoT & Battery Role)
- **Role During Pet Pulling**: The Mana Mentalist is **NOT the puller**. It acts as stationary power battery, peeler, and AoE DoT finisher.
- **Power Regeneration Buff**: Maintain group Power Regeneration buff active at all times.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold fire and do NOT cast DoTs while mobs are in transit to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, cast **Baseline Root** or single DD to assist tanks in peeling it immediately.
- **Power Battery Support**:
  - During the pull, monitor the pet puller and group healers; transfer power if their mana dips below 50%.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - If needed for emergency healing, wait until mobs lose 5% health, then cast the smallest heal (`HealEfficient`).
- **THE 85% MOB HEALTH AoE DoT TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Mana Mentalist unleashes **AoE DoTs** into the center of the cluster around the pet to accelerate the group melt!
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized** (Bog of Cullen, Connacht). Open zones afford room for Mana DoT stacks and Mana DD kiting, backed by pulsing mana regen.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO**. Narrow corridors trap the Mana Mentalist against walls and into linked packs. Join a group with a tank and healer for dungeons.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills; pulsing mana regen keeps downtime near zero.
  - **Secondary (Yellow)**: Standard target. Stack Mana DoT and kite.
  - **Restricted (Orange)**: Only engage with full mana pool.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Maintain self-pulsing Mana Regeneration buff.
  - 2. Opener: Cast Spec Mana DoT on target from 1500 units.
  - 3. Kiting: Cast baseline Root / Snare; channel Mana DD as mob crawls forward.
  - 4. Finisher: Fast Mana DD to finish target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%. Pulsing mana regen makes recovery very fast.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Root -> Sprint retreat.

### 7. Offensive Priorities
1. AoE Damage-over-Time: Priority 95 (Cast into pet cluster at 85% mob HP or open on RvR clusters)
2. Spec Heat/Energy Direct Damage: Priority 80 (Single target nuke on called target)
3. Baseline Root: Priority 75 (Peel loose adds)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Power Transfer to Healer: Priority 90 (When friendly Druid/Bard mana < 30%)
3. Mastery of Concentration: Priority 85 (Power through interrupts to cast AoE DoT or heal)
4. Baseline Heal on Ally: Priority 75 (Emergency triage)

### 9. Target Priorities
1. Clustered Mob Pack around Pet (at 85% HP): Priority 100 (For AoE DoT)
2. Friendly Group Healer (for Power Transfer): Priority 95
3. Loose Break-off Add: Priority 90 (For Root / peel)
4. Primary Assist Target: Priority 75

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1300–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Power Transfer to Healer: Priority 95
3. AoE Damage-over-Time: Priority 90 (At 85% mob health)
4. `AtlasOF_MCL`: Priority 85 (Restore power)
5. `AtlasOF_MasteryOfConcentration`: Priority 80

### 12. Resource Management
- **Power Reserve Floor**: 25% (Reserve power for mana transfers to healers)
- **Transfer Rule**: Do not transfer mana to allies if self mana is below 35%.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Maintain continuous group Power Regeneration buff.
- Transfer power to friendly group healers when their mana drops below 35%.
- Unleash AoE DoTs into the cluster at 85% mob health during pet pulls.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast AoE DoTs while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.
- Never transfer mana when self is critically low on power.

### 15. Target Selection Matrix
- **Condition: Friendly Healer Mana < 30% & Self Mana > 50%**: Select Healer -> Cast Power Transfer.
- **Condition: Mob Cluster at Pet & HP <= 85%**: Target Center Mob -> Cast AoE DoT.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Baseline Root.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, stop offensive casting, reserve remaining power exclusively for emergency power transfers.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfMagery` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (DoT damage boost)
5. `AtlasOF_WildPower` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 48] (High mana regen)
8. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 49]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Mentalism Mentalist
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Mentalism"
  min_point_ratio: 0.50
  spec_type_aliases: ["MentaMenta"]
```

### 1. Intended Role
AoE Root Controller, Area Confusion Specialist, and Mind DD Disruptor.

### 2. Intended Role Summary
The Mentalism Mentalist is Hibernia's specialized crowd-control and battlefield disruption caster. It specializes in locking down enemy melee formations with Area-of-Effect Roots, breaking enemy caster coordination with Area Confusion, and delivering high-impact Mentalism direct damage.

### 3. Combat Philosophy
Battlefield containment and mass confusion. The Mentalism Mentalist immobilizes incoming threats with AoE Roots, sows chaos across enemy ranks with confusion spells, and snipes priority targets with mind DDs while allies execute the main engagement.

### 4. Mode: RvR
- **AoE Root Control**: Cast maximum-range AoE Root on charging enemy melee trains to peel them off the Hibernian caster line.
- **Area Confusion**: Cast Area Confusion spells into enemy caster groups to interrupt and break spell targets.
- **Mind DD Burst**: Focus single-target Mentalism DD on the called assist target.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Root & Nuke**: Pin opponent with AoE/single Root, step out of melee reach, and burn them down with Mentalism DD.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Root on roaming adds that aggro during combat.
- **Assist DPS**: Burn down primary target with Mentalism DD.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (Root Controller & Peeler Role)
- **Role During Pet Pulling**: The Mentalism Mentalist is **NOT the puller**. It acts as stationary root controller and burst support.
- **Mobs Stay on the Pet**:
  - Let the pet absorb the pull and let the pulsing damage shield establish baseline threat.
  - Strictly hold fire and do NOT cast roots or confusion while mobs are in transit to camp.
- **Break-Off Add Protection (PRIMARY DUTY)**:
  - If any mob breaks off from the pet toward the group, cast **Single Root** or **AoE Root** on that loose add immediately to protect group members.
- **The 85% Mob Health Trigger**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Mentalism Mentalist casts spec **Mentalism DD** into the designated primary kill target within the cluster.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Prioritized (Unless Charmed Mob is used as Tank)**.
  - **Dungeon Suitability**: **SOLO EMBARGO / PROHIBITED SOLO (Except Entrance Charm Soloing)**. Standard soloing in tight corridors is prohibited due to high interrupt risk. However, Mentalism spec provides **Charm Animal/Monster** capability; if a suitable Yellow/Blue beast is available at a dungeon entrance, the Mentalist may charm it to act as an expendable front-line door-block. Otherwise, stay outdoors unless grouped.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Charmed pet or Mentalism DD eliminates mob easily.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with Charmed pet holding threat.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure Charmed pet is active and buffed, or setup at 1500 range.
  - 2. Opener: Send Charmed pet or cast Mentalism DD pull.
  - 3. Support: Cast Mentalism DoTs / Debuffs while pet tanks.
  - 4. Finisher: Mentalism DD execution.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 25%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - QuickCast baseline Mez -> Sprint retreat to safety.

### 7. Offensive Priorities
1. AoE / Single Root: Priority 95 (Lock down loose adds or charging enemy melee)
2. Area Confusion: Priority 85 (Disrupt enemy caster lines in RvR)
3. Spec Mentalism Direct Damage: Priority 80 (Nuke called target at 85% mob HP)
4. Melee Staff Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Root on Attacker: Priority 95 (Peel melee attacker immediately)
3. Mastery of Concentration: Priority 90 (Power through interrupts to cast AoE root)
4. Baseline Heal on Ally: Priority 75 (Emergency spot triage)

### 9. Target Priorities
1. Loose Break-off Add: Priority 100 (For instant Root peel)
2. Charging Enemy Melee Train in RvR: Priority 95 (For AoE Root)
3. Primary Assist Target: Priority 75 (For DD at 85% mob HP)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. AoE / Single Root: Priority 95
3. `AtlasOF_MasteryOfConcentration`: Priority 90
4. Spec Mentalism DD: Priority 80
5. `AtlasOF_MCL`: Priority 75

### 12. Resource Management
- **Mana Floor**: 20% (Reserve power for emergency roots and peels)
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Root incoming enemy melee trains in RvR with AoE Root.
- Peel loose break-off adds immediately during pet pulling with roots.
- Cast Mentalism DD on the called target once mobs reach 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never root targets currently taking heavy melee damage (breaks root).
- Never cast roots or nukes while the pet is still in transit pulling mobs to camp.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Root.
- **Condition: Enemy Melee Train Charging**: Select Center -> Cast AoE Root.
- **Condition: Mob Pack HP <= 85%**: Select Primary Target -> Cast Mentalism DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cease offensive DD, reserve power exclusively for emergency single roots and peels.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfTheArt` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 30] (Cast speed boost)
5. `AtlasOF_WildPower` (Rank 1-2) - 1 + 3 = 4 pts [Total: 34]
6. `AtlasOF_MasteryOfMagery` (Rank 1-2) - 1 + 3 = 4 pts [Total: 38]
7. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 48]
8. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 49]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]
