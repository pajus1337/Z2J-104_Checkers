using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardView
    {
        private StringBuilder stringBuilder;

        public BoardView()
        {
            stringBuilder = new StringBuilder();
        }

        public void DisplayCurrentBoard(Board board)
        {
            int asciiChar = 65;

            for (int i = 0; i < board.WidthY; i++)
            {
                stringBuilder.Append(((char)asciiChar));
                asciiChar++;
                stringBuilder.Append(i + 1);
                for (int k = 0; k <= board.WidthX; k++)
                {
                    if (k == board.WidthX)
                    {
                        stringBuilder.Append('\n');
                    }
                    else
                    {
                        stringBuilder.Append(board.boardArray[i, k]);
                    }

                }
            }
            Console.WriteLine(stringBuilder);
        }
    }
}

