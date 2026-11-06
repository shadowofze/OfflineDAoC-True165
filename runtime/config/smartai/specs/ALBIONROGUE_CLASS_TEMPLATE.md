---
schema_version: 1
class_name: "AlbionRogue"
class_id: 17
realm: "Albion"
primary_stats: ["Dexterity", "Quickness"]
default_personality: "Opportunistic"
---

# Class: Rogue (Albion) - base class

## Shared Class Capabilities
- **Levels**: 1 to 5. At level 5 a Rogue chooses its class (Infiltrator, Minstrel or Scout), which has its own file.
- **Armor**: Leather.
- **Weapons**: Slashing and thrusting weapons.
- **Lines**: `Slash`, `Thrust`. One specialization per line; the line with the most trained points decides, and the first one plays when nothing decides yet.
- Written 2026-11-05 for SmartAI (owner: "build the brains for the base classes"). The rules are improvised from how the full classes of the same archetype play; no combat number changes.

---

## Specialization 1: Slash Rogue
- **role**: StealthMeleeDPS
- **default_personality**: Opportunistic

```yaml
match_specs:
  primary_line: "Slash"
  min_point_ratio: 0.40
  spec_type_aliases: []
  fallback: true             # plays this when no line decides (nothing past 1, or a tie)
```

### 1. Intended Role
Young rogue (levels 1-5) leaning on Slash, before the class choice at level 5.
### 2. Intended Role Summary
Damage from the side or back of the group target with Slash styles; stealth while travelling.
### 3. Combat Philosophy
Let the front line take the monster, then hit from a flank. Light armour cannot hold a monster for long.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Fight back on the attacker; do not chase.
### 6a. Mode: PvE Standard
- Assist the group target; positional styles from the side or back when they are trained.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters, one at a time; rest below half health.
### 7. Offensive Priorities
1. Slash Weapon Style on Group Target: Priority 90
2. Melee Attack on Group Target: Priority 80
### 8. Defensive Priorities
- Step back to the group when a monster turns on the rogue.
### 9. Target Priorities
1. Group target: Priority 90
### 10. Positioning Preferences
- Side or back of the group target.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Endurance**: Styles cost endurance; rest between pulls when it is below a quarter.
### 13. What the AI SHOULD Do
- Attack the group target, not loose monsters nobody holds.
### 14. What the AI SHOULD NOT Do
- Do not pull for the group.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Add damage on the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 2: Thrust Rogue
- **role**: StealthMeleeDPS
- **default_personality**: Opportunistic

```yaml
match_specs:
  primary_line: "Thrust"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young rogue (levels 1-5) leaning on Thrust, before the class choice at level 5.
### 2. Intended Role Summary
Damage from the side or back of the group target with Thrust styles; stealth while travelling.
### 3. Combat Philosophy
Let the front line take the monster, then hit from a flank. Light armour cannot hold a monster for long.
### 4. Mode: RvR
- Not expected below level 5.
### 5. Mode: PvP
- Fight back on the attacker; do not chase.
### 6a. Mode: PvE Standard
- Assist the group target; positional styles from the side or back when they are trained.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue monsters, one at a time; rest below half health.
### 7. Offensive Priorities
1. Thrust Weapon Style on Group Target: Priority 90
2. Melee Attack on Group Target: Priority 80
### 8. Defensive Priorities
- Step back to the group when a monster turns on the rogue.
### 9. Target Priorities
1. Group target: Priority 90
### 10. Positioning Preferences
- Side or back of the group target.
### 11. Ability Usage Priorities
- No realm abilities below level 5.
### 12. Resource Management
- **Endurance**: Styles cost endurance; rest between pulls when it is below a quarter.
### 13. What the AI SHOULD Do
- Attack the group target, not loose monsters nobody holds.
### 14. What the AI SHOULD NOT Do
- Do not pull for the group.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Add damage on the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).
