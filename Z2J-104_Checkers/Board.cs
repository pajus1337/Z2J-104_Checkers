using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class Board
    {
        public readonly char WhiteField;
        public readonly char BlacKField;
        public int WidthX { get; private set; }
        public int WidthY { get; private set; }
        public char[,] boardArray { get; set; }

        

    public Board()
        {
            WhiteField = 'O';
            BlacKField = 'X';
            WidthX = 8;
            WidthY = 8;
            boardArray = new char[WidthX, WidthY];
        }
    }
}
