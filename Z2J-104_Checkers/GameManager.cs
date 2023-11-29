using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class GameManager 
    {
        // Board board = new BoardBuilder().CreateNewGameBoard();
        Board gameBoard = new PawnController().PlacePawnsForNewGame(new BoardBuilder().CreateNewGameBoard());
        BoardView boardView = new BoardView();


        public GameManager()
        {
           
        }

        public void test()
        {
            boardView.DisplayCurrentBoard(gameBoard);
        }
        
    }
}
