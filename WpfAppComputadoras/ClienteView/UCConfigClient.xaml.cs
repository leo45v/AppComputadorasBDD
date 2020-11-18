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

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCConfigClient.xaml
    /// </summary>
    public partial class UCConfigClient : UserControl
    {
        public Cliente cliente;
        private Administrador admin;
        private ViewMain mainView;
        private bool modoVista = false;
        public UCConfigClient(ViewMain viewMain, Cliente clienteView)
        {
            InitializeComponent();
            cliente = clienteView;
            mainView = viewMain;
            LoadCliente(clienteView);
        }
        public UCConfigClient(ViewMain viewMain, Administrador adminView)
        {
            InitializeComponent();
            admin = adminView;
            mainView = viewMain;
        }

        public void LoadCliente(Cliente cli)
        {
            txtApellido.Text = cli.Apellido;
            txtContrasenia.Text = cli.Usuario.Contrasenia;
            txtFechaNacimiento.SelectedDate = cli.FechaNacimiento;
            txtNombre.Text = cli.Nombre;
            txtNombreUsuario.Text = cli.Usuario.NombreUsuario;
            txtSexo.SelectedIndex = cli.Sexo + 1;
        }

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
        }
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            ViewMode(!modoVista);
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Usuario.Contrasenia = txtContrasenia.Text;
            cliente.Usuario.NombreUsuario = txtNombreUsuario.Text;
            cliente.FechaNacimiento = txtFechaNacimiento.SelectedDate.Value;
            cliente.Sexo = (byte)(txtSexo.SelectedIndex - 1);
            if (ClientsBrl.Update(cliente))
            {
                mainView.txNombreView.Text = cliente.Nombre + " " + cliente.Apellido;
                ViewMode(true);
                MessageBox.Show("Moficicacion Exitosa");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMsj = MessageBox.Show("Estas seguro de querer \"Eliminar\" tu cuenta?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsj == MessageBoxResult.Yes)
            {
                if (ClientsBrl.Delete(cliente.IdPersona))
                {
                    MessageBox.Show("Se elimino la cuenta");
                    mainView.Close();
                }
            }
        }
    }
}
