using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Z2J_104_Checkers.BoardServices;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            DependencyInjectionConfig.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var gameManager = serviceProvider.GetRequiredService<GameManager>();
            //  MenuView _menuView = new MenuView();


            do
            {
            int chosenOption = UserInputValidator.ValidateAndSetMainMenuInput();
            switch (chosenOption)
            {
                case 1:
                    Console.WriteLine("Start Game");
                        gameManager.InitGame();
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