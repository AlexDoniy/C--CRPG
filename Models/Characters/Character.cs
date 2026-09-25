using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;

namespace Console_RPG.Models;

public abstract class Character : ITakeDamage, IAttack, IHealable, ICharacterInfo
{
    public string Name { get; private set; }
    public StatBlock BaseStats { get; private set; }
    public abstract StatBlock TotalStats { get; }

    public int Health { get; private set; }
    public int MaxHealth { get; private set; } 
    public bool isAlive { get; private set; }


    protected Character(string name, int maxHealth = 100, int damage = 10, int defense = 0)
    {
        Name = name;

        BaseStats = new StatBlock
        {
            Damage = damage,
            MaxHealth = maxHealth,
            Defense = defense
        };

        MaxHealth = BaseStats.MaxHealth;
        Health = BaseStats.MaxHealth;
        isAlive = true;
    }

    public abstract void Attack(ITakeDamage target);

    public void TakeDamage(int damage)
    {
        int actualDamage = Math.Max(0, damage - TotalStats.Defense);
        Health = Health < actualDamage ? 0 : Health - (actualDamage);
        if (Health <= 0)
        {
            isAlive = false;
        }
    }

    public void Heal(int heal)
    {
        Health = Math.Min(TotalStats.MaxHealth, Health + heal);
    }
    
    public void RefreshMaxHealth()
    {
        int newMax = TotalStats.MaxHealth;
        int delta = newMax - MaxHealth;

        MaxHealth = newMax;
        Health = Math.Clamp(Health + delta, 1, MaxHealth);
    }
    
    public void SetHealth(int health)
    {
        Health = Math.Clamp(health, 0, TotalStats.MaxHealth);
    }
}