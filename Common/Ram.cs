using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Ram : Producto
    {
        public int Memoria { get; set; }

        public int Frecuencia { get; set; }

        public int Latencia { get; set; }
        public static Ram Dictionary_A_Ram(Dictionary<string, object> data)
        {
            return new Ram()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Frecuencia = (int)data["Frecuencia"],
                Imagen = (string)data["Imagen"],
                Latencia = (int)data["Latencia"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Memoria = (int)data["Memoria"],
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"]
            };
        }
    }
}
