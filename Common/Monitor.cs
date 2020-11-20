using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Monitor : Producto

    {
        public int Tamano { get; set; }

        public int Frecuencia { get; set; }


        public string Resolucion { get; set; }
        public List<Colores> Colores { get; set; }
        public string Ratio { get; set; }

    }
}
