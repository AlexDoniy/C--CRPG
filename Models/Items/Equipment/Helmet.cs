using Console_RPG.Enums;
using Console_RPG.Models.Interfaces;

namespace Console_RPG.Models.Items;

public class Helmet(string name, int price, int damage = 0, int health = 0, int defense = 0)
    : Equipment(name, price, damage, health, defense)
{
    public override EquipmentSlot Slot => EquipmentSlot.Helmet;
}