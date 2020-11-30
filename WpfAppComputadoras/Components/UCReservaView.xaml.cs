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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using WpfAppComputadoras.ClienteView;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCReservaView.xaml
    /// </summary>
    public partial class UCReservaView : UserControl
    {
        public ViewMain mainView;
        public UCReservasView parent;
        public Reserva reserva;
        public UCReservaView()
        {
            InitializeComponent();
        }
        public UCReservaView(ViewMain viewMain, UCReservasView uCReservasView, Reserva reserva)
        {
            InitializeComponent();
            this.parent = uCReservasView;
            this.mainView = viewMain;
            this.reserva = reserva;
            txtNombreCliente.Text = reserva.Cliente.ToString();
            txtFechaReserva.Text = reserva.FechaReserva.ToString();
            if (viewMain.rol.IdRol == ERol.Administrador)
            {
                txtNombreCliente.Visibility = Visibility.Visible;
            }
            else
            {
                txtNombreCliente.Visibility = Visibility.Collapsed;
            }
        }

        private void Btn_DeleteReserva_Click(object sender, RoutedEventArgs e)
        {
            ReservasBrl.Delete(reserva.IdReserva);
            this.parent.DeleteReservaUI(this);
        }

        private void Btn_VerDetalle_Click(object sender, RoutedEventArgs e)
        {
            this.parent.MostrarDetalleReserva(this, this.reserva);
            btnVerDetalle.IsEnabled = false;
        }
        public void UptadeReserva(Reserva reserva)
        {
            this.reserva = reserva;
        }
    }
}
