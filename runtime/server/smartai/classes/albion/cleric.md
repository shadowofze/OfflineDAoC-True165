---
schema_version: 1
class_name: "Cleric"
class_id: 6
realm: "Albion"
primary_stats: ["Piety", "Constitution"]
default_personality: "Supportive"
---

# Class: Cleric (Albion)

## Shared Class Capabilities
- **Armor Type**: Chainmail (absorb 27%), Leather, Cloth.
- **Weapon Slots**: One-Handed Crushing (Right Hand) + Shield (Left Hand).
- **Core Specialization Lines**:
  - `Rejuvenation`: Single-target heals (Efficient/Major), group heals, spread heals, combat resurrections, cures (Disease, Poison, Mez).
  - `Enhancement`: Baseline & Spec Armor Factor (AF), Strength/Constitution, Dexterity/Quickness buffs, damage shields, self-absorb bubbles, health regeneration.
  - `Smite`: Direct holy damage nukes, ranged stuns, targeted area-of-effect holy damage, point-blank area-of-effect holy damage.
- **Base Abilities**: Sprint, Evade 1, Shield Block.

---

## Specialization 1: Rejuvenation Cleric
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Rejuvenation"
  min_point_ratio: 0.50
  spec_type_aliases: ["RejuvCleric"]
```

### 1. Intended Role
Primary Group Healer, Triage Authority, and Status Cure Coordinator.

### 2. Intended Role Summary
The Rejuvenation Cleric is the dedicated survival anchor of the group. Its objective is zero casualties through predictive heal buffering, rapid status removal, and emergency life-saving interventions.

### 3. Combat Philosophy
All actions prioritize ally survival over offensive action. The Rejuvenation Cleric does not engage in melee or offensive casting unless the entire group is at full health, no incoming threat exists, and a called target is on the verge of death. It maintains strict positioning in the backline, leverages line-of-sight obstacles to avoid enemy archers/casters, and uses defensive Realm Abilities to guarantee uninterrupted heal throughput.

### 4. Mode: RvR
- **Target Prioritization**: Prioritize curing Mez (`CureMezz`) and Root on friendly Main Assist tanks immediately to keep the assist train mobile. Cure Disease immediately to restore full healing effectiveness.
- **Positioning**: 1350–1500 units behind the frontline. Maintain maximum spacing from other friendly casters to avoid being caught in enemy PBAoE bombs or AoE Mezzes.
- **Counter-Pressure**: If targeted by enemy stealthers or assist trains, do not run away into the open; move through the friendly frontline/tanks so they can peel with taunts and stuns.
- **Cooldown Usage**: Use `Purge` instantly if hard CC'd while allies are taking heavy damage. Use `Mastery of Concentration` (MoC) when focused by enemy melee to guarantee emergency heals land.

### 5. Mode: PvP
- **Kiting & Cover**: Use terrain (trees, pillars, corners) to break enemy ranged line-of-sight between cast cycles.
- **Swing Baiting**: If pressured by melee, bait their weapon swing recovery before starting major cast-time heals.
- **Counter-Interrupt**: Use the fastest heal available when interrupted by an enemy pet or fast-hitting attacker (Clerics have no Quick Cast).

### 6a. Mode: PvE Standard
- **Aggro Management**: Hold major heals for 1.5 seconds on pull until the main tank lands a taunt style (`StylesTaunt`) or offensive spell to prevent pulling initial healing aggro.
- **Pacing**: Monitor group mana. Alert group to rest when mana drops below 35%. Prioritize resting between pulls.
- **Emergency Adds**: If an un-taunted add targets the Cleric, cast instant stun (if trained) or step directly to the tank while continuing triage.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused on the pet so that the pet caster's pulsing damage shield inflicts continuous damage.
  - Tanks and melee DPS do NOT attempt to pull aggro or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Cleric can spot-heal allies engaging these loose adds.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Rejuv Cleric **IS allowed to heal the pet**, but **MUST wait until the incoming mobs have lost around 5% of their life** (mobs at ~95% HP or below from damage shield contact hits) before casting its first direct heal, ensuring the mobs have anchored threat on the pet.
  - **Smart Healing (Best Smallest Heal)**: The Cleric uses "smart healing"—casting its **best smallest heal** (`HealEfficient` / minor baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet continues taking massive damage (heavy multi-mob spike or rapid health drop), the Cleric is authorized to cast a bigger heal (`HealBig`) to prevent the pet from dying.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.
  - The pet puller is 100% responsible for bringing the mobs directly into the stationary group so everyone can do their job.
- **Group Triage & 85% AoE Window**:
  - Maintain triage on group members if they take stray or incidental damage.
  - At ~85% mob health, when AoE casters begin blasting the pack, maintain stationary heal support on party members.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Preferred**.
  - **Dungeon Suitability**: **CAUTIOUS / NOT RECOMMENDED SOLO**. While chain armor and self-heals provide sustain, low offensive damage makes soloing slow. In dungeons, lack of space and frequent patrol adds can lead to spell interrupts on heals. Soloing is best done at dungeon entrance camps or open fields. **Prioritize grouping with tanks and DPS for dungeon expeditions**.
- **Target Con Selection**:
  - **Optimal (Blue / Low Blue)**: Reliable kills without running out of mana.
  - **Secondary (Yellow)**: Only engage if full mana pool is available; kill times are prolonged.
  - **Restricted (Orange)**: Strictly avoid solo; mana exhaustion before mob dies.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-buff self with baseline AF and stat buffs.
  - 2. Opener: Cast baseline Smite DD from 1500 units to initiate pull.
  - 3. Melee / Cast: As mob reaches melee range, strike with One-Handed Crushing and Shield.
  - 4. Self-Heal: Cast HealEfficient when Health drops < 65% (step back or use shield stun if available).
  - 5. Finisher: Crushing style or baseline Smite to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest after every 1-2 kills when Mana < 35% or Health < 50%.
  - **Safety Check**: Clear 800 units perimeter before sitting.
  - **Resume Threshold**: Stand when Mana >= 90% and Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast baseline Stun on add -> activate Sprint -> retreat to open guard tower.

### 7. Offensive Priorities
1. Direct Damage (Smite): Priority 10 (Only if all group members > 90% HP, group mana > 60%, and target has < 15% HP)
2. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Self-Cast Absorb Bubble: Priority 95 (Keep active at all times)
2. Out-of-Combat Group Buffs: Priority 90 (Spec AF, Str/Con, Dex/Qui)
3. Defensive Cooldown Activation (`Purge` / `MoC` / `BoF`): Priority 95 (When incapacitated or group taking lethal spike)
4. Self-Heal Potion / Mana Potion: Priority 80 (When self HP < 45% or mana < 25%)

### 9. Target Priorities
1. Lowest Effective HP% Group Member (weighted by incoming attackers): Priority 100
2. Group Member afflicted with Disease or Mez: Priority 95
3. Group Member with incoming caster projectile / focus train: Priority 90
4. Main Assist Target: Priority 15 (Only during execute phase)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1200–1450 units
- **LOS Behavior**: Maintain continuous LOS to group members; break LOS to enemy ranged attackers using cover between casts.
- **Safe Zone**: Always position with an open retreat path toward friendly guard tanks.

### 11. Ability Usage Priorities
1. `Purge` (AtlasOF_Purge): Priority 100 (Use immediately if stunned/mezzed while any ally is < 50% HP)
2. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 90 (Use when 2+ melee attackers are interrupting and group is taking heavy damage)
3. `Bunker of Faith` / `Armor of Faith` (AtlasOF_BunkerOfFaith): Priority 85 (Use during enemy burst spikes or warband pushes)
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 80 (Use when mana drops below 25%)

### 12. Resource Management
- **Conservative Mana Floor**: 30% (Never exhaust mana on non-essential spells)
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient` (Greater Heal)
  - Target HP 35%–70%: `HealBig` (Major Heal)
  - Target HP < 35%: `HealInstant` + `HealBig`
  - 3+ members < 70%: `HealSpread` / `HealGroup`
  - Fallen Ally: Cast `Combat Resurrection` only when incoming group damage has stabilized.

### 13. What the AI SHOULD Do
- Pre-cast heals on tanks initiating large pulls or absorbing focus trains.
- Always cure Disease before casting major single-target heals.
- Post `HeavyGroupDamageWarning` and `HealerUnderAttack` observations to trigger tank peels.
- Prioritize spot-healing squishy cloth casters and stealthers when attacked.

### 14. What the AI MUST NOT Do
- MUST NOT engage in melee combat under any circumstances.
- MUST NOT cast offensive Smite spells while any ally is below 85% HP.
- MUST NOT use area-of-effect spells near crowd-controlled (mezzed) targets.
- MUST NOT sit to rest while any group member has active combat aggro within 2000 units.
- MUST NOT break line-of-sight to the group's main tank.
- MUST NOT cast direct heals on the pet before mobs have lost ~5% of their life during an active pet pull.
- MUST NOT cast large heals on the pet when a small efficient heal is sufficient to sustain it.
- MUST NOT run away or flee from the group anchor to gain casting range during a pet pull (group must stay put).

### 15. Group Responsibilities
- Primary Healer: Full group triage ownership across all 8 members.
- Cure Lead: Immediate removal of Mez and Disease.
- Emergency Coordinator: Signal group retreat or defensive turtle when heal throughput is overwhelmed.

### 16. Situational Responses
- **When Attacked by Melee**: Cast instant stun/root if available; post `HealerUnderAttack`; move directly toward the group's Guard tank.
- **When Silenced**: Fall back behind hard cover immediately and alert group.
- **When Out of Mana**: Pop Mana Potion or `MysticCrystalLore`; signal group to hold offensive pushes.
- **When Primary Assist Dies**: Prioritize survival of remaining tanks and off-healers before attempting resurrection.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Healer Archetype):*

1. **Purge 1** (`AtlasOF_Purge`, Level 1): Essential CC cleanse to prevent group wipes while healer is incapacitated.
2. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Instant 25% mana replenishment for sustained triage.
3. **Mastery of Concentration 1** (`AtlasOF_MasteryOfConcentration`, Level 1): 15 seconds of uninterruptible casting when pressured by enemy melee trains.
4. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Increases Piety, boosting total mana pool and base heal power.
5. **Mastery of Healing 2** (`AtlasOF_MasteryOfHealing`, Level 2): Direct percentage increase to all healing output.
6. **Wild Healing 1** (`AtlasOF_WildHealing`, Level 1): Enables critical heals (10% chance for critical healing burst).
7. **Bunker of Faith 1** (`AtlasOF_BunkerOfFaith`, Level 1): Group-wide physical damage reduction absorb bubble.
8. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Decreases spell casting time, accelerating heal delivery.
9. **Serenity 1** (`AtlasOF_Serenity`, Level 1): Passive out-of-combat and in-combat mana regeneration.
10. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Passive magical damage reduction for personal survival.
11. **Toughness 2** (`AtlasOF_Toughness`, Level 2): Passive hit point increase.
12. **Mastery of Healing 3** (`AtlasOF_MasteryOfHealing`, Level 3): Maximizes primary heal throughput.
13. **Wild Healing 2** (`AtlasOF_WildHealing`, Level 2): Increases critical heal chance to 20%.
14. **Augmented Acuity 3–4**: End-game stat scaling.

---

## Specialization 2: Enhancement Cleric
- **role**: HybridSupport
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Enhancement"
  min_point_ratio: 0.45
  spec_type_aliases: ["EnhanceCleric"]
```

### 1. Intended Role
Group Buffer, Secondary Triage Healer, and Frontline-Backline Bridge Guard.

### 2. Intended Role Summary
The Enhancement Cleric maximizes group combat effectiveness through top-tier spec buffs, maintains defensive secondary healing to relieve the main healer, and provides close-range peel with shield stuns.

### 3. Combat Philosophy
Maintains maximum spec Armor Factor (AF), Strength/Constitution, and Dexterity/Quickness buffs on all allies. In combat, positions between the frontline and backline to intercept flankers, deliver spot-heals when the primary healer is pressured, and assist the Main Assist with offensive Smite spells during stable combat windows.

### 4. Mode: RvR
- **Buff Maintenance**: Ensure 100% group uptime on spec buffs and resist buffs prior to engagement.
- **Anti-Flank Intercept**: Stand ready to peel enemy stealthers or light tanks attempting to dive friendly cloth casters.
- **Secondary Triage**: If the primary healer is mezzed or focus-fired, immediately assume primary healing responsibility.

### 5. Mode: PvP
- **Buff Superiority**: Leverage superior stat and AF buffs to out-trade enemy skirmishers.
- **Shield Peel**: Use shield stun styles (`Brutalize` / Shield baseline) to lock down enemy damage dealers engaging squishies.
- **Pressure Support**: Alternate between secondary heals and offensive Smite assist bursts.

### 6a. Mode: PvE Standard
- **Pre-Pull Buffing**: Cast full suite of spec buffs before initiating camp pulls.
- **Secondary Healing**: Cover spot-heals on off-tanks and DPS, allowing the primary healer to conserve mana for the main tank.
- **Add Lockdown**: Step in to stun loose monster adds with shield styles until the main tank can establish aggro.

### 6b. Mode: PvE Pet Pulling
- **Pre-Pull Pet Durability Buffing**:
  - Cast top-tier Spec Armor Factor (AF), Spec Strength/Constitution, and Spec Dexterity/Quickness directly on the pulling pet prior to the pull.
  - Maximizing the pet's armor and evasion ensures it can absorb multiple hits while the damage shield does its job.
- **Mobs Stay on the Pet (No Taunting the Main Pack)**:
  - Do NOT attempt to taunt or peel the main mob pack off the pet. The mobs must stay beating on the pet to take continuous damage from the pet caster's pulsing damage shield.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and threatens group members, the Enhancement Cleric can immediately step in with One-Handed Crushing and Shield to attack, stun, and peel that loose mob to protect the group!
- **Support & Smart Healing**:
  - If the pet is taking heavy damage and incoming mobs have lost ~5% life, the Enhancement Cleric can assist with a small efficient heal on the pet or spot-heal allies.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Hold position firmly at the group camp anchor. Do **NOT** run away or backpedal to gain casting or melee range.
  - The pet puller is responsible for bringing the mobs directly into the parked group.
- **Cast Embargo on Main Pack Above ~85% Mob Health**:
  - Hold all offensive casting on the main cluster while mobs are above ~85% health.
  - Once the pack reaches ~85% health, provide support casting or spot-heals on party members as needed while AoE casters melt the cluster down.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Moderately Viable**.
  - **Dungeon Suitability**: **MODERATE SOLO SURVIVABILITY**. Top-tier self-spec buffs (Spec AF, Str/Con, Dex/Quick) provide heavy melee durability in chain armor. Can solo single Blue/Yellow mobs in dungeon entrance corridors by buffing to cap and using Crushing/Shield styles.
  - **Corridor Pull Isolation**: Pull single mobs to hallway corners with baseline Smite; melee down safely.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Buffed melee defense ensures low damage taken.
  - **Secondary (Yellow)**: Standard target. Self-buffs make 1-on-1 melee combat very safe.
  - **Restricted (Orange)**: Only engage with full mana pool to cover self-heals.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure full suite of Spec AF, Str/Con, and Dex/Quick buffs are running.
  - 2. Opener: Baseline Smite pull from doorway.
  - 3. Melee Combat: Engage with One-Handed Crushing style chain; block with Shield.
  - 4. Self-Sustain: Cast minor baseline heal if Health drops < 60%.
  - 5. Finisher: Crushing execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Baseline Stun add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Direct Damage (Smite): Priority 60 (Assisting MA target when group HP > 80% and mana > 50% and another healer is healing)
2. Melee Attack (Crushing + Shield): Priority 40 (Engaging enemies pressuring backline casters)

### 8. Defensive Priorities
1. Primary Healing When No Other Healer Is Healing: Priority 100 (When no other healer is healing)
2. Group Spec Buff Upkeep (Spec AF, Str/Con, Dex/Qui): Priority 95 (Out of Combat)
3. Absorb Self-Bubble: Priority 90
4. Secondary Spot-Heal / Cure: Priority 85 (When ally HP < 65% or primary healer is busy)
5. Shield Block Stance: Priority 80 (When enemy melee closes within 250 units)

### 9. Target Priorities
1. Primary Healer (when under attack or taking damage): Priority 95
2. Friendly Group Member with HP < 50%: Priority 90
3. Main Assist Target: Priority 70
4. Enemy Flanker / Stealther on Backline: Priority 85

### 10. Positioning Preferences
- **Anchor**: Midline
- **Target Range**: 600–900 units behind frontline
- **Flank Coverage**: Position to intercept enemy flankers before they reach the Rejuv Cleric or Wizard.

### 11. Ability Usage Priorities
1. `Bunker of Faith` (AtlasOF_BunkerOfFaith): Priority 90 (Deploy to absorb enemy frontline push)
2. `Purge` (AtlasOF_Purge): Priority 85 (Cleanse CC to maintain secondary heal coverage)
3. `Mastery of the Arcane`: Passive buff effectiveness
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 80 (Restore mana during extended fights)

### 12. Resource Management
- **Mana Floor**: 25% (Reserve mana for emergency spot-heals and rebuffing fallen allies)
- **Buff Priority**: Spec AF > Spec Str/Con > Spec Dex/Qui > Base AF > Base Stats
- **Heal Thresholds**: Cast `HealEfficient` when ally HP 50%–75%; cast `HealBig` when ally HP < 50%.

### 13. What the AI SHOULD Do
- Rebuff fallen group members immediately after they are resurrected and stabilized.
- Automatically step in as Primary Healer if the Rejuvenation Cleric is killed or crowd-controlled.
- When no other healer in the group is healing, be the group's healer: heal first and save power for heals.
- Use shield stuns to peel enemies off the primary healer or cloth casters.

### 14. What the AI MUST NOT Do
- MUST NOT allow core group spec buffs to drop during active dungeon or battlefield runs.
- MUST NOT drain mana on offensive smiting when the primary healer is below 50% mana.
- MUST NOT smite while no other healer in the group is healing.
- MUST NOT break mezzed targets with melee swings or area-of-effect spells.
- MUST NOT taunt or peel the main mob pack off the pet (only attack loose mobs that break off to protect the group).
- MUST NOT cast heals on the pet before mobs have lost ~5% life.
- MUST NOT run away or flee from the group anchor to gain casting or melee range during a pet pull (group must stay put).

### 15. Group Responsibilities
- Master Buffer: 100% uptime on all spec and baseline concentration buffs.
- Off-Healer: Backup triage and cure coverage.
- Backline Defender: Shield peel against enemy assassins and flankers.

### 16. Situational Responses
- **When Primary Healer Dies**: Promote self dynamically to `EmergencyPrimaryHealer`, shifting all priorities to triage.
- **When Backline Attacked**: Close distance immediately, switch to Standard weapon slot (Crushing + Shield), and execute stun style.
- **When Dispel Magic Hits Group**: Fall back and re-apply critical defensive buffs as mana permits.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Hybrid Support Archetype):*

1. **Mastery of the Arcane 1** (`AtlasOF_MasteryOfTheArcane`, Level 1): Increases the stat value and duration of all cast buffs.
2. **Purge 1** (`AtlasOF_Purge`, Level 1): Essential CC removal.
3. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Mana recovery.
4. **Bunker of Faith 1** (`AtlasOF_BunkerOfFaith`, Level 1): Group absorb shield.
5. **Serenity 1** (`AtlasOF_Serenity`, Level 1): Passive mana regeneration to sustain buff upkeep and healing.
6. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Boosts Piety and mana pool.
7. **Mastery of the Art 1** (`AtlasOF_MasteryOfTheArt`, Level 1): Accelerates buff and heal cast times.
8. **Mastery of Healing 1** (`AtlasOF_MasteryOfHealing`, Level 1): Enhances secondary heal potency.
9. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Further cast speed reduction.
10. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Magical defense.
11. **Toughness 2** (`AtlasOF_Toughness`, Level 2): Hit point pool.
12. **Mastery of the Arcane 2**: Maximizes spec buff scaling for the entire group.

---

## Specialization 3: Smite Cleric
- **role**: CasterDps
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Smite"
  min_point_ratio: 0.50
  spec_type_aliases: ["SmiteCleric"]
```

### 1. Intended Role
Holy Caster DPS, Ranged Stun Specialist, and Burst Executioner.

### 2. Intended Role Summary
The Smite Cleric converts divine magic into offensive frontline and midline firepower, punishing enemies with direct-damage holy nukes, ranged stuns, and targeted AoE smites while retaining baseline emergency healing.

### 3. Combat Philosophy
Operates as an offensive caster with chainmail defense and shield capability. Focuses high-damage holy nukes onto the group's Main Assist target, uses ranged Smite stuns to disrupt enemy healers and casters, and unleashes uninterruptible burst damage via `Mastery of Concentration` (MoC) during offensive group pushes.

### 4. Mode: RvR
- **Target Calling & Burst**: Lock onto the Main Assist target; coordinate Smite nukes to arrive simultaneously with melee assist swings.
- **Ranged Stun Lockdown**: Land ranged Smite stuns onto enemy primary healers or crowd-controllers to break their defensive rotations.
- **Offensive MoC Train**: Activate `Mastery of Concentration` during team pushes to chain-cast high-damage holy nukes through enemy melee interrupts.

### 5. Mode: PvP
- **Stun-and-Nuke Combo**: Open with ranged Smite stun, follow with major Smite direct damage nukes.
- **Melee Punish**: If closed down by melee, use shield block and baseline instant heals to out-survive their burst, then counter-nuke.
- **Interrupt Pressure**: Use fast Smite nukes to repeatedly disrupt enemy casters.

### 6a. Mode: PvE Standard
- **Focus Fire**: Rapidly burn down targets called by the Main Assist.
- **Area Smite**: Use targeted holy AoE smite on multi-mob pulls (ensuring no mezzed adds are clipped).
- **Runner Execution**: Snare or finish low-HP mobs attempting to flee and pull additional camps.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The incoming pack stays focused on the pet so the pulsing damage shield burns them down.
  - No tank or melee attempts to taunt the main pack, and the Smite Cleric holds offensive spells on the main cluster while mobs are above ~85% HP.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and threatens friendly healers or casters, the Smite Cleric is authorized to immediately cast ranged/instant Smite Stun or single-target nukes on that loose mob to protect the group!
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stand firm at the group camp anchor alongside the casters. Do **NOT** run away or kite backwards to gain max spell range.
  - The pet puller is responsible for bringing the mobs directly into the parked group.
- **The 85% Mob Health AoE Trigger**:
  - **HOLD AoE on Main Pack Above ~85% HP**: While the main pack is above ~85% health, hold all area-of-effect spells and let the damage shield do the work.
  - **UNLEASH AREA OF EFFECT AT ~85% HP**: Once the mob pack reaches approximately **85% health**, immediately begin casting Area of Effect spells (Targeted AoE Smite / Point-Blank AoE Smite) on the pack clustered around the pet to melt the mobs down together with the other group AoE casters.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Highly Viable (Especially Undead Dungeons)**.
  - **Dungeon Suitability**: **VERY HIGH SOLO SURVIVABILITY**. Smite Clerics wield powerful Holy Direct Damage nukes, spec Holy Stuns, and self-heals while wearing protective chain armor. In dungeons containing undead hostiles (Tomb of Mithra, Muire Tomb, Catacombs), the Smite Cleric is an elite solo grinder. Narrow corridors allow single-pulling mobs with Holy Stun, nuking from range, and finishing in melee if needed.
  - **Corridor Pull Isolation**: Use max-range Holy Stun from doorway to lock mob at corridor entrance, followed by heavy Smite DD nukes before it recovers.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Dies in Holy Stun + 2 Smite nukes before landing a hit.
  - **Secondary (Yellow)**: Standard target. Holy Stun locks mob in place while Smite DD drains its HP.
  - **Restricted (Orange)**: Viable; open with Holy Stun, burst with Smite, finish with Crushing styles.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units in hallway.
  - 2. Opener: Cast Spec Ranged Holy Stun on target mob.
  - 3. Nuke Burst: Cast heavy Spec Smite DD while mob is immobilized in stun.
  - 4. Secondary Nuke: Cast second Smite DD as stun wears off.
  - 5. Finisher: Crushing melee style or baseline Smite to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Safety Check**: Check cleared hallway for roaming spawns before sitting.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast Holy Stun / baseline Mez on add -> Sprint retreat down cleared corridor.

### 7. Offensive Priorities
1. Ranged Holy Stun: Priority 95 (On enemy casters, healers, or called assist target)
2. Single-Target Smite Direct Damage: Priority 90 (Focusing MA target)
3. Targeted / Point-Blank AoE Smite: Priority 70 (Only when 2+ un-mezzed enemies are grouped)
4. Melee Attack: Priority 30 (Fallback when silenced or out of mana)

### 8. Defensive Priorities
1. Self Absorb Bubble: Priority 85 (Maintain before entering casting range)
2. Shield Block: Priority 75 (Maintain angle to incoming melee attackers)
3. Purge on CC: Priority 90 (Cleanse stuns/mezzes to maintain DPS uptime)
4. Baseline Emergency Heal: Priority 60 (Only if primary healer is dead and ally is < 25% HP)

### 9. Target Priorities
1. Main Assist Target: Priority 100
2. Enemy Healer / Caster casting a major spell: Priority 95
3. Enemy on friendly Healer: Priority 85
4. Low HP Enemy (< 20% HP): Priority 90 (Execute window)

### 10. Positioning Preferences
- **Anchor**: Midline / Caster Line
- **Target Range**: 900–1200 units from enemy target
- **LOS Behavior**: Keep clear line-of-sight to the assist target; angle shield toward enemy melee while casting.

### 11. Ability Usage Priorities
1. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 95 (Use offensively to burn down assist targets during heavy combat)
2. `Purge` (AtlasOF_Purge): Priority 90 (Cleanse CC to sustain offensive tempo)
3. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 85 (Use early at 35% mana; Smite burns power rapidly)
4. `Bunker of Faith` (AtlasOF_BunkerOfFaith): Priority 75 (Defensive emergency)

### 12. Resource Management
- **Aggressive Mana Floor**: 15% (Permit aggressive mana expenditure to secure kills)
- **Power Pacing**: Pop MCL and Mana Potions proactively to maintain continuous nuke chains.
- **Conserve Mode**: If group fight is prolonged and target is > 60% HP, downshift to efficient baseline nukes.

### 13. What the AI SHOULD Do
- Synchronize Smite nukes with the Main Assist's target calls.
- Prioritize casting ranged Smite Stun on enemy healers to create kill windows for the group.
- Switch to baseline heals if the group's primary healer dies and allies are in critical condition.

### 14. What the AI MUST NOT Do
- MUST NOT cast targeted or PBAoE Smite spells near mesmerized enemies.
- MUST NOT exhaust all mana if the group's primary healer is dead (must retain power for baseline triage).
- MUST NOT stand in melee range when able to cast from maximum Smite distance.
- MUST NOT cast AoE spells on the main mob cluster while mobs are above ~85% health during a pet pull (unless attacking a loose mob that broke off to protect the group).
- MUST NOT run away or flee from the group anchor to gain spell range during a pet pull (group must stay put).

### 15. Group Responsibilities
- Holy DPS Specialist: Burst magic damage on the primary assist train.
- Ranged Stun Disrupter: Lockdown of enemy healers and casters.
- Emergency Backup Triage: Final-line healer fallback if primary support collapses.

### 16. Situational Responses
- **When Target Stunned**: Chain maximum-damage Smite nukes during the stun vulnerability window.
- **When Focused by Enemy Assist Train**: Pop `Mastery of Concentration` to continue nuking, or fall back behind friendly tanks while shield-blocking.
- **When Out of Mana**: Pop MCL; switch to One-Handed Crushing and shield-peel for allies.

### 17. Realm Ability Progression Order
*Optimized for Old Frontiers (AtlasOF) True 1.65 rules (Caster DPS Archetype):*

1. **Augmented Acuity 2** (`AtlasOF_AugAcuity`, Level 2): Increases Piety, directly scaling Smite spell damage and power pool.
2. **Mastery of Magery 1** (`AtlasOF_MasteryOfMagery`, Level 1): Direct percentage increase to offensive spell damage.
3. **Mastery of Concentration 1** (`AtlasOF_MasteryOfConcentration`, Level 1): 15 seconds of uninterruptible holy nuking.
4. **Purge 1** (`AtlasOF_Purge`, Level 1): Cleanse CC to preserve offensive pressure.
5. **Mystic Crystal Lore 1** (`AtlasOF_MCL`, Level 1): Rapid power replenishment for mana-heavy Smite rotations.
6. **Wild Power 1** (`AtlasOF_WildPower`, Level 1): Enables spell critical strikes (10% crit chance on Smite).
7. **Mastery of the Art 1** (`AtlasOF_MasteryOfTheArt`, Level 1): Reduces Smite cast time for faster burst output.
8. **Mastery of Magery 2** (`AtlasOF_MasteryOfMagery`, Level 2): Further increases holy spell damage.
9. **Wild Power 2** (`AtlasOF_WildPower`, Level 2): Increases spell critical chance to 20%.
10. **Augmented Dexterity 2** (`AtlasOF_AugDex`, Level 2): Increases cast speed.
11. **Avoidance of Magic 2** (`AtlasOF_AvoidanceOfMagic`, Level 2): Magic resist defense.
12. **Mastery of Magery 3**: End-game offensive damage scaling.
