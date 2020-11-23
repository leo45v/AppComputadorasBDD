using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class PlacaBase : Producto
    {
        public SocketProcesador SoporteProcesador { get; set; }
        public int NumeroDims { get; set; }
        public int CapacidadMem { get; set; }
        public string Tamano { get; set; }

    }
}
