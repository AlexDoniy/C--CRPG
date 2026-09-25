using Console_RPG.Models.Interfaces;
using Console_RPG.Views;

namespace Console_RPG.Models.Items;

public class Inventory : IInventoryInfo, ITrading
{
    public List<InventorySlot> Slots { get; }
    private readonly Hero _hero;
    public int GoldBalance { get; private set; }


    public Inventory(Hero hero)
    {
        Slots = new List<InventorySlot>();
        _hero = hero;
    }

    public void AddItem(Item item, int quantity = 1)
    {
        InventorySlot existing = Slots.Find(slot => slot.Item.Name == item.Name);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            Slots.Add(new InventorySlot(item, quantity));
        }
    }

    public void DeleteItem(Item item, int quantity = 1)
    {
        InventorySlot existing = Slots.Find(slot => slot.Item.Name == item.Name);

        if (existing == null) return;

        if (existing.IsEquipped && item is IEquipable equipable)
        {
            Unequip(equipable);
        }

        existing.Quantity -= quantity;

        if (existing.Quantity <= 0)
            Slots.Remove(existing);
    }

    public void Use(IUsable item)
    {
        if (item == null) return;
        item.Use(_hero);
        DeleteItem((Item)item, 1);
    }

    public void Equip(IEquipable item)
    {
        if (item == null) return;

        Unequip(_hero.Equipment.GetValueOrDefault(item.Slot));

        _hero.Equipment[item.Slot] = item;
        InventorySlot slot = Slots.Find(slot => slot.Item.Name == item.Name);
        if (slot != null)
        {
            slot.IsEquipped = true;
        }
        
        _hero.RefreshMaxHealth();
    }

    public void Unequip(IEquipable item)
    {
        if (item == null) return;

        if (_hero.Equipment[item.Slot] == item)
        {
            _hero.Equipment[item.Slot] = null;

            InventorySlot existing = Slots.Find(slot => slot.Item.Name == item.Name);
            if (existing != null)
            {
                existing.IsEquipped = false;
            }
        }
        
        _hero.RefreshMaxHealth();
    }
    
    public bool SpendGold(int amount)
    {
        if (GoldBalance < amount) return false;
        GoldBalance -= amount;
        return true;
    }

    public void AddGold(int amount)
    {
        GoldBalance += amount;
    }
}