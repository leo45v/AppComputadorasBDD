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
using WpfAppComputadoras.Administrator.Vistas;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCListProductsView.xaml
    /// </summary>
    public partial class UCItemProductView : UserControl
    {
        private readonly ViewMain mainView;
        public UCProductView uCProcesadorView;
        private readonly Producto producto;
        public UCItemProductView(ViewMain viewMain, Producto producto)
        {
            InitializeComponent();
            mainView = viewMain;
            this.producto = producto;
            LoadProduct(producto);
            btnEliminar.Click += BtnEliminar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnVer.Click += BtnVer_Click;
        }

        private void BtnVer_Click(object sender, RoutedEventArgs e)
        {
            ETipoProducto tipo = ProductosBrl.GetType(producto.IdProducto);
            CreatePageFromProduct(tipo, producto.IdProducto);
            if (!(uCProcesadorView is null))
            {
                uCProcesadorView.Height = 400;
                uCProcesadorView.Width = 640;
                uCProcesadorView.ModoVista();
                uCProcesadorView.ShowDialog();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            ETipoProducto tipo = ProductosBrl.GetType(producto.IdProducto);
            CreatePageFromProduct(tipo, producto.IdProducto);
            if (!(uCProcesadorView is null))
            {
                uCProcesadorView.ShowDialog();
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultOption = MessageBox.Show("Estas Seguro de Eliminar el Producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultOption == MessageBoxResult.Yes)
            {
                if (ProductosBrl.Delete(producto.IdProducto))
                {
                    MessageBox.Show("Producto Eliminado", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    int auxPag = mainView.pagSelect;
                    if (auxPag >= mainView.buttonCant)
                    {
                        auxPag--;
                    }
                    mainView.pagSelect = auxPag;
                    mainView.ConfigAdministradorInterface(mainView.rol.IdRol);
                }
                else
                {
                    MessageBox.Show("Error al Eliminar el producto", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void LoadProduct(Producto producto)
        {
            imgProducto.Source = ViewMain.LoadImage(producto.Imagen);
            nombreProducto.Text = producto.Nombre;
            marcaProducto.Text = producto.Marca.NombreMarca;
            tipoProducto.Text = ProductosBrl.GetType(producto.IdProducto).ToString();
        }
        public void CreatePageFromProduct(ETipoProducto tipo, Guid idProducto)
        {
            if (tipo != ETipoProducto.None)
            {
                uCProcesadorView = new UCProductView(mainView, idProducto, tipo);
            }
        }
    }
}
