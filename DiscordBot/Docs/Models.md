# Models

## Character related models

### Character

Describes a user's character

#### Fields

- `id - [pk]`
- `username - [string]`
- `health - [int]`
- `mana - [int]`
- `level - [int]`
- `experience - [int]`

### Fiend

Describe a fiend.

#### Fields

- `id - [pk]`
- `name - [string]`
- `health - [int]`
- `mana - [int]`
- `level - [int]`
- `fiendType - [FiendType]`

### FiendType

Describe a fiend type / template

#### Fields

- `id - [pk]`
- `name - [string]`
- `description - [string]`
- `baseHealth - [int]`
- `baseEnergy - [int]`

## Combat related models

### Fight

Describes a fight that has happened or is happening.
There should be only one active fight per user at any time.

#### Fields

- `id - [pk]`
- `allies - [fk Character[]]` list of users engaged in the fight
- `ennemies - [fk Fiend[]]` List of fiends engaged in the fight
- `events - [fk FightEvent[]]` List of events related to the fight
- `is_active - [boolean]` Wether the combat has finished
- `is_global - [boolean]` Wether the combat is global ("world boss-like")

### FightEvent

Describes a event during a fight, be it an attack, a parry, a lost or gain of Health points, etc.

#### Fields

- `id - [pk]`
- `fight - [fk Combat]` Related fight
- `type - [EFightEventType]` Type of event, described by the `EFightEventType` enum
- `value - [number]` The amount of whatever thing (related to `type`)

# Ideas

Remove events completely, and have a "stat" tables that auto-update with each actions