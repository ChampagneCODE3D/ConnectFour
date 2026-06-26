namespace ConnectFour;

/// <summary>
/// Handles all console display and user interface output.
/// Demonstrates SEPARATION OF CONCERNS - UI logic is isolated from game logic.
/// </summary>
public class GameView
{
    /// <summary>
    /// Displays the current state of the game board.
    /// </summary>
    /// <param name="board">The board to display.</param>
    public void DisplayBoard(Board board)
    {
        char[,] grid = board.GetGrid();
        Console.WriteLine();
        Console.WriteLine("  1   2   3   4   5   6   7");
        Console.WriteLine("┌───┬───┬───┬───┬───┬───┬───┐");

        for (int row = 0; row < 6; row++)
        {
            Console.Write("│");
            for (int col = 0; col < 7; col++)
            {
                Console.Write($" {grid[row, col]} │");
            }
            Console.WriteLine();

            if (row < 5)
            {
                Console.WriteLine("├───┼───┼───┼───┼───┼───┼───┤");
            }
        }

        Console.WriteLine("└───┴───┴───┴───┴───┴───┴───┘");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays the welcome message and game title.
    /// </summary>
    public void DisplayWelcome()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         CONNECT FOUR GAME              ║");
        Console.WriteLine("║     SODV 1202 Term Project             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Objective: Connect four of your symbols");
        Console.WriteLine("horizontally, vertically, or diagonally!");
        Console.WriteLine("Tip: Press Esc anytime to open the exit menu.");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays the main menu and gets the player's choice.
    /// </summary>
    /// <returns>1 for Human vs Human, 2 for Human vs Computer, or 0 to exit.</returns>
    public int DisplayMenu()
    {
        Console.WriteLine("SELECT GAME MODE:");
        Console.WriteLine("1. Human vs Human");
        Console.WriteLine("2. Human vs Computer");
        Console.WriteLine("Esc. Exit menu");
        Console.WriteLine();

        Console.Write("Enter your choice (1 or 2): ");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();

                if (ConsolePrompts.ShowExitConfirmation())
                {
                    return 0;
                }

                Console.WriteLine();
                Console.Write("Enter your choice (1 or 2): ");
                continue;
            }

            if (key.KeyChar == '1' || key.KeyChar == '2')
            {
                Console.WriteLine(key.KeyChar);
                return key.KeyChar - '0';
            }
        }
    }

    /// <summary>
    /// Displays the AI difficulty menu and gets the player's choice.
    /// </summary>
    /// <returns>Difficulty level or null if user requested exit.</returns>
    public AiDifficulty? DisplayDifficultyMenu()
    {
        Console.WriteLine();
        Console.WriteLine("SELECT AI DIFFICULTY:");
        Console.WriteLine("1. Easy (random moves)");
        Console.WriteLine("2. Medium (win/block strategy)");
        Console.WriteLine("3. Hard (stronger positioning)");
        Console.WriteLine("Esc. Exit menu");
        Console.WriteLine();

        Console.Write("Enter your choice (1, 2, or 3): ");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();

                if (ConsolePrompts.ShowExitConfirmation())
                {
                    return null;
                }

                Console.WriteLine();
                Console.Write("Enter your choice (1, 2, or 3): ");
                continue;
            }

            if (key.KeyChar == '1')
            {
                Console.WriteLine('1');
                return AiDifficulty.Easy;
            }

            if (key.KeyChar == '2')
            {
                Console.WriteLine('2');
                return AiDifficulty.Medium;
            }

            if (key.KeyChar == '3')
            {
                Console.WriteLine('3');
                return AiDifficulty.Hard;
            }
        }
    }

    /// <summary>
    /// Displays the winner announcement.
    /// </summary>
    /// <param name="winner">The player who won the game.</param>
    public void DisplayWinner(Player winner)
    {
        Console.WriteLine();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine($"║  🎉 {winner.Name} ({winner.Symbol}) WINS! 🎉");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays the draw message.
    /// </summary>
    public void DisplayDraw()
    {
        Console.WriteLine();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║       GAME OVER - IT'S A DRAW!         ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
    }

    /// <summary>
    /// Asks if the players want to play again.
    /// </summary>
    /// <returns>True if they want to play again, false otherwise.</returns>
    public bool AskPlayAgain()
    {
        while (true)
        {
            if (!ConsolePrompts.TryReadLineWithEscape("Play again? (y/n): ", out string rawInput))
            {
                return false;
            }

            string input = rawInput.Trim().ToLowerInvariant();

            if (input == "y" || input == "yes")
            {
                return true;
            }

            if (input == "n" || input == "no")
            {
                return false;
            }

            Console.WriteLine("Invalid choice. Enter y or n.");
        }
    }

    /// <summary>
    /// Displays the goodbye message.
    /// </summary>
    public void DisplayGoodbye()
    {
        Console.WriteLine();
        Console.WriteLine("Thanks for playing Connect Four!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
