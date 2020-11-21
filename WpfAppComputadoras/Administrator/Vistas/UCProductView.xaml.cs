using Microsoft.Win32;
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

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCProductView.xaml
    /// </summary>
    public partial class UCProductView : Window
    {
        public Almacenamiento almacenamiento = new Almacenamiento()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public Fuente fuente = new Fuente()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public Gabinete gabinete = new Gabinete()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public Monitor monitor = new Monitor()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public PlacaBase placaBase = new PlacaBase()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public Procesador procesador = new Procesador()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        public Ram ram = new Ram();
        public Grafica tarjetaGrafica = new Grafica()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };

        private Producto producto = new Producto()
        {
            Marca = new Marca(),
            Descontinuado = false,
            Eliminado = false,
        };
        private ETipoProducto tipoProducto;

        private List<Marca> marcas;
        public ViewMain mainView;

        public UCProductView(ViewMain viewMain, ETipoProducto tipoProducto)
        {
            InitializeComponent();
            mainView = viewMain;
            LoadComboMarcas();
            btnAction.Content = "Insertar";
            containerTipo.Children.Clear();
            this.tipoProducto = tipoProducto;
            LoadInterface(Guid.Empty);
            lblTitle.Text = "INGRESE LOS DATOS PARA INSERTAR EL " + tipoProducto.ToString().ToUpper();
        }
        public void ModoVista()
        {
            Modo("vista");
        }
        private void Modo(string tipo)
        {
            bool noVista = true;
            if (tipo == "vista")
            {
                noVista = false;
            }
            cbMarca.IsEnabled = noVista;
            txtNombre.IsEnabled = noVista;
            txtPrecioUnidad.IsEnabled = noVista;
            txtStock.IsEnabled = noVista;
            btnAction.IsEnabled = noVista;
            btnLoadImg.IsEnabled = noVista;
            if (noVista) { btnAction.Visibility = Visibility.Visible; btnLoadImg.Visibility = Visibility.Visible; }
            else { btnAction.Visibility = Visibility.Hidden; btnLoadImg.Visibility = Visibility.Hidden; }
        }
        public UCProductView(ViewMain viewMain, Guid idProducto, ETipoProducto tipoProducto)
        {
            InitializeComponent();
            mainView = viewMain;
            LoadComboMarcas();
            btnAction.Content = "Actualizar";
            containerTipo.Children.Clear();
            this.tipoProducto = tipoProducto;
            LoadInterface(idProducto);
            lblTitle.Text = "MODIFIQUE LOS DATOS PARA EL " + tipoProducto.ToString().ToUpper();
        }
        public void LoadInterface(Guid idProducto)
        {
            bool modoInsert = false;
            if (idProducto == Guid.Empty)
            {
                modoInsert = true;
            }

            if (tipoProducto == ETipoProducto.Procesador)
            {
                if (!modoInsert)
                {
                    procesador = ProductosBrl.Procesador.Get(idProducto);
                    producto = procesador as Producto;
                }
                UCProcesadorView uCViewTypeProduct = new UCProcesadorView(this);
                containerTipo.Children.Add(uCViewTypeProduct);

            }
            else if (tipoProducto == ETipoProducto.Ram)
            {
                if (!modoInsert)
                {
                    ram = ProductosBrl.Ram.Get(idProducto);
                    producto = ram as Producto;
                }
                UCRamView uCViewTypeProduct = new UCRamView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.Almacenamiento)
            {
                if (!modoInsert)
                {
                    almacenamiento = ProductosBrl.Almacenamiento.Get(idProducto);
                    producto = almacenamiento as Producto;
                }
                UCAlmancenamientoView uCViewTypeProduct = new UCAlmancenamientoView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.Fuente)
            {
                if (!modoInsert)
                {
                    fuente = ProductosBrl.Fuente.Get(idProducto);
                    producto = fuente as Producto;
                }
                UCFuenteView uCViewTypeProduct = new UCFuenteView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.Gabinete)
            {
                if (!modoInsert)
                {
                    gabinete = ProductosBrl.Gabinete.Get(idProducto);
                    producto = gabinete as Producto;
                }
                UCGabineteView uCViewTypeProduct = new UCGabineteView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.Monitor)
            {
                if (!modoInsert)
                {
                    monitor = ProductosBrl.Montior.Get(idProducto);
                    producto = monitor as Producto;
                }
                UCMonitorView uCViewTypeProduct = new UCMonitorView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.PlacaBase)
            {
                if (!modoInsert)
                {
                    placaBase = ProductosBrl.PlacaBase.Get(idProducto);
                    producto = placaBase as Producto;
                }
                UCPlacaBaseView uCViewTypeProduct = new UCPlacaBaseView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            else if (tipoProducto == ETipoProducto.TarjetaGrafica)
            {
                if (!modoInsert)
                {
                    tarjetaGrafica = ProductosBrl.TarjetaGrafica.Get(idProducto);
                    producto = tarjetaGrafica as Producto;
                }
                UCTarjetaGraficaView uCViewTypeProduct = new UCTarjetaGraficaView(this);
                containerTipo.Children.Add(uCViewTypeProduct);
            }
            if (!modoInsert)
            {
                LlenarCampos(producto);
            }
        }
        public void LoadComboMarcas()
        {
            marcas = ViewMain.MarcaList;
            cbMarca.ItemsSource = marcas;
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
        }
        public void LlenarCampos(Producto item)
        {
            if (!(item is null))
            {
                cbMarca.SelectedValue = item.Marca.IdMarca;
                txtNombre.Text = item.Nombre;
                txtPrecioUnidad.Text = item.PrecioUnidad.ToString();
                txtStock.Text = item.Stock.ToString();
                imgProduct.Source = ViewMain.LoadImage(item.Imagen);
                imgProduct.Stretch = Stretch.Uniform;
            }
        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            bool estado = false;
            if (btnAction.Content.ToString() == "Insertar")
            {
                if (producto.IdProducto == Guid.Empty)
                {
                    producto.IdProducto = Guid.NewGuid();
                }
            }
            if (tipoProducto == ETipoProducto.Almacenamiento)
            {
                almacenamiento.IdProducto = producto.IdProducto;
                almacenamiento.Nombre = producto.Nombre;
                almacenamiento.Imagen = producto.Imagen;
                almacenamiento.Marca.IdMarca = producto.Marca.IdMarca;
                almacenamiento.PrecioUnidad = producto.PrecioUnidad;
                almacenamiento.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Almacenamiento.Update(almacenamiento);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Almacenamiento.Insert(almacenamiento);
                }
            }
            else if (tipoProducto == ETipoProducto.Fuente)
            {
                fuente.IdProducto = producto.IdProducto;
                fuente.Nombre = producto.Nombre;
                fuente.Imagen = producto.Imagen;
                fuente.Marca.IdMarca = producto.Marca.IdMarca;
                fuente.PrecioUnidad = producto.PrecioUnidad;
                fuente.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Fuente.Update(fuente);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Fuente.Insert(fuente);
                }
            }
            else if (tipoProducto == ETipoProducto.Gabinete)
            {
                gabinete.IdProducto = producto.IdProducto;
                gabinete.Nombre = producto.Nombre;
                gabinete.Imagen = producto.Imagen;
                gabinete.Marca.IdMarca = producto.Marca.IdMarca;
                gabinete.PrecioUnidad = producto.PrecioUnidad;
                gabinete.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Gabinete.Update(gabinete);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Gabinete.Insert(gabinete);
                }
            }
            else if (tipoProducto == ETipoProducto.Monitor)
            {
                monitor.IdProducto = producto.IdProducto;
                monitor.Nombre = producto.Nombre;
                monitor.Imagen = producto.Imagen;
                monitor.Marca.IdMarca = producto.Marca.IdMarca;
                monitor.PrecioUnidad = producto.PrecioUnidad;
                monitor.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Montior.Update(monitor);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Montior.Insert(monitor);
                }
            }
            else if (tipoProducto == ETipoProducto.PlacaBase)
            {
                placaBase.IdProducto = producto.IdProducto;
                placaBase.Nombre = producto.Nombre;
                placaBase.Imagen = producto.Imagen;
                placaBase.Marca.IdMarca = producto.Marca.IdMarca;
                placaBase.PrecioUnidad = producto.PrecioUnidad;
                placaBase.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.PlacaBase.Update(placaBase);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.PlacaBase.Insert(placaBase);
                }
            }
            else if (tipoProducto == ETipoProducto.Procesador)
            {
                procesador.IdProducto = producto.IdProducto;
                procesador.Nombre = producto.Nombre;
                procesador.Imagen = producto.Imagen;
                procesador.Marca.IdMarca = producto.Marca.IdMarca;
                procesador.PrecioUnidad = producto.PrecioUnidad;
                procesador.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Procesador.Update(procesador);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Procesador.Insert(procesador);
                }
            }
            else if (tipoProducto == ETipoProducto.Ram)
            {
                ram.IdProducto = producto.IdProducto;
                ram.Nombre = producto.Nombre;
                ram.Imagen = producto.Imagen;
                ram.Marca.IdMarca = producto.Marca.IdMarca;
                ram.PrecioUnidad = producto.PrecioUnidad;
                ram.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.Ram.Update(ram);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.Ram.Insert(ram);
                }
            }
            else if (tipoProducto == ETipoProducto.TarjetaGrafica)
            {
                tarjetaGrafica.IdProducto = producto.IdProducto;
                tarjetaGrafica.Nombre = producto.Nombre;
                tarjetaGrafica.Imagen = producto.Imagen;
                tarjetaGrafica.Marca.IdMarca = producto.Marca.IdMarca;
                tarjetaGrafica.PrecioUnidad = producto.PrecioUnidad;
                tarjetaGrafica.Stock = producto.Stock;
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    estado = ProductosBrl.TarjetaGrafica.Update(tarjetaGrafica);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    estado = ProductosBrl.TarjetaGrafica.Insert(tarjetaGrafica);
                }
            }
            if (estado)
            {
                if (btnAction.Content.ToString() == "Actualizar")
                {
                    MessageBox.Show("El " + tipoProducto.ToString() + " se Modifico con exito!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (btnAction.Content.ToString() == "Insertar")
                {
                    MessageBox.Show("El " + tipoProducto.ToString() + " se Inserto con exito!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                mainView.ConfigAdministradorInterface(mainView.rol.IdRol);
                this.Close();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            txtPrecioUnidad.PreviewTextInput += Prevent_PreviewTextInputDecimal;
            txtPrecioUnidad.TextChanged += TxtPrecioUnidad_TextChanged;

            txtStock.PreviewTextInput += Prevent_PreviewTextInput;
            txtStock.TextChanged += TxtStock_TextChanged;

            cbMarca.SelectionChanged += CbMarca_SelectionChanged;

            txtNombre.TextChanged += TxtNombre_TextChanged;
            Modo("normal");
        }

        private void TxtNombre_TextChanged(object s, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { producto.Nombre = ((TextBox)s).Text; }
        }

        private void CbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producto.Marca.IdMarca = (byte)cbMarca.SelectedValue;
        }
        private void TxtStock_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { producto.Stock = short.Parse(((TextBox)s).Text); }
        }
        private void TxtPrecioUnidad_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == ".")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (((TextBox)s).Text.Length > 0 && (((TextBox)s).Text.Substring(0, 1) == "0"))
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { producto.PrecioUnidad = decimal.Parse(((TextBox)s).Text); }
        }
        private void Prevent_PreviewTextInputDecimal(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                { approvedDecimalPoint = true; }
            }
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10 || approvedDecimalPoint))
            { e.Handled = true; }
        }
        private void Prevent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 4))
            { e.Handled = true; }
        }

        private string lastDirectory = @"C:\";
        private void btnCargarImagen(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog abrir_ = new OpenFileDialog
                {
                    Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif",
                    InitialDirectory = lastDirectory,
                    Title = "Seleccione una imagen para cargar."
                };
                if (abrir_.ShowDialog() == true)
                {
                    lastDirectory = abrir_.FileName;
                    producto.Imagen = abrir_.FileName;
                    imgProduct.Source = ViewMain.LoadImage(abrir_.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
