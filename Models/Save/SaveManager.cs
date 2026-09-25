using System.Text.Json;
using Console_RPG.Models;
using Console_RPG.Models.Save;

public static class SaveManager
{
    private const string SavePath = "save.json";

    public static bool TrySave(Hero hero, GameState gameState, out string errorMessage)
    {
        errorMessage = null;

        try
        {
            var saveData = new SaveData
            {
                Hero = SaveConverter.ToSaveData(hero),
                CurrentDay = gameState.CurrentDay
            };

            string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SavePath, json);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            errorMessage = "No permission to write the save file.";
            return false;
        }
        catch (IOException ex)
        {
            errorMessage = $"Failed to write save file: {ex.Message}";
            return false;
        }
    }

    public static bool TryLoad(out Hero hero, out GameState gameState, out string errorMessage)
    {
        hero = null;
        gameState = null;
        errorMessage = null;

        if (!File.Exists(SavePath))
        {
            errorMessage = "Save file not found.";
            return false;
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(json);

            if (saveData?.Hero == null)
            {
                errorMessage = "Save file is empty or invalid.";
                return false;
            }

            hero = SaveConverter.FromSaveData(saveData.Hero);
            gameState = new GameState();
            gameState.SetDay(saveData.CurrentDay);
            return true;
        }
        catch (JsonException)
        {
            errorMessage = "Save file is corrupted and cannot be read.";
            return false;
        }
        catch (IOException ex)
        {
            errorMessage = $"Failed to read save file: {ex.Message}";
            return false;
        }
    }
}