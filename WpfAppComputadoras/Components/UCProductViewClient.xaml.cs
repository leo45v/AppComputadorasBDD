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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using WpfAppComputadoras.Administrator;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCProductViewClient.xaml
    /// </summary>
    public partial class UCProductViewClient : UserControl
    {
        ETipoProducto tipoProducto;
        Producto producto;
        ViewMain mainView;
        public UCProductViewClient(ViewMain viewMain, Producto producto)
        {
            InitializeComponent();
            this.mainView = viewMain;
            if (!(producto is null))
            {
                this.producto = producto;
                txtNombre.Text = producto.Nombre;
                txtPrecio.Text = producto.PrecioUnidad.ToString("#.##");
                tipoProducto = ProductosBrl.GetType(producto.IdProducto);
                txtTipo.Text = tipoProducto.ToString();
            }
        }

        private void Btn_ShowProduct(object sender, RoutedEventArgs e)
        {
            UCProductView uCProcesadorView = new UCProductView(true, mainView, producto.IdProducto, tipoProducto);
            if (!(uCProcesadorView is null))
            {
                uCProcesadorView.ModoVista();
                uCProcesadorView.ShowDialog();
            }
        }
    }
}
