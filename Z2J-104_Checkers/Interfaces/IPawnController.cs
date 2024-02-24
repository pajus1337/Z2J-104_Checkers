using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;

namespace Z2J_104_Checkers.Interfaces
{
    public interface IPawnController
    {
        List<Pawn> PawnsInGame { get; }
        Board PlacePawnsForNewGame(Board board);
        Pawn CreatePawn(int positionX, int positionY, bool isPlayerPawn);
        Pawn SelectPawn();
        (int, int) SelectNewPawnPosition();
        void MovePlayerPawn(Board board);
        void MoveCpuPawn(CpuPawn cpuPawnInAction, int newPositionX, int newPositionY);
        void RemovePawn(Pawn pawn);
    }
}
