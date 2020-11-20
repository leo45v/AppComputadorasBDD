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
using WpfAppComputadoras.Administrator.Vistas;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.Administrator
{
    /// <summary>
    /// Interaction logic for AdmiMenuView.xaml
    /// </summary>
    public partial class AdmiMenuView : UserControl
    {
        private ViewMain viewMain;
        private ERol rol;
        List<UCItemProductView> uCItemProductViews = new List<UCItemProductView>();
        public AdmiMenuView(ViewMain viewMainObj, ERol eRol)
        {
            InitializeComponent();
            viewMain = viewMainObj;
            rol = eRol;
        }
        public void CargarProductos(List<Producto> productos)
        {
            lstProductos.Children.Clear();
            uCItemProductViews.Clear();
            foreach (Producto producto in productos)
            {
                UCItemProductView uCItemProductView = new UCItemProductView();
                uCItemProductView.LoadProduct(producto);
                uCItemProductView.Height = 37.5;
                uCItemProductView.btnEliminar.Click += ((s, e) =>
                {
                    MessageBoxResult resultOption = MessageBox.Show("Estas Seguro de Eliminar el Producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (resultOption == MessageBoxResult.Yes)
                    {
                        if (ProductosBrl.Delete(producto.IdProducto))
                        {
                            MessageBox.Show("Producto Eliminado", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            int auxPag = viewMain.pagSelect;
                            viewMain.ConfigAdministradorInterface(rol);
                            int buttonCant = ViewMain.RedondeoSiempre((double)viewMain.maxProducts / 10);
                            if (auxPag >= buttonCant)
                            {
                                auxPag--;
                            }
                            viewMain.pagSelect = auxPag;
                            viewMain.LoadButton(auxPag, buttonCant);
                            viewMain.admiMenuView.CargarProductos(ProductosBrl.GetWithRange(auxPag * 10, 10));
                        }
                        else
                        {
                            MessageBox.Show("Error al Eliminar el producto", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                });
                uCItemProductView.btnEditar.Click += ((s, e) =>
                {
                    string tipo = ProductosBrl.GetType(producto.IdProducto);
                    uCItemProductView.CreatePageFromProduct(tipo, producto.IdProducto);
                    if (!(uCItemProductView.uCProcesadorView is null))
                    {
                        uCItemProductView.uCProcesadorView.btnAction.Click += ((s, e) =>
                        {
                            if (uCItemProductView.uCProcesadorView.btnAction.Content.ToString() == "Actualizar" && ProductosBrl.Procesador.Update(uCItemProductView.uCProcesadorView.procesador))
                            {
                                int buttonCant = ViewMain.RedondeoSiempre((double)viewMain.maxProducts / 10);
                                MessageBox.Show("El Procesador se modifico con exito!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                uCItemProductView.uCProcesadorView.Close();
                                viewMain.LoadButton(viewMain.pagSelect, buttonCant);
                                viewMain.admiMenuView.CargarProductos(ProductosBrl.GetWithRange(viewMain.pagSelect * 10, 10));
                            }
                        });
                        uCItemProductView.uCProcesadorView.ShowDialog();
                    }
                });
                uCItemProductView.btnVer.Click += ((object s, RoutedEventArgs e) =>
                {
                    string tipo = ProductosBrl.GetType(producto.IdProducto);
                    uCItemProductView.CreatePageFromProduct(tipo, producto.IdProducto);
                    if (!(uCItemProductView.uCProcesadorView is null))
                    {
                        uCItemProductView.uCProcesadorView.Height = 400;
                        uCItemProductView.uCProcesadorView.Width = 640;
                        uCItemProductView.uCProcesadorView.ModoVista();
                        uCItemProductView.uCProcesadorView.ShowDialog();
                    }
                });
                uCItemProductViews.Add(uCItemProductView);
                lstProductos.Children.Add(uCItemProductView);
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            cbTipoProducto.Items.Add("Tipo Producto");//0
            cbTipoProducto.Items.Add("Almacenamiento");
            cbTipoProducto.Items.Add("Fuente");
            cbTipoProducto.Items.Add("Gabinete");
            cbTipoProducto.Items.Add("Monitor");
            cbTipoProducto.Items.Add("PlacaBase");
            cbTipoProducto.Items.Add("Procesador");
            cbTipoProducto.Items.Add("Ram");
            cbTipoProducto.Items.Add("Tarjeta Grafica");//8
            cbTipoProducto.SelectedIndex = 0;
        }

        private void BtnInsertarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipoProducto.SelectedIndex > 0)
            {
                UCProductView uCInsertProductView = new UCProductView((ETipoProducto)(cbTipoProducto.SelectedIndex - 1));
                if (!(uCInsertProductView is null))
                {
                    uCInsertProductView.Height = 400;
                    uCInsertProductView.Width = 640;
                    uCInsertProductView.ShowDialog();
                }
            }
        }
    }
}
