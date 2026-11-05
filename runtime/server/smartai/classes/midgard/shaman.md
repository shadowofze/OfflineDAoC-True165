---
schema_version: 1
class_name: "Shaman"
class_id: 28
realm: "Midgard"
primary_stats: ["Piety", "Constitution"]
default_personality: "Supportive"
---

# Class: Shaman (Midgard)

## Shared Class Capabilities
- **Armor Type**: Leather, Reinforced Armor (absorb 19%), Cloth.
- **Weapon Slots**: One-Handed Hammer (Right Hand) + Small / Medium Round Shield (Left Hand), or Two-Handed Hammer.
- **Core Specialization Lines**:
  - `Mending`: Baseline and spec single-target heals (Efficient/Major), group heals, combat resurrections, cures (Poison, Disease, Blindness, Nearsight).
  - `Augmentation`: Baseline and spec group enhancements including Strength/Constitution (Str/Con), Dexterity/Quickness (Dex/Qui), Acuity, Endurance Regeneration (Endo), Damage Add, Armor Factor (AF), Health Regeneration, and elemental/magic resistance buffs.
  - `Subterranean` (Cave Magic): Baseline Bolt and single-target direct damage (DD), single-target Root (Creeper / Roots, 99% speed decrease); Specialization includes Area-of-Effect Disease (Tribal Sickness / Epidemic / Plague - halves incoming heals by 50% and slows), Area-of-Effect Roots (Entangling Roots), Damage-Over-Time (DoT - Decay / Contagion), and combat stat debuffs (Str/Con, Dex/Qui debuffs).
- **Base Abilities**: Sprint, Evade 1, Shield Block.
- **Hybrid Role**: Can fight with melee weapons (Hammers) and shield block, but in group combat acts as a high-value backline support/caster.
- **Add-Control Root Rule**: Per `ShamanBotCombatPolicy.KeepsRootsForAdds`, roots never enter ordinary PvE attack rotations on primary kill targets; instead, single-target roots are strictly reserved for adds charging grouped allies.

---

## Specialization 1: Mending Shaman
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Mending"
  min_point_ratio: 0.50
  spec_type_aliases: ["MendShaman"]
```

### 1. Intended Role
Primary Group Healer, Disease/Poison Cleanser, and Triage Anchor.

### 2. Intended Role Summary
The Mending Shaman is the dedicated life-support anchor of the Midgard group. It specializes in maximizing direct and group healing, disease and poison cleansing, maintaining baseline stat buffs, and utilizing `Mastery of Concentration` (MoC) to sustain allies under intense enemy pressure.

### 3. Combat Philosophy
Ally survival is paramount. The Mending Shaman maintains backline positioning, triage-heals allies based on effective health percentage, cures incoming debuffs, and conserves mana to prevent group wipes. Offense is strictly limited to emergency add-peeling or finishing low-health runners when all group members are safe.

### 4. Mode: RvR
- **Triage Priority**: Prioritize friendly main crowd controllers (Healers/Pacification), main tanks, and casters under assist-train focus.
- **Disease Cleansing**: Rapidly cast `Cure Disease` on friendly frontline tanks and healers to remove incoming healing penalty debuffs.
- **Positioning**: 1350–1500 units behind the frontline. Maintain line-of-sight to the group while staying spread out to minimize exposure to enemy AoE crowd-control or PBAoE bombs.
- **Defensive Peel**: If an enemy assassin or frontline tank breaks into the backline, apply instant `Purge` if crowd-controlled, pop `Mastery of Concentration` if interrupted, and cast single-target root on the pursuer before kiting toward friendly peelers.

### 5. Mode: PvP
- **Line-of-Sight Management**: Break line-of-sight against enemy ranged casters and archers around terrain corners between heal casts.
- **Counter-Interrupt**: Use `Mastery of Concentration` to power through enemy melee interrupts when self or partner health drops below 40%.
- **Debuff Defense**: Cleanse enemy poisons and diseases immediately upon application.

### 6a. Mode: PvE Standard
- **Aggro Management**: Hold major heals for 1.5 seconds on pull until the frontline tank lands initial taunt or melee styles to prevent pulling healing aggro.
- **Pacing**: Monitor group mana. Call for rest when party mana drops below 35%.
- **Emergency Add Control**: If an un-taunted monster charges the party (`TryShamanPveAddRoot`), cast single-target root (Creeper) while the add is > 300 units away, then resume group healing.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused on the pet so that the pet caster's pulsing damage shield inflicts continuous damage.
  - Tanks and melee DPS do NOT attempt to pull aggro or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Mending Shaman can cast single-target root on the break-off add or spot-heal allies engaging it.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Mending Shaman **IS allowed to heal the pet**, but **MUST wait until the incoming mobs have lost around 5% of their life** (mobs at ~95% HP or below from damage shield contact hits) before casting its first direct heal, ensuring the mobs have anchored threat on the pet.
  - **Smart Healing (Best Smallest Heal)**: The Shaman uses "smart healing"—casting its **best smallest heal** (`HealEfficient` / minor baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet continues taking massive damage (heavy multi-mob spike or rapid health drop), the Shaman is authorized to cast a bigger heal (`HealBig`) to prevent the pet from dying.
  - Pre-cast Health Regeneration buffs and baseline stat buffs remain running on the pet prior to the pull.
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
  - **Dungeon Suitability**: **CAUTIOUS / NOT RECOMMENDED SOLO**. While self-heals and chain armor provide durability, low offensive damage makes soloing slow. In dungeons, narrow hallways and roaming adds risk spell interrupts on heals. Soloing is best done at dungeon entrance camps or open fields. **Prioritize grouping with tanks and DPS for dungeon expeditions**.
- **Target Con Selection**:
  - **Optimal (Blue / Low Blue)**: Reliable kills without running out of mana.
  - **Secondary (Yellow)**: Only engage if full mana pool is available; kill times are prolonged.
  - **Restricted (Orange)**: Strictly avoid solo; mana exhaustion before mob dies.
  - **Prohibited (Red / Purple)**: Strictly avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Pre-buff self with baseline AF and stat buffs.
  - 2. Opener: Cast baseline Cave DD from 1500 units to initiate pull.
  - 3. Melee / Cast: As mob reaches melee range, strike with One-Handed Hammer and Shield.
  - 4. Self-Heal: Cast HealEfficient when Health drops < 65%.
  - 5. Finisher: Hammer style or baseline DD to execute.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit to rest after every 1-2 kills when Mana < 35% or Health < 50%.
  - **Safety Check**: Clear 800 units perimeter before sitting.
  - **Resume Threshold**: Stand when Mana >= 90% and Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast baseline Root on add -> activate Sprint -> retreat to open guard tower.

### 7. Offensive Priorities
1. Emergency Add Root: Priority 60 (Reserved for incoming adds threatening the group)
2. Subterranean DD: Priority 10 (Only if all group members > 90% HP, mana > 60%, and called target < 15% HP)
3. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Self-Cast Str/Con & Dex/Qui Buffs: Priority 95 (Keep active at all times)
2. Out-of-Combat Group Buffs: Priority 90 (Spec/Base Str/Con, Dex/Qui, Health Regen)
3. Purge (AtlasOF_Purge): Priority 100 (Instant cleanse when hit by long-duration CC)
4. Mastery of Concentration: Priority 90 (When under heavy melee pressure and allies < 50% HP)
5. Cure Disease / Poison: Priority 85 (Cleanse group members immediately)

### 9. Target Priorities
1. Lowest Effective HP% Group Member: Priority 100
2. Group Member afflicted with Disease or Poison: Priority 95
3. Group Member focused by enemy assist train: Priority 90
4. Incoming Add threatening the backline: Priority 80 (For root application)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1250–1450 units
- **Spacing**: Maintain 400+ units separation from other support casters (Healer/Runemaster) to avoid clustered CC.

### 11. Ability Usage Priorities
1. `Purge` (AtlasOF_Purge): Priority 100 (Self CC cleanse)
2. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 90 (15 seconds of uninterruptible healing)
3. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 85 (Restore mana when below 25%)
4. `HealBig` / Major Spec Heal: Priority 80 (When target HP < 50%)
5. `HealEfficient` / Minor Heal: Priority 70 (When target HP between 50%–85%)

### 12. Resource Management
- **Conservative Mana Floor**: 30% (Reserve power for critical triage and resurrections)
- **Triage Thresholds**:
  - Target HP 70%–85%: `HealEfficient` (Minor Heal)
  - Target HP 35%–70%: `HealBig` (Major Heal)
  - Target HP < 35%: `Mastery of Concentration` + `HealBig`
  - 3+ members < 70%: `HealGroup`
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Monitor group crowd-control status and trigger `Purge` decisively.
- Pre-buff group with baseline stat buffs and health regeneration during rest periods.
- Cure disease on frontline tanks immediately.
- Use `Mastery of Concentration` when pressured by enemy melee.
- Follow pet pulling smart healing rules: wait for 5% mob health loss before first heal, use smallest heal, escalate only on massive damage.

### 14. What the AI SHOULD NOT Do
- Never melee or switch to hammer combat during group engagements.
- Never use direct DD or DoT spells when party members are below 85% health.
- Never root the main kill target in PvE (damage immediately breaks the root).
- Never break camp anchor or run away to achieve maximum casting range during pet pulling.
- Never cast heals on the pet mid-pull before the incoming mobs reach camp and register 5% damage shield loss.

### 15. Target Selection Matrix
- **Condition: Ally HP < 35%**: Select Ally -> Cast `HealBig` (Escalate to MoC if interrupted).
- **Condition: Ally Afflicted with Disease**: Select Ally -> Cast `Cure Disease`.
- **Condition: Ally HP 35%–85%**: Select Lowest Ally HP% -> Cast `HealEfficient`.
- **Condition: 3+ Allies HP < 70%**: Cast `HealGroup`.
- **Condition: Add incoming at > 300 range**: Select Add -> Cast Root (Creeper).

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Announce low mana, activate `Mystic Crystal Lore`, suppress offensive rooting, cast only `HealEfficient` on critical allies.
- **Full OOM (0% Mana)**: Withdraw to maximum tether range (1500 units), drink power potion if available, call for peel.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildHealing` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildHealing` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildHealing` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfHealing` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfHealing` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AugDex` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 2: Augmentation Shaman
- **role**: Buffer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Augmentation"
  min_point_ratio: 0.50
  spec_type_aliases: ["AugShaman"]
```

### 1. Intended Role
Group Buffer, Endurance Battery, Secondary Healer, and Defensive Anchor.

### 2. Intended Role Summary
The Augmentation Shaman is the premier enhancement powerhouse of Midgard. It specializes in maximizing group Endurance Regeneration (Endo), Damage Add buffs, base and spec stat buffs (Str/Con, Dex/Qui, Acuity, AF, Resists), while providing essential secondary healing and emergency add rooting.

### 3. Combat Philosophy
Enrich and sustain the group. The Aug Shaman ensures Midgard's heavy melee frontline (Warriors, Berserkers, Thanes, Savages) never starves for endurance while maximizing their melee damage output. During combat, it assists the primary healer with spot heals and provides add-control roots on incoming threats.

### 4. Mode: RvR
- **Endurance & Buff Maintenance**: Keep group Endurance Regeneration and Damage Add active at all times. Re-buff fallen allies immediately upon resurrection.
- **Secondary Healing**: Spot-heal backline squishies (Healers, Runemasters) and frontline tanks under focused fire.
- **Add Disruption**: Root incoming enemy peelers or stealthers attempting to flank the caster line.
- **Durability**: Leverage high spec AF and passive resists to survive enemy melee train diversions.

### 5. Mode: PvP
- **Buff Preservation**: Keep self-buffs maximized to trade efficiently.
- **Endurance Pressure**: Maintain endurance regen so partner fighters can execute high-cost styles continuously.
- **Emergency Heals**: Interleave baseline heals when primary partner drops below 50% HP.

### 6a. Mode: PvE Standard
- **Endo Supply**: Ensure continuous uptime of spec Endurance Regeneration so melee party members can style without running dry.
- **Pre-Combat Buffing**: Cast full suite of base and spec buffs during setup phase.
- **Add Containment**: Execute `TryShamanPveAddRoot` to lock down roaming adds targeting the party.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused on the pet so that the pet caster's pulsing damage shield inflicts continuous damage.
  - Tanks and melee DPS do NOT attempt to pull aggro or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Aug Shaman roots the break-off add or spot-heals allies engaging it.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - The Aug Shaman **IS allowed to heal the pet**, but **MUST wait until the incoming mobs have lost around 5% of their life** (mobs at ~95% HP or below from damage shield contact hits) before casting its first direct heal, ensuring the mobs have anchored threat on the pet.
  - **Smart Healing (Best Smallest Heal)**: Cast the **best smallest heal** (`HealEfficient` / minor baseline heal) to sustain the pet while generating minimal healing aggro.
  - **Massive Damage Escalation**: If the pet takes massive spike damage, cast `HealBig` to stabilize.
  - Pre-cast Health Regeneration, Str/Con, and Dex/Qui buffs stay active on the pet prior to the pull.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.
  - The pet puller is 100% responsible for bringing the mobs directly into the stationary group so everyone can do their job.
- **Group Triage & 85% AoE Window**:
  - Maintain triage and endurance regen on group members.
  - At ~85% mob health, when AoE casters begin blasting the pack, maintain stationary support on party members.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Moderately Viable**.
  - **Dungeon Suitability**: **MODERATE SOLO SURVIVABILITY**. Top-tier self-spec buffs (Spec Str/Con, Dex/Quick) provide heavy melee durability in chain armor. Can solo single Blue/Yellow mobs in dungeon entrance corridors by buffing to cap and using Hammer/Shield styles.
  - **Corridor Pull Isolation**: Pull single mobs to hallway corners with baseline Cave DD; melee down safely.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Buffed melee defense ensures low damage taken.
  - **Secondary (Yellow)**: Standard target. Self-buffs make 1-on-1 melee combat very safe.
  - **Restricted (Orange)**: Only engage with full mana pool to cover self-heals.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure full suite of Spec Str/Con, Dex/Quick, and AF buffs are running.
  - 2. Opener: Baseline Cave DD pull from doorway.
  - 3. Melee Combat: Engage with One-Handed Hammer style chain; block with Shield.
  - 4. Self-Sustain: Cast minor baseline heal if Health drops < 60%.
  - 5. Finisher: Hammer execution style.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 30% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Baseline Root add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Emergency Add Root: Priority 65 (Root loose adds heading for party members)
2. Subterranean DD: Priority 10 (Only if party is stable > 90% HP and mana > 65%)
3. Melee Attack: Priority 0 (Strictly disabled)

### 8. Defensive Priorities
1. Group Endurance Regeneration Buff: Priority 100 (Maintain continuous uptime)
2. Damage Add Buff on Melee Allies: Priority 95 (Keep active on all melee party members)
3. Full Base/Spec Group Buffs: Priority 90 (Str/Con, Dex/Qui, AF, Resists)
4. Purge (AtlasOF_Purge): Priority 100 (Cleanse CC on self)
5. Spot Healing Critical Allies: Priority 85 (Target HP < 50%)

### 9. Target Priorities
1. Lowest Effective HP% Group Member: Priority 95
2. Friendly Main Tank / Melee DPS (for Endurance & Damage Add uptime): Priority 90
3. Incoming Add threatening the backline: Priority 85 (For root application)

### 10. Positioning Preferences
- **Anchor**: Midline / Backline
- **Target Range**: 1100–1350 units
- **Spacing**: Close enough to maintain buff range on frontline while remaining out of enemy melee reach.

### 11. Ability Usage Priorities
1. `Purge` (AtlasOF_Purge): Priority 100 (Self CC cleanse)
2. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 90 (Emergency uninterruptible healing)
3. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 85 (Power recovery)
4. Baseline `HealEfficient` / `HealBig`: Priority 75 (Secondary triage support)
5. `TryShamanPveAddRoot`: Priority 70 (Root incoming adds)

### 12. Resource Management
- **Mana Reserve Floor**: 25% (Reserve power for buff recasting and emergency heals)
- **Triage Thresholds**:
  - Target HP 50%–75%: Baseline `HealEfficient`
  - Target HP < 50%: Baseline `HealBig`
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Keep group Endurance Regeneration active 100% of the time during combat.
- Re-apply Damage Add to melee allies immediately after resurrection.
- Assist primary healer when multiple party members take heavy damage.
- Follow pet pulling smart healing rules: wait for 5% mob health loss before first heal, use smallest heal, escalate only on massive damage.

### 14. What the AI SHOULD NOT Do
- Never let Endurance Regeneration drop during prolonged encounters.
- Never engage in melee combat; stay focused on buffing, add control, and secondary healing.
- Never break camp anchor or run away to achieve maximum casting range during pet pulling.
- Never heal the pet mid-pull before the incoming mobs reach camp and register 5% damage shield loss.

### 15. Target Selection Matrix
- **Condition: Ally Endurance Buff Expired**: Select Ally -> Cast Endurance Buff.
- **Condition: Ally Damage Add Expired**: Select Melee Ally -> Cast Damage Add.
- **Condition: Ally HP < 50%**: Select Lowest Ally HP% -> Cast `HealBig`.
- **Condition: Add incoming at > 300 range**: Select Add -> Cast Root (Creeper).

### 16. Fallback Behavior
- **Low Mana (< 20%)**: Activate `Mystic Crystal Lore`, suspend secondary healing, conserve mana exclusively for maintaining Endurance Regeneration.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_Serenity` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfTheArcane` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfTheArcane` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 35]
10. `AtlasOF_AvoidanceOfMagic` (Rank 2) - 3 pts [Total: 38]
11. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 39]
12. `AtlasOF_Toughness` (Rank 2) - 3 pts [Total: 42]
13. `AtlasOF_AugCon` (Rank 1) - 1 pt [Total: 43]
14. `AtlasOF_AugCon` (Rank 2) - 3 pts [Total: 46]
15. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 47]
16. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 50]

---

## Specialization 3: Subterranean Shaman (Cave Shaman)
- **role**: CrowdControl
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Subterranean"
  min_point_ratio: 0.50
  spec_type_aliases: ["SubtShaman"]
```

### 1. Intended Role
Offensive Crowd Controller, AoE Disease Spreader, Debuffer, and Pressure Caster.

### 2. Intended Role Summary
The Subterranean Shaman (Cave Shaman) is one of Midgard's most devastating offensive tactical casters. It specializes in spreading Area-of-Effect Disease (halving enemy healing received by 50% and slowing combat speed), deploying AoE and single-target roots, applying stacking damage-over-time (DoT) spells, casting combat stat debuffs, and dropping the devastating `Ichor of the Deep` active Realm Ability.

### 3. Combat Philosophy
Offensive disruption and pressure. The Cave Shaman cripples enemy coordination by disabling enemy healers with AoE Disease, locking down enemy melee trains with AoE roots and `Ichor of the Deep`, and interrupting enemy casters with continuous DoTs and bolts. It maintains tactical distance while shifting the flow of battle in Midgard's favor.

### 4. Mode: RvR
- **AoE Disease Priority**: Cast Area-of-Effect Disease (`Plague` / `Epidemic`) immediately onto clustered enemy groups. Halving the enemy's incoming healing received and reducing their movement speed breaks their frontline sustain and neutralizes stealthers across wide areas.
- **Ichor of the Deep**: Deploy `Ichor of the Deep` (15 pt active RA) onto clustered enemy melee trains or caster lines to inflict massive ground-targeted direct damage and a 350-radius snare/root.
- **AoE Root Control**: Cast AoE Root (`Entangling Roots`) to separate the enemy frontline from their backline support. Avoid casting on targets currently being attacked by friendly melee to prevent breaking the root.
- **Caster Interruption**: Maintain single-target DoTs and baseline Bolt/DD on enemy casters to disrupt their cast bars continuously.
- **Stat Debuffing**: Apply spec Str/Con and Dex/Qui debuffs onto enemy main assists to weaken their physical damage output and casting speed.

### 5. Mode: PvP
- **Disease & DoT Kiting**: Apply Disease and DoT, root the opponent with Creeper, and maneuver out of melee range while peppering them with DD and bolts.
- **Counter-Interrupt**: Use `Mastery of Concentration` when collapsed upon to ensure AoE disease and root are applied.

### 6a. Mode: PvE Standard
- **Add Lockdown**: Prioritize rooting roaming adds that aggro onto the group using single-target root (Creeper), keeping them isolated until the primary target dies.
- **Offensive Rotation**: Once adds are controlled and group is stable, cast Disease on the main target, apply DoT, and cast DD/Bolt when recasts are up.
- **Secondary Healing**: Cast baseline heals if the group healer is incapacitated or out of mana.
- **Sit Between Heals**: When every party member is above 90% health and nothing needs healing, curing or casting, sit to keep power up. Stand to heal the moment anyone is at 90% or below, or when a monster turns on you.

### 6b. Mode: PvE Pet Pulling
- **Mobs Stay on the Pet (Damage Shield Does the Damage)**:
  - The mobs must stay focused on the pet so that the pet caster's pulsing damage shield inflicts continuous damage.
  - Tanks and melee DPS do NOT attempt to pull aggro or taunt the main pack off the pet.
- **Loose Mob Protection (Break-Off Protection)**:
  - If any mob breaks off from the pet and targets the group, tanks and other DPS can immediately attack and secure that loose mob to protect the group. The Cave Shaman roots the break-off add or assists in bursting it down.
- **Smart Healing for the Pet (The 5% Mob Life Lost Threshold)**:
  - If called to assist with pet healing, the Cave Shaman **MUST wait until the incoming mobs have lost around 5% of their life** (mobs at ~95% HP or below from damage shield contact hits) before casting its first direct heal.
  - Use the **best smallest heal** (`HealEfficient` / minor baseline heal) to sustain the pet while generating minimal healing aggro. Escalate to `HealBig` only on massive damage spikes.
- **Group Stays Put (Strict Stationary Discipline — No Running for Range)**:
  - Stay put at the camp anchor location alongside the group. Do **NOT** run away or kite backwards to achieve maximum casting range.
  - The pet puller is 100% responsible for bringing the mobs directly into the stationary group so everyone can do their job.
- **The 85% Mob Health AoE Trigger (AoE Disease & DoT Burn Window)**:
  - **PRIMARY OFFENSIVE TRIGGER**: Once the pet's pulsing damage shield whittles the incoming mob pack down to approximately **85% health**, the Cave Shaman is authorized to unleash its Area of Effect spells:
    - Cast **AoE Disease** (`Plague` / `Epidemic`) into the cluster around the pet to slow attack speeds and soften the entire pack.
    - Cast **AoE DoT / Stacking DoTs** onto the clustered mobs to accelerate the burn.
  - Maintain strict stationary positioning while channeling spells into the cluster surrounding the pet.
- **Sit Between Heals**: While the pack is on the pet and every party member is above 90% health, sit between heals to keep power up. Stand to heal the pet or a party member, or when a mob breaks off toward you.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Outside Open-World Leveling Strongly Prioritized; Dungeons Viable at Entrance Rooms**.
  - **Dungeon Suitability**: **HIGH OUTDOOR DOT/ROOT KITER**. The Cave Shaman excels at long-range outdoor DoT and Root kiting (Gotar, Raumarik). In dungeons, disease, single-target DoTs, and Cave DD nukes allow soloing single mobs at corridor entrances; however, deep dungeon corridors increase risk of spell interrupts on adds. Maintain outdoor leveling for maximum grinding efficiency.
  - **Corridor Pull Isolation**: Root target mob from doorway -> apply Disease and Cave DoT -> channel Cave DD while target is immobilized.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Dies rapidly under Disease + DoT + 2 Cave DD nukes.
  - **Secondary (Yellow)**: Standard target. Root locks mob in place while DoT ticks and DD drains HP.
  - **Restricted (Orange)**: Viable outdoors with full mana and long retreat lane.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Position at 1500 units in open field or dungeon doorway.
  - 2. Opener: Cast Subterranean Root on target.
  - 3. Affliction: Cast Subterranean Disease followed by primary DoT.
  - 4. Nuke: Channel Subterranean DD nukes while target crawls forward.
  - 5. Finisher: Rapid Cave DD or hammer style if mob reaches melee.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 25% or Health < 50%.
  - **Resume Threshold**: Stand when Mana >= 85%, Health >= 90%.
- **Emergency Escape Protocol**:
  - Cast AoE Root / Disease on adds -> Sprint retreat to open safe zone.

### 7. Offensive Priorities
1. AoE Disease (Plague / Epidemic): Priority 95 (Apply to enemy clusters at start of fight or at 85% mob HP during pet pull)
2. Ichor of the Deep (AtlasOF_Ichor): Priority 90 (Deploy on clustered enemy groups)
3. AoE / Single Root on Adds: Priority 85 (Lock down enemy melee or PvE adds)
4. DoT Application: Priority 80 (Keep DoT ticking on primary targets and enemy casters)
5. Subterranean DD / Bolt: Priority 70 (Direct damage nukes)
6. Stat Debuffs (Str/Con, Dex/Qui): Priority 65 (Debuff enemy main assist)

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100 (Instant CC cleanse on self)
2. Mastery of Concentration: Priority 90 (Power through interrupts to cast AoE root or disease)
3. Emergency Spot Heal: Priority 80 (When friendly healer or ally < 35% HP)
4. Self AF Buff / Resists: Priority 75 (Keep active at all times)

### 9. Target Priorities
1. Clustered Enemy Group (3+ targets): Priority 100 (For AoE Disease / Ichor)
2. Enemy Primary Healer / Support: Priority 95 (Apply Disease and DoT to halve heals and interrupt)
3. Incoming Add threatening the group: Priority 90 (For root lockdown)
4. Primary Assist Target: Priority 80 (For DoT, DD, and stat debuffs)

### 10. Positioning Preferences
- **Anchor**: Backline
- **Target Range**: 1250–1450 units
- **Spacing**: Maintain 400+ units separation from primary healers to avoid shared CC.

### 11. Ability Usage Priorities
1. `Purge` (AtlasOF_Purge): Priority 100 (Instant self CC cleanse)
2. `Ichor of the Deep` (AtlasOF_Ichor): Priority 95 (Ground-target snare/damage on clusters)
3. `Mastery of Concentration` (AtlasOF_MasteryOfConcentration): Priority 90 (15 seconds uninterruptible casting)
4. `Mystic Crystal Lore` (AtlasOF_MCL): Priority 85 (Power recovery when mana < 25%)
5. AoE Disease: Priority 80 (Spread 50% healing reduction debuff)
6. AoE / Single Root: Priority 75 (CC incoming targets)

### 12. Resource Management
- **Mana Reserve Floor**: 20% (Reserve power for emergency CC and spot heals)
- **AoE Conservation**: If mana drops below 30%, stop casting AoE DoTs and transition to high-efficiency single-target spells.
- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling: sit to regain power while every party member is above 90% health; stand to cast, when hit, or when the group moves)

### 13. What the AI SHOULD Do
- Open RvR engagements with AoE Disease on enemy caster/healer lines.
- Deploy `Ichor of the Deep` on clustered enemy groups to pin them in place.
- Root adds approaching the party at > 300 range.
- Follow pet pulling rules: unleash AoE Disease and DoTs once mobs hit the 85% health threshold; hold stationary camp discipline; never run for range.

### 14. What the AI SHOULD NOT Do
- Never cast AoE Root or single root on a target currently taking melee damage.
- Never burn mana on low-yield DD spells when high-priority targets lack Disease or DoT debuffs.
- Never break camp anchor or run away to achieve maximum casting range during pet pulling.
- Never cast heals or nukes into the mob pack mid-pull before the mobs arrive at camp and reach the 5% / 85% health thresholds.

### 15. Target Selection Matrix
- **Condition: Clustered Enemies (3+) without Disease**: Select Group Cluster -> Cast AoE Disease.
- **Condition: Clustered Enemies in Range**: Select Target Center -> Deploy `Ichor of the Deep`.
- **Condition: Un-crowd-controlled Add > 300 range**: Select Add -> Cast Root (Creeper).
- **Condition: Primary Target has No DoT**: Select Primary Target -> Cast DoT.
- **Condition: Primary Target has Disease + DoT**: Select Primary Target -> Cast DD / Bolt.
- **Condition: Friendly Healer HP < 30%**: Select Healer -> Cast Emergency `HealBig`.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Activate `Mystic Crystal Lore`, suppress offensive AoE spells, reserve mana exclusively for emergency add roots and spot heals.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_Ichor` (Rank 1) - 15 pts [Total: 20] (Ichor of the Deep - 15 pt signature Midgard active RA)
3. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 30]
4. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 35]
5. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 36]
6. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 39]
7. `AtlasOF_WildArcana` (Rank 1) - 1 pt [Total: 40]
8. `AtlasOF_WildArcana` (Rank 2) - 3 pts [Total: 43]
9. `AtlasOF_AugAcuity` (Rank 1) - 1 pt [Total: 44]
10. `AtlasOF_AugAcuity` (Rank 2) - 3 pts [Total: 47]
11. `AtlasOF_AugDex` (Rank 1) - 1 pt [Total: 48]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 49]
13. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 50]
