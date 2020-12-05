using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using WpfAppComputadoras.Components;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Listas;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCReservasView.xaml
    /// </summary>
    public partial class UCReservasView : UserControl
    {

        #region PROPIEDADES
        public ViewMain mainView;
        public Cliente cliente;
        public Reserva reservaActualInView;
        public List<Reserva> reservas;
        public UCReservaView uCReservaViewActual;
        private Button ActualSelect;
        #endregion


        #region CONSTRUCTOR
        public UCReservasView()
        {
            InitializeComponent();
        }
        public UCReservasView(ViewMain viewMain, Cliente cliente)
        {
            InitializeComponent();
            this.mainView = viewMain;
            this.cliente = cliente;
            UpdateReservas();
        }
        #endregion


        #region METODOS
        public void UpdateReservas()
        {
            containerPriceView.Visibility = Visibility.Hidden;
            containerReservas.Children.Clear();
            reservas = ReservasBrl.GetReservas(cliente.IdPersona);
            if (reservas != null)
            {
                foreach (var item in reservas)
                {
                    UCReservaView uCReservaView = new UCReservaView(this.mainView, this, item);
                    containerReservas.Children.Add(uCReservaView);
                }
            }
        }
        public void DeleteReservaUI(UCReservaView uCReservaView)
        {
            containerReservas.Children.Remove(uCReservaView);
        }
        public void DeleteProductReserva(UCProductViewClient uCProductViewClient, Guid idProducto)
        {
            int index = reservas.IndexOf(this.reservaActualInView);
            int index2 = containerReservas.Children.IndexOf(uCReservaViewActual);
            decimal costoTotal = Decimal.Parse(txtContoTotal.Text);
            if (reservas[index] != null)
            {
                bool noInsertado = false;
                ListaProductos listita = new ListaProductos();
                foreach (var item in reservas[index].Productos)
                {
                    if (item.IdProducto == idProducto && !noInsertado)
                    {
                        costoTotal -= item.PrecioUnidad;
                        noInsertado = true;
                    }
                    else
                    {
                        listita.Add(item);
                    }
                }
                reservas[index].Productos = listita;
            }
            ((UCReservaView)containerReservas.Children[index2]).reserva = reservas[index];
            containerViewReserva.Children.Remove(uCProductViewClient);
            ReservasBrl.QuitarProducto(idProducto, this.reservaActualInView.IdReserva);
            UpdateCost(reservas[index]);
        }
        public void UpdateCost(Reserva reserva)
        {
            if (reserva != null && reserva.Productos != null)
            {
                txtContoTotal.Text = reserva.Productos.CostoTotal.ToString("#.00");
            }
        }
        public void MostrarDetalleReserva(UCReservaView uCReservaView, Reserva reserva)
        {
            containerPriceView.Visibility = Visibility.Visible;
            if (ActualSelect != null)
            {
                int index = -1;
                int i = 0;
                foreach (UCReservaView item in containerReservas.Children)
                {
                    if (item.btnVerDetalle == ActualSelect)
                    {
                        index = i;
                    }
                    i++;
                }
                if (index != -1)
                {
                    ((UCReservaView)containerReservas.Children[index]).btnVerDetalle.IsEnabled = true;
                }
            }
            ActualSelect = uCReservaView.btnVerDetalle;
            uCReservaViewActual = uCReservaView;
            this.reservaActualInView = reserva;
            containerViewReserva.Children.Clear();
            if (reserva.Productos != null)
            {
                decimal costoTotal = new Decimal(0.0);
                foreach (var item in reserva.Productos)
                {
                    UCProductViewClient uCProductViewClient = new UCProductViewClient(this.mainView, this, item);
                    containerViewReserva.Children.Add(uCProductViewClient);
                    costoTotal += item.PrecioUnidad;
                }
                txtContoTotal.Text = reserva.Productos.CostoTotal.ToString("#.00");
            }
        }
        private void Btn_Atras_Click(object sender, RoutedEventArgs e)
        {
            mainView.btnConfigurar.IsEnabled = true;
            mainView.gridAutomaitc.Children.Clear();
            if (mainView.rol.IdRol == ERol.Cliente)
            {
                mainView.gridAutomaitc.Children.Add(mainView.uCTypeComputerView);
                mainView.ViewModeMain = mainView.uCTypeComputerView;
            }
            else if (mainView.rol.IdRol == ERol.Administrador)
            {
                //-> VISTA CLIENTES
            }
            mainView.ViewModeMainANTERIOR = this;
        }
        private void Btn_ActualizarReservas_Click(object sender, RoutedEventArgs e)
        {
            mainView.uCReservasView.UpdateReservas();
            mainView.LoadReservasUI();
            containerViewReserva.Children.Clear();
        }
        #endregion
    }
}
