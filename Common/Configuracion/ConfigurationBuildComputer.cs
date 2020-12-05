using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class ConfigurationBuildComputer
    {
        private readonly Requirements requirements;
        public Requirements Requisitos
        {
            get { return requirements; }
        }
        private TipoComputadora tipoComputadora;
        public TipoComputadora CambiarTipoComputadora
        {
            get { return tipoComputadora; }
            set
            {
                tipoComputadora = value;
                this.requirements.ComputadoraX = SetConfigurations;
            }
        }
        /// <summary>
        /// Configura los requerimientos por defecto como tipo de computadora "Estudio"
        /// </summary>
        public ConfigurationBuildComputer()
        {
            this.requirements = new Requirements();
            this.tipoComputadora = TipoComputadora.Estudio;
            this.requirements.ComputadoraX = SetConfigurations;
        }
        /// <summary>
        /// Configura los requerimientos dependiendo del tipo de computadora "tipoComputadora"
        /// </summary>
        /// <param name="tipoComputadora"></param>
        public ConfigurationBuildComputer(TipoComputadora tipoComputadora)
        {
            this.requirements = new Requirements();
            this.tipoComputadora = tipoComputadora;
            this.requirements.ComputadoraX = SetConfigurations;
        }
        private Requirements.TipoComputer SetConfigurations
        {
            get
            {
                Requirements.TipoComputer tipoComputer = GetConfigurationsDefault;
                if (this.tipoComputadora == TipoComputadora.Gaming)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.Max = 200;
                    tipoComputer.Almacenamiento.PrecioUnidad.Min = 50;

                    tipoComputer.Almacenamiento.Capacidad.Max = 4000;
                    tipoComputer.Almacenamiento.Capacidad.Min = 2000;
                    tipoComputer.Almacenamiento.Tipo.Min = ETipoDisco.HDD;
                    tipoComputer.Almacenamiento.Tipo.Max = ETipoDisco.SSD;

                    tipoComputer.Fuente.PrecioUnidad.Max = 250;
                    tipoComputer.Fuente.PrecioUnidad.Min = 120;
                    tipoComputer.Fuente.Certificacion.Max = ECertificacion.Platinum_80Plus;
                    tipoComputer.Fuente.Certificacion.Min = ECertificacion.Bronze_80Plus;

                    tipoComputer.Gabinete.PrecioUnidad.Max = 180;
                    tipoComputer.Gabinete.PrecioUnidad.Min = 120;

                    tipoComputer.Monitor.PrecioUnidad.Max = 700;
                    tipoComputer.Monitor.PrecioUnidad.Min = 250;
                    tipoComputer.Monitor.Frecuencia.Max = 144;
                    tipoComputer.Monitor.Frecuencia.Min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.Max = 450;
                    tipoComputer.PlacaBase.PrecioUnidad.Min = 150;
                    tipoComputer.PlacaBase.NumeroDims.Max = 4;
                    tipoComputer.PlacaBase.NumeroDims.Min = 4;

                    tipoComputer.Procesador.PrecioUnidad.Max = 900;
                    tipoComputer.Procesador.PrecioUnidad.Min = 220;
                    tipoComputer.Procesador.Consumo.Max = 450;
                    tipoComputer.Procesador.Consumo.Min = 120;
                    tipoComputer.Procesador.FrecuenciaBase.Max = 5000;
                    tipoComputer.Procesador.FrecuenciaBase.Min = 3500;
                    tipoComputer.Procesador.NumeroNucleos.Max = 12;
                    tipoComputer.Procesador.NumeroNucleos.Min = 6;
                    tipoComputer.Procesador.NumeroHilo.Max = 24;
                    tipoComputer.Procesador.NumeroHilo.Min = 6;

                    tipoComputer.Ram.PrecioUnidad.Max = 190;
                    tipoComputer.Ram.PrecioUnidad.Min = 70;
                    tipoComputer.Ram.Capacidad.Max = 64;
                    tipoComputer.Ram.Capacidad.Min = 16;
                    tipoComputer.Ram.Frecuencia.Max = 4200;
                    tipoComputer.Ram.Frecuencia.Min = 2133;
                    tipoComputer.Ram.Cantidad.Max = 4;
                    tipoComputer.Ram.Cantidad.Min = 2;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.Max = 1500;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.Min = 100;
                    tipoComputer.TarjetaGrafica.Vram.Max = 24;
                    tipoComputer.TarjetaGrafica.Vram.Min = 4;
                }
                else if (this.tipoComputadora == TipoComputadora.Oficina)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.Max = 100;
                    tipoComputer.Almacenamiento.PrecioUnidad.Min = 40;
                    tipoComputer.Almacenamiento.Capacidad.Max = 1000;
                    tipoComputer.Almacenamiento.Capacidad.Min = 500;
                    tipoComputer.Almacenamiento.Tipo.Min = ETipoDisco.HDD;
                    tipoComputer.Almacenamiento.Tipo.Max = ETipoDisco.HDD;

                    tipoComputer.Fuente.PrecioUnidad.Max = 120;
                    tipoComputer.Fuente.PrecioUnidad.Min = 40;
                    tipoComputer.Fuente.Certificacion.Max = ECertificacion.Bronze_80Plus;
                    tipoComputer.Fuente.Certificacion.Min = ECertificacion.None;

                    tipoComputer.Gabinete.PrecioUnidad.Max = 80;
                    tipoComputer.Gabinete.PrecioUnidad.Min = 30;

                    tipoComputer.Monitor.PrecioUnidad.Max = 150;
                    tipoComputer.Monitor.PrecioUnidad.Min = 50;
                    tipoComputer.Monitor.Frecuencia.Max = 60;
                    tipoComputer.Monitor.Frecuencia.Min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.Max = 150;
                    tipoComputer.PlacaBase.PrecioUnidad.Min = 50;
                    tipoComputer.PlacaBase.NumeroDims.Max = 2;
                    tipoComputer.PlacaBase.NumeroDims.Min = 1;

                    tipoComputer.Procesador.PrecioUnidad.Max = 200;
                    tipoComputer.Procesador.PrecioUnidad.Min = 50;
                    tipoComputer.Procesador.Consumo.Max = 100;
                    tipoComputer.Procesador.Consumo.Min = 50;
                    tipoComputer.Procesador.FrecuenciaBase.Max = 3000;
                    tipoComputer.Procesador.FrecuenciaBase.Min = 1800;
                    tipoComputer.Procesador.NumeroNucleos.Max = 4;
                    tipoComputer.Procesador.NumeroNucleos.Min = 2;
                    tipoComputer.Procesador.NumeroHilo.Max = 8;
                    tipoComputer.Procesador.NumeroHilo.Min = 2;

                    tipoComputer.Ram.PrecioUnidad.Max = 70;
                    tipoComputer.Ram.PrecioUnidad.Min = 30;
                    tipoComputer.Ram.Capacidad.Max = 8;
                    tipoComputer.Ram.Capacidad.Min = 4;
                    tipoComputer.Ram.Frecuencia.Max = 2666;
                    tipoComputer.Ram.Frecuencia.Min = 2133;
                    tipoComputer.Ram.Cantidad.Max = 2;
                    tipoComputer.Ram.Cantidad.Min = 1;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.Max = 150;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.Min = 0;
                    tipoComputer.TarjetaGrafica.Vram.Max = 1;
                    tipoComputer.TarjetaGrafica.Vram.Min = 0;
                }
                else if (this.tipoComputadora == TipoComputadora.TrabajoDiseno)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.Max = 700;
                    tipoComputer.Almacenamiento.PrecioUnidad.Min = 300;
                    tipoComputer.Almacenamiento.Capacidad.Max = 10000;
                    tipoComputer.Almacenamiento.Capacidad.Min = 4000;
                    tipoComputer.Almacenamiento.Tipo.Min = ETipoDisco.SSD;
                    tipoComputer.Almacenamiento.Tipo.Max = ETipoDisco.SSD;

                    tipoComputer.Fuente.PrecioUnidad.Max = 350;
                    tipoComputer.Fuente.PrecioUnidad.Min = 150;
                    tipoComputer.Fuente.Certificacion.Max = ECertificacion.Titanium_80Plus;
                    tipoComputer.Fuente.Certificacion.Min = ECertificacion.Gold_80Plus;

                    tipoComputer.Gabinete.PrecioUnidad.Max = 250;
                    tipoComputer.Gabinete.PrecioUnidad.Min = 120;

                    tipoComputer.Monitor.PrecioUnidad.Max = 500;
                    tipoComputer.Monitor.PrecioUnidad.Min = 180;
                    tipoComputer.Monitor.Frecuencia.Max = 1000;// 
                    tipoComputer.Monitor.Frecuencia.Min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.Max = 500;
                    tipoComputer.PlacaBase.PrecioUnidad.Min = 150;
                    tipoComputer.PlacaBase.NumeroDims.Max = 8;
                    tipoComputer.PlacaBase.NumeroDims.Min = 4;

                    tipoComputer.Procesador.PrecioUnidad.Max = 4000;
                    tipoComputer.Procesador.PrecioUnidad.Min = 400;
                    tipoComputer.Procesador.Consumo.Max = 600;
                    tipoComputer.Procesador.Consumo.Min = 200;
                    tipoComputer.Procesador.FrecuenciaBase.Max = 5000;
                    tipoComputer.Procesador.FrecuenciaBase.Min = 3500;
                    tipoComputer.Procesador.NumeroNucleos.Max = 64;
                    tipoComputer.Procesador.NumeroNucleos.Min = 8;
                    tipoComputer.Procesador.NumeroHilo.Max = 128;
                    tipoComputer.Procesador.NumeroHilo.Min = 16;

                    tipoComputer.Ram.PrecioUnidad.Max = 250;
                    tipoComputer.Ram.PrecioUnidad.Min = 150;
                    tipoComputer.Ram.Capacidad.Max = 128;
                    tipoComputer.Ram.Capacidad.Min = 32;
                    tipoComputer.Ram.Frecuencia.Max = 5000;
                    tipoComputer.Ram.Frecuencia.Min = 2133;
                    tipoComputer.Ram.Cantidad.Max = 8;
                    tipoComputer.Ram.Cantidad.Min = 4;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.Max = 4200;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.Min = 500;
                    tipoComputer.TarjetaGrafica.Vram.Max = 48;
                    tipoComputer.TarjetaGrafica.Vram.Min = 8;
                }
                else if (this.tipoComputadora == TipoComputadora.Estudio)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.Max = 100;
                    tipoComputer.Almacenamiento.PrecioUnidad.Min = 50;
                    tipoComputer.Almacenamiento.Capacidad.Max = 1000;
                    tipoComputer.Almacenamiento.Capacidad.Min = 500;
                    tipoComputer.Almacenamiento.Tipo.Min = ETipoDisco.HDD;
                    tipoComputer.Almacenamiento.Tipo.Max = ETipoDisco.SSD;

                    tipoComputer.Fuente.PrecioUnidad.Max = 450;
                    tipoComputer.Fuente.PrecioUnidad.Min = 200;
                    tipoComputer.Fuente.Certificacion.Max = ECertificacion.Bronze_80Plus;
                    tipoComputer.Fuente.Certificacion.Min = ECertificacion.None;

                    tipoComputer.Gabinete.PrecioUnidad.Max = 100;
                    tipoComputer.Gabinete.PrecioUnidad.Min = 40;

                    tipoComputer.Monitor.PrecioUnidad.Max = 120;
                    tipoComputer.Monitor.PrecioUnidad.Min = 70;
                    tipoComputer.Monitor.Frecuencia.Max = 60;
                    tipoComputer.Monitor.Frecuencia.Min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.Max = 150;
                    tipoComputer.PlacaBase.PrecioUnidad.Min = 50;
                    tipoComputer.PlacaBase.NumeroDims.Max = 4;
                    tipoComputer.PlacaBase.NumeroDims.Min = 2;

                    tipoComputer.Procesador.PrecioUnidad.Max = 150;
                    tipoComputer.Procesador.PrecioUnidad.Min = 80;
                    tipoComputer.Procesador.Consumo.Max = 120;
                    tipoComputer.Procesador.Consumo.Min = 50;
                    tipoComputer.Procesador.FrecuenciaBase.Max = 3200;
                    tipoComputer.Procesador.FrecuenciaBase.Min = 1800;
                    tipoComputer.Procesador.NumeroNucleos.Max = 6;
                    tipoComputer.Procesador.NumeroNucleos.Min = 4;
                    tipoComputer.Procesador.NumeroHilo.Max = 8;
                    tipoComputer.Procesador.NumeroHilo.Min = 4;

                    tipoComputer.Ram.PrecioUnidad.Max = 80;
                    tipoComputer.Ram.PrecioUnidad.Min = 40;
                    tipoComputer.Ram.Capacidad.Max = 12;
                    tipoComputer.Ram.Capacidad.Min = 8;
                    tipoComputer.Ram.Frecuencia.Max = 2666;
                    tipoComputer.Ram.Frecuencia.Min = 2133;
                    tipoComputer.Ram.Cantidad.Max = 2;
                    tipoComputer.Ram.Cantidad.Min = 1;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.Max = 180;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.Min = 0;
                    tipoComputer.TarjetaGrafica.Vram.Max = 4;
                    tipoComputer.TarjetaGrafica.Vram.Min = 1;
                }
                return tipoComputer;
            }
        }
        private Requirements.TipoComputer GetConfigurationsDefault
        {
            get
            {
                return new Requirements.TipoComputer()
                {
                    Almacenamiento = new Requirements.TipoComputer.AlmacenamientoR()
                    {
                        Capacidad = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>(),
                        Tipo = new Requirements.TipoComputer.MinMax<ETipoDisco>()
                    },
                    Fuente = new Requirements.TipoComputer.FuenteR()
                    {
                        Certificacion = new Requirements.TipoComputer.MinMax<ECertificacion>(),
                        Potencia = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    Gabinete = new Requirements.TipoComputer.GabineteR()
                    {
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    Monitor = new Requirements.TipoComputer.MonitorR()
                    {
                        Frecuencia = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    PlacaBase = new Requirements.TipoComputer.PlacaBaseR()
                    {
                        NumeroDims = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    Procesador = new Requirements.TipoComputer.ProcesadorR()
                    {
                        Consumo = new Requirements.TipoComputer.MinMax<int>(),
                        FrecuenciaBase = new Requirements.TipoComputer.MinMax<int>(),
                        NumeroNucleos = new Requirements.TipoComputer.MinMax<int>(),
                        NumeroHilo = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    Ram = new Requirements.TipoComputer.RamR()
                    {
                        Capacidad = new Requirements.TipoComputer.MinMax<int>(),
                        Frecuencia = new Requirements.TipoComputer.MinMax<int>(),
                        Cantidad = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    },
                    TarjetaGrafica = new Requirements.TipoComputer.TarjetaGraficaR()
                    {
                        Vram = new Requirements.TipoComputer.MinMax<int>(),
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                    }
                };
            }
        }

    }
}
