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

        public static void SelectNewPositionForPawn()
        {
            Console.WriteLine($"Specify the new position for the pawn to be moved.");
        }

        public static void WrongPawnChoice()
        {
            Console.WriteLine("There is no pawn under this position.");
        }

        public static void MoveFailed()
        {
            Console.WriteLine("An impossible move was made, Confirm with any key\n");
            Console.ReadKey();
        }

        public static void Gameover()
        {
            Console.WriteLine("End of the game with a result");
            Console.WriteLine($"Player Score : \n\rCPU Score: ");
        }

        public static string ScoreStatusMessage(string player, string playerCpu, int playerScore, int playerCpuScore)
        {
            var messageBody = string.Empty;
            var playerCpuName = "CPU";
            var playerName = "Player";
            var messageId = "Status : ";
            var messageHead = "**** **** GAME STATUS **** ****\n";
            var messageFooter = "\n**** **** STATUS ENDS **** ****";

            if (playerScore > playerCpuScore)
            {
                var message = $"\x1b[32m{playerName} leads\x1b[0m the game\n{messageId}{playerName} Score : {playerScore}\n{messageId}{playerCpuName} Score : {playerCpuScore}";
                messageBody = message;
            }
            else if (playerScore < playerCpuScore)
            {
                var message = $"\u001b[31m{playerCpuName} leads\u001b[0m the game\n{messageId}{playerCpuName} Score : {playerCpuScore}\n{messageId}{playerName} Score : {playerScore}";
                messageBody = message;
            }
            else
            {                
                var message = $"\x1b[33mWe Have draw\u001b[0m\n{messageId}{playerName} {playerScore} - {playerCpuName} {playerCpuScore}";
                messageBody = message;
            }

            return $"{messageHead}{messageId}{messageBody}{messageFooter}";
        }
    }
}
