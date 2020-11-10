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
            CreateComponentsProducts();
        }
        private void CreateComponentsProducts()
        {
            //< Components:UCProductView x:Name = "ucMotherboard" HorizontalAlignment = "Center" VerticalAlignment = "Center" Grid.Row = "4" />
            menuProducts.Add("Procesador", new UCProductView());
            menuProducts.Add("Tarjeta_Grafica", new UCProductView());
            menuProducts.Add("Mother_Board", new UCProductView());
            menuProducts.Add("Fuente", new UCProductView());
            menuProducts.Add("Gabinete", new UCProductView());
            menuProducts.Add("Monitor", new UCProductView());
            menuProducts.Add("Ram", new UCProductView());
            menuProducts.Add("Almacenamiento", new UCProductView());
            int rowMenu = 0;
            foreach ((string key, UCProductView value) in menuProducts)
            {
                menuProducts[key].button.Name = key;
                menuProducts[key].button.Click += btnMenuProduct_Click;
                menuProducts[key].HorizontalAlignment = HorizontalAlignment.Center;
                menuProducts[key].VerticalAlignment = VerticalAlignment.Center;
                menuProducts[key].lblProduct.Content = key;
                if (File.Exists(@"assets/" + key + ".jpg"))
                {
                    menuProducts[key].imgProduct.ImageSource = LoadImage("assets/" + key + ".jpg");
                }
                //menuProducts[key].imgProduct.Stretch = Stretch.Fill;
                Grid.SetColumn(menuProducts[key], 0);
                Grid.SetRow(menuProducts[key], rowMenu);
                gridMenuProducts.Children.Add(menuProducts[key]);
                rowMenu += 2;
            }

        }

        private void btnMenuProduct_Click(object sender, RoutedEventArgs e)
        {
            //string key = menuProducts.FirstOrDefault(element => element.Value == (UCProductView)sender).Key;
            string key = ((Button)sender).Name;

            string[] justInCase = key.Split('_');
            string aux = key;
            if (justInCase.Length > 1)
            {
                aux = "";
                foreach (string text in justInCase)
                {
                    aux += text + " ";
                }
                aux = aux.Trim();
            }


            MessageBox.Show("ORIGINAL: " + key + " AUXILIAR: " + aux);


            ucMainStore.TypeProduct = key;
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
