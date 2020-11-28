using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCComputerSpec.xaml
    /// </summary>
    public partial class UCComputerSpec : UserControl
    {
        public UCComputerSpec(ViewMain viewMain, Computadora computadora)
        {
            InitializeComponent();
            LoadComputerSpec(computadora);
        }

        public void LoadComputerSpec(Computadora computadora)
        {
            double almanCapacidad = 0.0;
            string tipos = "";
            foreach (var item in computadora.Almacenamientos)
            {
                almanCapacidad += item.Capacidad;
                tipos += item.Tipo + "  ";
            }
            txtAlmacenamientoCapacidad.Text = (almanCapacidad / 1000).ToString();
            txtAlmacenamientoTipo.Text = tipos;
            txtFuenteCertificacion.Text = computadora.Fuente.Certificacion.ToString();
            txtFuentePotencia.Text = computadora.Fuente.Potencia.ToString();
            if (computadora.TarjetaGrafica is null)
            {
                containerGrafica.Visibility = Visibility.Collapsed;
            }
            else
            {
                containerGrafica.Visibility = Visibility.Visible;
                txtGraficaConsumo.Text = computadora.TarjetaGrafica.Consumo.ToString();
                txtGraficaFreBase.Text = computadora.TarjetaGrafica.FrecuenciaBase.ToString();
                txtGraficaFreTur.Text = computadora.TarjetaGrafica.FrecuenciaTurbo.ToString();
                txtGraficaVram.Text = computadora.TarjetaGrafica.Vram.ToString();
            }
            txtMBCapacidad.Text = computadora.PlacaBase.CapacidadMem.ToString();
            txtMBTam.Text = computadora.PlacaBase.Tamano;
            txtProConsumo.Text = computadora.Procesador.Consumo.ToString();
            txtProFreBas.Text = ((double)(computadora.Procesador.FrecuenciaBase / 1000)).ToString();
            txtProFreTur.Text = ((double)(computadora.Procesador.FrecuenciaTurbo / 1000)).ToString();
            txtProHilo.Text = computadora.Procesador.NumeroHilos.ToString();
            txtProLitografia.Text = computadora.Procesador.Litografia.ToString();
            txtProNucleo.Text = computadora.Procesador.NumeroNucleos.ToString();
            int capacidadRam = 0;
            int frecuenciaRam = 0;
            foreach (var item in computadora.Rams)
            {
                capacidadRam = item.Memoria;
                frecuenciaRam = item.Frecuencia;
            }
            txtRamCantidad.Text = computadora.Rams.Count.ToString();
            txtRamFrecuencia.Text = frecuenciaRam.ToString();
            txtRamMemoria.Text = capacidadRam.ToString();
        }
    }
}
