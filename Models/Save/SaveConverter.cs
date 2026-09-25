using Console_RPG.Enums;
using Console_RPG.Factories;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;

namespace Console_RPG.Models.Save;

public static class SaveConverter
{
    public static HeroSaveData ToSaveData(Hero hero)
    {
        var data = new HeroSaveData
        {
            Name = hero.Name,
            Health = hero.Health,
            Gold = hero.Inventory.GoldBalance,
        };

        foreach (var slot in hero.Inventory.Slots)
        {
            data.InventoryItems.Add(new ItemSaveData
            {
                Name = slot.Item.Name,
                Quantity = slot.Quantity,
                IsEquipped = slot.IsEquipped
            });
        }

        foreach (var kv in hero.Equipment)
        {
            if (kv.Value != null)
            {
                data.Equipment[kv.Key.ToString()] = kv.Value.Name;
            }
        }

        return data;
    }
    
    public static Hero FromSaveData(HeroSaveData data)
    {
        Hero hero = Hero.CreateEmpty(data.Name);
        hero.SetHealth(data.Health);

        foreach (var itemData in data.InventoryItems)
        {
            Item item = ItemFactory.Create(itemData.Name);
            hero.Inventory.AddItem(item, itemData.Quantity);
        }

        hero.Inventory.AddGold(data.Gold);

        foreach (var kv in data.Equipment)
        {
            EquipmentSlot slot = Enum.Parse<EquipmentSlot>(kv.Key);

            InventorySlot? inventorySlot = hero.Inventory.Slots
                .Find(s => s.Item.Name == kv.Value);

            if (inventorySlot?.Item is IEquipable equipable)
            {
                hero.Inventory.Equip(equipable);
            }
        }

        return hero;
    }
}