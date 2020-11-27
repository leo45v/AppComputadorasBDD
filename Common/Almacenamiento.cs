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
        public static Almacenamiento Dictionary_A_Almacenamiento(Dictionary<string, object> data)
        {
            return new Almacenamiento()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Capacidad = (int)data["Capacidad"],
                Escritura = (int)data["Escritura"],
                Imagen = (string)data["Imagen"],
                Lectura = (int)data["Lectura"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Tipo = (string)data["Tipo"],
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"]
            };
        }
    }
}
