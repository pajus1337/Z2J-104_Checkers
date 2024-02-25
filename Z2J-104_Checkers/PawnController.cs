using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class PawnController : IPawnController
    {
        public List<Pawn> PawnsInGame { get; private set; }
        private readonly IMovementAnalyzer _movementAnalyzer;
        private readonly IGameStateController _gameStateController;
        private readonly IGameStatusSender _gameStatusSender;
        int chosenPositionX = -1;
        int chosenPositionY = -1;

        public PawnController(IMovementAnalyzer movementAnalyzer, IGameStateController gameStateController, IGameStatusSender gameStatusSender, List<Pawn> pawnsInGame)
        {
            PawnsInGame = pawnsInGame;
            _gameStateController = gameStateController;
            _movementAnalyzer = movementAnalyzer;
            _gameStatusSender = gameStatusSender;
        }

        public Board PlacePawnsForNewGame(Board board)
        {
            var boardWithPawns = board;
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlackField)
                    {
                        var cpuPawn = CreatePawn(x, y, false);
                        board.boardArray[y, x] = cpuPawn.PawnSymbol;
                        PawnsInGame.Add(cpuPawn);
                    }
                }
            }

            for (int y = 5; y <= 7; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlackField)
                    {
                        var playerPawn = CreatePawn(x, y, true);
                        board.boardArray[y, x] = playerPawn.PawnSymbol;
                        PawnsInGame.Add(playerPawn);
                    }
                }
            }
            return boardWithPawns;
        }

        public Pawn CreatePawn(int positionX, int positionY, bool isPlayerPawn)
        {
            if (!isPlayerPawn)
            {
                return new CpuPawn(positionX, positionY);
            }
            return new PlayerPawn(positionX, positionY);
        }

        public Pawn SelectPawn()
        {
            Pawn? pawn;
            do
            {
                MenuView.SelectPawnToMove();

                int letters_axis = MenuView.EntryPosition(nameof(letters_axis));
                int digits_axis = MenuView.EntryPosition(nameof(digits_axis));

                Pawn? selectedPawn = PawnsInGame.FirstOrDefault(p => p.PositionX == letters_axis && p.PositionY == digits_axis);

                if (selectedPawn != null)
                {
                    return selectedPawn;
                }

                pawn = selectedPawn;
                MenuView.WrongPawnChoice();
            } while (pawn == null);

            return pawn;
        }

        public (int, int) SelectNewPawnPosition()
        {
            MenuView.SelectNewPositionForPawn();
            int letters_axis = MenuView.EntryPosition(nameof(letters_axis));
            int digits_axis = MenuView.EntryPosition(nameof(digits_axis));
            return (x: digits_axis, y: letters_axis);
        }

        public void MovePlayerPawn(Board board)
        {
            int newPositionY;
            int newPositionX;
            var gameBoard = board;
            var selectedPawn = SelectPawn();
            (newPositionY, newPositionX) = SelectNewPawnPosition();

            if (_movementAnalyzer.IsAllowedMovement(gameBoard, PawnsInGame, selectedPawn, newPositionY, newPositionX))
            {
                _gameStatusSender.SendStatus($"Player : Moving -> {selectedPawn.ToString()}\nPlayer : To new Position X : {newPositionX} , Y : {newPositionY}");
                selectedPawn.PositionX = newPositionX;
                selectedPawn.PositionY = newPositionY;
                _gameStatusSender.SendStatus("**** **** PLAYER TURN ENDS **** ****");
                _gameStateController.TurnEnds();
            }
            _gameStateController.OnInvalidMove();
        }

        public void MoveCpuPawn(CpuPawn cpuPawnInAction, int newPositionX, int newPositionY)
        {
            _gameStatusSender.SendStatus($"CPU : Moving -> {cpuPawnInAction.ToString()}\nCPU : To new Position X : {newPositionX} , Y : {newPositionY}");
            cpuPawnInAction.PositionX = newPositionX;
            cpuPawnInAction.PositionY = newPositionY;
            _gameStatusSender.SendStatus("**** **** CPU TURN ENDS **** ****");
            _gameStateController.TurnEnds();
        }

        public void RemovePawn(Pawn pawn)
        {
            PawnsInGame.Remove(pawn);
        }
    }
}

