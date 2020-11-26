using System;
using System.Windows.Controls;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCRamView.xaml
    /// </summary>
    public partial class UCRamView : UserControl
    {
        private UCProductView mainView;
        public UCRamView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtFrecuencia.Text = uCProductView.ram.Frecuencia.ToString();
            txtLatencia1.Text = uCProductView.ram.Latencia.ToString();
            txtMemoria.Text = uCProductView.ram.Memoria.ToString();
        }
        private void ModoVista(bool activo)
        {
            txtFrecuencia.IsEnabled = !activo;
            txtLatencia1.IsEnabled = !activo;
            txtMemoria.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuencia.TextChanged += TxtFrecuencia_TextChanged;
            txtFrecuencia.PreviewTextInput += TxtFrecuencia_PreviewTextInput;
            txtLatencia1.TextChanged += TxtLatencia1_TextChanged;
            txtLatencia1.PreviewTextInput += TxtFrecuencia_PreviewTextInput;
            txtMemoria.TextChanged += TxtMemoria_TextChanged;
            txtMemoria.PreviewTextInput += TxtFrecuencia_PreviewTextInput;
        }

        private void TxtFrecuencia_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }

        private void TxtMemoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Memoria = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtLatencia1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Latencia = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtFrecuencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Frecuencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
