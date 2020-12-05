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
        public decimal PrecioUnidad { get; set; }

        public string Imagen { get; set; }

        public short Stock { get; set; }

        public bool Descontinuado { get; set; }
        public Marca Marca { get; set; }
        public bool Eliminado { get; set; }
        public static Producto Dictionary_A_Producto(Dictionary<string, object> data)
        {
            return new Producto()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Imagen = (string)data["Imagen"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"]
            };
        }
    }
}
