using System.Security.Cryptography;

namespace rspls
{
    class Program
    {
        static bool CountArguments(string[] arguments)
        {
            if (arguments.Length < 3 || arguments.Length % 2 == 0 && arguments.Length != arguments.Distinct().Count())
            {
                Console.WriteLine("Incorrect result. You must write 3 or more arguments, but odd numbers arguments or all throws should difference ");
                return false;
            }

            return true;
        }


        static void Main(string[] arguments)
        {
            if (!CountArguments(arguments))
            {
                return;
            }


            EncryptionKey encryptionKey = new EncryptionKey();
            TableRules tableRules = new TableRules(arguments);
            Referee referee = new Referee(arguments.Length);

            bool endofthegame = false;

            while (!endofthegame)
            {
                string key = encryptionKey.GenerateKey();
                int pcthrow = RandomNumberGenerator.GetInt32(arguments.Length);
                string hmac = encryptionKey.GenerateHMAC(key, arguments[pcthrow]);

                Console.WriteLine("HMAC: " + hmac);

                Console.WriteLine("Available throws:");
                for (int i = 0; i < arguments.Length; i++)
                {
                    Console.WriteLine(i + 1 + " - " + arguments[i]);
                }
                Console.WriteLine("0 - exit");
                Console.WriteLine("? - help");

                Console.Write("Choose a throw: ");
                var ans = Console.ReadLine();

                if (ans == "?")
                {
                    tableRules.OnDisplay();
                    Console.Write("\n\n\n");
                    continue;
                }

                if (ans == "0")
                {
                    endofthegame = true;
                    continue;
                }

                int mythrow = 0;

                if (!int.TryParse(ans, out mythrow) || mythrow <= 0 || mythrow > arguments.Length)
                {
                    Console.Write("\n\n\n");
                    continue;
                }

                Console.WriteLine("Your move: " + arguments[mythrow - 1]);
                Console.WriteLine("Computer move: " + arguments[pcthrow]);

                switch (referee.Result(pcthrow, mythrow - 1))
                {
                    case PartyResult.win:
                        Console.WriteLine("You won!");
                        break;

                    case PartyResult.lose:
                        Console.WriteLine("You lost!");
                        break;

                    default:
                        Console.WriteLine("Draw!");
                        break;
                }

                Console.WriteLine("HMAC key: " + key);
                Console.Write("\n\n\n");
            }
        }
    }
}
