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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using WpfAppComputadoras.Administrator;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCListProductsView.xaml
    /// </summary>
    public partial class UCItemProductView : UserControl
    {
        private Guid idProducto;
        public UCProcesadorView uCProcesadorView;
        public UCItemProductView()
        {
            InitializeComponent();
        }

        public void LoadProduct(Producto producto)
        {
            imgProducto.Source = ViewMain.LoadImage(producto.Imagen);
            nombreProducto.Text = producto.Nombre;
            marcaProducto.Text = producto.Marca.NombreMarca;
            tipoProducto.Text = ProductosBrl.GetType(producto.IdProducto);
            //if (producto.Descontinuado)
            //{
            //    descontinuadoProducto.Foreground = new SolidColorBrush(Color.FromRgb(216, 48, 163));
            //}
            //else
            //{
            //    descontinuadoProducto.Foreground = new SolidColorBrush(Color.FromRgb(24, 114, 32));
            //}
            idProducto = producto.IdProducto;
        }

        public void CreatePageFromProduct(string tipo, Guid idProducto)
        {
            if (tipo == "Procesador")
            {
                uCProcesadorView = new UCProcesadorView(idProducto);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultOption = MessageBox.Show("Estas Seguro de Eliminar el Producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resultOption == MessageBoxResult.Yes)
            {
                if (ProductosBrl.Delete(idProducto))
                {
                    MessageBox.Show("Producto Eliminado", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Error al Eliminar el producto", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
