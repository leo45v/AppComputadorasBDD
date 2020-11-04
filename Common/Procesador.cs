using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Procesador : Producto
    {

        public string FrecuenciaBase { get; set; }
        public string FrecuenciaTurbo { get; set; }
        public string NumeroNucleos { get; set; }
        public string NumeroHilos { get; set; }
        public string Consumo { get; set; }
        public string Litografia { get; set; }
 

    }
}
