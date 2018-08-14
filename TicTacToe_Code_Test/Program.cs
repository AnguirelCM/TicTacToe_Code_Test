using System;
using System.Collections.Generic;

namespace TicTacToe_Code_Test
{
    class Program
    {
        // For storing previous Moves made, so the undo feature can function
        class Move
        {
            public readonly int X_Location;
            public readonly int Y_Location;
            public Move(int X, int Y)
            {
                X_Location = X;
                Y_Location = Y;
            }
        }

        // future improvement: move to using a string table for these lines, eliminate this function.
        static private void PrintUsage()
        {
            Console.WriteLine("TicTacToe -- usage: 3x3 game default with no arguments");
            Console.WriteLine("Required Arguments for a non-default game: <Width> <Length> <Win Condition>");
        }

        static void Main(string[] args)
        {
            int boardWidth = 3;
            int boardLength = 3;
            int numToWin = 3;

            if(0 != args.Length && 3 != args.Length)
            {
                // Incorrect number of arguments, print help text
                PrintUsage();
                Console.ReadKey();
                return;
            }
            if (3 == args.Length)
            {
                // Perform some argument value sanitization and sanity checks
                try
                {
                    boardWidth = int.Parse(args[0]);
                    boardLength = int.Parse(args[1]);
                    numToWin = int.Parse(args[2]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to parse arguments.");
                    PrintUsage();
                    Console.ReadKey();
                    return;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Argument value too large.");
                    PrintUsage();
                    Console.ReadKey();
                    return;
                }
                // ensure all arguments are positive value over 1
                if (boardWidth <= 0 || boardLength <= 0 || numToWin <= 0)
                {
                    Console.WriteLine("Use positive integer values for Width, Length, and Number of Marks in a Row to Win.");
                    PrintUsage();
                    Console.ReadKey();
                    return;
                }
                // is win value possible to reach?
                if (numToWin > boardWidth && numToWin > boardLength)
                {
                    Console.WriteLine("Impossible to Win -- requires marks more in a row than can be obtained.");
                    PrintUsage();
                    Console.ReadKey();
                    return;
                }
                // also check max value -- this is more flexible, but a cap to keep the board pretty isn't a bad idea
                if (boardWidth > 35 || boardLength > 20)
                {
                    Console.WriteLine("Large Boards may be difficult to read on the console.");
                    Console.ReadKey();
                }
            }
            TicTacToeBoard theGameBoard = new TicTacToeBoard(boardWidth, boardLength, numToWin);
            theGameBoard.PrintBoard();
            TicTacToeBoard.Player currentPlayer = TicTacToeBoard.Player.Player_X;
            string inputMove = "";
            int X_location = 0, Y_location = 0;
            int maxMoveCount = boardWidth * boardLength;
            Stack<Move> Moves = new Stack<Move>();
            bool winnerExists = false;
            // future improvement: strings table would be better, but having a single place to update the line is still useful
            const string validInputsMessage = "Please enter '(q)uit', '(u)ndo', or '<x>,<y>'";

            Console.WriteLine(validInputsMessage);

            // Main Loop
            while (true)
            {
                Console.Write("Move for " + currentPlayer.ToString() + ": ");
                inputMove = Console.ReadLine();
                // input validation and parsing
                if(inputMove.Equals("quit", StringComparison.OrdinalIgnoreCase) || inputMove.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
                if(inputMove.Equals("undo", StringComparison.OrdinalIgnoreCase) || inputMove.Equals("u", StringComparison.OrdinalIgnoreCase))
                {
                    if(Moves.Count <= 0)
                    {
                        Console.WriteLine("No previous moves.");
                        continue;
                    }
                    //pop Move from Moves stack, swap back to previous player
                    Move lastMove = Moves.Pop();
                    theGameBoard.ResetSquare(lastMove.X_Location, lastMove.Y_Location);
                    if (TicTacToeBoard.Player.Player_X == currentPlayer)
                    {
                        currentPlayer = TicTacToeBoard.Player.Player_O;
                    }
                    else
                    {
                        currentPlayer = TicTacToeBoard.Player.Player_X;
                    }
                    winnerExists = false;
                    theGameBoard.PrintBoard();
                    continue;
                }
                if(winnerExists)
                {
                    Console.WriteLine("A player has won. Only (q)uit and (u)ndo options available.");
                    continue;
                }
                string[] splitMove = inputMove.Split(',');
                if(2 != splitMove.Length)
                {
                    Console.WriteLine("Invalid Entry. " + validInputsMessage);
                    continue;
                }
                if(!int.TryParse(splitMove[0], out X_location) || !int.TryParse(splitMove[1], out Y_location))
                {
                    Console.WriteLine("Invalid Entry. " + validInputsMessage);
                    continue;
                }
                --X_location;
                --Y_location;
                if(X_location < 0 || Y_location < 0 || X_location >= boardWidth || Y_location >= boardLength)
                {
                    Console.WriteLine("Invalid Move, outside board space. " + validInputsMessage);
                    continue;
                }
                // if we have a valid-appearing move entered, attempt to play it...
                if (theGameBoard.PlayMove(currentPlayer, X_location, Y_location))
                {
                    // if it succeeds, push it on the stack of Moves, and reprint the board
                    Moves.Push(new Move(X_location, Y_location));
                    theGameBoard.PrintBoard();
                    // check to see if this new move caused a win
                    // no need to check the whole board, only the most recent move should be able to generate a win condition
                    if (theGameBoard.CheckWinFrom(currentPlayer, X_location, Y_location))
                    {
                        Console.WriteLine(currentPlayer.ToString() + " wins!");
                        winnerExists = true;
                    }
                    // Possible future improvement - early check for Draw if no possible way to win remains
                    // this would be more processor intensive, as many posibilities may need to be checked, especially in large board games
                    // Basic first pass would essentially use GetLongestLine but allow both blank and same mark spaces to count
                    // Flagging a space + direction pair (horizontal/vertical/diag1/diag2) as having been checked would reduce redundant checks
                    if (Moves.Count >= maxMoveCount)
                    {
                        Console.WriteLine("Draw!");
                    }
                    if (TicTacToeBoard.Player.Player_X == currentPlayer)
                    {
                        currentPlayer = TicTacToeBoard.Player.Player_O;
                    }
                    else
                    {
                        currentPlayer = TicTacToeBoard.Player.Player_X;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Move, space already occupied. " + validInputsMessage);
                }
            }
        }
    }
}
