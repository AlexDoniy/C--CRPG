using Console_RPG.Models;
using Console_RPG.Views;

namespace Console_RPG.Controllers;

public class SaveController
{
    private const string SavePath = "save.json";

    public bool SaveExists() => File.Exists(SavePath);

    public void Save(Hero hero, GameState gameState)
    {
        CoreView.ShowEmpty();
        if (SaveManager.TrySave(hero, gameState, out string error))
        {
            CoreView.ShowString("Game saved!");
        }
        else
        {
            CoreView.ShowString($"Save failed: {error}");
        }
        CoreView.Wait();
    }
    public bool TryLoad(out Hero hero, out GameState gameState)
    {
        bool success = SaveManager.TryLoad(out hero, out gameState, out string error);

        if (!success && error != null)
        {
            CoreView.ShowString($"Load failed: {error}");
        }

        return success;
    }

    public void DeleteSave()
    {
        CoreView.ShowEmpty();
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            CoreView.ShowString("Save file deleted.");
        }
        else
        {
            CoreView.ShowString("No save file found.");
        }
        CoreView.Wait();
    }

    public (Hero hero, GameState gameState) RunStartupMenu()
    {
        if (!SaveExists())
        {
            return (Hero.CreateNew(), new GameState());
        }

        while (true)
        {
            CoreView.ShowEmpty();
            CoreView.ShowTitle("Save Found");
            CoreView.ShowString("1. Continue saved game");
            CoreView.ShowString("2. Start new game (overwrite save)");
            CoreView.ShowString("3. Delete save and exit");
            CoreView.ShowText("Enter your choice: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (TryLoad(out Hero loadedHero, out GameState loadedState))
                    {
                        CoreView.ShowString("Save loaded!");
                        return (loadedHero, loadedState);
                    }
                    CoreView.ShowString("Failed to load save. Starting new game.");
                    return (Hero.CreateNew(), new GameState());

                case "2":
                    return (Hero.CreateNew(), new GameState());

                case "3":
                    DeleteSave();
                    Environment.Exit(0);
                    break;

                default:
                    CoreView.ShowString("Incorrect input!");
                    break;
            }
        }
    }
}