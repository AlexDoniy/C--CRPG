using Console_RPG.Models.Items;

namespace Console_RPG.Models.Interfaces;

public interface IShopInfo
{
    public List<InventorySlot> Stock { get; }
    public int GoldBalance { get; }
}