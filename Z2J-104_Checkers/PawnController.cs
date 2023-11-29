using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class PawnController
    {
        private List<Pawn> pawns = new List<Pawn>();

        public Board PlacePawnsForNewGame(Board board)
        {
            var boardWithPawns = board;
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlacKField)
                    {
                        var cpuPawn = CreatePawn(x, y, false);
                        board.boardArray[y, x] = cpuPawn.PawnSymbol;
                        pawns.Add(cpuPawn);
                    }
                }
            }

            for (int y = 5; y <= 7; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlacKField)
                    {
                        var playerPawn = CreatePawn(x, y, true);
                        board.boardArray[y, x] = playerPawn.PawnSymbol;
                        pawns.Add(playerPawn);
                    }
                }
            }
            return boardWithPawns;
        }

        public Pawn CreatePawn(int positionX, int positionY, bool isPlayerPawn)
        {
            if (!isPlayerPawn)
            {
                return new CpuPawn(positionX, positionY);
            }
            return new PlayerPawn(positionX, positionY);
        }
    }
}

