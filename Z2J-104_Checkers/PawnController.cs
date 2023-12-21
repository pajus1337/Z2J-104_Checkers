using System.ComponentModel;

namespace Z2J_104_Checkers
{
    public class PawnController : IGameManager
    {
        public List<Pawn> PawnsInGame { get; private set; }
        private IGameManager gameManager;
        MenuView menuView;
        int chosenPositionX = -1;
        int chosenPositionY = -1;

        public PawnController(MenuView menuView, IGameManager gameManager)
        {
            this.menuView = menuView;
            PawnsInGame = new List<Pawn>();
            this.gameManager = gameManager;
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
            menuView.SelectPawnToMove();

            int letters_axis = menuView.EntryPosition(nameof(letters_axis)); 
            int digits_axis = menuView.EntryPosition(nameof(digits_axis)); 

            Pawn? selectedPawn = PawnsInGame.FirstOrDefault(p => p.PositionX == letters_axis && p.PositionY == digits_axis);
            if (selectedPawn == null)
            {
                menuView.WrongPawnChoice();
            }
            return selectedPawn;
        }

        public (int, int) SelectNewPawnPosition()
        {
            menuView.SelectNewPostionForPawn();

            int letters_axis = menuView.EntryPosition(nameof(letters_axis));
            int digits_axis = menuView.EntryPosition(nameof(digits_axis));

            // Add MovementAnalyzer


            return (digits_axis, letters_axis);
        }
        
        public void MovePlayerPawn()
        {
            int newPositionY;
            int newPositionX;
            var gameBoard = GetBoard();
            var SelectedPawn = SelectPawn();
            var movementAnalyzer = GetMovementAnalyzer();
           
            (newPositionY, newPositionX) = SelectNewPawnPosition();


            if (movementAnalyzer.IsAllowedMovement(gameBoard,PawnsInGame, SelectedPawn, newPositionY, newPositionX))
            {
                SelectedPawn.PositionX = newPositionX;
                SelectedPawn.PositionY = newPositionY;
            }
            else
            {
                menuView.MoveFailed();
            }
        }

    public void MoveCpuPawn(CpuPawn cpuPawnInAction, int newPositionX, int newPositionY)
        {
            cpuPawnInAction.PositionX = newPositionX;
            cpuPawnInAction.PositionY = newPositionY;
        }



        public bool CheckIfPawnExistOnBoard(int x, int y) => PawnsInGame.Any(p => p.PositionX == x && p.PositionY == y) ;

        public Board GetBoard() => gameManager.GetBoard();

        public MovementAnalyzer GetMovementAnalyzer() => gameManager.GetMovementAnalyzer();
    }
}

