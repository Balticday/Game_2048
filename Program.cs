using System;
using System.Threading;

namespace Day9_temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board newGame = new Board();
            bool gameIsFinished = false;

            PrintInYellow(@"                WELCOME TO THE GAME ""KAN 2048!""");
            PrintInYellow("                      / version 1.105 /");
            PrintInYellow("-----------------------------------------------------------------");

            Welcome();

            newGame.NewNumbers();                                            // method create a new number 

            do // because we have to print the board at least once
            {
                PrintInYellow($"GAME OVER! Thank you for playing! Your SCORE is {newGame.Score}");
                Console.WriteLine();

                newGame.NewNumbers();                                        // method create a new number 
                newGame.PrintBoard();                                        // and print board with new numbers

                Console.WriteLine(); 
                PrintInBlack($"                SCORE: {newGame.Score}   ");
                Console.WriteLine();

                PrintInYellow("For new move press arrow: ");

                newGame.KeyPressed = false;
                while (!newGame.KeyPressed)
                {
                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.RightArrow:
                            newGame.newMoveRight();
                            break;
                        case ConsoleKey.LeftArrow:
                            newGame.newMoveLeft();
                            break;
                        case ConsoleKey.UpArrow:
                            newGame.newMoveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            newGame.newMoveDown();
                            break;
                        default:
                            Console.WriteLine();
                            PrintInRed("ERROR! Please press an ARROW key.");
                            break;
                    } 
                }
            } while (!gameIsFinished);                                                  // "true" is temporary solution
        }
        static void Welcome()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine("                    Simple rules to follow:                      ");
            Console.WriteLine();
            Thread.Sleep(3000);
            Console.Beep();
            Console.WriteLine("1. You need to move the numbers and every time you move one,     ");
            Console.WriteLine("   another number pops up in a random manner anywhere in the box.");
            Console.WriteLine();
            Thread.Sleep(3000);
            Console.Beep();
            Console.WriteLine("2. When two tiles with the same number on them collide           ");
            Console.WriteLine("   they will merge into one tile that equals to their sum.       ");
            Console.WriteLine();
            Thread.Sleep(3000);
            Console.Beep();
            Console.WriteLine("3. Press ARROW keys to move the numbers!                         ");
            Console.ResetColor();

            Console.WriteLine();
            PrintInYellow("              Press any key for the game to start:");
            Console.ReadKey();

        }
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
        public string GameIsFinished(bool gameIsFinished)
        {
            if (!gameIsFinished)
            {
                Console.WriteLine("The game is in progress...");
            }
            return $"Congratulations! You won!";
        }
    }
}
