using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2J_104_Checkers.Interfaces;

namespace Z2J_104_Checkers
{
    public class PawnControllerFactory : IPawnControllerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PawnControllerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPawnController CreatePawnController()
        {
            return _serviceProvider.GetRequiredService<IPawnController>();
        }
    }
}
