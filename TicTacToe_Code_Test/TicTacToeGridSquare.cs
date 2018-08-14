using System;

namespace TicTacToe_Code_Test
{
    public class TicTacToeGridSquare
    {
        // effectively a wrapper for a char to do some value restrictions
        private char _mCurrentMark;
        public char CurrentMark
        {
            get
            {
                return _mCurrentMark;
            }
            set
            {
                if ('X' != value && 'O' != value && ' ' != value)
                {
                    throw new ArgumentException("Improper value given to TicTactToeGridSquare: " + value.ToString());
                }
                _mCurrentMark = value;
            }
        }
        public TicTacToeGridSquare() => _mCurrentMark = ' ';
    }
}