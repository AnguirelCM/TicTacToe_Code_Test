using System;

namespace TicTacToe_Code_Test
{
    public class TicTacToeBoard
    {
        public enum Player
        {
            Player_X,
            Player_O
        }
        private readonly TicTacToeGridSquare[,] _mBoardSquares;
        private readonly int _mBoardWidth;
        private readonly int _mBoardLength;
        private readonly int _mNumToWin;
        public TicTacToeBoard(int m, int n, int k)
        {
            // ensure proper values given for input
            if (m < 1 || n < 1 || k < 1 || (k > m && k > n))
            {
                throw new ArgumentException("Invalid size or victory conditions passed to TicTacToeBoard. Received: " + m.ToString() + "," + n.ToString() + "," + k.ToString());
            }
            _mBoardSquares = new TicTacToeGridSquare[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    _mBoardSquares[i, j] = new TicTacToeGridSquare();
                }
            }
            _mBoardWidth = m;
            _mBoardLength = n;
            _mNumToWin = k;
        }
        public void PrintBoard()
        {
            // future improvement: add numbers for columns and rows to make it easier to select coordinates
            // future improvement: consider some method of highlighting every third grid line to make it easier to follow lines out 
            for (int current_X = 0; current_X < _mBoardWidth; ++current_X)
            {
                Console.Write("--");
            }
            Console.WriteLine("-");
            for (int current_Y = 0; current_Y < _mBoardLength; ++current_Y)
            {
                for (int current_X = 0; current_X < _mBoardWidth; ++current_X)
                {
                    Console.Write('|' + _mBoardSquares[current_X, current_Y].CurrentMark.ToString());
                }
                Console.WriteLine('|');
                for (int current_X = 0; current_X < _mBoardWidth; ++current_X)
                {
                    Console.Write("--");
                }
                Console.WriteLine("-");
            }
        }
        public void ResetSquare(int X_location, int Y_location)
        {
            // ensure proper values given for input
            if (0 > X_location || _mBoardWidth <= X_location)
            {
                throw new ArgumentException("X value passed to TicTacToeBoard.ResetSquare is outside board.");
            }
            if (0 > Y_location || _mBoardLength <= Y_location)
            {
                throw new ArgumentException("Y value passed to TicTacToeBoard.ResetSquare is outside board.");
            }

            _mBoardSquares[X_location, Y_location].CurrentMark = ' ';
        }
        public bool PlayMove(Player player, int X_location, int Y_location)
        {
            // ensure proper values given for input
            if (0 > X_location || _mBoardWidth <= X_location)
            {
                throw new ArgumentException("X value passed to TicTacToeBoard.PlayMove is outside board.");
            }
            if (0 > Y_location || _mBoardLength <= Y_location)
            {
                throw new ArgumentException("Y value passed to TicTacToeBoard.PlayMove is outside board.");
            }

            char Mark_Type = ' ';
            if (player == Player.Player_X)
            {
                Mark_Type = 'X';
            }
            if (player == Player.Player_O)
            {
                Mark_Type = 'O';
            }

            // 
            if (' ' == _mBoardSquares[X_location, Y_location].CurrentMark)
            {
                _mBoardSquares[X_location, Y_location].CurrentMark = Mark_Type;
                return true;
            }
            return false;
        }
        public bool CheckWinFrom(Player player, int X_location, int Y_location)
        {
            // ensure proper values given for input
            if (0 > X_location || _mBoardWidth <= X_location)
            {
                throw new ArgumentException("X value passed to TicTacToeBoard.CheckWinFrom is outside board.");
            }
            if (0 > Y_location || _mBoardLength <= Y_location)
            {
                throw new ArgumentException("Y value passed to TicTacToeBoard.CheckWinFrom is outside board.");
            }

            // Direction Around Current
            // 0 | 1 | 2
            // 3 | C | 4
            // 5 | 6 | 7
            int[] directionLengths = { 0, 0, 0, 0, 0, 0, 0, 0 };

            // Possible future improvement: could run checks in parallel
            for (int i = 0; i < 8; ++i)
            {
                directionLengths[i] = GetLongestLine(player, X_location, Y_location, i);
            }

            // GetLongestLine is a vector from a point -- but the actual longest line combines both directions, and the result will be 1 larger than the actual line ends up being
            if (directionLengths[0] + directionLengths[7] > _mNumToWin ||
                directionLengths[1] + directionLengths[6] > _mNumToWin ||
                directionLengths[2] + directionLengths[5] > _mNumToWin ||
                directionLengths[3] + directionLengths[4] > _mNumToWin)
            {
                return true;
            }
            return false;
        }
        public int GetLongestLine(Player player, int X_location, int Y_location, int Direction)
        {
            // ensure proper values given for input
            if (0 > X_location || _mBoardWidth <= X_location)
            {
                throw new ArgumentException("X value passed to TicTacToeBoard.GetLongestLine is outside board.");
            }
            if (0 > Y_location || _mBoardLength <= Y_location)
            {
                throw new ArgumentException("Y value passed to TicTacToeBoard.GetLongestLine is outside board.");
            }

            char Mark_Type = ' ';
            if (player == Player.Player_X)
            {
                Mark_Type = 'X';
            }
            if (player == Player.Player_O)
            {
                Mark_Type = 'O';
            }
            int currentLine = 0;
            int delta_X;
            int delta_Y;

            // Direction Around Current
            // 0 | 1 | 2
            // 3 | C | 4
            // 5 | 6 | 7
            switch (Direction)
            {
                case 0:
                    delta_X = -1;
                    delta_Y = -1;
                    break;
                case 1:
                    delta_X = 0;
                    delta_Y = -1;
                    break;
                case 2:
                    delta_X = 1;
                    delta_Y = -1;
                    break;
                case 3:
                    delta_X = -1;
                    delta_Y = 0;
                    break;
                case 4:
                    delta_X = 1;
                    delta_Y = 0;
                    break;
                case 5:
                    delta_X = -1;
                    delta_Y = 1;
                    break;
                case 6:
                    delta_X = 0;
                    delta_Y = 1;
                    break;
                case 7:
                    delta_X = 1;
                    delta_Y = 1;
                    break;
                default:
                    throw new ArgumentException("Direction value passed to TicTacToeBoard.GetLongestLine was invalid. Received: " + Direction.ToString());
            }

            // redesigned from initial plan using tail recursion, now in an iterative non-stack-destroying form
            while (true)
            {
                if (X_location >= 0 && X_location < _mBoardWidth && Y_location >= 0 && Y_location < _mBoardLength && _mBoardSquares[X_location, Y_location].CurrentMark == Mark_Type)
                {
                    X_location += delta_X;
                    Y_location += delta_Y;
                    ++currentLine;
                }
                else
                {
                    return currentLine;
                }
            }
        }
    }
}