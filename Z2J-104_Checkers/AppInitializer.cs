using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers
{
    public static class AppInitializer
    {

        public static IServiceProvider Initializer()
        {
            var services = new ServiceCollection();
            DependencyInjectionConfig.ConfigureServices(services);
            return services.BuildServiceProvider();
        }
        //var services = new ServiceCollection();
        //DependencyInjectionConfig.ConfigureServices(services);
        //    var serviceProvider = services.BuildServiceProvider();
        //var gameManager = serviceProvider.GetService<GameManager>();
    }
}
