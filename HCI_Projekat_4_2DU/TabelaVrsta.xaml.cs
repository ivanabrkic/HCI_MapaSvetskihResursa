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
    /// Interaction logic for TabelaVrsta.xaml
    /// </summary>
    public partial class TabelaVrsta : Window
       
    {
        public ObservableCollection<Vrsta> listaVrstaPrikaz { get; set; }
        public ObservableCollection<Tip> listaTipovaPrikaz { get; set; }
        public ListCollectionView collectionView { get; set; }

        public TabelaVrsta()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listaVrstaPrikaz = MainWindow.ListaVrsta;
            listaTipovaPrikaz = MainWindow.ListaTipova;
            this.DataContext = this;
            InitializeComponent();

            collectionView = new ListCollectionView(listaVrstaPrikaz);
            Data.ItemsSource = collectionView;

            ShowDialog();
        }

        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            collectionView.Filter = (et) =>
            {
                Vrsta eti = et as Vrsta;
                if (eti.Id.Contains(IdTextBox.Text))
                {
                    return true;
                }

                return false;
            };
        }

        private void ImeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            collectionView.Filter = (et) =>
            {
                Vrsta eti = et as Vrsta;
                if (eti.Ime.Contains(ImeTextBox.Text))
                {
                    return true;
                }

                return false;
            };
        }

        private void OpasnaDa_Checked(object sender, RoutedEventArgs e)
        {
            if (OpasnaNe.IsChecked.Value)
            {
                OpasnaNe.IsChecked = false;
            }
        }

        private void OpasnaNe_Checked(object sender, RoutedEventArgs e)
        {
            if (OpasnaDa.IsChecked.Value)
            {
                OpasnaDa.IsChecked = false;
            }
        }

        private void RegionDa_Checked(object sender, RoutedEventArgs e)
        {
            if (RegionNe.IsChecked.Value)
            {
                RegionNe.IsChecked = false;
            }
        }

        private void RegionNe_Checked(object sender, RoutedEventArgs e)
        {
            if (RegionDa.IsChecked.Value)
            {
                RegionDa.IsChecked = false;
            }
        }

        private void CrvenaDa_Checked(object sender, RoutedEventArgs e)
        {
            if (CrvenaNe.IsChecked.Value)
            {
                CrvenaNe.IsChecked = false;
            }
        }

        private void CrvenaNe_Checked(object sender, RoutedEventArgs e)
        {
            if (CrvenaDa.IsChecked.Value)
            {
                CrvenaDa.IsChecked = false;
            }
        }

        private void pretrazi_Click(object sender, RoutedEventArgs e)
        {

            collectionView.Filter = (et) =>
            {
                Vrsta eti = et as Vrsta;

                if (Tipovi.SelectedIndex != -1)
                {
                    if (!eti.Tip.Id.Equals(((Tip)Tipovi.SelectedItem).Id))
                    {
                        return false;
                    }
                }

                if (OpasnaDa.IsChecked.Value || OpasnaNe.IsChecked.Value)
                {
                    if (eti.Opasna)
                    {
                        if (!OpasnaDa.IsChecked.Value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!OpasnaNe.IsChecked.Value)
                        {
                            return false;
                        }
                    }
                     
                }


                if (RegionDa.IsChecked.Value || RegionNe.IsChecked.Value)
                {
                    if (eti.NaseljenRegion)
                    {
                        if (!RegionDa.IsChecked.Value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!RegionNe.IsChecked.Value)
                        {
                            return false;
                        }
                    }

                }


                if (CrvenaDa.IsChecked.Value || CrvenaNe.IsChecked.Value)
                {
                    if (eti.CrvenaLista)
                    {
                        if (!CrvenaDa.IsChecked.Value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!CrvenaNe.IsChecked.Value)
                        {
                            return false;
                        }
                    }

                }

                return true;
            };
        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            collectionView.Filter = (et) =>
            {
                Tipovi.SelectedIndex = -1;
                OpasnaDa.IsChecked = false;
                OpasnaNe.IsChecked = false;
                RegionDa.IsChecked = false;
                RegionNe.IsChecked = false;
                CrvenaDa.IsChecked = false;
                CrvenaNe.IsChecked = false;
                return true;
            };
        }
    }
    public class YesNoToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "True":
                    return true;
                case "False":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "Da";
                else
                    return "Ne";
            }
            return "Ne";
        }
    }
}
