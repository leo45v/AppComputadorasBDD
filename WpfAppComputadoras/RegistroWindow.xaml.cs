using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using WpfAppComputadoras.Extra;
using System.Windows.Threading;
using System.Threading;

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for RegistroWindow.xaml
    /// </summary>
    public partial class RegistroWindow : Window
    {
        private readonly MainWindow mainWindow;
        private readonly DispatcherTimer errorTimerRemove;
        public RegistroWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            txtError.Visibility = Visibility.Collapsed;

            errorTimerRemove = new DispatcherTimer();
            errorTimerRemove.Tick += QuitarMensajeError;
            errorTimerRemove.Interval = new TimeSpan(0, 0, 5);
        }
        private void QuitarMensajeError(object sender, EventArgs e)
        {
            QuitarMensajeError();
        }
        private void PonerMensajeError(string mensaje)
        {
            txtError.Visibility = Visibility.Visible;
            txtError.Text = mensaje;
            errorTimerRemove.Start();
        }
        private void QuitarMensajeError()
        {
            txtError.Visibility = Visibility.Collapsed;
            txtError.Text = "";
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            combo_sexo.Items.Clear();
            combo_sexo.Items.Add("Masculino");
            combo_sexo.Items.Add("Femenino");
            combo_sexo.Items.Add("Indefinido");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }
        private void Mover_Ventana_Controller(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Btn_Create_Account(object sender, RoutedEventArgs e)
        {
            errorTimerRemove.Stop();
            QuitarMensajeError();
            string name = txt_Nombre.Text;
            string lastName = txt_Apellido.Text;
            string email = txt_Email.Text;
            string emailR = txt_Email.Text;
            string password = txt_Password_1.Password;
            string passwordR = txt_Password_1.Password;
            string userName = txt_NombreUsuario.Text;
            int sexo = combo_sexo.SelectedIndex;
            DateTime date = dpFechaNacimiento.SelectedDate == null ? DateTime.MinValue : dpFechaNacimiento.SelectedDate.Value;
            if (userName.Length < 4 && !UsuarioBrl.NombreUsuario_Libre(userName))
            {
                PonerMensajeError("El Nombre de Usuario esta en uso o Es demasiado corto");
                return;
            }
            if (name.Length < 1)
            {
                PonerMensajeError("El Nombre es demasiado corto");
                return;
            }
            if (lastName.Length < 1)
            {
                PonerMensajeError("El Apellido es demasiado corto");
                return;
            }
            if (!Methods.Comprobar_Formato_Email_(email) || email != emailR)
            {
                PonerMensajeError("El correo ingresado no es aceptado, Verifique que el correo sea el mismo en ambos campos");
                return;
            }
            if (password.Length < 3 || password != passwordR)
            {
                PonerMensajeError("La contraseña ingresado es muy corta, Verifique haber escrito la misma contraseña en ambos campos");
                return;
            }
            if (sexo < 0)
            {
                PonerMensajeError("Seleccione un sexo");
                return;
            }
            if (date.Date == DateTime.MinValue)
            {
                PonerMensajeError("Seleccione su fecha de nacimiento, verifique haber seleccionado el día, mes y año correspondientes");
                return;
            }

            Cliente nuevoCliente = new Cliente()
            {
                Apellido = lastName,
                Eliminado = false,
                Email = email,
                FechaNacimiento = date,
                IdPersona = Guid.NewGuid(),
                Nombre = name,
                Sexo = (byte)(combo_sexo.SelectedIndex),
                Usuario = new Usuario()
                {
                    Contrasenia = password,
                    Eliminado = false,
                    IdUsuario = Guid.NewGuid(),
                    NombreUsuario = userName,
                    Rol = new Rol()
                    {
                        IdRol = ERol.Cliente,
                    }
                }
            };
            bdWaiting.IsOpen = true;
            var loginProcess = new Thread(CrearCuenta);
            loginProcess.Start(nuevoCliente);

        }
        private void CrearCuenta(object argumento)
        {
            Cliente nuevoCliente = (Cliente)argumento;
            bool estado = ClientsBrl.Insertar(nuevoCliente);
            Application.Current.Dispatcher.Invoke(new Action<bool>(UICrearCuenta), estado);
        }
        private void UICrearCuenta(bool estado)
        {
            bdWaiting.IsOpen = false;
            if (estado)
            {
                MessageBox.Show("Cuenta creada Exitosamente", "Exito!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                PonerMensajeError("Error al crear la cuenta revise los datos o intente más tarde");
            }
        }
        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
