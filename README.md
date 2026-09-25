# Console RPG

A turn-based console RPG written in C#. Explore the shop, gear up your hero, fight enemies that scale with in-game days, and save/load your progress between sessions.

## Features

- **Turn-based battles** — attack, use items, or attempt to flee from enemies that get tougher (and more rewarding) as days pass
- **Equipment system** — four slots (Weapon, Armor, Helmet, Ring) with stat bonuses (damage, defense, max health)
- **Inventory management** — use, equip/unequip, or discard items
- **Shop** — buy and sell items, with stock that refreshes daily and scales with progression
- **Weighted loot & enemy tables** — random rolls weighted by item/enemy rarity and unlocked over time
- **Save/Load** — JSON-based save system with a startup menu (continue / new game / delete save)

## Project structure

```
Console_RPG/
├── Controllers/       # Game flow and input handling (Battle, Hero, Inventory, Save, Shop, Game)
├── Models/            # Domain models (Hero, Enemy, Character, Inventory, Shop, GameState, StatBlock)
│   ├── Items/          # Item hierarchy (Equipment: Weapon/Armor/Helmet/Ring, Potion)
│   ├── Interfaces/      # Contracts (IAttack, IEquipable, IUsable, ITrading, ICharacterInfo, ...)
│   ├── RandomRoller/    # Weighted random tables for loot and enemies
│   └── Save/            # Save DTOs and Hero <-> save-data conversion
├── Factories/          # Whitelisted factories for creating items and enemies by name
├── Views/              # Console output (CoreView)
└── Program.cs           # Entry point
```

## Getting started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)

### Run

```bash
dotnet run --project Console_RPG
```

### Build

```bash
dotnet build
```

## How to play

On launch, you'll land in the main menu:

```
1. Your Hero      — view stats and equipped gear
2. Open Inventory — use, equip/unequip, or throw away items
3. Battle         — fight a random enemy
4. Shop           — buy and sell items
8. Save game
9. Delete save
```

Progress is saved to `save.json` in the working directory. On startup, if a save file is found, you'll be offered to continue, start a new game (overwriting the save), or delete the save.

## Notes

- Loot, shop stock, and enemy encounters are drawn from weighted tables that shift as `CurrentDay` advances, so later days bring stronger (and rarer) items and enemies.
- Items and enemies are created through name-based factories (`ItemFactory`, `EnemyFactory`), which keeps save data restricted to a known, whitelisted set of content.

## License

Add your preferred license here (e.g. MIT).
