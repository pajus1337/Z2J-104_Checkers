using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class GameStateController : IGameStateController
    {
        public bool IsGameOver { get; private set; }
        public Board GameBoard { get; private set; }
        public List<Pawn> PawnsInGame { get; set; }

        private readonly IBoardView _boardView;
        private readonly IPawnController _pawnController;
        private readonly ICPUChoiceAnalyzer _cpuChoiceAnalyzer;

        public event Action PlayerTurnStarted;
        public event Action CPUTurnStarted;
        public event Action<Board> BoardUpdate;
        public enum Turn { Player, CPU }
        private Turn currentTurn;

        public GameStateController(IBoardView boardView, IPawnController pawnController, ICPUChoiceAnalyzer cpuChoiceAnalyzer, Board board, List<Pawn> pawnsInGame)
        {
            _boardView = boardView;
            _pawnController = pawnController;
            _cpuChoiceAnalyzer = cpuChoiceAnalyzer;
            GameBoard = board;
            PawnsInGame = pawnsInGame;
        }

        public void Initialize()
        {
            SetupTurnHandlers();
            currentTurn = Turn.Player;
            PlayerTurnStart();
        }

        public void TurnEnds()
        {
            if (currentTurn == Turn.Player)
            {
                currentTurn = Turn.CPU;
                CPUTurnStarted?.Invoke();
            }
            else
            {
                currentTurn = Turn.Player;
                PlayerTurnStarted?.Invoke();
            }
        }

        private void SetupTurnHandlers()
        {
            PlayerTurnStarted += PlayerTurnStart;
            CPUTurnStarted += CPUTurnStart;
        }

        private void OnTurnChanged()
        {
            if (!IsGameOver)
            {
                Console.Clear();
                BoardBuilder.UpdateBoardState(GameBoard, PawnsInGame);
                _boardView.DisplayCurrentBoard(GameBoard);
                BoardUpdate?.Invoke(GameBoard);
            }
            else
            {
                // Console.WriteLine("Game Over");
            }
        }

        private void PlayerTurnStart()
        {
            OnTurnChanged();
            _pawnController.MovePlayerPawn();
        }

        private void CPUTurnStart()
        {
            //  Debug.Print("CPU TURN START");
            _cpuChoiceAnalyzer.PickAndMoveCPUPawn();
            OnTurnChanged();
            TurnEnds();
        }
    }
}

