using System.Reflection.Metadata;
using Console_RPG.Models.Interfaces;

namespace Console_RPG.Models.Items;

public class HealingPotion : Potion
{
    private int HealPoints { get; set; }

    public HealingPotion(string name = "Heal potion", int price = 10, int healPoints = 50) : base(name, price)
    {
        HealPoints = healPoints;
    }
    
    public override void Use(Hero hero)
    {
        hero.Heal(HealPoints);
    }
}