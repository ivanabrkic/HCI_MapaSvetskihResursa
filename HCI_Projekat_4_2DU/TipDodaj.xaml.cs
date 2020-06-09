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
using System.Windows.Forms;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for TipDodaj.xaml
    /// </summary>
    public partial class TipDodaj : Window
    {
        public TipDodaj()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.DecodePixelHeight = 100;
                bitmap.DecodePixelWidth = 100;
                bitmap.EndInit();
                image1.Source = bitmap;

            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            image1.Source = null;
        }

        public bool validno()
        {
            foreach (char c in textBoxID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID tipa ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }
            }
                if (String.IsNullOrWhiteSpace(textBoxNazivTipa.Text))
                {
                    System.Windows.MessageBox.Show("Polje ime tipa ne sme biti prazno!");
                    return false;
                }

                if (String.IsNullOrWhiteSpace(textBoxID.Text))
                {
                    System.Windows.MessageBox.Show("Polje ID tipa ne sme biti prazno!");
                    return false;

                }
                if (image1.Source == null)
            {
                    System.Windows.MessageBox.Show("Slika je obavezna za tip!");
                    return false;
            }

                foreach(Tip t in MainWindow.ListaTipova)
                {
                    if (t.Id.Equals(textBoxID.Text))
                    {
                        System.Windows.MessageBox.Show("Uneti ID tipa već postoji!");
                        return false;
                    }
                }
            return true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (validno())
            {
                Tip noviTip = new Tip();
                noviTip.Id = textBoxID.Text;
                noviTip.Ime = textBoxNazivTipa.Text;
                noviTip.Opis = textBoxOpis.Text;
                noviTip.Ikonica = (BitmapImage)image1.Source;
                MainWindow.ListaTipova.Add(noviTip);
                this.Close();
            }
        }

        private void textBoxID_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBoxID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxID.ToolTip = "ID tipa ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBoxID.ClearValue(Border.BorderBrushProperty);
                    textBoxID.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBoxID.Text))
            {
                textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                textBoxID.ToolTip = "Polje ID tipa ne sme biti prazno!";
                return;
            }
            else
            {
                textBoxID.ClearValue(Border.BorderBrushProperty);
                textBoxID.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
            }

            foreach (Tip t in MainWindow.ListaTipova)
            {
                if (t.Id.Equals(textBoxID.Text))
                {
                    textBoxID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBoxID.ToolTip = "Uneti ID tipa već postoji!";
                    return;
                }
                else
                {
                    textBoxID.ClearValue(Border.BorderBrushProperty);
                    textBoxID.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
                }
           } 
        }

        private void textBoxNazivTipa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxNazivTipa.Text))
            {
                textBoxNazivTipa.BorderBrush = System.Windows.Media.Brushes.Red;
                textBoxNazivTipa.ToolTip = "Polje ime tipa ne sme biti prazno!";
                return;
            }
            else
            {
                textBoxNazivTipa.ClearValue(Border.BorderBrushProperty);
                textBoxNazivTipa.ToolTip = "Ovde unesite ime tipa ugrožene vrste.";
            }
        }
    }
}
