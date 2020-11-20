using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos
{
    public class TarjetaGraficaBrl
    {
        public Grafica Get(Guid idTarjetaGrafica)
        {
            return TarjetaGraficaDal.Get(idTarjetaGrafica);
        }
        public bool Insert(Grafica tarjetaGrafica)
        {
            return TarjetaGraficaDal.Insertar(tarjetaGrafica);
        }
        public bool Update(Grafica tarjetaGrafica)
        {
            return TarjetaGraficaDal.Update(tarjetaGrafica);
        }
        public int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            return TarjetaGraficaDal.Count(idMarca, minPrice, maxPrice);
        }
        public List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            return TarjetaGraficaDal.GetWithRange(start, cant, idMarca, minPrice, maxPrice);
        }
        public List<Marca> Get_ListMarcas()
        {
            return TarjetaGraficaDal.Get_ListMarcas();
        }
    }
}
