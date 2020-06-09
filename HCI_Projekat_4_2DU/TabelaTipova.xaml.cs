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
    /// Interaction logic for TabelaTipova.xaml
    /// </summary>
    public partial class TabelaTipova : Window
    {
        public ObservableCollection<Tip> listaTipovaPrikaz { get; set; }
        public ListCollectionView collectionView { get; set; }

        public TabelaTipova()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listaTipovaPrikaz = MainWindow.ListaTipova;
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            collectionView = new ListCollectionView(listaTipovaPrikaz);
            Data.ItemsSource = collectionView;

            ShowDialog();
        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            collectionView.Filter = (et) =>
            {
                Tip eti = et as Tip;
                if (eti.Id.Contains(ID.Text))
                {
                    return true;
                }

                return false;
            };
        }

        private void IME_TextChanged(object sender, TextChangedEventArgs e)
        {

            collectionView.Filter = (et) =>
            {
                Tip eti = et as Tip;
                if (eti.Ime.Contains(IME.Text))
                {
                    return true;
                }

                return false;
            };
        }
    }
}
