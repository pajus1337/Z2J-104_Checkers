using System.Threading.Channels;

namespace Z2J_104_Checkers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuView menuView = new MenuView();
            GameManager gameManager = new GameManager(menuView);

            do
            {
            int chosenOption = menuView.MainMenuOptionsView();
            switch (chosenOption)
            {
                case 1:
                    Console.WriteLine("Start Game");
                    gameManager.test();
                    break;
                case 2:
                    Console.WriteLine("Option 2");
                    break;
                default:
                    Console.WriteLine("def");
                    break;
            }
            } while (true);
        }
    }
}