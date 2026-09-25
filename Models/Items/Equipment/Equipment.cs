using Console_RPG.Enums;
using Console_RPG.Models.Interfaces;

namespace Console_RPG.Models.Items;

public abstract class Equipment : Item, IEquipable
{
    
    public abstract EquipmentSlot Slot { get; }
    public StatBlock Stats { get; set; }
    
    public Equipment(string name, int price, int damage = 0, int health = 0, int defense = 0) : base(name, price)
    {
        Stats = new StatBlock
        {
            Damage = damage,
            MaxHealth = health,
            Defense = defense
        };

    }
}
