# Console RPG

A turn-based console RPG built in C# (.NET) from scratch as a learning project, focused on clean architecture, SOLID principles, and idiomatic C#.

## Features

- **Turn-based combat** — attack, use items mid-battle, or attempt to flee
- **Inventory system** — stackable items, equip/unequip, drop items
- **Equipment** — weapons and armor modify the hero's total stats (damage, defense, max health)
- **Shop** — buy and sell items for gold, with a sell-price coefficient
- **Weighted loot system** — items and enemies are rolled using weighted randomness, with rarer entries becoming more common as in-game days pass
- **Save/load system** — JSON serialization to disk, with a startup menu to continue an existing save, start a new game, or delete the save
- **Post-battle rewards** — random gold and item drops, plus healing on victory as a new day begins

## Architecture

The project follows a layered structure:

```
Models/        Game entities: Hero, Enemy, Item, Weapon, Potion, Inventory, GameState
Controllers/   Game flow and user input: GameController, BattleController,
               ShopController, InventoryController, SaveController
Views/         Console output formatting: CoreView
Factories/     Centralized object creation: ItemFactory, EnemyFactory
Data/          Static tables: LootTable, EnemyTable, WeightedEntry
```

Key design choices:

- **Interfaces over concrete types** — `IUsable`, `IEquipable`, `ICharacterInfo`, `IInventoryInfo` decouple game logic from specific implementations
- **Dependency injection via constructors** — controllers receive only the dependencies they actually need (e.g. `GameState` is only passed to controllers that use it)
- **Command–query separation** — stat calculations (`TotalStats`) are side-effect-free; state changes happen through explicit methods
- **Factories over direct instantiation** — items and enemies are created by name through `ItemFactory` / `EnemyFactory`, keeping content data separate from game logic
- **Generic weighted roller** — a single `WeightedRoller<T>` class drives both loot and enemy generation, parameterized by a factory delegate
- **DTOs for persistence** — save data is converted to plain, cycle-free objects before serialization to avoid circular references and polymorphism issues

## Tech Stack & Architecture

Built with a focus on clean architecture, extensibility, and maintainable C# code.

- **Language:** C#
- **Platform:** .NET
- **Serialization:** `System.Text.Json`, using plain DTOs (`SaveData`) to avoid circular references (`Hero` ↔ `Inventory`) and polymorphism issues (items are restored by name via `ItemFactory` rather than deserialized directly)
- **Data querying:** LINQ for filtering, projecting, and aggregating collections (loot tables, inventory, equipment)

### Applied Design Patterns & Principles

- **Factory pattern** — `ItemFactory` / `EnemyFactory` centralize object creation by name, decoupling game logic from concrete constructors
- **Strategy via generics** — `WeightedRoller<T>` drives both loot and enemy generation, parameterized by a factory delegate (`Func<string, T>`)
- **Dependency injection via constructors** — controllers receive only the dependencies they use (e.g. `GameState` is passed only where the current day matters)
- **Command–query separation** — stat calculations (`TotalStats`) are pure and side-effect-free; state changes go through explicit methods (`RefreshMaxHealth`, `TrySpendGold`)
- **Interface segregation** — `IUsable`, `IEquipable`, `ICharacterInfo`, `IInventoryInfo` expose narrow contracts instead of full concrete classes
- **DTO pattern** — save data is converted to simple, flat objects before serialization, separate from the live game model

## Getting started

```bash
git clone <repo-url>
cd Console_RPG
dotnet run
```

## Possible future improvements

- Character leveling and experience
- More equipment slots and item rarity tiers
- Multiple enemy encounters per battle
- Configurable loot tables loaded from external JSON
