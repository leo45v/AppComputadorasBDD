﻿using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class Requirements
    {
        public TipoComputer Estudio { get; set; }
        public TipoComputer Oficina { get; set; }
        public TipoComputer TrabajoDiseno { get; set; }
        public TipoComputer Gaming { get; set; }
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
            public double CostoMaximo
            {
                get
                {
                    double totalCosto = 0.0;
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
            public double CostoMinimo
            {
                get
                {
                    double totalCosto = 0.0;
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
            public double Costo
            {
                get
                {
                    double totalCosto = 0.0;
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
                public MinMax<double> Capacidad { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class FuenteR
            {
                public MinMax<ECertificacion> Certificacion { get; set; }
                public MinMax<double> Potencia { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class PlacaBaseR
            {
                public MinMax<double> PrecioUnidad { get; set; }
                public MinMax<double> NumeroDims { get; set; }
            }
            public class ProcesadorR
            {
                public MinMax<double> NumeroNucleos { get; set; }
                public MinMax<double> NumeroHilo { get; set; }
                public MinMax<double> Consumo { get; set; }
                public MinMax<double> FrecuenciaBase { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class RamR
            {
                public MinMax<double> Cantidad { get; set; }
                public MinMax<double> Capacidad { get; set; }
                public MinMax<double> Frecuencia { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class TarjetaGraficaR
            {
                public MinMax<double> Vram { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class MonitorR
            {
                public MinMax<double> Frecuencia { get; set; }
                public MinMax<double> PrecioUnidad { get; set; }
            }
            public class GabineteR
            {
                public MinMax<double> PrecioUnidad { get; set; }
            }
        }

    }

}