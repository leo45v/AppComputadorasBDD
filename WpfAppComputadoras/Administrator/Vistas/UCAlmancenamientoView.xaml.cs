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
    /// Interaction logic for UCAlmancenamientoView.xaml
    /// </summary>
    public partial class UCAlmancenamientoView : UserControl
    {
        private UCProductView mainView;
        public UCAlmancenamientoView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
            txtCapacidad.Text = uCProductView.almacenamiento.Capacidad.ToString();
            txtEscritura.Text = uCProductView.almacenamiento.Escritura.ToString();
            txtLectura.Text = uCProductView.almacenamiento.Lectura.ToString();
            txtTipo.Text = uCProductView.almacenamiento.Tipo;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtCapacidad.TextChanged += TxtCapacidad_TextChanged;

            txtEscritura.TextChanged += TxtEscritura_TextChanged;

            txtLectura.TextChanged += TxtLectura_TextChanged;

            txtTipo.TextChanged += TxtTipo_TextChanged;

        }

        private void TxtTipo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Tipo = ((TextBox)sender).Text; }
        }

        private void TxtLectura_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Lectura = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtEscritura_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Escritura = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtCapacidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.almacenamiento.Capacidad = int.Parse(((TextBox)sender).Text); }
        }
    }
}
