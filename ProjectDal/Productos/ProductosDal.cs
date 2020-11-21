using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

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

        public static bool Insertar(Producto producto)
        {
            bool estado = true;
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
        public static List<Producto> GetWithRange(int inicio, int cantidad = 10)
        {
            List<Producto> productos = null;
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
                    productos = new List<Producto>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        productos.Add(ProductosDal.Dictionary_A_Producto(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return productos;
        }
        public static List<Producto> GetWithRangeWithFillter(int inicio, int cantidad, string productName, Marca marca, ETipoProducto tipoProduct)
        {
            List<Producto> productos = null;
            string query = @"SELECT pro.IdProducto, pro.Eliminado, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado,
                             mar.NombreMarca
                             FROM Producto pro 
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
                             INNER JOIN ProductoTipo pt ON pt.IdProducto = pro.IdProducto
                             WHERE pro.Eliminado = 0 ";
            if (!(productName is null))
            {
                query += @" AND pro.Nombre LIKE '%' + @NombreProducto + '%' ";
                OperationsSql.AddWithValueString("NombreProducto", productName);
            }
            if (!(marca is null))
            {
                query += @" AND pro.IdMarca = @IdMarca ";
                OperationsSql.AddWithValueString("IdMarca", marca.IdMarca);
            }
            if (tipoProduct != ETipoProducto.None)
            {
                query += @" AND pt.Tipo = @TipoProducto ";
                OperationsSql.AddWithValueString("TipoProducto", tipoProduct.ToString());
            }
            query += @" ORDER BY pro.Nombre ASC
                             OFFSET " + inicio + @" ROWS
                             FETCH NEXT " + cantidad + @" ROWS ONLY";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    productos = new List<Producto>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        productos.Add(ProductosDal.Dictionary_A_Producto(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return productos;
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
        public static int CountWithFilter(string productName, Marca marca, ETipoProducto tipoProduct)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Producto pro
                             INNER JOIN ProductoTipo pt ON pt.IdProducto = pro.IdProducto 
                             WHERE pro.Eliminado = 0";
            if (!(String.IsNullOrWhiteSpace(productName)))
            {
                query += @" AND pro.Nombre LIKE '%' + @NombreProducto + '%' ";
                OperationsSql.AddWithValueString("NombreProducto", productName);
            }
            if (!(marca is null))
            {
                query += @" AND pro.IdMarca = @IdMarca ";
                OperationsSql.AddWithValueString("IdMarca", marca.IdMarca);
            }
            if (tipoProduct != ETipoProducto.None)
            {
                query += @" AND pt.Tipo = @TipoProducto ";
                OperationsSql.AddWithValueString("TipoProducto", tipoProduct.ToString());
            }
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
        public static ETipoProducto GetType(Guid idProducto)
        {
            ETipoProducto tipo = ETipoProducto.None;
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
                    tipo = (ETipoProducto)Enum.Parse(typeof(ETipoProducto), (string)data["Tipo"]);

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
        public static bool InsertarColores(Guid idProducto, int idColor)
        {
            bool estado = false;
            string query = @"INSERT INTO ProductoColor (IdProducto, IdColor)
                                                 Values(@IdProducto, @IdColor)";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                OperationsSql.AddWithValueString("IdColor", idColor);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                estado = true;
            }
            catch (Exception)
            {
                throw;
            }
            return estado;
        }
        public static List<Colores> GetColores(Guid idProducto)
        {
            List<Colores> colores = null;
            string query = @"SELECT co.IdColor, co.Nombre 
                             FROM Colores co 
                             INNER JOIN ProductoColor pc ON pc.IdColor = co.IdColor 
                             WHERE pc.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProducto);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    colores = new List<Colores>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        colores.Add(new Colores()
                        {
                            IdColor = (int)item["IdColor"],
                            Nombre = (string)item["Nombre"]
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
            return colores;
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
    }
}