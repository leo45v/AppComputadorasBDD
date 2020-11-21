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
            mainView = uCProductView;
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
