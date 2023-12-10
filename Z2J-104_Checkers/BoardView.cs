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
            stringBuilder.Clear();
            int asciiChar = 65;
            stringBuilder.Append(' ');
            for (int i = 0; i < board.WidthY; i++)
            {
                stringBuilder.Append(((char)asciiChar));
                asciiChar++;
                if (i == board.WidthX - 1)
                {
                    stringBuilder.Append('\n');
                }
            }


            for (int y = 0; y < board.WidthY; y++)
            {
                stringBuilder.Append(y + 1);
                for (int x = 0; x <= board.WidthX; x++)
                {
                    if (x == board.WidthX)
                    {
                        stringBuilder.Append(y + 1);
                        stringBuilder.Append('\n');
                    }
                    else
                    {
                        stringBuilder.Append(board.boardArray[y, x]);
                    }
                }
            }

            asciiChar = 65;
            stringBuilder.Append(' ');
            for (int i = 0; i < board.WidthY; i++)
            {          
                stringBuilder.Append(((char)asciiChar));
                asciiChar++;
                if (i == board.WidthX - 1)
                {
                    stringBuilder.Append('\n');
                }
            }
            Console.WriteLine(stringBuilder);
        }
    }
}


