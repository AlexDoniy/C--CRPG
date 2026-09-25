namespace Console_RPG.Models.Interfaces;

public interface ITrading
{
    public int GoldBalance { get; }

    public bool SpendGold(int amount);
    public void AddGold(int amount);
}