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
    /// Interaction logic for UCGabineteView.xaml
    /// </summary>
    public partial class UCGabineteView : UserControl
    {
        private UCProductView mainView;
        public UCGabineteView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
            txtAltura.Text = uCProductView.gabinete.Altura.ToString();
            txtLargo.Text = uCProductView.gabinete.Largo.ToString();
            txtPeso.Text = uCProductView.gabinete.Peso.ToString();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtAltura.TextChanged += TxtAltura_TextChanged;

            txtLargo.TextChanged += TxtLargo_TextChanged;

            txtPeso.TextChanged += TxtPeso_TextChanged;
        }

        private void TxtPeso_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtLargo_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtAltura_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
