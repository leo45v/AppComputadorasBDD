﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ClientsBrl
    {
        public static void Insertar(Cliente cliente)
        {
            ClienteDal.Insertar(cliente);
        }
    }
}