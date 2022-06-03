# REFACTORS


- [ ] Currently, clients of TaaontiaCore receive instanciated DB models which gives them access to waaaaay too many DB-related things.
A future refactor should include DTOs to encapsulate the core from the rest

- [ ] Remove Events completely. Instead, make a StatsService or something similar coupled with a Stats database modal to handle all things stat-related on-the-fly.

- [ ] Hardcore mode: a player can register using `!register hardcore` to create a hardcore character.
An hardcore enables multiple things:
  - An hardcore character will permanently die when it inevitably happens
  - To "migrate" a character to hardcore, they'll have to visit an altar thing to sacrifice themselves. Doing so will create a new hardcore-enabled character at level 1, keeping a bit of stats from their previous selves.
  - Every X level, a player can make the choice to "sacrifice" its hardcore character, making them return to level 1, while keeping some stats, equiments, of buffs they have acquired during their run.
    The amount of kept depends of the number of "sacrified" levels".

- [ ] Stats

- [ ] ACHIEVEMENTS !!!!