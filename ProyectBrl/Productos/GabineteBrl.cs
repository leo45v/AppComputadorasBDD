using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class GabineteBrl
    {
        public Gabinete Get(Guid idGabinete)
        {
            return GabineteDal.Get(idGabinete);
        }
        public bool Insert(Gabinete gabinete)
        {
            return GabineteDal.Insertar(gabinete);
        }
        public bool Update(Gabinete gabinete)
        {
            return GabineteDal.Update(gabinete);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return GabineteDal.Count(idMarca, minPrice, maxPrice);
        }
        public List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return GabineteDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return GabineteDal.Get_ListMarcas();
        }
    }
}
