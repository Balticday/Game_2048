using System;
using System.Threading;

namespace Game_2048__KAN_version
{
    class Program
    {
        static void Main(string[] args)
        {
            Game newGame = new Game();

            #region Name of the game and rules
            Console.WriteLine();
            PrintInYellow(@"                WELCOME TO THE GAME ""KAN 2048!""");              //welocoming message and rules for the game
            PrintInYellow("                      / version 1.105 /");
            PrintInYellow("-----------------------------------------------------------------");

            PrintInYellow("        Press ANY key to continue OR 'ENTER' to SKIP the rules:");

            if (Console.ReadKey().Key != ConsoleKey.Enter)                                     // if player wants to skip the rules -> press "ENTER"
            {
                Console.WriteLine();
                PrintInBlack("                    Simple rules to follow:                      ");
                Console.WriteLine();
                Thread.Sleep(2000);
                Console.Beep();
                PrintInBlack("1. You need to move the numbers and every time you move one,     ");
                PrintInBlack("   another number pops up in a random manner anywhere in the box.");
                Console.WriteLine();
                Thread.Sleep(2000);
                Console.Beep();
                PrintInBlack("2. When two tiles with the same number on them collide           ");
                PrintInBlack("   they will merge into one tile that equals to their sum.       ");
                Console.WriteLine();
                Thread.Sleep(2000);
                Console.Beep();
                PrintInBlack("3. Press ARROW keys to move the numbers!                         ");
                Console.WriteLine();

                PrintInYellow("              Press ANY key for the game to start:");             // to start the game a player can press ENTER
                Console.ReadKey(false);
            }
            #endregion Name of the game and rules

            newGame.NewNumbers();                                         // method creates a new number 
                                                                          // (because at the beginning we need two numbers) 
            do                                                            // using "do..while" loop 
            {                                                             // because we have to print the board at least once
                newGame.NewNumbers();                                     // method creates a new number 
                newGame.PrintBoard();                                     // and prints the board with new numbers

                #region Info below board about score and key for move
                Console.WriteLine();
                Console.Write("                   ");
                PrintInBlack($" SCORE: {newGame.Score}   ");              // prints the player's score
                Console.WriteLine();
                PrintInYellow("            For new move press arrow: ");
                #endregion Info below board about score and key for move

                #region Arrow pressing
                while (!newGame.PressArrow())
                {
                    PrintInRed("ERROR! Please press an ARROW key.");      // if player presses a key that is not an ARROW
                }
                #endregion Arrow pressing

            } while (!newGame.GameIsFinished);                            // exits the loop when game is over

            PrintInYellow(newGame.IsWon == true ? "Congratulations! YOU WON! Your SCORE is {newGame.Score}" :
                                                 $"GAME OVER! Thank you for playing! Your SCORE is {newGame.Score}");
            // lines above: check if the player won or the game is just over
            #region Info about exit
            Console.WriteLine();
            PrintInYellow("Press ESCAPE to close game:");                 // to exit the game when it is finished
            newGame.EscapeForExit();
            #endregion Info about exit
        }
        #region Methods for printing colored text
        private static void PrintInRed(string text)                       // a method for printing a text in red color
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static void PrintInYellow(string text)                   // a method for printing a text in yellow color
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private static void PrintInBlack(string text)                    // a method for printing highlighted text 
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        #endregion Methods for printing colored text
    }
}
