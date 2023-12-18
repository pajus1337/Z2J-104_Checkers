using System;
using System.Collections.Generic;
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
            var pawnToAction = SearchForFrontPawn(board);
            CheckPossibilityOfMove(pawnToAction);
            if (!movementAnalyzer.IsAllowedMovement(board, pawnController.PawnsInGame, pawnToAction, newPositionY, newPositionX))
            {
                TryOneFieldMove();
            }

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
            for (int y = board.WidthY-1; y > 0; y--)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y,x] == CpuPawn.CPU_PAWN_SYMBOL)
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

        private (int newPostionX,int newPostionY) TrySetNewPositionForPawn(CpuPawn cpuPawn)
        {
            int newPositionY = -1;
            int newPositionX = -1;

            if (cpuPawn.PositionY + 2 < board.WidthY)
            {
                newPositionY = cpuPawn.PositionY + 2;
                if (cpuPawn.PositionX - 2 < 0)
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

        private void CheckPossibilityOfMove(CpuPawn cpuPawn)
        {
            if (cpuPawn != null && !cpuPawn.IsSuperPawn)
            {
                (newPositionX, newPositionY) = TrySetNewPositionForPawn(cpuPawn);
            }
        }

        private void SearchForEnemyPawn(CpuPawn cpuPawn, Board board)
        {

        }
    }
}
