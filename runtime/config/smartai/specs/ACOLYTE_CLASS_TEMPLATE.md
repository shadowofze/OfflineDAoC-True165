---
schema_version: 1
class_name: "Acolyte"
class_id: 16
realm: "Albion"
primary_stats: ["Piety", "Constitution"]
default_personality: "Supportive"
---

# Class: Acolyte (Albion) - base class

## Shared Class Capabilities
- **Levels**: 1 to 5. At level 5 a Acolyte chooses its class (Cleric or Friar), which has its own file.
- **Armor**: Leather and studded.
- **Weapons**: Crushing weapons and staves.
- **Lines**: `Rejuvenation`, `Enhancement`. One specialization per line; the line with the most trained points decides, and the first one plays when nothing decides yet.
- Written 2026-11-05 for SmartAI (owner: "build the brains for the base classes"). The rules are improvised from how the full classes of the same archetype play; no combat number changes.

---

## Specialization 1: Rejuvenation Acolyte
- **role**: PrimaryHealer
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Rejuvenation"
  min_point_ratio: 0.40
  spec_type_aliases: []
  fallback: true             # plays this when no line decides (nothing past 1, or a tie)
```

### 1. Intended Role
Young support (levels 1-5) leaning on Rejuvenation, before the class choice at level 5.
### 2. Intended Role Summary
Keeps the group alive with its first heals and buffs, and adds its weapon to the fight when nobody needs healing.
### 3. Combat Philosophy
Heal first, buff when quiet, hit the group target when the group is healthy.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Heal itself and its group; fight back on the attacker.
### 6a. Mode: PvE Standard
- Heal whoever is lowest; otherwise melee the group target.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; play as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters; heal itself between swings when below half health.
### 7. Offensive Priorities
1. Melee Attack on Group Target: Priority 40 (group HP > 75%)
### 8. Defensive Priorities
1. Group Buff upkeep: Priority 50 (group HP > 90%)
### 9. Target Priorities
1. Lowest HP Group Member: Priority 100 (group HP < 85%)
### 10. Positioning Preferences
- Just behind the front line, within heal range of everyone.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Power**: Keep enough for one more heal; rest to full between pulls.
### 13. What the AI SHOULD Do
- Heal the lowest member before anything else.
- Keep its buffs up between fights.
### 14. What the AI SHOULD NOT Do
- Do not melee while a member is low.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Heal the group; buffs between fights.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 2: Enhancement Acolyte
- **role**: HybridSupport
- **default_personality**: Supportive

```yaml
match_specs:
  primary_line: "Enhancement"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young support (levels 1-5) leaning on Enhancement, before the class choice at level 5.
### 2. Intended Role Summary
Keeps the group alive with its first heals and buffs, and adds its weapon to the fight when nobody needs healing.
### 3. Combat Philosophy
Heal first, buff when quiet, hit the group target when the group is healthy.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Heal itself and its group; fight back on the attacker.
### 6a. Mode: PvE Standard
- Heal whoever is lowest; otherwise melee the group target.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; play as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters; heal itself between swings when below half health.
### 7. Offensive Priorities
1. Melee Attack on Group Target: Priority 40 (group HP > 75%)
### 8. Defensive Priorities
1. Group Buff upkeep: Priority 50 (group HP > 90%)
### 9. Target Priorities
1. Lowest HP Group Member: Priority 100 (group HP < 85%)
### 10. Positioning Preferences
- Just behind the front line, within heal range of everyone.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Power**: Keep enough for one more heal; rest to full between pulls.
### 13. What the AI SHOULD Do
- Heal the lowest member before anything else.
- Keep its buffs up between fights.
### 14. What the AI SHOULD NOT Do
- Do not melee while a member is low.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Heal the group; buffs between fights.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).
