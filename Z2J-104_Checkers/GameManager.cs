using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class GameManager : IGameManager
    {
        PawnController pawnController;
        public Board GameBoard { get; private set; }
        BoardView boardView;
        private MovementAnalyzer movementAnalyzer;
        MenuView menuView;

        public GameManager(MenuView menuView)
        {
            this.menuView = menuView;
            this.pawnController = new PawnController(this.menuView, this);
            this.GameBoard = pawnController.PlacePawnsForNewGame(BoardBuilder.CreateNewGameBoard());
            this.boardView = new BoardView();
            this.movementAnalyzer = new MovementAnalyzer();
        }

        public void test()
        {
            do
            {
                Console.Clear();
                boardView.DisplayCurrentBoard(GameBoard);
                //pawnController.SelectPawn();
                //pawnController.SelectNewPawnPosition();
                pawnController.MovePawn();
                BoardBuilder.UpdateBoardState(GameBoard, pawnController.PawnsInGame);
                boardView.DisplayCurrentBoard(GameBoard);
                Console.ReadKey();
            } while (true);

        }

        public Board GetBoard() => this.GameBoard;

        public MovementAnalyzer GetMovementAnalyzer() => this.movementAnalyzer;
    }
}
