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
            bool isBlackField = IsABlackColorField(board, newPositionX, newPositionY);
            bool isNotTooFar = IsDistanceNotTooFar(board, pawn, newPositionY, newPositionX);
            bool isTwoFieldMove = IsTwoFieldMove(board, pawn, newPositionY, newPositionX);
            bool isCaptureOfPawnPossible = IsCaptureOfOpponentsPawnPossible(board, listOfPawns, pawn, newPositionX, newPositionY);
            bool isNotTooShort = IsDistanceNotTooShort(board, pawn, newPositionY, newPositionX);

            if (isNotTooShort && isNotTooFar && isBlackField)
            {
                if (isTwoFieldMove && isCaptureOfPawnPossible && isBlackField)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
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

        private bool IsDistanceNotTooShort(Board board, Pawn pawn, int newPositionY, int newPositionX) 
        {

            if (pawn.IsPlayer & newPositionY - pawn.PositionY == -1 & pawn.PositionX - newPositionX == -1 | pawn.PositionX - newPositionX == +1 )
            {
                return true;
            }

            else if (!pawn.IsPlayer && pawn.PositionY - newPositionX == 1 && pawn.PositionY - newPositionY == 1)
            {
                return true;
            }
            return false;
        }

        private bool IsTwoFieldMove(Board board, Pawn pawn, int newPositionY, int newPositionX)
        {
            if (pawn.PositionY - newPositionY == -2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDistanceNotTooFar(Board board, Pawn pawn, int newPositionY, int newPositionX)
        {
            if (pawn.IsPlayer && !pawn.IsSuperPawn)
            {
                if (pawn.PositionX - newPositionX  >= -2 && pawn.PositionY - newPositionY >= -2)
                {
                    return true;
                }
            }
            return board.boardArray[pawn.PositionY - newPositionY, pawn.PositionX - newPositionX] == board.BlackField && ((pawn.PositionY - newPositionY) == 2);
        }


        // replace remove of pawn into other class pawncontroller, or req true before execute it
        private bool IsCaptureOfOpponentsPawnPossible(Board board, List<Pawn> pawns, Pawn pawn, int newPositionX, int newPositionY)
        {
            if (!IsFieldEmpty(board, newPositionX, newPositionY))
            {
                if (pawn.IsPlayer && !pawn.IsSuperPawn)
                {

                    // to refactor 
                    var enemyPawn = pawns.FirstOrDefault(p => p.PositionX == pawn.PositionX - 1 | p.PositionX == pawn.PositionX + 1);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == CpuPawn.CPU_PAWN_SYMBOL)
                    {
                        pawns.Remove(enemyPawn);
                        return true;
                    }
                }
            }

            else if (!pawn.IsPlayer)
            {
                if (board.boardArray[pawn.PositionX - 1, pawn.PositionY - 1] == PlayerPawn.PLAYER_PAWN_SYMBOL)
                {
                    pawns.RemoveAll(p => p.PositionX == p.PositionX - 1 && p.PositionY == p.PositionY - 1);
                    return true;
                }
                else if (board.boardArray[pawn.PositionX + 1, pawn.PositionY - 1] == PlayerPawn.PLAYER_PAWN_SYMBOL)
                {
                    pawns.RemoveAll(p => p.PositionX == p.PositionX + 1 && p.PositionY == p.PositionY - 1);
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }     
    }
}

