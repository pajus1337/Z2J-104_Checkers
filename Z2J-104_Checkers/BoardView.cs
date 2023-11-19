using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardView
    {
        private Board _board;

        CpuPawn cpuPawn = new CpuPawn();
        PlayerPawn playerPawn = new PlayerPawn();

        public BoardView(Board board)
        {
            _board = board;
        }


        public void test()
        {

        }

    }
}
