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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for ComputerBuildView.xaml
    /// </summary>
    public partial class ComputerBuildView : UserControl
    {
        #region PROPIEDADES
        private ViewMain mainView;
        public Computadora computadora;
        #endregion


        public ComputerBuildView(ViewMain viewMain, Computadora computadora)
        {
            InitializeComponent();
            this.mainView = viewMain;
            this.computadora = computadora;
            UpdateUI(computadora);
        }



        #region METODOS
        private void Btn_VolverArmar_Click(object sender, RoutedEventArgs e)
        {
            mainView.gridAutomaitc.Children.Clear();
            mainView.gridAutomaitc.Children.Add(mainView.uCTypeComputerView);
            mainView.ViewModeMainANTERIOR = mainView.ViewModeMain;
            mainView.ViewModeMain = mainView.uCTypeComputerView;
        }
        private void Btn_Reservar_Click(object sender, RoutedEventArgs e)
        {
            ListaProductos productos = new ListaProductos();

            productos.InsertarComputadora(computadora);

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
            else
            {
                MessageBox.Show(ReservasBrl.Errores[0]);
            }
        }
        public void UpdateUI(Computadora computadora)
        {
            containerComputer.Children.Clear();
            containerCosto.Visibility = Visibility.Hidden;
            btnReserva.IsEnabled = false;
            if (!(computadora is null))
            {
                containerCosto.Visibility = Visibility.Visible;
                btnReserva.IsEnabled = true;
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
            }
        }
        public void QuitarComponente(UCProductViewClient uCProductViewClient, Guid idProducto)
        {
            containerComputer.Children.Remove(uCProductViewClient);
            computadora.DeleteComponent(idProducto);
        }
        #endregion
    }
}
