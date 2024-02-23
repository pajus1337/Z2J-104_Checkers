using System;
using System.ComponentModel.DataAnnotations;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public static class MenuView 
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to a simple game: checkers");
            Console.WriteLine($"Some important information before you start playing,\n-The size of the board is 8x8\n- The white squares are marked 'O'\n- The black squares are marked 'X'\n");
            Console.WriteLine($"- Computer pawns are marked \n- Player pawns are marked 'U'\nHave a nice game\n");
            Console.WriteLine($"Press any key to start");
            Console.ReadKey();
        }

        public static int EntryPosition(string selectedAxis)
        {
         //   Console.WriteLine("Enter the position of the pawn you want to move, eg: a5 or 5b");
            if (selectedAxis == "letters_axis")
            {
                char userKey;
                int resultValue;
                bool isWrongValue;
                do
                {
                    Console.WriteLine($"Enter a value [A,B,C,D .. etc] in the visible range of {selectedAxis}");
                    userKey = Console.ReadKey(true).KeyChar;
                    (resultValue, isWrongValue) = UserInputValidator.IsChosenCorrectLetter(userKey);
                    if (isWrongValue)
                    {
                        Console.WriteLine($"The entered value [{userKey}] is wrong");
                    }

                } while (isWrongValue);
                return resultValue;
            }
            else
            {
                char userKey;
                int resultValue;
                bool isWrongValue;
                do
                {
                    Console.WriteLine($"Enter a value [1,2,3,4 .. etc] in the visible range of {selectedAxis}");
                    userKey = Console.ReadKey(true).KeyChar;
                    (resultValue, isWrongValue) = UserInputValidator.IsChosenCorrectNumber(userKey);
                    if (isWrongValue)
                    {
                        Console.WriteLine($"The entered value [{userKey}] is wrong");
                    }
                } while (isWrongValue);
                return resultValue;
            }
        }

        public static void SelectPawnToMove()
        {
            Console.WriteLine($"Specify the position of the pawn to be moved.");       
        }

        public static void SelectNewPostionForPawn()
        {
            Console.WriteLine($"Specify the new position for the pawn to be moved.");
        }

        public static void WrongPawnChoice()
        {
            Console.WriteLine("There is no pawn under this position.");
        }

        public static void MoveFailed()
        {
            Console.WriteLine("An impossible move was made");
        }

        public static void Gameover()
        {
            Console.WriteLine("End of the game with a result");
            Console.WriteLine($"Player Score : \n\rCPU Score: ");
        }
    }
}
