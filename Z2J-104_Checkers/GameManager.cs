using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class GameManager : IGameManager
    {
        public enum Turn { Player, CPU }
        private Turn currentTurn;

        PawnController pawnController;
        public Board GameBoard { get; private set; }

        BoardView boardView;
        private MovementAnalyzer movementAnalyzer;
        MenuView menuView;
        CPUChoiceAnalyzer cpuChoiceAnalyzer;
        public bool IsGameOver { get; private set; }
        private readonly bool _isGameOver = false;
        public int PlayerScore { get; set; } = 0;
        public int CPUScore { get; set; } = 0;



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
                currentTurn = Turn.Player;
                PlayerTurn();

            } while (!IsGameOver);
        }

        public Board GetBoard() => this.GameBoard;

        public MovementAnalyzer GetMovementAnalyzer() => this.movementAnalyzer;

        public void TurnEnds()
        {
            if (currentTurn == Turn.Player)
            {
                currentTurn = Turn.CPU;
                CPUTurn?.Invoke();
            }
            else
            {
                currentTurn = Turn.Player;
                PlayerTurn?.Invoke();
            }
        }

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
            if (!IsGameOver)
            {
                Console.Clear();
                BoardBuilder.UpdateBoardState(GameBoard, pawnController.PawnsInGame);
                boardView.DisplayCurrentBoard(GameBoard);
            }
            else
            {
                Console.WriteLine("Game Over");
            }
        }

        public void PlayerTurnStart()
        {
            Console.Clear();
            BoardBuilder.UpdateBoardState(GameBoard, pawnController.PawnsInGame);
            boardView.DisplayCurrentBoard(GameBoard);
            pawnController.MovePlayerPawn();
        }

        public void CPUTurnStart()
        {
            cpuChoiceAnalyzer.PickAndMoveCPUPawn();
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

        public bool isGameOver(bool isGameOver = false)
        {
            IsGameOver = isGameOver;
            return isGameOver;
        }

    }
}
