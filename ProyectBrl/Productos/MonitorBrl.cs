using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class MontiorBrl
    {
        public Monitor Get(Guid idMonitor)
        {
            return MonitorDal.Get(idMonitor);
        }
        public bool Insert(Monitor monitor)
        {
            return MonitorDal.Insertar(monitor);
        }
        public bool Update(Monitor monitor)
        {
            return MonitorDal.Update(monitor);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return MonitorDal.Count(idMarca, minPrice, maxPrice);
        }
        public List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return MonitorDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return MonitorDal.Get_ListMarcas();
        }
    }
}
