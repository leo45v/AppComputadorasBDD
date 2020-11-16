﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos
{
    public class ProcesadorDal
    {
        public static void Insertar(Procesador procesador)
        {
            string queryString = @"INSERT INTO Procesador
                                   (IdProducto, FrecuenciaBase, FrecuenciaTurbo, NumeroNucleos, NumeroHilos, Consumo, Litografia) 
                                   VALUES
                                   (@IdProducto, @FrecuenciaBase, @FrecuenciaTurbo, @NumeroNucleos, @NumeroHilos, @Consumo, @Litografia)";
            try
            {
                OperationsSql.OpenConnection();
                ProductosDal.Insertar(procesador as Producto);
                OperationsSql.CreateBasicCommandWithTransaction(queryString);
                OperationsSql.AddWithValueString("FrecuenciaBase", procesador.FrecuenciaBase);
                OperationsSql.AddWithValueString("FrecuenciaTurbo", procesador.FrecuenciaTurbo);
                OperationsSql.AddWithValueString("NumeroNucleos", procesador.NumeroNucleos);
                OperationsSql.AddWithValueString("NumeroHilos", procesador.NumeroHilos);
                OperationsSql.AddWithValueString("Consumo", procesador.Consumo);
                OperationsSql.AddWithValueString("Litografia", procesador.Litografia);
                OperationsSql.ExecuteBasicCommandWithTransaction();
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OperationsSql.CloseConnection();
            }
        }
        public static Procesador Get(Guid idProcesador)
        {
            Procesador procesador = null;
            return procesador;
        }
        public static List<Procesador> GetAll()
        {
            List<Procesador> procesadors = null;
            string query = @"SELECT r.IdProducto, r.FrecuenciaBase, r.FrecuenciaTurbo, r.NumeroNucleos, r.NumeroHilos, r.Consumo, r.Litografia
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado,
                             mar.NombreMarca
                             FROM Procesador r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
                             WHERE pro.Descontinuado = 0";
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
                        procesadors.Add(Dictionary_A_Procesador(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return procesadors;
        }
        public static List<Producto> GetWithRange(int start, int cant, int? idMarca, double? minPrice, double? maxPrice)
        {
            List<Producto> productos = null;
            string query = @"SELECT r.IdProducto, 
                             pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado,
                             mar.NombreMarca
                             FROM Procesador r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca " +
                             (!(idMarca == 0) || (!(minPrice is null) && !(maxPrice is null)) ? @"WHERE " : @"") +
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
        public static bool Delete(Guid idProducto)
        {
            bool estado = false;
            string query = @"DELETE FROM Producto WHERE IdProducto = @IdProducto";
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
        public static List<Marca> Get_ListMarcas()
        {
            List<Marca> listaMarcas = null;
            string query = @"SELECT Marca.NombreMarca, Marca.IdMarca
                            FROM Marca
                            INNER JOIN Producto pro ON pro.IdMarca = Marca.IdMarca
                            INNER JOIN Procesador ON Procesador.IdProducto = pro.IdProducto
                            WHERE pro.Descontinuado = 0
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
        public static int Count(int? idMarca, double? minPrice, double? maxPrice)
        {
            int cantidad = 0;
            string query = @"SELECT COUNT(*) as Cantidad
                             FROM Procesador r
                             INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
                             WHERE pro.Descontinuado = 0 " +
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
        private static Procesador Dictionary_A_Procesador(Dictionary<string, object> data)
        {
            return new Procesador()
            {
                IdProducto = (Guid)data["IdProducto"],
                Descontinuado = (bool)data["Descontinuado"],
                FrecuenciaBase = (int)data["FrecuenciaBase"],
                Consumo = (int)data["Consumo"],
                Imagen = (string)data["Imagen"],
                FrecuenciaTurbo = (int)data["FrecuenciaTurbo"],
                Marca = new Marca()
                {
                    IdMarca = (byte)data["IdMarca"],
                    NombreMarca = (string)data["NombreMarca"]
                },
                Litografia = (int)data["Litografia"],
                Nombre = (string)data["Nombre"],
                PrecioUnidad = (decimal)data["PrecioUnidad"],
                Stock = (short)data["Stock"],
                NumeroHilos = (int)data["NumeroHilos"],
                NumeroNucleos = (int)data["NumeroNucleos"]
            };
        }
    }
}