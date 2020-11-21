using System;
using System.Collections.Generic;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.Productos;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl
{
    public class ProductosBrl
    {
        private static AlmacenamientoBrl almacenamientoBrl = new AlmacenamientoBrl();
        public static AlmacenamientoBrl Almacenamiento
        {
            get { return almacenamientoBrl; }
            private set { almacenamientoBrl = value; }
        }
        private static FuenteBrl fuenteBrl = new FuenteBrl();
        public static FuenteBrl Fuente
        {
            get { return fuenteBrl; }
            private set { fuenteBrl = value; }
        }

        private static GabineteBrl gabineteBrl = new GabineteBrl();
        public static GabineteBrl Gabinete
        {
            get { return gabineteBrl; }
            private set { gabineteBrl = value; }
        }
        private static MontiorBrl montiorBrl = new MontiorBrl();
        public static MontiorBrl Montior
        {
            get { return montiorBrl; }
            private set { montiorBrl = value; }
        }
        private static PlacaBaseBrl placaBaseBrl = new PlacaBaseBrl();
        public static PlacaBaseBrl PlacaBase
        {
            get { return placaBaseBrl; }
            private set { placaBaseBrl = value; }
        }
        private static ProcesadorBrl procesadorBrl = new ProcesadorBrl();
        public static ProcesadorBrl Procesador
        {
            get { return procesadorBrl; }
            private set { procesadorBrl = value; }
        }
        private static RamBrl ramBrl = new RamBrl();
        public static RamBrl Ram
        {
            get { return ramBrl; }
            private set { ramBrl = value; }
        }
        private static TarjetaGraficaBrl tarjetaGraficaBrl = new TarjetaGraficaBrl();
        public static TarjetaGraficaBrl TarjetaGrafica
        {
            get { return tarjetaGraficaBrl; }
            private set { tarjetaGraficaBrl = value; }
        }


        public static List<Producto> GetWithRange(int inicio, int cantidad)
        {
            return ProductosDal.GetWithRange(inicio, cantidad);
        }
        public static List<Producto> GetWithRangeWithFillter(int start, int cant, string productName, Marca marca, ETipoProducto tipoProduct)
        {
            return ProductosDal.GetWithRangeWithFillter(start, cant, productName, marca, tipoProduct);
        }
        public static bool Delete(Guid idProducto)
        {
            return ProductosDal.Delete(idProducto);
        }
        public static int Count
        {
            get
            {
                return ProductosDal.CountAll();
            }
        }
        public static int CountWithFilter(string productName, Marca marca, ETipoProducto tipoProduct)
        {
            return ProductosDal.CountWithFilter(productName, marca, tipoProduct);
        }
        public static ETipoProducto GetType(Guid idProducto)
        {
            return ProductosDal.GetType(idProducto);
        }
        public static List<Marca> GetMarcas()
        {
            return ProductosDal.GetMarcas();
        }
        public static List<Colores> GetColores()
        {
            return ProductosDal.GetColores();
        }
    }
}

