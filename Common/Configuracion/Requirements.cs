using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class Requirements
    {
        public TipoComputer ComputadoraX { get; set; }
        public class TipoComputer
        {
            public AlmacenamientoR Almacenamiento { get; set; }
            public FuenteR Fuente { get; set; }
            public PlacaBaseR PlacaBase { get; set; }
            public ProcesadorR Procesador { get; set; }
            public RamR Ram { get; set; }
            public TarjetaGraficaR TarjetaGrafica { get; set; }
            public MonitorR Monitor { get; set; }
            public GabineteR Gabinete { get; set; }
            public decimal CostoMaximo
            {
                get
                {
                    decimal totalCosto = (decimal)0.0;
                    totalCosto += Fuente.PrecioUnidad.max;
                    totalCosto += PlacaBase.PrecioUnidad.max;
                    totalCosto += Procesador.PrecioUnidad.max;
                    totalCosto += Ram.PrecioUnidad.max;
                    totalCosto += TarjetaGrafica.PrecioUnidad.max;
                    totalCosto += Monitor.PrecioUnidad.max;
                    totalCosto += Gabinete.PrecioUnidad.max;
                    return totalCosto;
                }
            }
            public decimal CostoMinimo
            {
                get
                {
                    decimal totalCosto = (decimal)0.0;
                    totalCosto += Fuente.PrecioUnidad.min;
                    totalCosto += PlacaBase.PrecioUnidad.min;
                    totalCosto += Procesador.PrecioUnidad.min;
                    totalCosto += Ram.PrecioUnidad.min;
                    totalCosto += TarjetaGrafica.PrecioUnidad.min;
                    totalCosto += Monitor.PrecioUnidad.min;
                    totalCosto += Gabinete.PrecioUnidad.min;
                    return totalCosto;
                }
            }
            public decimal Costo
            {
                get
                {
                    decimal totalCosto = (decimal)0.0;
                    totalCosto += Fuente.PrecioUnidad.CostoReal;
                    totalCosto += PlacaBase.PrecioUnidad.CostoReal;
                    totalCosto += Procesador.PrecioUnidad.CostoReal;
                    totalCosto += Ram.PrecioUnidad.CostoReal;
                    totalCosto += TarjetaGrafica.PrecioUnidad.CostoReal;
                    totalCosto += Monitor.PrecioUnidad.CostoReal;
                    totalCosto += Gabinete.PrecioUnidad.CostoReal;
                    return totalCosto;
                }
            }
            public class MinMax<T> where T : struct
            {
                public T min { get; set; }
                public T max { get; set; }
                public T CostoReal { get; set; }
            }
            public class AlmacenamientoR
            {
                public MinMax<int> Capacidad { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
                public MinMax<ETipoDisco> Tipo { get; set; }
            }
            public class FuenteR
            {
                public MinMax<ECertificacion> Certificacion { get; set; }
                public MinMax<int> Potencia { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
            public class PlacaBaseR
            {
                public MinMax<decimal> PrecioUnidad { get; set; }
                public MinMax<int> NumeroDims { get; set; }
            }
            public class ProcesadorR
            {
                public MinMax<int> NumeroNucleos { get; set; }
                public MinMax<int> NumeroHilo { get; set; }
                public MinMax<int> Consumo { get; set; }
                public MinMax<int> FrecuenciaBase { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
            public class RamR
            {
                public MinMax<int> Cantidad { get; set; }
                public MinMax<int> Capacidad { get; set; }
                public MinMax<int> Frecuencia { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
            public class TarjetaGraficaR
            {
                public MinMax<int> Vram { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
            public class MonitorR
            {
                public MinMax<int> Frecuencia { get; set; }
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
            public class GabineteR
            {
                public MinMax<decimal> PrecioUnidad { get; set; }
            }
        }

    }

}
