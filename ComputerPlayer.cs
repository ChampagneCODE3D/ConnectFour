namespace ConnectFour;

/// <summary>
/// Represents an AI computer player with rule-based strategy.
/// Demonstrates INHERITANCE by extending the Player base class.
/// </summary>
public class ComputerPlayer : Player
{
    private readonly Random _random = new Random();

    /// <summary>
    /// Initializes a new computer player.
    /// </summary>
    /// <param name="symbol">The symbol representing this player on the board.</param>
    public ComputerPlayer(char symbol) : base(symbol, "Computer")
    {
    }

    /// <summary>
    /// Gets a move from the AI using rule-based strategy.
    /// Priority: 1) Win if possible, 2) Block opponent's win, 3) Random valid move.
    /// Demonstrates METHOD OVERRIDING and POLYMORPHISM.
    /// </summary>
    /// <param name="board">The current game board state.</param>
    /// <returns>The column number (1-7) chosen by the AI.</returns>
    public override int GetMove(Board board)
    {
        Console.WriteLine($"{Name} ({Symbol}) is thinking...");
        Thread.Sleep(1000); // Simulate thinking time

        // Strategy 1: Check if AI can win
        int? winningMove = FindWinningMove(board, Symbol);
        if (winningMove.HasValue)
        {
            Console.WriteLine($"{Name} chooses column {winningMove.Value}");
            return winningMove.Value;
        }

        // Strategy 2: Block opponent's winning move
        char opponentSymbol = Symbol == 'X' ? 'O' : 'X';
        int? blockingMove = FindWinningMove(board, opponentSymbol);
        if (blockingMove.HasValue)
        {
            Console.WriteLine($"{Name} chooses column {blockingMove.Value}");
            return blockingMove.Value;
        }

        // Strategy 3: Make a random valid move
        List<int> availableColumns = new List<int>();
        for (int col = 1; col <= 7; col++)
        {
            if (board.IsColumnAvailable(col))
            {
                availableColumns.Add(col);
            }
        }

        int randomChoice = availableColumns[_random.Next(availableColumns.Count)];
        Console.WriteLine($"{Name} chooses column {randomChoice}");
        return randomChoice;
    }

    /// <summary>
    /// Finds a column that would result in a win for the specified player.
    /// </summary>
    /// <param name="board">The current game board.</param>
    /// <param name="symbol">The symbol to check for winning moves.</param>
    /// <returns>Column number if a winning move exists, null otherwise.</returns>
    private int? FindWinningMove(Board board, char symbol)
    {
        for (int col = 1; col <= 7; col++)
        {
            if (board.IsColumnAvailable(col))
            {
                // Simulate dropping a piece
                Board testBoard = board.Clone();
                testBoard.DropPiece(col, symbol);

                // Check if this move wins
                if (testBoard.CheckWin(symbol))
                {
                    return col;
                }
            }
        }
        return null;
    }
}
