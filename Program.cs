using System;
using System.Threading;

namespace Day9_temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game newGame = new Game();

            PrintInYellow(@"                WELCOME TO THE GAME ""KAN 2048!""");
            PrintInYellow("                      / version 1.105 /");
            PrintInYellow("-----------------------------------------------------------------");
            Welcome();

            newGame.NewNumbers();                                            // method create a new number 

            do                                                               // because we have to print the board at least once
            {
                newGame.NewNumbers();                                        // method create a new number 
                newGame.PrintBoard();                                        // and print board with new numbers

                Console.WriteLine(); 
                Console.Write("                   "); 
                PrintInBlack($" SCORE: {newGame.Score}   ");
                Console.WriteLine();
                PrintInYellow("            For new move press arrow: ");

                #region Arrow pressing
                newGame.KeyPressed = false;
                while (!newGame.KeyPressed)
                {
                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.RightArrow:
                            newGame.NewMoveRight();
                            break;
                        case ConsoleKey.LeftArrow:
                            newGame.NewMoveLeft();
                            break;
                        case ConsoleKey.UpArrow:
                            newGame.NewMoveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            newGame.NewMoveDown();
                            break;
                        default:
                            Console.WriteLine();
                            PrintInRed("ERROR! Please press an ARROW key.");
                            break;
                    } 
                }
                #endregion Arrow pressing

            } while (!newGame.GameIsFinished);                                // "true" is temporary solution

            PrintInYellow(newGame.IsWon == true ? "Congratulations! You won!" : $"GAME OVER! Thank you for playing! Your SCORE is {newGame.Score}");

            Console.WriteLine();
            PrintInYellow("Press ESCAPE to close game:");
            //Console.ReadKey();
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }

        #region Name of the game and rules
        static string Welcome()
        {
            PrintInYellow("        Press ANY key to continue OR 'ENTER' to SKIP the rules:");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine();
                PrintInBlack("                    Simple rules to follow:                      ");
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.Beep();
                PrintInBlack("1. You need to move the numbers and every time you move one,     ");
                PrintInBlack("   another number pops up in a random manner anywhere in the box.");
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.Beep();
                PrintInBlack("2. When two tiles with the same number on them collide           ");
                PrintInBlack("   they will merge into one tile that equals to their sum.       ");
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.Beep();
                PrintInBlack("3. Press ARROW keys to move the numbers!                         ");
                Console.WriteLine();
                PrintInYellow("              Press any key for the game to start:");
                Console.ReadKey();
                return "";
            }
            return "";
        }
        #endregion Name of the game and rules

        //private static void Congratulations()
        //{
        //    Console.WriteLine("Congratulations! You won! ");
        //}
        //private static void InfoAboutGameIsOver(int score)
        //{
        //    Console.WriteLine($"GAME OVER! Thank you for playing! Your SCORE is {score}");
        //}

        #region Methods for printing colored text
        private static void PrintInRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static void PrintInYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static void PrintInBlack(string text)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        #endregion Methods for printing colored text
    }
}
