using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common
{
    public class Procesador : Producto
    {
        public int FrecuenciaBase { get; set; }
        public int FrecuenciaTurbo { get; set; }
        public int NumeroNucleos { get; set; }
        public int NumeroHilos { get; set; }
        public int Consumo { get; set; }
        public int Litografia { get; set; }
        public SocketProcesador Socket { get; set; }
        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                base.IdProducto, base.Nombre, base.Stock, base.PrecioUnidad, base.Marca,
                Consumo, FrecuenciaBase, NumeroNucleos, NumeroHilos);
        }
        public static Procesador Dictionary_A_Procesador(Dictionary<string, object> data)
        {
            return new Procesador()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                FrecuenciaBase = (int)data["FrecuenciaBase"],
                Consumo = (int)data["Consumo"],
                Imagen = (string)data["Imagen"],
                FrecuenciaTurbo = (int)data["FrecuenciaTurbo"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Litografia = (int)data["Litografia"],
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                NumeroHilos = (int)data["NumeroHilos"],
                NumeroNucleos = (int)data["NumeroNucleos"],
                Eliminado = (bool)data["Eliminado"],
                Socket = new SocketProcesador()
                {
                    IdSocket = (int)data["IdSocket"],
                    Descripcion = (string)data["Descripcion"],
                    NombreSocket = (string)data["NombreSocket"]//(ESocketProcesador)Enum.Parse(typeof(ESocketProcesador), (string)data["NombreSocket"])
                }
            };
        }
    }
}
