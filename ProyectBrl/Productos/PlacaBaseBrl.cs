using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class PlacaBaseBrl
    {
        public PlacaBase Get(Guid idPlacaBase)
        {
            return PlacaBaseDal.Get(idPlacaBase);
        }
        public bool Insert(PlacaBase placaBase)
        {
            return PlacaBaseDal.Insertar(placaBase);
        }
        public bool Update(PlacaBase placaBase)
        {
            return PlacaBaseDal.Update(placaBase);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return PlacaBaseDal.Count(idMarca, minPrice, maxPrice);
        }
        public ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return PlacaBaseDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return PlacaBaseDal.Get_ListMarcas();
        }
    }
}
