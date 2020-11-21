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
    /// Interaction logic for UCPlacaBaseView.xaml
    /// </summary>
    public partial class UCPlacaBaseView : UserControl
    {
        private UCProductView mainView;
        public UCPlacaBaseView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
        }

        private void TxtTamano_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.placaBase.Tamano = ((TextBox)sender).Text; }
        }

        private void TxtSoporteProcesador_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
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

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtCapacidad.TextChanged += TxtCapacidad_TextChanged;
            txtNumeroDims.TextChanged += TxtNumeroDims_TextChanged;
            //txtSoporteProcesador.TextChanged += TxtSoporteProcesador_TextChanged;
            txtTamano.TextChanged += TxtTamano_TextChanged;
        }
    }
}
