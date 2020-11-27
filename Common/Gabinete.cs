using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Gabinete : Producto
    {
        public int Altura { get; set; }
        public int Largo { get; set; }
        public List<Colores> Colores { get; set; }
        public decimal Peso { get; set; }
        public static Gabinete Dictionary_A_Gabinete(Dictionary<string, object> data)
        {
            return new Gabinete()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Altura = (int)data["Altura"],
                Peso = (decimal)data["Peso"],
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
