using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectBrl;
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
            txtAltura.PreviewTextInput += TxtAltura_PreviewTextInput;

            txtLargo.TextChanged += TxtLargo_TextChanged;
            txtLargo.PreviewTextInput += TxtAltura_PreviewTextInput;

            txtPeso.TextChanged += TxtPeso_TextChanged;
            txtPeso.PreviewTextInput += TxtPeso_PreviewTextInput;
        }

        private void TxtAltura_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10))
            { e.Handled = true; }
        }

        private void TxtPeso_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                { approvedDecimalPoint = true; }
            }
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) && ((TextBox)sender).Text.Length < 10 || approvedDecimalPoint))
            { e.Handled = true; }
        }

        private void TxtPeso_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (((TextBox)sender).Text.Length > 0 && ((TextBox)sender).Text.Substring(0, 1) == ".")
            { ((TextBox)sender).Text = ((TextBox)sender).Text[1..]; }
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.gabinete.Peso = decimal.Parse(((TextBox)sender).Text); }
        }

        private void TxtLargo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if (!String.IsNullOrWhiteSpace(((TextBox)sender).Text))
            { mainView.gabinete.Largo = int.Parse(((TextBox)sender).Text); }
        }

        private void TxtAltura_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
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

        private void Btn_RefreshAddColor_Click(object sender, RoutedEventArgs e)
        {
            this.mainView.mainView.Colores = ConfiguracionesBrl.Colores.GetAll();
            newComboColor = new List<ComboColor>();
            foreach (Colores item in this.mainView.mainView.Colores)
            {
                ComboColor comboColor = new ComboColor()
                {
                    IdColor = item.IdColor,
                    Nombre = item.Nombre,
                    ColorRGB = item.ColorRGB
                };
                foreach (Colores gabineteColor in this.mainView.gabinete.Colores)
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
    }
}
