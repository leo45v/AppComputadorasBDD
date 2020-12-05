using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Pantalla;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Reservas;
using System.Windows.Threading;

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Guid idUsuario = Guid.Empty;
        public List<Colores> ListaColores { get; set; }
        public List<Marca> ListaMarcas { get; set; }
        public List<Resolucion> ListaResolucion { get; set; }
        public List<Ratio> ListaRatio { get; set; }
        public List<SocketProcesador> ListaSockets { get; set; }
        private readonly DispatcherTimer errorTimerRemove;

        public MainWindow()
        {
            InitializeComponent();
            txt_Usuario.Focus();

            Thread thread = new Thread(GetDataBg);
            thread.Start();

            txtError.Visibility = Visibility.Collapsed;
            btn_Iniciar_Secion.IsEnabled = false;

            errorTimerRemove = new DispatcherTimer();
            errorTimerRemove.Tick += QuitarMensajeError;
            errorTimerRemove.Interval = new TimeSpan(0, 0, 5);
        }

        private void QuitarMensajeError(object sender, EventArgs e)
        {
            QuitarMensajeError();
        }

        private void GetDataBg()
        {
            this.ListaMarcas = ConfiguracionesBrl.Marca.GetAll();
            this.ListaColores = ConfiguracionesBrl.Colores.GetAll();
            this.ListaResolucion = ConfiguracionesBrl.Resolucion.GetAll();
            this.ListaRatio = ConfiguracionesBrl.Ratio.GetAll();
            this.ListaSockets = ConfiguracionesBrl.Socket.GetAll();
        }
        private void Mover_Ventana_Controller(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
        }
        private void Txt_Usuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txt_Usuario.Text)
                || String.IsNullOrWhiteSpace(txt_Contrasena.Password))
            {
                btn_Iniciar_Secion.IsEnabled = false;
            }
            else
            {
                btn_Iniciar_Secion.IsEnabled = true;
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
            errorTimerRemove.Stop();
            QuitarMensajeError();
            if (String.IsNullOrWhiteSpace(txt_Usuario.Text))
            {
                PonerMensajeError("Ingrese su Nombre de Usuario");
                return;
            }
            if (String.IsNullOrWhiteSpace(txt_Contrasena.Password))
            {
                PonerMensajeError("Ingrese una Contraseña");
                return;
            }
            bdWaiting.IsOpen = true;
            ModoLogin(true);
            object argumento = new object[2] { txt_Usuario.Text, txt_Contrasena.Password };
            var loginProcess = new Thread(LogueoBg);
            loginProcess.Start(argumento);
        }
        private void LogueoBg(object argumento)
        {
            Array argumentoArray = new object[3];
            argumentoArray = (Array)argumento;
            idUsuario = UsuarioBrl.Obtener_Id_Usuario((string)argumentoArray.GetValue(0), (string)argumentoArray.GetValue(1));
            Application.Current.Dispatcher.Invoke(new Action<Guid>(UIAccept), idUsuario);
        }
        ViewMain viewMain;
        private void UIAccept(Guid idUsuario)
        {
            bdWaiting.IsOpen = false;
            if (idUsuario != Guid.Empty)
            {
                viewMain = new ViewMain(this, idUsuario);
                viewMain.Show();
                ModoLogin(false);
                this.Hide();
            }
            else
            {
                PonerMensajeError("Nombre de Usuario o Contraseña incorrectos");
                ModoLogin(false);
            }
        }
        public void CleanMemory()
        {
            GC.SuppressFinalize(viewMain);
            viewMain = null;
            GC.Collect();
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
