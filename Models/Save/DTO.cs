namespace Console_RPG.Models.Save;

public class SaveData
{
    public HeroSaveData Hero { get; set; }
    public int CurrentDay { get; set; }
}

public class HeroSaveData
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public List<ItemSaveData> InventoryItems { get; set; } = new();
    public Dictionary<string, string> Equipment { get; set; } = new(); 
}

public class ItemSaveData
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }
}