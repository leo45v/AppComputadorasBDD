using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class ConfiguracionComputerOperation
    {
        public decimal presupuesto;
        public ConfiguracionComputerOperation()
        {
            this.presupuesto = new Decimal(0.0);
        }
        public ConfiguracionComputerOperation(decimal presupuesto)
        {
            this.presupuesto = presupuesto;
        }
        public List<PlacaBase> PlacaBaseRecomendados(Requirements.TipoComputer.PlacaBaseR placaBaseR, Procesador procesador)
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
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.NumeroDims >= @NumeroDimsMin 
            AND r.SoporteProcesador = @SoporteProcesador";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", placaBaseR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("NumeroDimsMin", placaBaseR.NumeroDims.Min);
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
                OperationsSql.AddWithValueString("CostoMax", procesadorR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("ConsumoMax", procesadorR.Consumo.Max);
                OperationsSql.AddWithValueString("FrecuenciaBaseMin", procesadorR.FrecuenciaBase.Min);
                OperationsSql.AddWithValueString("NumeroHiloMin", procesadorR.NumeroHilo.Min);
                OperationsSql.AddWithValueString("NumeroNucleosMin", procesadorR.NumeroNucleos.Min);
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
        public List<Ram> RamsRecomendados(Requirements.TipoComputer.RamR ramR)
        {
            List<Ram> rams = null;
            string query = @"SELECT r.IdProducto, r.Memoria, r.Frecuencia, r.Latencia,  
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca 
            FROM Ram r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Frecuencia >= @FrecuenciaMin  
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", ramR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("FrecuenciaMin", ramR.Frecuencia.Min);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    rams = new List<Ram>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        rams.Add(Ram.Dictionary_A_Ram(item));
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
        public List<Fuente> FuenteRecomendados(Requirements.TipoComputer.FuenteR fuenteR)
        {
            List<Fuente> fuentes = null;
            string query = @"SELECT r.IdProducto, r.Potencia, r.Certificacion, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca 
            FROM Fuente r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Potencia >= @PotenciaMin 
            AND r.Certificacion >= @CertificacionMin  
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", fuenteR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("PotenciaMin", fuenteR.Potencia.Min);
                OperationsSql.AddWithValueString("CertificacionMin", (int)fuenteR.Certificacion.Min);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    fuentes = new List<Fuente>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        fuentes.Add(Fuente.Dictionary_A_Fuente(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return fuentes;
        }
        public List<Monitor> MonitorRecomendados(Requirements.TipoComputer.MonitorR monitorR)
        {
            List<Monitor> monitores = null;
            string query = @"SELECT r.IdProducto, r.Tamano, r.Frecuencia, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca,
            ra.IdRatio, ra.NombreRatio, 
            res.IdResolucion, res.NombreResolucion 
            FROM Monitor r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            INNER JOIN Ratio ra ON ra.IdRatio = r.IdRatio 
            INNER JOIN Resolucion res ON res.IdResolucion = r.IdResolucion 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Frecuencia >= @FrecuenciaMin 
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", monitorR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("FrecuenciaMin", monitorR.Frecuencia.Min);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    monitores = new List<Monitor>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        monitores.Add(Monitor.Dictionary_A_Monitor(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return monitores;
        }
        public List<Gabinete> GabinetesRecomendados(Requirements.TipoComputer.GabineteR gabineteR)
        {
            List<Gabinete> gabinetes = null;
            string query = @"SELECT r.IdProducto, r.Altura, r.Peso, r.Largo, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca 
            FROM Gabinete r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", gabineteR.PrecioUnidad.Max);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    gabinetes = new List<Gabinete>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        Gabinete gabinete = Gabinete.Dictionary_A_Gabinete(item);
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
        public List<Grafica> TarjetaGraficaRecomendados(Requirements.TipoComputer.TarjetaGraficaR tarjetaGraficaR)
        {
            List<Grafica> tarjetasGraficas = null;
            string query = @"SELECT r.IdProducto, r.Vram, r.FrecuenciaBase, r.FrecuenciaTurbo, r.TipoMemoria, r.Consumo, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca 
            FROM TarjetaGrafica r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Vram >= @VramMin  
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", tarjetaGraficaR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("VramMin", tarjetaGraficaR.Vram.Min);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    tarjetasGraficas = new List<Grafica>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        tarjetasGraficas.Add(Grafica.Dictionary_A_Grafica(item));
                    }
                }
                OperationsSql.ExecuteTransactionCommit();
            }
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return tarjetasGraficas;
        }
        public List<Almacenamiento> AlmacenamientoRecomendados(Requirements.TipoComputer.AlmacenamientoR almacenamientoR)
        {
            List<Almacenamiento> almacenamientos = null;
            string query = @"SELECT r.IdProducto, r.Capacidad, r.Escritura, r.Lectura, r.Tipo, 
            pro.PrecioUnidad, pro.Imagen, pro.Nombre, pro.Stock, pro.IdMarca, pro.Descontinuado, pro.Eliminado, 
            mar.NombreMarca 
            FROM Almacenamiento r
            INNER JOIN Producto pro ON pro.IdProducto = r.IdProducto
            INNER JOIN Marca mar ON mar.IdMarca = pro.IdMarca 
            WHERE pro.PrecioUnidad <= @CostoMax 
            AND r.Capacidad <= @CapacidadMax  
            AND ( r.Tipo LIKE '%' + @TipoMin + '%' OR r.Tipo LIKE '%' + @TipoMax + '%' )
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", almacenamientoR.PrecioUnidad.Max);
                OperationsSql.AddWithValueString("CapacidadMax", almacenamientoR.Capacidad.Max);
                OperationsSql.AddWithValueString("TipoMin", almacenamientoR.Tipo.Min.ToString());
                OperationsSql.AddWithValueString("TipoMax", almacenamientoR.Tipo.Max.ToString());
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
            catch (Exception)
            {
                throw;
            }
            finally { OperationsSql.CloseConnection(); }
            return almacenamientos;
        }
    }
}
