using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
   public  class Grafica : Producto
    {
        public string Vram { get; set; }

        public string FrecuenciaBase { get; set; }

        public string FrecuenciaTurbo { get; set; }
        public string TipoMemoria { get; set; }
    }
}
