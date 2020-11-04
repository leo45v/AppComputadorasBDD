using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Ram : Producto
    {
        public int Memoria { get; set; }

        public int Frecuencia { get; set; }

        public int Latencia { get; set; }

    }
}
