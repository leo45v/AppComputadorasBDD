using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class RamBrl 
    {


        //public new void Add(Ram ram)
        //{
        //    if (RamDal.Insertar(ram))
        //    {
        //        base.Add(ram);
        //    }
        //}
        //public new void Remove(Ram ram)
        //{
        //    if (RamDal.Delete_Ram(ram.IdProducto))
        //    {
        //        base.Remove(ram);
        //    }
        //}

        public static List<Producto> GetWithRange(int comienzo, int cantidad)
        {
            return RamDal.GetWithRange(comienzo, cantidad);
        }
        public static int Count()
        {
            return RamDal.Count();
        }

    }
}
