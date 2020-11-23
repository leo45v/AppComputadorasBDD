using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using WpfAppComputadoras.Extra;

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCGabineteView.xaml
    /// </summary>
    public partial class UCGabineteView : UserControl
    {
        private UCProductView mainView;
        private List<ComboColor> newComboColor;
        public UCGabineteView(UCProductView uCProductView)
        {
            InitializeComponent();
            ModoVista(uCProductView.VistaMode);
            mainView = uCProductView;
            txtAltura.Text = uCProductView.gabinete.Altura.ToString();
            txtLargo.Text = uCProductView.gabinete.Largo.ToString();
            txtPeso.Text = uCProductView.gabinete.Peso.ToString();
            if (mainView.gabinete.Colores is null)
            {
                mainView.gabinete.Colores = new List<Colores>();
            }
            newComboColor = new List<ComboColor>();
            foreach (Colores item in uCProductView.mainView.Colores)
            {

                ComboColor comboColor = new ComboColor()
                {
                    IdColor = item.IdColor,
                    Nombre = item.Nombre,
                    ColorRGB = item.ColorRGB
                };
                foreach (Colores gabineteColor in mainView.gabinete.Colores)
                {
                    if (gabineteColor.IdColor == comboColor.IdColor)
                    {
                        comboColor.Check_Status = true;
                    }
                }
                newComboColor.Add(comboColor);
            }
            lxtColores.ItemsSource = newComboColor;

            BindListBOX();
        }
        private void ModoVista(bool activo)
        {
            txtAltura.IsEnabled = !activo;
            txtLargo.IsEnabled = !activo;
            txtPeso.IsEnabled = !activo;
            lxtColores.IsEnabled = !activo;
            lstSe.IsEnabled = !activo;
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            txtAltura.TextChanged += TxtAltura_TextChanged;

            txtLargo.TextChanged += TxtLargo_TextChanged;

            txtPeso.TextChanged += TxtPeso_TextChanged;
        }

        private void TxtPeso_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.gabinete.Peso = decimal.Parse(((TextBox)sender).Text); }
        }

        private void TxtLargo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.gabinete.Largo = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtAltura_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.gabinete.Altura = int.Parse(((TextBox)sender).Text); }
        }
        private void LxtColoresChanged(object sender, TextChangedEventArgs e)
        {
            lxtColores.ItemsSource = newComboColor.Where(x => x.Nombre.StartsWith(lxtColores.Text.Trim()));
        }
        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            BindListBOX();
        }
        private void BindListBOX()
        {
            mainView.gabinete.Colores.Clear();
            lstSe.Items.Clear();
            foreach (ComboColor comboColorX in newComboColor)
            {
                if (comboColorX.Check_Status == true)
                {
                    mainView.gabinete.Colores.Add(new Colores()
                    {
                        IdColor = comboColorX.IdColor,
                        Nombre = comboColorX.Nombre,
                        ColorRGB = comboColorX.ColorRGB
                    });
                    lstSe.Items.Add(comboColorX.Nombre);
                }
            }
        }
    }
}
