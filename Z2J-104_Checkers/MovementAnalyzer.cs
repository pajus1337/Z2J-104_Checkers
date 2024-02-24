﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class MovementAnalyzer : IMovementAnalyzer
    {
        public bool IsEnemyPawnCapturedOnLastMove { get; private set; }
        private readonly IGameStatusSender _gameStatusSender;

        public MovementAnalyzer(IGameStatusSender gameStatusSender)
        {
            _gameStatusSender = gameStatusSender;
        }

        public bool IsAllowedMovement(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionY, int newPositionX)

        {
            IsEnemyPawnCapturedOnLastMove = false;

            if (pawn == null)
            {
                MenuView.WrongPawnChoice();
                return false;
            }

            bool IsBlackField = IsValidField(board, newPositionX, newPositionY);
            if (!IsBlackField)
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

            if (isTwoFieldMove)
            {
                bool isCaptureOfPawnPossible = false;
                isCaptureOfPawnPossible = IsCaptureOfOpponentsPawnPossible(board, listOfPawns, pawn, newPositionX, newPositionY);
                if (isCaptureOfPawnPossible)
                {                    
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

        public static bool IsValidField(Board board, int positionX, int positionY)
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
            return false;
        }

        // replace remove of pawn into other class pawncontroller, or req true before execute it
        private bool IsCaptureOfOpponentsPawnPossible(Board board, List<Pawn> listOfPawns, Pawn pawn, int newPositionX, int newPositionY)
         {
            if (IsValidField(board, newPositionX, newPositionY))
            {
                int midPointX = (pawn.PositionX + newPositionX) / 2;
                int midPointY = (pawn.PositionY + newPositionY) / 2;

                if (pawn.IsPlayer)
                {
                    var enemyPawn = listOfPawns.FirstOrDefault(p => p.PositionX == midPointX && p.PositionY == midPointY);
                    if (enemyPawn != null && enemyPawn.PawnSymbol == CpuPawn.CPU_PAWN_SYMBOL)
                    {
                        _gameStatusSender.SendStatus($"System : Player Captured CPU {enemyPawn.ToString()}\nSystem : +1 Score");
                        listOfPawns.Remove(enemyPawn);
                        IsEnemyPawnCapturedOnLastMove = true;
                        return true;
                    }
                }

                else if (!pawn.IsPlayer)
                {

                    var enemyPawn = listOfPawns.FirstOrDefault(p => p.PositionX == midPointX && p.PositionY == midPointY);

                    if (enemyPawn != null && enemyPawn.PawnSymbol == PlayerPawn.PLAYER_PAWN_SYMBOL)
                    {
                        _gameStatusSender.SendStatus($"System : CPU Captured Player {enemyPawn.ToString()}\nSystem : +1 Score");
                        listOfPawns.Remove(enemyPawn);
                        IsEnemyPawnCapturedOnLastMove = true;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

