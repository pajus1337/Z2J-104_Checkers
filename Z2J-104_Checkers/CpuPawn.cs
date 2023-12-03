using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class CpuPawn : Pawn
    {
        public const char CPU_PAWN_SYMBOL = 'O';
        public CpuPawn(int postionX, int positionY) : base(postionX, positionY, 'O', false)
        {
        }
    }
}
