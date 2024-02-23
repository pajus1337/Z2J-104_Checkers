using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;

namespace Z2J_104_Checkers
{
    public static class UserInputValidator
    {
       public static (int, bool)IsChosenCorrectLetter(char userInput)
        {
            bool isWrongValue = true;
            if (!char.IsDigit(userInput) && char.ToUpper(userInput) >= 65 && char.ToUpper(userInput) <= 72)
            {
                Enum.TryParse<BoardLetters>(userInput.ToString(), true, out BoardLetters result); ;
                Console.WriteLine("Good its not a digit");
                return ((int)result, !isWrongValue);
            }
            else
            {
                return (-1, isWrongValue);
            }
        }

        public static (int, bool) IsChosenCorrectNumber(char userInput)
        {
            bool isWrongValue = true;
            if (char.IsDigit(userInput) && (userInput >= '1' && userInput <= '8'))
            {
                Console.WriteLine("Good its a digit");
                return (userInput - '1', !isWrongValue);
            }
            else
            {
                return (-1, isWrongValue);
            }
        }

        public static int ValidateAndSetMainMenuInput()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Possible options :\n1.start the game.\n2.exit the game");
                var userChosen = Console.ReadKey(true).KeyChar.ToString();

                if (int.TryParse(userChosen, out int chosenOption) && chosenOption == 1 | chosenOption == 2)
                {
                    return chosenOption;
                }
                Console.WriteLine($"You used the [{userChosen}] key for which there is no assigned property, try again and select the correct option ");
                Console.WriteLine("Press any key to return to the selection menu");
                Console.ReadKey();
            } while (true);
        }

        public static int PassCheckedUserInput(int approvedUserInput) => approvedUserInput;       
    }





}
