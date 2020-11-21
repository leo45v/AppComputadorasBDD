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
            mainView = uCProductView;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuencia.TextChanged += TxtFrecuencia_TextChanged;
            txtLatencia1.TextChanged += TxtLatencia1_TextChanged;
            txtMemoria.TextChanged += TxtMemoria_TextChanged;
        }

        private void TxtMemoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Memoria = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtLatencia1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Latencia = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtFrecuencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.ram.Frecuencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
