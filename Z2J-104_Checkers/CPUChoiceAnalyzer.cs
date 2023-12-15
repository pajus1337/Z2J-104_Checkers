using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class CPUChoiceAnalyzer
    {
        MovementAnalyzer movementAnalyzer;
        Board board;
        PawnController pawnController;

        public CPUChoiceAnalyzer(Board board, MovementAnalyzer movementAnalyzer, PawnController pawnController)
        {
            this.pawnController = pawnController;
            this.movementAnalyzer = movementAnalyzer;
            this.board = board;
        }

        public  void ChoseBestPawnToMove(Board board)
        {

        }
        public void SearchForFrontPawn(Board board)
        {
            for (int y = board.WidthY-1; y > 0; y--)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y,x] == CpuPawn.CPU_PAWN_SYMBOL)
                    {
                        var pawn = pawnController.PawnsInGame.FirstOrDefault(p => p.PositionX == x && p.PositionY == y);
                        continue;
                    }

                }

            }
            Console.WriteLine("test");
        }

        private void CheckPossibilityOfAttack()
        {

        }

    }
}
