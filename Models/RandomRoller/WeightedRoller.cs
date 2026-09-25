using Console_RPG.Factories;
using Console_RPG.Models.Items;

namespace Console_RPG.Models.RandomRoller;

public static class WeightedRoller<T>
{
    private static readonly Random _random = new();

    public static T RollRandom(int currentDay, List<WeightedEntry> entries, Func<string, T> factory)
    {
        var weighted = entries
            .Select(entry => new
            {
                entry.Name,
                Weight = CalculateWeight(entry, currentDay)
            })
            .Where(x => x.Weight > 0)
            .ToList();

        if (weighted.Count == 0)
            throw new InvalidOperationException($"There are no {typeof(T).Name} available to roll.");

        int totalWeight = weighted.Sum(x => x.Weight);
        int roll = _random.Next(totalWeight);
        int cumulative = 0;
        foreach (var entry in weighted)
        {
            cumulative += entry.Weight;
            if (roll < cumulative)
                return factory(entry.Name);
        }
        return factory(weighted.Last().Name);
    }


    public static int CalculateWeight(WeightedEntry entry, int currentDay)
    {
        if (currentDay < entry.MinDayToAppear)
            return 0;

        int daysSinceUnlock = currentDay - entry.MinDayToAppear;
        int bonus = daysSinceUnlock * 2;

        return entry.BaseWeight + bonus;
    }
}