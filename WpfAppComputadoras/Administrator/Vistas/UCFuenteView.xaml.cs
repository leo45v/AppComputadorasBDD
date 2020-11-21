using System;
using System.Windows.Controls;

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
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.fuente.Certificacion = ((TextBox)sender).Text; }
        }

        private void TxtPotencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.fuente.Potencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
