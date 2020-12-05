using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class RamBrl
    {
        public Ram Get(Guid idRam)
        {
            return RamDal.Get(idRam);
        }
        public bool Add(Ram ram)
        {
            return RamDal.Insertar(ram);
        }
        public bool Insert(Ram ram)
        {
            return RamDal.Insertar(ram);
        }
        public bool Update(Ram ram)
        {
            return RamDal.Update(ram);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return RamDal.Count(idMarca, minPrice, maxPrice);
        }
        public ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return RamDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return RamDal.Get_ListMarcas();
        }

    }
}
