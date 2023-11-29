using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardBuilder
    {
        private StringBuilder stringBuilder;

        public BoardBuilder()
        {
            stringBuilder = new StringBuilder();
        }

        public Board CreateNewGameBoard()
        {
            Board board = new Board();
            FillNewBoard(board);
            return board;
        }

        public void FillNewBoard(Board board)
        {
            stringBuilder.Append(' ');
            int counter = 1;
            int asciiChar = 65;
            for (int i = 0; i < board.WidthY; i++)
            {
                stringBuilder.Append(((char)asciiChar));
                asciiChar++;
                if (i == board.WidthX - 1)
                {
                    stringBuilder.Append('\n');
                }

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
