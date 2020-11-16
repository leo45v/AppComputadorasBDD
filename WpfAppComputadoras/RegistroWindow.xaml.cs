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
using System.Windows.Shapes;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for RegistroWindow.xaml
    /// </summary>
    public partial class RegistroWindow : Window
    {
        private MainWindow mainWindow;
        private Dictionary<string, bool> boton_activo = new Dictionary<string, bool>();
        //private bool[] boton_Activo = new bool[14];
        private bool Nivel1 = false, Nivel2 = false, Nivel3 = false, Nivel4 = false, Nivel5 = false;
        private bool editable = false;
        public RegistroWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            combo_Dia.Items.Clear();
            combo_Año.Items.Clear();
            combo_Dia.Items.Add("Día");
            combo_Dia.SelectedIndex = 0;
            combo_Año.Items.Add("Año");
            combo_Año.SelectedIndex = 0;
            for (int i = 0; i < 31; i++)
            {
                combo_Dia.Items.Add(i + 1);
            }
            for (int i = 0; i < 70; i++)
            {
                combo_Año.Items.Add(2015 - i);
            }
            combo_sexo.Items.Clear();
            combo_sexo.Items.Add("Sexo");
            combo_sexo.SelectedIndex = 0;
            combo_sexo.Items.Add("Masculino");
            combo_sexo.Items.Add("Femenino");
            combo_sexo.Items.Add("Indefinido");

            boton_activo.Add("Nombre", false);
            boton_activo.Add("Apellido", false);
            boton_activo.Add("Sexo", false);
            boton_activo.Add("NombreUsuario", false);
            boton_activo.Add("Password1", false);
            boton_activo.Add("Password2", false);
            boton_activo.Add("Email", false);
            boton_activo.Add("EmailR", false);
            boton_activo.Add("Año", false);
            boton_activo.Add("Mes", false);
            boton_activo.Add("Dia", false);
            boton_activo.Add("Condiciones", false);
            boton_activo.Add("Aceptar", false);
            editable = true;
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
            string date_Year = combo_Año.SelectedValue.ToString();
            string date_Month = combo_Mes.SelectedValue.ToString();
            string date_Day = combo_Dia.SelectedValue.ToString();
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
            if (!MainWindow.Comprobar_Formato_Email_(email) && email != emailR)
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
                FechaNacimiento = new DateTime(int.Parse(combo_Año.Text),
                combo_Mes.SelectedIndex,
                int.Parse(combo_Dia.Text)),
                IdPersona = Guid.NewGuid(),
                Nombre = txt_Nombre.Text,
                Sexo = (short)(combo_sexo.SelectedIndex - 1),
                Usuario = new Usuario()
                {
                    Contrasenia = txt_Password_1.Text,
                    Eliminado = false,
                    IdUsuario = Guid.NewGuid(),
                    NombreUsuario = txt_NombreUsuario.Text,
                    Rol = new Rol()
                    {
                        IdRol = 1,
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
