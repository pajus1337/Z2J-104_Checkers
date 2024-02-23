using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;

namespace Z2J_104_Checkers.Interfaces
{
    public interface IBoardView
    {
        void DisplayCurrentBoard(Board board);
    }
}
