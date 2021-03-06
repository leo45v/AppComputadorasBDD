﻿using System;
using System.Windows;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using WpfAnimatedGif;
using WpfAppComputadoras.Administrator;
using WpfAppComputadoras.Administrator.Vistas;
using WpfAppComputadoras.Extra;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCListProductsView.xaml
    /// </summary>
    public partial class UCItemProductView : UserControl
    {
        #region PROPIEDADES
        private readonly ViewMain mainView;
        public UCProductView uCProcesadorView;
        private readonly Producto producto;
        public bool VistaMode = false;
        #endregion

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

        #region METODOS
        private void BtnVer_Click(object sender, RoutedEventArgs e)
        {
            ETipoProducto tipo = ProductosBrl.GetType(producto.IdProducto);
            VistaMode = true;
            CreatePageFromProduct(tipo, producto.IdProducto);
            if (!(uCProcesadorView is null))
            {
                uCProcesadorView.ModoVista();
                uCProcesadorView.ShowDialog();
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            ETipoProducto tipo = ProductosBrl.GetType(producto.IdProducto);
            VistaMode = false;
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
            ImageBehavior.SetAnimatedSource(imgProducto, Methods.LoadImage(producto.Imagen));
            nombreProducto.Text = producto.Nombre;
            marcaProducto.Text = producto.Marca.NombreMarca;
            tipoProducto.Text = ProductosBrl.GetType(producto.IdProducto).ToString();
        }
        public void CreatePageFromProduct(ETipoProducto tipo, Guid idProducto)
        {
            if (tipo != ETipoProducto.None)
            {
                uCProcesadorView = new UCProductView(VistaMode, mainView, idProducto, tipo);
            }
        }
        public void OcultarEvents()
        {
            productConfig.Visibility = Visibility.Hidden;
        }
        #endregion

    }
}
