---
schema_version: 1
class_name: "Viking"
class_id: 35
realm: "Midgard"
primary_stats: ["Strength", "Constitution"]
default_personality: "Aggressive"
---

# Class: Viking (Midgard) - base class

## Shared Class Capabilities
- **Levels**: 1 to 5. At level 5 a Viking chooses its class (Warrior, Berserker, Savage, Thane or Skald), which has its own file.
- **Armor**: Leather and studded at these levels.
- **Weapons**: Swords, axes and hammers, small shields.
- **Lines**: `Sword`, `Axe`, `Hammer`. One specialization per line; the line with the most trained points decides, and the first one plays when nothing decides yet.
- Written 2026-11-05 for SmartAI (owner: "build the brains for the base classes"). The rules are improvised from how the full classes of the same archetype play; no combat number changes.

---

## Specialization 1: Sword Viking
- **role**: DefensiveTank
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Sword"
  min_point_ratio: 0.40
  spec_type_aliases: []
  fallback: true             # plays this when no line decides (nothing past 1, or a tie)
```

### 1. Intended Role
Young front-line fighter (levels 1-5) leaning on Sword, before the class choice at level 5.
### 2. Intended Role Summary
Takes the monster's attention in a small group and kills it with Sword styles. Has no taunts or shield styles yet beyond what training gives; wears the best armour it owns.
### 3. Combat Philosophy
Stand in front, hit the group target, use every style trained. Low-level fights are short, so endurance matters more than tricks.
### 4. Mode: RvR
- Not expected below level 5. If attacked, fight back on the group target.
### 5. Mode: PvP
- Fight back on the attacker with styles; do not chase.
### 6a. Mode: PvE Standard
- Hold the pulled monster at the group, face it away from the casters, and use styles on cooldown.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue and yellow monsters, one at a time; rest below half health or a quarter endurance.
### 7. Offensive Priorities
1. Sword Weapon Style on Group Target: Priority 90
2. Melee Attack on Group Target: Priority 80
### 8. Defensive Priorities
- Stay between the monster and the group's casters.
- Use a shield if one is equipped; nothing to cast.
### 9. Target Priorities
1. Monster on a group caster or support: Priority 95
2. Group target: Priority 90
### 10. Positioning Preferences
- Front of the group, in melee range of the group target.
### 11. Ability Usage Priorities
- No realm abilities below level 5. Sprint only to get away.
### 12. Resource Management
- **Endurance**: Styles cost endurance; rest between pulls when it is below a quarter.
### 13. What the AI SHOULD Do
- Engage the group target first and keep it busy.
- Use trained styles whenever they are ready.
### 14. What the AI SHOULD NOT Do
- Do not charge monsters the group has not pulled.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Hold the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 2: Axe Viking
- **role**: DefensiveTank
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Axe"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young front-line fighter (levels 1-5) leaning on Axe, before the class choice at level 5.
### 2. Intended Role Summary
Takes the monster's attention in a small group and kills it with Axe styles. Has no taunts or shield styles yet beyond what training gives; wears the best armour it owns.
### 3. Combat Philosophy
Stand in front, hit the group target, use every style trained. Low-level fights are short, so endurance matters more than tricks.
### 4. Mode: RvR
- Not expected below level 5. If attacked, fight back on the group target.
### 5. Mode: PvP
- Fight back on the attacker with styles; do not chase.
### 6a. Mode: PvE Standard
- Hold the pulled monster at the group, face it away from the casters, and use styles on cooldown.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue and yellow monsters, one at a time; rest below half health or a quarter endurance.
### 7. Offensive Priorities
1. Axe Weapon Style on Group Target: Priority 90
2. Melee Attack on Group Target: Priority 80
### 8. Defensive Priorities
- Stay between the monster and the group's casters.
- Use a shield if one is equipped; nothing to cast.
### 9. Target Priorities
1. Monster on a group caster or support: Priority 95
2. Group target: Priority 90
### 10. Positioning Preferences
- Front of the group, in melee range of the group target.
### 11. Ability Usage Priorities
- No realm abilities below level 5. Sprint only to get away.
### 12. Resource Management
- **Endurance**: Styles cost endurance; rest between pulls when it is below a quarter.
### 13. What the AI SHOULD Do
- Engage the group target first and keep it busy.
- Use trained styles whenever they are ready.
### 14. What the AI SHOULD NOT Do
- Do not charge monsters the group has not pulled.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Hold the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).

---

## Specialization 3: Hammer Viking
- **role**: DefensiveTank
- **default_personality**: Aggressive

```yaml
match_specs:
  primary_line: "Hammer"
  min_point_ratio: 0.40
  spec_type_aliases: []
```

### 1. Intended Role
Young front-line fighter (levels 1-5) leaning on Hammer, before the class choice at level 5.
### 2. Intended Role Summary
Takes the monster's attention in a small group and kills it with Hammer styles. Has no taunts or shield styles yet beyond what training gives; wears the best armour it owns.
### 3. Combat Philosophy
Stand in front, hit the group target, use every style trained. Low-level fights are short, so endurance matters more than tricks.
### 4. Mode: RvR
- Not expected below level 5. If attacked, fight back on the group target.
### 5. Mode: PvP
- Fight back on the attacker with styles; do not chase.
### 6a. Mode: PvE Standard
- Hold the pulled monster at the group, face it away from the casters, and use styles on cooldown.
### 6b. Mode: PvE Pet Pulling
- No pets in the group at these levels; fight as in 6a.
### 6c. Mode: Solo Leveling & Dungeon Strategy
- Blue and yellow monsters, one at a time; rest below half health or a quarter endurance.
### 7. Offensive Priorities
1. Hammer Weapon Style on Group Target: Priority 90
2. Melee Attack on Group Target: Priority 80
### 8. Defensive Priorities
- Stay between the monster and the group's casters.
- Use a shield if one is equipped; nothing to cast.
### 9. Target Priorities
1. Monster on a group caster or support: Priority 95
2. Group target: Priority 90
### 10. Positioning Preferences
- Front of the group, in melee range of the group target.
### 11. Ability Usage Priorities
- No realm abilities below level 5. Sprint only to get away.
### 12. Resource Management
- **Endurance**: Styles cost endurance; rest between pulls when it is below a quarter.
### 13. What the AI SHOULD Do
- Engage the group target first and keep it busy.
- Use trained styles whenever they are ready.
### 14. What the AI SHOULD NOT Do
- Do not charge monsters the group has not pulled.
- Do not break a mesmerized monster.
### 15. Group Responsibilities
- Hold the group target.
### 16. Fallback Behavior
- Below 30% health with the group in trouble: move back toward the group's support.
### 17. Realm Ability Progression Order
None: base classes have no realm abilities (the full class at level 5 uses its own file).
