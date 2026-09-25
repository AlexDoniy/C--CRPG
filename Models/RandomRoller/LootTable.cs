
namespace Console_RPG.Models.RandomRoller;

public static class LootTable
{
    public static readonly List<WeightedEntry> EquipableEntries = new()
    {
        new WeightedEntry("Wooden Sword", 1, 0),
        new WeightedEntry("Wooden Armor", 1, 0),
        new WeightedEntry("Wooden Helmet", 1, 0),
        new WeightedEntry("Wooden Ring", 1, 0),
        
        
        new WeightedEntry("Iron Sword", 3, 0),
        new WeightedEntry("Iron Armor", 3, 0),
        new WeightedEntry("Iron Helmet", 3, 0),
        new WeightedEntry("Iron Ring", 3, 0),
        
        
        new WeightedEntry("Obsidian Sword", 10, 5),
        new WeightedEntry("Obsidian Armor", 10, 5),
        new WeightedEntry("Obsidian Helmet", 10, 5),
        new WeightedEntry("Obsidian Ring", 10, 5),
        
        new WeightedEntry("Orichalcum Sword", 30, 10),
        new WeightedEntry("Orichalcum Armor", 30, 10),
        new WeightedEntry("Orichalcum Helmet", 30, 10),
        new WeightedEntry("Orichalcum Ring", 30, 10),
        
    };
    
    public static readonly List<WeightedEntry> UsableEntries = new()
    {
        new WeightedEntry("Small Healing Potion", 100, 0),
        new WeightedEntry("Medium Healing Potion", 30, 0),
        new WeightedEntry("Large Healing Potion", 10, 0),
    };

}