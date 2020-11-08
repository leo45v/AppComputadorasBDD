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
        private void MoveGlobal(string activeButton, object component)
        {
            Mover_Entre_Casillas(activeButton, "Nombre", (TextBox)component, txt_Nombre, "Nombre");
            Mover_Entre_Casillas(activeButton, "Apellido", (TextBox)component, txt_Apellido, "Nombre");
            Mover_Entre_Casillas(activeButton, "Email", (TextBox)component, txt_Email, "Ingresa tu e-mail");
            Mover_Entre_Casillas(activeButton, "EmailR", (TextBox)component, txt_Email_Reiter, "Ingresa tu e-mail de  nuevo");
            Mover_Entre_Casillas(activeButton, "Password1", (TextBox)component, txt_Password_1, "Ingresa tu contraseña");
            Mover_Entre_Casillas(activeButton, "Password2", (TextBox)component, txt_Password_2, "Vuelva a ingresa tu contraseña");
            Mover_Entre_Casillas(activeButton, "NombreUsuario", (TextBox)component, txt_NombreUsuario, "Nombre de Usuario");
        }
        private void check_Aceptar_Condicion_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Condiciones", new TextBox());
            //Mover_Entre_Casillas("Condiciones", "Nombre", new TextBox(), txt_Nombre, "Nombre");
            //Mover_Entre_Casillas("Condiciones", "Apellido", new TextBox(), txt_Apellido, "Nombre");
            //Mover_Entre_Casillas("Condiciones", "Email", new TextBox(), txt_Email, "Ingresa tu e-mail");
            //Mover_Entre_Casillas("Condiciones", "EmailR", new TextBox(), txt_Email_Reiter, "Ingresa tu e-mail de  nuevo");
            //Mover_Entre_Casillas("Condiciones", "Password1", new TextBox(), txt_Password_1, "Ingresa tu contraseña");
            //Mover_Entre_Casillas("Condiciones", "Password2", new TextBox(), txt_Password_2, "Vuelva a ingresa tu contraseña");
            //Mover_Entre_Casillas("Condiciones", "NombreUsuario", new TextBox(), txt_NombreUsuario, "Nombre de Usuario");

        }
        private void check_AceptarRegistro_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Aceptar", new TextBox());
            //Mover_Entre_Casillas("Aceptar", "Nombre", new TextBox(), txt_Nombre, "Nombre");
            //Mover_Entre_Casillas("Aceptar", "Apellido", new TextBox(), txt_Apellido, "Nombre");
            //Mover_Entre_Casillas("Aceptar", "Email", new TextBox(), txt_Email, "Ingresa tu e-mail");
            //Mover_Entre_Casillas("Aceptar", "EmailR", new TextBox(), txt_Email_Reiter, "Ingresa tu e-mail de  nuevo");
            //Mover_Entre_Casillas("Aceptar", "Password1", new TextBox(), txt_Password_1, "Ingresa tu contraseña");
            //Mover_Entre_Casillas("Aceptar", "Password2", new TextBox(), txt_Password_2, "Vuelva a ingresa tu contraseña");
            //Mover_Entre_Casillas("Aceptar", "NombreUsuario", new TextBox(), txt_NombreUsuario, "Nombre de Usuario");
        }
        private void combo_Sexo_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Sexo", new TextBox());
            //Mover_Entre_Casillas("Sexo", "Nombre", new TextBox(), txt_Nombre, "Nombre");
            //Mover_Entre_Casillas("Sexo", "Apellido", new TextBox(), txt_Apellido, "Nombre");
            //Mover_Entre_Casillas("Sexo", "Email", new TextBox(), txt_Email, "Ingresa tu e-mail");
            //Mover_Entre_Casillas("Sexo", "EmailR", new TextBox(), txt_Email_Reiter, "Ingresa tu e-mail de  nuevo");
            //Mover_Entre_Casillas("Sexo", "Password1", new TextBox(), txt_Password_1, "Ingresa tu contraseña");
            //Mover_Entre_Casillas("Sexo", "Password2", new TextBox(), txt_Password_2, "Vuelva a ingresa tu contraseña");
            //Mover_Entre_Casillas("Sexo", "NombreUsuario", new TextBox(), txt_NombreUsuario, "Nombre de Usuario");
        }
        private void combo_Mes_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Mes", new TextBox());
        }
        private void combo_Dia_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Dia", new TextBox());
        }
        private void combo_Año_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Año", new TextBox());
        }
        private void txt_Nombre_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Nombre", txt_Nombre);
        }
        private void txt_Apellido_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Apellido", txt_Apellido);
        }
        private void txt_Email_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Email", txt_Email);
            grid_Division.RowDefinitions[13].Height = new GridLength(35);
            txt_Email_Corrige.Visibility = Visibility.Visible;
        }
        private void txt_Email_Reiter_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("EmailR", txt_Email_Reiter);
            grid_Division.RowDefinitions[13].Height = new GridLength(35);
            txt_Email_Corrige.Visibility = Visibility.Visible;
        }
        private void txt_Password_1_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Password1", txt_Password_1);
            grid_Division.RowDefinitions[15].Height = new GridLength(125);
            borde_Ground1.Visibility = Visibility.Visible;
            borde_Ground2.Visibility = Visibility.Visible;
            borde_Ground3.Visibility = Visibility.Visible;
        }
        private void txt_Password_2_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("Password2", txt_Password_2);
            grid_Division.RowDefinitions[15].Height = new GridLength(125);
            borde_Ground1.Visibility = Visibility.Visible;
            borde_Ground2.Visibility = Visibility.Visible;
            borde_Ground3.Visibility = Visibility.Visible;
        }
        private void txt_NombreUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            MoveGlobal("NombreUsuario", txt_NombreUsuario);
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
            txt_Password_1_TextChanged(sender, e);
        }
        private void txt_Password_1_KeyUp(object sender, KeyEventArgs e)
        {
            //int ultima_Posicion = txt_Password_1.Text.Length - 1;
            //  txt_Password_1.Select(txt_Password_1.Text.Length, 0);
        }
        private void txt_Password_1_KeyDown(object sender, KeyEventArgs e) { }
        private void txt_Password_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (editable)
            {

                int strengthlevel = 0;
                string password = txt_Password_1.Text;
                int STRENGTH_NULL = 0;
                int STRENGTH_SHORT = 1;
                int STRENGTH_WEAK = 2;
                int STRENGTH_FAIR = 3;
                int STRENGTH_STRONG = 4;
                string[] Niveles = { " ", "Muy corta", "Endeble", "Regular", "Fuerte" };
                if (Tiene_Numero(password) && Verificar_Texto_N(password, 8))
                { strengthlevel = STRENGTH_STRONG; }
                else if (String.IsNullOrEmpty(password))
                { strengthlevel = STRENGTH_NULL; }
                else if (Verificar_Texto_N(password, 8))
                { strengthlevel = STRENGTH_FAIR; }
                else if (Verificar_Texto_N(password, 4))
                { strengthlevel = STRENGTH_WEAK; }
                else
                { strengthlevel = STRENGTH_SHORT; }
                Brush color = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
                if (txt_Password_1.Foreground.ToString() == color.ToString())
                {
                    strengthlevel = STRENGTH_NULL;
                }
                Activar_Imagen_String_Forting(strengthlevel, Niveles[strengthlevel]);
                if (txt_Password_1.Text.Length >= 8)
                {
                    ///level1 cambiar imagen....
                    Imagen_Btn_Move(level1, "Requisitos/acept.png");
                    level1_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 130, 20));
                    Nivel1 = true;
                }
                else if (txt_Password_1.Text.Length < 8 && txt_Password_1.Text.Length > 0)
                {
                    Imagen_Btn_Move(level1, "Requisitos/Punto.png");
                    level1_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2));
                    Nivel1 = false;
                }
                else
                {
                    Nivel1 = false;
                    Nivel2 = false;
                    Nivel3 = false;
                    Nivel4 = false;
                    Nivel5 = false;
                    Imagen_Btn_Move(level1, "Requisitos/Punto1.png");
                    level1_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

                    Imagen_Btn_Move(level2, "Requisitos/Punto1.png");
                    level2_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

                    Imagen_Btn_Move(level3, "Requisitos/Punto1.png");
                    level3_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

                    Imagen_Btn_Move(level4, "Requisitos/Punto1.png");
                    level4_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

                    Imagen_Btn_Move(level5, "Requisitos/Punto1.png");
                    level5_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

                }
                bool tiene_Numeros = false;
                bool tiene_Letras = false;
                bool tiene_Simbolos = false;
                foreach (char caracter in txt_Password_1.Text)
                {
                    if (Char.IsDigit(caracter))
                    {
                        tiene_Numeros = true;
                    }
                    if (Char.IsLetter(caracter))
                    {
                        tiene_Letras = true;
                    }
                    if (Char.IsSymbol(caracter))
                    {
                        tiene_Simbolos = true;
                    }
                }
                if ((tiene_Numeros || tiene_Letras) && tiene_Simbolos == false)
                {
                    Imagen_Btn_Move(level3, "Requisitos/acept.png");
                    level3_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 130, 20));
                    Nivel3 = true;
                }
                else if (tiene_Simbolos)
                {
                    Nivel3 = false;
                    Imagen_Btn_Move(level3, "Requisitos/Punto.png");
                    level3_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2));
                }
                var email_nombre = new List<string>();
                foreach (string partes in txt_Email.Text.Split('@'))
                {
                    email_nombre.Add(partes);
                }
                if (txt_Password_1.Text.ToLower().Contains(email_nombre[0]) && (!String.IsNullOrEmpty(email_nombre[0])))
                {
                    Nivel4 = false;
                    Imagen_Btn_Move(level4, "Requisitos/Punto.png");
                    level4_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2));
                }
                else if (!txt_Password_1.Text.ToLower().Contains(email_nombre[0]))
                {
                    Nivel4 = true;
                    Imagen_Btn_Move(level4, "Requisitos/acept.png");
                    level4_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 130, 20));
                }
                if (txt_Password_1.Text == txt_Password_2.Text)
                {
                    Nivel5 = true;
                    Imagen_Btn_Move(level5, "Requisitos/acept.png");
                    level5_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 130, 20));
                }
                else if (txt_Password_2.Text.Length > 0)
                {
                    Nivel5 = false;
                    Imagen_Btn_Move(level5, "Requisitos/Punto.png");
                    level5_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2));
                }
                for (int i = 0; i < txt_Password_1.Text.Length; i++)
                {
                    if (!(Char.IsNumber(txt_Password_1.Text[i]) || Char.IsLetter(txt_Password_1.Text[i])))
                    {
                        Nivel2 = false;
                        Imagen_Btn_Move(level2, "Requisitos/Punto.png");
                        level2_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2));
                        return;
                    }
                    else if (txt_Password_1.Text.Length > 0)
                    {
                        Nivel2 = true;
                        Imagen_Btn_Move(level2, "Requisitos/acept.png");
                        level2_R.Foreground = new SolidColorBrush(Color.FromArgb(255, 10, 130, 20));
                    }
                }
                if (!(Nivel1 && Nivel2 && Nivel3 && Nivel4 && Nivel5))
                {
                    txt_Password_1.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                    txt_Password_2.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                    bloque_Contrasena.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                }
                else
                {
                    txt_Password_1.BorderBrush = new SolidColorBrush();
                    txt_Password_2.BorderBrush = new SolidColorBrush();
                    bloque_Contrasena.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }
        }
        private void Btn_Create_Account(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Mover_Entre_Casillas(string Casilla_Actual, string Casilla_Siguiente, TextBox Casilla_Texto_Actual, TextBox Casilla_Texto_Siguiente, string Texto_Complementario)
        {
            if (Casilla_Texto_Actual == Casilla_Texto_Siguiente)
            {
                return;
            }
            Casilla_Texto_Actual.Background = new SolidColorBrush(Colors.White);
            if (boton_activo["Email"] || boton_activo["EmailR"])
            {
                grid_Division.RowDefinitions[13].Height = new GridLength(35);
                txt_Email_Corrige.Visibility = Visibility.Visible;
                //txt_Email_Corrige1.Visibility = Visibility.Hidden;
                if (txt_Email.Text != txt_Email_Reiter.Text)
                {
                    txt_Email_Corrige1.Visibility = Visibility.Visible;
                    txt_Email_Corrige.Visibility = Visibility.Visible;
                }
                else
                {
                    txt_Email_Corrige1.Visibility = Visibility.Hidden;
                    txt_Email_Corrige.Visibility = Visibility.Hidden;
                    grid_Division.RowDefinitions[13].Height = new GridLength(10);
                }
            }
            else
            {
                txt_Email_Corrige1.Visibility = Visibility.Hidden;
                txt_Email_Corrige.Foreground = new SolidColorBrush(Colors.Gray);
                txt_Email.BorderBrush = new SolidColorBrush();
                txt_Email_Corrige.Text = "Este será el nombre de usuario que utilizarás\npara conectarte.";
                bloque_Email.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
            if (boton_activo["Password1"] || boton_activo["Password2"])
            {
                grid_Division.RowDefinitions[15].Height = new GridLength(125);
                borde_Ground1.Visibility = Visibility.Visible;
                borde_Ground2.Visibility = Visibility.Visible;
                borde_Ground3.Visibility = Visibility.Visible;
            }
            else
            {
                borde_Ground1.Visibility = Visibility.Hidden;
                borde_Ground2.Visibility = Visibility.Hidden;
                borde_Ground3.Visibility = Visibility.Hidden;
            }
            if (boton_activo[Casilla_Actual] == false)
            {
                Casilla_Texto_Actual.Clear();
                Casilla_Texto_Actual.Background = new SolidColorBrush(Colors.White);
                Casilla_Texto_Actual.Foreground = new SolidColorBrush(Colors.Black);
                boton_activo[Casilla_Actual] = true;
                if ((!MainWindow.Comprobar_Formato_Email_(txt_Email.Text)))
                {
                    txt_Email_Corrige.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                    txt_Email.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                    bloque_Email.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                    txt_Email_Corrige.Text = "No es una dirección valida de e-mail.";
                }
                if (MainWindow.Comprobar_Formato_Email_(txt_Email.Text))
                {
                    txt_Email_Corrige.Foreground = new SolidColorBrush(Colors.Gray);
                    txt_Email.BorderBrush = new SolidColorBrush();
                    txt_Email_Corrige.Text = "Este será el nombre de usuario que\nutilizarás para conectarte.";
                    bloque_Email.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

            if (Casilla_Texto_Siguiente.Text == "")
            {
                Casilla_Texto_Siguiente.Background = Color_TextBox_Blanco();
                Casilla_Texto_Siguiente.Foreground = new SolidColorBrush(Color.FromArgb(255, 171, 173, 179));
                Casilla_Texto_Siguiente.Text = Texto_Complementario;
                boton_activo[Casilla_Siguiente] = false;
                grid_Division.RowDefinitions[13].Height = new GridLength(10);
                grid_Division.RowDefinitions[15].Height = new GridLength(10);
                txt_Email_Corrige.Visibility = Visibility.Hidden;
                txt_Email_Corrige1.Visibility = Visibility.Hidden;

                // grid_Division.RowDefinitions[15].Height = new GridLength(10);
                borde_Ground1.Visibility = Visibility.Hidden;
                borde_Ground2.Visibility = Visibility.Hidden;
                borde_Ground3.Visibility = Visibility.Hidden;
            }
            else
            {
                Casilla_Texto_Siguiente.Background = Color_TextBox_Blanco();
                Casilla_Texto_Actual.Foreground = new SolidColorBrush(Colors.Black);
                grid_Division.RowDefinitions[15].Height = new GridLength(10);

                borde_Ground1.Visibility = Visibility.Hidden;
                borde_Ground2.Visibility = Visibility.Hidden;
                borde_Ground3.Visibility = Visibility.Hidden;
            }
            if (!(Nivel1 && Nivel2 && Nivel3 && Nivel4 && Nivel5))
            {
                txt_Password_1.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                txt_Password_2.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
                bloque_Contrasena.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 98, 98));
            }
            else
            {
                txt_Password_1.BorderBrush = new SolidColorBrush();
                txt_Password_2.BorderBrush = new SolidColorBrush();
                bloque_Contrasena.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }
        private LinearGradientBrush Color_TextBox_Blanco()
        {
            LinearGradientBrush Color_Gradiante = new LinearGradientBrush();
            Color_Gradiante.StartPoint = new Point(0.5, 0);
            Color_Gradiante.EndPoint = new Point(0.5, 1);
            Color_Gradiante.GradientStops.Add(new GradientStop(Color.FromArgb(255, 199, 199, 199), 0));
            Color_Gradiante.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 255, 255), 0.411));
            return Color_Gradiante;
        }
        private bool Verificar_Texto_N(string password, int tam)
        {
            if (password.Length > tam)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool Tiene_Numero(string password)
        {
            bool tiene_Numero = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsNumber(password[i]))
                {
                    tiene_Numero = true;
                }
            }
            return tiene_Numero;
        }
        private void Activar_Imagen_String_Forting(int Nivel, string Nivele_String)
        {
            Rectangle[] rectangulo = { fuerte1, fuerte5, fuerte4, fuerte3, fuerte2 };
            rectangulo[Nivel].Visibility = Visibility.Visible;
            Bloque_requisitos.Text = "";
            Bloque_requisitos.Inlines.Add("Solidez de la contraseña: ");
            if (Nivel < 2)
            { Bloque_requisitos.Inlines.Add(new Run(Nivele_String) { Foreground = new SolidColorBrush(Color.FromArgb(255, 115, 2, 2)), FontWeight = FontWeights.Bold }); }
            else
            { Bloque_requisitos.Inlines.Add(new Run(Nivele_String) { Foreground = new SolidColorBrush(Color.FromArgb(255, 74, 74, 74)), FontWeight = FontWeights.Bold }); }
            rectangulo[Nivel].IsEnabled = true;
            for (int i = 1; i < rectangulo.Length; i++)
            {
                if (i != Nivel)
                {
                    rectangulo[i].Visibility = Visibility.Hidden;
                    rectangulo[i].IsEnabled = false;
                }
            }
        }
        private void Imagen_Btn_Move(Image Btn_Imagen, string Direccion_Imagen)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("assets/" + Direccion_Imagen, UriKind.Relative);
            bi3.EndInit();
            Btn_Imagen.Stretch = Stretch.Fill;
            Btn_Imagen.Source = bi3;
        }
    }
}
