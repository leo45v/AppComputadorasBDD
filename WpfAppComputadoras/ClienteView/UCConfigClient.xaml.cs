using System;
using System.Windows;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCConfigClient.xaml
    /// </summary>
    public partial class UCConfigClient : UserControl
    {
        #region PROPIEDADES
        public Cliente cliente;
        private Administrador admin;
        private ViewMain mainView;
        private bool modoVista = false;
        private Rol tipoRol;
        #endregion


        #region CONSTRUCTOR
        public UCConfigClient(ViewMain viewMain, Cliente assets)
        {
            InitializeComponent();
            cliente = assets;
            mainView = viewMain;
            LoadCliente(assets);
            tipoRol = assets.Usuario.Rol;
        }
        public UCConfigClient(ViewMain viewMain, Administrador adminView)
        {
            InitializeComponent();
            admin = adminView;
            mainView = viewMain;
            LoadAdministrador(adminView);
            tipoRol = adminView.Usuario.Rol;
            //admiMenuView --> RELOAD
        }
        #endregion



        #region UI_ADMINISTRADOR_METODOS
        public void LoadAdministrador(Administrador admin)
        {
            txtApellido.Text = admin.Apellido;
            txtContrasenia.Text = admin.Usuario.Contrasenia;
            txtFechaNacimiento.SelectedDate = admin.FechaNacimiento;
            txtNombre.Text = admin.Nombre;
            txtNombreUsuario.Text = admin.Usuario.NombreUsuario;
            txtSexo.SelectedIndex = admin.Sexo + 1;
        }
        private void Editar_Administrador()
        {
            admin.Nombre = txtNombre.Text;
            admin.Apellido = txtApellido.Text;
            admin.Usuario.Contrasenia = txtContrasenia.Text;
            admin.Usuario.NombreUsuario = txtNombreUsuario.Text;
            admin.FechaNacimiento = txtFechaNacimiento.SelectedDate.Value;
            admin.Sexo = (byte)(txtSexo.SelectedIndex - 1);
            if (AdministradorBrl.Update(admin))
            {
                mainView.txNombreView.Text = admin.ToString();
                ViewMode(true);
                MessageBox.Show("Moficicacion Exitosa");
            }
        }
        private void UI_AtrtasAdmin()
        {
            mainView.btnConfigurar.IsEnabled = true;
            mainView.gridAutomaitc.Children.Clear();
            mainView.gridAutomaitc.Children.Add(mainView.ViewModeMainANTERIOR);
            mainView.ViewModeMain = mainView.ViewModeMainANTERIOR;
            mainView.ViewModeMainANTERIOR = mainView.uCConfigClient;
        }
        private void BorrarCuenta_Admin()
        {
            if (AdministradorBrl.Delete(admin.IdPersona))
            {
                MessageBox.Show("Se elimino la cuenta");
                mainView.Close();
            }
        }
        #endregion



        #region UI_CLIENTE_METODOS
        public void LoadCliente(Cliente cli)
        {
            txtApellido.Text = cli.Apellido;
            txtContrasenia.Text = cli.Usuario.Contrasenia;
            txtFechaNacimiento.SelectedDate = cli.FechaNacimiento;
            txtNombre.Text = cli.Nombre;
            txtNombreUsuario.Text = cli.Usuario.NombreUsuario;
            txtSexo.SelectedIndex = cli.Sexo + 1;
            //FALTA EMAIL 
        }
        private void UI_AtrasCliente()
        {
            mainView.btnConfigurar.IsEnabled = true;
            mainView.gridAutomaitc.Children.Clear();
            mainView.gridAutomaitc.Children.Add(mainView.ViewModeMainANTERIOR);
            mainView.ViewModeMain = mainView.ViewModeMainANTERIOR;
            mainView.ViewModeMainANTERIOR = mainView.uCConfigClient;
        }
        private void Editar_Cliente()
        {
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Usuario.Contrasenia = txtContrasenia.Text;
            cliente.Usuario.NombreUsuario = txtNombreUsuario.Text;
            cliente.FechaNacimiento = txtFechaNacimiento.SelectedDate.Value;
            cliente.Sexo = (byte)(txtSexo.SelectedIndex - 1);
            if (ClientsBrl.Update(cliente))
            {
                mainView.txNombreView.Text = cliente.ToString();
                ViewMode(true);
                MessageBox.Show("Moficicacion Exitosa");
            }
        }
        private void BorrarCuenta_Cliente()
        {
            if (ClientsBrl.Delete(cliente.IdPersona))
            {
                MessageBox.Show("Se elimino la cuenta");
                mainView.Close();
            }
        }
        #endregion





        #region METODOS
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtSexo.Items.Clear();
            txtSexo.Items.Add("Sexo");
            txtSexo.Items.Add("Masculino");
            txtSexo.Items.Add("Femenino");
            txtSexo.Items.Add("Indefinido");
            txtSexo.SelectedIndex = 0;
            txtApellido.IsEnabled = false;
            ViewMode(true);
            bthAtras.Click += BthAtras_Click;
        }
        private void BthAtras_Click(object sender, RoutedEventArgs e)
        {
            if (tipoRol.IdRol == ERol.Cliente)
            {
                UI_AtrasCliente();
            }
            else if (tipoRol.IdRol == ERol.Administrador)
            {
                UI_AtrtasAdmin();
            }
        }
        public void ViewMode(bool activo)
        {
            modoVista = activo;
            txtApellido.IsEnabled = !activo;
            txtContrasenia.IsEnabled = !activo;
            txtFechaNacimiento.IsEnabled = !activo;
            txtNombre.IsEnabled = !activo;
            txtNombreUsuario.IsEnabled = !activo;
            txtSexo.IsEnabled = !activo;
            if (!activo)
            {
                iconModificar.Kind = MaterialDesignThemes.Wpf.PackIconKind.EditOff;
            }
            else
            {
                iconModificar.Kind = MaterialDesignThemes.Wpf.PackIconKind.Edit;
            }
        }
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ViewMode(!modoVista);
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (tipoRol.IdRol == ERol.Cliente)
            {
                Editar_Cliente();
            }
            else if (tipoRol.IdRol == ERol.Administrador)
            {
                Editar_Administrador();
            }
        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMsj = MessageBox.Show("Estas seguro de querer \"Eliminar\" tu cuenta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsj == MessageBoxResult.Yes)
            {
                if (tipoRol.IdRol == ERol.Cliente)
                {
                    BorrarCuenta_Cliente();
                }
                else if (tipoRol.IdRol == ERol.Administrador)
                {
                    BorrarCuenta_Admin();
                }
            }
        }
        #endregion
    }
}
