using System;
using System.Collections.Generic;
using System.Text;

namespace Game_2048_KAN_version
{
    enum BoardEnum
    {
        empty,
        number_2,
        number_4,
        number_8,
        number_16,
        number_32,
        number_64,
        number_128,
        number_256,
        number_512,
        number_1024,
        number_2048,
        number_4096
    }
    class Game
    {
        public int Score { get; private set; }
        public bool GameIsFinished { get; private set; } = false;
        public bool IsWon { get; private set; } = false;
        static BoardEnum[,] NewBoard { get; } = new BoardEnum[4, 4];
        static Random Rand { get; } = new Random();          // for getting new random value
        static int Row { get; set; }                              // variable for row index
        static bool KeyPressed { get; set; } = false;             // variable for checking if it is time to break operation
        static int Column { get; set; }                           // variable for column index
        static int IndexForTempArray { get; set; }                // variable for temporary array index
        static BoardEnum[] TempArray { get;} = { BoardEnum.empty, BoardEnum.empty, BoardEnum.empty, BoardEnum.empty }; // temporary array
        public void NewNumbers()
        {
            bool success = false;
            int checkForEmptySpot = 0;

            while (!success)
            {
                for (int Row = 0; Row < NewBoard.GetLength(0); Row++)
                {
                    for (Column = 0; Column < NewBoard.GetLength(1); Column++)
                    {
                        if (NewBoard[Row, Column] == BoardEnum.empty)
                        {
                            checkForEmptySpot++;
                        }
                    }
                }
                if (checkForEmptySpot > 0)
                {
                    //int row = (int)Rand.Next(4);
                    //int column = (int)Rand.Next(4);
                    int newRandom = Rand.Next(4);
                    Row = (int)Rand.Next(4);
                    Column = (int)Rand.Next(4);

                    if (NewBoard[Row, Column] != BoardEnum.empty)
                    {
                        continue;
                    }
                    else if (newRandom == 0)
                    {
                        NewBoard[Row, Column] = BoardEnum.number_4;
                        success = true;
                    }
                    else
                    {
                        NewBoard[Row, Column] = BoardEnum.number_2;
                        success = true;
                    }
                }
                else
                {
                    success = CheckPossibleMove();
                    if (!success)
                    {
                        GameIsFinished = true;
                        success = true;
                    }
                }
            }
        }
        public void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine();

            for (Row = 0; Row < NewBoard.GetLength(0); Row++)                       // check row - 0, then row - 1 etc.
            {
                Console.WriteLine();
                Console.Write("           ");

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)          // check row - 0 column - 0, then row - 0 column - 1 etc.
                {
                    switch (NewBoard[Row, Column])
                    {
                        case BoardEnum.empty:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            // if spot's value is 0, print " _" 
                            Console.Write("|_____|");
                            break;
                        case BoardEnum.number_2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("|__2__|");
                            break;
                        case BoardEnum.number_4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("|__4__|");
                            break;
                        case BoardEnum.number_8:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("|__8__|");
                            break;
                        case BoardEnum.number_16:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("|__16_|");
                            break;
                        case BoardEnum.number_32:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("|__32_|");
                            break;
                        case BoardEnum.number_64:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("|__64_|");
                            break;
                        case BoardEnum.number_128:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("|_128_|");
                            break;
                        case BoardEnum.number_256:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("|_256_|");
                            break;
                        case BoardEnum.number_512:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("|_512_|");
                            break;
                        case BoardEnum.number_1024:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("| 1024|");
                            break;
                        case BoardEnum.number_2048:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("| 2048|");
                            break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public bool PressArrow() 
        {
            KeyPressed = false;
            while (!KeyPressed)
            {
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.RightArrow:
                        NewMoveRight();
                        return true;
                    case ConsoleKey.LeftArrow:
                        NewMoveLeft();
                        return true;
                    case ConsoleKey.UpArrow:
                        NewMoveUp();
                        return true;
                    case ConsoleKey.DownArrow:
                        NewMoveDown();
                        return true;
                    default:
                        Console.WriteLine();
                        return false; 
                }
            }
            return false;
        }
        public void EscapeForExit()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }
        private void NewMoveRight()
        {
            for (int row = 0; row < NewBoard.GetLength(0); row++)
            {
                IndexForTempArray = 0;

                for (Column = NewBoard.GetLength(1) - 1; Column >= 0; Column--)
                {
                    if (NewBoard[row, Column] != BoardEnum.empty)
                    {
                        TempArray[IndexForTempArray] = NewBoard[row, Column];
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                IndexForTempArray = 0;

                for (Column = NewBoard.GetLength(1) - 1; Column >= 0; Column--)
                {
                    NewBoard[row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray += 1;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
            KeyPressed = true;
        }
        private void NewMoveLeft()
        {
            for (int Row = 0; Row < NewBoard.GetLength(0); Row++)             // check row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)    // check row - 0 column - 0, then row - 0 column - 1 etc.
                {                                                             // 
                    if (NewBoard[Row, Column] != BoardEnum.empty)             // if spot not empty             
                    {                                                         //
                        TempArray[IndexForTempArray] = NewBoard[Row, Column]; // than move this value to temp array
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)    // to move values from temp array to board
                {
                    NewBoard[Row, Column] = TempArray[Column];
                    TempArray[Column] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
            KeyPressed = true;
        }
        private void NewMoveUp()
        {
            for (int Column = 0; Column < NewBoard.GetLength(1); Column++)
            {
                IndexForTempArray = 0;

                for (Row = 0; Row < NewBoard.GetLength(0); Row++)
                {
                    if (NewBoard[Row, Column] != BoardEnum.empty)
                    {
                        TempArray[IndexForTempArray] = NewBoard[Row, Column];
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                for (Row = 0; Row < NewBoard.GetLength(0); Row++)
                {
                    NewBoard[Row, Column] = TempArray[Row];
                    TempArray[Row] = BoardEnum.empty;
                }
            }
            PrintBoard();
            Console.WriteLine();
            KeyPressed = true;
        }
        private void NewMoveDown()
        {
            for (int Column = 0; Column < NewBoard.GetLength(1); Column++)
            {
                IndexForTempArray = 0;

                for (Row = (NewBoard.GetLength(0) - 1); Row >= 0; Row--)
                {
                    if (NewBoard[Row, Column] != BoardEnum.empty)
                    {
                        TempArray[IndexForTempArray] = NewBoard[Row, Column];
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                IndexForTempArray = 0;

                for (Row = (NewBoard.GetLength(0) - 1); Row >= 0; Row--)
                {
                    NewBoard[Row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray++;
                }
            }
            PrintBoard();
            Console.WriteLine();
            KeyPressed = true;
        }
        private bool CheckPossibleMove()
        {
            // row1, check if col1 == col2 or col2 == col3 or col3 == col4
            for (int Row = 0; Row < NewBoard.GetLength(0); Row++)
            {
                if (NewBoard[Row, 0] == NewBoard[Row, 1] ||
                    NewBoard[Row, 1] == NewBoard[Row, 2] ||
                    NewBoard[Row, 2] == NewBoard[Row, 3])
                {
                    return true;
                }
            }
            for (Column = 0; Column < NewBoard.GetLength(1); Column++)
            {
                if (NewBoard[0, Column] == NewBoard[1, Column] ||
                    NewBoard[1, Column] == NewBoard[2, Column] ||
                    NewBoard[2, Column] == NewBoard[3, Column])
                {
                    return true;
                }
            }
            return false;
        }
        private BoardEnum[] CheckTempArray(BoardEnum[] tempArray)
        {
            if (TempArray[0] == TempArray[1])                     // for example index0 = index1 // 2 2 0 0
            {
                TempArray[0] = DoubleNumber(TempArray[0]);        // then to double index0

                if (TempArray[2] == TempArray[3])                  // and index2 = index3 // 2 2 2 2 
                {
                    TempArray[1] = DoubleNumber(TempArray[2]);
                    TempArray[2] = BoardEnum.empty;
                    TempArray[3] = BoardEnum.empty;
                }
                else
                {
                    TempArray[1] = TempArray[2];
                    TempArray[2] = TempArray[3];
                    TempArray[3] = BoardEnum.empty;
                }
                return tempArray;
            }
            else if (TempArray[1] == TempArray[2])                  // 2 4 4 2
            {
                TempArray[1] = DoubleNumber(TempArray[1]);
                TempArray[2] = TempArray[3];
                TempArray[3] = BoardEnum.empty;
                return tempArray;
            }
            else if (TempArray[2] == TempArray[3])                  // 2 4 2 2
            {
                TempArray[2] = DoubleNumber(TempArray[2]);
                TempArray[3] = BoardEnum.empty;
                return tempArray;
            }
            return tempArray;
        }
        private BoardEnum DoubleNumber(BoardEnum boardEnum)
        {
            switch (boardEnum)
            {
                case BoardEnum.number_2:
                    Score += 4;
                    return BoardEnum.number_4;
                case BoardEnum.number_4:
                    Score += 8;
                    //IsWon = true;
                    //gameIsFinished = true;
                    return BoardEnum.number_8;
                case BoardEnum.number_8:
                    Score += 16;
                    return BoardEnum.number_16;
                case BoardEnum.number_16:
                    Score += 32;
                    return BoardEnum.number_32;
                case BoardEnum.number_32:
                    Score += 64;
                    return BoardEnum.number_64;
                case BoardEnum.number_64:
                    Score += 128;
                    return BoardEnum.number_128;
                case BoardEnum.number_128:
                    Score += 256;
                    return BoardEnum.number_256;
                case BoardEnum.number_256:
                    Score += 512;
                    return BoardEnum.number_512;
                case BoardEnum.number_512:
                    Score += 1024;
                    return BoardEnum.number_1024;
                case BoardEnum.number_1024:
                    Score += 2048;
                    IsWon = true;
                    GameIsFinished = true;
                    return BoardEnum.number_2048;
                default:
                    return BoardEnum.empty;
            }
        }
    }
}
