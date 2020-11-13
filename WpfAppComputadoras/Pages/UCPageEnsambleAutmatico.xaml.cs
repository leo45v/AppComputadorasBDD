using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfAppComputadoras.Components;

namespace WpfAppComputadoras.Pages
{
    /// <summary>
    /// Interaction logic for UCPageEnsambleAutmatico.xaml
    /// </summary>
    public partial class UCPageEnsambleAutmatico : UserControl
    {
        private Button btn_Siguiente;
        private bool automatico = true;
        private RadioButton chk_Automatico;
        private RadioButton chk_Manual;
        private double presupuesto = 0.0;
        private StackPanel stackPanel;
        #region AUTMATICO_PROPERTIES

        private string tipo_PC = "Oficina";
        private string marca_Procesador = "AMD";
        #endregion


        public UCPageEnsambleAutmatico()
        {
            InitializeComponent();
            //RenderUI_Step1();
            stackPanel = RenderUI_Step1();
            this.container.Children.Add(stackPanel);
        }
        public StackPanel RenderUI_Step1()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" }; ;
            TextBlock txtNewLog = new TextBlock
            {
                Text = "INGRESE SU PRESUPUESTO",
                FontSize = 25,
                Margin = new Thickness(0, 10, 0, 10)
            };
            TextBox txt_Presupuesto = new TextBox
            {
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 10),
                Height = 50,

            };
            Grid groupRadio = new Grid()
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            groupRadio.ColumnDefinitions.Add(new ColumnDefinition());
            groupRadio.ColumnDefinitions.Add(new ColumnDefinition());
            chk_Automatico = new RadioButton()
            {
                Content = "Automatico",
                FontSize = 20,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Padding = new Thickness(5, -10, 0, 0),
                IsChecked = true
            };
            chk_Manual = new RadioButton()
            {
                Content = "Manual",
                FontSize = 20,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Padding = new Thickness(5, -10, 0, 0),
                IsChecked = false
            };
            Grid.SetColumn(chk_Automatico, 0);
            Grid.SetColumn(chk_Manual, 1);
            groupRadio.Children.Add(chk_Automatico);
            groupRadio.Children.Add(chk_Manual);
            txt_Presupuesto.TextChanged += Txt_Presupuesto_Changed;
            txt_Presupuesto.PreviewTextInput += Txt_Presupuesto_PreviewText;
            btn_Siguiente = new Button()
            {
                Name = "btn_Siguiente",
                Content = "Siguiente",
                FontSize = 20,
                Height = 50,
                Margin = new Thickness(0, 30, 0, 10),
                IsEnabled = false
            };
            btn_Siguiente.Click += Btn_Siguiente_Click;

            stackPanel.Children.Add(txtNewLog);
            stackPanel.Children.Add(txt_Presupuesto);
            stackPanel.Children.Add(groupRadio);
            stackPanel.Children.Add(btn_Siguiente);

            return stackPanel;
        }

        private StackPanel RenderUI_Step2_MANUAL()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtMarca = new TextBlock() { Text = "Marca: ", FontSize = 18 };
            Grid.SetColumn(txtMarca, 0);
            Grid.SetRow(txtMarca, 0);
            ComboBox cbMarca = new ComboBox();
            cbMarca.ItemsSource = ProcesadorBrl.Get_ListMarcas();
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
            //cbMarca.SelectionChanged += CBMarca_SelectionChanged;

            var dp = DependencyPropertyDescriptor.FromProperty(
             ComboBox.TextProperty,
             typeof(ComboBox));

            Grid.SetColumn(cbMarca, 0);
            Grid.SetRow(cbMarca, 1);


            TextBlock txtNombre = new TextBlock() { Text = "Nombre: ", FontSize = 18 };
            Grid.SetColumn(txtNombre, 4);
            Grid.SetRow(txtNombre, 0);
            TextBox txt_Nombre = new TextBox();
            Grid.SetColumn(txt_Nombre, 4);
            Grid.SetRow(txt_Nombre, 1);

            UCMainStore uCMainStore = new UCMainStore();
            uCMainStore.baseContainer.RowDefinitions[0].Height = new GridLength(1);
            uCMainStore.header.Visibility = Visibility.Hidden;
            uCMainStore.header.Height = 0;
            uCMainStore.Height = 400;
            uCMainStore.Width = 900;
            foreach (UCProductDescription item in uCMainStore.ucPX)
            {
                //<RowDefinition Height="100"/>
                //   <RowDefinition Height="50"/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition Height="28"/>
                item.txtPrice.Visibility = Visibility.Hidden;
                item.txtPriceFix.Visibility = Visibility.Hidden;
                item.gridTam.RowDefinitions[0].Height = GridLength.Auto;
                item.gridTam.RowDefinitions[1].Height = new GridLength((item.gridTam.RowDefinitions[1].Height.Value - 10));
                item.gridTam.RowDefinitions[2].Height = new GridLength(2);
                item.gridTam.RowDefinitions[3].Height = new GridLength(2);
                item.gridTam.RowDefinitions[4].Height = new GridLength(2);
                //item.gridTam.RowDefinitions[5].Height = new GridLength(50, GridUnitType.Pixel);
                item.imgProduct.Height = 80;
                item.btnAgregar.FontSize = 15;
                item.btnAgregar.Content = "Seleccionar";
                item.stockContainer.Visibility = Visibility.Hidden;
                item.Visibility = Visibility.Visible;
            }
            uCMainStore.TypeProduct = "Procesador";
            Grid.SetColumn(uCMainStore, 0);
            Grid.SetColumnSpan(uCMainStore, filter.ColumnDefinitions.Count);
            Grid.SetRow(uCMainStore, 2);

            dp.AddValueChanged(cbMarca, (sender, args) =>
            {
                uCMainStore.IdMarcaSelectFromCb = ((Marca)cbMarca.SelectedItem).IdMarca;
                uCMainStore.IdMarcaSelect = ((Marca)cbMarca.SelectedItem).NombreMarca;
                //MessageBox.Show("IM CHANGED " + ((Marca)cbMarca.SelectedItem).NombreMarca);


            });

            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 10, 0, 0), Width = 200, Height = 80 };
            //btn_NextStep.IsEnabled = false;
            btn_NextStep.Click += ((sender, args) =>
            {
                container.Children.Remove(stackPanel);
                stackPanel = RenderUI_Step3_MANUAL();
                container.Children.Add(stackPanel);
            });


            filter.Children.Add(cbMarca);
            filter.Children.Add(txtMarca);
            filter.Children.Add(txtNombre);
            filter.Children.Add(txt_Nombre);
            filter.Children.Add(uCMainStore);


            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);

            return stackPanel;
        }
        private StackPanel RenderUI_Step3_MANUAL()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(430) });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtMarca = new TextBlock() { Text = "Marca: ", FontSize = 18 };
            Grid.SetColumn(txtMarca, 0);
            Grid.SetRow(txtMarca, 0);
            ComboBox cbMarca = new ComboBox();
            cbMarca.ItemsSource = RamBrl.Get_ListMarcasRam();
            cbMarca.DisplayMemberPath = "NombreMarca";
            cbMarca.SelectedValuePath = "IdMarca";
            

            Grid.SetColumn(cbMarca, 0);
            Grid.SetRow(cbMarca, 1);

            TextBlock txtCapacidad = new TextBlock() { Text = "Capacidad: ", FontSize = 18 };
            Grid.SetColumn(txtCapacidad, 2);
            Grid.SetRow(txtCapacidad, 0);
            ComboBox cbCpacidad = new ComboBox();
            Grid.SetColumn(cbCpacidad, 2);
            Grid.SetRow(cbCpacidad, 1);

            TextBlock txtFrecuencia = new TextBlock() { Text = "Frecuencia: ", FontSize = 18 };
            Grid.SetColumn(txtFrecuencia, 4);
            Grid.SetRow(txtFrecuencia, 0);
            ComboBox cbFrecuencia = new ComboBox();
            Grid.SetColumn(cbFrecuencia, 4);
            Grid.SetRow(cbFrecuencia, 1);

            TextBlock txtLatencia = new TextBlock() { Text = "Latencia: ", FontSize = 18 };
            Grid.SetColumn(txtLatencia, 6);
            Grid.SetRow(txtLatencia, 0);
            ComboBox cbLatencia = new ComboBox();
            Grid.SetColumn(cbLatencia, 6);
            Grid.SetRow(cbLatencia, 1);

            TextBlock txtNombre = new TextBlock() { Text = "Nombre: ", FontSize = 18 };
            Grid.SetColumn(txtNombre, 8);
            Grid.SetRow(txtNombre, 0);
            TextBox txt_Nombre = new TextBox();
            Grid.SetColumn(txt_Nombre, 8);
            Grid.SetRow(txt_Nombre, 1);

            UCMainStore uCMainStore = new UCMainStore();
            uCMainStore.baseContainer.RowDefinitions[0].Height = new GridLength(1);
            uCMainStore.header.Visibility = Visibility.Hidden;
            uCMainStore.header.Height = 0;
            uCMainStore.Height = 400;
            uCMainStore.Width = 900;
            foreach (UCProductDescription item in uCMainStore.ucPX)
            {

                item.txtPrice.Visibility = Visibility.Hidden;
                item.txtPriceFix.Visibility = Visibility.Hidden;
                item.gridTam.RowDefinitions[0].Height = GridLength.Auto;
                item.gridTam.RowDefinitions[1].Height = new GridLength((item.gridTam.RowDefinitions[1].Height.Value - 10));
                item.gridTam.RowDefinitions[2].Height = new GridLength(2);
                item.gridTam.RowDefinitions[3].Height = new GridLength(2);
                item.gridTam.RowDefinitions[4].Height = new GridLength(2);
                item.imgProduct.Height = 80;
                item.btnAgregar.FontSize = 15;
                item.btnAgregar.Content = "Seleccionar";
                item.stockContainer.Visibility = Visibility.Hidden;
                item.Visibility = Visibility.Visible;
            }
            Grid.SetColumn(uCMainStore, 0);
            Grid.SetColumnSpan(uCMainStore, filter.ColumnDefinitions.Count);
            Grid.SetRow(uCMainStore, 2);

            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 10, 0, 0), Width = 200, Height = 80 };



            uCMainStore.TypeProduct = "Ram";
            cbMarca.SelectionChanged += ((sender, args) =>
            {
                uCMainStore.IdMarcaSelectFromCb = ((Marca)cbMarca.SelectedItem).IdMarca;
                uCMainStore.IdMarcaSelect = ((Marca)cbMarca.SelectedItem).NombreMarca;
            });
            btn_NextStep.Click += ((sender, args) =>
            {
                container.Children.Remove(stackPanel);
                stackPanel = RenderUI_Step4_MANUAL();
                container.Children.Add(stackPanel);
            });


            filter.Children.Add(cbMarca);
            filter.Children.Add(txtMarca);
            filter.Children.Add(txtCapacidad);
            filter.Children.Add(cbCpacidad);
            filter.Children.Add(txtFrecuencia);
            filter.Children.Add(cbFrecuencia);
            filter.Children.Add(txtLatencia);
            filter.Children.Add(cbLatencia);
            filter.Children.Add(txtNombre);
            filter.Children.Add(txt_Nombre);

            filter.Children.Add(uCMainStore);


            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);
            return stackPanel;
        }

        private StackPanel RenderUI_Step4_MANUAL()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(350) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtMarca = new TextBlock() { Text = "Tipo: ", FontSize = 18 };
            Grid.SetColumn(txtMarca, 0);
            Grid.SetRow(txtMarca, 0);
            Grid gridTipo = new Grid();
            gridTipo.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            gridTipo.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            gridTipo.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            RadioButton radSSD = new RadioButton() { Content = "SSD", Padding = new Thickness(10, -10, 0, 0), FontSize = 22, Margin = new Thickness(0, 20, 0, 0) };
            RadioButton radHDD = new RadioButton() { Content = "HDD", Padding = new Thickness(10, -10, 0, 0), FontSize = 22, Margin = new Thickness(0, 20, 0, 0) };
            RadioButton radBoth = new RadioButton() { Content = "SSD y HDD", Padding = new Thickness(10, -10, 0, 0), FontSize = 22, Margin = new Thickness(0, 20, 0, 0), IsChecked = true };
            Grid.SetColumn(radBoth, 0);
            Grid.SetColumn(radSSD, 1);
            Grid.SetColumn(radHDD, 2);
            gridTipo.Children.Add(radBoth);
            gridTipo.Children.Add(radSSD);
            gridTipo.Children.Add(radHDD);


            Grid.SetColumn(gridTipo, 0);
            Grid.SetRow(gridTipo, 1);

            TextBlock txtCapacidad = new TextBlock() { Text = "Capacidad: ", FontSize = 18 };
            Grid.SetColumn(txtCapacidad, 2);
            Grid.SetRow(txtCapacidad, 0);
            ComboBox cbCpacidad = new ComboBox();
            cbCpacidad.Items.Add("120 GB");
            cbCpacidad.Items.Add("256 GB");
            cbCpacidad.Items.Add("512 GB");
            cbCpacidad.Items.Add("512 GB");
            cbCpacidad.Items.Add("1 TB");
            cbCpacidad.Items.Add("2 TB");
            cbCpacidad.Items.Add("3 TB");
            cbCpacidad.Items.Add("4 TB");
            Grid.SetColumn(cbCpacidad, 2);
            Grid.SetRow(cbCpacidad, 1);

            TextBlock txtNombre = new TextBlock() { Text = "Nombre: ", FontSize = 18 };
            Grid.SetColumn(txtNombre, 4);
            Grid.SetRow(txtNombre, 0);
            TextBox txt_Nombre = new TextBox();
            Grid.SetColumn(txt_Nombre, 4);
            Grid.SetRow(txt_Nombre, 1);

            UCMainStore uCMainStore = new UCMainStore();
            uCMainStore.baseContainer.RowDefinitions[0].Height = new GridLength(1);
            uCMainStore.header.Visibility = Visibility.Hidden;
            uCMainStore.header.Height = 0;
            uCMainStore.Height = 400;
            uCMainStore.Width = 900;
            foreach (UCProductDescription item in uCMainStore.ucPX)
            {

                item.txtPrice.Visibility = Visibility.Hidden;
                item.txtPriceFix.Visibility = Visibility.Hidden;
                item.gridTam.RowDefinitions[0].Height = GridLength.Auto;
                item.gridTam.RowDefinitions[1].Height = new GridLength((item.gridTam.RowDefinitions[1].Height.Value - 10));
                item.gridTam.RowDefinitions[2].Height = new GridLength(2);
                item.gridTam.RowDefinitions[3].Height = new GridLength(2);
                item.gridTam.RowDefinitions[4].Height = new GridLength(2);
                item.imgProduct.Height = 80;
                item.btnAgregar.FontSize = 15;
                item.btnAgregar.Content = "Seleccionar";
                item.stockContainer.Visibility = Visibility.Hidden;
                item.Visibility = Visibility.Visible;
            }
            Grid.SetColumn(uCMainStore, 0);
            Grid.SetColumnSpan(uCMainStore, filter.ColumnDefinitions.Count);
            Grid.SetRow(uCMainStore, 2);

            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 10, 0, 0), Width = 200, Height = 80 };



            filter.Children.Add(gridTipo);
            filter.Children.Add(txtMarca);
            filter.Children.Add(txtCapacidad);
            filter.Children.Add(cbCpacidad);
            filter.Children.Add(txtNombre);
            filter.Children.Add(txt_Nombre);

            filter.Children.Add(uCMainStore);

            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);


            return stackPanel;
        }
        private StackPanel RenderUI_Step5_MANUAL()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtMarca = new TextBlock() { Text = "Marca: ", FontSize = 18 };
            Grid.SetColumn(txtMarca, 0);
            Grid.SetRow(txtMarca, 0);
            ComboBox cbMarca = new ComboBox();
            Grid.SetColumn(cbMarca, 0);
            Grid.SetRow(cbMarca, 1);


            TextBlock txtNombre = new TextBlock() { Text = "Nombre: ", FontSize = 18 };
            Grid.SetColumn(txtNombre, 4);
            Grid.SetRow(txtNombre, 0);
            TextBox txt_Nombre = new TextBox();
            Grid.SetColumn(txt_Nombre, 4);
            Grid.SetRow(txt_Nombre, 1);

            UCMainStore uCMainStore = new UCMainStore();
            uCMainStore.baseContainer.RowDefinitions[0].Height = new GridLength(1);
            uCMainStore.header.Visibility = Visibility.Hidden;
            uCMainStore.header.Height = 0;
            uCMainStore.Height = 400;
            uCMainStore.Width = 900;
            foreach (UCProductDescription item in uCMainStore.ucPX)
            {
                //<RowDefinition Height="100"/>
                //   <RowDefinition Height="50"/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition Height="28"/>
                item.txtPrice.Visibility = Visibility.Hidden;
                item.txtPriceFix.Visibility = Visibility.Hidden;
                item.gridTam.RowDefinitions[0].Height = GridLength.Auto;
                item.gridTam.RowDefinitions[1].Height = new GridLength((item.gridTam.RowDefinitions[1].Height.Value - 10));
                item.gridTam.RowDefinitions[2].Height = new GridLength(2);
                item.gridTam.RowDefinitions[3].Height = new GridLength(2);
                item.gridTam.RowDefinitions[4].Height = new GridLength(2);
                //item.gridTam.RowDefinitions[5].Height = new GridLength(50, GridUnitType.Pixel);
                item.imgProduct.Height = 80;
                item.btnAgregar.FontSize = 15;
                item.btnAgregar.Content = "Seleccionar";
                item.stockContainer.Visibility = Visibility.Hidden;
                item.Visibility = Visibility.Visible;
            }
            Grid.SetColumn(uCMainStore, 0);
            Grid.SetColumnSpan(uCMainStore, filter.ColumnDefinitions.Count);
            Grid.SetRow(uCMainStore, 2);


            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 10, 0, 0), Width = 200, Height = 80 };

            filter.Children.Add(cbMarca);
            filter.Children.Add(txtMarca);
            filter.Children.Add(txtNombre);
            filter.Children.Add(txt_Nombre);

            filter.Children.Add(uCMainStore);
            //cbMarca.ItemsSource = RamBrl.Get_ListMarcasRam();
            //cbMarca.DisplayMemberPath = "NombreMarca";
            //cbMarca.SelectedValuePath = "IdMarca";
            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);

            return stackPanel;
        }
        private StackPanel RenderUI_Step6_MANUAL()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtMarca = new TextBlock() { Text = "Marca: ", FontSize = 18 };
            Grid.SetColumn(txtMarca, 0);
            Grid.SetRow(txtMarca, 0);
            ComboBox cbMarca = new ComboBox();
            Grid.SetColumn(cbMarca, 0);
            Grid.SetRow(cbMarca, 1);


            TextBlock txtNombre = new TextBlock() { Text = "Nombre: ", FontSize = 18 };
            Grid.SetColumn(txtNombre, 4);
            Grid.SetRow(txtNombre, 0);
            TextBox txt_Nombre = new TextBox();
            Grid.SetColumn(txt_Nombre, 4);
            Grid.SetRow(txt_Nombre, 1);

            UCMainStore uCMainStore = new UCMainStore();
            uCMainStore.baseContainer.RowDefinitions[0].Height = new GridLength(1);
            uCMainStore.header.Visibility = Visibility.Hidden;
            uCMainStore.header.Height = 0;
            uCMainStore.Height = 400;
            uCMainStore.Width = 900;
            foreach (UCProductDescription item in uCMainStore.ucPX)
            {
                //<RowDefinition Height="100"/>
                //   <RowDefinition Height="50"/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition/>
                //   <RowDefinition Height="28"/>
                item.txtPrice.Visibility = Visibility.Hidden;
                item.txtPriceFix.Visibility = Visibility.Hidden;
                item.gridTam.RowDefinitions[0].Height = GridLength.Auto;
                item.gridTam.RowDefinitions[1].Height = new GridLength((item.gridTam.RowDefinitions[1].Height.Value - 10));
                item.gridTam.RowDefinitions[2].Height = new GridLength(2);
                item.gridTam.RowDefinitions[3].Height = new GridLength(2);
                item.gridTam.RowDefinitions[4].Height = new GridLength(2);
                //item.gridTam.RowDefinitions[5].Height = new GridLength(50, GridUnitType.Pixel);
                item.imgProduct.Height = 80;
                item.btnAgregar.FontSize = 15;
                item.btnAgregar.Content = "Seleccionar";
                item.stockContainer.Visibility = Visibility.Hidden;
                item.Visibility = Visibility.Visible;
            }
            Grid.SetColumn(uCMainStore, 0);
            Grid.SetColumnSpan(uCMainStore, filter.ColumnDefinitions.Count);
            Grid.SetRow(uCMainStore, 2);


            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 10, 0, 0), Width = 200, Height = 80 };

            filter.Children.Add(cbMarca);
            filter.Children.Add(txtMarca);
            filter.Children.Add(txtNombre);
            filter.Children.Add(txt_Nombre);

            filter.Children.Add(uCMainStore);
            //cbMarca.ItemsSource = RamBrl.Get_ListMarcasRam();
            //cbMarca.DisplayMemberPath = "NombreMarca";
            //cbMarca.SelectedValuePath = "IdMarca";
            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);

            return stackPanel;
        }
        private StackPanel RenderUI_Step2_AUTOMATICO()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtTitle = new TextBlock() { Text = "Seleccione un Tipo: " };
            Grid.SetColumn(txtTitle, 0);
            Grid.SetColumnSpan(txtTitle, filter.ColumnDefinitions.Count);
            Grid.SetRow(txtTitle, 0);
            RadioButton radOficina = new RadioButton() { Content = "Oficina", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0), IsChecked = true };
            Grid.SetColumn(radOficina, 0);
            Grid.SetRow(radOficina, 1);
            RadioButton radJuego = new RadioButton() { Content = "Gamer", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0) };
            Grid.SetColumn(radJuego, 2);
            Grid.SetRow(radJuego, 1);
            RadioButton radTrabajo = new RadioButton() { Content = "Work & Disign", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0) };
            Grid.SetColumn(radTrabajo, 4);
            Grid.SetRow(radTrabajo, 1);

            radOficina.Checked += Rad_Oficina_Checked;
            radJuego.Checked += Rad_Oficina_Checked;
            radTrabajo.Checked += Rad_Oficina_Checked;

            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 100, 0, 0), Width = 200, Height = 80 };
            btn_NextStep.Click += Btn_Siguiente_Step2_Automatico;

            filter.Children.Add(txtTitle);
            filter.Children.Add(radOficina);
            filter.Children.Add(radJuego);
            filter.Children.Add(radTrabajo);

            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);

            return stackPanel;
        }

        private void Btn_Siguiente_Step2_Automatico(object sender, RoutedEventArgs e)
        {
            container.Children.Remove(stackPanel);
            stackPanel = RenderUI_Step3_AUTOMATICO();
            container.Children.Add(stackPanel);
        }

        private void Rad_Oficina_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            tipo_PC = radioButton.Content.ToString();
        }

        private StackPanel RenderUI_Step3_AUTOMATICO()
        {
            StackPanel stackPanel = new StackPanel() { Name = "stackX" };
            Grid filter = new Grid();
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
            filter.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            filter.ColumnDefinitions.Add(new ColumnDefinition());
            filter.RowDefinitions.Add(new RowDefinition());
            filter.RowDefinitions.Add(new RowDefinition());

            TextBlock txtTitle = new TextBlock() { Text = "Seleccione una Marca Para Procesador: " };
            Grid.SetColumn(txtTitle, 0);
            Grid.SetColumnSpan(txtTitle, filter.ColumnDefinitions.Count);
            Grid.SetRow(txtTitle, 0);
            RadioButton radOficina = new RadioButton() { Content = "AMD", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0), IsChecked = true };
            Grid.SetColumn(radOficina, 0);
            Grid.SetRow(radOficina, 1);
            RadioButton radJuego = new RadioButton() { Content = "INTEL", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0) };
            Grid.SetColumn(radJuego, 2);
            Grid.SetRow(radJuego, 1);
            RadioButton radTrabajo = new RadioButton() { Content = "CUALQUIERA", Padding = new Thickness(10, -20, 0, 0), FontSize = 35, Margin = new Thickness(0, 100, 0, 0) };
            Grid.SetColumn(radTrabajo, 4);
            Grid.SetRow(radTrabajo, 1);


            radOficina.Checked += Rad_Procesador_Checked;
            radJuego.Checked += Rad_Procesador_Checked;
            radTrabajo.Checked += Rad_Procesador_Checked;


            Button btn_NextStep = new Button() { Content = "Siguiente", Margin = new Thickness(0, 100, 0, 0), Width = 200, Height = 80 };
            btn_NextStep.Click += Btn_Siguiente_Step3_Automatico;


            filter.Children.Add(txtTitle);
            filter.Children.Add(radOficina);
            filter.Children.Add(radJuego);
            filter.Children.Add(radTrabajo);

            stackPanel.Children.Add(filter);
            stackPanel.Children.Add(btn_NextStep);

            return stackPanel;
        }

        private void Btn_Siguiente_Step3_Automatico(object sender, RoutedEventArgs e)
        {
            //container.Children.Remove(stackPanel);
            //stackPanel = RenderUI_Step4_AUTOMATICO();
            //container.Children.Add(stackPanel);
        }

        private void Rad_Procesador_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            marca_Procesador = radioButton.Content.ToString();
        }

        private void Txt_Presupuesto_Changed(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
            if ((((TextBox)sender).Text.Length > 0 && !((TextBox)sender).Text.Contains(".")) || (((TextBox)sender).Text.Contains(".") && ((TextBox)sender).Text.Length > 1))
            {
                btn_Siguiente.IsEnabled = true;
                //IEnumerator x = container.Children.GetEnumerator();
                //while (x.MoveNext())
                //{
                //    if (x.Current.GetType() == typeof(Button) && ((Button)x.Current).Name == "btn_Siguiente")
                //    {
                //        ((Button)x.Current).IsEnabled = true;
                //    }
                //}
            }
            else
            {
                btn_Siguiente.IsEnabled = false;
            }
            double.TryParse(((TextBox)sender).Text, out presupuesto);
        }
        private void Btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {

            if (presupuesto > 1.0)
            {
                automatico = chk_Automatico.IsChecked.Value;
                container.Children.Remove(stackPanel);
                if (automatico)
                {
                    stackPanel = RenderUI_Step2_AUTOMATICO();
                }
                else
                {
                    stackPanel = RenderUI_Step2_MANUAL();
                }
                container.Children.Add(stackPanel);
                //MessageBox.Show("ACEPTADO, PRESUPUIESTO DE: " + presupuesto.ToString() + " ESCOGIO EL MODO " + (automatico ? "AUTOMATICO" : "MANUAL"));
            }
        }
        private void Txt_Presupuesto_PreviewText(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                { approvedDecimalPoint = true; }
            }
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
            { e.Handled = true; }
        }

    }
}
