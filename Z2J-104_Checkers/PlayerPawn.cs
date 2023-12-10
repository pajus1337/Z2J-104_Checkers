using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class PlayerPawn : Pawn
    {
        public const char PLAYER_PAWN_SYMBOL = 'X';
        public PlayerPawn(int positionX, int positionY) : base(positionX, positionY,'X', true)
        {

        }
    }
}
 