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
                    uCItemProductView.uCProcesadorView.btnAction.Click += ((s, e) =>
                    {
                        if (uCItemProductView.uCProcesadorView.btnAction.Content.ToString() == "Actualizar" && ProcesadorBrl.Update(uCItemProductView.uCProcesadorView.procesador))
                        {
                            int buttonCant = ViewMain.RedondeoSiempre((double)viewMain.maxProducts / 10);
                            MessageBox.Show("El Procesador se modifico con exito!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            uCItemProductView.uCProcesadorView.Close();
                            viewMain.LoadButton(viewMain.pagSelect, buttonCant);
                            viewMain.admiMenuView.CargarProductos(ProductosBrl.GetWithRange(viewMain.pagSelect * 10, 10));
                        }
                    });
                    uCItemProductView.uCProcesadorView.ShowDialog();
                });
                uCItemProductViews.Add(uCItemProductView);
                lstProductos.Children.Add(uCItemProductView);
            }
        }
    }
}
