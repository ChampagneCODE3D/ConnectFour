namespace ConnectFour;

/// <summary>
/// Controls the game flow and manages game state.
/// Demonstrates the CONTROLLER pattern in MVC architecture.
/// </summary>
public class GameController
{
    private readonly GameView _view;
    private Board _board;
    private Player? _player1;
    private Player? _player2;
    private Player? _currentPlayer;

    /// <summary>
    /// Initializes a new game controller.
    /// </summary>
    public GameController()
    {
        _view = new GameView();
        _board = new Board();
    }

    /// <summary>
    /// Starts the game and manages the main game loop.
    /// </summary>
    public void Start()
    {
        _view.DisplayWelcome();

        bool playAgain = true;

        while (playAgain)
        {
            if (!SetupGame())
            {
                break;
            }

            if (!PlayGame())
            {
                break;
            }

            playAgain = _view.AskPlayAgain();

            if (playAgain)
            {
                _view.DisplayWelcome();
            }
        }

        _view.DisplayGoodbye();
    }

    /// <summary>
    /// Sets up the game based on the selected game mode.
    /// </summary>
    /// <returns>True if setup completed, false if user requested exit.</returns>
    private bool SetupGame()
    {
        int gameMode = _view.DisplayMenu();
        if (gameMode == 0)
        {
            return false;
        }

        _board = new Board();

        if (gameMode == 1)
        {
            // Human vs Human
            string? name1 = PromptForPlayerName("\nPlayer 1, enter your name: ", "Player 1");
            if (name1 is null)
            {
                return false;
            }

            string? name2 = PromptForPlayerName("Player 2, enter your name: ", "Player 2");
            if (name2 is null)
            {
                return false;
            }

            _player1 = new HumanPlayer('X', name1);
            _player2 = new HumanPlayer('O', name2);
        }
        else
        {
            // Human vs Computer
            string? name = PromptForPlayerName("\nEnter your name: ", "Player");
            if (name is null)
            {
                return false;
            }

            AiDifficulty? difficulty = _view.DisplayDifficultyMenu();
            if (!difficulty.HasValue)
            {
                return false;
            }

            _player1 = new HumanPlayer('X', name);
            _player2 = new ComputerPlayer('O', difficulty.Value);
        }

        _currentPlayer = _player1;
        Console.WriteLine($"\n{_player1.Name} will play as 'X'");
        Console.WriteLine($"{_player2.Name} will play as 'O'");

        // Poka-yoke gate: explicit Enter starts the cycle; Escape must be confirmed in exit menu.
        if (!ConsolePrompts.WaitForEnterWithEscape("\nPress Enter to start the game (Esc for exit menu): "))
        {
            return false;
        }

        return true;
    }

    private static string? PromptForPlayerName(string prompt, string defaultName)
    {
        while (true)
        {
            if (!ConsolePrompts.TryReadLineWithEscape(prompt, out string rawInput))
            {
                return null;
            }

            string normalizedName = NormalizeName(rawInput);

            if (string.IsNullOrEmpty(normalizedName))
            {
                return defaultName;
            }

            if (normalizedName.Length > 100)
            {
                Console.WriteLine("Name is too long. Use 100 characters or fewer.");
                continue;
            }

            return normalizedName;
        }
    }

    private static string NormalizeName(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        string trimmed = input.Trim();
        List<char> normalizedChars = new();
        bool previousWasSpace = false;

        foreach (char c in trimmed)
        {
            if (char.IsWhiteSpace(c))
            {
                if (!previousWasSpace)
                {
                    normalizedChars.Add(' ');
                    previousWasSpace = true;
                }

                continue;
            }

            normalizedChars.Add(c);
            previousWasSpace = false;
        }

        return new string([.. normalizedChars]);
    }

    private int GetTopOccupiedRow(int column)
    {
        char[,] grid = _board.GetGrid();
        int colIndex = column - 1;

        for (int row = 0; row < 6; row++)
        {
            if (grid[row, colIndex] != ' ')
            {
                return row;
            }
        }

        return 5;
    }

    /// <summary>
    /// Main game loop - alternates turns until win or draw.
    /// </summary>
    /// <returns>True if game ended normally, false if user requested exit.</returns>
    private bool PlayGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            Console.Clear();
            _view.DisplayBoard(_board);

            int column;
            try
            {
                // Get the current player's move (demonstrates POLYMORPHISM)
                column = _currentPlayer!.GetMove(_board);
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            // Drop the piece
            _board.DropPiece(column, _currentPlayer.Symbol);
            int droppedRow = GetTopOccupiedRow(column);
            int droppedCol = column - 1;

            // Check for win
            if (_board.CheckWin(_currentPlayer.Symbol))
            {
                Console.Clear();
                _view.DisplayBoard(_board);
                _view.DisplayWinner(_currentPlayer);
                gameOver = true;
            }
            // Check for draw
            else if (_board.IsFull())
            {
                Console.Clear();
                _view.DisplayBoard(_board);
                _view.DisplayDraw();
                gameOver = true;
            }
            else
            {
                if (_currentPlayer is ComputerPlayer)
                {
                    Console.Clear();
                    _view.DisplayBoard(_board, droppedRow, droppedCol);
                    Console.WriteLine($"{_currentPlayer.Name} has made a move.");

                    if (!ConsolePrompts.WaitForEnterWithEscape("Press Enter to continue (Esc for exit menu): "))
                    {
                        return false;
                    }
                }

                // Switch to the next player
                SwitchPlayer();
            }
        }

        return true;
    }

    /// <summary>
    /// Switches the current player to the other player.
    /// </summary>
    private void SwitchPlayer()
    {
        _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
    }
}
