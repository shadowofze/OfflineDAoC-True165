---
schema_version: 1
class_name: "Druid"
class_id: 47
realm: "Hibernia"
primary_stats: ["Empathy", "Constitution"]
default_personality: "Supportive"
---

# Class: Druid (Hibernia)

## Shared Class Capabilities
- **Armor Type**: Leather, Reinforced Armor (absorb 19%), Cloth.
- **Weapon Slots**: One-Handed Blades or Blunt (Right Hand) + Small Shield (Left Hand).
- **Core Specialization Lines**:
  - `Regrowth`: Baseline and spec single-target heals (Efficient/Major), group heals, spread heals, combat resurrections, cures (Poison, Disease).
  - `Nurture`: Baseline and spec Strength/Constitution, Dexterity/Quickness buffs, health regeneration buffs, damage shields, self-Armor Factor (AF) buffs.
  - `Nature`: Nature companion pet summoning (Woodland Guardian / Sylvan Hound), nature direct damage (Energy DD), Nature Root (single-target and area-of-effect roots), Nature Disease, damage-over-time (DoT) spells.
- **Base Abilities**: Sprint, Evade 1, Shield Block.
- **Summoned Companion**: Summons a natural protector pet capable of melee attack, intercepting flankers, and executing pet commands (`/pet attack`, `/pet follow`, `/pet stay`, `/pet guard`).

---

## Specialization 1: Regrowth Druid
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Regrowth"
  min_point_ratio: 0.50
  spec_type_aliases: ["RegrowthDruid"]
```

### 1. Intended Role
Primary Group Healer, Group Purge Bearer, and Triage Anchor.

### 2. Intended Role Summary
The Regrowth Druid is the dedicated life-support anchor of the Hibernian group. It specializes in maximizing heal-over-time (HoT) buffering, instant and cast-time heal triage, disease/poison cleansing, and decisive deployment of the game-changing `Group Purge` Realm Ability.

### 3. Combat Philosophy
Ally survival is the supreme mandate. The Regrowth Druid maintains backline positioning, keeps pet defense active on `Passive` / `Guard` near the caster line, and focuses on keeping all 8 group members alive through efficient triage. It leverages `Mastery of Concentration` (MoC) and `Group Purge` to nullify enemy crowd-control and melee trains.

### 4. Mode: RvR
- **Group Purge Authority**: The Regrowth Druid holds the signature `Group Purge` active ability. Use `Group Purge` immediately when 2+ group members (or the primary crowd-controller / main assist) are hit with enemy AoE Mez, AoE Stun, or Root.
- **Disease & Poison Curing**: Rapidly cure Disease on tanks to restore 100% healing effectiveness.
- **Positioning**: 1350–1500 units behind the frontline. Spread away from fellow casters (Bards, Eldritches, Enchanters) to avoid clustered enemy PBAoE bombs.
- **Pet Support**: Set nature pet to `Guard` the primary crowd-controller (Bard) or self to disrupt stealthers attempting to open from behind.

### 5. Mode: PvP
- **Kiting & Pillar Play**: Break enemy archer and caster lines of sight between cast cycles.
- **Counter-Interrupt**: Use the fastest heal available when pressured by enemy pets or fast melee attacks (Druids have no Quick Cast).
- **Pet Harass**: Command pet to attack enemy casters to disrupt their cast timers while the Druid stays back healing.

### 6a. Mode: PvE Standard
- **Aggro Management**: Hold major heals for 1.5 seconds on pull until the frontline tank lands initial taunt or melee styles to prevent pulling healing aggro.
- **Pacing**: Monitor group mana. Call for rest when party mana drops below 35%.
- **Emergency Adds**: If an un-taunted monster targets the Druid, cast instant root or command pet to intercept while continuing triage.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused on the pet so that the pet caster's pulsing damage shield inflicts continuous damage.
  - Tanks and melee DPS do NOT attempt to pull aggro or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Druid can spot-heal allies engaging these loose adds.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Regrowth Druid **IS allowed to heal the pet**, but **MUST wait until the incoming mobs have lost around 5% of their life** (mobs at ~95% HP or below from damage shield contact hits) before casting its first direct heal, ensuring the mobs have anchored threat on the pet.
  - **Smart Healing (Best Smallest Heal)**: The Druid uses "smart healing"—casting its **best smallest heal** (`HealEfficient` / minor baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet continues taking massive damage (heavy multi-mob spike or rapid health drop), the Druid is authorized to cast a bigger heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.
  - The pet puller is 100% responsible for bringing the mobs directly into the stationary group so everyone can do their job.
- **Group Triage & 85% AoE Window**:
  - Maintain triage on group members if they take stray or incidental damage.
  - At ~85% mob health, when AoE casters begin blasting the pack, maintain stationary heal support on party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Preferred**.
  - **Dungeon Suitability**: **CAUTIOUS / NOT RECOMMENDED SOLO**. While self-heals and the Nature pet companion provide some sustain, low offensive damage makes soloing slow. In dungeons, narrow hallways and roaming adds risk spell interrupts on heals. Soloing is best done at dungeon entrance camps or open fields. **Prioritize grouping with tanks and DPS for dungeon expeditions**.
- **Target Con Selection**:
  - **Optimal (Blue / Low Blue)**: Reliable kills without running out of mana.
  - **Secondary (Yellow)**: Only engage if full mana pool is available; kill times are prolonged.
  - **Restricted (Orange)**: Strictly avoid solo; mana exhaustion before mob dies.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Summon Nature pet companion; pre-buff self with baseline AF and stat buffs.
  - 2. Opener: Cast baseline Nature DD from 1500 units to initiate pull.
  - 3. Pet Engage: Nature pet attacks mob on contact, absorbing initial hits.
  - 4. Self-Heal: Cast HealEfficient when Health drops < 65%.
  - 5. Finisher: Melee strike with One-Handed Club or baseline DD.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest after every 1-2 kills when Mana < 35% or Health < 50%.
  - **Safety Check**: Clear 800 units perimeter before sitting.
  - **Resume Threshold**: Stand when Mana >= 90% and Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast baseline Root on add -> Nature pet distracts -> activate Sprint -> retreat to open guard tower.

### 7. Offensive Priorities
1. Nature Energy DD: Priority 10 (Only if all group members > 90% HP, mana > 60%, and called target < 15% HP)
2. Nature Pet Attack: Priority 30 (Sending pet to interrupt enemy casters)
3. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Self-Cast Barkskin / AF Buff: Priority 95 (Keep active at all times)
2. Out-of-Combat Group Buffs: Priority 90 (Spec Str/Con, Dex/Qui, Health Regen)
3. Group Purge / Self Purge: Priority 100 (When group or self is incapacitated)
4. Mastery of Concentration: Priority 90 (When under heavy melee pressure)

### 9. Target Priorities
1. Lowest Effective HP% Group Member: Priority 100
2. Group Member afflicted with Disease or Poison: Priority 95
3. Group Member focused by enemy assist train: Priority 90
4. Friendly Crowd Controller (Bard/Mentalist) under attack: Priority 95

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1250–1450 units
- **Pet Placement**: Guard mode within 300 units of the backline casters.

### 11. Ability Usage Priorities
1. `Group Purge` (AtlasOF_GroupPurge): Priority 100 (Cleanse AoE crowd-control on 2+ allies)
2. `Purge` (AtlasOF_Purge): Priority 95 (Self CC cleanse)
3. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 90 (15 seconds of uninterruptible healing)
4. `Armor of Faith` (AtlasOF_ArmorOfFaith): Priority 85 (Group physical absorb bubble)
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 80 (Restore mana when below 25%)

### 12. Resource Management
- **Conservative Mana Floor**: 30% (Reserve power for critical triage and resurrections)
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient` (Greater Heal)
  - Target HP 35%–70%: `HealBig` (Major Heal)
  - Target HP < 35%: `HealInstant` + `HealBig`
  - 3+ members < 70%: `HealSpread` / `HealGroup`
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Monitor group crowd-control status and trigger `Group Purge` decisively.
- Pre-cast heals on tanks initiating heavy combat pulls.
- Cure Disease immediately before casting major single-target heals.
- Keep the nature pet set to `Guard` on squishy backline casters to intercept stealthers.

### 14. What the AI MUST NOT Do
- MUST NOT engage in melee combat under any circumstances.
- MUST NOT cast offensive Nature spells while any ally is below 85% HP.
- MUST NOT cast direct heals on a pulling pet before mobs have lost ~5% of their life during a pet pull.
- MUST NOT cast large heals on the pet during a pet pull when a small efficient heal suffices.
- MUST NOT run away or flee from the group anchor to gain casting range during a pet pull (group must stay put).
- MUST NOT break crowd-controlled (mezzed) targets with direct damage or pet attacks.

### 15. Group Responsibilities
- Primary Healer: Full group triage ownership across all 8 members.
- Group CC Cleanser: Sole authority over `Group Purge`.
- Status Lead: Immediate removal of Poison and Disease.

### 16. Situational Responses
- **When Group Stunned/Mezzed**: Trigger `Group Purge` instantly to restore full team mobility.
- **When Focused by Melee Train**: Pop `Mastery of Concentration` (MoC) to chain-heal through interrupts; command pet to peel.
- **When Silenced**: Fall back behind hard cover immediately and alert group.
- **When Out of Mana**: Pop `MysticCrystalLore` or Mana Potion; signal group to hold offensive pushes.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Hibernia Healer Archetype):*

1. **Group Purge 1** (`AtlasOF_GroupPurge`, Level 1): Signature realm ability; cleanses crowd-control across the entire 8-man group.
2. **Purge 1** (`AtlasOF_Purge`, Level 1): Self CC cleanse.
3. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Instant 25% mana replenishment for sustained triage.
4. **Mastery of Concentration 1** (`AtlasOF_MasteryOfConcentration`, Level 1): 15 seconds of uninterruptible casting when pressured by enemy melee.
5. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Increases Empathy, scaling mana pool and base healing power.
6. **Mastery of Healing 2** (`AtlasOF_MasteryOfHealing`, Level 2): Direct percentage increase to all healing output.
7. **Wild Healing 1** (`AtlasOF_WildHealing`, Level 1): Enables critical heals (10% critical heal chance).
8. **Armor of Faith 1** (`AtlasOF_ArmorOfFaith`, Level 1): Group-wide physical damage absorb bubble.
9. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Decreases spell casting time.
10. **Serenity 1** (`AtlasOF_Serenity`, Level 1): Passive mana regeneration.
11. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Passive magic damage reduction.
12. **Toughness 2** (`AtlasOF_Toughness`, Level 2): Hit point pool.
13. **Mastery of Healing 3**: Maximizes primary heal throughput.
14. **Wild Healing 2**: Increases critical heal chance to 20%.

---

## Specialization 2: Nurture Druid
- **role**: HybridSupport
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Nurture"
  min_point_ratio: 0.45
  spec_type_aliases: ["NurtureDruid"]
```

### 1. Intended Role
Group Buffer, Secondary Triage Healer, and Midline Defense Reinforcement.

### 2. Intended Role Summary
The Nurture Druid maximizes group physical and magical combat effectiveness through top-tier spec buffs, provides secondary triage and spot-healing, and defends the backline using pet peels and shield blocks.

### 3. Combat Philosophy
Maintains 100% uptime on maximum spec Strength/Constitution, Dexterity/Quickness, and Health Regeneration buffs on all allies. Positions in the midline between the frontline and backline, ready to deliver spot-heals, shield-block flankers, or command the nature pet to harass enemy attackers.

### 4. Mode: RvR
- **Buff Maintenance**: Ensure all allies possess top-tier spec buffs prior to engagement.
- **Secondary Healing**: Cover spot-heals on off-tanks and DPS, freeing the Regrowth Druid to focus on the main tank and `Group Purge`.
- **Pet Defense**: Set pet on aggressive intercept against incoming stealthers or light tanks.

### 5. Mode: PvP
- **Buff Superiority**: Leverage superior stat and regeneration buffs to out-sustain enemy skirmishers.
- **Shield Defense**: Use small shield block to mitigate enemy archer and melee burst.
- **Pet Harass**: Send pet to disrupt enemy casters while the Druid provides secondary healing.

### 6a. Mode: PvE Standard
- **Pre-Pull Buffing**: Cast full suite of spec buffs before initiating camp pulls.
- **Secondary Healing**: Cover spot-heals on party members, conserving main healer mana.
- **Pet Off-Tank**: Use pet to distract loose monster adds until the main tank can establish aggro.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Pre-Pull Pet Durability Buffing**:
  - Cast top-tier Spec AF, Spec Strength/Constitution, and Spec Dexterity/Quickness directly on the pulling pet prior to the pull.
  - Maximizing the pet's armor and evasion ensures it can absorb multiple hits while the damage shield does its job.
- **Mobs Stay on the Pet (No Taunting the Main Pack)**:
  - Do NOT attempt to taunt or peel the main mob pack off the pet. The mobs must stay beating on the pet to take continuous damage from the pet caster's pulsing damage shield.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and threatens group members, the Nurture Druid can immediately step in with One-Handed weapon, shield block, and nature pet to attack, distract, and peel that loose mob to protect the group!
- **Support & Smart Healing**:
  - If the pet is taking heavy damage and incoming mobs have lost ~5% life, the Nurture Druid can assist with a small efficient heal on the pet or spot-heal allies.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Hold position firmly at the group camp anchor. Do **NOT** run away or backpedal to gain casting or melee range.
  - The pet puller is responsible for bringing the mobs directly into the parked group.
- **Cast Embargo on Main Pack Above ~85% Mob Health**:
  - Hold all offensive casting on the main cluster while mobs are above ~85% health.
  - Once the pack reaches ~85% health, provide support casting or spot-heals on party members as needed while AoE casters melt the cluster down.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Moderately Viable**.
  - **Dungeon Suitability**: **MODERATE SOLO SURVIVABILITY VIA SPEC BUFFS**. Top-tier self-spec buffs (Spec Str/Con, Dex/Quick, AF) provide heavy durability in reinforced scale armor, supported by the Nature pet companion. Can solo single Blue/Yellow mobs in dungeon entrance corridors by buffing to cap and using Club/Shield styles.
  - **Corridor Pull Isolation**: Pull single mobs to hallway corners with baseline Nature DD; melee down safely while pet assists.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Buffed melee defense ensures low damage taken.
  - **Secondary (Yellow)**: Standard target. Self-buffs make 1-on-1 melee combat very safe.
  - **Restricted (Orange)**: Only engage with full mana pool to cover self-heals.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure full suite of Spec Str/Con, Dex/Quick, and AF buffs are running on self and pet.
  - 2. Opener: Baseline Nature DD pull from doorway.
  - 3. Melee Combat: Engage with One-Handed Club style chain; pet attacks from flank.
  - 4. Self-Sustain: Cast minor baseline heal if Health drops < 60%.
  - 5. Finisher: Club execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Baseline Root add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Nature Pet Attack: Priority 60 (Directing pet onto called assist target)
2. Nature Energy DD: Priority 40 (Assisting MA target when group HP > 80% and mana > 50%)
3. Melee Attack: Priority 30 (Fallback when defending against backline flankers)

### 8. Defensive Priorities
1. Group Spec Buff Upkeep (Spec Str/Con, Dex/Qui, Health Regen): Priority 95 (Out of Combat)
2. Self-Cast Barkskin / AF Buff: Priority 90
3. Secondary Spot-Heal / Cure: Priority 85 (When ally HP < 65% or primary healer is pressured)
4. Shield Block Stance: Priority 75 (When melee closes within 300 units)

### 9. Target Priorities
1. Primary Healer (when taking damage or pressured): Priority 95
2. Friendly Group Member with HP < 50%: Priority 90
3. Main Assist Target: Priority 60
4. Enemy Flanker / Stealther on Backline: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline
- **Target Range**: 700–1000 units behind frontline
- **Flank Coverage**: Position to intercept enemy flankers before they reach the Regrowth Druid or Bard.

### 11. Ability Usage Priorities
1. `Armor of Faith` (AtlasOF_ArmorOfFaith): Priority 90 (Deploy to absorb enemy frontline burst)
2. `Purge` (AtlasOF_Purge): Priority 85 (Cleanse CC to maintain secondary heal coverage)
3. `Mastery of the Arcane`: Passive buff effectiveness
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 80 (Restore mana during extended fights)

### 12. Resource Management
- **Mana Floor**: 25% (Reserve mana for emergency spot-heals and rebuffing fallen allies)
- **Buff Priority**: Spec Str/Con > Spec Dex/Qui > Base AF > Base Stats > Health Regen
- **Heal Thresholds**: Cast `HealEfficient` when ally HP 50%–75%; cast `HealBig` when ally HP < 50%.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Rebuff fallen group members immediately after they are resurrected and stabilized.
- Automatically step in as Primary Healer if the Regrowth Druid is killed or crowd-controlled.
- Direct nature pet to peel enemies off the primary healer or cloth casters.

### 14. What the AI MUST NOT Do
- MUST NOT allow core group spec buffs to drop during active dungeon or battlefield runs.
- MUST NOT drain mana on offensive casting when the primary healer is below 50% mana.
- MUST NOT taunt or peel the main mob pack off the pet during a pet pull (only attack loose mobs that break off to protect the group).
- MUST NOT cast heals on the pet before mobs have lost ~5% life during a pet pull.
- MUST NOT run away or flee from the group anchor to gain casting or melee range during a pet pull (group must stay put).
- MUST NOT break mezzed targets with direct damage or pet attacks.

### 15. Group Responsibilities
- Master Buffer: 100% uptime on all spec and baseline concentration buffs.
- Off-Healer: Backup triage and cure coverage.
- Midline Defender: Shield block and pet peel against enemy assassins and flankers.

### 16. Situational Responses
- **When Primary Healer Dies**: Promote self dynamically to `EmergencyPrimaryHealer`, shifting all priorities to triage.
- **When Backline Attacked**: Command pet to attack flanker immediately; position shield between attacker and squishy ally.
- **When Dispel Magic Hits Group**: Fall back and re-apply critical defensive buffs as mana permits.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Hybrid Support Archetype):*

1. **Mastery of the Arcane 1** (`AtlasOF_MasteryOfTheArcane`, Level 1): Increases the stat value and duration of all cast buffs.
2. **Purge 1** (`AtlasOF_Purge`, Level 1): Essential CC removal.
3. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Mana recovery.
4. **Armor of Faith 1** (`AtlasOF_ArmorOfFaith`, Level 1): Group physical absorb shield.
5. **Serenity 1** (`AtlasOF_Serenity`, Level 1): Passive mana regeneration to sustain buff upkeep and healing.
6. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Boosts Empathy and mana pool.
7. **Mastery of the Art 1** (`AtlasOF_MasteryOfTheArt`, Level 1): Accelerates buff and heal cast times.
8. **Mastery of Healing 1** (`AtlasOF_MasteryOfHealing`, Level 1): Enhances secondary heal potency.
9. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Further cast speed reduction.
10. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Magical defense.
11. **Toughness 2** (`AtlasOF_Toughness`, Level 2): Hit point pool.
12. **Mastery of the Arcane 2**: Maximizes spec buff scaling for the entire group.

---

## Specialization 3: Nature Druid
- **role**: CasterDps
- **default_personality**: Opportunistic

```yaml
match_specs:
  primary_line: "Nature"
  min_point_ratio: 0.45
  spec_type_aliases: ["NatureDruid"]
```

### 1. Intended Role
Nature Pet Master, AoE Root Specialist, and Offensive Off-Healer.

### 2. Intended Role Summary
The Nature Druid unleashes the offensive might of Hibernian nature, commanding a high-level nature companion buffed by `WildMinion`, delivering energy direct-damage nukes, casting long-duration area-of-effect roots, and inflicting Nature Disease while retaining baseline emergency healing.

### 3. Combat Philosophy
Operates as an offensive caster with a formidable pet and superior crowd control. Uses single-target and Area of Effect (AoE) Roots to peel enemy melee trains, commands the nature pet to tear into called assist targets, casts Nature Disease to cut enemy healing in half, and provides baseline healing only when group survival is at stake.

### 4. Mode: RvR
- **Area of Effect Root Lockdown**: Land AoE Nature Root on enemy melee trains attempting to push the group, neutralizing their charge.
- **Disease on Assist Targets**: Cast Nature Disease onto enemy primary targets to halve all enemy healing received.
- **Pet Burst Pressure**: Command the nature pet (boosted by `WildMinion`) to focus-fire with the Main Assist train onto enemy casters and healers.

### 5. Mode: PvP
- **Root-and-Nuke Kiting**: Root enemy melee attackers and step back to cast maximum-damage Nature Energy DDs.
- **Pet Harass**: Command pet to aggressively stick to enemy casters, creating constant spell interrupts.
- **Disease & DoT Pressure**: Apply Nature DoT and Disease to pressure enemy healers.

### 6a. Mode: PvE Standard
- **Add Lockdown**: Use Nature Root to park loose monster adds safely away from the group.
- **Pet Assist**: Command pet to attack the Main Assist target, contributing heavy melee damage.
- **Focus Fire**: Chain-cast Nature Energy direct damage on called targets.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The incoming pack stays focused on the pulling pet so the pulsing damage shield burns them down.
  - No tank or melee attempts to taunt the main pack, and the Nature Druid holds offensive spells on the main cluster while mobs are above ~85% HP.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and threatens friendly healers or casters, the Nature Druid is authorized to immediately cast Nature Root, direct-damage nukes, or send its nature pet onto that loose mob to protect the group!
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stand firm at the group camp anchor alongside the casters. Do **NOT** run away or kite backwards to gain max spell range.
  - The pet puller is responsible for bringing the mobs directly into the parked group.
- **The 85% Mob Health AoE Trigger**:
  - **HOLD AoE on Main Pack Above ~85% HP**: While the main pack is above ~85% health, hold all area-of-effect spells and let the damage shield do the work.
  - **UNLEASH AREA OF EFFECT AT ~85% HP**: Once the mob pack reaches approximately **85% health**, immediately begin casting Area of Effect spells (AoE Root / targeted AoE spells) on the pack clustered around the pet to melt the mobs down together with the other group AoE casters.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized; Dungeons Viable at Entrance Rooms**.
  - **Dungeon Suitability**: **HIGH OUTDOOR DOT/ROOT KITER**. The Nature Druid excels at long-range outdoor DoT and Root kiting (Shannon Estuary, Bog of Cullen), backed by the Nature pet companion. In dungeons, Nature DoTs and DD nukes allow soloing single mobs at corridor entrances; however, deep dungeon corridors increase risk of spell interrupts on adds. Maintain outdoor leveling for maximum grinding efficiency.
  - **Corridor Pull Isolation**: Root target mob from doorway -> apply Nature DoT -> Nature pet attacks while Druid channels DD nukes.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Dies rapidly under DoT + Pet attacks + 2 Nature DD nukes.
  - **Secondary (Yellow)**: Standard target. Root locks mob in place while DoT ticks and DD drains HP.
  - **Restricted (Orange)**: Viable outdoors with full mana and long retreat lane.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units in open field or dungeon doorway.
  - 2. Opener: Cast Nature Root on target.
  - 3. Affliction: Cast primary Nature DoT; command pet to engage.
  - 4. Nuke: Channel Nature DD nukes while target crawls forward.
  - 5. Finisher: Rapid Nature DD or club style if mob reaches melee.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast AoE Root on adds -> pet intercepts -> Sprint retreat to open safe zone.

### 7. Offensive Priorities
1. Nature Pet Attack (with WildMinion): Priority 95 (Focused on MA target)
2. AoE / Single-Target Nature Root: Priority 90 (Peeling enemy melee or locking down assist targets)
3. Nature Disease: Priority 85 (Debuffing enemy healers and assist targets)
4. Nature Energy Direct Damage: Priority 80 (Focusing MA target)
5. Nature DoT: Priority 65 (Applied to secondary targets)

### 8. Defensive Priorities
1. Self-Cast Barkskin / AF Buff: Priority 85 (Maintain before engaging)
2. Purge on CC: Priority 90 (Cleanse stuns/mezzes to maintain offensive pressure)
3. Shield Block: Priority 70 (Angle shield toward incoming melee)
4. Baseline Emergency Heal: Priority 60 (Only if primary healer is dead and ally is < 25% HP)

### 9. Target Priorities
1. Main Assist Target: Priority 100
2. Enemy Melee pursuing friendly Healer / Caster (Target for Nature Root): Priority 95
3. Enemy Healer casting major spell (Target for Pet Harass & Disease): Priority 90
4. Low HP Enemy (< 20% HP): Priority 85 (Execute window)

### 10. Positioning Preferences
- **Anchor**: Midline / Caster Line
- **Target Range**: 900–1200 units from enemy target
- **Pet Placement**: Deployed onto the frontline assist target.

### 11. Ability Usage Priorities
1. `Wild Minion`: Passive pet damage and critical strike chance
2. `Purge` (AtlasOF_Purge): Priority 90 (Cleanse CC to sustain offensive output)
3. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 85 (Uninterruptible rooting and nuking)
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 80 (Mana replenishment)
5. `The Empty Mind` (AtlasOF_EmptyMind): Priority 80 (25% magic absorption when taking caster focus)

### 12. Resource Management
- **Aggressive Mana Floor**: 20% (Permit aggressive mana spend to secure kills and land CC)
- **Power Pacing**: Pop MCL proactively during long fights.
- **Conserve Mode**: If target is > 70% HP and pet is dealing steady damage, weave baseline nukes rather than burning max power.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Land Nature Disease on the primary enemy target to cut all incoming healing by 50%.
- Deploy AoE Nature Root onto enemy melee trains to peel them off friendly cloth casters.
- Command the nature pet to constantly harass and interrupt enemy casters.
- Switch to baseline heals if the group's primary healer dies and allies are in critical condition.

### 14. What the AI MUST NOT Do
- MUST NOT cast AoE spells on the main mob cluster while mobs are above ~85% health during a pet pull (unless attacking a loose mob that broke off to protect the group).
- MUST NOT run away or flee from the group anchor to gain spell range during a pet pull (group must stay put).
- MUST NOT break mezzed targets with direct damage, DoTs, or pet attacks.
- MUST NOT exhaust all mana if the group's primary healer is dead (must retain power for baseline triage).

### 15. Group Responsibilities
- Nature DPS & Pet Handler: Heavy melee pet pressure on the assist train.
- Root Peeler: Instant and AoE root lockdown of enemy pursuers.
- Healing Reducer: 100% Disease uptime on primary kill targets.

### 16. Situational Responses
- **When Enemy Melee Charges Backline**: Cast AoE Nature Root immediately; command pet to peel attacker.
- **When Focused by Enemy Casters**: Pop `The Empty Mind` (25% magic absorption) and return fire with Nature Energy DD.
- **When Out of Mana**: Pop MCL; command pet to sustain melee damage while falling back.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Caster / Pet Master Archetype):*

1. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Increases Empathy, boosting spell damage, root land rate, and mana pool.
2. **Wild Minion 1** (`AtlasOF_WildMinion`, Level 1): Greatly enhances nature pet melee damage and gives pet critical strike capability.
3. **Purge 1** (`AtlasOF_Purge`, Level 1): Essential CC cleanse.
4. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Mana recovery for sustained nuke and root rotations.
5. **Mastery of Concentration 1** (`AtlasOF_MasteryOfConcentration`, Level 1): Uninterruptible root and nuke casting under pressure.
6. **Wild Minion 2** (`AtlasOF_WildMinion`, Level 2): Maximizes pet critical strikes and combat threat.
7. **The Empty Mind 1** (`AtlasOF_EmptyMind`, Level 1): Active 25% magic absorption shield against enemy caster burst.
8. **Mastery of the Art 1** (`AtlasOF_MasteryOfTheArt`, Level 1): Reduces cast time for Nature Root and Energy DD.
9. **Armor of Faith 1** (`AtlasOF_ArmorOfFaith`, Level 1): Group physical absorb shield.
10. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Further cast speed reduction.
11. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Magic resistance defense.
12. **Wild Minion 3**: End-game pet damage scaling.
