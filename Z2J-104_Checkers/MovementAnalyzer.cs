using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Z2J_104_Checkers
{
    public class MovementAnalyzer
    {
        public bool IsAllowedMovement(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionY, int newPositionX)
        {
            bool isNotTooShort = IsDistanceNotTooShort(board, pawn, newPositionY, newPositionX);
            bool isBlackField = IsABlackColorField(board, newPositionX, newPositionY);
            bool isNotTooFar = IsDistanceNotTooFar(board, pawn, newPositionY, newPositionX);
            bool isTwoFieldMove = IsTwoFieldMove(board, pawn, newPositionY, newPositionX);
            bool isCaptureOfPawnPossible = IsCaptureOfOpponentsPawnPossible(board, listOfPawns, pawn, newPositionX, newPositionY);

            if (isNotTooShort && isNotTooFar && isBlackField)
            {
                if (isTwoFieldMove && !isCaptureOfPawnPossible)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsABlackColorField(Board board, int positionX, int positionY)
        {
            if (board.boardArray[positionY, positionX] == board.BlackField)
            {
                return true;
            }
            return false;
        }

        public static bool IsFieldEmpty(Board board, int positionX, int positionY)
        {
            if (board.boardArray[positionY, positionX] == board.BlackField)
            {
                return true;
            }
            return false;
        }

        // Re-work needed.
        private bool IsDistanceNotTooShort(Board board, Pawn pawn, int newPositionY, int newPositionX) 
        {
            if (Math.Abs(newPositionY - pawn.PositionY) >= 1 && Math.Abs(newPositionX - pawn.PositionX) >= 1)
            {
                return true;
            }
            return false;
        }

        private bool IsDistanceNotTooFar(Board board, Pawn pawn, int newPositionY, int newPositionX)
        {
            if (!pawn.IsSuperPawn)
            {
                if (Math.Abs(pawn.PositionX - newPositionX) <= 2 && Math.Abs(pawn.PositionY - newPositionY) <= 2)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTwoFieldMove(Board board, Pawn pawn, int newPositionY, int newPositionX)
        {
            if (Math.Abs(pawn.PositionY - newPositionY) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // replace remove of pawn into other class pawncontroller, or req true before execute it
        private bool IsCaptureOfOpponentsPawnPossible(Board board, List<Pawn> pawns, Pawn pawn, int newPositionX, int newPositionY)
        {
            if (IsFieldEmpty(board, newPositionX, newPositionY))
            {
                if (pawn.IsPlayer && !pawn.IsSuperPawn)
                {
                    // to refactor 
                    var enemyPawn = pawns.FirstOrDefault(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1 && p.PositionY == pawn.PositionY - 1);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == CpuPawn.CPU_PAWN_SYMBOL && Math.Abs(enemyPawn.PositionX - newPositionX) == 1)
                    {
                        pawns.Remove(enemyPawn);
                        return true;
                    }
                }

                else if (!pawn.IsPlayer)
                {
                    // to refactor 
                    var enemyPawn = pawns.FirstOrDefault(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1 && p.PositionY == pawn.PositionY + 1);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == PlayerPawn.PLAYER_PAWN_SYMBOL && Math.Abs(enemyPawn.PositionX - newPositionX) == 1)
                    {
                        pawns.Remove(enemyPawn);
                        return true;
                    }
                }
            }
            return false;
        }     
    }
}

