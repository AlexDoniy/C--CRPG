using System.Reflection.Metadata;

namespace Console_RPG.Models.Interfaces;

public interface IUsable
{
    public void Use(Hero hero);
    public string Name {
        get;
    }
}