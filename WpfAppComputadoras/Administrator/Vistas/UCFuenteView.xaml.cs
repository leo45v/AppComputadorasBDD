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
    /// Interaction logic for UCFuenteView.xaml
    /// </summary>
    public partial class UCFuenteView : UserControl
    {
        private UCProductView mainView;
        public UCFuenteView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
            txtPotencia.Text = uCProductView.fuente.Potencia.ToString();
            txtCertificacion.Text = uCProductView.fuente.Certificacion;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtPotencia.TextChanged += TxtPotencia_TextChanged;

            txtCertificacion.TextChanged += TxtCertificacion_TextChanged;
        }

        private void TxtCertificacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtPotencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
