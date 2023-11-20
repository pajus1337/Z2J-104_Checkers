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
        private List<Pawn> pawns = new List<Pawn>();
        private StringBuilder stringBuilder;

        public BoardView(Board board)
        {
            this.board = board;
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
                        stringBuilder.Append(board.boardArray[i, k]);
                    }

                }
            }
            Console.WriteLine(stringBuilder);
        }

        public Pawn AddCpuPawn(int positionX, int positionY)
        {
            return new CpuPawn(positionX, positionY);
        }

        public void AddPlayerPawn()
        {

        }

        public void PlacePawnsForNewGame()
        {
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlacKField)
                    {
                        //AddCpuPawn(x,y);
                        var cpuPawn = AddCpuPawn(x, y); 
                        board.boardArray[y, x] = cpuPawn.PawnSymbol;
                        pawns.Add(cpuPawn);
                    }
                }
            }

            for (int i = 5; i <= 7; i++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[i, x] == board.BlacKField)
                    {
                    }
                }
            }
        }
    }
}

