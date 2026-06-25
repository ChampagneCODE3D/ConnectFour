namespace ConnectFour;

/// <summary>
/// Represents the Connect Four game board.
/// Demonstrates ENCAPSULATION by hiding internal grid state and exposing only necessary methods.
/// </summary>
public class Board
{
    private const int Rows = 6;
    private const int Columns = 7;
    private readonly char[,] _grid;

    /// <summary>
    /// Initializes a new empty game board.
    /// </summary>
    public Board()
    {
        _grid = new char[Rows, Columns];
        InitializeBoard();
    }

    /// <summary>
    /// Private constructor for cloning.
    /// </summary>
    private Board(char[,] grid)
    {
        _grid = new char[Rows, Columns];
        Array.Copy(grid, _grid, grid.Length);
    }

    /// <summary>
    /// Initializes the board with empty spaces.
    /// </summary>
    private void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                _grid[row, col] = ' ';
            }
        }
    }

    /// <summary>
    /// Checks if a column has space for another piece.
    /// </summary>
    /// <param name="column">Column number (1-7).</param>
    /// <returns>True if the column is not full, false otherwise.</returns>
    public bool IsColumnAvailable(int column)
    {
        int colIndex = column - 1;
        return colIndex >= 0 && colIndex < Columns && _grid[0, colIndex] == ' ';
    }

    /// <summary>
    /// Drops a piece into the specified column.
    /// </summary>
    /// <param name="column">Column number (1-7).</param>
    /// <param name="symbol">The player's symbol ('X' or 'O').</param>
    /// <returns>True if the piece was successfully placed, false otherwise.</returns>
    public bool DropPiece(int column, char symbol)
    {
        int colIndex = column - 1;

        if (!IsColumnAvailable(column))
        {
            return false;
        }

        // Find the lowest available row in this column
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (_grid[row, colIndex] == ' ')
            {
                _grid[row, colIndex] = symbol;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the specified player has won.
    /// </summary>
    /// <param name="symbol">The player's symbol to check.</param>
    /// <returns>True if the player has four in a row, false otherwise.</returns>
    public bool CheckWin(char symbol)
    {
        // Check horizontal wins
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (_grid[row, col] == symbol &&
                    _grid[row, col + 1] == symbol &&
                    _grid[row, col + 2] == symbol &&
                    _grid[row, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        // Check vertical wins
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (_grid[row, col] == symbol &&
                    _grid[row + 1, col] == symbol &&
                    _grid[row + 2, col] == symbol &&
                    _grid[row + 3, col] == symbol)
                {
                    return true;
                }
            }
        }

        // Check diagonal wins (top-left to bottom-right)
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (_grid[row, col] == symbol &&
                    _grid[row + 1, col + 1] == symbol &&
                    _grid[row + 2, col + 2] == symbol &&
                    _grid[row + 3, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        // Check diagonal wins (bottom-left to top-right)
        for (int row = 3; row < Rows; row++)
        {
            for (int col = 0; col <= Columns - 4; col++)
            {
                if (_grid[row, col] == symbol &&
                    _grid[row - 1, col + 1] == symbol &&
                    _grid[row - 2, col + 2] == symbol &&
                    _grid[row - 3, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the board is full (draw condition).
    /// </summary>
    /// <returns>True if all columns are full, false otherwise.</returns>
    public bool IsFull()
    {
        for (int col = 0; col < Columns; col++)
        {
            if (_grid[0, col] == ' ')
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Gets the board state as a 2D array for display purposes.
    /// </summary>
    /// <returns>A copy of the internal grid.</returns>
    public char[,] GetGrid()
    {
        char[,] gridCopy = new char[Rows, Columns];
        Array.Copy(_grid, gridCopy, _grid.Length);
        return gridCopy;
    }

    /// <summary>
    /// Creates a deep copy of the board.
    /// Used by AI to test potential moves.
    /// </summary>
    /// <returns>A new Board instance with the same state.</returns>
    public Board Clone()
    {
        return new Board(_grid);
    }
}
