using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public interface IGameManager
    {
        public Board GetBoard();
        public MovementAnalyzer GetMovementAnalyzer();
        public void TurnEnds();
    }
}
