using Console_RPG.Factories;
using Console_RPG.Models;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;
using Console_RPG.Models.RandomRoller;
using Console_RPG.Views;

namespace Console_RPG.Controllers;

public class BattleController
{
    private readonly Hero _hero;
    private Enemy _enemy;
    private readonly GameState _gameState;
    private bool _fled;

    public BattleController(Hero hero, GameState gameState)
    {
        _hero = hero;
        _gameState = gameState;
    }

    public void Run()
    {
        CoreView.Clear();
        CoreView.ShowEmpty();
        CoreView.ShowTitle("Battle Begun!");

        _enemy = WeightedRoller<Enemy>.RollRandom(_gameState.CurrentDay, EnemyTable.EquipableEntries,
            EnemyFactory.Create);
        _fled = false;

        CoreView.ShowString($"A wild {_enemy.Name} appears!");

        while (true)
        {
            CoreView.ShowEmpty();
            CoreView.ShowString(
                $"Your HP: {_hero.Health}/{_hero.TotalStats.MaxHealth}   {_enemy.Name} HP: {_enemy.Health}/{_enemy.TotalStats.MaxHealth}");
            CoreView.ShowText("Choose action: [a] Attack, [i] Use item, [r] Run: ");

            string input = Console.ReadLine()?.ToUpper();
            CoreView.ShowEmpty();

            bool turnTaken = HandlePlayerTurn(input);

            if (!turnTaken)
            {
                continue;
            }

            if (_fled)
            {
                CoreView.ShowEmpty();
                CoreView.ShowString("You escaped the battle.");
                break;
            }

            if (!_enemy.isAlive)
            {
                CoreView.ShowDying(_enemy);
                CoreView.ShowEmpty();
                CoreView.ShowString("Hero has won!");
                GrantLoot();
                AdvanceDayAndHeal();
                break;
            }

            EnemyTurn();

            if (!_hero.isAlive)
            {
                CoreView.ShowDying(_hero);
                CoreView.ShowEmpty();
                CoreView.ShowString("Enemy has won!");
                break;
            }
        }

        CoreView.ShowEmpty();
        CoreView.ShowTitle("Battle Ended!");

        if (!_enemy.isAlive)
        {
            CoreView.ShowEmpty();
            CoreView.ShowString($"A new day has come! Day {_gameState.CurrentDay}. You feel fully rested.");
        }

        CoreView.Wait();
    }

    private bool HandlePlayerTurn(string input)
    {
        switch (input)
        {
            case "A":
                CoreView.ShowAttack(_hero, _enemy);
                _hero.Attack(_enemy);
                return true;

            case "I":
                return UseItemTurn();

            case "R":
                TryToFlee();
                return true;

            default:
                CoreView.ShowString("Incorrect input!");
                return false;
        }
    }

    private bool UseItemTurn()
    {
        var usableSlots = _hero.Inventory.Slots
            .Where(slot => slot.Item is IUsable)
            .ToList();

        if (usableSlots.Count == 0)
        {
            CoreView.ShowString("You have no usable items!");
            return false;
        }

        CoreView.ShowString("Select item to use:");
        for (int i = 0; i < usableSlots.Count; i++)
        {
            CoreView.ShowString($"{i + 1}. {usableSlots[i].Item.Name} x{usableSlots[i].Quantity}");
        }

        CoreView.ShowText("Enter number or [0] to cancel: ");

        if (!int.TryParse(Console.ReadLine(), out int selected))
        {
            CoreView.ShowString("Incorrect input!");
            return false;
        }

        if (selected == 0)
        {
            return false;
        }

        int index = selected - 1;
        if (index < 0 || index >= usableSlots.Count)
        {
            CoreView.ShowString("There is no item with such number.");
            return false;
        }

        IUsable usable = (IUsable)usableSlots[index].Item;
        _hero.Inventory.Use(usable);
        CoreView.ShowString($"{usableSlots[index].Item.Name} used!");
        return true;
    }

    private void TryToFlee()
    {
        bool success = Random.Shared.Next(2) == 0;

        if (success)
        {
            CoreView.ShowString("You successfully fled the battle!");
            _fled = true;
        }
        else
        {
            CoreView.ShowString("You failed to flee!");
        }
    }

    private void EnemyTurn()
    {
        CoreView.ShowAttack(_enemy, _hero);
        _enemy.Attack(_hero);
    }

    private void AdvanceDayAndHeal()
    {
        _gameState.AdvanceDay();
        _hero.Heal(_hero.MaxHealth);
        CoreView.ShowEmpty();
    }

    private const double ItemDropChance = 0.5;

    private Item RollLoot(int currentDay)
    {
        if (Random.Shared.NextDouble() > ItemDropChance)
            return null;

        return WeightedRoller<Item>.RollRandom(currentDay, LootTable.EquipableEntries, ItemFactory.Create);
    }

    private int RollGold(int currentDay)
    {
        int baseMin = 5;
        int baseMax = 15;
        int bonus = currentDay / 2;

        return Random.Shared.Next(baseMin + bonus, baseMax + bonus + 1);
    }

    private void GrantLoot()
    {
        int goldReward = RollGold(_gameState.CurrentDay);
        _hero.Inventory.AddGold(goldReward);

        CoreView.ShowEmpty();
        CoreView.ShowString($"You found {goldReward} gold!");

        Item lootItem = RollLoot(_gameState.CurrentDay);
        if (lootItem != null)
        {
            _hero.Inventory.AddItem(lootItem);
            CoreView.ShowString($"You found {lootItem.Name}!");
        }
    }
}