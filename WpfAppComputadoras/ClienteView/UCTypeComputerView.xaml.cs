using System;
using System.Collections.Generic;
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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCTypeComputerView.xaml
    /// </summary>
    public partial class UCTypeComputerView : UserControl
    {
        public double presupuesto;
        public TipoComputadora tipoComputadora;
        public object parametros;
        public UCTypeComputerView()
        {
            InitializeComponent();
            presupuesto = 500;//-> procesador buscamos el 50% del presupuesto o en su defecto el mas barato segun caracteristicas....

            ConfiguracionComputerOperation configXD = new ConfiguracionComputerOperation(presupuesto);
            ConfigurationBuildComputer getParametros = new ConfigurationBuildComputer(TipoComputadora.Estudio);
            List<Computadora> computadoras = new List<Computadora>();

            List<Procesador> procesadors = configXD.ProcesadoresRecomendados(getParametros.Requisitos.Estudio.Procesador);
            if (procesadors is null)
            {
                getParametros.Requisitos.Estudio.Procesador.PrecioUnidad.max *= 1.20;//AUMENTAMOS EL MAXIMO en un 20%
                procesadors = configXD.ProcesadoresRecomendados(getParametros.Requisitos.Estudio.Procesador);
            }
            foreach (var procesador in procesadors)
            {
                List<PlacaBase> placaBases = configXD.PlacaBaseRecomendados(getParametros.Requisitos.Estudio.PlacaBase, procesador);
                if (placaBases is null)
                {
                    getParametros.Requisitos.Estudio.Procesador.PrecioUnidad.max *= 1.20;//AUMENTAMOS EL MAXIMO en un 20%
                    placaBases = configXD.PlacaBaseRecomendados(getParametros.Requisitos.Estudio.PlacaBase, procesador);
                }
                foreach (var placaBase in placaBases)
                {
                    Computadora nueva = new Computadora()
                    {
                        Procesador = procesador,
                        PlacaBase = placaBase,
                    };
                    double nuevoPresupuesto = nueva.CostoTotal();
                    //-> BUSCAR -> Rams (LISTA PUEDE SER 1 o 2 SEGUN EL PRESUPUESTO QUE QUEDE ),
                    //-> BUSCAR -> Monitor 
                    //-> BUSCAR -> Gabinete
                    //-> BUSCAR -> TARJETA DE VIDEO -> SEGUN EL PRESUPUESTO QUE QUEDE SOLO SE BUSCARA EN GAMING, TRABAJODISEÑO y ESTUDIO SI ALCANZA
                    //-> BUSCAR -> FUENTE EN BASE AL CONSUMO DE PROCESADOR + GRAFICA -> 

                    computadoras.Add(nueva);
                }
            }

            //-> FILTRAR LAS COMPUTADORAS ARMADAS QUE NO EXCEDAN EL RANGO DE PRECIO 
            //-> SI SOBREPASA SE BORRA-> SE ORDENA POR PRECIO MAS BAJO
            //-> MOSTRAR EL PRIMERO SI EXISTEN MAS DE DOS, DAR OPCION A VER EL SIGUIENTE 
            //-> MOSTRAR SUS CARACTERISTICAS -> 

            MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
                getParametros.Requisitos.Estudio.CostoMaximo,
                getParametros.Requisitos.Estudio.CostoMinimo), "ESTUDIO");

            getParametros.CambiarTipo = TipoComputadora.Gaming;
            MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
                getParametros.Requisitos.Estudio.CostoMaximo,
                getParametros.Requisitos.Estudio.CostoMinimo), "GAMING");

            getParametros.CambiarTipo = TipoComputadora.Oficina;
            MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
                getParametros.Requisitos.Estudio.CostoMaximo,
                getParametros.Requisitos.Estudio.CostoMinimo), "OFICINA");

            getParametros.CambiarTipo = TipoComputadora.TrabajoDiseno;
            MessageBox.Show(String.Format("MAX: {0}\n\rMIN: {1}",
                getParametros.Requisitos.Estudio.CostoMaximo,
                getParametros.Requisitos.Estudio.CostoMinimo), "TRABAJO DISEÑO");

        }

    }
}
