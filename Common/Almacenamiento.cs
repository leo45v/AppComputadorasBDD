using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Almacenamiento : Producto
    {
      

        public int Capacidad { get; set; }

        public string Tipo { get; set; }

        public int Escritura { get; set; }

        public int Lectura { get; set; }

    }
}
