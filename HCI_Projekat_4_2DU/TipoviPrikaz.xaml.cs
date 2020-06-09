﻿using System;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for TipoviPrikaz.xaml
    /// </summary>
    public partial class TipoviPrikaz : Window
    {
        public ObservableCollection<Tip> listaZaPrikaz { get; set; }
        public TipoviPrikaz()

        {
            listaZaPrikaz = MainWindow.ListaTipova;
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
        }
    }
}
