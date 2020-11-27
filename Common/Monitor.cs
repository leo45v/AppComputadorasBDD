using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Monitor : Producto
    {
        public int Tamano { get; set; }
        public int Frecuencia { get; set; }
        public Resolucion Resolucion { get; set; }
        public List<Colores> Colores { get; set; }
        public Ratio Ratio { get; set; }
        public static Monitor Dictionary_A_Monitor(Dictionary<string, object> data)
        {
            return new Monitor()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Tamano = (int)data["Tamano"],
                Frecuencia = (int)data["Frecuencia"],
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
                Resolucion = new Resolucion()
                {
                    IdResolucion = (byte)data["IdResolucion"],
                    NombreResolucion = (string)data["NombreResolucion"]
                },
                Ratio = new Ratio()
                {
                    IdRatio = (byte)data["IdRatio"],
                    NombreRatio = (string)data["NombreRatio"]
                }
            };
        }
    }
}
