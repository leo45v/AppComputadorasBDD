﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Personas;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class AdministradorBrl
    {
        public static Administrador GetAdministradorByIdUsuario(Guid idUsuario)
        {
            return AdministradorDal.Get_Administrador_By_IdUsuario(idUsuario);
        }
    }
}