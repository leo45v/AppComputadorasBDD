using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class ConfiguracionComputerOperation
    {
        public double presupuesto;
        public ConfiguracionComputerOperation(double presupuesto)
        {
            this.presupuesto = presupuesto;
        }
        public List<PlacaBase> PlacaBaseRecomendados(Requirements.TipoComputer.PlacaBaseR placaBaseR, Procesador procesador)
        {
            List<PlacaBase> placaBases = null;
            if (placaBaseR.PrecioUnidad.max >= ((presupuesto - (double)procesador.PrecioUnidad) * 0.2))//20% de lo que resta
            {
                placaBaseR.PrecioUnidad.max = (presupuesto - (double)procesador.PrecioUnidad) * 0.2;
            }
            string query = @"SELECT r.IdProducto, r.NumeroDims, r.CapacidadMem, r.Tamano, r.SoporteProcesador as IdSocket, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca, 
            sp.NombreSocket, sp.Descripcion 
            FROM PlacaBase r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca
            INNER JOIN SocketProcesador sp ON sp.IdSocket = r.SoporteProcesador
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.NumeroDims >= @NumeroDimsMin 
            AND r.SoporteProcesador = @SoporteProcesador";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", placaBaseR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("NumeroDimsMin", placaBaseR.NumeroDims.min);
                OperationsSql.AddWithValueString("SoporteProcesador", procesador.Socket.IdSocket);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    placaBases = new List<PlacaBase>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        placaBases.Add(PlacaBase.Dictionary_A_PlacaBase(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            return placaBases;
        }
        public List<Procesador> ProcesadoresRecomendados(Requirements.TipoComputer.ProcesadorR procesadorR)
        {
            List<Procesador> procesadors = null;
            if (procesadorR.PrecioUnidad.max >= presupuesto * 0.5)
            {
                procesadorR.PrecioUnidad.max = presupuesto * 0.5;
            }
            if (procesadorR.PrecioUnidad.min >= presupuesto * 0.5)
            {
                procesadorR.PrecioUnidad.max = presupuesto * 0.25;
            }

            string query = @"SELECT r.IdProducto, r.FrecuenciaBase, r.FrecuenciaTurbo, r.NumeroNucleos, r.NumeroHilos, r.Consumo, r.Litografia, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca, 
            sp.IdSocket, sp.NombreSocket, sp.Descripcion
            FROM Procesador r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            INNER JOIN SocketProcesador sp ON sp.IdSocket = r.Socket 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Consumo <= @ConsumoMax 
            AND r.FrecuenciaBase >= @FrecuenciaBaseMin  
            AND r.NumeroHilos >= @NumeroHiloMin 
            AND r.NumeroNucleos >= @NumeroNucleosMin 
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMin", procesadorR.PrecioUnidad.min);
                OperationsSql.AddWithValueString("CostoMax", procesadorR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("ConsumoMin", procesadorR.Consumo.min);
                OperationsSql.AddWithValueString("ConsumoMax", procesadorR.Consumo.max);
                OperationsSql.AddWithValueString("FrecuenciaBaseMin", procesadorR.FrecuenciaBase.min);
                OperationsSql.AddWithValueString("FrecuenciaBaseMax", procesadorR.FrecuenciaBase.max);
                OperationsSql.AddWithValueString("NumeroHiloMin", procesadorR.NumeroHilo.min);
                OperationsSql.AddWithValueString("NumeroHiloMax", procesadorR.NumeroHilo.max);
                OperationsSql.AddWithValueString("NumeroNucleosMin", procesadorR.NumeroNucleos.min);
                OperationsSql.AddWithValueString("NumeroNucleosMax", procesadorR.NumeroNucleos.max);
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
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return procesadors;
        }


    }
}
