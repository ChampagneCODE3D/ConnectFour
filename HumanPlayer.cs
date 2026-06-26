namespace ConnectFour;

/// <summary>
/// Represents a human player who makes moves via console input.
/// Demonstrates INHERITANCE by extending the Player base class.
/// </summary>
public class HumanPlayer : Player
{
    /// <summary>
    /// Initializes a new human player.
    /// </summary>
    /// <param name="symbol">The symbol representing this player on the board.</param>
    /// <param name="name">The player's name.</param>
    public HumanPlayer(char symbol, string name) : base(symbol, name)
    {
    }

    /// <summary>
    /// Gets a move from the human player via console input.
    /// Demonstrates METHOD OVERRIDING and POLYMORPHISM.
    /// </summary>
    /// <param name="board">The current game board state.</param>
    /// <returns>The column number (1-7) chosen by the player.</returns>
    public override int GetMove(Board board)
    {
        while (true)
        {
            List<int> availableColumns = GetAvailableColumns(board);
            string availableText = string.Join(", ", availableColumns);

            Console.Write($"{Name} ({Symbol}), enter column ({availableText}): ");
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();

                UserFlowAction action = ConsolePrompts.ShowExitConfirmation();
                if (action == UserFlowAction.ExitGame || action == UserFlowAction.RestartToMenu)
                {
                    throw new UserFlowException(action);
                }

                Console.Clear();
                GameView refreshedView = new();
                refreshedView.DisplayBoard(board);
                continue;
            }

            Console.WriteLine(key.KeyChar);

            if (key.KeyChar < '1' || key.KeyChar > '7')
            {
                Console.WriteLine($"Invalid move. Choose one of: {availableText}.");
                continue;
            }

            int column = key.KeyChar - '0';

            if (!board.IsColumnAvailable(column))
            {
                Console.WriteLine($"Invalid move. Choose one of: {availableText}.");
                continue;
            }

            return column;
        }
    }

    private static List<int> GetAvailableColumns(Board board)
    {
        List<int> availableColumns = new();

        for (int col = 1; col <= 7; col++)
        {
            if (board.IsColumnAvailable(col))
            {
                availableColumns.Add(col);
            }
        }

        return availableColumns;
    }
}
