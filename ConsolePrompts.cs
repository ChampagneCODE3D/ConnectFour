namespace ConnectFour;

public static class ConsolePrompts
{
    public static UserFlowAction ShowExitConfirmation()
    {
        Console.WriteLine();
        Console.WriteLine("Exit Menu");
        Console.WriteLine("1. Return to game");
        Console.WriteLine("2. Restart to main menu");
        Console.WriteLine("3. Exit game");
        Console.Write("Choose 1, 2, or 3: ");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.KeyChar == '1')
            {
                Console.WriteLine('1');
                return UserFlowAction.Continue;
            }

            if (key.KeyChar == '2')
            {
                Console.WriteLine('2');
                return UserFlowAction.RestartToMenu;
            }

            if (key.KeyChar == '3' || key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine('3');
                return UserFlowAction.ExitGame;
            }
        }
    }

    /// <summary>
    /// Poka-yoke style confirmation gate: only Enter advances the flow.
    /// Escape routes to a confirm-exit menu to prevent accidental progression.
    /// </summary>
    public static UserFlowAction WaitForEnterWithEscape(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    UserFlowAction action = ShowExitConfirmation();

                    if (action != UserFlowAction.Continue)
                    {
                        return action;
                    }

                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return UserFlowAction.Continue;
                }
            }
        }
    }

    public static UserFlowAction TryReadLineWithEscape(string prompt, out string input)
    {
        while (true)
        {
            Console.Write(prompt);
            List<char> chars = new();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    UserFlowAction action = ShowExitConfirmation();

                    if (action != UserFlowAction.Continue)
                    {
                        input = string.Empty;
                        return action;
                    }

                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    input = new string([.. chars]);
                    return UserFlowAction.Continue;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (chars.Count > 0)
                    {
                        chars.RemoveAt(chars.Count - 1);
                        Console.Write("\b \b");
                    }

                    continue;
                }

                if (!char.IsControl(key.KeyChar))
                {
                    chars.Add(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
        }
    }
}
