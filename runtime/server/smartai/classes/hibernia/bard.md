---
schema_version: 1
class_name: "Bard"
class_id: 48
realm: "Hibernia"
primary_stats: ["Charisma", "Empathy"]
default_personality: "Supportive"
---

# Class: Bard (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Reinforced Armor (absorb 19%), Leather, Cloth.
- **Weapon Slots**: One-Handed Blades or Blunt (Right Hand) + Small Shield (Left Hand), Instruments (Lute, Flute, Drum, Harp).
- **Core Specialization Lines**:
  - `Music`: Songs of Speed (Speed of the Realm), Songs of Mesmerization (AoE and single-target Mez), Area-of-Effect Roots, Amnesia (memory wipe), Direct Damage Shouts.
  - `Nurture`: Health and Power regeneration songs, spec and baseline stat buffs (Strength/Constitution, Dexterity/Quickness, Acuity), and combat speed buffs.
  - `Regrowth`: Baseline and spec single heals, group heals, combat resurrections, cures (Poison, Disease).
- **Base Abilities**: Sprint, Evade 1, Shield Block, Pulsing Instrument Songs.
- **Hibernia's Group Backbone**: The essential anchor uniting group mobility, wide-area crowd control, power replenishment, and group triage.

---

## Specialization 1: Music Bard
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Music"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Primary Crowd Controller, Speed Driver, and Power Battery.

### 2. Intended Role Summary
The Music Bard is Hibernia's ultimate crowd-control and tactical tempo anchor. It specializes in locking down entire enemy battlegroups with maximum-range Area-of-Effect Mesmerize, pinning melee trains with AoE Roots, maintaining continuous group movement speed via Speed of the Realm, and replenishing group mana pools with Power regeneration songs.

### 3. Combat Philosophy
Total area lockdown and mana sustain. The Music Bard neutralizes opposing forces before they can engage, cycling AoE Mez, AoE Root, and Amnesia to break enemy coordination while maintaining songs that ensure Hibernia never runs dry on power or speed.

### 4. Mode: RvR
- **Opening Crowd Control**: Cast maximum-range AoE Mesmerize on clustered enemy groups.
- **AoE Root Peeling**: Drop AoE Root on charging enemy melee trains to isolate them from their healers.
- **Song Twisting**: Run Speed of the Realm to position; toggle Power and Health songs in combat.
- **SOS Authority**: Activate `AtlasOF_SOS` (Speed of Sound) to secure rapid group engages or flawless tactical disengages.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Mez & Reset**: Land single or AoE Mez, reset distance, apply roots, and assist partner with shouts and secondary heals.

### 6a. Mode: PvE Standard
- **Add Containment**: Immediately cast AoE Mez or single Mez on any extra monster groups that aggro onto the party.
- **Power Regeneration**: Maintain pulsing power song so casters and healers can sustain combat indefinitely.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (Support & Triage Role)
- **Role During Pet Pulling**: The Bard is **NOT the puller**. It acts as stationary song, CC, and spot-heal support.
- **Song Discipline**: Run **Power Regeneration** and **Health Regeneration** songs at camp.
- **Mobs Stay on the Pet**: Let the pet absorb the pull and let the pulsing damage shield establish baseline threat. Do NOT cast Mez or spells on mobs in transit.
- **Break-Off Add Protection**: If any loose mob breaks off from the pet toward the group, cast **Single Mez** or **Root** on that loose add immediately to protect the group.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - If called to assist with pet healing, the Bard **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Cast the **best smallest heal** (`HealEfficient` / baseline heal) to minimize healing aggro. Escalate to bigger heal only on massive spike damage.
- **Group Stays Put (Strict Stationary Discipline)**:
  - The Bard and group remain firmly anchored at camp. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, when AoE casters begin blasting the cluster, maintain power songs and spot-heal party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors EXCELLENT (CHARM PET LEVELING MASTER)**.
  - **Dungeon Suitability**: **SUPREME SOLO PET SURVIVABILITY**. The Bard is one of the strongest solo classes in DAoC because of the **Charm** song. In dungeons (Coruscating Mine, Muire Tomb, Treibh Caillte), the Bard charms a powerful local monster (up to Yellow con), turning it into a disposable front-line tank! The Bard parks the Charmed pet in a corridor bottleneck, sends it to attack dungeon hostiles, and pulses health regen, mana regen, and song buffs from behind while the charmed pet absorbs all melee attacks. Zero interrupt risk for the Bard.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Charmed pet kills target rapidly with zero downtime.
  - **Secondary (Yellow)**: Standard target. Charmed pet holds aggro effortlessly.
  - **Restricted (Orange)**: Viable; assist pet with instant shouts and flank blade styles.
  - **Prohibited (Red / Purple)**: Avoid solo; charmed pet will die quickly against Purple.
- **Pull & Engagement Sequence**:
  - 1. Locate and Charm a local Yellow or High Blue humanoid/monster to serve as your pet tank.
  - 2. Position pet in corridor bottleneck.
  - 3. Opener: Command Charmed pet to attack target mob (/pet attack).
  - 4. Song Support: Pulse Health Regen song / End Regen song on pet.
  - 5. Combat Assist: Fire instant DD shouts or step behind mob to land rear Blade/Blunt styles.
  - 6. If Charmed pet is about to die, break charm and let pet and mob kill each other, or re-charm.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind Charmed pet when Health < 50% or Endurance < 20%. Speed/Health songs accelerate recovery.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant AoE Mez song on adds -> activate Speed Song -> Sprint away at maximum realm speed.

### 7. Offensive Priorities
1. AoE Mesmerize: Priority 100 (Open RvR / multi-add encounters)
2. AoE / Single Root: Priority 90 (Lock down charging melee or break-offs)
3. DD Shouts: Priority 60 (Cast on called target off cooldown)
4. Melee Weapon Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100 (Group disengage / burst mobility)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Mastery of Concentration: Priority 95 (15 seconds uninterruptible CC / heals)
4. Song Twisting (Power / Health): Priority 90 (Keep active continuously)

### 9. Target Priorities
1. Uncontrolled Enemy Group in RvR: Priority 100 (For AoE Mez)
2. Loose Break-off Add: Priority 95 (For Single Mez / Root)
3. Friendly Caster / Healer under attack: Priority 90 (For CC peel)
4. Primary Assist Target: Priority 70 (For DD shout assist)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. `AtlasOF_MasteryOfConcentration`: Priority 95
4. AoE Mesmerize: Priority 90
5. Pulsing Songs (Power/Health/Speed): Priority 85

### 12. Resource Management
- **Mana Floor**: 20% (Songs consume minimal power; reserve mana for CC and emergency heals)
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Open RvR battles with AoE Mesmerize.
- Keep Power song active continuously during combat.
- Peel break-off adds with single Mez or Root during pet pulls.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never mez targets taking damage from the pulsing damage shield (breaks mez).
- Never run away backwards out of camp to seek max range.
- Never let power song drop during prolonged combat.

### 15. Target Selection Matrix
- **Condition: Uncontrolled Multiple Enemies in RvR**: Select Center of Cluster -> Cast AoE Mez.
- **Condition: Add breaks off toward healer**: Select Add -> Cast Single Mez / Root.
- **Condition: Friendly Ally HP < 40%**: Select Ally -> Cast Baseline Heal.

### 16. Fallback Behavior
- **Low Health / Collapsed Upon**: Pop `AtlasOF_SOS`, sprint to backline safety, activate `Mastery of Concentration` if casting under pressure.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20] (Speed of Sound - signature 15 pt active RA)
3. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
4. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 35]
5. `AtlasOF_MasteryOfTheArt` (Rank 1-2) - 1 + 3 = 4 pts [Total: 39] (Cast speed boost)
6. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 43]
7. `AtlasOF_AugAcuity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
8. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 48]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
10. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Regrowth Bard
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Regrowth"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Dedicated Group Healer Bard, Triage Anchor, and Song Sustainer.

### 2. Intended Role Summary
The Regrowth Bard pairs high-tier single-target and group heals with pulsing power/health songs and secondary crowd control, providing a dual healing engine alongside Druids.

### 3. Combat Philosophy
Life preservation through heal triage and mana cycling. The Regrowth Bard balances the dual demands of constant song upkeep and urgent healing, using `Mastery of Concentration` and `Speed of Sound` to ensure allies remain fully sustained through intense enemy focus.

### 4. Mode: RvR
- **Primary Group Triage**: Prioritize friendly crowd-controllers, main tanks, and light casters under assist-train focus.
- **Disease & Poison Curing**: Rapidly cleanse diseases and poisons off group members.
- **Song Maintenance**: Keep pulsing Health and Power regeneration songs active while cycling heals.
- **Positioning**: 1350–1500 units back.

### 5. Mode: PvP
- **Pillar Kiting**: Maneuver around terrain obstacles while casting heals on self and partner.
- **Emergency CC**: Use baseline Mez or Root to create breathing room.

### 6a. Mode: PvE Standard
- **Aggro Management**: Hold major heals for 1.5 seconds on pull until tank secures aggro.
- **Group Triage**: Cast group heals when 3+ party members drop below 70% health.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (Dedicated Healer & Song Role)
- **Role During Pet Pulling**: The Regrowth Bard is **NOT the puller**. It acts as stationary triage healer and song support.
- **Song Discipline**: Run **Power Regeneration** and **Health Regeneration** songs at camp.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused entirely on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold heals mid-pull while the pet is returning to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, tanks attack it immediately. The Regrowth Bard spot-heals allies engaging that loose add.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Regrowth Bard **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - **Smart Healing (Best Smallest Heal)**: Cast the **best smallest heal** (`HealEfficient` / baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet takes massive spike damage, cast a bigger spec heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, when AoE casters begin blasting the cluster, maintain power songs and group heal triage on party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **UNBREAKABLE HEALING & CHARM SUSTAIN**. Regrowth spec provides powerful direct heals and instant group heals. When combined with a Charmed dungeon pet, the Bard can keep the pet healed indefinitely in narrow corridors.
  - **Corridor Pull Isolation**: Send charmed pet to doorway; heal pet from safe hallway.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Safe, steady pet grinding.
  - **Secondary (Yellow)**: Standard target. Spot-heals keep charmed pet alive.
  - **Restricted (Orange)**: Viable with active pet healing.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Charm dungeon mob.
  - 2. Send pet to attack target.
  - 3. Cast Regrowth direct heal on pet when pet HP < 60%.
  - 4. Pet finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind pet when Mana < 25%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez song -> Speed Song flee.

### 7. Offensive Priorities
1. Baseline Instruments Mez: Priority 70 (Peel loose adds)
2. DD Shouts: Priority 40 (Finishing low-health runners)
3. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100 (Emergency group mobility / disengage)
2. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
3. Mastery of Concentration: Priority 95 (15 seconds uninterruptible healing)
4. Heal Triage (Lowest HP% Ally): Priority 90
5. Song Twisting (Power / Health): Priority 90

### 9. Target Priorities
1. Lowest Effective HP% Ally: Priority 100
2. Group Pet (at 5% mob HP loss threshold): Priority 85
3. Loose Break-off Add: Priority 80 (For baseline Mez/Root peel)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor; never retreat to seek max range.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. `AtlasOF_MasteryOfConcentration`: Priority 95
4. Spec Major Heal (`HealBig`): Priority 90 (Target HP < 50%)
5. `HealEfficient` / Minor Heal: Priority 85 (Target HP 50%–85%)
6. Group Heal: Priority 80 (3+ allies < 70% HP)

### 12. Resource Management
- **Mana Floor**: 20%
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient`
  - Target HP 35%–70%: `HealBig`
  - Target HP < 35%: `Mastery of Concentration` + `HealBig`
  - 3+ members < 70%: `HealGroup`
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Heal the pet using smallest heal after mobs lose ~5% health.
- Keep Power song active at all times.
- Cleanse disease and poison on friendly tanks immediately.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never heal the pet mid-pull before mobs reach camp and register 5% health loss.
- Never run away backwards out of camp to seek max range.
- Never let Power song drop during combat.

### 15. Target Selection Matrix
- **Condition: Ally HP < 35%**: Select Ally -> Cast `HealBig` (Escalate to MoC if interrupted).
- **Condition: Pet HP < 75% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.
- **Condition: 3+ Allies HP < 70%**: Cast `HealGroup`.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, cast only `HealEfficient` on critical allies, rely on Power song regen.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20]
3. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
4. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 35]
5. `AtlasOF_WildHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 39]
6. `AtlasOF_MasteryOfHealing` (Rank 1-2) - 1 + 3 = 4 pts [Total: 43]
7. `AtlasOF_Serenity` (Rank 1-2) - 1 + 3 = 4 pts [Total: 47]
8. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 48]
9. `AtlasOF_AugCon` (Rank 1) - 1 pt [Total: 49]
10. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Nurture Bard
- **role**: Buffer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Nurture"
  min_point_ratio: 0.50
  spec_type_aliases: []
```

### 1. Intended Role
Buffer, Balanced Songster, and Secondary Healer.

### 2. Intended Role Summary
The Nurture Bard maximizes health and power regeneration song values while balancing mid-tier crowd control and heals, providing continuous group sustain.

### 3. Combat Philosophy
Sustained group endurance and buff uptime. The Nurture Bard maximizes song potency to keep allies fighting at peak performance while assisting with secondary triage.

### 4. Mode: RvR
- **Song Optimization**: Maximize Power and Health song tiers to counter enemy attrition.
- **Mid-Tier CC**: Deploy baseline AoE Mez and Roots to slow enemy advances.
- **Positioning**: 1350 units back.

### 5. Mode: PvP
- **Sustained Kiting**: Use songs to maintain full endurance and health while kiting.

### 6a. Mode: PvE Standard
- **Song Uptime**: Keep songs active 100% of the time; spot-heal when needed.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling (Buffer & Peeler Role)
- **Role During Pet Pulling**: The Nurture Bard is **NOT the puller**. It acts as stationary song buffer and secondary peeler.
- **Song Discipline**: Run **Power Regeneration** and **Health Regeneration** songs at camp.
- **Mobs Stay on the Pet**:
  - The mobs must stay focused on the pet while the pulsing damage shield deals continuous damage.
  - Strictly hold fire and heals while mobs are in transit to camp.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet toward the group, cast **Baseline Mez** or **Root** on that loose add immediately.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Nurture Bard **IS allowed to heal the pet**, but **MUST wait until incoming mobs have lost around 5% of their life** (~95% HP from damage shield contact hits) before casting direct heals.
  - Use the **best smallest heal** (`HealEfficient` / baseline heal).
- **Group Stays Put (Strict Stationary Discipline)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to seek casting range.
- **The 85% Mob Health Window**:
  - At ~85% mob health, maintain songs and spot-heal party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable**.
  - **Dungeon Suitability**: **MAXIMUM SONG BUFF SUSTAIN**. Top-tier pulsing songs (Endurance, Health, Speed) keep the Bard and charmed pet at peak combat efficiency in dungeon corridors.
  - **Corridor Pull Isolation**: Send charmed pet to hallway; melee assist.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Continuous grinding with zero endurance downtime.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable with charmed pet.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Send charmed pet or pull mob to corridor.
  - 2. Opener: Instant shout + Blade opener.
  - 3. Combo: Blade positional / evade reaction combo.
  - 4. Finisher: Execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Health < 50% or End < 25%.
  - **Resume Threshold**: Stand when resources >= 85%.
- **Emergency Escape Protocol**:
  - Instant Mez song -> Speed Song flee.

### 7. Offensive Priorities
1. Baseline Instrument Mez: Priority 85 (Peel loose adds or incoming threats)
2. Baseline Instrument Root: Priority 80
3. DD Shouts: Priority 40
4. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Speed of Sound (AtlasOF_SOS): Priority 100
2. Purge (AtlasOF_Purge): Priority 100
3. Pulsing Songs (Power/Health): Priority 95
4. Mastery of Concentration: Priority 90
5. Secondary Heal on Ally: Priority 80 (When ally HP < 45%)

### 9. Target Priorities
1. Loose Break-off Add: Priority 95 (For Mez/Root peel)
2. Lowest Effective HP% Ally: Priority 90 (For secondary heal)
3. Group Pet (at 5% mob HP loss threshold): Priority 80

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1350–1500 units
- **Stationary Camp Discipline**: Hold ground firmly at camp anchor during pet pulling.

### 11. Ability Usage Priorities
1. `AtlasOF_SOS`: Priority 100
2. `AtlasOF_Purge`: Priority 100
3. Pulsing Songs: Priority 95
4. Baseline Mez / Root: Priority 85
5. Baseline Heals: Priority 80

### 12. Resource Management
- **Mana Floor**: 20% (Reserve power for secondary CC and heals)
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Keep Power song active at all times.
- Peel break-off adds with Mez or Root during pet pulls.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never mez targets taking damage from the pulsing damage shield (breaks mez).
- Never heal the pet mid-pull before mobs reach camp and register 5% health loss.
- Never run away backwards out of camp to seek max range.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Baseline Mez / Root.
- **Condition: Ally HP < 45%**: Select Ally -> Cast Baseline Heal.
- **Condition: Pet HP < 70% & Mobs Lost 5% HP**: Select Pet -> Cast `HealEfficient`.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `AtlasOF_MCL`, conserve power for songs and emergency single-target CC.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_SOS` (Rank 1) - 15 pts [Total: 20]
3. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
4. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 35]
5. `AtlasOF_Serenity` (Rank 1-3) - 1 + 3 + 6 = 10 pts [Total: 45]
6. `AtlasOF_AvoidanceOfMagic` (Rank 1-2) - 1 + 3 = 4 pts [Total: 49]
7. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 50]
