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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for ComputerBuildView.xaml
    /// </summary>
    public partial class ComputerBuildView : UserControl
    {
        private ViewMain mainView;
        public Computadora computadora;
        //private List<UIElement> componentes = new List<UIElement>();
        public ComputerBuildView(ViewMain viewMain, Computadora computadora)
        {
            InitializeComponent();
            this.mainView = viewMain;
            this.computadora = computadora;
            UpdateUI(computadora);
        }

        private void Btn_VolverArmar_Click(object sender, RoutedEventArgs e)
        {
            mainView.gridAutomaitc.Children.Clear();
            mainView.gridAutomaitc.Children.Add(mainView.uCTypeComputerView);
            mainView.ViewModeMainANTERIOR = mainView.ViewModeMain;
            mainView.ViewModeMain = mainView.uCTypeComputerView;
        }

        private void Btn_Reservar_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            if (!(computadora.Fuente is null))
            {
                productos.Add(computadora.Fuente);
            }
            if (!(computadora.Procesador is null))
            {
                productos.Add(computadora.Procesador);
            }
            if (!(computadora.TarjetaGrafica is null))
            {
                productos.Add(computadora.TarjetaGrafica);
            }
            if (!(computadora.Gabinete is null))
            {
                productos.Add(computadora.Gabinete);
            }
            if (!(computadora.Monitor is null))
            {
                productos.Add(computadora.Monitor);
            }
            if (!(computadora.PlacaBase is null))
            {
                productos.Add(computadora.PlacaBase);
            }
            if (!(computadora.Almacenamientos is null))
            {
                foreach (var item in computadora.Almacenamientos)
                {
                    productos.Add(item);
                }
            }
            if (!(computadora.Rams is null))
            {
                foreach (var item in computadora.Rams)
                {
                    productos.Add(item);
                }
            }
            Reserva nuevaReserva = new Reserva()
            {
                Cliente = mainView.cliente,
                Productos = productos
            };
            if (ReservasBrl.Insert(nuevaReserva))
            {
                computadora = null;
                this.UpdateUI(computadora);
                MessageBox.Show("Reserva Exitosa");
            }
        }

        public void UpdateUI(Computadora computadora)
        {
            containerComputer.Children.Clear();
            if (!(computadora is null))
            {
                UCProductViewClient uCProcesador = new UCProductViewClient(mainView, this, computadora.Procesador);
                containerComputer.Children.Add(uCProcesador);
                UCProductViewClient uCPlacaBase = new UCProductViewClient(mainView, this, computadora.PlacaBase);
                containerComputer.Children.Add(uCPlacaBase);
                foreach (var item in computadora.Rams)
                {
                    UCProductViewClient uCRam = new UCProductViewClient(mainView, this, item);
                    containerComputer.Children.Add(uCRam);
                }
                foreach (var item in computadora.Almacenamientos)
                {
                    UCProductViewClient uCAlmacenamiento = new UCProductViewClient(mainView, this, item);
                    containerComputer.Children.Add(uCAlmacenamiento);
                }
                UCProductViewClient uCFuente = new UCProductViewClient(mainView, this, computadora.Fuente);
                containerComputer.Children.Add(uCFuente);
                UCProductViewClient uCGabinete = new UCProductViewClient(mainView, this, computadora.Gabinete);
                containerComputer.Children.Add(uCGabinete);
                UCProductViewClient uCMonitor = new UCProductViewClient(mainView, this, computadora.Monitor);
                containerComputer.Children.Add(uCMonitor);
                if (!(computadora.TarjetaGrafica is null))
                {
                    UCProductViewClient uCTarjetaGrafica = new UCProductViewClient(mainView, this, computadora.TarjetaGrafica);
                    containerComputer.Children.Add(uCTarjetaGrafica);
                }
                txtContoTotal.Text = computadora.CostoTotal.ToString("#.##");
                //UCComputerSpec uCComputerSpec = new UCComputerSpec(viewMain, computadora);
                //containerSpec.Children.Add(uCComputerSpec);
            }
        }

        public void QuitarComponente(UCProductViewClient uCProductViewClient, Guid idProducto)
        {
            //componentes.Remove(uCProductViewClient);
            containerComputer.Children.Remove(uCProductViewClient);
            computadora.DeleteComponent(idProducto);
        }
    }
}
