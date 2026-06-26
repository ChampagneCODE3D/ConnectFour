namespace ConnectFour;

public static class ConsolePrompts
{
    public static bool ShowExitConfirmation()
    {
        Console.WriteLine();
        Console.WriteLine("Exit Menu");
        Console.WriteLine("1. Return to game");
        Console.WriteLine("2. Exit game");
        Console.Write("Choose 1 or 2: ");

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(intercept: true);

            if (key.KeyChar == '1')
            {
                Console.WriteLine('1');
                return false;
            }

            if (key.KeyChar == '2' || key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine('2');
                return true;
            }
        }
    }

    /// <summary>
    /// Poka-yoke style confirmation gate: only Enter advances the flow.
    /// Escape routes to a confirm-exit menu to prevent accidental progression.
    /// </summary>
    public static bool WaitForEnterWithEscape(string prompt)
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

                    if (ShowExitConfirmation())
                    {
                        return false;
                    }

                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return true;
                }
            }
        }
    }

    public static bool TryReadLineWithEscape(string prompt, out string input)
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

                    if (ShowExitConfirmation())
                    {
                        input = string.Empty;
                        return false;
                    }

                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    input = new string([.. chars]);
                    return true;
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
