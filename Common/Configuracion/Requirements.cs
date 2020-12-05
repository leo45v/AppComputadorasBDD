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
                    decimal totalCosto = new Decimal(0.0);
                    totalCosto += Fuente.PrecioUnidad.Max;
                    totalCosto += PlacaBase.PrecioUnidad.Max;
                    totalCosto += Procesador.PrecioUnidad.Max;
                    totalCosto += Ram.PrecioUnidad.Max;
                    totalCosto += TarjetaGrafica.PrecioUnidad.Max;
                    totalCosto += Monitor.PrecioUnidad.Max;
                    totalCosto += Gabinete.PrecioUnidad.Max;
                    return totalCosto;
                }
            }
            public decimal CostoMinimo
            {
                get
                {
                    decimal totalCosto = new Decimal(0.0);
                    totalCosto += Fuente.PrecioUnidad.Min;
                    totalCosto += PlacaBase.PrecioUnidad.Min;
                    totalCosto += Procesador.PrecioUnidad.Min;
                    totalCosto += Ram.PrecioUnidad.Min;
                    totalCosto += TarjetaGrafica.PrecioUnidad.Min;
                    totalCosto += Monitor.PrecioUnidad.Min;
                    totalCosto += Gabinete.PrecioUnidad.Min;
                    return totalCosto;
                }
            }
            public class MinMax<T> where T : struct
            {
                public T Min { get; set; }
                public T Max { get; set; }
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
