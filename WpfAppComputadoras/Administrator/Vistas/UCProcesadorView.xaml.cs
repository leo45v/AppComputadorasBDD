using System;
using System.Windows.Controls;
using System.Windows.Input;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCProcesadorView.xaml
    /// </summary>
    public partial class UCProcesadorView : UserControl
    {
        private readonly UCProductView mainView;
        public UCProcesadorView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            if (!(uCProductView.mainView.SocketsList is null))
            {
                cbSocket.ItemsSource = uCProductView.mainView.SocketsList;
                cbSocket.DisplayMemberPath = "NombreSocket";
                cbSocket.SelectedValuePath = "IdSocket";
            }
            this.mainView = uCProductView;
            txtConsumo.Text = uCProductView.procesador.Consumo.ToString();
            txtFrecuenciaBase.Text = uCProductView.procesador.FrecuenciaBase.ToString();
            txtFrecuenciaTurbo.Text = uCProductView.procesador.FrecuenciaTurbo.ToString();
            txtLitografia.Text = uCProductView.procesador.Litografia.ToString();
            txtNumeroHilos.Text = uCProductView.procesador.NumeroHilos.ToString();
            txtNumeroNucleos.Text = uCProductView.procesador.NumeroNucleos.ToString();
            cbSocket.SelectedValue = uCProductView.procesador.Socket.IdSocket;
        }
        private void ModoVista(bool activo)
        {
            txtConsumo.IsEnabled = !activo;
            txtFrecuenciaBase.IsEnabled = !activo;
            txtFrecuenciaTurbo.IsEnabled = !activo;
            txtLitografia.IsEnabled = !activo;
            txtNumeroHilos.IsEnabled = !activo;
            txtNumeroNucleos.IsEnabled = !activo;
            cbSocket.IsEnabled = !activo;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            txtConsumo.PreviewTextInput += Prevent_PreviewTextInput;
            txtConsumo.TextChanged += TxtConsumo_TextChanged;

            txtFrecuenciaBase.PreviewTextInput += Prevent_PreviewTextInput;
            txtFrecuenciaBase.TextChanged += TxtFrecuenciaBase_TextChanged;

            txtFrecuenciaTurbo.PreviewTextInput += Prevent_PreviewTextInput;
            txtFrecuenciaTurbo.TextChanged += TxtFrecuenciaTurbo_TextChanged;

            txtLitografia.PreviewTextInput += Prevent_PreviewTextInput;
            txtLitografia.TextChanged += TxtLitografia_TextChanged;

            txtNumeroNucleos.PreviewTextInput += Prevent_PreviewTextInput;
            txtNumeroNucleos.TextChanged += TxtNumeroNucleos_TextChanged;


            txtNumeroHilos.PreviewTextInput += Prevent_PreviewTextInput;
            txtNumeroHilos.TextChanged += TxtNumeroHilos_TextChanged;

            cbSocket.SelectionChanged += CbSocket_SelectionChanged;
        }

        private void CbSocket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSocket.SelectedIndex != 0)
            {
                mainView.procesador.Socket = (SocketProcesador)cbSocket.SelectedItem;
            }
        }

        private void TxtNumeroHilos_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.NumeroHilos = int.Parse(((TextBox)s).Text); }
        }

        private void TxtNumeroNucleos_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.NumeroNucleos = int.Parse(((TextBox)s).Text); }
        }

        private void TxtLitografia_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.Litografia = int.Parse(((TextBox)s).Text); }
        }

        private void TxtFrecuenciaTurbo_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.FrecuenciaTurbo = int.Parse(((TextBox)s).Text); }
        }

        private void TxtFrecuenciaBase_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.FrecuenciaBase = int.Parse(((TextBox)s).Text); }
        }

        private void TxtConsumo_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { mainView.procesador.Consumo = int.Parse(((TextBox)s).Text); }
        }

        private void Prevent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 4))
            { e.Handled = true; }
        }
    }
}
