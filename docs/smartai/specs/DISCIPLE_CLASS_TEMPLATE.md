---
schema_version: 1
class_name: "Disciple"
class_id: 20
realm: "Albion"
primary_stats: ["Intelligence", "Dexterity"]
default_personality: "Tactical"
---

# Class: Disciple (Albion) - base class

## Shared Class Capabilities
- **Levels**: 1 to 5. At level 5 a Disciple chooses its class (Necromancer), which has its own file.
- **Armor**: Cloth.
- **Weapons**: Staff.
- **Lines**: `Deathsight`, `Painworking`, `Death Servant`. One specialization per line; the line with the most trained points decides, and the first one plays when nothing decides yet.
- Written 2026-11-05 for SmartAI (owner: "build the brains for the base classes"). The rules are improvised from how the full classes of the same archetype play; no combat number changes.

---

## Specialization 1: Deathsight Disciple
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Deathsight"
  min_point_ratio: 0.40
  spec_type_aliases: []
  fallback: true             # plays this when no line decides (nothing past 1, or a tie)
```

### 1. Intended Role
Young caster (levels 1-5) leaning on Deathsight, before the class choice at level 5.
### 2. Intended Role Summary
Damage at range with the Deathsight spells it has, from behind the front line. A small power pool: every spell counts.
### 3. Combat Philosophy
Cast on the group target from range; when power is gone, swing the staff rather than stand idle, then rest after the fight.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Cast at the attacker; do not chase.
### 6a. Mode: PvE Standard
- Cast on the group target once it is held; wait at range while a spell recharges.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels (a Death Servant pet stays on the group target); fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters, opened from range; rest below half power.
### 7. Offensive Priorities
1. Deathsight Direct Damage on Group Target: Priority 90
2. Deathsight Damage over Time or Debuff on Group Target: Priority 70
3. Staff Melee Attack: Priority 20 (out of power)
### 8. Defensive Priorities
- Step back to the group when a monster turns on the caster; keep casting if the cast will land.
### 9. Target Priorities
1. Group target: Priority 90
### 10. Positioning Preferences
- Behind the front line, at spell range.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Power**: A tiny pool at these levels; rest to full between pulls.
### 13. What the AI SHOULD Do
- Cast on the group target, the strongest spell that is ready first.
- Wait at range while spells recharge.
### 14. What the AI SHOULD NOT Do
- Do not walk into melee while it has power.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Ranged damage on the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 2: Painworking Disciple
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Painworking"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young caster (levels 1-5) leaning on Painworking, before the class choice at level 5.
### 2. Intended Role Summary
Damage at range with the Painworking spells it has, from behind the front line. A small power pool: every spell counts.
### 3. Combat Philosophy
Cast on the group target from range; when power is gone, swing the staff rather than stand idle, then rest after the fight.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Cast at the attacker; do not chase.
### 6a. Mode: PvE Standard
- Cast on the group target once it is held; wait at range while a spell recharges.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels (a Death Servant pet stays on the group target); fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters, opened from range; rest below half power.
### 7. Offensive Priorities
1. Painworking Direct Damage on Group Target: Priority 90
2. Painworking Damage over Time or Debuff on Group Target: Priority 70
3. Staff Melee Attack: Priority 20 (out of power)
### 8. Defensive Priorities
- Step back to the group when a monster turns on the caster; keep casting if the cast will land.
### 9. Target Priorities
1. Group target: Priority 90
### 10. Positioning Preferences
- Behind the front line, at spell range.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Power**: A tiny pool at these levels; rest to full between pulls.
### 13. What the AI SHOULD Do
- Cast on the group target, the strongest spell that is ready first.
- Wait at range while spells recharge.
### 14. What the AI SHOULD NOT Do
- Do not walk into melee while it has power.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Ranged damage on the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 3: Death Servant Disciple
- **role**: OffensiveCaster
- **default_personality**: Tactical

```yaml
match_specs:
  primary_line: "Death Servant"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young caster (levels 1-5) leaning on Death Servant, before the class choice at level 5.
### 2. Intended Role Summary
Damage at range with the Death Servant spells it has, from behind the front line. A small power pool: every spell counts.
### 3. Combat Philosophy
Cast on the group target from range; when power is gone, swing the staff rather than stand idle, then rest after the fight.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Cast at the attacker; do not chase.
### 6a. Mode: PvE Standard
- Cast on the group target once it is held; wait at range while a spell recharges.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels (a Death Servant pet stays on the group target); fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters, opened from range; rest below half power.
### 7. Offensive Priorities
1. Death Servant Direct Damage on Group Target: Priority 90
2. Death Servant Damage over Time or Debuff on Group Target: Priority 70
3. Staff Melee Attack: Priority 20 (out of power)
### 8. Defensive Priorities
- Step back to the group when a monster turns on the caster; keep casting if the cast will land.
### 9. Target Priorities
1. Group target: Priority 90
### 10. Positioning Preferences
- Behind the front line, at spell range.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Power**: A tiny pool at these levels; rest to full between pulls.
### 13. What the AI SHOULD Do
- Cast on the group target, the strongest spell that is ready first.
- Wait at range while spells recharge.
### 14. What the AI SHOULD NOT Do
- Do not walk into melee while it has power.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Ranged damage on the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).
