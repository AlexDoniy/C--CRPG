using Console_RPG.Controllers;
using Console_RPG.Models;
using Console_RPG.Views;

public class GameController
{
    private readonly HeroController _heroController;
    private readonly InventoryController _inventoryController;
    private readonly BattleController _battleController;
    private readonly ShopController _shopController;
    private readonly SaveController _saveController;
    private readonly Hero _hero;
    private readonly Shop _shop;
    private readonly GameState _gameState;

    public GameController()
    {
        _saveController = new SaveController();
        (_hero, _gameState) = _saveController.RunStartupMenu();

        _shop = new Shop();

        _heroController = new HeroController(_hero);
        _shopController = new ShopController(_shop, _hero, _gameState);
        _battleController = new BattleController(_hero, _gameState);
        _inventoryController = new InventoryController(_hero.Inventory);
    }

    public void Run()
    {
        CoreView.Welcome();

        while (true)
        {
            CoreView.Clear();
            CoreView.ShowState(_gameState);
            CoreView.ShowMainMenu();

            if (int.TryParse(Console.ReadLine(), out var selected))
            {
                switch (selected)
                {
                    case 1:
                        _heroController.Run();
                        break;
                    case 2:
                        _inventoryController.Run();
                        break;
                    case 3:
                        _battleController.Run();
                        break;
                    case 4:
                        _shopController.Run();
                        break;
                    case 8:
                        
                        _saveController.Save(_hero, _gameState);
                        break;
                    case 9:
                        _saveController.DeleteSave();
                        break;
                    default:
                        CoreView.ShowString("Incorrect input! Try again:");
                        break;
                }
            }
        }
    }
}