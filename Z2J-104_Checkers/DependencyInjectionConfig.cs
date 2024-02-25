using Microsoft.Extensions.DependencyInjection;
using Z2J_104_Checkers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.BoardServices;
using Microsoft.Extensions.Logging;

namespace Z2J_104_Checkers
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBoardView, BoardView>();
            services.AddSingleton<BoardBuilder>();
            services.AddSingleton<Board>();

            services.AddSingleton<IPawnControllerFactory, PawnControllerFactory>();
            services.AddSingleton<IPawnController, PawnController>();
            services.AddTransient(typeof(Lazy<>), typeof(LazilyResolved<>));

            services.AddSingleton<GameManager>();
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IGameStateController, GameStateController>();

            services.AddSingleton<IMovementAnalyzer, MovementAnalyzer>();
            services.AddSingleton<ICPUChoiceAnalyzer, CPUChoiceAnalyzer>();

            services.AddSingleton<Pawn>();
            services.AddSingleton<CpuPawn>();
            services.AddSingleton<PlayerPawn>();
            services.AddSingleton<List<Pawn>>(new List<Pawn>());

            services.AddSingleton<IGameStatusSender, GameStatusSender>();
        }
    }
}
