namespace Z2J_104_Checkers
{
    public class PawnController
    {
        private List<Pawn> pawns = new List<Pawn>();
        MenuView menuView;
        int chosenPositionX = -1;
        int chosenPositionY = -1;

        public PawnController(MenuView menuView)
        {
            this.menuView = menuView;
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
                        pawns.Add(cpuPawn);
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
                        pawns.Add(playerPawn);
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

            Pawn? selectedPawn = pawns.FirstOrDefault(p => p.PositionX == letters_axis && p.PositionY == digits_axis);
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

            return (digits_axis, letters_axis);
        }
        

        public void Test((int x, int y) position)
        {

        }
        public void MovePawn(Pawn pawn)
        {
            Test(SelectNewPawnPostion());

            int letters_axis = new MenuView().EntryPosition(nameof(letters_axis)); // X
            int digits_axis = new MenuView().EntryPosition(nameof(digits_axis)); // y
        }
        public bool CheckIfPawnExistOnBoard(int x, int y) => pawns.Any(p => p.PositionX == x && p.PositionY == y);

    }
}

