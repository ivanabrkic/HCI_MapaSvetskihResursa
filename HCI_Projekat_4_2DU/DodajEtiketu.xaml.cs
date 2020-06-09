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

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for DodajEtiketu.xaml
    /// </summary>
    public partial class DodajEtiketu : Window
    {
        public DodajEtiketu()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (validno())
            {
                Etiketa novaEtiketa = new Etiketa();
                novaEtiketa.Id = textBoxID.Text;
                novaEtiketa.Opis = textBoxOpis.Text;
                Color boja = new Color();
                boja = (Color)ClrPcker_Background.SelectedColor;
                novaEtiketa.Boja = (SolidColorBrush)(new BrushConverter().ConvertFrom(boja.ToString()));
                MainWindow.ListaEtiketa.Add(novaEtiketa);
                this.Close();
            }
        }

        public bool validno()
        {
            foreach (char c in textBoxID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID etikete ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }
            }
                if (String.IsNullOrWhiteSpace(textBoxID.Text))
                {
                    System.Windows.MessageBox.Show("Polje ID etikete ne sme biti prazno!");
                    return false;
                }

                foreach (Etiketa e in MainWindow.ListaEtiketa)
                {
                    if (e.Id.Equals(textBoxID.Text))
                    {
                        System.Windows.MessageBox.Show("Uneti ID etikete već postoji!");
                        return false;
                    }
                }

            return true;
        }

        private void textBoxID_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBoxID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxID.ToolTip = "ID etikete ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBoxID.ClearValue(Border.BorderBrushProperty);
                    textBoxID.ToolTip = "Ovde unesite jedinstveni ID etikete.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBoxID.Text))
            {
                textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                textBoxID.ToolTip = "Polje ID etikete ne sme biti prazno!";
                return;
            }
            else
            {
                textBoxID.ClearValue(Border.BorderBrushProperty);
                textBoxID.ToolTip = "Ovde unesite jedinstveni ID etikete.";
            }

            foreach (Etiketa et in MainWindow.ListaEtiketa)
            {
                if (et.Id.Equals(textBoxID.Text))
                {
                    textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxID.ToolTip = "Uneti ID etikete već postoji!";
                    return;
                }
                else
                {
                    textBoxID.ClearValue(Border.BorderBrushProperty);
                    textBoxID.ToolTip = "Ovde unesite jedinstveni ID etikete.";
                }
            }
        }
    }
}
