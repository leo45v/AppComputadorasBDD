using System;
using System.Windows.Controls;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCTarjetaGraficaView.xaml
    /// </summary>
    public partial class UCTarjetaGraficaView : UserControl
    {
        private UCProductView mainView;
        public UCTarjetaGraficaView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtFrecuenciaBase.Text = uCProductView.tarjetaGrafica.FrecuenciaBase.ToString();
            txtFrecuenciaTurbo.Text = uCProductView.tarjetaGrafica.FrecuenciaTurbo.ToString();
            txtTipoMemoria.Text = uCProductView.tarjetaGrafica.TipoMemoria;
            txtVram.Text = uCProductView.tarjetaGrafica.Vram.ToString();
        }
        private void ModoVista(bool activo)
        {
            txtFrecuenciaBase.IsEnabled = !activo;
            txtFrecuenciaTurbo.IsEnabled = !activo;
            txtTipoMemoria.IsEnabled = !activo;
            txtVram.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuenciaBase.TextChanged += TxtFrecuenciaBase_TextChanged;
            txtFrecuenciaTurbo.TextChanged += TxtFrecuenciaTurbo_TextChanged;
            txtTipoMemoria.TextChanged += TxtTipoMemoria_TextChanged;
            txtVram.TextChanged += TxtVram_TextChanged;
        }

        private void TxtVram_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.tarjetaGrafica.Vram = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtTipoMemoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.tarjetaGrafica.TipoMemoria = ((TextBox)sender).Text; }
        }

        private void TxtFrecuenciaTurbo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.tarjetaGrafica.FrecuenciaTurbo = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtFrecuenciaBase_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.tarjetaGrafica.FrecuenciaBase = int.Parse(((TextBox)sender).Text); }
        }
    }
}
