namespace Console_RPG.Models.Items;

public class StatBlock
{
    public int Damage { get; set; }
    public int Defense { get; set; }
    public int MaxHealth { get; set; }

    public void Add(StatBlock other)
    {
        if (other == null) return;
        Damage += other.Damage;
        Defense += other.Defense;
        MaxHealth += other.MaxHealth;
    }
    
}