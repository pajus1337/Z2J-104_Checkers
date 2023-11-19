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
        private Board board;

        private CpuPawn cpuPawn;
        private PlayerPawn playerPawn;
        private StringBuilder stringBuilder;

        public BoardView(Board board)
        {
            this.board = board;
            cpuPawn = new CpuPawn();
            playerPawn = new PlayerPawn();
            stringBuilder = new StringBuilder();
        }

        public void CreateStartUpBoard()
        {
            stringBuilder.Append(' ');
            int counter = 1;
            int asciiChar = 65;
            for (int i = 0; i < board.WidthY; i++)
            {              
                stringBuilder.Append(((char)asciiChar));
                asciiChar++;
                if (i == board.WidthX-1)
                {
                    stringBuilder.Append('\n');
                }

                for (int j = 0; j < board.WidthX; j++)
                {
                    if ( i%2 == 0)
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

        public void DisplayBoard()
        {
            for (int i = 0; i < board.WidthY; i++)
            {
                stringBuilder.Append(i + 1);
                for (int k = 0; k <= board.WidthX; k++)
                {
                    if (k == board.WidthX)
                    {
                        stringBuilder.Append('\n');
                    }
                    else
                    {
                        stringBuilder.Append(board.boardArray[i,k]);
                    }

                }
            }
            Console.WriteLine(stringBuilder);
        }

    }
}

