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
            Console.Title = "Main Game Window - Checkers Z2J-104";
            var services = new ServiceCollection();
            DependencyInjectionConfig.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var gameManager = serviceProvider.GetRequiredService<GameManager>();
            var gameStatusSender = serviceProvider.GetRequiredService<IGameStatusSender>();

            do
            {
            int chosenOption = UserInputValidator.ValidateAndSetMainMenuInput();
            switch (chosenOption)
            {
                case 1:
                    Console.WriteLine("Start Game");
                        gameStatusSender.SendStatus("Start Game");
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