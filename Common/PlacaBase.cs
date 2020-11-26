using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class PlacaBase : Producto
    {
        public SocketProcesador SoporteProcesador { get; set; }
        public int NumeroDims { get; set; }
        public int CapacidadMem { get; set; }
        public string Tamano { get; set; }
        public static PlacaBase Dictionary_A_PlacaBase(Dictionary<string, object> data)
        {
            return new PlacaBase()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Tamano = (string)data["Tamano"],
                NumeroDims = (int)data["NumeroDims"],
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
                CapacidadMem = (int)data["CapacidadMem"],
                SoporteProcesador = new SocketProcesador()
                {
                    IdSocket = (int)data["IdSocket"],
                    Descripcion = (string)data["Descripcion"],
                    NombreSocket = (string)data["NombreSocket"]//(ESocketProcesador)Enum.Parse(typeof(ESocketProcesador), (string)data["NombreSocket"])
                }
            };
        }
    }
}
