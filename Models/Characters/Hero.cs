using Console_RPG.Enums;
using Console_RPG.Factories;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;

namespace Console_RPG.Models;

public class Hero : Character
{
    private Weapon _weapon { get; set; }
    public Inventory Inventory { get;}
    public Dictionary<EquipmentSlot, IEquipable?>? Equipment {get;}

    public override StatBlock TotalStats
    {
        get
        {
            var total = new StatBlock
            {
                Damage = BaseStats.Damage,
                Defense = BaseStats.Defense,
                MaxHealth = BaseStats.MaxHealth
            };

            foreach (var item in Equipment)
            {
                if (item.Value != null)
                {
                    total.Add(item.Value.Stats);
                }
            }
            return total;
        }
    }
    
    private Hero(string name) : base(name)
    {
        Equipment = new Dictionary<EquipmentSlot, IEquipable>
        {
            { EquipmentSlot.Weapon, null },
            { EquipmentSlot.Armor, null },
            { EquipmentSlot.Helmet, null },
            { EquipmentSlot.Ring, null }
        };

        Inventory = new Inventory(this);
    }

    public override void Attack(ITakeDamage target)
    {
        target.TakeDamage(TotalStats.Damage);
    }
    
    public static Hero CreateNew(string name = "Hero")
    {
        Hero hero = new Hero(name);
        hero.Inventory.AddItem(ItemFactory.Create("Small Healing Potion"));
        hero.Inventory.AddGold(50);
        return hero;
    }

    public static Hero CreateEmpty(string name)
    {
        return new Hero(name);
    }
    
}