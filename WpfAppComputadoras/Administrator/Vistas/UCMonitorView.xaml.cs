using System;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

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

            cbResolucion.ItemsSource = uCProductView.mainView.ListaResolucion;
            cbResolucion.DisplayMemberPath = "NombreResolucion";
            cbResolucion.SelectedValuePath = "IdResolucion";
            cbRatio.ItemsSource = uCProductView.mainView.ListaRatio;
            cbRatio.DisplayMemberPath = "NombreRatio";
            cbRatio.SelectedValuePath = "IdRatio";

            mainView = uCProductView;
            txtFrecuencia.Text = uCProductView.monitor.Frecuencia.ToString();
            cbRatio.SelectedValue = uCProductView.monitor.Ratio.NombreRatio;
            cbResolucion.SelectedValue = uCProductView.monitor.Resolucion.NombreResolucion;
            txtTamano.Text = uCProductView.monitor.Tamano.ToString();
            //txtColor.Text = uCProductView.monitor.Tamano.ToString();
        }
        private void ModoVista(bool activo)
        {
            txtFrecuencia.IsEnabled = !activo;
            cbRatio.IsEnabled = !activo;
            cbResolucion.IsEnabled = !activo;
            txtTamano.IsEnabled = !activo;
            //txtColor.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuencia.TextChanged += TxtFrecuencia_TextChanged;
            txtFrecuencia.PreviewTextInput += TxtFrecuencia_PreviewTextInput;

            txtTamano.TextChanged += TxtTamano_TextChanged;
            txtTamano.PreviewTextInput += TxtFrecuencia_PreviewTextInput;

            cbRatio.SelectionChanged += CbRatio_SelectionChanged;
            cbResolucion.SelectionChanged += CbResolucion_SelectionChanged;
        }

        private void TxtFrecuencia_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }

        private void CbResolucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbResolucion.SelectedIndex >= 0)
            {
                mainView.monitor.Resolucion = (Resolucion)cbResolucion.SelectedItem;
            }
        }

        private void CbRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRatio.SelectedIndex >= 0)
            {
                mainView.monitor.Ratio = (Ratio)cbRatio.SelectedItem;
            }
        }

        private void TxtTamano_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.monitor.Tamano = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtFrecuencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.monitor.Frecuencia = int.Parse(((TextBox)sender).Text); }
        }

        private void Btn_RefreshAddResolucion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.mainView.mainView.ListaResolucion = ConfiguracionesBrl.Resolucion.GetAll();
            cbResolucion.ItemsSource = this.mainView.mainView.ListaResolucion;
            cbResolucion.DisplayMemberPath = "NombreResolucion";
            cbResolucion.SelectedValuePath = "IdResolucion";
            cbResolucion.SelectedValue = this.mainView.monitor.Resolucion.NombreResolucion;
        }

        private void Btn_RefreshAddRatio_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.mainView.mainView.ListaRatio = ConfiguracionesBrl.Ratio.GetAll();
            cbRatio.ItemsSource = this.mainView.mainView.ListaRatio;
            cbRatio.DisplayMemberPath = "NombreRatio";
            cbRatio.SelectedValuePath = "IdRatio";
            cbRatio.SelectedValue = this.mainView.monitor.Ratio.NombreRatio;
        }
    }
}
