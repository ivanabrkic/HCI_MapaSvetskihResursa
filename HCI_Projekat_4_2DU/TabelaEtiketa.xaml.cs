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
    /// Interaction logic for TabelaEtiketa.xaml
    /// </summary>
    /// 
    public partial class TabelaEtiketa : Window
    {
        public ObservableCollection<Etiketa> listaEtiketaPrikaz { get; set; }
        public ListCollectionView collectionView { get; set; }

        public TabelaEtiketa()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listaEtiketaPrikaz = MainWindow.ListaEtiketa;
            this.DataContext = this;
            InitializeComponent();

            collectionView = new ListCollectionView(listaEtiketaPrikaz);
            Data.ItemsSource = collectionView;

            ShowDialog();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            collectionView.Filter = (et) =>
            {
                Etiketa eti = et as Etiketa;
                if (eti.Id.Contains(textBox.Text))
                {
                    return true;
                }

                return false;
            };
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            collectionView.Filter = (et) =>
            {
                Etiketa eti = et as Etiketa;
                if ((Color)BOJA.SelectedColor == eti.Boja.Color) { 
                    return true;
                }
                return false;
            };
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

            collectionView.Filter = (et) =>
            {
 
                return true;
            };
        }
    }
}
