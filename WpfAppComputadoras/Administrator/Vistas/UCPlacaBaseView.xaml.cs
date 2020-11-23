using System;
using System.Linq;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;

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
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.Tamano = ((TextBox)sender).Text; }
        }
        private void TxtNumeroDims_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.NumeroDims = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtCapacidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.CapacidadMem = int.Parse(((TextBox)sender).Text); }
        }
        private void TxtSoporteProcesador_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtSoporteProcesador.SelectedIndex != 0)
            {
                mainView.placaBase.SoporteProcesador = (SocketProcesador)txtSoporteProcesador.SelectedItem;
            }
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtCapacidad.TextChanged += TxtCapacidad_TextChanged;
            txtNumeroDims.TextChanged += TxtNumeroDims_TextChanged;
            txtSoporteProcesador.SelectionChanged += TxtSoporteProcesador_SelectionChanged;
            txtTamano.TextChanged += TxtTamano_TextChanged;
        }
    }
}
