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
        ConfigurationBuildComputer getParametros = new ConfigurationBuildComputer(TipoComputadora.Estudio);

        public UCTypeComputerView(ViewMain viewMain)
        {
            InitializeComponent();
            this.mainView = viewMain;

            //MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
            //    getParametros.Requisitos.ComputadoraX.CostoMaximo,
            //    getParametros.Requisitos.ComputadoraX.CostoMinimo), "ESTUDIO");

            //getParametros.CambiarTipo = TipoComputadora.Gaming;
            //MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
            //    getParametros.Requisitos.ComputadoraX.CostoMaximo,
            //    getParametros.Requisitos.ComputadoraX.CostoMinimo), "GAMING");

            //getParametros.CambiarTipo = TipoComputadora.Oficina;
            //MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
            //    getParametros.Requisitos.ComputadoraX.CostoMaximo,
            //    getParametros.Requisitos.ComputadoraX.CostoMinimo), "OFICINA");

            //getParametros.CambiarTipo = TipoComputadora.TrabajoDiseno;
            //MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
            //    getParametros.Requisitos.ComputadoraX.CostoMaximo,
            //    getParametros.Requisitos.ComputadoraX.CostoMinimo), "TRABAJO DISEÑO");

        }
        private void Btn_Siguiente(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtPresupuesto.Text))
            {
                MessageBox.Show("Ingrese un Presupuesto");
                return;
            }
            if (btnGaming.IsChecked.Value)
            {
                getParametros.CambiarTipo = TipoComputadora.Gaming;
                tipoComputadora = TipoComputadora.Gaming;
            }
            else if (btnOficina.IsChecked.Value)
            {
                getParametros.CambiarTipo = TipoComputadora.Oficina;
                tipoComputadora = TipoComputadora.Oficina;
            }
            else if (btnTrabajo.IsChecked.Value)
            {
                getParametros.CambiarTipo = TipoComputadora.TrabajoDiseno;
                tipoComputadora = TipoComputadora.TrabajoDiseno;
            }
            else if (BtnEstudios.IsChecked.Value)
            {
                getParametros.CambiarTipo = TipoComputadora.Estudio;
                tipoComputadora = TipoComputadora.Estudio;
            }
            else { MessageBox.Show("Seleccione un Tipo de Computadora"); return; }
            if (double.TryParse(txtPresupuesto.Text, out presupuesto))
            {
                ComputadoraBuildBrl.Presupuesto = presupuesto;
                bdWaiting.IsOpen = true;
                Thread nuevoHiloProcesos = new Thread(BuscarComputadora);
                nuevoHiloProcesos.Start();
            }
            else
            {
                MessageBox.Show("Ingrese un Presupuesto correcto");
            }

        }
        private void BuscarComputadora()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<Computadora> computadoras = ComputadoraBuildBrl.GetComputersBuild(getParametros.Requisitos.ComputadoraX);
            //List<Computadora> computadorasSinPantalla = computadoras.Select(x => { x.Monitor = null; return x; }).ToList();
            //List<Computadora> nuevito = computadorasSinPantalla.Where(x => x.CostoTotal <= presupuesto && x.TarjetaGrafica.PrecioUnidad > 400).OrderByDescending(x => x.CostoTotal).ToList();
            Computadora filtradoPorPotencia = null;
            if (!(computadoras is null))
            {
                List<Computadora> listaRenovada = computadoras;//.Where(x => x.CostoTotal <= presupuesto).ToList();
                if (listaRenovada.Count > 0)
                {
                    List<Computadora> evitarFuentesMayoresA = null;
                    if (tipoComputadora == TipoComputadora.Gaming)
                    {
                        evitarFuentesMayoresA = listaRenovada
                        .Select(x =>
                        {
                            var f = x.Almacenamientos.Where(y => y.Tipo.Contains("SSD") || y.Tipo.Contains("HDD")).ToList();
                            if (f.Count > 0)
                            {
                                x.Almacenamientos = f;
                            }
                            else { x.Almacenamientos = null; }
                            return x;
                        })
                        .Where(x => !(x.Almacenamientos is null))
                         .Where(x => x.CostoTotal <= presupuesto)
                        .OrderBy(x => x.TarjetaGrafica.PrecioUnidad)
                        .ToList();
                    }
                    else if (tipoComputadora == TipoComputadora.TrabajoDiseno)
                    {
                        evitarFuentesMayoresA = listaRenovada
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
                        .Where(x => x.CostoTotal <= presupuesto)
                        .ToList();
                    }
                    else if (tipoComputadora == TipoComputadora.Oficina)
                    {
                        evitarFuentesMayoresA = listaRenovada
                            .Where(x => x.TarjetaGrafica is null)
                             .Where(x => x.CostoTotal <= presupuesto)
                            .ToList();
                    }
                    else
                    {
                        evitarFuentesMayoresA = listaRenovada
                            .Where(x => (!(x.TarjetaGrafica is null) && x.TarjetaGrafica.PrecioUnidad > 20))
                             .Where(x => x.CostoTotal <= presupuesto)
                            .ToList();
                    }
                    if (evitarFuentesMayoresA.Count > 0)
                    {
                        filtradoPorPotencia = evitarFuentesMayoresA.Last();
                    }
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            int elapsedSeg = (int)(elapsedMs / 1000);
            elapsedMs -= (elapsedSeg * 1000);
            Application.Current.Dispatcher.Invoke(new Action(CloseWaiting));
            Application.Current.Dispatcher.Invoke(new Action<Computadora>(mainView.UIReservaComputer), filtradoPorPotencia);
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

        private void Click_Estudio(object sender, MouseButtonEventArgs e)
        {
            BtnEstudios.IsChecked = true;
        }

        private void Click_TrabajoDiseno(object sender, MouseButtonEventArgs e)
        {
            btnTrabajo.IsChecked = true;
        }

        private void Click_Gaming(object sender, MouseButtonEventArgs e)
        {
            btnGaming.IsChecked = true;
        }

        private void Click_Oficina(object sender, MouseButtonEventArgs e)
        {
            btnOficina.IsChecked = true;
        }
    }
}
