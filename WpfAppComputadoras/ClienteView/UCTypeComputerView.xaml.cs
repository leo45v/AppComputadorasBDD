using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
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
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl.ComputadoraBuild;

namespace WpfAppComputadoras.ClienteView
{
    /// <summary>
    /// Interaction logic for UCTypeComputerView.xaml
    /// </summary>
    public partial class UCTypeComputerView : UserControl
    {

        #region PROPIEDADES
        public decimal presupuesto;
        public TipoComputadora tipoComputadora;
        public object parametros;
        public ViewMain mainView;
        private readonly ConfigurationBuildComputer requisitosComputadora;
        #endregion


        public UCTypeComputerView(ViewMain viewMain)
        {
            InitializeComponent();
            requisitosComputadora = new ConfigurationBuildComputer();
            this.mainView = viewMain;
        }


        #region METODOS

       
        private void Btn_Siguiente(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtPresupuesto.Text))
            {
                MessageBox.Show("Ingrese un Presupuesto");
                return;
            }

            tipoComputadora = ObtenerTipoComputadora;

            if (tipoComputadora == TipoComputadora.Ninguno)
            {
                MessageBox.Show("Seleccione un Tipo de Computadora");
                return;
            }

            requisitosComputadora.CambiarTipoComputadora = tipoComputadora;
            if (decimal.TryParse(txtPresupuesto.Text, out presupuesto))
            {
                ComputadoraBuildBrl.Presupuesto = presupuesto;
                bdWaiting.IsOpen = true;
                Thread nuevoHiloProcesos = new Thread(BuscarComputadora);
                nuevoHiloProcesos.Start();
            }
            else
            {
                MessageBox.Show("Ingrese un Presupuesto correcto");
            }
        }
        private void BuscarComputadora()
        {
            Computadora computadora = ComputadoraBuildBrl.ObtenerComputadoraRecomendada(requisitosComputadora.Requisitos.ComputadoraX);
            Application.Current.Dispatcher.Invoke(new Action(CloseWaiting));
            Application.Current.Dispatcher.Invoke(new Action<Computadora>(mainView.UIReservaComputer), computadora);
            computadora = null;
            GC.Collect();
        }
        private void CloseWaiting()
        {
            bdWaiting.IsOpen = false;
        }
        private TipoComputadora ObtenerTipoComputadora
        {
            get
            {
                if (btnGaming.IsChecked.Value)
                {
                    return TipoComputadora.Gaming;
                }
                else if (btnOficina.IsChecked.Value)
                {
                    return TipoComputadora.Oficina;
                }
                else if (btnTrabajo.IsChecked.Value)
                {
                    return TipoComputadora.TrabajoDiseno;
                }
                else if (BtnEstudios.IsChecked.Value)
                {
                    return TipoComputadora.Estudio;
                }
                return TipoComputadora.Ninguno;
            }
        }
        #endregion

        private void Click_Estudio(object sender, MouseButtonEventArgs e)
        {
            BtnEstudios.IsChecked = true;
        }

        private void Click_TrabajoDiseno(object sender, MouseButtonEventArgs e)
        {
            btnTrabajo.IsChecked = true;
        }

        private void Click_Gaming(object sender, MouseButtonEventArgs e)
        {
            btnGaming.IsChecked = true;
        }

        private void Click_Oficina(object sender, MouseButtonEventArgs e)
        {
            btnOficina.IsChecked = true;
        }
    }
}
