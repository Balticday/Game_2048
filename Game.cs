using System;
using System.Collections.Generic;
using System.Text;

namespace Game_2048__KAN_version
{
    enum BoardEnum                                                // creates an Enum for all possible numbers in the game
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
        number_2048
    }
    class Game
    {
        public int Score { get; private set; }                    // a variable for score
        public bool GameIsFinished { get; private set; } = false; // a variable for checking if the game is over
        public bool IsWon { get; private set; } = false;          // a variable for checking if player is won

        static BoardEnum[,] NewBoard { get; } = new BoardEnum[4, 4]; // creates the board using 2D array
        static Random Rand { get; } = new Random();               // for getting new random value
        static int Row { get; set; }                              // a variable for a row index
        static bool KeyPressed { get; set; } = false;             // a variable for checking if it is time to break operation
        static int Column { get; set; }                           // a variable for a column index
        static int IndexForTempArray { get; set; }                // a variable for temporary array index
        static BoardEnum[] TempArray { get; } = { BoardEnum.empty, BoardEnum.empty, BoardEnum.empty, BoardEnum.empty }; // a temporary array

        public void NewNumbers()                                      // a method which creates new random numbers ( "2" or "4")
        {
            bool success = false;
            int checkForEmptySpot = 0;                                  // a variable for empty spot

            while (!success)
            {
                for (int Row = 0; Row < NewBoard.GetLength(0); Row++)   // check row - 0, then row - 1 etc.
                {
                    for (Column = 0; Column < NewBoard.GetLength(1); Column++) // check row - 0 column - 0, then row - 0 column - 1 etc.
                    {
                        if (NewBoard[Row, Column] == BoardEnum.empty)
                        {
                            checkForEmptySpot++;                        // if spot is empty variable "checkForEmptySpot" increases 
                        }                                               // and we define how many empty spots we have
                    }
                }

                if (checkForEmptySpot > 0)                              // if there are more empty spots than 0
                {
                    int newRandom = Rand.Next(4);                       // new random value in the range from 0 to 3
                    Row = (int)Rand.Next(4);                            // random number for row in the range from 0 to 3
                    Column = (int)Rand.Next(4);                         // random number for column in the range from 0 to 3

                    if (NewBoard[Row, Column] != BoardEnum.empty)
                    {
                        continue;                                       // if the spot is not empty, a new iteration
                    }
                    else if (newRandom == 0)
                    {
                        NewBoard[Row, Column] = BoardEnum.number_4;     // in this case number "4" appears 25% of the game
                        success = true;
                    }
                    else
                    {
                        NewBoard[Row, Column] = BoardEnum.number_2;     // in this case number "2" appears 75% of the game
                        success = true;
                    }
                }
                else
                {
                    success = CheckPossibleMove();
                    if (!success)
                    {
                        GameIsFinished = true;                          // if there are not empty spots and numbers can't merge, 
                        success = true;                                 // then game is over
                    }
                }
            }
        }
        public void PrintBoard()                                      // a method which creates a board for the game
        {
            Console.Clear();                                            // clears numbers from previous spot (when player makes a move)
            Console.WriteLine();

            for (Row = 0; Row < NewBoard.GetLength(0); Row++)                 // check row - 0, then row - 1 etc.
            {
                Console.WriteLine();
                Console.Write("           ");                                 // move the board to the middle 

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)    // check row - 0 column - 0, then row - 0 column - 1 etc.
                {
                    switch (NewBoard[Row, Column])
                    {
                        case BoardEnum.empty:                                 // if spot's value is 0, print "|_____|"  
                            Console.ForegroundColor = ConsoleColor.Yellow;    // each Enum value is printed in different color
                            Console.Write("|_____|");
                            break;
                        case BoardEnum.number_2:
                            Console.ForegroundColor = ConsoleColor.Red;       // if spot's value is number, print "|_____|" 
                            Console.Write("|__2__|");                         // and "number" (same for all values)
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
        public bool PressArrow()                                      // a method which checks if the player pressed correct key
        {
            KeyPressed = false;

            while (!KeyPressed)
            {
                switch (Console.ReadKey(false).Key)                     // checks if the player usees the correct key 
                {                                                       // for the game (Right, Left, Up or Down)
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
                    default:                                            // if player presses other keys (not ARROW)
                        Console.WriteLine();
                        return false;
                }
            }
            return false;
        }
        public void EscapeForExit()                                   // a method which exits the game when it is finished
        {
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)                           // by pressing "ESC" a player can exit the game
            {                                                           // simplified ESCAPE for exit method 
                Environment.Exit(0);                                    // by using Built-In method
            }
        }
        private void NewMoveRight()                                   // a method for making a move to the right
        {
            for (int row = 0; row < NewBoard.GetLength(0); row++)               // checks row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = NewBoard.GetLength(1) - 1; Column >= 0; Column--) // check row - 0 column - 3, 
                {                                                               // then row - 0 column - 2 etc.
                    if (NewBoard[row, Column] != BoardEnum.empty)               // if spot is not empty
                    {
                        TempArray[IndexForTempArray] = NewBoard[row, Column];   // than move this value to the temp array
                        IndexForTempArray += 1;                                 // and change the index
                    }
                }
                CheckTempArray(TempArray);                                      // a method which provides numbers movement and changes

                IndexForTempArray = 0;

                for (Column = NewBoard.GetLength(1) - 1; Column >= 0; Column--) // to move values back from the temp array to the board
                {
                    NewBoard[row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray += 1;
                }
            }
            PrintBoard();                                                       // prints the board with the new values
            Console.WriteLine();

            KeyPressed = true;                                                  // confirms that the player is pressed the correct key
        }
        private void NewMoveLeft()                                    // a method for making a move to the right
        {
            for (int Row = 0; Row < NewBoard.GetLength(0); Row++)            // checks row - 0, then row - 1 etc.
            {
                IndexForTempArray = 0;

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)   // check row - 0 column - 0,
                {                                                            // then row - 0 column - 1 etc.
                    if (NewBoard[Row, Column] != BoardEnum.empty)            // if spot is not empty             
                    {
                        TempArray[IndexForTempArray] = NewBoard[Row, Column];// than move this value to the temp array
                        IndexForTempArray += 1;                              // and change the index
                    }
                }
                CheckTempArray(TempArray);                                   // a method which provides numbers movement and changes

                for (Column = 0; Column < NewBoard.GetLength(1); Column++)   // to move values back from the temp array to the board
                {
                    NewBoard[Row, Column] = TempArray[Column];
                    TempArray[Column] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                    // prints the board with the new values
            Console.WriteLine();
            KeyPressed = true;                                               // confirms that the player is pressed the correct key
        }
        private void NewMoveUp()                                      // a method for making a move to the up
        {
            for (int Column = 0; Column < NewBoard.GetLength(1); Column++)     // checks column - 0, then column - 1 etc.
            {
                IndexForTempArray = 0;

                for (Row = 0; Row < NewBoard.GetLength(0); Row++)              // check row - 0 column - 0, 
                {                                                              // then row - 1 column - 0 etc.
                    if (NewBoard[Row, Column] != BoardEnum.empty)              // if spot is not empty
                    {
                        TempArray[IndexForTempArray] = NewBoard[Row, Column];  // than move this value to the temp array
                        IndexForTempArray += 1;                                // and change the index
                    }
                }
                CheckTempArray(TempArray);                                     // a method which provides numbers movement and changes

                for (Row = 0; Row < NewBoard.GetLength(0); Row++)              // to move values back from the temp array to the board
                {
                    NewBoard[Row, Column] = TempArray[Row];
                    TempArray[Row] = BoardEnum.empty;
                }
            }
            PrintBoard();                                                      // prints the board with the new values
            Console.WriteLine();
            KeyPressed = true;                                                 // confirms that the player is pressed the correct key
        }
        private void NewMoveDown()                                    // a method for making a move to the up
        {
            for (int Column = 0; Column < NewBoard.GetLength(1); Column++)     // checks column - 0, then column - 1 etc.
            {
                IndexForTempArray = 0;

                for (Row = (NewBoard.GetLength(0) - 1); Row >= 0; Row--)       // check row - 3 column - 0,
                {                                                              // then row - 2 column - 0 etc.
                    if (NewBoard[Row, Column] != BoardEnum.empty)              // if spot is not empty
                    {
                        TempArray[IndexForTempArray] = NewBoard[Row, Column];  // than move this value to the temp array
                        IndexForTempArray += 1;                                // and change the index
                    }
                }
                CheckTempArray(TempArray);                                     // a method which provides numbers movement and changes

                IndexForTempArray = 0;

                for (Row = (NewBoard.GetLength(0) - 1); Row >= 0; Row--)       // to move values back from the temp array to the board
                {
                    NewBoard[Row, Column] = TempArray[IndexForTempArray];
                    TempArray[IndexForTempArray] = BoardEnum.empty;
                    IndexForTempArray++;
                }
            }
            PrintBoard();                                                      // prints the board with the new values
            Console.WriteLine();
            KeyPressed = true;                                                 // confirms that the player is pressed the correct key
        }
        private bool CheckPossibleMove()                              // a method for checking if it is possible to move and combine numbers
        {
            for (int Row = 0; Row < NewBoard.GetLength(0); Row++)           // checks row - 0, then row - 1 etc.
            {
                if (NewBoard[Row, 0] == NewBoard[Row, 1] ||                 // a comparison process
                    NewBoard[Row, 1] == NewBoard[Row, 2] ||
                    NewBoard[Row, 2] == NewBoard[Row, 3])
                {
                    return true;
                }
            }
            for (Column = 0; Column < NewBoard.GetLength(1); Column++)      // checks column - 0, then column - 1 etc.
            {
                if (NewBoard[0, Column] == NewBoard[1, Column] ||           // a comparison process
                    NewBoard[1, Column] == NewBoard[2, Column] ||
                    NewBoard[2, Column] == NewBoard[3, Column])
                {
                    return true;
                }
            }
            return false;
        }
        private BoardEnum[] CheckTempArray(BoardEnum[] tempArray)     // a method which provides numbers movement and changes
        {
            if (TempArray[0] == TempArray[1])                         // for example: "2 2 0 0" 
            {
                TempArray[0] = DoubleNumber(TempArray[0]);            // then to double index [0]  "4 . . ."

                if (TempArray[2] == TempArray[3])                     // in the case "2 2 0 0" true 0 == 0
                {
                    TempArray[1] = DoubleNumber(TempArray[2]);        // index [1]  "4 0 . ."
                    TempArray[2] = BoardEnum.empty;                   // index [2]  "4 0 0 ."
                    TempArray[3] = BoardEnum.empty;                   // index [3]  "4 0 0 0"
                }
                else                                                  // for example: "2 2 0 2"
                {
                    TempArray[1] = TempArray[2];                      // index [1]  "4 0 . ."
                    TempArray[2] = TempArray[3];                      // index [2]  "4 0 2 ."
                    TempArray[3] = BoardEnum.empty;                   // index [3]  "4 0 2 0"
                }
                return tempArray;
            }
            else if (TempArray[1] == TempArray[2])                    // for example: "2 4 4 2"
            {                                                         // index [0] remains the same
                TempArray[1] = DoubleNumber(TempArray[1]);            // then to double index [1]    "2 8 . ."
                TempArray[2] = TempArray[3];                          // index [2]  "2 8 2 ."
                TempArray[3] = BoardEnum.empty;                       // index [3]  "2 8 2 0"
                return tempArray;
            }
            else if (TempArray[2] == TempArray[3])                    // for example: "4 2 4 4"
            {                                                         // index [0] and index [1] remain the same
                TempArray[2] = DoubleNumber(TempArray[2]);            // then to double index [2]   "4 2 8 ."
                TempArray[3] = BoardEnum.empty;                       // index [3]  "4 2 8 0"
                return tempArray;
            }
            return tempArray;
        }
        private BoardEnum DoubleNumber(BoardEnum boardEnum)           // a method which sums up both identic numbers and counts player's score
        {
            switch (boardEnum)
            {
                case BoardEnum.number_2:                              // if there are two numbers of the same value side by side "2 , 2",
                    Score += 4;                                       // then return their sum "4".  
                    return BoardEnum.number_4;                        // Four (4) points are added to the total score (same for all numbers).
                case BoardEnum.number_4:
                    Score += 8;
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
                    IsWon = true;                                    // if a player got the number 2048, the game is won and is over
                    GameIsFinished = true;
                    return BoardEnum.number_2048;
                default:
                    return BoardEnum.empty;
            }
        }
    }
}
