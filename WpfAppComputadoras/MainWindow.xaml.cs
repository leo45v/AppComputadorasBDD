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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas;

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
            Guid idNuevo = Guid.NewGuid();
            UsuarioBrl.Insertar(new Usuario()
            {
                Contrasenia = "123",
                Eliminado = false,
                IdUsuario = idNuevo,
                NombreUsuario = "Pedrito3",
                Rol = new Rol()
                {
                    IdRol = 2,
                }
            });
            Usuario miUsuario = UsuarioBrl.Seleccionar(idNuevo);
            if (miUsuario != null)
            {
                lblPrueba.Content = String.Format("Usuario: {0}\n\rContraseña: {1}\n\rEliminado: {2}\n\rRol: {3}\n\rID: {4}",
                    miUsuario.NombreUsuario, miUsuario.Contrasenia,
                    miUsuario.Eliminado, miUsuario.Rol.NombreRol, miUsuario.IdUsuario);
                miUsuario.NombreUsuario = "pollito";
                miUsuario.Contrasenia = "321";
                UsuarioBrl.Actualizar(miUsuario);
                UsuarioBrl.Borrar(miUsuario);
            }

            Usuario verificarUsuario = UsuarioBrl.Seleccionar(idNuevo);
            if (verificarUsuario != null)
            {
                lblPrueba.Content = "El Usuario \"" + idNuevo.ToString() + "\" No existe";
            }



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
