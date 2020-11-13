using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<UCProductDescription> ucPX = new List<UCProductDescription>();
        private byte idMarcaSelectFromCb;

        public byte IdMarcaSelectFromCb
        {
            private get { return idMarcaSelectFromCb; }
            set { idMarcaSelectFromCb = value; }
        }

        private string idMarcaSelect = null;
        public string IdMarcaSelect
        {
            private get { return idMarcaSelect; }
            set
            {
                idMarcaSelect = value;
                LoadDataProduct(typeProduct, idMarcaSelect);
            }
        }
        public string TypeProduct
        {
            get { return typeProduct; }
            set
            {
                string[] justInCase = value.Split('_');
                string aux = value;
                if (justInCase.Length > 1)
                {
                    aux = "";
                    foreach (string text in justInCase)
                    {
                        aux += text + " ";
                    }
                    aux = aux.Trim();
                }
                if (aux == typeProduct)
                {
                    return;
                }
                typeProduct = aux;
                LoadMarca(typeProduct);
                LoadDataProduct(typeProduct, null);
            }
        }

        public UCMainStore()
        {
            InitializeComponent();
            int row = 1, col = 1;
            for (int i = 0; i < 10; i++)
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
            LoadDataProduct("Procesadores", idMarcaSelect);
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
            //sliderPrice.LowerSlider.ValueChanged += EventSliderLower;
            //sliderPrice.UpperSlider.ValueChanged += EventSliderUpper;
            //sliderPrice.LowerSlider.Value = sliderPrice.LowerValue;
            //sliderPrice.UpperSlider.Value = sliderPrice.UpperValue;
            //txtMinValue.Text = "Bs." + sliderPrice.LowerValue;
            //txtMaxValue.Text = "Bs." + sliderPrice.UpperValue;

            var dp = DependencyPropertyDescriptor.FromProperty(
             TextBlock.TextProperty,
             typeof(TextBlock));
            dp.AddValueChanged(txtMinValue, (sender, args) =>
            {
                LoadDataProduct(typeProduct, idMarcaSelect);
            });
            dp.AddValueChanged(txtMaxValue, (sender, args) =>
            {
                LoadDataProduct(typeProduct, idMarcaSelect);
            });
        }
        public UCMainStore(string type)
        {
            InitializeComponent();
            this.typeProduct = type;
        }
        private void LoadDataProduct(string type, string? idMarca)
        {
            LoadDataProduct(type, 0, 0, idMarca);
        }
        private void LoadDataProduct(string type, int start, int index, string? idMarca)
        {
            List<Producto> productsList = null;
            Set_Visibility_UCPX(Visibility.Hidden);
            lblTitle.Text = "Lista de " + type;
            cantidad = 0;
            byte valuex = idMarca is null ? (byte)0 : idMarca is null ? (byte)0 : idMarcaSelectFromCb;
            if (type == "Ram")
            {
                productsList = RamBrl.GetWithRange(start, 10, valuex, sliderPrice.LowerValue, sliderPrice.UpperValue);
                cantidad = RamBrl.Count(valuex, sliderPrice.LowerValue, sliderPrice.UpperValue);
            }
            else if (type == "Procesador")
            {
                productsList = ProcesadorBrl.GetWithRange(start, 10, valuex, sliderPrice.LowerValue, sliderPrice.UpperValue);
                cantidad = ProcesadorBrl.Count(valuex, sliderPrice.LowerValue, sliderPrice.UpperValue);
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
            ChangeButtons(cantidad, index);
            if (!(productsList is null))
            {
                //indexer = (int)Math.Floor((decimal)(cantidad / 10));
                int counter = 0;
                foreach (Producto item in productsList)
                {
                    PrintProductInWindows(item, ucPX[counter]);
                    counter++;
                }
            }
        }
        private void LoadMarca(string type)
        {
            if (type == "Ram")
            {
                cbMarca.ItemsSource = RamBrl.Get_ListMarcasRam();
            }
            else if (type == "Procesador")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Almacenamiento")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Fuente")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Gabinete")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Monitor")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Mother Board")
            {
                cbMarca.ItemsSource = null;
            }
            else if (type == "Tarjeta Grafica")
            {
                cbMarca.ItemsSource = null;
            }

        }
        public void PrintProductInWindows(Producto producto, UCProductDescription ucProductDesc)
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
            if (indexer == 0 && counter > 10)
            {
                btnSiguiente.Visibility = Visibility.Visible;
                btnAtras.Visibility = Visibility.Visible;
                btnSiguiente.IsEnabled = true;
                btnAtras.IsEnabled = false;
            }
            else if (indexer != 0 && (indexer + 1) * 10 > counter)
            {
                btnSiguiente.Visibility = Visibility.Visible;
                btnAtras.Visibility = Visibility.Visible;
                btnSiguiente.IsEnabled = false;
                btnAtras.IsEnabled = true;
            }
            else if (indexer > 0)
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
            int indice = (int)Math.Floor((decimal)(cantidad / 10));
            indexer++;
            if (indexer > indice)
            {
                indexer = indice;
            }
            ChangeButtons(cantidad, indexer);
            LoadDataProduct(TypeProduct, indexer * 10, indexer, idMarcaSelect);
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            indexer--;
            if (indexer < 0)
            {
                indexer = 0;
            }
            ChangeButtons(cantidad, indexer);
            LoadDataProduct(TypeProduct, indexer * 10, indexer, idMarcaSelect);
        }
        private void EventSliderLower(object sender, RoutedEventArgs e)
        {
            txtMinValue.Text = "Bs." + sliderPrice.LowerSlider.Value;
        }
        private void EventSliderUpper(object sender, RoutedEventArgs e)
        {
            txtMaxValue.Text = "Bs." + sliderPrice.UpperSlider.Value;
        }

        private void cbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idMarcaSelect = null;
            if (cbMarca.Items.Count > 0)
            {
                idMarcaSelect = ((Marca)cbMarca.SelectedItem).NombreMarca;
                idMarcaSelectFromCb = ((Marca)cbMarca.SelectedItem).IdMarca;
                LoadDataProduct(typeProduct, idMarcaSelect);
            }
        }
    }
}
