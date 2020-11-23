using System;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

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
            txtCertificacion.Items.Add("Tipo Certificacion");
            txtCertificacion.Items.Add(ECertificacion.None.ToString());//0
            txtCertificacion.Items.Add(ECertificacion.White_80Plus.ToString());
            txtCertificacion.Items.Add(ECertificacion.Bronze_80Plus.ToString());
            txtCertificacion.Items.Add(ECertificacion.Silver_80Plus.ToString());
            txtCertificacion.Items.Add(ECertificacion.Gold_80Plus.ToString());
            txtCertificacion.Items.Add(ECertificacion.Platinum_80Plus.ToString());
            txtCertificacion.Items.Add(ECertificacion.Titanium_80Plus.ToString());
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtPotencia.Text = uCProductView.fuente.Potencia.ToString();
            txtCertificacion.SelectedIndex = ((int)uCProductView.fuente.Certificacion) + 1;
        }
        private void ModoVista(bool activo)
        {
            txtPotencia.IsEnabled = !activo;
            txtCertificacion.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtPotencia.TextChanged += TxtPotencia_TextChanged;

            txtCertificacion.SelectionChanged += TxtCertificacion_SelectionChanged; ;
        }

        private void TxtCertificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtCertificacion.SelectedIndex != 0)
            {
                mainView.fuente.Certificacion = (ECertificacion)(txtCertificacion.SelectedIndex - 1);
            }
        }

        private void TxtCertificacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            //{ mainView.fuente.Certificacion = (ECertificacion)((TextBox)sender).Text; }
        }

        private void TxtPotencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.fuente.Potencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
