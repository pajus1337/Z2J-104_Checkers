﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class Pawn 
    {
        private static int lastPawnId = 0;
        public int PawnId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char PawnSymbol { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsSuperPawn { get; private set; } = false;

        public Pawn(int positionX, int positionY, char pawnSymbol, bool is_Player)
        {
            PositionX = positionX;
            PositionY = positionY;
            PawnSymbol = pawnSymbol;
            IsPlayer = is_Player;
            PawnId = ++lastPawnId;
        }

        public override string ToString()
        {
            return $"Pawn ID : {this.PawnId} on Position X : {PositionX} , Y : {PositionY}";
        }
    }
}
