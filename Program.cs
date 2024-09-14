using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class KeyGenerator
{
    public static byte[] GenerateKey(int length = 32)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] key = new byte[length];
            rng.GetBytes(key);
            return key;
        }
    }
}

class HMACCalculator
{
    private readonly byte[] _key;

    public HMACCalculator(byte[] key)
    {
        _key = key;
    }

    public string CalculateHMAC(string message)
    {
        using (var hmac = new HMACSHA256(_key))
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

class GameRules
{
    private readonly string[] _moves;

    public GameRules(string[] moves)
    {
        _moves = moves;
    }

    public string DetermineWinner(string playerMove, string computerMove)
    {
        if (playerMove == computerMove)
            return "Draw";

        int half = _moves.Length / 2;
        int playerIndex = Array.IndexOf(_moves, playerMove);
        int computerIndex = Array.IndexOf(_moves, computerMove);

        if ((computerIndex - playerIndex + _moves.Length) % _moves.Length <= half)
            return "Lose";
        else
            return "Win";
    }
}

class HelpTable
{
    public static void GenerateTable(string[] moves)
    {
        int n = moves.Length;
        Console.Write("     ");
        Console.WriteLine(string.Join("  ", moves));

        for (int i = 0; i < n; i++)
        {
            Console.Write(moves[i] + "  ");
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    Console.Write("Draw  ");
                else if ((j - i + n) % n <= n / 2)
                    Console.Write("Lose  ");
                else
                    Console.Write("Win   ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ensure valid input: odd number of moves (>= 3), non-repeating
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            Console.WriteLine($"Error: You must pass an odd number ≥ 3 of non-repeating moves.\nExample: {AppDomain.CurrentDomain.FriendlyName} Rock Paper Scissors");
            return;
        }

        string[] moves = args.Distinct().ToArray();

        if (moves.Length != args.Length)
        {
            Console.WriteLine("Error: Moves must be non-repeating.");
            return;
        }

        // Generate cryptographic key
        byte[] key = KeyGenerator.GenerateKey();
        // Select a random computer move
        string computerMove = moves[new Random().Next(moves.Length)];
        // Calculate HMAC for computer's move
        var hmacCalculator = new HMACCalculator(key);
        string hmac = hmacCalculator.CalculateHMAC(computerMove);

        Console.WriteLine($"HMAC: {hmac}");

        var rules = new GameRules(moves);

        while (true)
        {
            // Display menu
            Console.WriteLine("\nAvailable moves:");
            for (int i = 0; i < moves.Length; i++)
                Console.WriteLine($"{i + 1} - {moves[i]}");
            Console.WriteLine("0 - Exit\n? - Help");

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("Game exited.");
                break;
            }
            else if (input == "?")
            {
                HelpTable.GenerateTable(moves);
                continue;
            }

            // Parse user's move
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > moves.Length)
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            string playerMove = moves[choice - 1];
            string result = rules.DetermineWinner(playerMove, computerMove);

            // Display resultS
            Console.WriteLine($"Your move: {playerMove}");
            Console.WriteLine($"Computer's move: {computerMove}");
            Console.WriteLine($"Result: You {result}");
            Console.WriteLine($"HMAC key: {BitConverter.ToString(key).Replace("-", "").ToLower()}");
            break;
        }

    }
}
