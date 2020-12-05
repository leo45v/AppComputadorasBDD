using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class ProcesadorBrl
    {
        public bool Insert(Procesador procesador)
        {
          return  ProcesadorDal.Insertar(procesador);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return ProcesadorDal.Count(idMarca, minPrice, maxPrice);
        }
        public ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return ProcesadorDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return ProcesadorDal.Get_ListMarcas();
        }
        public Procesador Get(Guid idProcesador)
        {
            return ProcesadorDal.Get(idProcesador);
        }
        public bool Update(Procesador procesador)
        {
            return ProcesadorDal.Update(procesador);
        }
    }
}
