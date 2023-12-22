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
        CPUChoiceAnalyzer cpuChoiceAnalyzer;



        public GameManager(MenuView menuView)
        {
            this.menuView = menuView;
            this.pawnController = new PawnController(this.menuView, this);
            this.GameBoard = pawnController.PlacePawnsForNewGame(BoardBuilder.CreateNewGameBoard());
            this.boardView = new BoardView();
            this.movementAnalyzer = new MovementAnalyzer();
            this.cpuChoiceAnalyzer = new CPUChoiceAnalyzer(this.GameBoard, movementAnalyzer, pawnController);
        }


        public void test()
        {
            do
            {
                SetupTurnHandlers();
                //cpuChoiceAnalyzer.TestOfMovementLogik();
                ////cpuChoiceAnalyzer.SearchForFrontPawn(GameBoard);
                //Console.ReadKey();

                Console.Clear();
                BoardBuilder.UpdateBoardState(GameBoard, pawnController.PawnsInGame);
                boardView.DisplayCurrentBoard(GameBoard);
                pawnController.MovePlayerPawn();
                Console.ReadKey();

            } while (true);
        }

        public Board GetBoard() => this.GameBoard;

        public MovementAnalyzer GetMovementAnalyzer() => this.movementAnalyzer;

        public void SetupTurnHandlers()
        {
            PlayerTurn += OnTurnChanged;
            PlayerTurn += PlayerTurnStart;

            CPUTurn += CPUTurnStart;
            CPUTurn += OnTurnChanged;
        }

        public delegate void PlayerTurnHandler();
        public delegate void CPUTurnHandler();
        public event PlayerTurnHandler PlayerTurn;
        public event CPUTurnHandler CPUTurn;

        public void OnTurnChanged()
        {
            Console.Clear();
            BoardBuilder.UpdateBoardState(GameBoard, pawnController.PawnsInGame);
            boardView.DisplayCurrentBoard(GameBoard);
        }

        public void PlayerTurnStart()
        {
            pawnController.MovePlayerPawn();
        }

        public void CPUTurnStart()
        {
            Console.WriteLine("CPU TURN START");
        }
        public void CPUTurnEnd()
        {
            PlayerTurn?.Invoke();
        }

        public void PlayerTurnEnd()
        {
            CPUTurn?.Invoke();
        }
    }
}
