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
        int column;
        bool validInput;

        do
        {
            Console.Write($"{Name} ({Symbol}), enter column (1-7): ");
            string? input = Console.ReadLine();
            
            // Validate input is a number between 1 and 7
            validInput = int.TryParse(input, out column) && column >= 1 && column <= 7;
            
            if (!validInput)
            {
                Console.WriteLine("Invalid input! Please enter a number between 1 and 7.");
            }
            else if (!board.IsColumnAvailable(column))
            {
                Console.WriteLine("That column is full! Please choose another column.");
                validInput = false;
            }
        } while (!validInput);

        return column;
    }
}
