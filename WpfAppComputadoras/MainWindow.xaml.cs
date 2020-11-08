using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Guid idUsuario = Guid.Empty;
        private string password = "";
        private string username = "";
        public MainWindow()
        {
            InitializeComponent();
            txt_Usuario.Focus();
            //ClientsBrl.Insertar(new Cliente()
            //{
            //    Apellido = "123",
            //    FechaNacimiento = new DateTime(1995, 5, 19),
            //    Eliminado = false,
            //    Email = "pepe@pepe.com",
            //    IdPersona = Guid.NewGuid(),
            //    Nombre = "pepe",
            //    Sexo = 1,
            //    Usuario = new Usuario()
            //    {
            //        Contrasenia = "123",
            //        Eliminado = false,
            //        IdUsuario = Guid.NewGuid(),
            //        NombreUsuario = "pepe@pepe.copm",
            //        Rol = new Rol()
            //        {
            //            IdRol = 2,
            //        }
            //    }
            //});
        }
        private void Mover_Ventana_Controller(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int aux = txt_Contrasena.TabIndex;
            txt_Contrasena.TabIndex = txt_Usuario.TabIndex;
            txt_Usuario.TabIndex = aux;
            if (((TextBox)sender) == txt_Usuario && txt_Usuario.Text == "")
            {
                txt_Usuario.Text = "Correo Electronico";
                username = "";
            }
            else if (((TextBox)sender) == txt_Contrasena && txt_Contrasena.Text == "")
            {
                txt_Contrasena.Text = "Password";
                password = "";
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            if (((TextBox)sender) == txt_Usuario && txt_Usuario.Text == "Correo Electronico")
            {
                txt_Usuario.Text = "";
                username = "";
            }
            else if (((TextBox)sender) == txt_Contrasena && txt_Contrasena.Text == "Password")
            {
                txt_Contrasena.Text = "";
                password = "";
            }
        }
        private void Txt_Usuario_KeyUp(object sender, KeyEventArgs e)
        {
            string Correo = txt_Usuario.Text;
            if (((TextBox)sender) == txt_Usuario)
            {
                username = txt_Usuario.Text;
            }
            if (((TextBox)sender) == txt_Contrasena)
            {
                password = txt_Contrasena.Text;
            }
            if (Comprobar_Formato_Email_(Correo) && username != "" && password != "")
            {
                btn_Iniciar_Secion.IsEnabled = true;
            }
            else
            {
                btn_Iniciar_Secion.IsEnabled = false;
            }
        }
        public static bool Comprobar_Formato_Email_(string E_Mail__Usuario)
        {
            String Formato_Email = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(E_Mail__Usuario, Formato_Email))
            {
                if (Regex.Replace(E_Mail__Usuario, Formato_Email, String.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_Iniciar_Secion_Click(object sender, RoutedEventArgs e)
        {
            idUsuario = UsuarioBrl.Obtener_Id_Usuario(username, password);
            if (idUsuario != Guid.Empty)
            {
                ViewMain viewMain = new ViewMain(this);
                viewMain.Show();
                this.Hide();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistroWindow registroWindow = new RegistroWindow(this);
            registroWindow.Show();
            this.Hide();
        }
    }
}
