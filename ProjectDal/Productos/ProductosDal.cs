using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class ProductosDal
    {
        //protected OperationsSql OperationsSql;
        //public ProductosDal()
        //{
        //    OperationsSql = new OperationsSql();
        //}

        protected static void Insertar(Producto producto)
        {
            string queryString = @"INSERT INTO Producto(IdProducto, PrecioUnidad, Imagen, Nombre, Stock, IdMarca, Descontinuado) 
                                                 VALUES(@IdProducto, @PrecioUnidad, @Imagen, @Nombre, @Stock, @IdMarca, @Descontinuado)";
            try
            {
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("IdProducto", producto.IdProducto);
                OperationsSql.AddWithValueString("PrecioUnidad", producto.PrecioUnidad);
                OperationsSql.AddWithValueString("Imagen", producto.Imagen);
                OperationsSql.AddWithValueString("Nombre", producto.Nombre);
                OperationsSql.AddWithValueString("Stock", producto.Stock);
                OperationsSql.AddWithValueString("IdMarca", producto.Marca.IdMarca);
                OperationsSql.AddWithValueString("Descontinuado", producto.Descontinuado);
                OperationsSql.ExecuteBasicCommandWithTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
