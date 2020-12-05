using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class TarjetaGraficaDal
    {
        public static bool Insertar(Grafica tarjetaGrafica)
        {
            bool estado = false;
            string query = @"INSERT INTO tarjetaGrafica (IdProducto, Vram, FrecuenciaBase, FrecuenciaTurbo, TipoMemoria, Consumo)
                                       Values(@IdProducto, @Vram, @FrecuenciaBase, @FrecuenciaTurbo, @TipoMemoria, @Consumo)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Insertar(tarjetaGrafica as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(query);
                    OperationsSql.AddWithValueString("IdProducto", tarjetaGrafica.IdProducto);
                    OperationsSql.AddWithValueString("Vram", tarjetaGrafica.Vram);
                    OperationsSql.AddWithValueString("FrecuenciaBase", tarjetaGrafica.FrecuenciaBase);
                    OperationsSql.AddWithValueString("FrecuenciaTurbo", tarjetaGrafica.FrecuenciaTurbo);
                    OperationsSql.AddWithValueString("TipoMemoria", tarjetaGrafica.TipoMemoria);
                    OperationsSql.AddWithValueString("Consumo", tarjetaGrafica.Consumo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                }
                estado = true;
            }
            catch (Exception)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Insertar el Producto -> Tarjeta de Video");
            }
            finally
            {
                    ProductosDal.cascada = false;
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static Grafica Get(Guid idTarjetaGrafica)
        {
            Grafica tarjetaGrafica = null;
            string query = @"SELECT r.IdProducto, r.Vram, r.FrecuenciaBase, r.FrecuenciaTurbo, r.TipoMemoria, r.Consumo,   
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM TarjetaGrafica r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             WHERE pro.Eliminado = 0 AND pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idTarjetaGrafica);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    tarjetaGrafica = Grafica.Dictionary_A_Grafica(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Insertar el Producto -> Tarjeta de Video");
            }
            finally { OperationsSql.CloseConnection(); }
            return tarjetaGrafica;
        }
        public static List<Grafica> GetAll()
        {
            List<Grafica> graficas = null;
            string query = @"SELECT r.IdProducto, r.Vram, r.FrecuenciaBase, r.FrecuenciaTurbo, r.TipoMemoria, r.Consumo,    
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM TarjetaGrafica r
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
                    graficas = new List<Grafica>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        // GET SOPORTE PROCESADORES
                        graficas.Add(Grafica.Dictionary_A_Grafica(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Insertar el Producto -> Tarjeta de Video");
            }
            finally { OperationsSql.CloseConnection(); }
            return graficas;
        }
        public static ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            ListaProductos productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM TarjetaGrafica r
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
                LogError.SetError("Problemas al Obtener los Productos -> Tarjeta de Video");
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
                            INNER JOIN TarjetaGrafica ON TarjetaGrafica.IdProducto = pro.IdProducto
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
                LogError.SetError("Problemas al Obtener los Productos -> Tarjeta de Video");
            }
            finally { OperationsSql.CloseConnection(); }
            return listaMarcas;
        }
        public static bool Update(Grafica tarjetaGrafica)
        {
            bool estado = false;
            string queryString = @"UPDATE TarjetaGrafica 
                                   SET Vram = @Vram, 
                                       FrecuenciaBase = @FrecuenciaBase, 
                                       FrecuenciaTurbo = @FrecuenciaTurbo,  
                                       TipoMemoria = @TipoMemoria,
                                       Consumo = @Consumo
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Update(tarjetaGrafica as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "Vram", tarjetaGrafica.Vram);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaBase", tarjetaGrafica.FrecuenciaBase);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaTurbo", tarjetaGrafica.FrecuenciaTurbo);
                    OperationsSql.AddWithValueString(parameter: "TipoMemoria", tarjetaGrafica.TipoMemoria);
                    OperationsSql.AddWithValueString(parameter: "Consumo", tarjetaGrafica.Consumo);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else { OperationsSql.ExecuteTransactionCancel(); }
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Actualizar el Producto -> Tarjeta de Video");
            }
            finally { ProductosDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM TarjetaGrafica r
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
                LogError.SetError("Problemas al Obtener la Cantidad de Productos -> Tarjeta de Video");
            }
            finally { OperationsSql.CloseConnection(); }
            return cantidad;
        }
    }
}
