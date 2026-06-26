namespace ConnectFour;

/// <summary>
/// Represents an AI computer player with rule-based strategy.
/// Demonstrates INHERITANCE by extending the Player base class.
/// </summary>
public class ComputerPlayer : Player
{
    private readonly Random _random = new Random();
    private readonly AiDifficulty _difficulty;

    /// <summary>
    /// Initializes a new computer player.
    /// </summary>
    /// <param name="symbol">The symbol representing this player on the board.</param>
    /// <param name="difficulty">The AI difficulty level.</param>
    public ComputerPlayer(char symbol, AiDifficulty difficulty) : base(symbol, "Computer")
    {
        _difficulty = difficulty;
    }

    /// <summary>
    /// Gets a move from the AI using a difficulty-based strategy.
    /// Demonstrates METHOD OVERRIDING and POLYMORPHISM.
    /// </summary>
    /// <param name="board">The current game board state.</param>
    /// <returns>The column number (1-7) chosen by the AI.</returns>
    public override int GetMove(Board board)
    {
        Console.WriteLine($"{Name} ({Symbol}) is thinking... [{_difficulty}]");
        Thread.Sleep(700);

        int move = _difficulty switch
        {
            AiDifficulty.Easy => GetRandomMove(board),
            AiDifficulty.Medium => GetStrategicMove(board),
            AiDifficulty.Hard => GetHardMove(board),
            _ => GetStrategicMove(board)
        };

        Console.WriteLine($"{Name} chooses column {move}");
        return move;
    }

    private int GetHardMove(Board board)
    {
        int strategicMove = GetStrategicMove(board);

        int centerColumn = 4;
        if (board.IsColumnAvailable(centerColumn))
        {
            Board centerTestBoard = board.Clone();
            centerTestBoard.DropPiece(centerColumn, Symbol);
            if (!centerTestBoard.CheckWin(Symbol))
            {
                return centerColumn;
            }
        }

        return strategicMove;
    }

    private int GetStrategicMove(Board board)
    {
        int? winningMove = FindWinningMove(board, Symbol);
        if (winningMove.HasValue)
        {
            return winningMove.Value;
        }

        char opponentSymbol = Symbol == 'X' ? 'O' : 'X';
        int? blockingMove = FindWinningMove(board, opponentSymbol);
        if (blockingMove.HasValue)
        {
            return blockingMove.Value;
        }

        return GetRandomMove(board);
    }

    private int GetRandomMove(Board board)
    {
        List<int> availableColumns = new();

        for (int col = 1; col <= 7; col++)
        {
            if (board.IsColumnAvailable(col))
            {
                availableColumns.Add(col);
            }
        }

        return availableColumns[_random.Next(availableColumns.Count)];
    }

    /// <summary>
    /// Finds a column that would result in a win for the specified player.
    /// </summary>
    /// <param name="board">The current game board.</param>
    /// <param name="symbol">The symbol to check for winning moves.</param>
    /// <returns>Column number if a winning move exists, null otherwise.</returns>
    private static int? FindWinningMove(Board board, char symbol)
    {
        for (int col = 1; col <= 7; col++)
        {
            if (board.IsColumnAvailable(col))
            {
                Board testBoard = board.Clone();
                testBoard.DropPiece(col, symbol);

                if (testBoard.CheckWin(symbol))
                {
                    return col;
                }
            }
        }

        return null;
    }
}
