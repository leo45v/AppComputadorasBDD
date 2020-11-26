using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Configuraciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ConfiguracionesBrl
    {
        public static RatioBrl Ratio { get; private set; } = new RatioBrl();
        public static ResolucionBrl Resolucion { get; private set; } = new ResolucionBrl();
        public static ColoresBrl Colores { get; private set; } = new ColoresBrl();
        public static MarcasBrl Marca { get; private set; } = new MarcasBrl();
        public static SocketsBrl Socket { get; private set; } = new SocketsBrl();
    }
}
