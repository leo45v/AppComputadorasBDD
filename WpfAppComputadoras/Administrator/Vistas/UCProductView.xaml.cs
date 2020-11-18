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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCProductView.xaml
    /// </summary>
    public partial class UCProductView : Window
    {
        public Procesador procesador = new Procesador();
        private List<Marca> marcas;
        public UCProductView()
        {
            InitializeComponent();
            LoadComboMarcas();
            btnAction.Content = "Insertar";
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
            txtConsumo.IsEnabled = noVista;
            txtFrecuenciaBase.IsEnabled = noVista;
            txtFrecuenciaTurbo.IsEnabled = noVista;
            txtLitografia.IsEnabled = noVista;
            cbMarca.IsEnabled = noVista;
            txtNombre.IsEnabled = noVista;
            txtNumeroNucleos.IsEnabled = noVista;
            txtPrecioUnidad.IsEnabled = noVista;
            txtStock.IsEnabled = noVista;
            txtNumeroHilos.IsEnabled = noVista;
            btnAction.IsEnabled = noVista;
            btnLoadImg.IsEnabled = noVista;
            if (noVista) { btnAction.Visibility = Visibility.Visible; btnLoadImg.Visibility = Visibility.Visible; }
            else { btnAction.Visibility = Visibility.Hidden; btnLoadImg.Visibility = Visibility.Hidden; }

        }
        public UCProductView(Guid idProducto)
        {
            InitializeComponent();
            LoadComboMarcas();
            procesador = ProcesadorBrl.Get(idProducto);
            LlenarCampos(procesador);
            btnAction.Content = "Actualizar";
        }
        public void LoadComboMarcas()
        {
            marcas = ViewMain.MarcaList;
            cbMarca.ItemsSource = marcas;
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
        }
        public void LlenarCampos(Procesador item)
        {
            txtConsumo.Text = item.Consumo.ToString();
            txtFrecuenciaBase.Text = item.FrecuenciaBase.ToString();
            txtFrecuenciaTurbo.Text = item.FrecuenciaTurbo.ToString();
            txtLitografia.Text = item.Litografia.ToString();
            cbMarca.SelectedValue = item.Marca.IdMarca;
            txtNombre.Text = item.Nombre;
            txtNumeroNucleos.Text = item.NumeroNucleos.ToString();
            txtPrecioUnidad.Text = item.PrecioUnidad.ToString();
            txtStock.Text = item.Stock.ToString();
            txtNumeroHilos.Text = item.NumeroHilos.ToString();
            imgProduct.Source = ViewMain.LoadImage(item.Imagen);
            imgProduct.Stretch = Stretch.Uniform;
        }


        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            if (btnAction.Content.ToString() == "Actualizar" && ProcesadorBrl.Update(procesador))
            {
                MessageBox.Show("El Procesador se modifico con exito!!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            txtConsumo.PreviewTextInput += Prevent_PreviewTextInput;
            txtConsumo.TextChanged += TxtConsumo_TextChanged;

            txtFrecuenciaBase.PreviewTextInput += Prevent_PreviewTextInput;
            txtFrecuenciaBase.TextChanged += TxtFrecuenciaBase_TextChanged;

            txtFrecuenciaTurbo.PreviewTextInput += Prevent_PreviewTextInput;
            txtFrecuenciaTurbo.TextChanged += TxtFrecuenciaTurbo_TextChanged;

            txtLitografia.PreviewTextInput += Prevent_PreviewTextInput;
            txtLitografia.TextChanged += TxtLitografia_TextChanged;

            //cbMarca.SelectedValue = item.Marca.IdMarca;
            txtNumeroNucleos.PreviewTextInput += Prevent_PreviewTextInput;
            txtNumeroNucleos.TextChanged += TxtNumeroNucleos_TextChanged;

            txtPrecioUnidad.PreviewTextInput += Prevent_PreviewTextInputDecimal;
            txtPrecioUnidad.TextChanged += TxtPrecioUnidad_TextChanged;

            txtStock.PreviewTextInput += Prevent_PreviewTextInput;
            txtStock.TextChanged += TxtStock_TextChanged;

            txtNumeroHilos.PreviewTextInput += Prevent_PreviewTextInput;
            txtNumeroHilos.TextChanged += TxtNumeroHilos_TextChanged;

            cbMarca.SelectionChanged += CbMarca_SelectionChanged;

            txtNombre.TextChanged += TxtNombre_TextChanged;
            Modo("normal");
        }

        private void TxtNombre_TextChanged(object s, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.Nombre = ((TextBox)s).Text; }
        }

        private void CbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            procesador.Marca.IdMarca = (byte)cbMarca.SelectedValue;
        }

        private void TxtNumeroHilos_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.NumeroHilos = int.Parse(((TextBox)s).Text); }
        }

        private void TxtStock_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.Stock = short.Parse(((TextBox)s).Text); }
        }

        private void TxtPrecioUnidad_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == ".")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (((TextBox)s).Text.Length > 0 && (((TextBox)s).Text.Substring(0, 1) == "0"))
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.PrecioUnidad = decimal.Parse(((TextBox)s).Text); }
        }

        private void TxtNumeroNucleos_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.NumeroNucleos = int.Parse(((TextBox)s).Text); }
        }

        private void TxtLitografia_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.Litografia = int.Parse(((TextBox)s).Text); }
        }

        private void TxtFrecuenciaTurbo_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.FrecuenciaTurbo = int.Parse(((TextBox)s).Text); }
        }

        private void TxtFrecuenciaBase_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.FrecuenciaBase = int.Parse(((TextBox)s).Text); }
        }

        private void TxtConsumo_TextChanged(object s, TextChangedEventArgs e)
        {
            ((TextBox)s).Text = ((TextBox)s).Text.Trim();
            if (((TextBox)s).Text.Length > 0 && ((TextBox)s).Text.Substring(0, 1) == "0")
            { ((TextBox)s).Text = ((TextBox)s).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)s).Text))
            { procesador.Consumo = int.Parse(((TextBox)s).Text); }
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

        private void btnCargarImagen(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog abrir_ = new OpenFileDialog
                {
                    Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif",
                    InitialDirectory = @"C:\",
                    Title = "Seleccione una imagen para cargar."
                };
                if (abrir_.ShowDialog() == true)
                {
                    procesador.Imagen = abrir_.FileName;
                    imgProduct.Source = ViewMain.LoadImage(abrir_.FileName);
                    //Servidor.Recuperar_Imagen(@abrir_.FileName);
                    //imagen_a.Contenido = File.ReadAllBytes(@abrir_.FileName);
                    //imagen_a.Name_Format = "Perfil.jpg";
                }
                //Thread cargar_Imagen = new Thread(Cargar_imagen);
                //cargar_Imagen.Start();

                ///ejecutar cambio de foto para todos ...

                //img_Perfil_Friend
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
