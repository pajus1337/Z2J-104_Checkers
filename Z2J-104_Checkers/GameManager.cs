using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class GameManager : IGameManager
    {
        public Board GameBoard { get; private set; }
        private readonly IGameStateController _gameStateController;
        private readonly IPawnControllerFactory _pawnControllerFactory;
        private IPawnController _pawnController;
        public bool IsGameOver { get; private set; }
        private readonly bool _isGameOver = false;
        public int PlayerScore { get; set; } = 0;
        public int CPUScore { get; set; } = 0;


        public GameManager(IGameStateController gameStateController)
        {
            _gameStateController = gameStateController;
            InitBoard();
        }

        public void InitGame()
        {
            Run();
            do
            {


            } while (!IsGameOver);
        }

        public void CheckGameStatus()
        {

        }
        private void InitBoard()
        {
            _pawnController = _pawnControllerFactory.CreatePawnController();
            GameBoard = _pawnController.PlacePawnsForNewGame(BoardBuilder.CreateNewGameBoard());
        }

        public void Run()
        {
            _gameStateController.Initialize();
        }

        public Board GetBoard() => this.GameBoard;

        public bool isGameOver(bool isGameOver = false)
        {
            IsGameOver = isGameOver;
            return isGameOver;
        }

    }
}
