using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WpfAppComputadoras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProductosBrl productos = new ProductosBrl();
            productos.rams.Add(new Ram()
            {
                Descontinuado = false,
                Frecuencia = 3000,
                IdProducto = Guid.NewGuid(),
                Imagen = "",
                Latencia = 23,
                Marca = new Marca()
                {
                    IdMarca = 5,
                },
                Memoria = 16,
                Nombre = "Ballistix",
                PrecioUnidad = 70,
                Stock = 10
            });
            //foreach (var item in productos.rams)
            //{
            //    Console.WriteLine(item);
            //}



        }
    }
}
