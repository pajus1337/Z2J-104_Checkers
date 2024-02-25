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
        private int counterOfAttackerFailMove = 0;

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
            isMovementAccomplished = IsPawnSetActionCompleted();

            if (!isMovementAccomplished)
            {
                throw new Exception("To develop ");
            }
        }

        private (int newPostionX, int newPostionY) TrySetNewPositionForTwoFieldMove(CpuPawn cpuPawn)
        {
            int newPositionY = -1;
            int newPositionX = -1;

            if (cpuPawn.PositionY + 2 < Board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 2;
                if (cpuPawn.PositionX - 2 >= 0 && cpuPawn.CountFailAttack < 1)
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
            Random random = new Random();
            int newPositionY = 0;
            int newPositionX = 0;
            var randomChoice = random.NextDouble();


            if (cpuPawn.PositionY + 1 < Board.WidthY)
            {
                if (cpuPawn.CountFailMove > 3 && cpuPawn.PositionX + 1 < Board.WidthX)
                {
                    newPositionX = cpuPawn.PositionX + 1;
                }
                else if (cpuPawn.CountFailMove > 2 && cpuPawn.PositionX - 1 > 0)
                {
                    newPositionX = cpuPawn.PositionX - 1;
                }
                else if (cpuPawn.PositionX - 1 > 0 && cpuPawn.PositionX + 1 < Board.WidthX)
                {
                    newPositionX = cpuPawn.PositionX + (randomChoice > 0.5 ? 1 : -1);
                }
                newPositionY = cpuPawn.PositionY + 1;
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

        private List<CpuPawn> GetPawnWithValidOneFieldMove()
        {
            var allPawns = _pawnController.Value.PawnsInGame;
            var positionMap = new HashSet<(int, int)>();
            foreach (var pawn in allPawns)
            {
                positionMap.Add((pawn.PositionX, pawn.PositionY));
            }

            var cpuPawnsWithValidMove = allPawns.OfType<CpuPawn>()
                .Where(cp =>
                ((cp.PositionX < 7 && cp.PositionY < 7 && !positionMap.Contains((cp.PositionX + 1, cp.PositionY + 1))) ||
                (cp.PositionY < 7 && cp.PositionX > 0 && !positionMap.Contains((cp.PositionX - 1, cp.PositionY + 1))))
                )
                .ToList();

            return cpuPawnsWithValidMove;
        }

        private bool IsPawnSetActionCompleted()
        {
            bool IsActionForPawnDone = TryActionForPawns();
            if (!IsActionForPawnDone)
            {
                TryToMoveWithoutAction();
            }
            return false;
        }

        private void ResetPawnFails()
        {
            foreach (var pawn in pawnsWithAction)
            {
                pawn.CountFailAttack = 0;
                pawn.CountFailMove = 0;
            }
        }

        private bool TryActionForPawns()
        {
            ResetPawnFails();
            int maxFailAttack = 2;

            foreach (var pawn in pawnsWithAction)
            {
                while (pawn.CountFailAttack < maxFailAttack)
                {                   
                    (int newPositionX, int newPositionY) = TrySetNewPositionForTwoFieldMove(pawn);

                    if (MovementAnalyzer.IsValidField(Board, newPositionX, newPositionY) &&
                        _movementAnalyzer.IsAllowedMovement(Board, _pawnController.Value.PawnsInGame, pawn, newPositionY, newPositionX))
                    {
                        pawn.CountFailAttack = 0;
                        _pawnController.Value.MoveCpuPawn(pawn, newPositionX, newPositionY);
                        return true;
                    }
                    pawn.CountFailAttack++;
                }
                pawn.CountFailAttack = 0;
            }
            return false;
        }

        private void TryToMoveWithoutAction()
        {
            var pawnsWithValidMove = GetPawnWithValidOneFieldMove();

            foreach (var cpuPawn in pawnsWithValidMove)
            {
                while (cpuPawn.CountFailMove < 5)
                {
                    (newPositionX, newPositionY) = TrySetNewPositionForOneFieldMove(cpuPawn);
                    if (_movementAnalyzer.IsAllowedMovement(Board, _pawnController.Value.PawnsInGame, cpuPawn, newPositionY, newPositionX))
                    {
                        cpuPawn.CountFailMove = 0;
                        _pawnController.Value.MoveCpuPawn(cpuPawn, newPositionX, newPositionY);
                        return;
                    }
                    cpuPawn.CountFailMove++;
                }
                cpuPawn.CountFailMove = 0;
            }
        }
    }
}