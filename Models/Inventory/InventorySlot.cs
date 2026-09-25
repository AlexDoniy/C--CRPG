namespace Console_RPG.Models.Items;

public class InventorySlot
{
    public Item Item { get; set; }
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }
    
    public InventorySlot(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
    
}