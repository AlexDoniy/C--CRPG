using Console_RPG.Models.Interfaces;
using Console_RPG.Models.Items;

namespace Console_RPG.Models;

public class Enemy : Character
{
    public override StatBlock TotalStats => BaseStats;

    public Enemy(string name, int damage, int maxHealth, int defense) : base(name, damage: damage, maxHealth: maxHealth, defense: defense)
    {
        
    }

    public override void Attack(ITakeDamage target)
    {
        target.TakeDamage(BaseStats.Damage);
    }
    
}