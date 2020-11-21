using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using WpfAppComputadoras.Extra;

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for RegistroWindow.xaml
    /// </summary>
    public partial class RegistroWindow : Window
    {
        private MainWindow mainWindow;
        public RegistroWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            combo_sexo.Items.Clear();
            combo_sexo.Items.Add("Sexo");
            combo_sexo.SelectedIndex = 0;
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
        private void txt_Email_KeyUp(object sender, KeyEventArgs e) { }
        private void txt_Email_Reiter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_Email.Text != txt_Email_Reiter.Text)
            {
                txt_Email_Corrige1.Visibility = Visibility.Visible;
            }
            else { txt_Email_Corrige1.Visibility = Visibility.Hidden; }
        }
        private void txt_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_Email.Text != "")
            {
            }
        }
        private void txt_Password_1_KeyUp(object sender, KeyEventArgs e)
        {
        }
        private void txt_Password_1_KeyDown(object sender, KeyEventArgs e) { }
        private void Btn_Create_Account(object sender, RoutedEventArgs e)
        {
            string name = txt_Nombre.Text;
            string lastName = txt_Apellido.Text;
            string email = txt_Email.Text;
            string emailR = txt_Email.Text;
            string password = txt_Password_1.Text;
            string passwordR = txt_Password_1.Text;
            string userName = txt_NombreUsuario.Text;
            string sexo = combo_sexo.SelectedValue.ToString();
            string date_Year = dpFechaNacimiento.SelectedDate.Value.Year.ToString();
            string date_Month = dpFechaNacimiento.SelectedDate.Value.Month.ToString();
            string date_Day = dpFechaNacimiento.SelectedDate.Value.Day.ToString();
            //bool checkCondition = check_Aceptar_Condicion.IsChecked.Value;
            if (userName.Length < 4 && !UsuarioBrl.NombreUsuario_Libre(userName))
            {
                MessageBox.Show("El Nombre de Usuario esta en uso o Es demasiado corto", "Error en Nombre de Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (name.Length < 1)
            {
                MessageBox.Show("El Nombre es demasiado corto", "Error en Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (lastName.Length < 1)
            {
                MessageBox.Show("El Apellido es demasiado corto", "Error en Apellido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Methods.Comprobar_Formato_Email_(email) && email != emailR)
            {
                MessageBox.Show("El correo ingresado no es aceptado, Verifique que el correo sea el mismo en ambos campos", "Error en Correo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password.Length < 3 && password != passwordR)
            {
                MessageBox.Show("La contraseña ingresado es muy corta, Verifique haber escrito la misma contraseña en ambos campos", "Error en Contraseña", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (sexo == "Sexo")
            {
                MessageBox.Show("Seleccione un sexo", "Error en el campo Sexo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (date_Year == "Año" || date_Month == "Mes" || date_Day == "Día")
            {
                MessageBox.Show("Seleccione su fecha de nacimiento, verifique haber seleccionado el día, mes y año correspondientes", "Error en Fecha de Nacimiento", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (!checkCondition)
            //{
            //    MessageBox.Show("No puedes crear una cuenta si no aceptas los terminos de condicion de uso", "No acepto las condiciones", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            Cliente nuevoCliente = new Cliente()
            {
                Apellido = txt_Apellido.Text,
                Eliminado = false,
                Email = txt_Email.Text,
                FechaNacimiento = dpFechaNacimiento.SelectedDate.Value,
                IdPersona = Guid.NewGuid(),
                Nombre = txt_Nombre.Text,
                Sexo = (byte)(combo_sexo.SelectedIndex - 1),
                Usuario = new Usuario()
                {
                    Contrasenia = txt_Password_1.Text,
                    Eliminado = false,
                    IdUsuario = Guid.NewGuid(),
                    NombreUsuario = txt_NombreUsuario.Text,
                    Rol = new Rol()
                    {
                        IdRol = ERol.Cliente,
                    }
                }
            };
        ClientsBrl.Insertar(nuevoCliente);
            MessageBox.Show("Cuenta creada Exitosamente", "Exito!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    private void Btn_Cancel(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}
}
