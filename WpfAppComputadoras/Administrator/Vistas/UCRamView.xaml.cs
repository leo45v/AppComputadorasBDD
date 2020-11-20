﻿using System;
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

namespace WpfAppComputadoras.Administrator.Vistas
{
    /// <summary>
    /// Interaction logic for UCRamView.xaml
    /// </summary>
    public partial class UCRamView : UserControl
    {
        private UCProductView mainView;
        public UCRamView(UCProductView uCProductView)
        {
            InitializeComponent();
            mainView = uCProductView;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }
    }
}