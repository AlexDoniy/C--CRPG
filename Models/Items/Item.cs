namespace Console_RPG.Models.Items;

public abstract class Item
{
    public string Name { get; }
    public int Price { get; }
    
    
    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
}
