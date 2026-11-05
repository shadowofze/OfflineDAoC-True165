# SmartAI: smarter bots and companions

SmartAI is an optional brain for your hired companions and the world's gamebots. With it on, each bot fights the
way its class and specialization are meant to be played: a Rejuvenation Cleric keeps the group alive, a tank guards
its healer, a Wizard quick-casts when something reaches it.

SmartAI is **in testing** and **off by default**. It changes only the bots' choices. They follow every player rule,
and no combat number changes: damage, healing, costs, ranges and reuse timers are exactly a player's.

## Turning it on and off

1. In the launcher, open the **Options** tab.
2. Tick **Use SmartAI** (untick it to go back).
3. Start the server (restart it if it is running).

With it off, every bot plays exactly as before. SmartAI also protects your game:

- If SmartAI hits a problem with one bot, that bot uses the normal bot AI for a short while and then tries again.
- If problems keep happening, SmartAI turns itself off for the rest of the session.
- If the server stops unexpectedly while SmartAI is on, the launcher offers to turn it off.

## What changes

**Every class.** Each of the 39 classes has a strategy file that describes its specializations. A bot plays the
specialization it has trained most. A companion's specialization comes only from the points you train. You can
edit these files to change how a class plays: see [SMARTAI-CLASS-FILES.md](SMARTAI-CLASS-FILES.md).

**Healers**

- Choose their heal by how hurt someone is: a small heal for a scratch, a big heal for a member in danger, a group
  heal when several are hurt.
- An Enhancement Cleric heals instead of smiting when nobody else in the group is healing.
- Use Mastery of Concentration when a monster reaches them, and Bunker of Faith when the group takes heavy damage (if
  their specialization uses those realm abilities).
- Sit between heals in a fight against monsters to keep their power up, while everyone in the group is above 90%
  health and nothing is attacking them (Mentalist, Druid, Healer, Shaman, Cleric, Friar, Warden and Bard, except
  the melee specializations). They stand to heal, when hit, or when you move.

**Tanks**

- Keep Guard and Protect on the group's healer when their specialization calls for it.
- With 7 or more in Shields, face an archer that is shooting them, Engage it, and walk in with the shield up.

**Melee**

- Use their class abilities: Berserk (not below 40% health), the Hero's Stag, the Blademaster's Triple Wield and
  Flurry, the Mercenary's Dirty Tricks and Flurry.
- They don't waste a long cooldown on a monster that is nearly dead.
- Stealthers open from stealth, and melee bots use side and back styles when they are in position.

**Casters**

- Turn an instant spell on an enemy that is casting, to interrupt it.
- The list casters (Wizard, Sorcerer, Theurgist, Cabalist, Runemaster, Spiritmaster, Bonedancer, Eldritch, Enchanter,
  Mentalist, Animist) QuickCast their next spell when something is attacking them, and a Mentalist its heal. As for
  a player, it costs double power, and there is a 30-second wait before the next one. No other class QuickCasts.

**Resting.** Healer and caster companions sit to regain power when it is below 90% and nobody in the group is
fighting. If you move more than 800 units away, they get up and follow you.

**The group**

- Bots in a group share one view of the fight: the main assist, the target, and who is hurt.
- Bots report trouble, and the group protects its healer.
- Companions can call out in party chat (at most one callout every few seconds).

**PvE, PvP and RvR.** Bots tell monsters from enemy players. Against players they go for healers and crowd
controllers first.

## Personalities

A personality tilts how a companion weighs attacking against healing and protecting. It never breaks the rules of
its specialization: an Aggressive Cleric still heals first when its spec says so, and nobody breaks a mez.

| Personality | Plays |
|---|---|
| Auto | as its specialization's default (the normal setting) |
| Aggressive | more attacking, less defense, a small power reserve |
| Defensive | more healing and protecting; peels attackers off the healer like a tank |
| Tactical | balanced, and focuses on the group's target |
| Opportunistic | goes after enemies that are nearly dead |
| Supportive | puts healing first, keeps a large power reserve |
| Conservative | careful, keeps a large power reserve |

To set one, target your companion and type `/smartai personality defensive`, or say
`<name> personality defensive` near it. `/smartai personality` on its own shows the current one. Each companion
remembers its personality.

## Pet pulling

Type `/petpull` before you send your pet in to gather monsters:

- Your companions leave the monsters on your pet to it until they drop to 85% health.
- Then casters and healers join, with area spells only.
- Melee only fights monsters that break away toward the group.
- Your pet gets your healers' smallest heal once a monster on it has taken a little damage.
- Nobody walks out to the pack or the pet.

Type `/petpull` again (or `/petpull off`) to stop. It works with `/defensive` and `/aggressive`.

## Where gamebot groups level

With SmartAI on, autonomous gamebot groups choose their camps and travel more carefully:

- **Pull level.** A gear score (armour and weapon against the wearer's level; the tank counts double), the group's
  level, size, tank, healer and recent wipes set the hardest con colour the group pulls.
- **Zones first.** Groups prefer zones whose typical monster is close to what they can pull.
- **Area search.** Every camp is checked for aggressive monsters of other kinds around it. A camp inside a
  hard monster's aggro range is picked much less often.
- **Learning.** Groups remember their kills and deaths per camp, and where members died on the road. Deadly
  camps and areas are picked less, safe ones more. Old results fade (half of their weight a week by default).
- **Travel.** A new group meets at the town nearest its leader and walks to the camp together, instead of each
  member walking alone to a town near the camp. A member walking alone (to the meeting point, back after a death,
  or far behind the leader) goes around aggressive monsters on its route. The leader keeps extra room for the
  members beside it.
- **Binding.** At the meeting town each member binds at the town's bind stone, as a player would, and a group
  member who releases goes back to that bind.
- **After a death.** A bot raised beside aggressive monsters first walks out of their aggro. When one member dies
  but at least half the party is still together, the survivors wait and rest at a safe spot nearby while the fallen
  walk back; only a worse loss sends the party to a town to regroup.

Two files in `runtime/server/state-165` hold this. Delete either one to start over:

- `smartai-world.json`: the zone level ranges and the settings. Edit `settings`, or add your own range or
  `"skip": true` for a zone under `overrides`. The server rewrites only `generated` and picks up your edits
  without a restart.
- `smartai-learning.json`: what the groups learned.

## Tools and reporting problems

Target a companion or bot and type:

| Command | Shows |
|---|---|
| `/smartai status` | whether SmartAI is on, the loaded class files, and this session's log file |
| `/smartai spec` | which specialization the bot fights as, and why |
| `/smartai group` | the group's main assist, target, strategy and wounded |
| `/smartai debug` | the bot's last SmartAI decisions (`/smartai debug 32` for more) |
| `/smartai debuggroup` | records every bot group's finished fights, one file per group (see below) |

**Recording how groups fight.** `/smartai debuggroup` records every group that has a bot in it (`/smartai debuggroup
mine` records only your group, or your target's). Each group gets its own file in
`runtime/logs/smart_ai_groups`. A fight is written only once it is over: the whole group, pets included, has been
out of combat for a few seconds, and no mesmerized or rooted enemy is still near. A fight cut short (the group
breaks up, you turn recording off, the server stops) is dropped rather than written in part. Each fight has:

- the group: every member's name, kind (player, hired, gamebot), class, level, SmartAI spec, role, personality, and
  health and power at the start and end;
- the time since the last fight, how it was spent (resting, the leader moving, other), and the leader's goal for
  gamebot groups;
- a timeline: SmartAI decisions when they change (with the options it weighed), every member's spells (players
  too) as they land, melee styles with where the attacker stood (front, side or back), the group's target calls,
  deaths, sitting;
- melee: each member's swings by position, the styles used, styles that did not fire, and their parries, blocks and
  evades with the reactive styles that followed (a bot that knew a reactive style and didn't use it is marked `!`);
- a summary per bot, and flags such as a caster meleeing (marked `!`, with its last decision) or a bot that did
  nothing.

`/smartai debuggroup status` shows what is recorded; `/smartai debuggroup off` stops. Recording stays on until you
turn it off or the server stops.

Every `/smartai` command and its answer are saved to `runtime/logs/smartai-session-<server start time>.log`. If a bot does
something odd, run `/smartai debug` on it right away, then send that log file with a short description of what
happened.

Monsters don't fight back against a GM. To test fights, play with a normal character: untick **Make Me a GM** in the
launcher. The `/smartai` tools work for normal players.

## Not yet in SmartAI

- Bots don't walk around to flank, spread out against area damage, or kite.
- Solo and dungeon play use the same choices as group play.