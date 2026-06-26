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

        bool exitGame = false;

        while (!exitGame)
        {
            UserFlowAction setupAction = SetupGame();
            if (setupAction == UserFlowAction.ExitGame)
            {
                break;
            }

            if (setupAction == UserFlowAction.RestartToMenu)
            {
                _view.DisplayWelcome();
                continue;
            }

            while (true)
            {
                UserFlowAction playAction = PlayGame();
                if (playAction == UserFlowAction.ExitGame)
                {
                    exitGame = true;
                    break;
                }

                if (playAction == UserFlowAction.RestartToMenu)
                {
                    _view.DisplayWelcome();
                    break;
                }

                bool isHumanVsComputer = _player2 is ComputerPlayer;
                UserFlowAction postGameAction = _view.AskPostGameAction(isHumanVsComputer);
                if (postGameAction == UserFlowAction.Continue)
                {
                    _board = new Board();
                    _currentPlayer = _player1;
                    continue;
                }

                if (postGameAction == UserFlowAction.ChangeDifficulty)
                {
                    if (_player1 is HumanPlayer human && _player2 is ComputerPlayer cpu)
                    {
                        Console.Clear();

                        (UserFlowAction difficultyAction, AiDifficulty? difficulty) = _view.DisplayDifficultyMenu();
                        if (difficultyAction == UserFlowAction.RestartToMenu)
                        {
                            _view.DisplayWelcome();
                            break;
                        }

                        if (difficultyAction == UserFlowAction.ExitGame)
                        {
                            exitGame = true;
                            break;
                        }

                        (UserFlowAction nameAction, string? updatedPlayerNameRaw) = PromptForPlayerName($"Enter your name (blank = {human.Name}): ", human.Name);
                        if (nameAction == UserFlowAction.RestartToMenu)
                        {
                            _view.DisplayWelcome();
                            break;
                        }

                        if (nameAction == UserFlowAction.ExitGame)
                        {
                            exitGame = true;
                            break;
                        }

                        string updatedPlayerName = updatedPlayerNameRaw!;
                        bool restartToMenu = false;
                        bool reconfiguredMatch = false;

                        while (true)
                        {
                            (nameAction, string? updatedComputerNameRaw) = PromptForPlayerName($"Enter computer name (blank = {cpu.Name}): ", cpu.Name);
                            if (nameAction == UserFlowAction.RestartToMenu)
                            {
                                _view.DisplayWelcome();
                                restartToMenu = true;
                                break;
                            }

                            if (nameAction == UserFlowAction.ExitGame)
                            {
                                exitGame = true;
                                break;
                            }

                            string updatedComputerName = updatedComputerNameRaw!;
                            if (string.Equals(updatedPlayerName, updatedComputerName, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Computer name must be different from player name.");
                                continue;
                            }

                            _player1 = new HumanPlayer(human.Symbol, updatedPlayerName);
                            _player2 = new ComputerPlayer(cpu.Symbol, difficulty!.Value, updatedComputerName);
                            _board = new Board();
                            _currentPlayer = _player1;
                            reconfiguredMatch = true;
                            break;
                        }

                        if (exitGame)
                        {
                            break;
                        }

                        if (reconfiguredMatch)
                        {
                            continue;
                        }

                        if (restartToMenu)
                        {
                            break;
                        }

                        break;
                    }

                    _view.DisplayWelcome();
                    break;
                }

                if (postGameAction == UserFlowAction.RestartToMenu)
                {
                    _view.DisplayWelcome();
                    break;
                }

                exitGame = true;
                break;
            }
        }

        _view.DisplayGoodbye();
    }

    /// <summary>
    /// Sets up the game based on the selected game mode.
    /// </summary>
    /// <returns>Flow action after setup prompt handling.</returns>
    private UserFlowAction SetupGame()
    {
        int gameMode = _view.DisplayMenu();
        if (gameMode == 0)
        {
            return UserFlowAction.ExitGame;
        }

        _board = new Board();

        if (gameMode == 1)
        {
            // Human vs Human
            (UserFlowAction action, string? name) = PromptForPlayerName("\nPlayer 1, enter your name: ", "Player 1");
            if (action != UserFlowAction.Continue)
            {
                return action;
            }

            string name1 = name!;
            string? name2;

            while (true)
            {
                (action, name) = PromptForPlayerName("Player 2, enter your name: ", "Player 2");
                if (action != UserFlowAction.Continue)
                {
                    return action;
                }

                name2 = name;
                if (string.Equals(name1, name2, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Player 2 name must be different from Player 1.");
                    continue;
                }

                break;
            }

            _player1 = new HumanPlayer('X', name1);
            _player2 = new HumanPlayer('O', name2!);
        }
        else
        {
            // Human vs Computer
            (UserFlowAction action, string? name) = PromptForPlayerName("\nEnter your name: ", "Player");
            if (action != UserFlowAction.Continue)
            {
                return action;
            }

            string playerName = name!;
            string? computerName;

            while (true)
            {
                (action, name) = PromptForPlayerName("Enter computer name (blank = Computer): ", "Computer");
                if (action != UserFlowAction.Continue)
                {
                    return action;
                }

                computerName = name;
                if (string.Equals(playerName, computerName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Computer name must be different from player name.");
                    continue;
                }

                break;
            }

            (UserFlowAction difficultyAction, AiDifficulty? difficulty) = _view.DisplayDifficultyMenu();
            if (difficultyAction != UserFlowAction.Continue)
            {
                return difficultyAction;
            }

            _player1 = new HumanPlayer('X', playerName);
            _player2 = new ComputerPlayer('O', difficulty!.Value, computerName!);
        }

        _currentPlayer = _player1;
        Console.WriteLine($"\n{_player1.Name} will play as 'X'");
        Console.WriteLine($"{_player2.Name} will play as 'O'");

        // Poka-yoke gate: explicit Enter starts the cycle; Escape must be confirmed in exit menu.
        return ConsolePrompts.WaitForEnterWithEscape("\nPress Enter to start the game (Esc for exit menu): ");
    }

    private static (UserFlowAction Action, string? Name) PromptForPlayerName(string prompt, string defaultName)
    {
        while (true)
        {
            UserFlowAction action = ConsolePrompts.TryReadLineWithEscape(prompt, out string rawInput);
            if (action != UserFlowAction.Continue)
            {
                return (action, null);
            }

            string normalizedName = NormalizeName(rawInput);

            if (string.IsNullOrEmpty(normalizedName))
            {
                return (UserFlowAction.Continue, defaultName);
            }

            if (normalizedName.Length > 50)
            {
                Console.WriteLine("Name is too long. Use 50 characters or fewer.");
                continue;
            }

            return (UserFlowAction.Continue, normalizedName);
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
    /// <returns>Flow action after gameplay prompt handling.</returns>
    private UserFlowAction PlayGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            Console.Clear();
            _view.DisplayBoard(_board);
            DisplayMatchInfo();

            int column;
            try
            {
                // Get the current player's move (demonstrates POLYMORPHISM)
                column = _currentPlayer!.GetMove(_board);
            }
            catch (UserFlowException ex)
            {
                return ex.Action;
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
                bool requireMoveAcknowledgement = _currentPlayer is ComputerPlayer || (_player1 is HumanPlayer && _player2 is HumanPlayer);

                if (requireMoveAcknowledgement)
                {
                    Player nextPlayer = GetNextPlayer();
                    UserFlowAction action = WaitForMoveAcknowledgement(droppedRow, droppedCol, column, nextPlayer);
                    if (action != UserFlowAction.Continue)
                    {
                        return action;
                    }
                }

                // Switch to the next player
                SwitchPlayer();
            }
        }

        return UserFlowAction.Continue;
    }

    private UserFlowAction WaitForMoveAcknowledgement(int highlightedRow, int highlightedCol, int column, Player nextPlayer)
    {
        bool isHumanVsHuman = _player1 is HumanPlayer && _player2 is HumanPlayer;

        while (true)
        {
            Console.Clear();
            _view.DisplayBoard(_board, highlightedRow, highlightedCol);
            DisplayMatchInfo();

            Console.WriteLine($"Move recorded: {_currentPlayer!.Name} ({_currentPlayer.Symbol}) placed in column {column}.");

            if (isHumanVsHuman)
            {
                Console.WriteLine($"Next turn: {nextPlayer.Name} ({nextPlayer.Symbol}).");
                Console.Write("Press Enter to pass control (Esc for exit menu): ");
            }
            else
            {
                Console.WriteLine($"{nextPlayer.Name} ({nextPlayer.Symbol}), your turn is ready.");
                Console.Write("Press Enter to continue (Esc for exit menu): ");
            }

            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return UserFlowAction.Continue;
            }

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                UserFlowAction action = ConsolePrompts.ShowExitConfirmation();
                if (action != UserFlowAction.Continue)
                {
                    return action;
                }

                continue;
            }
        }
    }

    private Player GetNextPlayer()
    {
        return (_currentPlayer == _player1) ? _player2! : _player1!;
    }

    private void DisplayMatchInfo()
    {
        if (_player1 == null || _player2 == null)
        {
            return;
        }

        if (_player2 is ComputerPlayer computer)
        {
            Console.WriteLine("Mode: Human vs Computer");
            Console.WriteLine($"Players: {_player1.Name} ({_player1.Symbol}) vs {computer.Name} ({computer.Symbol})");
            Console.WriteLine($"AI Difficulty: {computer.Difficulty}");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Mode: Human vs Human");
        Console.WriteLine($"Players: {_player1.Name} ({_player1.Symbol}) vs {_player2.Name} ({_player2.Symbol})");
        Console.WriteLine();
    }

    /// <summary>
    /// Switches the current player to the other player.
    /// </summary>
    private void SwitchPlayer()
    {
        _currentPlayer = (_currentPlayer == _player1) ? _player2 : _player1;
    }
}
