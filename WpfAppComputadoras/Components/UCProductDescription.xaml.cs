using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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

namespace WpfAppComputadoras.Components
{
    /// <summary>
    /// Interaction logic for UCProductDescription.xaml
    /// </summary>
    public partial class UCProductDescription : UserControl
    {
        private string nameProduct;

        public string NameProduct
        {
            private get { return nameProduct; }
            set
            {
                txtName.Text = value;
                nameProduct = value;
            }
        }
        private string stockProduct;

        public string StockProduct
        {
            private get { return stockProduct; }
            set { txtStock.Text = value; stockProduct = value; }
        }
        private string unitPrice;

        public string UnitPrice
        {
            private get { return unitPrice; }
            set
            {
                txtPrice.Text = value;
                unitPrice = value;
                ChanceUnitPrice();
            }
        }
        private string unitPriceFix;

        public string UnitPriceFix
        {
            private get { return unitPriceFix; }
            set
            {
                txtPriceFix.Text = value;
                unitPriceFix = value;
                ChanceUnitPrice();
            }
        }

        private int stock;
        public int Stock
        {
            private get { return stock; }
            set
            {
                stock = value;
                if (stock > 0)
                {
                    txtStock.Foreground = new SolidColorBrush(Color.FromArgb(255, 75, 255, 75));
                    txtStock.Text = "En Stock";
                    RadialGradientBrush colorRadialStock = new RadialGradientBrush();
                    colorRadialStock.GradientStops.Add(new GradientStop(Color.FromArgb(255, 203, 238, 212), 0.0));
                    colorRadialStock.GradientStops.Add(new GradientStop(Color.FromArgb(255, 3, 255, 72), 1.0));
                    stockColor.Background = colorRadialStock;
                }
                else
                {
                    txtStock.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 75, 75));
                    txtStock.Text = "Sin Stock";
                    RadialGradientBrush colorRadialStock = new RadialGradientBrush();
                    colorRadialStock.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 168, 183), 0.0));
                    colorRadialStock.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 0, 85), 1.0));
                    stockColor.Background = colorRadialStock;
                }
            }
        }
        public UCProductDescription()
        {
            InitializeComponent();
        }
        public UCProductDescription(string type)
        {
            InitializeComponent();
            if (type == "Empty")
            {
                this.Visibility = Visibility.Hidden;
            }
        }
        private void ChanceUnitPrice()
        {
            if (unitPriceFix != "")
            {
                txtPrice.TextDecorations.Add(TextDecorations.Strikethrough);
                txtPrice.Foreground = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            }
            else
            {
                txtPrice.TextDecorations.Clear();
                txtPrice.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }


    }
}
