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
       public bool ChosenCorrectLetter(char userInput)
        {
            if (!char.IsDigit(userInput))
            {
                Console.WriteLine("Good its not a digit");
                return true;
            }
            return false;
        }

        public bool ChosenCorrectNumber(char userInput)
        {
            if (char.IsDigit(userInput))
            {
                Console.WriteLine("Good its a digit");
                return true;
            }
            return false;
        }

        public int PassCheckedUserInput(int approvedUserInput) => approvedUserInput;
        
    }


}
