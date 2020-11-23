using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
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
        public List<Colores> ListaColores { get; private set; }
        public List<Marca> ListaMarcas { get; private set; }
        public delegate string NoArgDelegate();

        public MainWindow()
        {
            InitializeComponent();
            txt_Usuario.Focus();
            Thread thread = new Thread(GetDataBg);
            thread.Start();
        }
        private void GetDataBg()
        {
            this.ListaMarcas = ProductosBrl.GetMarcas();
            this.ListaColores = ProductosBrl.GetColores();
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
            if (sender.GetType() == typeof(TextBox) && txt_Usuario.Text == "")
            {
                txt_Usuario.Text = "Nombre de Usuario";
                username = "";
            }
            else if (sender.GetType() == typeof(PasswordBox) && txt_Contrasena.Password == "")
            {
                txt_Contrasena.Password = "";
                password = "";
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(TextBox) && txt_Usuario.Text == "Nombre de Usuario")
            {
                txt_Usuario.Text = "";
                username = "";
            }
            else if (sender.GetType() == typeof(PasswordBox) && txt_Contrasena.Password == "Password")
            {
                txt_Contrasena.Password = "";
                password = "";
            }
        }
        private void Txt_Usuario_KeyUp(object sender, KeyEventArgs e)
        {
            string Correo = txt_Usuario.Text;
            if (sender.GetType() == typeof(TextBox) && ((TextBox)sender) == txt_Usuario)
            {
                username = txt_Usuario.Text;
            }
            if (sender.GetType() == typeof(PasswordBox) && ((PasswordBox)sender) == txt_Contrasena)
            {
                password = txt_Contrasena.Password;
            }
            if (Correo.Length > 1 && username != "" && password != "")
            {
                btn_Iniciar_Secion.IsEnabled = true;
            }
            else
            {
                btn_Iniciar_Secion.IsEnabled = false;
            }
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
            if (txt_Usuario.Text == "Nombre de Usuario")
            {
                return;
            }
            if (txt_Usuario.Text == "")
            {
                MessageBox.Show("Ingrese su Nombre de Usuario");
                return;
            }
            if (txt_Contrasena.Password == "")
            {
                return;
            }
            bdWaiting.Visibility = Visibility.Visible;
            ModoLogin(true);
            object args = new object[2] { txt_Usuario.Text, txt_Contrasena.Password };
            var loginProcess = new Thread(LogueoBg);
            loginProcess.Start(args);

        }
        private void LogueoBg(object args)
        {
            Array argArray = new object[3];
            argArray = (Array)args;
            //Application.Current.Dispatcher.Invoke(new Action<bool>(ModoLogin), true);
            idUsuario = UsuarioBrl.Obtener_Id_Usuario((string)argArray.GetValue(0), (string)argArray.GetValue(1));


            //Thread.Sleep(5000);



            Application.Current.Dispatcher.Invoke(new Action<Guid>(UIAccept), idUsuario);
            //if (idUsuario != Guid.Empty)
            //{
            //    Application.Current.Dispatcher.Invoke(new Action<Guid>(UIAccept), idUsuario);
            //    Application.Current.Dispatcher.Invoke(new Action(txt_Usuario.Clear));
            //    Application.Current.Dispatcher.Invoke(new Action(txt_Contrasena.Clear));
            //    ViewMain viewMain = new ViewMain(this, idUsuario);
            //    viewMain.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Nombre de Usuario o Contraseña incorrectos", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    Application.Current.Dispatcher.Invoke(new Action<bool>(ModoLogin), true);
            //}
        }
        private void UIAccept(Guid idUsuario)
        {
            bdWaiting.Visibility = Visibility.Hidden;
            if (idUsuario != Guid.Empty)
            {
                txt_Usuario.Clear();
                txt_Contrasena.Clear();
                ViewMain viewMain = new ViewMain(this, idUsuario);
                viewMain.Show();
                ModoLogin(false);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nombre de Usuario o Contraseña incorrectos", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                ModoLogin(false);
            }
        }
        private void ModoLogin(bool estado)
        {
            txt_Usuario.IsEnabled = !estado;
            txt_Contrasena.IsEnabled = !estado;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistroWindow registroWindow = new RegistroWindow(this);
            registroWindow.Show();
            this.Hide();
        }
    }
}
