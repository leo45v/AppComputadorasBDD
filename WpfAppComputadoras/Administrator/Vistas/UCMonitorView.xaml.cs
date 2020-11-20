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
    /// Interaction logic for UCMonitorView.xaml
    /// </summary>
    public partial class UCMonitorView : UserControl
    {
        private UCProductView mainView;
        public UCMonitorView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
            txtFrecuencia.Text = uCProductView.monitor.Frecuencia.ToString();
            //txtRatio.Text = uCProductView.monitor.Ratio;
            //txtResolucion.Text = uCProductView.monitor.Resolucion;
            txtTamano.Text = uCProductView.monitor.Tamano.ToString();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtFrecuencia.TextChanged += TxtFrecuencia_TextChanged;

            txtTamano.TextChanged += TxtTamano_TextChanged;
        }

        private void TxtTamano_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TxtFrecuencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
