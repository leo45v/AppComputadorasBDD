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
            ConfigurationBuildComputer getParametros = new ConfigurationBuildComputer(TipoComputadora.Estudio);
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
