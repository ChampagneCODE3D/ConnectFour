namespace ConnectFour;

/// <summary>
/// Abstract base class representing a Connect Four player.
/// Demonstrates ABSTRACTION by defining a contract that all players must follow.
/// </summary>
public abstract class Player
{
    /// <summary>
    /// Gets the player's symbol ('X' or 'O').
    /// </summary>
    public char Symbol { get; }

    /// <summary>
    /// Gets the player's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new player with a symbol and name.
    /// </summary>
    /// <param name="symbol">The symbol representing this player on the board.</param>
    /// <param name="name">The player's name.</param>
    protected Player(char symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }

    /// <summary>
    /// Abstract method that must be implemented by all derived player types.
    /// Gets the player's column choice for their next move.
    /// Demonstrates POLYMORPHISM - different player types implement this differently.
    /// </summary>
    /// <param name="board">The current game board state.</param>
    /// <returns>The column number (1-7) where the player wants to drop their disc.</returns>
    public abstract int GetMove(Board board);
}
