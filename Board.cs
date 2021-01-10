using System;
using System.Collections.Generic;
using System.Text;

namespace Day9_temp
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
    class Board
    {
        public BoardEnum[,] newBoard { get; set; } = new BoardEnum[4, 4];

        static Random Rand { get; set; } = new Random();          // for getting new random value
        static bool Success { get; set; } = false;                // variable for checking if it is time to break operation
        static int Row { get; set; }                              // variable for row index
        static int Column { get; set; }                           // variable for column index
        static int IndexForTempArray { get; set; }                // variable for temporary array index
        static BoardEnum[] TempArray { get; set; } = { BoardEnum.empty, BoardEnum.empty, BoardEnum.empty, BoardEnum.empty }; // temporary array
        public void PrintBoard()
        {
            //Console.Clear();
            Console.WriteLine();

            for (Row = 0; Row < newBoard.GetLength(0); Row++)                       // check row - 0, then row - 1 etc.
            {
                for (Column = 0; Column < newBoard.GetLength(1); Column++)          // check row - 0 column - 0, then row - 0 column - 1 etc.
                {
                    switch (newBoard[Row, Column])
                    {
                        case BoardEnum.empty:
                            Console.ForegroundColor = ConsoleColor.Yellow;          // if spot's value is 0, print " _" 
                            Console.Write("   _");
                            break;
                        case BoardEnum.number_2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("   2");
                            break;
                        case BoardEnum.number_4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("   4");
                            break;
                        case BoardEnum.number_8:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("   8");
                            break;
                        case BoardEnum.number_16:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("  16");
                            break;
                        case BoardEnum.number_32:
                            break;
                        case BoardEnum.number_64:
                            break;
                        case BoardEnum.number_128:
                            break;
                        case BoardEnum.number_256:
                            break;
                        case BoardEnum.number_512:
                            break;
                        case BoardEnum.number_1024:
                            break;
                        case BoardEnum.number_2048:
                            break;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void newNumbers()
        {
            Success = false;

            while (!Success)
            {
                int row = (int)Rand.Next(4);
                int column = (int)Rand.Next(4);
                if (newBoard[row, column] != BoardEnum.empty)
                {
                    continue;
                }
                else
                {
                    newBoard[row, column] = BoardEnum.number_2;
                    Success = true;
                }
            }
        }
        public void newMoveLeft()
        {
            for (int row = 0; row < newBoard.GetLength(0); row++)             // check row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = 0; Column < newBoard.GetLength(1); Column++)    // check row - 0 column - 0, then row - 0 column - 1 etc.
                {                                                             // 
                    if (newBoard[row, Column] != BoardEnum.empty)             // if spot not empty             
                    {                                                         //
                        TempArray[IndexForTempArray] = newBoard[row, Column]; // than move this value to temp array
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                for (Column = 0; Column < newBoard.GetLength(1); Column++)    // to move values from temp array to board
                {
                    newBoard[row, Column] = TempArray[Column];
                    TempArray[Column] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
        }
        public void newMoveRight()
        {
            for (int row = 0; row < newBoard.GetLength(0); row++)             // check row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = newBoard.GetLength(1)-1; Column >= 0; Column--) // check row - 0 column - 3, then row - 0 column - 2 etc.
                {                                                             // 
                    if (newBoard[row, Column] != BoardEnum.empty)             // if spot not empty             
                    {                                                         //
                        TempArray[IndexForTempArray] = newBoard[row, Column]; // than move this value to temp array
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                IndexForTempArray = 0;

                for (Column = newBoard.GetLength(1)-1; Column >= 0; Column--)    // to move values from temp array to board
                {
                    newBoard[row, Column] = TempArray[IndexForTempArray];
                    IndexForTempArray += 1;
                    TempArray[Column] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
        }

        private BoardEnum[] CheckTempArray(BoardEnum[] tempArray)
        {
            for (int j = 1; j < TempArray.Length; j++)                    // chech each value in the temp array from 1 (not 0) to the last
            {                                                             // 
                if (TempArray[j] == TempArray[j - 1])                     // if value is equal to the previous (for example index1 = index0)
                {                                                         //  
                    TempArray[j - 1] = DoubleNumber(TempArray[j]);        // then to double the previous (index0)
                    TempArray[j] = BoardEnum.empty;                       // and to assign empty to index1 
                    if (j+1 <= TempArray.Length-1)
                    {
                        TempArray[j] = TempArray[j + 1];
                    }
                    if (j + 2 <= TempArray.Length-1)
                    {
                        TempArray[j+1] = TempArray[j + 2];
                    }
                }
            }
            return tempArray;
        }

        private BoardEnum DoubleNumber(BoardEnum boardEnum)
        {
            switch (boardEnum)
            {
                case BoardEnum.empty:
                    return BoardEnum.empty;
                case BoardEnum.number_2:
                    return BoardEnum.number_4;
                case BoardEnum.number_4:
                    return BoardEnum.number_8;
                case BoardEnum.number_8:
                    return BoardEnum.number_16;
                case BoardEnum.number_16:
                    return BoardEnum.number_32;
                case BoardEnum.number_32:
                    return BoardEnum.number_64;
                case BoardEnum.number_64:
                    return BoardEnum.number_128;
                case BoardEnum.number_128:
                    return BoardEnum.number_256;
                case BoardEnum.number_256:
                    return BoardEnum.number_512;
                case BoardEnum.number_512:
                    return BoardEnum.number_1024;
                case BoardEnum.number_1024:
                    return BoardEnum.number_2048;
                case BoardEnum.number_2048:
                    return BoardEnum.number_4096;
                default:
                    return BoardEnum.empty;
            }
        }
    }
}
