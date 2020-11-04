using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    class PlacaBase : Producto
    {
        public string SoporteProcesador { get; set; }
        public string NumeroDims { get; set; }
        public string CapacidadMem { get; set; }
        public string Tamano { get; set; }

    }
}
