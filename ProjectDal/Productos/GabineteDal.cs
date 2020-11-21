using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class GabineteDal
    {
        public static bool Insertar(Gabinete gabinete)
        {
            bool estado = false;
            string query = @"INSERT INTO Gabinete (IdProducto, Altura, Peso, Largo)
                                       Values(@IdProducto, @Altura, @Peso, @Largo)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Insertar(gabinete as Producto))
                {
                    ProductosDal.cascada = false;
                    foreach (Colores item in gabinete.Colores)
                    {
                        ProductosDal.InsertarColores(gabinete.IdProducto, item.IdColor);
                    }
                    OperationsSql.CreateBasicCommandWithTransaction(query);
                    OperationsSql.AddWithValueString("IdProducto", gabinete.IdProducto);
                    OperationsSql.AddWithValueString("Altura", gabinete.Altura);
                    OperationsSql.AddWithValueString("Peso", gabinete.Peso);
                    OperationsSql.AddWithValueString("Largo", gabinete.Largo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                }
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

        public static Gabinete Get(Guid idGabinete)
        {
            Gabinete gabinete = null;
            string query = @"SELECT r.IdProducto, r.Altura, r.Peso, r.Largo,  
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Gabinete r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             WHERE pro.Eliminado = 0 AND pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idGabinete);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    gabinete = Dictionary_A_Gabinete(data);
                    gabinete.Colores = ProductosDal.GetColores(idGabinete);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return gabinete;
        }
        public static List<Gabinete> GetAll()
        {
            List<Gabinete> gabinetes = null;
            string query = @"SELECT r.IdProducto, r.Altura, r.Peso, r.Largo,  
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Gabinete r
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
                    gabinetes = new List<Gabinete>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        Gabinete gabinete = Dictionary_A_Gabinete(item);
                        gabinete.Colores = ProductosDal.GetColores(gabinete.IdProducto);
                        gabinetes.Add(gabinete);
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return gabinetes;
        }
        public static List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            List<Producto> productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Gabinete r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca " +
                             @"WHERE pro.Eliminado = 0 " +
                             (!(idMarca == 0) || (!(minPrice is null) && !(maxPrice is null)) ? @"AND " : @"") +
                             (!(idMarca == 0) ? @"pro.IdMarca = " + idMarca + " " : @"") +
                             (!(idMarca == 0) && (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                             (!(minPrice is null) && !(maxPrice is null) ? @"pro.PrecioUnidad > " + minPrice + " AND pro.PrecioUnidad < " + maxPrice + " " : @"") +
                             @"ORDER BY pro.Nombre ASC
                             OFFSET " + start + @" ROWS
                             FETCH NEXT " + cant + @" ROWS ONLY";
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
        public static List<Marca> Get_ListMarcas()
        {
            List<Marca> listaMarcas = null;
            string query = @"SELECT Marca.NombreMarca, Marca.IdMarca
                            FROM Marca
                            INNER JOIN Producto pro ON pro.IdMarca = Marca.IdMarca
                            INNER JOIN Gabinete ON Gabinete.IdProducto = pro.IdProducto
                            WHERE pro.Eliminado = 0
                            GROUP BY Marca.NombreMarca, Marca.IdMarca
                            ORDER BY Marca.NombreMarca";
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
        public static bool Update(Gabinete gabinete)
        {
            bool estado = false;
            string queryString = @"UPDATE Gabinete 
                                   SET Altura = @Altura, 
                                       Peso = @Peso, 
                                       Largo = @Largo 
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Update(gabinete as Producto))
                {
                    List<Colores> coloresExistentes = ProductosDal.GetColores(gabinete.IdProducto);
                    foreach (Colores item in gabinete.Colores)
                    {
                        if (coloresExistentes is null || !coloresExistentes.Exists(x => x.Nombre == item.Nombre))
                        {
                            ProductosDal.InsertarColores(gabinete.IdProducto, item.IdColor);
                        }
                    }
                    if (!(coloresExistentes is null))
                    {
                        foreach (Colores item in coloresExistentes)
                        {
                            if (!gabinete.Colores.Exists(x => x.Nombre == item.Nombre)) // ROJO, BLANCO
                            {
                                ProductosDal.DeleteColorProduct(gabinete.IdProducto, item.IdColor);
                            }
                        }
                    }
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "IdProducto", gabinete.IdProducto);
                    OperationsSql.AddWithValueString(parameter: "Altura", gabinete.Altura);
                    OperationsSql.AddWithValueString(parameter: "Peso", gabinete.Peso);
                    OperationsSql.AddWithValueString(parameter: "Largo", gabinete.Largo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else { OperationsSql.ExecuteTransactionCancel(); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { ProductosDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Gabinete r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             WHERE pro.Eliminado = 0 " +
                             (!(idMarca == 0) || (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                             (!(idMarca == 0) ? @"pro.IdMarca = " + idMarca + " " : @"") +
                             (!(idMarca == 0) && (!(minPrice is null) && !(maxPrice is null)) ? @" AND " : @"") +
                             (!(minPrice is null) && !(maxPrice is null) ? @"pro.PrecioUnidad > " + minPrice + " AND pro.PrecioUnidad < " + maxPrice + " " : @"");
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
        private static Gabinete Dictionary_A_Gabinete(Dictionary<string, object> data)
        {
            return new Gabinete()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                Altura = (int)data["Altura"],
                Peso = (decimal)data["Peso"],
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
