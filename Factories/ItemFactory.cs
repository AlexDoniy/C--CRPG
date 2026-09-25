using Console_RPG.Models.Items;

namespace Console_RPG.Factories;

public class ItemFactory
{
    private static readonly Dictionary<string, Func<Item>> _items = new()
    {
        ["Wooden Sword"] = () => new Weapon("Wooden Sword", 15, damage: 5, health: 0, defense: 0),
        ["Iron Sword"] = () => new Weapon("Iron Sword", 40, damage: 10, health: 10, defense: 0),
        ["Obsidian Sword"] = () => new Weapon("Obsidian Sword", 130, damage: 20, health: 30, defense: 2),
        ["Orichalcum Sword"] = () => new Weapon("Orichalcum Sword", 300, damage: 30, health: 50, defense: 5),
        
        ["Wooden Armor"] = () => new Armor("Wooden Armor", 20, damage: 0, health: 50, defense: 4),
        ["Iron Armor"] = () => new Armor("Iron Armor", 45, damage: 0, health: 100, defense: 9),
        ["Obsidian Armor"] = () => new Armor("Obsidian Armor", 150, damage: 5, health: 200, defense: 14),
        ["Orichalcum Armor"] = () => new Armor("Orichalcum Armor", 375, damage: 7, health: 300, defense: 20),
        
        ["Wooden Helmet"] = () => new Helmet("Wooden Helmet", 12, damage: 0, health: 30, defense: 2),
        ["Iron Helmet"] = () => new Helmet("Iron Helmet", 35, damage: 0, health: 70, defense: 5),
        ["Obsidian Helmet"] = () => new Helmet("Obsidian Helmet", 110, damage: 2, health: 150, defense: 9),
        ["Orichalcum Helmet"] = () => new Helmet("Orichalcum Helmet", 230, damage: 5, health: 220, defense: 13),
        
        ["Wooden Ring"] = () => new Ring("Wooden Ring", 10, damage: 2, health: 0, defense: 1),
        ["Iron Ring"] = () => new Ring("Iron Ring", 30, damage: 5, health: 10, defense: 3),
        ["Obsidian Ring"] = () => new Ring("Obsidian Ring", 100, damage: 12, health: 70, defense: 5),
        ["Orichalcum Ring"] = () => new Ring("Orichalcum Ring", 220, damage: 18, health: 100, defense: 10),
        
        ["Small Healing Potion"] = () => new HealingPotion("Small Healing Potion", 18, 50),
        ["Medium Healing Potion"] = () => new HealingPotion("Medium Healing Potion", 32, 100),
        ["Large Healing Potion"] = () => new HealingPotion("Large Healing Potion", 60, 200),
    };

    public static Item Create(string name)
    {
        if (!_items.TryGetValue(name, out var factory))
            throw new ArgumentException($"Item {name} not found in allowed items list.");
        return factory();
    }

    public static IEnumerable<Item> GetAll() => _items.Values.Select(f => f());


}