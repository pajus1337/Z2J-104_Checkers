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
        public static int PlayerScore { get; set; } = 0;
        public static int CPUScore { get; set; } = 0;
        private readonly IBoardView _boardView;
        private readonly ICPUChoiceAnalyzer _cpuChoiceAnalyzer;
        private readonly IPawnControllerFactory _pawnControllerFactory;
        private readonly IGameStatusSender _gameStatusSender;
        private IPawnController _pawnController;
        public event Action PlayerTurnStarted;
        public event Action CPUTurnStarted;
        public event Action InvalidMovementPlayer;
        public event Action InvalidMovementPlayerCpu;
        public event Action<Board> BoardUpdate;
        public enum Turn { Player, CPU }
        private Turn currentTurn;

        public GameStateController(IBoardView boardView, ICPUChoiceAnalyzer cpuChoiceAnalyzer, IPawnControllerFactory pawnControllerFactory, IGameStatusSender gameStatusSender, Board board, List<Pawn> pawnsInGame)
        {
            _boardView = boardView;
            _cpuChoiceAnalyzer = cpuChoiceAnalyzer;
            _pawnControllerFactory = pawnControllerFactory;
            _gameStatusSender = gameStatusSender;
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

        public void OnInvalidMove()
        {
            if (currentTurn == Turn.Player)
            {
                InvalidMovementPlayer?.Invoke();
            }
            else
            {
                InvalidMovementPlayerCpu?.Invoke();
            }
        }

        private void SetupTurnHandlers()
        {
            PlayerTurnStarted += PlayerTurnStart;
            CPUTurnStarted += CPUTurnStart;
            InvalidMovementPlayer += InvalidPlayerMovement;
            InvalidMovementPlayerCpu += InvalidPlayerCpuMovement;
        }

        private void OnTurnChanged()
        {
            CheckGameState();
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
            _gameStatusSender.SendStatus("**** **** PLAYER TURN STARTS **** ****");
                        OnTurnChanged();
            _pawnController = _pawnControllerFactory.CreatePawnController();
            _pawnController.MovePlayerPawn(GameBoard);
        }

        private void CPUTurnStart()
        {
            _gameStatusSender.SendStatus("**** **** CPU TURN STARTS **** ****");
            //  Debug.Print("CPU TURN START");
            _cpuChoiceAnalyzer.PickAndMoveCPUPawn();
            OnTurnChanged();
            TurnEnds();
        }

        private void InvalidPlayerMovement()
        {
            MenuView.MoveFailed();
            _gameStatusSender.SendStatus("System : Invalid Player Movement");
            PlayerTurnStart();
        }

        private void InvalidPlayerCpuMovement()
        {
            _gameStatusSender.SendStatus("System : Invalid CPU Movement");
            CPUTurnStart();
        }

        private void CheckGameState()
        {
            SendScoreMessage();
        }

        private void SendScoreMessage()
        {
            var scoreMessage = MenuView.ScoreStatusMessage("Player", "CPU", PlayerScore, CPUScore);
            _gameStatusSender.SendStatus(scoreMessage);
        }
    }
}

