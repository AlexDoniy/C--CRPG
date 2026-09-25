namespace Console_RPG.Models.RandomRoller;

public class WeightedEntry
{
    public string Name { get; }
    public int BaseWeight { get; }
    public int MinDayToAppear { get; }
    
    
    public WeightedEntry(string name, int weight, int day)
    {
        Name = name;
        BaseWeight = weight;
        MinDayToAppear = day;
    }
}
