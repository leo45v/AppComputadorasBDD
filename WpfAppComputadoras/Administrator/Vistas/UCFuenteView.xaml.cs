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
            txtCertificacion.Items.Add(ECertificacion.None.ToString());
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
            txtPotencia.PreviewTextInput += TxtPotencia_PreviewTextInput;

            txtCertificacion.SelectionChanged += TxtCertificacion_SelectionChanged;
        }
        private void TxtPotencia_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }
        private void TxtCertificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtCertificacion.SelectedIndex > 0)
            {
                mainView.fuente.Certificacion = (ECertificacion)(txtCertificacion.SelectedIndex - 1);
            }
        }
        private void TxtPotencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.fuente.Potencia = int.Parse(((TextBox)sender).Text); }
        }
    }
}
