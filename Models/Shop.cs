using Console_RPG.Factories;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;
using Console_RPG.Models.RandomRoller;

namespace Console_RPG.Models;

public class Shop : IShopInfo, ITrading
{
    public List<InventorySlot> Stock { get; private set; }
    public int GoldBalance { get; private set; }
    private int LastRefreshedDay { get; set; } = 1;
    
    public Shop()
    {
        GoldBalance = Random.Shared.Next(30, 101);
        Stock = new List<InventorySlot>();
        Refresh(1);
    }

    public void AddStock(Item item, int quantity = 1)
    {
        var existing = Stock.Find(slot => slot.Item.Name == item.Name);
        if (existing != null)
            existing.Quantity += quantity;
        else
            Stock.Add(new InventorySlot(item, quantity));
    }

    public void DeleteStock(Item item, int quantity = 1)
    {
        var existing = Stock.Find(slot => slot.Item.Name == item.Name);
        if (existing == null) return;
        
        existing.Quantity -= quantity;
        if (existing.Quantity <= 0)
        {
            Stock.Remove(existing);
        }
    }

    public void RefreshIfNeeded(int currentDay)
    {
        if (currentDay <= LastRefreshedDay) return;
        
        Refresh(currentDay);
        LastRefreshedDay = currentDay;
    }
    
    public void Refresh(int currentDay)
    {
        Stock.Clear();
        for (int i = 0; i < 6; i++)
        {
            AddStock(WeightedRoller<Item>.RollRandom(currentDay, LootTable.EquipableEntries, ItemFactory.Create), 1);
        }

        for (int i = 0; i < 3; i++)
        {
            AddStock(WeightedRoller<Item>.RollRandom(currentDay, LootTable.UsableEntries, ItemFactory.Create), Random.Shared.Next(1, 3));
        }
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

    public static bool Transaction(ITrading buyer, ITrading seller, int amount)
    {
        if (buyer.GoldBalance - amount < 0) return false;
        seller.AddGold(amount);
        buyer.SpendGold(amount);
        return true;
    }
}