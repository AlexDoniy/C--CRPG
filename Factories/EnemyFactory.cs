using Console_RPG.Models;

namespace Console_RPG.Factories;

public class EnemyFactory
{
    private static readonly Dictionary<string, Func<Enemy>> _enemies = new()
    {
        ["Goblin"] = () => new Enemy("Goblin", 10, 100, 0),
        ["Wolf"] = () => new Enemy("Wolf", 12, 140, 2),
        ["Orc"] = () => new Enemy("Orc", 20, 200, 5),
        ["Dragon"] = () => new Enemy("Dragon", 30, 500, 10),

    };

    public static Enemy Create(string name)
    {
        if (!_enemies.TryGetValue(name, out var factory))
            throw new ArgumentException($"Enemy {name} not found in allowed enemies list.");
        return factory();
    }
    
    public static IEnumerable<Enemy> GetAll() => _enemies.Values.Select(f => f());


}