using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;

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
            double restante = (presupuesto - (double)procesador.PrecioUnidad);
            if (placaBaseR.PrecioUnidad.max >= restante)//20% de lo que resta
            {
                //placaBaseR.PrecioUnidad.max = restante * 0.2;
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
            AND r.Memoria >= @CapacidadMin 
            AND r.Frecuencia >= @FrecuenciaMin  
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", ramR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("CapacidadMin", ramR.Capacidad.min);
                OperationsSql.AddWithValueString("FrecuenciaMin", ramR.Frecuencia.min);
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
                OperationsSql.AddWithValueString("CostoMax", fuenteR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("PotenciaMin", fuenteR.Potencia.min);
                OperationsSql.AddWithValueString("CertificacionMin", (int)fuenteR.Certificacion.min);
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
                OperationsSql.AddWithValueString("CostoMax", monitorR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("FrecuenciaMin", monitorR.Frecuencia.min);
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
                OperationsSql.AddWithValueString("CostoMax", gabineteR.PrecioUnidad.max);
                List<Dictionary<string, object>> data = OperationsSql.ExecuteReaderMany();
                if (data != null)
                {
                    gabinetes = new List<Gabinete>();
                    foreach (Dictionary<string, object> item in data)
                    {
                        Gabinete gabinete = Gabinete.Dictionary_A_Gabinete(item);
                        gabinete.Colores = GetColores(gabinete.IdProducto);
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
                OperationsSql.AddWithValueString("CostoMax", tarjetaGraficaR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("VramMin", tarjetaGraficaR.Vram.min);
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
            AND pro.Eliminado = 0 
            AND pro.Stock > 0
            ORDER BY pro.PrecioUnidad ASC ";
            try
            {
                OperationsSql.OpenConnection();
                OperationsSql.CreateBasicCommandWithTransaction(query);
                OperationsSql.AddWithValueString("CostoMax", almacenamientoR.PrecioUnidad.max);
                OperationsSql.AddWithValueString("CapacidadMax", almacenamientoR.Capacidad.max);
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
        public static List<Colores> GetColores(Guid idProducto)
        {
            List<Colores> colores = null;
            string query = @"SELECT co.IdColor, co.Nombre, co.ColorRgb
                             FROM Color co 
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
                        string[] rgb = item["ColorRgb"].ToString().Split(",");
                        colores.Add(new Colores()
                        {
                            IdColor = (short)item["IdColor"],
                            Nombre = (string)item["Nombre"],
                            ColorRGB = Color.FromArgb(255, int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]))
                        });
                    }
                }
            }
            catch (Exception)
            {
                LogError.SetError("Problemas al Obtener los Colores del Producto");
            }
            return colores;
        }
    }
}
