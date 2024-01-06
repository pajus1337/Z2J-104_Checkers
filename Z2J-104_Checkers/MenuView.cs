using System;
using System.ComponentModel.DataAnnotations;

namespace Z2J_104_Checkers
{
    public class MenuView
    {
        public static UserInputValidator userInputValidator = new UserInputValidator();
        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to a simple game: checkers");
            Console.WriteLine($"Some important information before you start playing,\n-The size of the board is 8x8\n- The white squares are marked 'O'\n- The black squares are marked 'X'\n");
            Console.WriteLine($"- Computer pawns are marked \n- Player pawns are marked 'U'\nHave a nice game\n");
            Console.WriteLine($"Press any key to start");
            Console.ReadKey();
        }

        public int MainMenuOptionsView()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Possible options :\n1.start the game.\n2.exit the game");
                var userChosen = Console.ReadKey(true).KeyChar.ToString();

                if (int.TryParse(userChosen,out int chosenOption) & chosenOption == 1 | chosenOption == 2)
                {
                    if (chosenOption == 1)
                    {
                        return 1;
                    }
                    else return 2;
                }
                Console.WriteLine($"You used the [{userChosen}] key for which there is no assigned property, try again and select the correct option ");
                Console.WriteLine("Press any key to return to the selection menu");
                Console.ReadKey();
            } while (true);
        }

        public int EntryPosition(string selectedAxis)
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
                    (resultValue, isWrongValue) = userInputValidator.IsChosenCorrectLetter(userKey);
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
                    (resultValue, isWrongValue) = userInputValidator.IsChosenCorrectNumber(userKey);
                    if (isWrongValue)
                    {
                        Console.WriteLine($"The entered value [{userKey}] is wrong");
                    }
                } while (isWrongValue);
                return resultValue;
            }
        }

        public void SelectPawnToMove()
        {
            Console.WriteLine($"Specify the position of the pawn to be moved.");       
        }

        public void SelectNewPostionForPawn()
        {
            Console.WriteLine($"Specify the new position for the pawn to be moved.");
        }

        public static void WrongPawnChoice()
        {
            Console.WriteLine("There is no pawn under this position.");
        }

        public void MoveFailed()
        {
            Console.WriteLine("An impossible move was made");
        }

        public void Gameover()
        {
            Console.WriteLine("End of the game with a result");
            Console.WriteLine($"Player Score : \n\rCPU Score: ");
        }
    }
}
