using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCMainStore.xaml
    /// </summary>
    public partial class UCMainStore : UserControl
    {
        private string typeProduct;
        private int cantidad = 0;
        private int indexer = 0;
        private List<UCProductDescription> ucPX = new List<UCProductDescription>();
        public string TypeProduct
        {
            get { return typeProduct; }
            set
            {
                string[] justInCase = value.Split('_');
                typeProduct = value;
                if (justInCase.Length > 1)
                {
                    typeProduct = "";
                    foreach (string text in justInCase)
                    {
                        typeProduct += text + " ";
                    }
                    typeProduct = typeProduct.Trim();
                }
                LoadDataProduct(typeProduct);
            }
        }

        public UCMainStore()
        {
            InitializeComponent();

            int row = 1, col = 1;
            for (int i = 0; i < 9; i++)
            {
                UCProductDescription px = new UCProductDescription("Empty");
                if (i == 5)
                {
                    row += 2;
                    col = 1;
                }
                Grid.SetRow(px, row);
                Grid.SetColumn(px, col);
                gridListItems.Children.Add(px);
                ucPX.Add(px);
                col += 2;
            }

        }
        public UCMainStore(string type)
        {
            InitializeComponent();
            this.typeProduct = type;
        }

        private void LoadDataProduct(string type)
        {
            List<Producto> productsList = null;
            Set_Visibility_UCPX(Visibility.Hidden);
            lblTitle.Content = "Lista de " + type;
            if (type == "Ram")
            {
                productsList = RamBrl.GetWithRange(0, 9);
                cantidad = RamBrl.Count();
            }
            else if (type == "Procesador")
            {
            }
            else if (type == "Almacenamiento")
            {
            }
            else if (type == "Fuente")
            {
            }
            else if (type == "Gabinete")
            {
            }
            else if (type == "Monitor")
            {
            }
            else if (type == "Mother Board")
            {
            }
            else if (type == "Tarjeta Grafica")
            {
            }
            if (!(productsList is null))
            {
                indexer = (int)Math.Floor((decimal)(cantidad / 9));
                ChangeButtons(cantidad, 0);
                int counter = 0;
                foreach (Producto item in productsList)
                {
                    PrintProductInWindows(item, ucPX[counter]);
                    counter++;
                }
            }
        }

        private void PrintProductInWindows(Producto producto, UCProductDescription ucProductDesc)
        {
            ucProductDesc.Visibility = Visibility.Visible;
            ucProductDesc.NameProduct = producto.Nombre;
            ucProductDesc.UnitPrice = "Bs." + producto.PrecioUnidad.ToString("F");
            ucProductDesc.UnitPriceFix = "Bs." + (producto.PrecioUnidad - (decimal)5.00).ToString("F"); ;
            ucProductDesc.Stock = producto.Stock;
            //ucPX[counter].imgProduct 
        }

        private void Set_Visibility_UCPX(Visibility visibility)
        {
            //ucPX.ForEach(x => x.Visibility = visibility);
            foreach (UCProductDescription item in ucPX)
            {
                item.Visibility = visibility;
            }
        }
        private void ChangeButtons(int counter, int indexer)
        {

            if (indexer == 0 && counter > 9)
            {
                btnSiguiente.Visibility = Visibility.Visible;
                btnAtras.Visibility = Visibility.Visible;
                btnSiguiente.IsEnabled = true;
                btnAtras.IsEnabled = false;
            }
            if (indexer > 0)
            {
                btnSiguiente.Visibility = Visibility.Visible;
                btnAtras.Visibility = Visibility.Visible;
                btnSiguiente.IsEnabled = true;
                btnAtras.IsEnabled = true;
            }
            else
            {
                btnSiguiente.Visibility = Visibility.Hidden;
                btnAtras.Visibility = Visibility.Hidden;
                btnSiguiente.IsEnabled = false;
                btnAtras.IsEnabled = false;
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            int indice = (int)Math.Floor((decimal)(cantidad / 9));
            indexer++;
            if (indexer > indice)
            {
                indexer = indice;
            }
            ChangeButtons(cantidad, indexer);
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            indexer--;
            if (indexer < 0)
            {
                indexer = 0;
            }
            ChangeButtons(cantidad, indexer);
        }
    }
}
