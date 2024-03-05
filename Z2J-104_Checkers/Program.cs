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
                        gameManager.InitGame();
                    break;
                case 2:
                        Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Chosen Wrong Option, Try again.");
                    break;
            }
            } while (true);
        }
    }
}