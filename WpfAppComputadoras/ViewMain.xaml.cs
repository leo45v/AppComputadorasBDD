using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using WpfAppComputadoras.Administrator;
using WpfAppComputadoras.ClienteView;

namespace WpfAppComputadoras
{
    public class ComboColor : Colores
    {
        public Boolean Check_Status
        {
            get;
            set;
        }
        private Brush myVar;

        public Brush ColorPath
        {
            get { return new SolidColorBrush(Color.FromRgb(ColorRGB.R, ColorRGB.G, ColorRGB.B)); }
            private set { myVar = value; }
        }
    }
    /// <summary>
    /// Interaction logic for ViewMain.xaml
    /// </summary>
    public partial class ViewMain : Window
    {
        private readonly MainWindow mainWindow;

        public Rol rol = new Rol();
        private Cliente cliente;
        private Administrador admin;
        public AdmiMenuView admiMenuView;

        private static List<Marca> marcas;

        public string queryProductName = "";
        public Marca queryProductMarca = null;
        public ETipoProducto queryProductTipo = ETipoProducto.None;
        public static List<Marca> MarcaList
        {
            get { return marcas; }
            private set
            {
                marcas = value;
            }
        }
        private List<Colores> colores;

        public List<Colores> Colores
        {
            get { return colores; }
            set { colores = value; }
        }


        public ViewMain(MainWindow main, Guid idUsuario)
        {
            InitializeComponent();
            mainWindow = main;

            rol = UsuarioBrl.GetRol(idUsuario);
            if (rol.IdRol == ERol.Cliente)
            {
                cliente = ClientsBrl.GetClienteByIdUsuario(idUsuario);
                ConfigClienteInterface(cliente.Usuario.Rol.IdRol);
                txNombreView.Text = cliente.Nombre + " " + cliente.Apellido;

            }
            else if (rol.IdRol == ERol.Administrador)
            {
                admin = AdministradorBrl.GetAdministradorByIdUsuario(idUsuario);
                LoadInterfaceAdmin(admin.Usuario.Rol.IdRol);
                txNombreView.Text = admin.Nombre + " " + admin.Apellido;
            }
            marcas = ProductosBrl.GetMarcas();
            colores = ProductosBrl.GetColores();
            //ucProcesador.lblProduct.Content = "Procesadores";
            //ucProcesador.imgProduct.Source = LoadImage("assets/procesadores.jpg");
            //ucProcesador.imgProduct.Stretch = Stretch.Fill;
        }
        public int pagSelect = 0;
        //public int maxProducts = 0;
        //private int myVar;

        public int MaxProducts
        {
            get { return ProductosBrl.CountWithFilter(queryProductName, queryProductMarca, queryProductTipo); }
            private set { }
        }

        public void ConfigClienteInterface(ERol eRol)
        {
            UCTypeComputerView uCTypeComputerView = new UCTypeComputerView();
            gridAutomaitc.Children.Add(uCTypeComputerView);
        }
        public int buttonCant = 0;
        public int cantidad = 10;
        private void LoadInterfaceAdmin(ERol eRol)
        {
            admiMenuView = new AdmiMenuView(this, eRol);
            buttonCant = RedondeoSiempre((double)MaxProducts / cantidad);
            gridAutomaitc.Children.Clear();
            gridAutomaitc.Children.Add(admiMenuView);
            ConfigAdministradorInterface(eRol);
            admiMenuView.btnFirst.Click += BtnFirst_Click;
            admiMenuView.btnLast.Click += BtnLast_Click;
            admiMenuView.btnPrevious.Click += BtnPrevious_Click;
            admiMenuView.btnNext.Click += BtnNext_Click;
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (pagSelect == buttonCant - 1)
            {
                return;
            }
            pagSelect++;
            LoadProductosAndLoadButtons();
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (pagSelect == 0)
            {
                return;
            }
            pagSelect--;
            LoadProductosAndLoadButtons();
        }

        private void BtnLast_Click(object sender, RoutedEventArgs e)
        {
            if (pagSelect == buttonCant - 1)
            {
                return;
            }
            pagSelect = buttonCant - 1;
            LoadProductosAndLoadButtons();
        }

        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (pagSelect == 0)
            {
                return;
            }
            pagSelect = 0;
            LoadProductosAndLoadButtons();
        }
        public void LoadProductosAndLoadButtons()
        {
            admiMenuView.CargarProductos(ProductosBrl.GetWithRangeWithFillter(pagSelect * 10, 10, queryProductName, queryProductMarca, queryProductTipo));
            LoadButton(pagSelect > 0 ? pagSelect - 1 : 0, buttonCant);
        }
        public void ConfigAdministradorInterface(ERol eRol)
        {
            if (eRol == ERol.Administrador)
            {
                buttonCant = RedondeoSiempre((double)MaxProducts / cantidad);
                //pagSelect = 0;
                LoadProductosAndLoadButtons();
            }
        }
        public void LoadButton(int start, int cant)
        {
            int comienzo = start < cant - 14 ? start : (cant - 14) < 0 ? 0 : cant - 14;
            admiMenuView.stackPagina.Children.Clear();
            for (int i = 0; i < comienzo; i++)
            {
                TextBlock txt = new TextBlock()
                {
                    Text = ".",
                    VerticalAlignment = VerticalAlignment.Bottom,
                    FontSize = 18
                };
                admiMenuView.stackPagina.Children.Add(txt);
            }
            for (int i = comienzo; i < cant; i++)
            {
                if (i < comienzo + 14)
                {
                    Button btn = new Button()
                    {
                        Content = i + 1 + "",
                        Width = 50
                    };
                    if (i == pagSelect)
                    {
                        btn.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        btn.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    }
                    else
                    {
                        btn.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        btn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    }
                    btn.Click += Btn_Click;
                    //btn.Click += ((s, ev) =>
                    //{
                    //    int aux = int.Parse(((Button)s).Content.ToString()) - 1;
                    //    if (aux == pagSelect)
                    //    {
                    //        return;
                    //    }
                    //    pagSelect = aux;
                    //    admiMenuView.CargarProductos(ProductosBrl.GetWithRangeWithFillter(pagSelect * 10, 10, queryProductName, queryProductMarca, queryProductTipo));
                    //    LoadButton(pagSelect > 0 ? pagSelect - 1 : 0, cant);
                    //});
                    admiMenuView.stackPagina.Children.Add(btn);
                }
                else
                {
                    TextBlock txt = new TextBlock()
                    {
                        Text = ".",
                        VerticalAlignment = VerticalAlignment.Bottom,
                        FontSize = 18
                    };
                    admiMenuView.stackPagina.Children.Add(txt);
                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            int aux = int.Parse(((Button)sender).Content.ToString()) - 1;
            if (aux == pagSelect)
            {
                return;
            }
            pagSelect = aux;
            admiMenuView.CargarProductos(ProductosBrl.GetWithRangeWithFillter(pagSelect * 10, 10, queryProductName, queryProductMarca, queryProductTipo));
            LoadButton(pagSelect > 0 ? pagSelect - 1 : 0, buttonCant);
        }

        public static int RedondeoSiempre(double number)
        {
            int n = (int)number;
            double decimales = number - n;
            if (decimales != 0)
            {
                n++;
            }
            return n;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mover_Ventana_Controller(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public static BitmapImage LoadImage(string path)
        {
            string pathImg = String.IsNullOrWhiteSpace(path) ? "assets/null.jpg" : path;
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.StreamSource = new MemoryStream(File.ReadAllBytes(@"" + pathImg));
            bi3.EndInit();
            return bi3;
        }

        private void BtnConfigurar_Click(object sender, RoutedEventArgs e)
        {
            if (ERol.Cliente == rol.IdRol)
            {
                btnConfigurar.IsEnabled = false;
                gridAutomaitc.Children.Clear();
                UCConfigClient uCConfigClient = new UCConfigClient(this, cliente);
                uCConfigClient.bthAtras.Click += ((s, e) =>
                {
                    btnConfigurar.IsEnabled = true;
                    ConfigClienteInterface(rol.IdRol);
                });
                gridAutomaitc.Children.Add(uCConfigClient);
            }
        }
    }
}
