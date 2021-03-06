﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class ProcesadorDal
    {
        public static bool Insertar(Procesador procesador)
        {
            bool estado = false;
            string queryString = @"INSERT INTO Procesador
                                   (IdProducto, FrecuenciaBase, FrecuenciaTurbo, NumeroNucleos, NumeroHilos, Consumo, Litografia, Socket) 
                                   VALUES
                                   (@IdProducto, @FrecuenciaBase, @FrecuenciaTurbo, @NumeroNucleos, @NumeroHilos, @Consumo, @Litografia, @Socket)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Insertar(procesador as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaBase", value: procesador.FrecuenciaBase);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaTurbo", value: procesador.FrecuenciaTurbo);
                    OperationsSql.AddWithValueString(parameter: "NumeroNucleos", value: procesador.NumeroNucleos);
                    OperationsSql.AddWithValueString(parameter: "NumeroHilos", value: procesador.NumeroHilos);
                    OperationsSql.AddWithValueString(parameter: "Consumo", value: procesador.Consumo);
                    OperationsSql.AddWithValueString(parameter: "Litografia", value: procesador.Litografia);
                    OperationsSql.AddWithValueString(parameter: "Socket", value: procesador.Socket.IdSocket);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else
                {
                    OperationsSql.ExecuteTransactionCancel();
                }
            }
            catch (Exception)
            {
                OperationsSql.ExecuteTransactionCancel();
                LogError.SetError("Problemas al Insertar el Producto -> Procesador");
            }
            finally
            {
                ProductosDal.cascada = false;
                OperationsSql.CloseConnection();
            }
            return estado;
        }
        public static Procesador Get(Guid idProcesador)
        {
            Procesador procesador = null;
            string query = @"SELECT r.IdProducto, r.FrecuenciaBase, r.FrecuenciaTurbo, r.NumeroNucleos, r.NumeroHilos, r.Consumo, r.Litografia, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca, 
                             sp.IdSocket, sp.NombreSocket, sp.Descripcion
                             FROM Procesador r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
                             INNER JOIN SocketProcesador sp ON sp.IdSocket = r.Socket 
                             WHERE pro.Eliminado = 0 AND pro.IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("IdProducto", idProcesador);
                Dictionary<string, object> data = OperationsSql.ExecuteReader();
                if (data != null)
                {
                    procesador = Procesador.Dictionary_A_Procesador(data);
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener el Producto -> Procesador");
            }
            finally { OperationsSql.CloseConnection(); }
            return procesador;
        }
        public static List<Procesador> GetAll()
        {
            List<Procesador> procesadors = null;
            string query = @"SELECT r.IdProducto, r.FrecuenciaBase, r.FrecuenciaTurbo, r.NumeroNucleos, r.NumeroHilos, r.Consumo, r.Litografia, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca, 
                             sp.IdSocket, sp.NombreSocket, sp.Descripcion
                             FROM Procesador r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
                             INNER JOIN SocketProcesador sp ON sp.IdSocket = r.Socket 
                             WHERE pro.Eliminado = 0";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    procesadors = new List<Procesador>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        procesadors.Add(Procesador.Dictionary_A_Procesador(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener los Productos -> Procesador");
            }
            finally { OperationsSql.CloseConnection(); }
            return procesadors;
        }
        public static ListaProductos GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            ListaProductos productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
                             mar.NombreMarca
                             FROM Procesador r
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
                LogError.SetError("Problemas al Obtener los Productos -> Procesador");
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
                            INNER JOIN Procesador ON Procesador.IdProducto = pro.IdProducto
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
                LogError.SetError("Problemas al Obtener las Marcas de los Productos -> Procesador");
            }
            finally { OperationsSql.CloseConnection(); }
            return listaMarcas;
        }
        public static bool Update(Procesador procesador)
        {
            bool estado = false;
            string queryString = @"UPDATE Procesador 
                                   SET FrecuenciaBase = @FrecuenciaBase, 
                                       FrecuenciaTurbo = @FrecuenciaTurbo, 
                                       NumeroNucleos = @NumeroNucleos, 
                                       NumeroHilos = @NumeroHilos,
                                       Consumo = @Consumo,
                                       Litografia = @Litografia, 
                                       Socket = @Socket
                                   WHERE IdProducto = @IdProducto";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.cascada = true;
                if (ProductosDal.Update(procesador as Producto))
                {
                    OperationsSql.CreateBasicCommandWithTransaction(queryString);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaBase", procesador.FrecuenciaBase);
                    OperationsSql.AddWithValueString(parameter: "FrecuenciaTurbo", procesador.FrecuenciaTurbo);
                    OperationsSql.AddWithValueString(parameter: "NumeroNucleos", procesador.NumeroNucleos);
                    OperationsSql.AddWithValueString(parameter: "NumeroHilos", procesador.NumeroHilos);
                    OperationsSql.AddWithValueString(parameter: "Consumo", procesador.Consumo);
                    OperationsSql.AddWithValueString(parameter: "Litografia", procesador.Litografia);
                    OperationsSql.AddWithValueString(parameter: "IdProducto", procesador.IdProducto);
                    OperationsSql.AddWithValueString(parameter: "Socket", procesador.Socket.IdSocket);
                    OperationsSql.ExecuteBasicCommandWithTransaction();
                    OperationsSql.ExecuteTransactionCommit();
                    estado = true;
                }
                else { OperationsSql.ExecuteTransactionCancel(); }
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Actualizar el Producto -> Procesador");
            }
            finally { ProductosDal.cascada = false; OperationsSql.CloseConnection(); }
            return estado;
        }
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Procesador r
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
                LogError.SetError("Problemas al Obtener la Cantidad de Productos -> Procesador");
            }
            finally { OperationsSql.CloseConnection(); }
            return cantidad;
        }
    }
}
