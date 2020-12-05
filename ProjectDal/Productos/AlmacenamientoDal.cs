using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class AlmacenamientoDal
    {
        public static bool Insertar(Almacenamiento almacenamiento)
        {
            bool estado = false;
            string query = @"INSERT INTO Almacenamiento (IdProducto, Capacidad, Escritura, Lectura, Tipo)
                                       Values(@IdProducto, @Capacidad, @Escritura, @Lectura, @Tipo)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Insertar(almacenamiento as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(query);
                    OperationsSql.AddWithValueString("IdProducto", almacenamiento.IdProducto);
                    OperationsSql.AddWithValueString("Capacidad", almacenamiento.Capacidad);
                    OperationsSql.AddWithValueString("Escritura", almacenamiento.Escritura);
                    OperationsSql.AddWithValueString("Lectura", almacenamiento.Lectura);
                    OperationsSql.AddWithValueString("Tipo", almacenamiento.Tipo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                }
                estado = true;
            }
            catch (Exception ex)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Insertar un Producto -> Almacenamiento", ex);
            }
            finally
            {
                ProductosDal.cascada = false;
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static Almacenamiento Get(Guid idAlmacenamiento)
        {
            Almacenamiento almacenamiento = null;
            string query = @"SELECT r.IdProducto, r.Capacidad, r.Escritura, r.Lectura, r.Tipo, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Almacenamiento r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             WHERE pro.Eliminado = 0 AND pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idAlmacenamiento);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    almacenamiento = Almacenamiento.Dictionary_A_Almacenamiento(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener un Producto -> Almacenamiento", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return almacenamiento;
        }
        public static List<Almacenamiento> GetAll()
        {
            List<Almacenamiento> almacenamientos = null;
            string query = @"SELECT r.IdProducto, r.Capacidad, r.Escritura, r.Lectura, r.Tipo, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Almacenamiento r
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
                    almacenamientos = new List<Almacenamiento>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        almacenamientos.Add(Almacenamiento.Dictionary_A_Almacenamiento(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener todos los Productos -> Almacenamiento", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return almacenamientos;
        }
        public static ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            ListaProductos productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Almacenamiento r
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
                    productos = new ListaProductos();
                    foreach (Dictionary<string, object> item in data)
                    {
                        productos.Add(Producto.Dictionary_A_Producto(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener los Productos -> Almacenamiento", ex);
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
                            INNER JOIN Almacenamiento ON Almacenamiento.IdProducto = pro.IdProducto
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
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener lista de Marcas -> Almacenamiento", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return listaMarcas;
        }
        public static bool Update(Almacenamiento almacenamiento)
        {
            bool estado = false;
            string queryString = @"UPDATE Almacenamiento 
                                   SET Capacidad = @Capacidad, 
                                       Escritura = @Escritura, 
                                       Lectura = @Lectura, 
                                       Tipo = @Tipo
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Update(almacenamiento as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "Capacidad", almacenamiento.Capacidad);
                    OperationsSql.AddWithValueString(parameter: "Escritura", almacenamiento.Escritura);
                    OperationsSql.AddWithValueString(parameter: "Lectura", almacenamiento.Lectura);
                    OperationsSql.AddWithValueString(parameter: "Tipo", almacenamiento.Tipo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else { OperationsSql.ExecuteTransactionCancel(); }
            }
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Actualizar el Producto -> Almacenamiento", ex);
            }
            finally { ProductosDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Almacenamiento r
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
            catch (Exception ex)
            {
                LogError.SetError("Problemas al Obtener la cantidad de Productos -> Almacenamiento", ex);
            }
            finally { OperationsSql.CloseConnection(); }
            return cantidad;
        }
    }
}
