using static Z2J_104_Checkers.GameManager;
using Z2J_104_Checkers.BoardServices;

namespace Z2J_104_Checkers.Interfaces
{
    public interface IGameStateController
    {
        bool IsGameOver { get; }
        Board GameBoard { get; }
        List<Pawn> PawnsInGame { get; set; }

        event Action PlayerTurnStarted;
        event Action CPUTurnStarted;
        event Action<Board> BoardUpdate;

        void Initialize();
        void TurnEnds();
    }
}