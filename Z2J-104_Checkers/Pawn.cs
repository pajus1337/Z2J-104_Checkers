using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class Pawn
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char PawnSymbol { get; set; }

        public Pawn(int postionX, int positionY, char pawnSymbol)
        {
            PositionX = postionX;
            PositionY = positionY;
            PawnSymbol = pawnSymbol;
        }
    }
}
