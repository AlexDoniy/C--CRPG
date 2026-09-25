try
{
    var game = new GameController();
    game.Run();
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("=== Unexpected error occurred ===");
    Console.WriteLine(ex.Message);
    Console.WriteLine("The game will now close.");
    Console.ReadKey();
}