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
    /// Interaction logic for IzmenaEtiketa.xaml
    /// </summary>
    public partial class IzmenaEtiketa : Window
    {
        public ObservableCollection<Etiketa> listaEtiketaIzmena { get; set; }
        public IzmenaEtiketa()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listaEtiketaIzmena = MainWindow.ListaEtiketa;
            this.DataContext = this;
            InitializeComponent();
            ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ((listBox.SelectedItem != null) && validno())
            {
                Etiketa temp = (Etiketa)listBox.SelectedItem;
                temp.Id = textBoxId.Text;
                Color boja = new Color();
                boja = (Color)ClrPcker_Background.SelectedColor;
                temp.Boja = (SolidColorBrush)(new BrushConverter().ConvertFrom(boja.ToString()));
                temp.Opis = textBoxOpis.Text;
                this.Close();
            }
        }

        public bool validno()
        {
            foreach (char c in textBoxId.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID etikete ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }
                if (String.IsNullOrWhiteSpace(textBoxId.Text))
                {
                    System.Windows.MessageBox.Show("Polje ID etikete ne sme biti prazno!");
                    return false;
                }

                foreach (Etiketa e in MainWindow.ListaEtiketa)
                {
                    if (e.Id.Equals(textBoxId.Text))
                    {
                        if (((Etiketa)listBox.SelectedItem).Id.Equals(textBoxId))
                        {
                            continue;
                        }
                        System.Windows.MessageBox.Show("Uneti ID etikete već postoji!");
                        return false;
                    }
                }

            }
            return true;
        }

        private void textBoxId_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBoxId.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBoxId.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxId.ToolTip = "ID etikete ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBoxId.ClearValue(Border.BorderBrushProperty);
                    textBoxId.ToolTip = "Ovde unesite jedinstveni ID etikete.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBoxId.Text))
            {
                textBoxId.BorderBrush = System.Windows.Media.Brushes.Red;
                textBoxId.ToolTip = "Polje ID etikete ne sme biti prazno!";
                return;
            }
            else
            {
                textBoxId.ClearValue(Border.BorderBrushProperty);
                textBoxId.ToolTip = "Ovde unesite jedinstveni ID etikete.";
            }

            foreach (Etiketa et in MainWindow.ListaEtiketa)
            {
                if (et.Id.Equals(textBoxId.Text))
                {
                    if (((Etiketa)listBox.SelectedItem).Id.Equals(textBoxId))
                    {
                        continue;
                    }
                    textBoxId.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxId.ToolTip = "Uneti ID etikete već postoji!";
                    ;
                }
                else
                {
                    textBoxId.ClearValue(Border.BorderBrushProperty);
                    textBoxId.ToolTip = "Ovde unesite jedinstveni ID etikete.";
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Etiketa etiketa = (Etiketa)listBox.SelectedItem;
            foreach (Vrsta v in MainWindow.ListaVrsta)
            {
                foreach(Etiketa et in v.Etikete)
                {
                    if (et.Id.Equals(etiketa.Id))
                    {
                        System.Windows.MessageBox.Show("Nemoguće je izbrisati etiketu kojom je neka vrsta tagovana!");
                        return;
                    }
                }
            }
            for (int i = 0; i < MainWindow.ListaEtiketa.Count; i++)
            {
                if (MainWindow.ListaEtiketa[i].Id.Equals(etiketa.Id))
                {
                    MainWindow.ListaEtiketa.RemoveAt(i);
                }
            }
        }
    }
}
