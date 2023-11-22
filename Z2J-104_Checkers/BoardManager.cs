using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class BoardManager 
    {
        private BoardView boardView;

        public BoardManager()
        {
            
            Board board = new Board();
            boardView = new BoardView(board);
        }

        public void test()
        {
            boardView.CreateStartUpBoard();
            boardView.PlacePawnsForNewGame();
            boardView.DisplayBoard();
            boardView.
        }
        
    }
}
