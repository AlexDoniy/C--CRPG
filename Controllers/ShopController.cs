using Console_RPG.Models;
using Console_RPG.Models.Items;
using Console_RPG.Views;

namespace Console_RPG.Controllers;

public class ShopController
{
    private readonly Shop _shop;
    private readonly Hero _hero;
    private GameState _gameState;
    
    private readonly double _sellingCoefficient = 0.75;
    public ShopController(Shop shop, Hero hero, GameState gameState)
    {
        _shop = shop;
        _hero = hero;
        _gameState = gameState;
    }

    public void Run()
    {
        _shop.RefreshIfNeeded(_gameState.CurrentDay);
        
        while (true)
        {
            CoreView.Clear();
            CoreView.ShowEmpty();
            CoreView.ShowTitle(" Welcome to the Shop!");
            CoreView.ShowText("Would you Buy [b] or Sell [s] items? Also enter [0] to return: ");

            string input = Console.ReadLine()?.ToUpper();

            switch (input)
            {
                case "B":
                    BuyingMode();
                    break;

                case "S":
                    SellingMode();
                    break;

                case "0":
                    return;

                default:
                    CoreView.ShowString("Incorrect input!");
                    break;
            }
        }
    }

    private void BuyingMode()
    {
        while (true)
        {
            CoreView.ShowShop(_shop);
            CoreView.ShowText("Select item or enter [0] to exit: ");

            if (!int.TryParse(Console.ReadLine(), out int selected))
            {
                CoreView.ShowString("Incorrect input!");
                continue;
            }

            if (selected == 0)
            {
                break;
            }

            int index = selected - 1;
            if (index < 0 || index >= _shop.Stock.Count)
            {
                CoreView.ShowString("There is no item with such number.");
                continue;
            }

            HandleBuying(_shop.Stock[index]);
        }
    }

    private void SellingMode()
    {
        while (true)
        {
            CoreView.ShowInventory(_hero.Inventory);
            CoreView.ShowText("Select item or enter [0] to exit: ");

            if (!int.TryParse(Console.ReadLine(), out int selected))
            {
                CoreView.ShowString("Incorrect input!");
                continue;
            }

            if (selected == 0)
            {
                break;
            }

            int index = selected - 1;
            if (index < 0 || index >= _hero.Inventory.Slots.Count)
            {
                CoreView.ShowString("There is no item with such number.");
                continue;
            }

            HandleSelling(_hero.Inventory.Slots[index]);
        }
    }

    private void HandleBuying(InventorySlot slot)
    {
        Item item = slot.Item;

        CoreView.ShowEmpty();
        CoreView.ShowString($"Selected: {item.Name}");
        CoreView.ShowString($"Are you sure to buy {item.Name} for {item.Price} gold?");
        CoreView.ShowText("Select option (y/n) or enter [0] to exit: ");

        string input = Console.ReadLine()?.ToUpper();
        CoreView.ShowEmpty();

        switch (input)
        {
            case "Y":
                ConfirmPurchase(item);
                break;

            case "N":
            case "0":
                return;

            default:
                CoreView.ShowString("Incorrect input!");
                break;
        }
    }
    
    private void HandleSelling(InventorySlot slot)
    {
        Item item = slot.Item;
        int sellingPrice = (int)Math.Floor(item.Price * _sellingCoefficient);
        
        CoreView.ShowEmpty();
        CoreView.ShowString($"Selected: {item.Name}");
        CoreView.ShowString($"Are you sure to sell {item.Name} for {sellingPrice} gold?");
        CoreView.ShowText("Select option (y/n) or enter [0] to exit: ");

        string input = Console.ReadLine()?.ToUpper();
        CoreView.ShowEmpty();

        switch (input)
        {
            case "Y":
                ConfirmSelling(item, sellingPrice);
                break;

            case "N":
            case "0":
                return;

            default:
                CoreView.ShowString("Incorrect input!");
                break;
        }
    }

    private void ConfirmPurchase(Item item)
    {
        CoreView.Clear();
        if (!Shop.Transaction(_hero.Inventory, _shop, item.Price))
        {
            CoreView.ShowString("Not enough gold!");
            return;
        }

        _shop.DeleteStock(item);
        _hero.Inventory.AddItem(item);
        CoreView.ShowString($"{item.Name} bought and added to your inventory!");
    }
    
    private void ConfirmSelling(Item item, int price)
    {
        CoreView.Clear();
        if (!Shop.Transaction(_shop, _hero.Inventory, price))
        {
            CoreView.ShowString("Not enough gold!");
            return;
        }

        _shop.AddStock(item);
        _hero.Inventory.DeleteItem(item);
        CoreView.ShowString($"{item.Name} sold successfully!");
    }
}