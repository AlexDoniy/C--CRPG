using Console_RPG.Models;
using Console_RPG.Views;

namespace Console_RPG.Controllers;

public class HeroController
{
    private readonly Hero _hero;

    public HeroController(Hero hero)
    {
        _hero = hero;
    }

    public void Run()
    {
        CoreView.Clear();
        CoreView.ShowCharacter(_hero);
        CoreView.ShowEmpty();
        CoreView.ShowEquipment(_hero.Equipment);
        CoreView.Wait();
    }
}