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
        public bool IsEnemyPawnCapturedOnLastMove { get; set; }
        public bool IsAllowedMovement(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionY, int newPositionX)

        {
            IsEnemyPawnCapturedOnLastMove = false;

            if (pawn == null)
            {
                MenuView.WrongPawnChoice();
                return false;
            }

            bool isBlackField = IsABlackColorField(board, newPositionX, newPositionY);
            if (!isBlackField)
            {
                return false;
            }

            bool isNotTooShort = IsDistanceNotTooShort(board, pawn, newPositionY, newPositionX);
            if (!isNotTooShort)
            {
                return false;
            }

            bool isNotTooFar = IsDistanceNotTooFar(board, pawn, newPositionY, newPositionX);
            if (!isNotTooFar)
            {
                return false;
            }

            bool isTwoFieldMove = IsTwoFieldMove(board, pawn, newPositionY);
            bool isCaptureOfPawnPossible = false;
            if (isTwoFieldMove)
            {
                isCaptureOfPawnPossible = IsCaptureOfOpponentsPawnPossible(board, listOfPawns, pawn, newPositionX, newPositionY);
                if (isCaptureOfPawnPossible)
                {
                    IsEnemyPawnCapturedOnLastMove = true;
                    return true;
                }
                return false;
            }

            if (isNotTooShort && isNotTooFar)
            {
                return true;
            }
            return false;
        }



        private static bool IsABlackColorField(Board board, int positionX, int positionY)
        {
            if (board.boardArray[positionY, positionX] == board.BlackField)
            {
                return true;
            }
            return false;
        }

        private static bool IsEmptyField(Board board, int positionX, int positionY)
        {
            if (board.boardArray[positionY, positionX] == board.BlackField)
            {
                return true;
            }
            return false;
        }
        private static bool IsDistanceNotTooShort(Board board, Pawn pawn, int newPositionY, int newPositionX) 

        {
            if (Math.Abs(newPositionY - pawn.PositionY) >= 1 && Math.Abs(newPositionX - pawn.PositionX) >= 1)
            {
                return true;
            }
            return false;
        }

        private static bool IsDistanceNotTooFar(Board board, Pawn pawn, int newPositionY, int newPositionX)
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

        private static bool IsTwoFieldMove(Board board, Pawn pawn, int newPositionY)
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
        private bool IsCaptureOfOpponentsPawnPossible(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionX, int newPositionY)
         {
            if (IsEmptyField(board, newPositionX, newPositionY))
            {
                if (pawn.IsPlayer && !pawn.IsSuperPawn)
                {
                    // to refactor 
                    //if (listOfPawns.Any(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1 && p.PositionY == pawn.PositionY - 1))
                    //{

                    var enemyPawn = listOfPawns.FirstOrDefault(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1 && p.PositionY == pawn.PositionY - 1);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == CpuPawn.CPU_PAWN_SYMBOL && Math.Abs(enemyPawn.PositionX - newPositionX) == 1)
                    {
                        listOfPawns.Remove(enemyPawn);

                        return true;
                    }
                }


                else if (!pawn.IsPlayer)
                {
                    // to refactor 
                    var enemyPawn = listOfPawns.FirstOrDefault(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1 && p.PositionY == pawn.PositionY + 1);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == PlayerPawn.PLAYER_PAWN_SYMBOL && Math.Abs(enemyPawn.PositionX - newPositionX) == 1)
                    {
                        listOfPawns.Remove(enemyPawn);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

