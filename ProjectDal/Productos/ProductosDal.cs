using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class ProductosDal
    {
        public static bool cascada = false;
        //protected OperationsSql OperationsSql;
        //public ProductosDal()
        //{
        //    OperationsSql = new OperationsSql();
        //}

        public static void Insertar(Producto producto)
        {
            string queryString = @"INSERT INTO Producto(IdProducto, PrecioUnidad, Imagen, Nombre, Stock, IdMarca, Descontinuado, Eliminado) 
                                                 VALUES(@IdProducto, @PrecioUnidad, @Imagen, @Nombre, @Stock, @IdMarca, @Descontinuado, @Eliminado)";
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
                OperationsSql.AddWithValueString("Eliminado", producto.Eliminado);
                OperationsSql.ExecuteBasicCommandWithTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Producto> GetWithRange(int inicio, int cantidad = 10)
        {
            List<Producto> rams = null;
            string query = @"SELECT pro.IdProducto, pro.Eliminado, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado,
                             mar.NombreMarca
                             FROM Producto pro 
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
                             WHERE pro.Eliminado = 0 " +
                             @"ORDER BY pro.Nombre ASC
                             OFFSET " + inicio + @" ROWS
                             FETCH NEXT " + cantidad + @" ROWS ONLY";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    rams = new List<Producto>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        rams.Add(ProductosDal.Dictionary_A_Producto(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return rams;
        }
        public static bool Update(Producto producto)
        {
            bool estado = false;
            string queryString = @"UPDATE Producto 
                                   SET PrecioUnidad = @PrecioUnidad, 
                                       Imagen = @Imagen, 
                                       Nombre = @Nombre, 
                                       Stock = @Stock,
                                       IdMarca = @IdMarca,
                                       Descontinuado = @Descontinuado,
                                       Eliminado = @Eliminado
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("PrecioUnidad", producto.PrecioUnidad);
                OperationsSql.AddWithValueString("Imagen", producto.Imagen);
                OperationsSql.AddWithValueString("Nombre", producto.Nombre);
                OperationsSql.AddWithValueString("Stock", producto.Stock);
                OperationsSql.AddWithValueString("IdMarca", producto.Marca.IdMarca);
                OperationsSql.AddWithValueString("Descontinuado", producto.Descontinuado);
                OperationsSql.AddWithValueString("Eliminado", producto.Eliminado);
                OperationsSql.AddWithValueString("IdProducto", producto.IdProducto);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                if (!cascada) { OperationsSql.ExecuteTransactionCommit(); }
                estado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (!cascada) { OperationsSql.CloseConnection(); } }
            return estado;
        }
        public static int CountAll()
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Producto pro
                             WHERE pro.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    cantidad = (int)data["Cantidad"];

                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return cantidad;
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
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"]
            };
        }
        public static bool Delete(Guid idProducto)
        {
            bool estado = false;
            string query = @"UPDATE Producto SET Eliminado = 1 WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
                estado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static string GetType(Guid idProducto)
        {
            string tipo = null;
            string query = @"SELECT Tipo
                             FROM ProductoTipo pro
                             WHERE pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    tipo = (string)data["Tipo"];

                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return tipo;
        }

        public static List<Marca> GetMarcas()
        {
            List<Marca> marcas = null;
            string query = @"SELECT IdMarca, NombreMarca
                             FROM Marca";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    marcas = new List<Marca>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        marcas.Add(new Marca()
                        {
                            IdMarca = (byte)item["IdMarca"],
                            NombreMarca = (string)item["NombreMarca"]
                        });
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return marcas;
        }
        //public static bool Delete(Guid idProducto)
        //{
        //    bool estado = false;
        //    string query = @"DELETE FROM Producto WHERE IdProducto = @IdProducto";
        //    try
        //    {
        //        OperationsSql.OpenConnection();
        //        OperationsSql.CreateBasicCommandWithTransaction(query);
        //        OperationsSql.AddWithValueString("IdProducto", idProducto);
        //        OperationsSql.ExecuteBasicCommandWithTransaction();
        //        OperationsSql.ExecuteTransactionCommit();
        //        estado = true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        OperationsSql.CloseConnection();
        //    }
        //    return estado;
        //}
    }
}
