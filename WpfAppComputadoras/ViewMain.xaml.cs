using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using System.Threading;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using WpfAppComputadoras.Administrator;
using WpfAppComputadoras.ClienteView;
using WpfAppComputadoras.Extra;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Operaciones;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;

namespace WpfAppComputadoras
{

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
        public UCTypeComputerView uCTypeComputerView;
        public ComputerBuildView computerBuildView;
        public UCConfigClient uCConfigClient;


        public UIElement ViewModeMain;
        public UIElement ViewModeMainANTERIOR;

        public Computadora computadoraCalculada = null;

        public string queryProductName = "";
        public Marca queryProductMarca = null;
        public ETipoProducto queryProductTipo = ETipoProducto.None;
        public List<Marca> MarcaList { get; set; } = new List<Marca>();
        public List<Colores> Colores { get; set; } = new List<Colores>();
        public List<SocketProcesador> SocketsList { get; set; } = new List<SocketProcesador>();
        public List<Resolucion> ListaResolucion { get; set; } = new List<Resolucion>();
        public List<Ratio> ListaRatio { get; set; } = new List<Ratio>();
        public ViewMain(MainWindow main, Guid idUsuario)
        {
            InitializeComponent();
            mainWindow = main;
            MarcaList = main.ListaMarcas;
            Colores = main.ListaColores;
            rol = UsuarioBrl.GetRol(idUsuario);
            SocketsList = main.ListaSockets;
            ListaResolucion = main.ListaResolucion;
            ListaRatio = main.ListaRatio;
            if (rol.IdRol == ERol.Cliente)
            {
                btnViewClients.Visibility = Visibility.Collapsed;
                panelClient.Visibility = Visibility.Visible;
                cliente = ClientsBrl.GetClienteByIdUsuario(idUsuario);
                ConfigClienteInterface();
                txNombreView.Text = cliente.Nombre + " " + cliente.Apellido;

            }
            else if (rol.IdRol == ERol.Administrador)
            {
                panelClient.Visibility = Visibility.Collapsed;
                btnViewClients.Visibility = Visibility.Visible;
                admin = AdministradorBrl.GetAdministradorByIdUsuario(idUsuario);
                LoadInterfaceAdmin(admin.Usuario.Rol.IdRol);
                txNombreView.Text = admin.Nombre + " " + admin.Apellido;
            }

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
        private void ConfigClienteInterface()
        {
            uCTypeComputerView = new UCTypeComputerView(this);
            ViewModeMain = uCTypeComputerView;
            gridAutomaitc.Children.Clear();
            gridAutomaitc.Children.Add(uCTypeComputerView);
        }
        public int buttonCant = 0;
        public int cantidad = 10;
        private void LoadInterfaceAdmin(ERol eRol)
        {
            admiMenuView = new AdmiMenuView(this, eRol);
            buttonCant = Methods.RedondeoSiempre((double)MaxProducts / cantidad);
            gridAutomaitc.Children.Clear();
            gridAutomaitc.Children.Add(admiMenuView);
            ConfigAdministradorInterface(eRol);
            admiMenuView.btnFirst.Click += BtnFirst_Click;
            admiMenuView.btnLast.Click += BtnLast_Click;
            admiMenuView.btnPrevious.Click += BtnPrevious_Click;
            admiMenuView.btnNext.Click += BtnNext_Click;
            ViewModeMain = admiMenuView;
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
                buttonCant = Methods.RedondeoSiempre((double)MaxProducts / cantidad);
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
        public void UIReservaComputer(Computadora computadora)
        {
            if (computadora is null)
            {
                MessageBox.Show("No Se Consiguio Armar con el Presupuesto Dado");
                return;
            }
            this.computadoraCalculada = computadora;
            computerBuildView = new ComputerBuildView(this, this.computadoraCalculada);
            ViewModeMain = computerBuildView;
            gridAutomaitc.Children.Clear();
            gridAutomaitc.Children.Add(computerBuildView);
            btnArmadoAnterior.IsEnabled = true;
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
        private void BtnConfigurar_Click(object sender, RoutedEventArgs e)
        {
            if (ERol.Cliente == rol.IdRol)
            {
                btnConfigurar.IsEnabled = false;
                gridAutomaitc.Children.Clear();
                uCConfigClient = new UCConfigClient(this, cliente);
                //uCConfigClient.bthAtras.Click += ((s, e) =>
                //{
                //    btnConfigurar.IsEnabled = true;
                //    ConfigClienteInterface();
                //});
                gridAutomaitc.Children.Add(uCConfigClient);
                ViewModeMainANTERIOR = ViewModeMain;
                ViewModeMain = uCConfigClient;
            }
            else if (ERol.Administrador == rol.IdRol)
            {
                btnConfigurar.IsEnabled = false;
                gridAutomaitc.Children.Clear();
                uCConfigClient = new UCConfigClient(this, admin);
                //uCConfigClient.bthAtras.Click += ((s, e) =>
                //{
                //    btnConfigurar.IsEnabled = true;
                //    ConfigClienteInterface(rol.IdRol);
                //});
                gridAutomaitc.Children.Add(uCConfigClient);
                ViewModeMainANTERIOR = ViewModeMain;
                ViewModeMain = uCConfigClient;
            }
        }

        private void Btn_Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GC.SuppressFinalize(this);
            mainWindow.CleanMemory();
        }

        private void Btn_VerArmadoAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModeMain != computerBuildView)
            {
                btnConfigurar.IsEnabled = true;
                gridAutomaitc.Children.Clear();
                gridAutomaitc.Children.Add(computerBuildView);
                ViewModeMainANTERIOR = ViewModeMain;
                ViewModeMain = computerBuildView;
            }
            else
            {
                MessageBox.Show("YA ESTAS AK");
            }
        }
    }
}
