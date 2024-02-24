using System.Diagnostics;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class CPUChoiceAnalyzer : ICPUChoiceAnalyzer
    {

        private readonly IMovementAnalyzer _movementAnalyzer;
        public Board Board { get; private set; }
       // private readonly IPawnControllerFactory _pawnControllerFactory;
        private readonly Lazy<IPawnController> _pawnController;
        private List<CpuPawn> pawnsWithAction;
        private int newPositionX;
        private int newPositionY;
        private int counterOfFailMove = 0;

        public CPUChoiceAnalyzer(Board board, IMovementAnalyzer movementAnalyzer, Lazy<IPawnController> pawnControllerFactory)
        {
            _pawnController = pawnControllerFactory;
            _movementAnalyzer = movementAnalyzer;
            Board = board;
        }

        public void PickAndMoveCPUPawn()
        {
            bool isMovementAccomplished = false;
            pawnsWithAction = FindPawnsWithNearbyOpponentPawns();

            do
            {
                isMovementAccomplished = IsPawnSetActionCompleted();
            } while (!isMovementAccomplished || (isMovementAccomplished && _movementAnalyzer.IsEnemyPawnCapturedOnLastMove));
        }

        /// <summary>
        /// Searches for a pawn at the front.
        /// </summary>
        /// <param name="board">The game board on which the search is to be conducted.</param>
        /// <returns>
        /// A <see cref="CpuPawn"/> object representing the front cpu pawn.
        /// May return null if a suitable pawn is not found !
        /// </returns>
        private CpuPawn SearchForFrontPawn(Board board)
        {
            for (int y = board.WidthY - 1; y > 0; y--)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == CpuPawn.CPU_PAWN_SYMBOL)
                    {
                        var pawn = _pawnController.Value.PawnsInGame.OfType<CpuPawn>().FirstOrDefault(p => p.PositionX == x && p.PositionY == y);
                        if (pawn != null)
                        {
                            return pawn;
                        }
                    }
                }
            }
            return null;
        }

        private (int newPostionX, int newPostionY) TrySetNewPositionForTwoFieldMove(CpuPawn cpuPawn)
        {
            int newPositionY = -1;
            int newPositionX = -1;

            if (cpuPawn.PositionY + 2 < Board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 2;
                if (cpuPawn.PositionX - 2 >= 0)
                {
                    newPositionX = cpuPawn.PositionX - 2;
                }
                else if (cpuPawn.PositionX + 2 < Board.WidthX)
                {
                    newPositionX = cpuPawn.PositionX + 2;
                }
            }
            return (newPositionX, newPositionY);
        }

        private (int newPostionX, int newPostionY) TrySetNewPositionForOneFieldMove(CpuPawn cpuPawn)
        {
            int newPositionY = -1;
            int newPositionX = -1;

            if (cpuPawn.PositionY + 1 < Board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 1;
                if (cpuPawn.PositionX - 1 > 0)
                {
                    newPositionX = cpuPawn.PositionX - 1;
                }
                else if (cpuPawn.PositionX + 1 < Board.WidthX)
                {
                    newPositionX = cpuPawn.PositionX + 1;
                }
            }
            return (newPositionX, newPositionY);
        }

        private List<CpuPawn> FindPawnsWithNearbyOpponentPawns()
        {
            var cpuPawnsWithAction = _pawnController.Value.PawnsInGame
                .OfType<CpuPawn>()
                .Where(cpuPawn => _pawnController.Value.PawnsInGame
                    .OfType<PlayerPawn>()
                    .Any(playerPawn => !ReferenceEquals(cpuPawn, playerPawn) &&
                    Math.Abs(cpuPawn.PositionX - playerPawn.PositionX) == 1 &&
                    Math.Abs(cpuPawn.PositionY - playerPawn.PositionY) == 1))
                .ToList();

            return cpuPawnsWithAction;
        }

        private bool IsPawnSetActionCompleted()
        {

            if (pawnsWithAction.Count == 0 || counterOfFailMove == pawnsWithAction.Count * 2)
            {
                TryToMoveWithoutAction();
                counterOfFailMove = 0;
                return true;
            }

            foreach (var pawn in pawnsWithAction)
            {
                (int newPositionX, int newPositionY) = TrySetNewPositionForTwoFieldMove(pawn);

                if (MovementAnalyzer.IsValidField(Board, newPositionX, newPositionY))
                    {
                    if (_movementAnalyzer.IsAllowedMovement(Board, _pawnController.Value.PawnsInGame, pawn, newPositionY, newPositionX))
                    {
                        _pawnController.Value.MoveCpuPawn(pawn, newPositionX, newPositionY);
                        Debug.Print("YES");
                        counterOfFailMove = 0;
                        return true;
                    }                   
                }
                counterOfFailMove++;
            }

            return false;
        }

        private void TryToMoveWithoutAction()
        {
            CpuPawn cpuPawn = SearchForFrontPawn(Board);
            (newPositionX, newPositionY) = TrySetNewPositionForOneFieldMove(cpuPawn);
            if (_movementAnalyzer.IsAllowedMovement(Board, _pawnController.Value.PawnsInGame, cpuPawn, newPositionY, newPositionX))
            {
                _pawnController.Value.MoveCpuPawn(cpuPawn, newPositionX, newPositionY);
                return;
            }
        }
    }
}