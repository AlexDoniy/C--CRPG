using Console_RPG.Models;
using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;
using Console_RPG.Views;

namespace Console_RPG.Controllers;

public class InventoryController
{
    private readonly Inventory _inventory;

    public InventoryController(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void Run()
    {
        while (true)
        {
            CoreView.Clear();
            CoreView.ShowInventory(_inventory);
            CoreView.ShowText("Select item or enter [0] to exit: ");

            if (!int.TryParse(Console.ReadLine(), out var selected))
            {
                CoreView.ShowString("Incorrect input!");
                continue;
            }

            if (selected == 0)
            {
                break;
            }

            int index = selected - 1;
            if (index < 0 || index >= _inventory.Slots.Count)
            {
                CoreView.ShowString("There are no items such number.");
                continue;
            }

            Handle(_inventory.Slots[index]);
            CoreView.Wait();
        }
    }

    private void Handle(InventorySlot slot)
    {
        Item item = slot.Item;
        
        CoreView.ShowEmpty();
        CoreView.ShowString($"Selected: {item.Name}");


        if (item is IUsable)
        {
            CoreView.ShowString("1. Use item?");
        }

        if (item is IEquipable)
        {
            CoreView.ShowString("1. Equip / Unequip item?");
        }

        CoreView.ShowString("2. Throw item?");
        CoreView.ShowText("Select option or enter [0] to exit: ");


        if (!int.TryParse(Console.ReadLine(), out var selected)) return;
        CoreView.ShowEmpty();
        
        switch (selected)
        {
            case 1 when item is IUsable usable:
                _inventory.Use(usable);
                CoreView.ShowString($"{item.Name} used!");
                break;
            case 1 when item is IEquipable equipable && slot.IsEquipped:
                _inventory.Unequip(equipable);
                CoreView.ShowString($"{item.Name} unequipped!");
                break;
            case 1 when item is IEquipable equipable:
                _inventory.Equip(equipable);
                CoreView.ShowString($"{item.Name} equipped!");
                break;
            
            case 2:
                CoreView.ShowString("Do you wanna throw all items?");
                CoreView.ShowText("Select option (y/n) or enter (0) to exit: ");
                string res =  Console.ReadLine()?.ToUpper();
                CoreView.ShowEmpty();
                switch (res)
                {
                    case ['Y']:
                        CoreView.ShowString($"x{slot.Quantity} {item.Name} dropped out.");
                        _inventory.DeleteItem(item, slot.Quantity);
                        break;
                    case ['N']:
                        CoreView.ShowString($"x1 {item.Name} dropped out.");
                        _inventory.DeleteItem(item);
                        break;
                    case ['0']:
                        break;
                    default:
                        CoreView.ShowString("Incorrect input!");
                        break;
                }
                break;
            default:
                CoreView.ShowString("Incorrect option!");
                break;
        }
    }
}