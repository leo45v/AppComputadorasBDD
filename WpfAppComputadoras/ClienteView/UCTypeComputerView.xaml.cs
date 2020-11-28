using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.ComputadoraBuild;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCTypeComputerView.xaml
    /// </summary>
    public partial class UCTypeComputerView : UserControl
    {
        public double presupuesto;
        public TipoComputadora tipoComputadora;
        public object parametros;
        public ViewMain mainView;
        public UCTypeComputerView(ViewMain viewMain)
        {
            InitializeComponent();
            this.mainView = viewMain;
        }
        ConfigurationBuildComputer getParametros = new ConfigurationBuildComputer(TipoComputadora.Estudio);
        private void Btn_Siguiente(object sender, RoutedEventArgs e)
        {
            presupuesto = 2000;
            ComputadoraBuildBrl.Presupuesto = presupuesto;
            getParametros.CambiarTipo = TipoComputadora.Gaming;
            bdWaiting.IsOpen = true;
            Thread nuevoHiloProcesos = new Thread(BuscarComputadora);
            nuevoHiloProcesos.Start();
        }
        private void BuscarComputadora()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<Computadora> computadoras = ComputadoraBuildBrl.GetComputersBuild(getParametros.Requisitos.ComputadoraX);
            //List<Computadora> computadorasSinPantalla = computadoras.Select(x => { x.Monitor = null; return x; }).ToList();
            //List<Computadora> nuevito = computadorasSinPantalla.Where(x => x.CostoTotal <= presupuesto && x.TarjetaGrafica.PrecioUnidad > 400).OrderByDescending(x => x.CostoTotal).ToList();

            List<Computadora> listaRenovada = computadoras.Where(x => x.CostoTotal <= presupuesto).OrderBy(x => x.CostoTotal).ToList();
            Computadora masBarata = listaRenovada.First();
            List<Computadora> evitarFuentesMayoresA = listaRenovada
                .Where(x => x.Fuente.Potencia <= (x.ConsumoEstimado + 200))
                .Select(x =>
                {
                    var f = x.Almacenamientos.Where(y => y.Tipo.Contains("SSD")).ToList();
                    if (f.Count > 0)
                    {
                        x.Almacenamientos = f;
                    }
                    else { x.Almacenamientos = null; }
                    return x;
                })
                .Where(x => !(x.Almacenamientos is null))
                .OrderBy(x => x.TarjetaGrafica.PrecioUnidad)
                .ToList();
            Computadora filtradoPorPotencia = evitarFuentesMayoresA.Last();
            Computadora masCercanaAlPresupuesto = listaRenovada.Last();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            int elapsedSeg = (int)(elapsedMs / 1000);
            elapsedMs -= (elapsedSeg * 1000);
            //MessageBox.Show(String.Format("Tiempo Demorado en armar {0}s {1}ms", elapsedSeg, elapsedMs));

            Application.Current.Dispatcher.Invoke(new Action(CloseWaiting));
            Application.Current.Dispatcher.Invoke(new Action<Computadora>(mainView.UIReservaComputer), filtradoPorPotencia);

            //MessageBox.Show(PrintComputer(masBarata));
            //MessageBox.Show(PrintComputer(filtadoPorMinPrecioGrafica));
            //MessageBox.Show(PrintComputer(masCercanaAlPresupuesto));

            //MessageBox.Show(String.Format("Posibles Computadoras: {0}", listaRenovada.Count));
        }

        private void CloseWaiting()
        {
            bdWaiting.IsOpen = false;
        }
        private string PrintComputer(Computadora computer)
        {
            string x = "";
            x += String.Format("PROCESADOR: {0}\n\r   FrecuenciaBase: {1}Mhz   FrecuenciaTurbo: {2}Mhz    Marca: {3}\n\rCosto: {4}$\n\r\n\r",
                computer.Procesador.Nombre, computer.Procesador.FrecuenciaBase, computer.Procesador.FrecuenciaTurbo, computer.Procesador.Marca.NombreMarca, computer.Procesador.PrecioUnidad);
            x += String.Format("PlacaBase: {0}\n\r   NumeroDims: {1}  Marca: {2}    \n\rCosto: {3}$\n\r\n\r",
               computer.PlacaBase.Nombre, computer.PlacaBase.NumeroDims, computer.PlacaBase.Marca.NombreMarca, computer.PlacaBase.PrecioUnidad);
            x += String.Format("Ram: {0}\n\r   Frecuencia: {1}Mhz   Capacidad: {2}GB Cantidad: {3}\n\r  Costo: {4}$\n\r\n\r",
               computer.Rams[0].Nombre, computer.Rams[0].Frecuencia, computer.Rams[0].Memoria, computer.Rams.Count, computer.Rams[0].PrecioUnidad * computer.Rams.Count);
            x += String.Format("Fuente: {0}\n\r   Certificacion: {1}   Potencia: {2}W     Marca: {3}\n\r  Costo: {4}$\n\r\n\r",
               computer.Fuente.Nombre, computer.Fuente.Certificacion, computer.Fuente.Potencia, computer.Fuente.Marca.NombreMarca, computer.Fuente.PrecioUnidad);
            x += String.Format("Monitor: {0}\n\r   Frecuencia: {1}   Tamaño: {2}\"     Marca: {3}\n\r  Costo: {4}$\n\r\n\r",
              computer.Monitor.Nombre, computer.Monitor.Frecuencia, computer.Monitor.Tamano, computer.Monitor.Marca.NombreMarca, computer.Monitor.PrecioUnidad);
            x += String.Format("Gabinete: {0}\n\r   Marca: {1} \n\r  Costo: {2}$\n\r\n\r",
             computer.Gabinete.Nombre, computer.Gabinete.Marca.NombreMarca, computer.Gabinete.PrecioUnidad);
            x += String.Format("Almacenamientos: {0}\n\r Capacidad: {1}   Marca: {2} \n\r  Cantidad: {3}\n\r\n\r",
             computer.Almacenamientos[0].Nombre, computer.Almacenamientos[0].Capacidad, computer.Almacenamientos[0].Marca.NombreMarca, computer.Almacenamientos.Count);
            if (computer.TarjetaGrafica is null)
            {
                x += "SIN TARJETA GRAFICA \n\r\n\r";
            }
            else
            {
                x += String.Format("Tarjeta de Video: {0}\n\r   Marca: {1}   VRam: {3}  \n\r  Costo: {3}$\n\r\n\r",
             computer.TarjetaGrafica.Nombre, computer.TarjetaGrafica.Marca.NombreMarca, computer.TarjetaGrafica.Vram, computer.TarjetaGrafica.PrecioUnidad);
            }
            x += string.Format("TOTAL COSTO: {0}", computer.CostoTotal);
            return x;
        }
    }
}
