using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class PlacaBaseDal
    {
        public static bool Insertar(PlacaBase placaBase)
        {
            bool estado = false;
            string query = @"INSERT INTO PlacaBase (IdProducto, NumeroDims, CapacidadMem, Tamano, SoporteProcesador)
                                       Values(@IdProducto, @NumeroDims, @CapacidadMem, @Tamano, @SoporteProcesador)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Insertar(placaBase as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(query);
                    OperationsSql.AddWithValueString("IdProducto", placaBase.IdProducto);
                    OperationsSql.AddWithValueString("NumeroDims", placaBase.NumeroDims);
                    OperationsSql.AddWithValueString("CapacidadMem", placaBase.CapacidadMem);
                    OperationsSql.AddWithValueString("Tamano", placaBase.Tamano);
                    OperationsSql.AddWithValueString("SoporteProcesador", placaBase.SoporteProcesador.IdSocket);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                }
                estado = true;
            }
            catch (Exception)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Insertar el Producto -> Placa Base");
            }
            finally
            {
                    ProductosDal.cascada = false;
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static PlacaBase Get(Guid idPlacaBase)
        {
            PlacaBase placaBase = null;
            string query = @"SELECT r.IdProducto, r.NumeroDims, r.CapacidadMem, r.Tamano, r.SoporteProcesador as IdSocket, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca, 
                             sp.NombreSocket, sp.Descripcion 
                             FROM PlacaBase r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             INNER JOIN SocketProcesador sp ON sp.IdSocket = r.SoporteProcesador
                             WHERE pro.Eliminado = 0 AND pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idPlacaBase);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    placaBase = PlacaBase.Dictionary_A_PlacaBase(data);
                    // GET SOPORTE PROCESADORES
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener el Producto -> Placa Base");
            }
            finally { OperationsSql.CloseConnection(); }
            return placaBase;
        }
        public static List<PlacaBase> GetAll()
        {
            List<PlacaBase> placaBases = null;
            string query = @"SELECT r.IdProducto, r.NumeroDims, r.CapacidadMem, r.Tamano, r.SoporteProcesador as IdSocket, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca, 
                             sp.NombreSocket, sp.Descripcion 
                             FROM PlacaBase r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
                             INNER JOIN SocketProcesador sp ON sp.IdSocket = r.SoporteProcesador 
                             WHERE pro.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    placaBases = new List<PlacaBase>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        // GET SOPORTE PROCESADORES
                        placaBases.Add(PlacaBase.Dictionary_A_PlacaBase(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener los Productos -> Placa Base");
            }
            finally { OperationsSql.CloseConnection(); }
            return placaBases;
        }
        public static ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            ListaProductos productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM PlacaBase r
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
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener los Productos -> Placa Base");
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
                            INNER JOIN PlacaBase ON PlacaBase.IdProducto = pro.IdProducto
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
                LogError.SetError("Problemas al Obtener las Marcas de los Productos -> Placa Base");
            }
            finally { OperationsSql.CloseConnection(); }
            return listaMarcas;
        }
        public static bool Update(PlacaBase placaBase)
        {
            bool estado = false;
            string queryString = @"UPDATE PlacaBase 
                                   SET NumeroDims = @NumeroDims, 
                                       CapacidadMem = @CapacidadMem, 
                                       Tamano = @Tamano, 
                                       SoporteProcesador = @SoporteProcesador 
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Update(placaBase as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "NumeroDims", placaBase.NumeroDims);
                    OperationsSql.AddWithValueString(parameter: "CapacidadMem", placaBase.CapacidadMem);
                    OperationsSql.AddWithValueString(parameter: "Tamano", placaBase.Tamano);
                    OperationsSql.AddWithValueString(parameter: "SoporteProcesador", placaBase.SoporteProcesador.IdSocket);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else { OperationsSql.ExecuteTransactionCancel(); }
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Actualizar el Producto -> Placa Base");
            }
            finally { ProductosDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM PlacaBase r
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
                LogError.SetError("Problemas al Obtener la Cantidad de los Productos -> Placa Base");
            }
            finally { OperationsSql.CloseConnection(); }
            return cantidad;
        }
    }
}
