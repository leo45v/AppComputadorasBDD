using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Gabinete : Producto
    {

        public int Tamano { get; set; }
        public int Altura { get; set; }
        public int Largo { get; set; }
        public List<Colores> Colores { get; set; }
        public decimal Peso { get; set; }
        public string Ratio { get; set; }


    }
}
