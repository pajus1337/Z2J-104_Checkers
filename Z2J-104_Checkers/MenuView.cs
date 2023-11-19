using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    internal class MenuView
    {
        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to a simple game: checkers");
            Console.WriteLine($"Some important information before you start playing,\n-The size of the board is 8x8\n- The white squares are marked 'O'\n- The black squares are marked 'X'\n");
            Console.WriteLine($"- Computer pawns are marked 'C'\n- Player pawns are marked 'U'\nHave a nice game\n");
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
    }
}
