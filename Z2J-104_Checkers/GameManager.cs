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
        MenuView menuView;


        public GameManager(MenuView menuView)
        {
            this.menuView = menuView;
            this.pawnController = new PawnController(this.menuView);
            this.gameBoard = pawnController.PlacePawnsForNewGame(BoardBuilder.CreateNewGameBoard());
            this.boardView = new BoardView();
            this.movementAnalyzer = new MovementAnalyzer();
        }

        public void test()
        {
            boardView.DisplayCurrentBoard(gameBoard);
            Console.WriteLine(pawnController.CheckIfPawnExistOnBoard(0, 1));
        }

        public void MovePawn()
        {
            int letters_axis = menuView.EntryPosition(nameof(letters_axis)); // X
            int digits_axis = menuView.EntryPosition(nameof(digits_axis)); // y
        }
    }
}
