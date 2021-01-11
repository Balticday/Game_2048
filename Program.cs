using System;

namespace Day9_temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board newGame = new Board();

            int iterationCounter = 1;

            do // because we have to print the board at least once
            {
                Console.WriteLine($"____________________________Iteration No {iterationCounter}");        // just for me ;) 
                iterationCounter++;
                Console.WriteLine();

                Console.WriteLine("New numbers!");                           // just for seeing that new numbers/number appear
                newGame.newNumbers();                                        // method create a new number 
                newGame.newNumbers();                                        // method create the second number 
                newGame.PrintBoard();                                        // and print board with new numbers
                Console.WriteLine();

                Console.Write("For new move please press arrow: ");

                switch (Console.ReadKey(false).Key)
                {
                    //case ConsoleKey.RightArrow:
                    //    newGame.newMoveRight();
                    //    break;
                    case ConsoleKey.LeftArrow:
                        newGame.newMoveLeft();
                        break;
                    case ConsoleKey.UpArrow:
                        newGame.newMoveUp();
                        break;
                        //case ConsoleKey.DownArrow:
                        //    newGame.newMoveDown();
                        //    break;

                }

            } while (true);                                                  // "true" is temporary solution
        }
    }
}
