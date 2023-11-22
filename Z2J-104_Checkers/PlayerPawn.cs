using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class PlayerPawn : Pawn
    {
        public PlayerPawn(int postionX, int positionY) : base(postionX, positionY, 'P', true)
        {
        }
    }
}
 