using Console_RPG.Models.Interfaces;

namespace Console_RPG.Models.Items;

public abstract class Potion : Item, IUsable
{
    public abstract void Use(Hero hero);

    public Potion(string name, int price) : base(name, price)
    {
    }
    
    
    
}