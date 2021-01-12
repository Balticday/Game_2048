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
        public int Score { get; set; }

        public void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine();

            for (Row = 0; Row < newBoard.GetLength(0); Row++)                       // check row - 0, then row - 1 etc.
            {
                for (Column = 0; Column < newBoard.GetLength(1); Column++)          // check row - 0 column - 0, then row - 0 column - 1 etc.
                {
                    switch (newBoard[Row, Column])
                    {
                        case BoardEnum.empty:
                            Console.ForegroundColor = ConsoleColor.Yellow;          // if spot's value is 0, print " _" 
                            Console.Write("    _");
                            break;
                        case BoardEnum.number_2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("    2");
                            break;
                        case BoardEnum.number_4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("    4");
                            break;
                        case BoardEnum.number_8:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("    8");
                            break;
                        case BoardEnum.number_16:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("   16");
                            break;
                        case BoardEnum.number_32:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("   32");
                            break;
                        case BoardEnum.number_64:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("   64");
                            break;
                        case BoardEnum.number_128:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("  128");
                            break;
                        case BoardEnum.number_256:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("  256");
                            break;
                        case BoardEnum.number_512:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("  512");
                            break;
                        case BoardEnum.number_1024:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(" 1024");
                            break;
                        case BoardEnum.number_2048:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(" 2048");
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
            for (int Row = 0; Row < newBoard.GetLength(0); Row++)             // check row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = 0; Column < newBoard.GetLength(1); Column++)    // check row - 0 column - 0, then row - 0 column - 1 etc.
                {                                                             // 
                    if (newBoard[Row, Column] != BoardEnum.empty)             // if spot not empty             
                    {                                                         //
                        TempArray[IndexForTempArray] = newBoard[Row, Column]; // than move this value to temp array
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                for (Column = 0; Column < newBoard.GetLength(1); Column++)    // to move values from temp array to board
                {
                    newBoard[Row, Column] = TempArray[Column];
                    TempArray[Column] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
        }
        public void newMoveRight()
        {
            for (int row = 0; row < newBoard.GetLength(0); row++)            
            {
                IndexForTempArray = 0;

                for (Column = newBoard.GetLength(1)-1; Column >= 0; Column--) 
                {                                                              
                    if (newBoard[row, Column] != BoardEnum.empty)                      
                    {                                                         
                        TempArray[IndexForTempArray] = newBoard[row, Column]; 
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                IndexForTempArray = 0;

                for (Column = newBoard.GetLength(1)-1; Column >= 0; Column--)
                {
                    newBoard[row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray += 1;
                }
            }
            PrintBoard();                                                     // print the board for seeng new values
            Console.WriteLine();
        }
        public void newMoveUp()
        {
            for (int Column = 0; Column < newBoard.GetLength(1); Column++)            
            {
                IndexForTempArray = 0;

                for (Row = 0; Row < newBoard.GetLength(0); Row++)    
                {                                                              
                    if (newBoard[Row, Column] != BoardEnum.empty)                      
                    {                                                         
                        TempArray[IndexForTempArray] = newBoard[Row, Column]; 
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);

                for (Row = 0; Row < newBoard.GetLength(0); Row++)
                {
                    newBoard[Row, Column] = TempArray[Row];
                    TempArray[Row] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                     
            Console.WriteLine();
        }
        public void newMoveDown()
        {
            for (int Column = 0; Column < newBoard.GetLength(1); Column++)
            {
                IndexForTempArray = 0;

                for (Row = (newBoard.GetLength(0) - 1); Row >=0; Row--)
                {
                    if (newBoard[Row, Column] != BoardEnum.empty)
                    {
                        TempArray[IndexForTempArray] = newBoard[Row, Column];
                        IndexForTempArray += 1;
                    }
                }
                CheckTempArray(TempArray);
                
                IndexForTempArray = 0;

                for (Row = (newBoard.GetLength(0) - 1); Row >= 0; Row--)
                {
                    newBoard[Row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray++;
                }
            }
            PrintBoard();
            Console.WriteLine();
        }

        private BoardEnum[] CheckTempArray(BoardEnum[] tempArray)
        {
            if (TempArray[0] == TempArray[1])                     // for example index0 = index1 // 2 2 0 0
            {                                                           
                TempArray[0] = DoubleNumber(TempArray[0]);        // then to double index0

                if (TempArray[2]== TempArray[3])                  // and index2 = index3 // 2 2 2 2 
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
                case BoardEnum.empty:
                    return BoardEnum.empty;
                case BoardEnum.number_2:
                    Score += 4;
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
