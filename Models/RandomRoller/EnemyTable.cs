namespace Console_RPG.Models.RandomRoller;

public static class EnemyTable
{
    public static readonly List<WeightedEntry> EquipableEntries = new()
    {
        new WeightedEntry("Goblin", 1, 0),
        new WeightedEntry("Wolf", 10, 3),
        new WeightedEntry("Orc", 100, 5),
        new WeightedEntry("Dragon", 1000, 10),
    };

}