using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers.BoardServices
{
    public class BoardBuilder
    {
        public static void CreateNewGameBoard(Board board) => FillNewBoard(board);

        private static void FillNewBoard(Board board)
        {
            int counter = 1;

            for (int i = 0; i < board.WidthY; i++)
            {
                for (int j = 0; j < board.WidthX; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (counter % 2 == 0)
                        {
                            counter++;
                            board.boardArray[i, j] = board.BlackField;
                        }
                        else
                        {
                            counter++;
                            board.boardArray[i, j] = board.WhiteField;
                        }
                    }
                    else
                    {
                        if (counter % 2 == 1)
                        {
                            counter++;
                            board.boardArray[i, j] = board.BlackField;
                        }
                        else
                        {
                            counter++;
                            board.boardArray[i, j] = board.WhiteField;
                        }
                    }
                }
            }
        }

        public static void UpdateBoardState(Board board, List<Pawn> pawnList)
        {
            int counter = 1;

            for (int y = 0; y < board.WidthY; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {

                    {
                        if (y % 2 == 0)
                        {
                            if (counter % 2 == 0)
                            {
                                counter++;
                                board.boardArray[y, x] = board.BlackField;
                            }
                            else
                            {
                                counter++;
                                board.boardArray[y, x] = board.WhiteField;
                            }
                        }
                        else
                        {
                            if (counter % 2 == 1)
                            {
                                counter++;
                                board.boardArray[y, x] = board.BlackField;
                            }
                            else
                            {
                                counter++;
                                board.boardArray[y, x] = board.WhiteField;
                            }
                        }

                        var pawn = pawnList.FirstOrDefault(p => p.PositionY == y && p.PositionX == x);
                        if (pawn != null)
                        {
                            board.boardArray[y, x] = pawn.PawnSymbol;
                        }
                    }
                }
            }
        }
    }
}
