using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.Pages
{
    /// <summary>
    /// Interaction logic for UCPageStore.xaml
    /// </summary>
    public partial class UCPageStore : UserControl
    {
        private readonly Dictionary<string, UCProductView> menuProducts = new Dictionary<string, UCProductView>();

        public UCPageStore()
        {
            InitializeComponent();
            CreateComponentsProducts();
        }
        private void CreateComponentsProducts()
        {
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
                    menuProducts[key].imgProduct.ImageSource = ViewMain.LoadImage("assets/" + key + ".jpg");
                }
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
            //MessageBox.Show("ORIGINAL: " + key + " AUXILIAR: " + aux);
            ucMainStore.TypeProduct = key;
        }
    }
}
