using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers.BoardServices
{
    public class Board
    {
        public readonly char WhiteField;
        public readonly char BlackField;
        public int WidthX { get; private set; }
        public int WidthY { get; private set; }
        public char[,] boardArray { get; set; }

        public Board()
        {
            WhiteField = '\u2588';
            BlackField = '\u2593';
            WidthX = 8;
            WidthY = 8;
            boardArray = new char[WidthX, WidthY];
        }
    }
}
