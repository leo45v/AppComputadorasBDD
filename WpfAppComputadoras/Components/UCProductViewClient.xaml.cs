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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using WpfAppComputadoras.Administrator;
using WpfAppComputadoras.ClienteView;

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
        ComputerBuildView parent;
        UCReservasView parent2;
        public UCProductViewClient()
        {
            InitializeComponent();

        }
        public UCProductViewClient(ViewMain viewMain, ComputerBuildView computerBuildView, Producto producto)
        {
            InitializeComponent();
            this.mainView = viewMain;
            this.parent = computerBuildView;
            if (!(producto is null))
            {
                this.producto = producto;
                txtNombre.Text = producto.Nombre;
                txtPrecio.Text = producto.PrecioUnidad.ToString("#.##");
                tipoProducto = ProductosBrl.GetType(producto.IdProducto);
                txtTipo.Text = tipoProducto.ToString();
            }
        }
        public UCProductViewClient(ViewMain viewMain, UCReservasView computerBuildView, Producto producto)
        {
            InitializeComponent();
            this.mainView = viewMain;
            this.parent2 = computerBuildView;
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

        private void Btn_Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (parent != null)
            {
                this.parent.QuitarComponente(this, this.producto.IdProducto);
            }
            else if (parent2 != null)
            {
                this.parent2.DeleteProductReserva(this, this.producto.IdProducto);
            }
        }
    }
}
