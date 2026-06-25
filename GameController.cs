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
            SetupGame();
            PlayGame();
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
    private void SetupGame()
    {
        int gameMode = _view.DisplayMenu();
        _board = new Board();

        if (gameMode == 1)
        {
            // Human vs Human
            Console.Write("\nPlayer 1, enter your name: ");
            string? name1 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name1))
            {
                name1 = "Player 1";
            }

            Console.Write("Player 2, enter your name: ");
            string? name2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name2))
            {
                name2 = "Player 2";
            }

            _player1 = new HumanPlayer('X', name1);
            _player2 = new HumanPlayer('O', name2);
        }
        else
        {
            // Human vs Computer
            Console.Write("\nEnter your name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "Player";
            }

            _player1 = new HumanPlayer('X', name);
            _player2 = new ComputerPlayer('O');
        }

        _currentPlayer = _player1;
        Console.WriteLine($"\n{_player1.Name} will play as 'X'");
        Console.WriteLine($"{_player2.Name} will play as 'O'");
        Console.WriteLine("\nPress any key to start the game...");
        Console.ReadKey();
    }

    /// <summary>
    /// Main game loop - alternates turns until win or draw.
    /// </summary>
    private void PlayGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            Console.Clear();
            _view.DisplayBoard(_board);

            // Get the current player's move (demonstrates POLYMORPHISM)
            int column = _currentPlayer!.GetMove(_board);

            // Drop the piece
            _board.DropPiece(column, _currentPlayer.Symbol);

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
                // Switch to the next player
                SwitchPlayer();
            }
        }
    }

    /// <summary>
    /// Switches the current player to the other player.
    /// </summary>
    private void SwitchPlayer()
    {
        _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
    }
}
