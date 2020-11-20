using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class AlmacenamientoBrl
    {
        public Almacenamiento Get(Guid idAlmacenamiento)
        {
            return AlmacenamientoDal.Get(idAlmacenamiento);
        }
        public bool Insert(Almacenamiento almacenamiento)
        {
            return AlmacenamientoDal.Insertar(almacenamiento);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return AlmacenamientoDal.Count(idMarca, minPrice, maxPrice);
        }
        public List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return AlmacenamientoDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return AlmacenamientoDal.Get_ListMarcas();
        }
        public bool Update(Almacenamiento almacenamiento)
        {
            return AlmacenamientoDal.Update(almacenamiento);
        }
    }
}
