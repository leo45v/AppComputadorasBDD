using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Extras;

namespace WpfAppComputadoras.Extra
{
    public class Methods
    {
        public static BitmapImage LoadImageX(string path)
        {
            string pathImg = String.IsNullOrWhiteSpace(path) ? "assets/null.jpg" : path;
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.StreamSource = new MemoryStream(File.ReadAllBytes(@"" + pathImg));
            bi3.EndInit();
            return bi3;
        }
        public static BitmapImage LoadImage(string path)
        {
            string pathImg = "pack://application:,,/assets/null.jpg";// Path.Combine(Directory.GetCurrentDirectory(), "assets /null.jpg");
            if (!String.IsNullOrWhiteSpace(path) && File.Exists(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "assets", path))))
            {
                pathImg = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "assets", path));
            }
            var uri = new Uri(pathImg, UriKind.RelativeOrAbsolute);
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = uri;
            bi3.EndInit();
            return bi3;
        }
        public static int RedondeoSiempre(double number)
        {
            int n = (int)number;
            double decimales = number - n;
            if (decimales != 0)
            {
                n++;
            }
            return n;
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
        private static string lastDirectory = @"c:\";
        private static string directoryMedia = @"assets\Products";
        public static string[] SelectImagen(string tipeProduct)
        {
            string[] pathImage = new string[2];
            try
            {
                OpenFileDialog abrir_ = new OpenFileDialog
                {
                    Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif",
                    InitialDirectory = lastDirectory,
                    Title = "Seleccione una imagen para cargar."
                };
                if (abrir_.ShowDialog() == true)
                {
                    string pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), directoryMedia, tipeProduct);
                    string fileName = abrir_.FileName.Substring(abrir_.FileName.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(pathDirectory))
                    {
                        Directory.CreateDirectory(pathDirectory);

                    }
                    if (Archivo.Guardar_Imagen(pathDirectory, new Archivo()
                    {
                        PathUri = abrir_.FileName
                    }))
                    {
                        pathImage[0] = Path.Combine(@"Products", tipeProduct, fileName);
                        pathImage[1] = abrir_.FileName;
                    }
                    lastDirectory = abrir_.FileName;
                    //pathImage = abrir_.FileName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pathImage;
        }

    }
    public class ComboColor : Colores
    {
        public Boolean Check_Status
        {
            get;
            set;
        }
        private Brush myVar;

        public Brush ColorPath
        {
            get { return new SolidColorBrush(Color.FromRgb(ColorRGB.R, ColorRGB.G, ColorRGB.B)); }
            private set { myVar = value; }
        }
    }
}
