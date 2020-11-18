using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ProductosBrl
    {
        //public RamBrl rams;
        //public ProductosBrl()
        //{
        //    rams = new RamBrl();
        //}
        public static List<Producto> GetWithRange(int inicio, int cantidad)
        {
            return ProductosDal.GetWithRange(inicio, cantidad);
        }
        public static bool Delete(Guid idProducto)
        {
            return ProductosDal.Delete(idProducto);
        }
        public static int Count
        {
            get
            {
                return ProductosDal.CountAll();
            }
        }
        public static string GetType(Guid idProducto)
        {
            return ProductosDal.GetType(idProducto);
        }
        public static List<Marca> GetMarcas()
        {
            return ProductosDal.GetMarcas();
        }
    }
}

