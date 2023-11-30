using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class GameManager
    {
        PawnController pawnController;
        Board gameBoard;
        BoardView boardView;
        MovementAnalyzer movementAnalyzer;


        public GameManager()
        {
            this.pawnController = new PawnController();
            this.gameBoard = pawnController.PlacePawnsForNewGame(BoardBuilder.CreateNewGameBoard());
            this.boardView = new BoardView();
            this.movementAnalyzer = new MovementAnalyzer();
        }

        public void test()
        {
            pawnController.MovePawn();
            boardView.DisplayCurrentBoard(gameBoard);
            Console.WriteLine(pawnController.CheckIfPawnExistOnBoard(0, 1));
        }

    }
}
