using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class CpuPawn : Pawn
    {
        public CpuPawn(int postionX, int positionY, char pawnSymbol) : base(postionX, positionY, pawnSymbol)
        {
        }
        public CpuPawn(int postionX, int positionY) : base(postionX, positionY, 'C')
        {

        }

    }
}
