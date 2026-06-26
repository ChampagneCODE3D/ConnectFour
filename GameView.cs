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
    public void DisplayBoard(Board board, int? highlightedRow = null, int? highlightedCol = null)
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
                char cell = grid[row, col];
                bool isHighlighted = highlightedRow == row && highlightedCol == col;
                ConsoleColor pieceColor = cell switch
                {
                    'X' => ConsoleColor.Red,
                    'O' => ConsoleColor.Yellow,
                    _ => ConsoleColor.Gray
                };

                if (isHighlighted)
                {
                    Console.BackgroundColor = pieceColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = pieceColor;
                }

                Console.Write($" {cell} ");
                Console.ResetColor();
                Console.Write("│");
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
        WriteModeMenu();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();

                UserFlowAction action = ConsolePrompts.ShowExitConfirmation();
                if (action == UserFlowAction.ExitGame)
                {
                    return 0;
                }

                Console.Clear();
                DisplayWelcome();
                WriteModeMenu();
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
    /// <returns>Tuple of flow action and optional selected difficulty.</returns>
    public (UserFlowAction Action, AiDifficulty? Difficulty) DisplayDifficultyMenu()
    {
        WriteDifficultyMenu();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();

                UserFlowAction action = ConsolePrompts.ShowExitConfirmation();
                if (action != UserFlowAction.Continue)
                {
                    return (action, null);
                }

                Console.Clear();
                WriteDifficultyMenu();
                continue;
            }

            if (key.KeyChar == '1')
            {
                Console.WriteLine('1');
                return (UserFlowAction.Continue, AiDifficulty.Easy);
            }

            if (key.KeyChar == '2')
            {
                Console.WriteLine('2');
                return (UserFlowAction.Continue, AiDifficulty.Medium);
            }

            if (key.KeyChar == '3')
            {
                Console.WriteLine('3');
                return (UserFlowAction.Continue, AiDifficulty.Hard);
            }
        }
    }

    private static void WriteModeMenu()
    {
        Console.WriteLine("SELECT GAME MODE:");
        Console.WriteLine("1. Human vs Human");
        Console.WriteLine("2. Human vs Computer");
        Console.WriteLine("Esc. Exit menu");
        Console.WriteLine();
        Console.Write("Enter your choice (1 or 2): ");
    }

    private static void WriteDifficultyMenu()
    {
        Console.WriteLine();
        Console.WriteLine("SELECT AI DIFFICULTY:");
        Console.WriteLine("1. Easy (random moves)");
        Console.WriteLine("2. Medium (win/block strategy)");
        Console.WriteLine("3. Hard (stronger positioning)");
        Console.WriteLine("Esc. Exit menu");
        Console.WriteLine();
        Console.Write("Enter your choice (1, 2, or 3): ");
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
    /// Asks for post-game next action.
    /// </summary>
    /// <returns>User flow action for rematch, main menu, or exit.</returns>
    public UserFlowAction AskPostGameAction(bool isHumanVsComputer)
    {
        while (true)
        {
            int promptTop = Console.CursorTop;
            Console.Write("Play again with same players? (y/n): ");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                UserFlowAction action = ConsolePrompts.ShowExitConfirmation();
                if (action != UserFlowAction.Continue)
                {
                    return action;
                }

                Console.SetCursorPosition(0, promptTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, promptTop);
                continue;
            }

            char choice = char.ToLowerInvariant(key.KeyChar);

            if (choice == 'y')
            {
                Console.WriteLine('y');
                return UserFlowAction.Continue;
            }

            if (choice == 'n')
            {
                Console.WriteLine('n');
                return ShowPostGameMenu(isHumanVsComputer);
            }

            Console.SetCursorPosition(0, promptTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, promptTop);
        }
    }

    private static UserFlowAction ShowPostGameMenu(bool isHumanVsComputer)
    {
        Console.WriteLine();
        Console.WriteLine("Next Step");

        if (isHumanVsComputer)
        {
            Console.WriteLine("1. Return to start menu");
            Console.WriteLine("2. Change AI difficulty");
            Console.WriteLine("3. Quit game");
            Console.Write("Choose 1, 2, or 3: ");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                if (key.KeyChar == '1')
                {
                    Console.WriteLine('1');
                    return UserFlowAction.RestartToMenu;
                }

                if (key.KeyChar == '2')
                {
                    Console.WriteLine('2');
                    return UserFlowAction.ChangeDifficulty;
                }

                if (key.KeyChar == '3' || key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine('3');
                    return UserFlowAction.ExitGame;
                }
            }
        }

        Console.WriteLine("1. Return to start menu");
        Console.WriteLine("2. Quit game");
        Console.Write("Choose 1 or 2: ");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.KeyChar == '1')
            {
                Console.WriteLine('1');
                return UserFlowAction.RestartToMenu;
            }

            if (key.KeyChar == '2' || key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine('2');
                return UserFlowAction.ExitGame;
            }
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
