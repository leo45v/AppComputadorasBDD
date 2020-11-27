using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Fuente : Producto
    {
        public int Potencia { get; set; }
        public ECertificacion Certificacion { get; set; }
        public static Fuente Dictionary_A_Fuente(Dictionary<string, object> data)
        {
            return new Fuente()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Potencia = (int)data["Potencia"],
                Certificacion = (ECertificacion)(int)data["Certificacion"],//Enum.Parse(typeof(ECertificacion), ),
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
