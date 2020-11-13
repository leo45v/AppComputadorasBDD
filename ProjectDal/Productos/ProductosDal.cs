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

        public static void Insertar(Producto producto)
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

        public static Producto Dictionary_A_Producto(Dictionary<string, object> data)
        {
            return new Producto()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Imagen = (string)data["Imagen"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"]
            };
        }
    }
}
