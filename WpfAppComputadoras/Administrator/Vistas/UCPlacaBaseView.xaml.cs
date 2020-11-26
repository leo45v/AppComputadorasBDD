using System;
using System.Linq;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCPlacaBaseView.xaml
    /// </summary>
    public partial class UCPlacaBaseView : UserControl
    {
        private UCProductView mainView;
        public UCPlacaBaseView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtCapacidad.Text = uCProductView.placaBase.CapacidadMem.ToString();
            txtNumeroDims.Text = uCProductView.placaBase.NumeroDims.ToString();
            if (!(uCProductView.mainView.SocketsList is null))
            {
                txtSoporteProcesador.ItemsSource = uCProductView.mainView.SocketsList;
                txtSoporteProcesador.DisplayMemberPath = "NombreSocket";
                txtSoporteProcesador.SelectedValuePath = "IdSocket";
            }
            //int indexp = uCProductView.mainView.SocketsList.FindIndex(x => x.IdSocket == uCProductView.placaBase.SoporteProcesador.IdSocket);
            txtSoporteProcesador.SelectedValue = uCProductView.placaBase.SoporteProcesador.IdSocket;
            txtTamano.Text = uCProductView.placaBase.Tamano;
        }
        private void ModoVista(bool activo)
        {
            txtCapacidad.IsEnabled = !activo;
            txtNumeroDims.IsEnabled = !activo;
            txtSoporteProcesador.IsEnabled = !activo;
            txtTamano.IsEnabled = !activo;
        }
        private void TxtTamano_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.Tamano = ((TextBox)sender).Text; }
        }
        private void TxtNumeroDims_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.NumeroDims = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtCapacidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.CapacidadMem = int.Parse(((TextBox)sender).Text); }
        }
        private void TxtSoporteProcesador_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtSoporteProcesador.SelectedIndex >= 0)
            {
                mainView.placaBase.SoporteProcesador = (SocketProcesador)txtSoporteProcesador.SelectedItem;
            }
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtCapacidad.TextChanged += TxtCapacidad_TextChanged;
            txtCapacidad.PreviewTextInput += TxtCapacidad_PreviewTextInput;
            txtNumeroDims.TextChanged += TxtNumeroDims_TextChanged;
            txtNumeroDims.PreviewTextInput += TxtCapacidad_PreviewTextInput;
            txtSoporteProcesador.SelectionChanged += TxtSoporteProcesador_SelectionChanged;
            txtTamano.TextChanged += TxtTamano_TextChanged;
        }

        private void TxtCapacidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }

        private void Btn_RefreshAddSocket_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.mainView.mainView.SocketsList = ConfiguracionesBrl.Socket.GetAll();
            if (!(this.mainView.mainView.SocketsList is null))
            {
                txtSoporteProcesador.ItemsSource = this.mainView.mainView.SocketsList;
                txtSoporteProcesador.DisplayMemberPath = "NombreSocket";
                txtSoporteProcesador.SelectedValuePath = "IdSocket";
            }
            txtSoporteProcesador.SelectedValue = this.mainView.placaBase.SoporteProcesador.IdSocket;
        }
    }
}
