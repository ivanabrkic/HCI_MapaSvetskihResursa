using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for EtiketePrikaz.xaml
    /// </summary>
  
    public partial class EtiketePrikaz : Window
    {
        public ObservableCollection<Etiketa> listaZaPrikaz { get; set; }

        public EtiketePrikaz()
        {
            listaZaPrikaz = MainWindow.ListaEtiketa;
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
        }
    }
}
