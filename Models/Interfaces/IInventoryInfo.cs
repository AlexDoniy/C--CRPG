using Console_RPG.Models.Items;

namespace Console_RPG.Models.Interfaces;

public interface IInventoryInfo
{
    public int GoldBalance { get; }
    public List<InventorySlot> Slots { get; }
}