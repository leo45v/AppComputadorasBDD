using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.ComputerBuild;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.ComputadoraBuild
{
    public class ComputadoraBuildBrl
    {
        public static double Presupuesto
        {
            get
            {
                return ComputerBuildDal.Presupuesto;
            }
            set
            {
                ComputerBuildDal.Presupuesto = value;
            }
        }
        public static List<Computadora> GetComputersBuild(Requirements.TipoComputer tipoComputer)
        {
            return ComputerBuildDal.GetComputersBuild(tipoComputer);
        }
    }
}
