using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class CPUChoiceAnalyzer
    {
        MovementAnalyzer movementAnalyzer;
        Board board;
        PawnController pawnController;

        private List<CpuPawn> pawnsWithAction;
        private int newPositionX;
        private int newPositionY;

        public CPUChoiceAnalyzer(Board board, MovementAnalyzer movementAnalyzer, PawnController pawnController)
        {
            this.pawnController = pawnController;
            this.movementAnalyzer = movementAnalyzer;
            this.board = board;
        }

        public void TestOfMovementLogik()
        {
            pawnsWithAction = FindPawnsWithNearbyOpponentPawns(pawnController);
            FindBestPawnForActionAndSetAction();


        }

        /// <summary>
        /// Searches for a pawn at the front.
        /// </summary>
        /// <param name="board">The game board on which the search is to be conducted.</param>
        /// <returns>
        /// A <see cref="CpuPawn"/> object representing the front cpu pawn.
        /// May return null if a suitable pawn is not found !
        /// </returns>
        public CpuPawn SearchForFrontPawn(Board board)
        {
            for (int y = board.WidthY - 1; y > 0; y--)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == CpuPawn.CPU_PAWN_SYMBOL)
                    {
                        var pawn = pawnController.PawnsInGame.OfType<CpuPawn>().FirstOrDefault(p => p.PositionX == x && p.PositionY == y);
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

            if (cpuPawn.PositionY + 2 < board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 2;
                if (cpuPawn.PositionX - 2 >= 0)
                {
                    newPositionX = cpuPawn.PositionX - 2;
                }
                else if (cpuPawn.PositionX + 2 < board.WidthX)
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

            if (cpuPawn.PositionY + 1 < board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 1;
                if (cpuPawn.PositionX - 1 > 0)
                {
                    newPositionX = cpuPawn.PositionX - 1;
                }
                else if (cpuPawn.PositionX + 1 < board.WidthX)
                {
                    newPositionX = cpuPawn.PositionX + 1;
                }
            }
            return (newPositionX, newPositionY);
        }

        private void CheckPossibilityOfMove(CpuPawn cpuPawn)
        {
            if (cpuPawn != null && !cpuPawn.IsSuperPawn)
            {
                (newPositionX, newPositionY) = TrySetNewPositionForTwoFieldMove(cpuPawn);
            }
        }

        private List<CpuPawn> FindPawnsWithNearbyOpponentPawns(PawnController pawnController)
        {
            var cpuPawnsWithAction = pawnController.PawnsInGame
                .OfType<CpuPawn>()
                .Where(cpuPawn => pawnController.PawnsInGame
                    .OfType<PlayerPawn>()
                    .Any(playerPawn => !ReferenceEquals(cpuPawn, playerPawn) &&
                                       Math.Abs(cpuPawn.PositionX - playerPawn.PositionX) == 1 &&
                                       Math.Abs(cpuPawn.PositionY - playerPawn.PositionY) == 1))
                .ToList();

            return cpuPawnsWithAction;
        }

        private void FindBestPawnForActionAndSetAction()
        {
            if (pawnsWithAction.Count == 0)
            {
                TryToMoveWithoutAction();
                return;
            }

            foreach (var pawn in pawnsWithAction)
            {
                (int newPositionX, int newPositionY) = TrySetNewPositionForTwoFieldMove(pawn);
                if (movementAnalyzer.IsAllowedMovement(board, pawnController.PawnsInGame, pawn, newPositionY, newPositionX))
                {
                    pawnController.MoveCpuPawn(pawn, newPositionX, newPositionY);
                    Debug.Print("YES");
                    return;
                }
            }
        }

        public void TryToMoveWithoutAction()
        {
            CpuPawn cpuPawn = SearchForFrontPawn(board);
            (newPositionX, newPositionY) = TrySetNewPositionForOneFieldMove(cpuPawn);
            if (movementAnalyzer.IsAllowedMovement(board, pawnController.PawnsInGame, cpuPawn, newPositionY, newPositionX))
            {
                pawnController.MoveCpuPawn(cpuPawn, newPositionX, newPositionY);
                return;
            }
        }

        private bool isAttackPossible(CpuPawn cpuPawn)
        {
            TrySetNewPositionForTwoFieldMove(cpuPawn);
            return true;
        }
    }
}
