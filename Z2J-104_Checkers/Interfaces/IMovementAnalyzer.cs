using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;

namespace Z2J_104_Checkers.Interfaces
{
    public interface IMovementAnalyzer
    {
        bool IsEnemyPawnCapturedOnLastMove { get; }
        bool IsAllowedMovement(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionY, int newPositionX);
    }
}
