---
schema_version: 1
class_name: "Bonedancer"
class_id: 30
realm: "Midgard"
primary_stats: ["Piety", "Dexterity"]
default_personality: "Tactical"
---

# Class: Bonedancer (Midgard)

## Shared Class Capabilities
- **Armor Type**: Cloth.
- **Weapon Slots**: Two-Handed Staff.
- **Core Specialization Lines**:
  - `Bone Army`: Bone Commander Pet Summoning (summons a central skeletal commander who in turn summons up to three skeletal melee or archer minions), Body Direct Damage, and Body resistance debuffs.
  - `Darkness`: High-impact Darkness Lifedrain (deals damage while healing caster), Darkness Snare, Darkness resistance debuffs, and Caster Minion Summons (skeletal minions that cast ranged lifedrains and snares).
  - `Suppression`: Area Damage-over-Time (AoE DoT), Suppression Direct Damage, Stat Debuffs, and Mending Healer Minion Summons (skeletal minions that cast group heals and health regeneration buffs).
- **Base Abilities**: Sprint, Quick Cast.
- **Midgard's Undead Necromancer**: The Bonedancer commands an autonomous squad of skeletal minions, combining pet swarms with heavy lifetaps, devastating AoE DoTs, and autonomous minion healing.

---

## Specialization 1: Bone Army Bonedancer
- **role**: PetCommander
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Bone_Army"
  min_point_ratio: 0.50
  spec_type_aliases: ["ArmyBone"]
```

### 1. Intended Role
Undead Squad Commander, Skeletal Melee/Archer Swarm Controller, Body Direct Damage Nuker, and Break-Off Interceptor.

### 2. Intended Role Summary
The Bone Army Bonedancer operates as a one-man strike team. By summoning a central Skeletal Commander who commands multiple skeletal warrior and archer retainers, the Bonedancer overwhelms enemy positions with physical pet swarms, softens targets with Body debuffs, and executes enemies with heavy Body DD.

### 3. Combat Philosophy
Pet swarm harassment and physical pressure. In RvR, the Bonedancer sends skeletal swarms to disrupt enemy casters and tie down support. In PvE pet pulling, the Bonedancer maintains strict minion control at camp, commanding minions to intercept loose adds and assisting with Body DD once the pack is stabilized at 85% health.

### 4. Mode: RvR
- **Minion Swarm Attack**: Order Skeletal Commander and minion squad to swarm enemy primary healers or cloth casters, delivering non-stop melee interrupts and ranged archery.
- **Body DD Assist**: Assist Midgard nukers with Body Direct Damage on debuffed focus targets.
- **Minion Screen**: Keep skeletal minions positioned between enemy stealthers and Midgard support.
- **Positioning**: 1200â€“1350 units back in the Midgard caster line.

### 5. Mode: PvP
- **Swarm & Drain**: Command minions to engage opponent -> Quick Cast Body DD -> Apply Body debuff -> Overwhelm target while minions absorb hits.

### 6a. Mode: PvE Standard
- **Pet Off-Tanking**: Utilize Skeletal Commander and minions to off-tank secondary mobs on large pulls; assist main assist with Body DD.

### 6b. Mode: PvE Pet Pulling (Minion Add Interception & Burst Role)
- **Role During Pet Pulling**: The Bone Army Bonedancer is **NOT the primary puller** when in a group with a designated puller (e.g. Spiritmaster). It acts as **stationary pet support and break-off peeler**.
- **Crucial Rule: Minions Set to PASSIVE at Camp**:
  - Keep the Skeletal Commander and all minions strictly on **PASSIVE** at camp during the pull.
  - If minions are on *Defensive* or *Aggressive*, they will charge the incoming pull pack, split mob pathing, and pull aggro off the main pet!
- **Break-Off Add Protection (Minion Intercept)**:
  - If any mob breaks away from the main pet toward the group healers, **immediately command the Skeletal Commander to attack that loose add**!
  - The skeletal minions will swarm the loose add, peel it off the healer, and hold it in place while the Bonedancer burns it down.
- **THE 85% MOB HEALTH TRIGGER**:
  - Once the main pet's pulsing damage shield brings the mob cluster down to **85% health**, the Bonedancer unleashes:
  - Command minions to attack the primary focus target within the cluster.
  - Cast **Body Direct Damage** to rapidly eliminate targets one by one!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor alongside the group. Do NOT run away or seek range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **SUPREME SOLO DUNGEON PET RETINUE**. The Bonedancer commands a Commander pet plus a retinue of sub-pets (healers, archers, or melee minions). In dungeons (Spindelhalla, Cursed Tomb, Varv), this skeleton army acts as an impassable wall in narrow corridors. The Bonedancer positions the Commander and minions at the corridor threshold, sends them to engage, and casts Bone Army DoTs, debuffs, and bone shards from behind with complete immunity to melee interrupts.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Non-stop minion grinding; sub-pets kill target with zero downtime.
  - **Secondary (Yellow)**: Standard 1-on-1 baseline. Commander pet easily holds aggro.
  - **Restricted (Orange)**: Fully viable; skeleton retinue absorbs damage while Bonedancer assists.
  - **Prohibited (Red / Purple)**: Red cons are soloable with full minion retinue; avoid Purple solo.
- **Pull & Engagement Sequence**:
  - 1. Ensure Commander pet and both sub-pets are summoned and buffed.
  - 2. Opener: Command Commander pet to /pet attack target.
  - 3. Swarm: Sub-pets immediately follow Commander and begin striking/casting on mob.
  - 4. Bone Shards & DoT: Apply Bone Army DoT; cast Bone Shard DD from 600 units behind retinue.
  - 5. Sustain: Cast pet mend/heal on Commander if health drops < 60%.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit behind skeleton army when Mana < 20% or Health < 50%.
  - **Safety Check**: Minions defend caster from pathing patrols while sitting.
  - **Resume Threshold**: Stand when Mana >= 80%.
- **Emergency Escape Protocol**:
  - Command skeleton army to /pet attack add -> Bonedancer sprints safely down cleared corridor.

### 7. Offensive Priorities
1. Minion Command on Loose Break-Off Add: Priority 100
2. Minion Swarm on Enemy Healer / Caster (RvR): Priority 95
3. Body Direct Damage on Focus Target: Priority 85
4. Body Resistance Debuff: Priority 80

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95
3. Minion Intercept on Attacking Melee: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group healers: Priority 100 (For Minion Intercept / Nuke)
2. Primary Assist Focus Target: Priority 90 (For Minion Attack / Body DD at 85% HP)
3. Enemy Support / Casters: Priority 80 (For Minion Harassment in RvR)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1200 units from target pack)
- **Minions**: Stationed on Passive alongside the Bonedancer at camp
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. Minion Attack Command: Priority 90 (On loose add or at 85% mob health)
3. Body Direct Damage: Priority 85
4. Body Resistance Debuff: Priority 80
5. `AtlasOF_MCL`: Priority 75

### 12. Resource Management
- **Pet Summoning Reserves**: Re-summoning the Skeletal Commander costs high mana; avoid summoning mid-fight unless safe.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Keep Skeletal Commander and minions on Passive at camp during pet pulling.
- Order minions immediately onto any loose mob that breaks away toward healers.
- Assist with Body DD on the pet's cluster once mobs hit 85% health.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let minions charge incoming mobs while they are traveling to camp.
- Never panic and attempt to re-summon pets mid-combat while holding aggro.
- Never flee backwards out of camp when mobs arrive at the pet.

### 15. Target Selection Matrix
- **Condition: Loose add breaks toward healer**: Select Add -> Command Minions Attack -> Cast Body DD.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Focus Target -> Command Minions Attack -> Cast Body DD.
- **Condition: Enemy healer casting in RvR**: Select Healer -> Command Minions Attack.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease direct damage casting; let skeletal minions deal sustained physical DPS while conserving power.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 2: Darkness Bonedancer
- **role**: OffensiveCaster
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Darkness"
  min_point_ratio: 0.50
  spec_type_aliases: ["DarkBone"]
```

### 1. Intended Role
Darkness Lifedrain Burst Nuker, Self-Sustaining Artillery, Caster Minion Commander, and Darkness Debuffer.

### 2. Intended Role Summary
The Darkness Bonedancer combines devastating magical burst with extreme personal durability. It casts high-damage Darkness Lifedrains that siphon health directly into its own pool, commands skeletal Caster Minions that mirror its ranged lifedrain barrage, and cripples targets with Darkness snares and debuffs.

### 3. Combat Philosophy
Self-sustaining magical annihilation. The Darkness Bonedancer softens targets with resistance debuffs, coordinates with its caster minions to unleash synchronized lifedrains, and absorbs incoming damage through constant health siphoning.

### 4. Mode: RvR
- **Lifedrain Focus**: Coordinate with Midgard nukers; apply Darkness debuff to primary kill target and melt them with spec Darkness Lifedrains.
- **Caster Minion Barrage**: Position caster minions at range to fire auxiliary lifedrains and snares into the enemy lines.
- **Snare Defense**: Apply Darkness Snare to enemy peelers charging Midgard casters.
- **Positioning**: 1200â€“1350 units back.

### 5. Mode: PvP
- **Snare & Drain**: Cast Snare -> Quick Cast Darkness Lifedrain -> Deploy Caster Minion -> Repeat Lifedrain to burn target while sustaining full health.

### 6a. Mode: PvE Standard
- **Single-Target Drain**: Apply Darkness debuff on main tank's target; burst with Lifedrain; sustain self through stray damage.

### 6b. Mode: PvE Pet Pulling (Lifedrain Burst & Break-Off Defense)
- **Role During Pet Pulling**: The Darkness Bonedancer is **NOT the puller**. It acts as **stationary single-target burst artillery**.
- **Hold Fire on Pull**: Strictly hold all lifedrains, snares, and debuffs while the pet puller is bringing the mobs to camp.
- **Minions on Passive**: Keep caster minions on Passive at camp so they do not cast on mobs in transit.
- **Break-Off Add Protection**:
  - If a loose mob breaks off from the pet toward the group, cast **Darkness Snare** on it or burn it down immediately with **Darkness Lifedrain**.
- **THE 85% MOB HEALTH BURN TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to **85% health**, the Darkness Bonedancer activates:
  - Cast **Darkness Resistance Debuff** on the primary focus target.
  - Follow immediately with heavy **spec Darkness Lifedrain** casts, commanding caster minions to assist on the target!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor alongside the group. Do NOT run away or seek range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT LIFETAP PET SUSTAIN**. Darkness Bonedancers utilize heavy lifetaps and suppression debuffs while the Commander pet tanks. In dungeon hallways, lifetap spells recover any lost health instantly from complete safety behind the pet wall.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Fast kills; lifetaps keep caster at 100% HP.
  - **Secondary (Yellow)**: Standard baseline target.
  - **Restricted (Orange)**: Viable; lifetap replenishes caster while pet absorbs hits.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Park Commander at corridor bottleneck.
  - 2. Opener: Send Commander pet to attack.
  - 3. Darkness Lifetap: Channel Darkness Lifetap from 600 units behind pet.
  - 4. Finisher: Rapid lifetap to execute target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%. Lifetaps ensure health remains high.
  - **Resume Threshold**: Resume at Mana >= 85%.
- **Emergency Escape Protocol**:
  - Command pet to hold add -> Sprint retreat into cleared corridor.

### 7. Offensive Priorities
1. Darkness Resistance Debuff: Priority 90 (Cast on primary target before burst)
2. Spec Darkness Lifedrain: Priority 85 (Primary nuke rotation at 85% mob HP)
3. Darkness Snare on Break-Off Add: Priority 80
4. Caster Minion Assist Command: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Mastery of Concentration: Priority 95 (Cast Lifedrains uninterrupted while focused)
3. Darkness Lifedrain for Self-Heal: Priority 90 (When own HP drops below 70%)
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Loose Break-off Mob threatening group: Priority 100 (For Snare / Drain)
2. Primary Assist Kill Target: Priority 90 (For Darkness Debuff -> Lifedrain)
3. Enemy Caster / Support: Priority 75 (For RvR burst)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from mob cluster)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Darkness Resistance Debuff: Priority 90
4. Spec Darkness Lifedrain: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **Lifedrain Efficiency**: Lifedrains provide built-in healing, preserving healer mana.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Hold fire completely during pet pulling until the mob pack hits 85% health.
- Apply Darkness debuff before nuking focus targets.
- Snare or lifedrain loose break-off mobs to protect group healers.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never cast lifetaps on incoming mobs while the pet is pulling them to camp.
- Never flee backwards out of camp to seek casting range.
- Never let caster minions roam freely on aggressive mode during pulls.

### 15. Target Selection Matrix
- **Condition: Add breaks off toward healer**: Select Add -> Cast Darkness Snare -> Lifedrain.
- **Condition: Mob Pack at Pet & HP <= 85%**: Select Primary Target -> Cast Darkness Debuff -> Spam Lifedrain.
- **Condition: Own HP < 60%**: Select Current Target -> Cast Quick Cast Lifedrain.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease debuffing; cast only baseline lifetaps when strictly needed for self-preservation.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]

---

## Specialization 3: Suppression Bonedancer
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Suppression"
  min_point_ratio: 0.50
  spec_type_aliases: ["SuppBone"]
```

### 1. Intended Role
Area Damage-over-Time (AoE DoT) Bombardier, Mending Healer Minion Commander, Suppression Direct Damage Nuker, and Attrition Specialist.

### 2. Intended Role Summary
The Suppression Bonedancer is Midgard's ultimate attrition caster. It blankets enemy forces in heavy Area of Effect Damage-over-Time (AoE DoT) afflictions, commands specialized skeletal Mending Healer Minions that cast autonomous heals and buffs on group members, and chips away at targets with Suppression Direct Damage.

### 3. Combat Philosophy
Mass attrition paired with autonomous group healing. The Suppression Bonedancer spreads debilitating AoE DoTs across clustered enemies, uses its healer minions to bolster Midgard's health recovery, and applies steady spell pressure.

### 4. Mode: RvR
- **AoE DoT Carpet**: Blanket clustered enemy zergs, choke points, and keep doors with AoE Suppression DoTs to unstealth enemies and drain enemy healer power.
- **Healer Minion Support**: Station healer minions in the Midgard backline to provide automatic single-target heals to injured casters and healers.
- **Suppression DD Assist**: Assist Midgard nukers with Suppression DD against softened targets.
- **Positioning**: 1350 units back.

### 5. Mode: PvP
- **DoT & Attrition**: Cast AoE/Single DoT -> Order healer minion to heal self -> Suppression DD rotation -> Outlast opponent through sustained minion heals.

### 6a. Mode: PvE Standard
- **Multi-Mob Cleave**: Apply AoE DoT across multi-mob pulls once tanks establish threat; let healer minions handle secondary party chip damage.

### 6b. Mode: PvE Pet Pulling (The Healer Minion & AoE DoT Role)
- **Role During Pet Pulling**: The Suppression Bonedancer is **NOT the puller**. It acts as the **AoE DoT specialist and minion healing support**.
- **Crucial Rule: Healer Minions on PASSIVE During Pull**:
  - **Set Healer Minions to PASSIVE at camp while the pull is underway**.
  - If healer minions cast a heal on the pulling pet while it is running back to camp, **the entire mob pack will bypass the pet and sprint directly into camp**!
  - Only allow healer minions to heal after the pack arrives at camp and mobs have lost ~5% health.
- **Break-Off Add Protection**:
  - If any mob breaks away from the pet toward the healers, cast **Suppression DD** or single-target DoT on it to peel it away.
- **THE 85% MOB HEALTH AoE DoT TRIGGER**:
  - Once the pet's pulsing damage shield whittles the incoming mob cluster down to approximately **85% health**, the Suppression Bonedancer unleashes:
  - Cast **AoE Suppression DoT** directly onto the cluster anchored around the pet!
  - Follow up with **Suppression DD** on the primary focus target within the pack.
  - The AoE DoT will tick down all mobs simultaneously alongside the damage shield!
- **Strict Stationary Discipline**:
  - Remain strictly stationed at the camp anchor alongside the group. Do NOT run away or seek range.

### 6c. Mode: Solo Leveling & Dungeon Strategy
- **Environment & Zone Preference**:
  - **Zone Priority**: **Both Dungeons and Outdoors Fully Viable (DUNGEON SOLO EXCEPTION)**.
  - **Dungeon Suitability**: **EXCELLENT SKELETAL HEALER RETINUE**. Suppression Bonedancers summon skeletal healer minions that continuously cast heals on the Commander pet and the Bonedancer, creating an un-killable front-line tank in dungeon corridors.
- **Target Con Selection**:
  - **Optimal (Blue / High Blue)**: Zero-damage grinding; healer minions top off pet continuously.
  - **Secondary (Yellow)**: Standard target.
  - **Restricted (Orange)**: Viable due to healer minion sustain.
  - **Prohibited (Red / Purple)**: Avoid solo.
- **Pull & Engagement Sequence**:
  - 1. Summon Commander + Healer sub-pet.
  - 2. Opener: Send Commander to engage target.
  - 3. Support: Cast Suppression debuffs and DD while minion heals Commander.
  - 4. Finisher: Commander finishes target.
- **Downtime & Rest Protocol**:
  - **Sit / Rest Threshold**: Sit when Mana < 20%.
  - **Resume Threshold**: Stand when Mana >= 85%.
- **Emergency Escape Protocol**:
  - Minions intercept add -> Sprint away to safety.

### 7. Offensive Priorities
1. AoE Suppression DoT on Mob Cluster (at 85% HP): Priority 95
2. Suppression Direct Damage on Focus Target: Priority 85
3. Single-Target DoT: Priority 80
4. Single-Target Peeling DD on Break-Off Add: Priority 75

### 8. Defensive Priorities
1. Purge (AtlasOF_Purge): Priority 100
2. Healer Minion Healing (After mobs lose 5% HP at camp): Priority 95
3. Mastery of Concentration: Priority 90
4. Quick Cast: Priority 80

### 9. Target Priorities
1. Mob Cluster at Pet (at 85% HP): Priority 100 (For AoE DoT)
2. Loose Break-off Mob threatening group: Priority 95 (For DD Peel)
3. Primary Focus Target: Priority 80 (For Suppression DD)

### 10. Positioning Preferences
- **Anchor**: Midline / Group Camp Center (1000â€“1350 units from target pack)
- **Pull Phase**: Stationary at camp anchor; never retreat out of camp.

### 11. Ability Usage Priorities
1. `AtlasOF_Purge`: Priority 100
2. `AtlasOF_MasteryOfConcentration`: Priority 95
3. Spec AoE Suppression DoT: Priority 90 (At 85% mob health)
4. Suppression Direct Damage: Priority 85
5. `AtlasOF_MCL`: Priority 80

### 12. Resource Management
- **AoE DoT Efficiency**: High damage-per-power efficiency.
- **MCL Timing**: Pop `AtlasOF_MCL` when power drops below 25%.

### 13. What the AI SHOULD Do
- Keep Healer Minions on Passive during the pull phase so they do not generate transit aggro.
- Hold AoE DoT until the mob pack hits 85% health at camp.
- Cast AoE Suppression DoT onto the anchored mob cluster at 85% health to accelerate cleave speed.
- Maintain stationary camp discipline during pet pulling.

### 14. What the AI SHOULD NOT Do
- Never let healer minions heal the pet while it is still running back to camp.
- Never cast AoE DoTs or single DoTs on mobs in transit to camp.
- Never flee backwards out of camp to seek casting range.

### 15. Target Selection Matrix
- **Condition: Mob Cluster at Pet & HP <= 85% & No DoT Active**: Cast AoE Suppression DoT.
- **Condition: Loose add breaks toward healer**: Select Add -> Cast Suppression DD.
- **Condition: Mob Cluster at Pet & DoT Active**: Select Primary Focus Target -> Cast Suppression DD.

### 16. Fallback Behavior
- **Low Mana (< 15%)**: Cease direct damage; rely on existing ticking DoTs and let healer minions support party health.

### 17. Suggested Realm Ability Progression Order
Budget: 50 Realm Ability Points (Old Frontiers / Classic 1.65 Rules)
1. `AtlasOF_Purge` (Rank 1) - 5 pts [Total: 5]
2. `AtlasOF_MasteryOfConcentration` (Rank 1) - 10 pts [Total: 15]
3. `AtlasOF_MCL` (Rank 1) - 5 pts [Total: 20]
4. `AtlasOF_MasteryOfMagery` (Rank 1) - 1 pt [Total: 21]
5. `AtlasOF_MasteryOfMagery` (Rank 2) - 3 pts [Total: 24]
6. `AtlasOF_MasteryOfMagery` (Rank 3) - 6 pts [Total: 30]
7. `AtlasOF_WildPower` (Rank 1) - 1 pt [Total: 31]
8. `AtlasOF_WildPower` (Rank 2) - 3 pts [Total: 34]
9. `AtlasOF_WildPower` (Rank 3) - 6 pts [Total: 40]
10. `AtlasOF_MasteryOfTheArt` (Rank 1) - 1 pt [Total: 41]
11. `AtlasOF_MasteryOfTheArt` (Rank 2) - 3 pts [Total: 44]
12. `AtlasOF_Serenity` (Rank 1) - 1 pt [Total: 45]
13. `AtlasOF_Serenity` (Rank 2) - 3 pts [Total: 48]
14. `AtlasOF_AvoidanceOfMagic` (Rank 1) - 1 pt [Total: 49]
15. `AtlasOF_Toughness` (Rank 1) - 1 pt [Total: 50]
