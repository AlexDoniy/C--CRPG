using Console_RPG.Models.Items;

namespace Console_RPG.Models.Interfaces;

public interface ICharacterInfo
{
    public string Name { get; }
    public StatBlock TotalStats { get; }
    public int Health { get; }
}