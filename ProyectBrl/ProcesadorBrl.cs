using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ProcesadorBrl
    {
        public static void Insertar(Procesador procesador)
        {
            ProcesadorDal.Insertar(procesador);
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return ProcesadorDal.Count(idMarca, minPrice, maxPrice);
        }
        public static bool Delete(Guid idProcesador)
        {
            return ProcesadorDal.Delete(idProcesador);
        }
        public static List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return ProcesadorDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public static List<Marca> Get_ListMarcas()
        {
            return ProcesadorDal.Get_ListMarcas();
        }
        public static Procesador Get(Guid idProcesador)
        {
            return ProcesadorDal.Get(idProcesador);
        }
    }
}
