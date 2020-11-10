using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for ViewMain.xaml
    /// </summary>
    public partial class ViewMain : Window
    {
        private readonly MainWindow mainWindow;
        private Dictionary<string, UCProductView> menuProducts = new Dictionary<string, UCProductView>();
        public ViewMain(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            //ucProcesador.lblProduct.Content = "Procesadores";
            //ucProcesador.imgProduct.Source = LoadImage("assets/procesadores.jpg");
            //ucProcesador.imgProduct.Stretch = Stretch.Fill;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mover_Ventana_Controller(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public static BitmapImage LoadImage(string path)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.StreamSource = new MemoryStream(File.ReadAllBytes(@"" + path));
            bi3.EndInit();
            return bi3;
        }
    }
}
