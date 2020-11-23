using System;
using System.Windows.Controls;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCMonitorView.xaml
    /// </summary>
    public partial class UCMonitorView : UserControl
    {
        private UCProductView mainView;
        public UCMonitorView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtFrecuencia.Text = uCProductView.monitor.Frecuencia.ToString();
            cbRatio.Text = uCProductView.monitor.Ratio;
            cbResolucion.Text = uCProductView.monitor.Resolucion;
            txtTamano.Text = uCProductView.monitor.Tamano.ToString();
            txtColor.Text = uCProductView.monitor.Tamano.ToString();
        }
        private void ModoVista(bool activo)
        {
            txtFrecuencia.IsEnabled = !activo;
            cbRatio.IsEnabled = !activo;
            cbResolucion.IsEnabled = !activo;
            txtTamano.IsEnabled = !activo;
            txtColor.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuencia.TextChanged += TxtFrecuencia_TextChanged;

            txtTamano.TextChanged += TxtTamano_TextChanged;
        }

        private void TxtTamano_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.monitor.Tamano = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtFrecuencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.monitor.Frecuencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
