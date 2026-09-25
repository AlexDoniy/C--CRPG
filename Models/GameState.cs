namespace Console_RPG.Models;

public class GameState
{
    public int CurrentDay { get; private set; } = 1;

    public void AdvanceDay()
    {
        CurrentDay += 1;
    }
    
    public void SetDay(int day)
    {
        CurrentDay = day;
    }
}