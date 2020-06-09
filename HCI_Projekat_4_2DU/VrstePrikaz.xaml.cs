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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for VrstePrikaz.xaml
    /// </summary>
    public partial class VrstePrikaz : Window
    {
        public ObservableCollection<Vrsta> listaZaPrikaz { get; set; }
        public VrstePrikaz()
        {
            listaZaPrikaz = MainWindow.ListaVrsta;
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
        }

        public VrstePrikaz(int index)
        {
            listaZaPrikaz = MainWindow.ListaVrsta;
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            listBox.SelectedIndex = index;
            this.Show();
        }
    }
}
