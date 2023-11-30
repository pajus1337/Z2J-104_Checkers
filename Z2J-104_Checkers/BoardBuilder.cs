using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardBuilder
    {

        public static Board CreateNewGameBoard()
        {
            Board board = new Board();
            FillNewBoard(board);
            return board;
        }

        public static void FillNewBoard(Board board)
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
                            board.boardArray[i, j] = board.BlacKField;
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
                            board.boardArray[i, j] = board.BlacKField;
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
    }
}
