using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
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

            bool IsRightDirection = IsValidDirection(pawn, newPositionY);
            if (!IsRightDirection)
            {
                return false;
            }

            bool isPawnOnField = IsPawnOnField(listOfPawns, newPositionX, newPositionY);
            if (isPawnOnField)
            {
                return false;
            }

            bool isDistanceValid = IsValidDistance(board, pawn, newPositionY, newPositionX);
            if (!isDistanceValid)
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

            if (isDistanceValid)
            {
                return true;
            }
            pawn.CountFailMove++;
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

        private static bool IsValidDistance(Board board, Pawn pawn, int newPositionY, int newPositionX)
        {
            if ((Math.Abs(newPositionY - pawn.PositionY) >= 1 && Math.Abs(newPositionY - pawn.PositionY) <= 2) && (Math.Abs(newPositionX - pawn.PositionX) >= 1 && Math.Abs(pawn.PositionX - newPositionX) <= 2))
            {
                return true;
            }
            return false;
        }

        private static bool IsValidDirection(Pawn pawn, int newPositionY)
        {
            var pawnType = pawn.GetType();
            if (pawnType == typeof(PlayerPawn))
            {
                return pawn.PositionY > newPositionY;
            }

            if (pawnType == typeof(CpuPawn))
            {
                return pawn.PositionY < newPositionY;
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
                        GameStateController.PlayerScore++;
                        _gameStatusSender.SendStatus($"System : Player Captured CPU {enemyPawn.ToString()}\nSystem : +1 Score\nSystem : Player Total Score : {GameStateController.PlayerScore}");
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
                        GameStateController.CPUScore++;
                        _gameStatusSender.SendStatus($"System : CPU Captured Player {enemyPawn.ToString()}\nSystem : +1 Score\nSystem : CPU Total Score {GameStateController.CPUScore}");
                        listOfPawns.Remove(enemyPawn);
                        IsEnemyPawnCapturedOnLastMove = true;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsPawnOnField(List<Pawn> listOfPawns, int newPositionX, int newPositionY) 
            => listOfPawns.Any<Pawn>(n => n.PositionX == newPositionX && n.PositionY == newPositionY);      
    }
}

