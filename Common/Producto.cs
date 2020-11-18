using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Producto
    {
        public Guid IdProducto { get; set; }

        public string Nombre { get; set; }
        public decimal PrecioUnidad { get; set; }//SqlMoney

        public string Imagen { get; set; }

        public short Stock { get; set; }

        public bool Descontinuado { get; set; }
        public Marca Marca { get; set; }
        public bool Eliminado { get; set; }

    }
}
