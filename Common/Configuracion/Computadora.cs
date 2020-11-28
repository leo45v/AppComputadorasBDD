using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion
{
    public class Computadora
    {
        public List<Almacenamiento> Almacenamientos { get; set; }
        public Fuente Fuente { get; set; }
        public Gabinete Gabinete { get; set; }
        public Monitor Monitor { get; set; }
        public PlacaBase PlacaBase { get; set; }
        public Procesador Procesador { get; set; }
        public List<Ram> Rams { get; set; }
        public Grafica TarjetaGrafica { get; set; }
        public int ConsumoEstimado
        {
            get
            {
                int consumo = 0;
                if (!(Almacenamientos is null))
                {
                    consumo += Almacenamientos.Count * 5;
                }
                else
                {
                    consumo += 4 * 5;
                }
                consumo += 50;//PLACA BASE
                if (!(Procesador is null))
                {
                    consumo += Procesador.Consumo;
                }
                if (!(Rams is null))
                {
                    consumo += Rams.Count * 5;
                }
                if (!(TarjetaGrafica is null))
                {
                    consumo += TarjetaGrafica.Consumo;
                }
                return consumo;
            }
        }
        public int DiscoMemoraTotal
        {
            get
            {
                int memoria = 0;
                if (!(Almacenamientos is null))
                {
                    foreach (var item in Almacenamientos)
                    {
                        memoria += item.Capacidad;
                    }
                }
                return memoria;
            }
        }
        public int CantidadMemoriaRam
        {
            get
            {
                int cantidadMemoria = 0;
                if (!(Rams is null))
                {
                    foreach (var item in Rams)
                    {
                        cantidadMemoria += item.Memoria;
                    }
                }
                return cantidadMemoria;
            }
        }
        public double CostoTotal
        {
            get
            {
                double coste = 0.0;
                if (!(this.Almacenamientos is null))
                {
                    foreach (var item in this.Almacenamientos)
                    {
                        coste += (double)item.PrecioUnidad;
                    }
                }
                if (!(this.Fuente is null))
                {
                    coste += (double)this.Fuente.PrecioUnidad;
                }
                if (!(this.Gabinete is null))
                {
                    coste += (double)this.Gabinete.PrecioUnidad;
                }
                if (!(this.Monitor is null))
                {
                    coste += (double)this.Monitor.PrecioUnidad;
                }
                if (!(this.PlacaBase is null))
                {
                    coste += (double)this.PlacaBase.PrecioUnidad;
                }
                if (!(this.Procesador is null))
                {
                    coste += (double)this.Procesador.PrecioUnidad;
                }
                if (!(this.Rams is null))
                {
                    foreach (var item in Rams)
                    {
                        coste += (double)item.PrecioUnidad;
                    }
                }
                if (!(this.TarjetaGrafica is null))
                {
                    coste += (double)this.TarjetaGrafica.PrecioUnidad;
                }
                return coste;
            }
        }
    }
}
