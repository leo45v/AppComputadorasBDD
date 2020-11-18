using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class RamDal
    {
        public static bool Insertar(Ram ram)
        {
            bool estado = false;
            string query = @"INSERT INTO Ram (IdProducto, Memoria, Frecuencia, Latencia)
                                       Values(@IdProducto, @Memoria, @Frecuencia, @Latencia)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.Insertar(ram as Producto);
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("Memoria", ram.Memoria);
                OperationsSql.AddWithValueString("Frecuencia", ram.Frecuencia);
                OperationsSql.AddWithValueString("Latencia", ram.Latencia);
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
        public static Ram Get(Guid idRam)
        {
            Ram ram = null;
            return ram;
        }
        public static List<Ram> GetAll()
        {
            List<Ram> rams = null;
            string query = @"SELECT r.IdProducto, r.Memoria, r.Frecuencia, r.Latencia
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado 
                             mar.NombreMarca
                             FROM Ram r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             WHERE pro.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    rams = new List<Ram>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        rams.Add(Dictionary_A_Ram(item));
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
        public static List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            List<Producto> rams = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado 
                             mar.NombreMarca
                             FROM Ram r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca " +
                             @"WHERE pro.Eliminado = 0 "+
                             (!(idMarca == 0) || (!(minPrice is null) && !(maxPrice is null)) ? @"AND " : @"") +
                             (!(idMarca == 0) ? @"pro.IdMarca = " + idMarca + " " : @"") +
                             (!(idMarca == 0) && (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                             (!(minPrice is null) && !(maxPrice is null) ? @"pro.PrecioUnidad > @minPrice AND pro.PrecioUnidad < @maxPrice " : @"") +
                             @"ORDER BY pro.Nombre ASC
                             OFFSET " + start + @" ROWS
                             FETCH NEXT " + cant + @" ROWS ONLY";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("minPrice", minPrice);
                OperationsSql.AddWithValueString("maxPrice", maxPrice);
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
        
        public static List<Marca> Get_ListMarcasRam()
        {
            List<Marca> listaMarcas = null;
            string query = @"SELECT Marca.NombreMarca, Marca.IdMarca
                            FROM Marca
                            INNER JOIN Producto pro ON pro.IdMarca = Marca.IdMarca
                            INNER JOIN Ram ON Ram.IdProducto = pro.IdProducto
                            WHERE pro.Eliminado = 0
                            GROUP BY Marca.NombreMarca, Marca.IdMarca
                            ORDER BY Marca.NombreMarca;
";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    listaMarcas = new List<Marca>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        listaMarcas.Add(new Marca()
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
            return listaMarcas;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;

            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Ram r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             WHERE pro.Eliminado = 0 " +
                             (!(idMarca == 0) || (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                             (!(idMarca == 0) ? @"pro.IdMarca = " + idMarca + " " : @"") +
                             (!(idMarca == 0) && (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                            (!(minPrice is null) && !(maxPrice is null) ? @"pro.PrecioUnidad > @minPrice AND pro.PrecioUnidad < @maxPrice " : @"");
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.AddWithValueString("minPrice", minPrice);
                OperationsSql.AddWithValueString("maxPrice", maxPrice);
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
        private static Ram Dictionary_A_Ram(Dictionary<string, object> data)
        {
            return new Ram()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Frecuencia = (int)data["Frecuencia"],
                Imagen = (string)data["Imagen"],
                Latencia = (int)data["Latencia"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Memoria = (int)data["Memoria"],
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                Eliminado = (bool)data["Eliminado"]
            };
        }
    }

}