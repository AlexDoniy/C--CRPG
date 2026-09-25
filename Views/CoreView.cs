using Console_RPG.Enums;
using Console_RPG.Models;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;

namespace Console_RPG.Views;

public class CoreView
{


    public static void ShowTitle(string title)
    {
        Console.WriteLine(string.Format("  ====== {0} ======", title));
    }
    
    public static void ShowString(string str)
    {
        Console.WriteLine(string.Format("    {0}", str));
    }
    
    public static void ShowText(string str)
    {
        Console.Write(string.Format("    {0}", str));
    }
        
    public static void ShowEmpty()
    {
        Console.WriteLine();
    }
    
    public static void Clear()
    {
        Console.Clear();
    }
    
    public static void Wait()
    {
        ShowEmpty();
        ShowString("Press any key to continue..");
        Console.ReadKey();
    }
    
    public static void Welcome()
    {
        ShowEmpty();
        ShowTitle("Welcome to the Console RPG!");
    }

    public static void ShowState(GameState gameState)
    {
        ShowEmpty();
        ShowTitle("Game state");
        ShowString($"Day: {gameState.CurrentDay}");
    }
    public static void ShowMainMenu()
    {
        ShowEmpty();
        ShowTitle("Menu");
        ShowString("1. Your Hero");
        ShowString("2. Open Inventory");
        ShowString("3. Battle");
        ShowString("4. Shop");
        ShowString("8. Save game");
        ShowString("9. Delete save");
        ShowText("Enter your choice: ");
    }


    public static void ShowCharacter(ICharacterInfo character)
    {
        ShowEmpty();
        ShowTitle("Hero Info");
        ShowString($"Name: {character.Name}");
        ShowString($"Health: {character.Health} / {character.TotalStats.MaxHealth}");
        ShowString($"Defense: {character.TotalStats.Defense}");
        ShowString($"Damage: {character.TotalStats.Damage}");
    }

    public static void ShowEquipment(Dictionary<EquipmentSlot, IEquipable> equipment)
    {
        ShowTitle("Your Equipment");
        foreach (var item in equipment)
        {
            ShowString($"{item.Key}: { (item.Value != null ? item.Value.Name : "Not selected")}");
        }
    }

    public static void ShowAttack(ICharacterInfo attacker, ICharacterInfo target)
    {
        ShowString($"{attacker.Name} attacked ({attacker.TotalStats.Damage-target.TotalStats.Defense}) {target.Name} ({target.Health}/{target.TotalStats.MaxHealth})");
    }

    public static void ShowDying(ICharacterInfo character)
    {
        ShowString($"{character.Name} ({character.Health}/{character.TotalStats.MaxHealth}) is dead!");
    }

    public static void ShowInventory(IInventoryInfo inventory)
    {
        ShowEmpty();
        ShowTitle("Inventory Info");
        ShowString($"Gold balance: {inventory.GoldBalance}");

        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            var slot = inventory.Slots[i];
            string equippedStatus = slot.IsEquipped ? " [E]" : "";
            string equipmentStats = slot.Item is Equipment equipment ? $" (dmg:{equipment.Stats.Damage}, hp:{equipment.Stats.MaxHealth}, def:{equipment.Stats.Defense})" : "";

            ShowString($"{i+1}. {slot.Item.Name}{equipmentStats} x{slot.Quantity}{equippedStatus}");
        }
    }

    public static void ShowShop(IShopInfo shop)
    {
        ShowEmpty();
        ShowString($"Shop Balance: {shop.GoldBalance} Gold");

        for (int i = 0; i < shop.Stock.Count; i++)
        {
            var slot = shop.Stock[i];
            string equipmentStats = slot.Item is Equipment equipment ? $" (dmg:{equipment.Stats.Damage}, hp:{equipment.Stats.MaxHealth}, def:{equipment.Stats.Defense})" : "";

            ShowString($"{i+1}. {slot.Item.Name}{equipmentStats} x{slot.Quantity} — (price: {slot.Item.Price})");
        }
    }
    
    
    
}
