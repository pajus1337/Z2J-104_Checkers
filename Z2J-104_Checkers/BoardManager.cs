using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardManager 
    {
        private BoardView _boardView;

        public BoardManager()
        {
            Board board = new Board();
            _boardView = new BoardView(board);
        }

        public void test()
        {
            _boardView.CreateStartUpBoard();
            _boardView.DisplayBoard();
        }
        
    }
}
