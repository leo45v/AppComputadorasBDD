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
        private readonly ViewMain viewMain;
        public AdmiMenuView(ViewMain viewMainObj, ERol eRol)
        {
            InitializeComponent();
            viewMain = viewMainObj;
        }
        public void CargarProductos(List<Producto> productos)
        {
            lstProductos.Children.Clear();
            if (!(productos is null))
            {
                foreach (Producto producto in productos)
                {
                    UCItemProductView uCItemProductView = new UCItemProductView(viewMain, producto)
                    {
                        Height = 37.5
                    };
                    lstProductos.Children.Add(uCItemProductView);
                }
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
                UCProductView uCInsertProductView = new UCProductView(viewMain, (ETipoProducto)(cbTipoProducto.SelectedIndex - 1));
                if (!(uCInsertProductView is null))
                {
                    uCInsertProductView.Height = 400;
                    uCInsertProductView.Width = 640;
                    uCInsertProductView.ShowDialog();
                }
            }
        }

        private void TxtSearch_Changed(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                viewMain.queryProductName = null;
            }
            else
            {
                viewMain.queryProductName = txtSearch.Text;
            }
        }

        private void Btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            viewMain.pagSelect = 0;
            viewMain.ConfigAdministradorInterface(viewMain.rol.IdRol);
        }
    }
}
