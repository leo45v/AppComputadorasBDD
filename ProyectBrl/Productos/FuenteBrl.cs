using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class FuenteBrl
    {
        public Fuente Get(Guid idFuente)
        {
            return FuenteDal.Get(idFuente);
        }
        public bool Insert(Fuente fuente)
        {
            return FuenteDal.Insertar(fuente);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return FuenteDal.Count(idMarca, minPrice, maxPrice);
        }
        public ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return FuenteDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return FuenteDal.Get_ListMarcas();
        }
        public bool Update(Fuente fuente)
        {
            return FuenteDal.Update(fuente);
        }
    }
}
