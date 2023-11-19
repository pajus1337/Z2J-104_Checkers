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
        private Board _board;

        private CpuPawn _cpuPawn;
        private PlayerPawn _playerPawn;
        private StringBuilder _stringBuilder;

        public BoardView(Board board)
        {
            _board = board;
            _cpuPawn = new CpuPawn();
            _playerPawn = new PlayerPawn();
            _stringBuilder = new StringBuilder();
        }

        public void SetStartUpBoard()
        {
            int counter = 1;
            for (int i = 1; i < _board.WidthY; i++)
            {
                for (int j = 1; j < _board.WidthX; j++)
                {
                    if (counter % 2 == 0)
                    {
                        counter++;
                        _board.boardArray[i, j] = _board.BlacKField;
                    }
                    else
                    {
                        counter++;
                        _board.boardArray[i, j] = _board.WhiteField;
                    }
                }
            }

        }

        public void DisplayBoard()
        {
            for (int i = 0; i < _board.WidthY; i++)
            {
                for (int k = 0; k <= _board.WidthX; k++)
                {
                    if (k == _board.WidthX)
                    {
                        _stringBuilder.Append('\n');
                    }
                    else
                    {
                        _stringBuilder.Append(_board.boardArray[i,k]);
                    }

                }
            }
            Console.WriteLine(_stringBuilder);
        }

    }
}

