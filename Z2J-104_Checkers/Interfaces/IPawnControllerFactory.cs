﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2J_104_Checkers.Interfaces
{
    public interface IPawnControllerFactory
    {
        IPawnController CreatePawnController();
    }
}
