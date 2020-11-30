using System;
using System.Linq;
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
        public void DeleteComponent(Guid idProducto)
        {
            if (!(this.Fuente is null) && this.Fuente.IdProducto == idProducto)
            {
                this.Fuente = null;
            }
            else if (!(this.Gabinete is null) && this.Gabinete.IdProducto == idProducto)
            {
                this.Gabinete = null;
            }
            else if (!(this.Monitor is null) && this.Monitor.IdProducto == idProducto)
            {
                this.Monitor = null;
            }
            else if (!(this.PlacaBase is null) && this.PlacaBase.IdProducto == idProducto)
            {
                this.PlacaBase = null;
            }
            else if (!(this.Procesador is null) && this.Procesador.IdProducto == idProducto)
            {
                this.Procesador = null;
            }
            else if (!(this.TarjetaGrafica is null) && this.TarjetaGrafica.IdProducto == idProducto)
            {
                this.TarjetaGrafica = null;
            }
            else if (!(this.Almacenamientos is null))
            {
                this.Almacenamientos = this.Almacenamientos.Where(x => x.IdProducto != idProducto).ToList();
                if (this.Almacenamientos.Count == 0)
                {
                    this.Almacenamientos = null;
                }
            }
            else if (!(this.Rams is null))
            {
                this.Rams = this.Rams.Where(x => x.IdProducto != idProducto).ToList();
                if (this.Rams.Count == 0)
                {
                    this.Rams = null;
                }
            }
        }
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
        public decimal CostoTotal
        {
            get
            {
                decimal coste = (decimal)0.0;
                if (!(this.Almacenamientos is null))
                {
                    foreach (var item in this.Almacenamientos)
                    {
                        coste += item.PrecioUnidad;
                    }
                }
                if (!(this.Fuente is null))
                {
                    coste += this.Fuente.PrecioUnidad;
                }
                if (!(this.Gabinete is null))
                {
                    coste += this.Gabinete.PrecioUnidad;
                }
                if (!(this.Monitor is null))
                {
                    coste += this.Monitor.PrecioUnidad;
                }
                if (!(this.PlacaBase is null))
                {
                    coste += this.PlacaBase.PrecioUnidad;
                }
                if (!(this.Procesador is null))
                {
                    coste += this.Procesador.PrecioUnidad;
                }
                if (!(this.Rams is null))
                {
                    foreach (var item in Rams)
                    {
                        coste += item.PrecioUnidad;
                    }
                }
                if (!(this.TarjetaGrafica is null))
                {
                    coste += this.TarjetaGrafica.PrecioUnidad;
                }
                return coste;
            }
        }
    }
}
