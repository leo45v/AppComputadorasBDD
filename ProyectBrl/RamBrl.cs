using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class RamBrl
    {
        public static bool Add(Ram ram)
        {
            return RamDal.Insertar(ram);
        }
        //public new void Remove(Ram ram)
        //{
        //    if (RamDal.Delete_Ram(ram.IdProducto))
        //    {
        //        base.Remove(ram);
        //    }
        //}

        public static List<Producto> GetWithRange(int comienzo, int cantidad, int? idMarca, double? minPrice, double? maxPrice)
        {
            return RamDal.GetWithRange(comienzo, cantidad, idMarca, minPrice, maxPrice);
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return RamDal.Count(idMarca, minPrice, maxPrice);
        }
        public static List<Marca> Get_ListMarcasRam()
        {
            return RamDal.Get_ListMarcasRam();
        }

    }
}
