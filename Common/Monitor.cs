using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Monitor : Producto
    {
        public int Tamano { get; set; }
        public int Frecuencia { get; set; }
        public Resolucion Resolucion { get; set; }
        public List<Colores> Colores { get; set; }
        public Ratio Ratio { get; set; }

    }
}
