using Console_RPG.Enums;
using Console_RPG.Models.Items;

namespace Console_RPG.Models.Interfaces;

public interface IEquipable
{
    public EquipmentSlot Slot { get; }
    public StatBlock Stats { get; set; }

    public string Name {
        get;
    }
}