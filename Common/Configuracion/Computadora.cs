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

        public double CostoTotal()
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
