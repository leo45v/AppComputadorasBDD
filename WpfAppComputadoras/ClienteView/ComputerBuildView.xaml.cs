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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for ComputerBuildView.xaml
    /// </summary>
    public partial class ComputerBuildView : UserControl
    {
        private ViewMain mainView;
        public ComputerBuildView(ViewMain viewMain, Computadora computadora)
        {
            InitializeComponent();
            this.mainView = viewMain;
            if (!(computadora is null))
            {
                Computadora n = new Computadora()
                {
                    Procesador = null,
                    PlacaBase = null,
                    Rams = null,
                    Almacenamientos = null,
                    Fuente = null,
                    Gabinete = null,
                    Monitor = null,
                    TarjetaGrafica = null
                };
                UCProductViewClient uCProcesador = new UCProductViewClient(viewMain, computadora.Procesador);
                containerComputer.Children.Add(uCProcesador);
                UCProductViewClient uCPlacaBase = new UCProductViewClient(viewMain, computadora.PlacaBase);
                containerComputer.Children.Add(uCPlacaBase);
                foreach (var item in computadora.Rams)
                {
                    UCProductViewClient uCRam = new UCProductViewClient(viewMain, item);
                    containerComputer.Children.Add(uCRam);
                }
                foreach (var item in computadora.Almacenamientos)
                {
                    UCProductViewClient uCAlmacenamiento = new UCProductViewClient(viewMain, item);
                    containerComputer.Children.Add(uCAlmacenamiento);
                }
                UCProductViewClient uCFuente = new UCProductViewClient(viewMain, computadora.Fuente);
                containerComputer.Children.Add(uCFuente);
                UCProductViewClient uCGabinete = new UCProductViewClient(viewMain, computadora.Gabinete);
                containerComputer.Children.Add(uCGabinete);
                UCProductViewClient uCMonitor = new UCProductViewClient(viewMain, computadora.Monitor);
                containerComputer.Children.Add(uCMonitor);
                if (!(computadora.TarjetaGrafica is null))
                {
                    UCProductViewClient uCTarjetaGrafica = new UCProductViewClient(viewMain, computadora.TarjetaGrafica);
                    containerComputer.Children.Add(uCTarjetaGrafica);
                }
                txtContoTotal.Text = computadora.CostoTotal.ToString("#.##");
                //UCComputerSpec uCComputerSpec = new UCComputerSpec(viewMain, computadora);
                //containerSpec.Children.Add(uCComputerSpec);
            }
        }

        private void Btn_VolverArmar_Click(object sender, RoutedEventArgs e)
        {
            mainView.gridAutomaitc.Children.Clear();
            mainView.gridAutomaitc.Children.Add(mainView.uCTypeComputerView);
            mainView.ViewModeMainANTERIOR = mainView.ViewModeMain;
            mainView.ViewModeMain = mainView.uCTypeComputerView;
        }
    }
}
