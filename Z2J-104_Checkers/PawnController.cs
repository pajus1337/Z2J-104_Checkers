namespace Z2J_104_Checkers
{
    public class PawnController
    {
        public List<Pawn> PawnsInGame { get; private set; }
        MenuView menuView;
        int chosenPositionX = -1;
        int chosenPositionY = -1;

        public PawnController(MenuView menuView)
        {
            this.menuView = menuView;
            PawnsInGame = new List<Pawn>();
        }

        public Board PlacePawnsForNewGame(Board board)
        {
            var boardWithPawns = board;
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x < board.WidthX; x++)
                {
                    if (board.boardArray[y, x] == board.BlacKField)
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
                    if (board.boardArray[y, x] == board.BlacKField)
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

        public (int, int) SelectNewPawnPostion()
        {
            menuView.SelectNewPostionForPawn();

            int letters_axis = menuView.EntryPosition(nameof(letters_axis));
            int digits_axis = menuView.EntryPosition(nameof(digits_axis));

            // Add MovementAnalyzer


            return (digits_axis, letters_axis);
        }
        
        public void MovePawn()
        {
            int positionY;
            int positionX;
            var SelectedPawn = SelectPawn();
            (positionY, positionX ) = SelectNewPawnPostion();
            SelectedPawn.PositionX = positionX; 
            SelectedPawn.PositionY = positionY;
        }

        public void UpdatePawnPosition(Pawn pawn ,int positionX, int PositionY)
        {

        }
        public bool CheckIfPawnExistOnBoard(int x, int y) => PawnsInGame.Any(p => p.PositionX == x && p.PositionY == y) ;

    }
}

