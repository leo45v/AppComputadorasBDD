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
        public int Consumo { get; set; }
        public static Grafica Dictionary_A_Grafica(Dictionary<string, object> data)
        {
            return new Grafica()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Vram = (int)data["Vram"],
                FrecuenciaBase = (int)data["FrecuenciaBase"],
                Imagen = (string)data["Imagen"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"],
                FrecuenciaTurbo = (int)data["FrecuenciaTurbo"],
                TipoMemoria = (string)data["TipoMemoria"],
                Consumo = (int)data["Consumo"]
            };
        }
    }
}
