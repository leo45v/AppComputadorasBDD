using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Procesador : Producto
    {

        public int FrecuenciaBase { get; set; }
        public int FrecuenciaTurbo { get; set; }
        public int NumeroNucleos { get; set; }
        public int NumeroHilos { get; set; }
        public int Consumo { get; set; }
        public int Litografia { get; set; }
        public SocketProcesador Socket { get; set; }
    }
}
