using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
   public  class Grafica : Producto
    {
        public int Vram { get; set; }

        public int FrecuenciaBase { get; set; }

        public int FrecuenciaTurbo { get; set; }
        public string TipoMemoria { get; set; }
    }
}
