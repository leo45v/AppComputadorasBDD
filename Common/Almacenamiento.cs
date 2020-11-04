using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Almacenamiento : Producto
    {
      

        public string Capacidad { get; set; }

        public string Tipo { get; set; }

        public string Escritura { get; set; }

        public string  Lectura { get; set; }

    }
}
