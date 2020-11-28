using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public enum TypeOfRequirements
    {
        Manual = 0,
        JsonFile = 1
    }
    public class ConfigurationBuildComputer
    {
        private Requirements requirements;
        public Requirements Requisitos
        {
            get { return requirements; }
        }
        private TipoComputadora tipoComputadora;
        public TipoComputadora CambiarTipo
        {
            get { return tipoComputadora; }
            set { tipoComputadora = value; this.requirements.ComputadoraX = SetConfigurations; }
        }
        public ConfigurationBuildComputer(TipoComputadora tipoComputadora)
        {
            TypeOfRequirements type = TypeOfRequirements.Manual;
            this.requirements = new Requirements();
            this.tipoComputadora = tipoComputadora;
            if (type == TypeOfRequirements.Manual)
            {
                this.requirements.ComputadoraX = SetConfigurations;
            }
            else
            {
                //RECUPERAR DEL JSON
            }
        }
        public decimal AlmacenamientoCoste
        {
            get
            {
                decimal coste = (decimal)0.0;
                if (this.tipoComputadora == TipoComputadora.Estudio)
                {
                    coste = this.requirements.ComputadoraX.Almacenamiento.PrecioUnidad.CostoReal;
                }
                return coste;
            }
            set
            {
                if (this.tipoComputadora == TipoComputadora.Estudio)
                {
                    this.requirements.ComputadoraX.Almacenamiento.PrecioUnidad.CostoReal = value;
                }
            }
        }

        private Requirements.TipoComputer SetConfigurations
        {
            get
            {
                Requirements.TipoComputer tipoComputer = GetConfigurationsDefault;
                if (this.tipoComputadora == TipoComputadora.Gaming)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.max = 200;
                    tipoComputer.Almacenamiento.PrecioUnidad.min = 50;

                    tipoComputer.Almacenamiento.Capacidad.max = 4000;
                    tipoComputer.Almacenamiento.Capacidad.min = 1000;
                    tipoComputer.Almacenamiento.Tipo.min = ETipoDisco.HDD;
                    tipoComputer.Almacenamiento.Tipo.max = ETipoDisco.SSD;

                    tipoComputer.Fuente.PrecioUnidad.max = 250;
                    tipoComputer.Fuente.PrecioUnidad.min = 120;
                    tipoComputer.Fuente.Certificacion.max = ECertificacion.Platinum_80Plus;
                    tipoComputer.Fuente.Certificacion.min = ECertificacion.Bronze_80Plus;

                    tipoComputer.Gabinete.PrecioUnidad.max = 150;
                    tipoComputer.Gabinete.PrecioUnidad.min = 50;

                    tipoComputer.Monitor.PrecioUnidad.max = 500;
                    tipoComputer.Monitor.PrecioUnidad.min = 250;
                    tipoComputer.Monitor.Frecuencia.max = 144;
                    tipoComputer.Monitor.Frecuencia.min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.max = 350;
                    tipoComputer.PlacaBase.PrecioUnidad.min = 150;
                    tipoComputer.PlacaBase.NumeroDims.max = 4;
                    tipoComputer.PlacaBase.NumeroDims.min = 4;

                    tipoComputer.Procesador.PrecioUnidad.max = 550;
                    tipoComputer.Procesador.PrecioUnidad.min = 200;
                    tipoComputer.Procesador.Consumo.max = 300;
                    tipoComputer.Procesador.Consumo.min = 120;
                    tipoComputer.Procesador.FrecuenciaBase.max = 5000;
                    tipoComputer.Procesador.FrecuenciaBase.min = 3500;
                    tipoComputer.Procesador.NumeroNucleos.max = 12;
                    tipoComputer.Procesador.NumeroNucleos.min = 6;
                    tipoComputer.Procesador.NumeroHilo.max = 12;
                    tipoComputer.Procesador.NumeroHilo.min = 24;

                    tipoComputer.Ram.PrecioUnidad.max = 180;
                    tipoComputer.Ram.PrecioUnidad.min = 70;
                    tipoComputer.Ram.Capacidad.max = 32;
                    tipoComputer.Ram.Capacidad.min = 16;
                    tipoComputer.Ram.Frecuencia.max = 4200;
                    tipoComputer.Ram.Frecuencia.min = 2133;
                    tipoComputer.Ram.Cantidad.max = 4;
                    tipoComputer.Ram.Cantidad.min = 2;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.max = 1500;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.min = 350;
                    tipoComputer.TarjetaGrafica.Vram.max = 24;
                    tipoComputer.TarjetaGrafica.Vram.min = 8;
                }
                else if (this.tipoComputadora == TipoComputadora.Oficina)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.max = 100;
                    tipoComputer.Almacenamiento.PrecioUnidad.min = 40;
                    tipoComputer.Almacenamiento.Capacidad.max = 1000;
                    tipoComputer.Almacenamiento.Capacidad.min = 500;
                    tipoComputer.Almacenamiento.Tipo.min = ETipoDisco.HDD;
                    tipoComputer.Almacenamiento.Tipo.max = ETipoDisco.HDD;

                    tipoComputer.Fuente.PrecioUnidad.max = 120;
                    tipoComputer.Fuente.PrecioUnidad.min = 40;
                    tipoComputer.Fuente.Certificacion.max = ECertificacion.Bronze_80Plus;
                    tipoComputer.Fuente.Certificacion.min = ECertificacion.None;

                    tipoComputer.Gabinete.PrecioUnidad.max = 80;
                    tipoComputer.Gabinete.PrecioUnidad.min = 30;

                    tipoComputer.Monitor.PrecioUnidad.max = 150;
                    tipoComputer.Monitor.PrecioUnidad.min = 50;
                    tipoComputer.Monitor.Frecuencia.max = 60;
                    tipoComputer.Monitor.Frecuencia.min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.max = 150;
                    tipoComputer.PlacaBase.PrecioUnidad.min = 50;
                    tipoComputer.PlacaBase.NumeroDims.max = 2;
                    tipoComputer.PlacaBase.NumeroDims.min = 1;

                    tipoComputer.Procesador.PrecioUnidad.max = 200;
                    tipoComputer.Procesador.PrecioUnidad.min = 50;
                    tipoComputer.Procesador.Consumo.max = 100;
                    tipoComputer.Procesador.Consumo.min = 50;
                    tipoComputer.Procesador.FrecuenciaBase.max = 3000;
                    tipoComputer.Procesador.FrecuenciaBase.min = 1800;
                    tipoComputer.Procesador.NumeroNucleos.max = 4;
                    tipoComputer.Procesador.NumeroNucleos.min = 2;
                    tipoComputer.Procesador.NumeroHilo.max = 8;
                    tipoComputer.Procesador.NumeroHilo.min = 2;

                    tipoComputer.Ram.PrecioUnidad.max = 70;
                    tipoComputer.Ram.PrecioUnidad.min = 30;
                    tipoComputer.Ram.Capacidad.max = 8;
                    tipoComputer.Ram.Capacidad.min = 4;
                    tipoComputer.Ram.Frecuencia.max = 2666;
                    tipoComputer.Ram.Frecuencia.min = 2133;
                    tipoComputer.Ram.Cantidad.max = 2;
                    tipoComputer.Ram.Cantidad.min = 1;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.max = 150;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.min = 0;
                    tipoComputer.TarjetaGrafica.Vram.max = 1;
                    tipoComputer.TarjetaGrafica.Vram.min = 0;
                }
                else if (this.tipoComputadora == TipoComputadora.TrabajoDiseno)
                {
                    tipoComputer.Almacenamiento.PrecioUnidad.max = 700;
                    tipoComputer.Almacenamiento.PrecioUnidad.min = 300;
                    tipoComputer.Almacenamiento.Capacidad.max = 10000;
                    tipoComputer.Almacenamiento.Capacidad.min = 4000;
                    tipoComputer.Almacenamiento.Tipo.min = ETipoDisco.SSD;
                    tipoComputer.Almacenamiento.Tipo.max = ETipoDisco.SSD;

                    tipoComputer.Fuente.PrecioUnidad.max = 350;
                    tipoComputer.Fuente.PrecioUnidad.min = 150;
                    tipoComputer.Fuente.Certificacion.max = ECertificacion.Titanium_80Plus;
                    tipoComputer.Fuente.Certificacion.min = ECertificacion.Gold_80Plus;

                    tipoComputer.Gabinete.PrecioUnidad.max = 250;
                    tipoComputer.Gabinete.PrecioUnidad.min = 120;

                    tipoComputer.Monitor.PrecioUnidad.max = 500;
                    tipoComputer.Monitor.PrecioUnidad.min = 180;
                    tipoComputer.Monitor.Frecuencia.max = 1000;// 
                    tipoComputer.Monitor.Frecuencia.min = 60;

                    tipoComputer.PlacaBase.PrecioUnidad.max = 500;
                    tipoComputer.PlacaBase.PrecioUnidad.min = 150;
                    tipoComputer.PlacaBase.NumeroDims.max = 8;
                    tipoComputer.PlacaBase.NumeroDims.min = 4;

                    tipoComputer.Procesador.PrecioUnidad.max = 4000;
                    tipoComputer.Procesador.PrecioUnidad.min = 400;
                    tipoComputer.Procesador.Consumo.max = 600;
                    tipoComputer.Procesador.Consumo.min = 200;
                    tipoComputer.Procesador.FrecuenciaBase.max = 5000;
                    tipoComputer.Procesador.FrecuenciaBase.min = 3500;
                    tipoComputer.Procesador.NumeroNucleos.max = 64;
                    tipoComputer.Procesador.NumeroNucleos.min = 8;
                    tipoComputer.Procesador.NumeroHilo.max = 128;
                    tipoComputer.Procesador.NumeroHilo.min = 16;

                    tipoComputer.Ram.PrecioUnidad.max = 250;
                    tipoComputer.Ram.PrecioUnidad.min = 150;
                    tipoComputer.Ram.Capacidad.max = 128;
                    tipoComputer.Ram.Capacidad.min = 32;
                    tipoComputer.Ram.Frecuencia.max = 5000;
                    tipoComputer.Ram.Frecuencia.min = 2133;
                    tipoComputer.Ram.Cantidad.max = 8;
                    tipoComputer.Ram.Cantidad.min = 4;

                    tipoComputer.TarjetaGrafica.PrecioUnidad.max = 4200;
                    tipoComputer.TarjetaGrafica.PrecioUnidad.min = 500;
                    tipoComputer.TarjetaGrafica.Vram.max = 48;
                    tipoComputer.TarjetaGrafica.Vram.min = 8;
                }
                return tipoComputer;
            }
        }
        private Requirements.TipoComputer GetConfigurationsDefault
        {
            get// ESTUDIO -> DEFAULT SETTINGS
            {
                return new Requirements.TipoComputer()
                {
                    Almacenamiento = new Requirements.TipoComputer.AlmacenamientoR()
                    {
                        Capacidad = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 500,
                            max = 1000
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 50,
                            max = 100
                        },
                        Tipo = new Requirements.TipoComputer.MinMax<ETipoDisco>()
                        {
                            min = ETipoDisco.HDD,
                            max = ETipoDisco.SSD
                        }
                    },
                    Fuente = new Requirements.TipoComputer.FuenteR()
                    {
                        Certificacion = new Requirements.TipoComputer.MinMax<ECertificacion>()
                        {
                            min = ECertificacion.None,
                            max = ECertificacion.Bronze_80Plus
                        },
                        Potencia = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 200,
                            max = 450
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 50,
                            max = 120
                        }
                    },
                    Gabinete = new Requirements.TipoComputer.GabineteR()
                    {
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 40,
                            max = 100
                        }
                    },
                    Monitor = new Requirements.TipoComputer.MonitorR()
                    {
                        Frecuencia = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 60,
                            max = 60
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 70,
                            max = 120
                        }
                    },
                    PlacaBase = new Requirements.TipoComputer.PlacaBaseR()
                    {
                        NumeroDims = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 2,
                            max = 4
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 50,
                            max = 150
                        }
                    },
                    Procesador = new Requirements.TipoComputer.ProcesadorR()
                    {
                        Consumo = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 50,
                            max = 120
                        },
                        FrecuenciaBase = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 1800,
                            max = 3200
                        },
                        NumeroNucleos = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 4,
                            max = 6
                        },
                        NumeroHilo = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 4,
                            max = 8
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 80,
                            max = 150
                        }
                    },
                    Ram = new Requirements.TipoComputer.RamR()
                    {
                        Capacidad = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 8,
                            max = 12
                        },
                        Frecuencia = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 2133,
                            max = 2666
                        },
                        Cantidad = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 1,
                            max = 2
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 40,
                            max = 80
                        }
                    },
                    TarjetaGrafica = new Requirements.TipoComputer.TarjetaGraficaR()
                    {
                        Vram = new Requirements.TipoComputer.MinMax<double>()
                        {
                            min = 1,
                            max = 4
                        },
                        PrecioUnidad = new Requirements.TipoComputer.MinMax<decimal>()
                        {
                            min = 0,
                            max = 180
                        }
                    }
                };
            }
        }

    }
}
