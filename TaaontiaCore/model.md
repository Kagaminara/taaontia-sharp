# Taaontia database model

## Player

- Guid Id - _Id_
- ulong RemoteId - _remote id_
- string Name - _Name_
- int Health - _Current character health_
- int MaxHealth - _Maximum character health (default 10)_
- int Energy - _Current character energy_
- int MaxEnergy - _Maximum character energy (default 10)_
- int Level - _Level_
- int Experience - _Total experience points_
- ICollection<Skill> Skills - _List of available skills_
- ICollection<Fight> Fights - _List of fights the character is or has participated in_


## Events

Make a list of event in player for stats.
Make the `Event` class a non-model that is solely used to communicate with client.