using System.ComponentModel;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class PawnController : IPawnController
    {
        public List<Pawn> PawnsInGame { get; private set; }
        private readonly IMovementAnalyzer _movementAnalyzer;
        private readonly IGameStateController _gameStateController;
        private Board _board1;
        int chosenPositionX = -1;
        int chosenPositionY = -1;

        public PawnController(IMovementAnalyzer movementAnalyzer, IGameStateController gameStateController, Board board)
        {
            PawnsInGame = new List<Pawn>();
            _gameStateController = gameStateController;
            _movementAnalyzer = movementAnalyzer;
            _board1 = board;
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

            //TEST TO REMOVE !! ! ! ! ! ! ! ! !
            //var dummyPawn = new PlayerPawn(2,3);
            //PawnsInGame.Add(dummyPawn);
            //board.boardArray[dummyPawn.PositionY,dummyPawn.PositionX] = dummyPawn.PawnSymbol;
            /// END 
            /// 

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
            MenuView.SelectNewPostionForPawn();

            int letters_axis = MenuView.EntryPosition(nameof(letters_axis));
            int digits_axis = MenuView.EntryPosition(nameof(digits_axis));

            // Add MovementAnalyzer


            return (x: digits_axis, y: letters_axis);
        }

        public void MovePlayerPawn()
        {
            int newPositionY;
            int newPositionX;
            var gameBoard = _board1;
            var SelectedPawn = SelectPawn();

            (newPositionY, newPositionX) = SelectNewPawnPosition();


            if (_movementAnalyzer.IsAllowedMovement(gameBoard, PawnsInGame, SelectedPawn, newPositionY, newPositionX))
            {
                SelectedPawn.PositionX = newPositionX;
                SelectedPawn.PositionY = newPositionY;
                if (!_movementAnalyzer.IsEnemyPawnCapturedOnLastMove)
                {
                    _gameStateController.TurnEnds();
                }
                else
                {
                    MenuView.MoveFailed();
                }
            }
        }

        public void MoveCpuPawn(CpuPawn cpuPawnInAction, int newPositionX, int newPositionY)
        {
            cpuPawnInAction.PositionX = newPositionX;
            cpuPawnInAction.PositionY = newPositionY;
            
            if (!_movementAnalyzer.IsEnemyPawnCapturedOnLastMove)
            {
                    _gameStateController.TurnEnds();
                }
        }

        public void RemovePawn(Pawn pawn)
        {
            PawnsInGame.Remove(pawn);
        }

        public bool CheckIfPawnExistOnBoard(int x, int y) => PawnsInGame.Any(p => p.PositionX == x && p.PositionY == y);

        public void TurnEnds()
        {
        }
    }
}

