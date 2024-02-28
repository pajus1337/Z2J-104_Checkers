using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public abstract class Pawn 
    {
        private static int lastPawnId = 0;
        public int PawnId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int CountFailMove { get; set; }
        public int CountFailAttack { get; set; }
        public char PawnSymbol { get; set; }
        public bool IsPlayer { get; set; }

        public Pawn(int positionX, int positionY, char pawnSymbol, bool is_Player)
        {
            PositionX = positionX;
            PositionY = positionY;
            PawnSymbol = pawnSymbol;
            IsPlayer = is_Player;
            PawnId = ++lastPawnId;
        }

        public abstract bool HasReachedEndBoard();

        public override string ToString()
        {
            return $"Pawn ID : {this.PawnId} on Position X : {PositionX} , Y : {PositionY}";
        }
    }
}
