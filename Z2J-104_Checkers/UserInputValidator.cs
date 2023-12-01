using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public class UserInputValidator
    {
       public (int, bool)IsChosenCorrectLetter(char userInput)
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

        public (int, bool) IsChosenCorrectNumber(char userInput)
        {
            bool isWrongValue = true;
            if (char.IsDigit(userInput) && (userInput >= 1 && userInput <= 8))
            {
                Console.WriteLine("Good its a digit");
                return (--userInput, !isWrongValue);
            }
            else
            {
                return (-1, isWrongValue);
            }
        }

        public int PassCheckedUserInput(int approvedUserInput) => approvedUserInput;
        
    }


}
