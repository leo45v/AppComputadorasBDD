using System;
using System.Windows.Controls;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCAlmancenamientoView.xaml
    /// </summary>
    public partial class UCAlmancenamientoView : UserControl
    {
        private UCProductView mainView;
        public UCAlmancenamientoView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
            ModoVista(uCProductView.VistaMode);
            txtCapacidad.Text = uCProductView.almacenamiento.Capacidad.ToString();
            txtEscritura.Text = uCProductView.almacenamiento.Escritura.ToString();
            txtLectura.Text = uCProductView.almacenamiento.Lectura.ToString();
            txtTipo.Text = uCProductView.almacenamiento.Tipo;
        }

        private void ModoVista(bool activo)
        {
            txtCapacidad.IsEnabled = !activo;
            txtEscritura.IsEnabled = !activo;
            txtLectura.IsEnabled = !activo;
            txtTipo.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtCapacidad.TextChanged += TxtCapacidad_TextChanged;
            txtCapacidad.PreviewTextInput += TxtCapacidad_PreviewTextInput;

            txtEscritura.TextChanged += TxtEscritura_TextChanged;
            txtEscritura.PreviewTextInput += TxtCapacidad_PreviewTextInput;

            txtLectura.TextChanged += TxtLectura_TextChanged;
            txtLectura.PreviewTextInput += TxtCapacidad_PreviewTextInput;

            txtTipo.TextChanged += TxtTipo_TextChanged;

        }
        private void TxtCapacidad_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }
        private void TxtTipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Tipo = ((TextBox)sender).Text; }
        }
        private void TxtLectura_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Lectura = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtEscritura_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Escritura = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtCapacidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Capacidad = int.Parse(((TextBox)sender).Text); }
        }
    }
}
