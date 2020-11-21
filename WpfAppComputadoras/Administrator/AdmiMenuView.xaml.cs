using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using WpfAppComputadoras.Administrator.Vistas;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.Administrator
{
    /// <summary>
    /// Interaction logic for AdmiMenuView.xaml
    /// </summary>
    public partial class AdmiMenuView : UserControl
    {
        public readonly ViewMain viewMain;
        public AdmiMenuView(ViewMain viewMainObj, ERol eRol)
        {
            viewMain = viewMainObj;
            InitializeComponent();
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


            List<Marca> listita = viewMain.MarcaList;
            listita.Insert(0, new Marca() { IdMarca = 0, NombreMarca = "Tipo Marca" });

            cbMarca.ItemsSource = viewMain.MarcaList;
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
            cbMarca.SelectedIndex = 0;
            //cbMarca.SelectedIndex = cbMarca.Items.Count - 1;
            //cbMarca.Items.MoveCurrentToFirst();

            cbTipoProducto.Items.Cast<object>().ToList().ForEach(i =>
                cbTipo.Items.Add(i)
            );
            cbTipo.SelectedIndex = 0;
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
            Marca marcaSelect = (Marca)cbMarca.SelectedItem;
            if (marcaSelect.IdMarca != 0)
            {
                viewMain.queryProductMarca = marcaSelect;
            }
            else
            {
                viewMain.queryProductMarca = null;
            }
            viewMain.queryProductTipo = (ETipoProducto)cbTipo.SelectedIndex - 1;
            viewMain.pagSelect = 0;
            viewMain.ConfigAdministradorInterface(viewMain.rol.IdRol);
        }

        private void Btn_LimpiarBuscador_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            cbTipo.SelectedIndex = 0;
            cbMarca.SelectedIndex = 0;
            viewMain.queryProductName = "";
            viewMain.queryProductMarca = null;
            viewMain.queryProductTipo = (ETipoProducto)cbTipo.SelectedIndex - 1;
            viewMain.pagSelect = 0;
            viewMain.ConfigAdministradorInterface(viewMain.rol.IdRol);
        }
    }
}
