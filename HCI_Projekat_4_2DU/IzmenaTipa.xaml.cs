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
using System.Windows.Forms;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for IzmenaTipa.xaml
    /// </summary>
    public partial class IzmenaTipa : Window
    {
        public ObservableCollection<Tip> listaTipovaPrikaz { get; set; }
        public IzmenaTipa()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            listaTipovaPrikaz = MainWindow.ListaTipova;
            this.DataContext = this;
            InitializeComponent();
            ShowDialog();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            image.Source = null;
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
                image.Source = bitmap;

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if ((listBox.SelectedItem != null) && validno())
            {
                Tip temp = (Tip)listBox.SelectedItem;
                temp.Id = textBox.Text;
                temp.Ime = textBox1.Text;
                temp.Opis = textBox2.Text;
                temp.Ikonica = (BitmapImage)image.Source;
                ((MainWindow)App.Current.MainWindow).updateCanvas();
                this.Close();
            }
        }

        public bool validno()
        {
            foreach (char c in textBox.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID tipa ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }
            }
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                System.Windows.MessageBox.Show("Polje ime tipa ne sme biti prazno!");
                return false;
            }

            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                System.Windows.MessageBox.Show("Polje ID tipa ne sme biti prazno!");
                return false;

            }
            if (image.Source == null)
            {
                System.Windows.MessageBox.Show("Slika je obavezna za tip!");
                return false;
            }

            foreach (Tip t in MainWindow.ListaTipova)
            {
                if (((Tip)listBox.SelectedItem).Id.Equals(textBox.Text))
                {
                    continue;
                }

                if (t.Id.Equals(textBox.Text))
                {
                    System.Windows.MessageBox.Show("Uneti ID tipa već postoji!");
                    return false;
                }
            }
            return true;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBox.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBox.ToolTip = "ID tipa ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBox.ClearValue(Border.BorderBrushProperty);
                    textBox.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox.ToolTip = "Polje ID tipa ne sme biti prazno!";
                return;
            }
            else
            {
                textBox.ClearValue(Border.BorderBrushProperty);
                textBox.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
            }

            foreach (Tip t in MainWindow.ListaTipova)
            {
                if (((Tip)listBox.SelectedItem).Id.Equals(textBox.Text))
                {
                    continue;
                }

                if (t.Id.Equals(textBox.Text))
                {
                    textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBox.ToolTip = "Uneti ID tipa već postoji!";
                    return;
                }
                else
                {
                    textBox.ClearValue(Border.BorderBrushProperty);
                    textBox.ToolTip = "Ovde unesite jedinstveni ID tipa ugrožebe vrste.";
                }
            }

        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox1.ToolTip = "Polje ime tipa ne sme biti prazno!";
                return;
            }
            else
            {
                textBox1.ClearValue(Border.BorderBrushProperty);
                textBox1.ToolTip = "Ovde unesite ime tipa ugrožene vrste.";
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Tip tip = (Tip)listBox.SelectedItem;
            foreach(Vrsta v in MainWindow.ListaVrsta)
            {
                if (v.Tip.Id.Equals(tip.Id)) {
                    System.Windows.MessageBox.Show("Nemoguće je izbrisati tip koji je dodeljen ugroženoj vrsti!");
                    return;
                }
            }
            for (int i = 0; i < MainWindow.ListaTipova.Count; i++)
            {
                if (MainWindow.ListaTipova[i].Id.Equals(tip.Id))
                {
                    MainWindow.ListaTipova.RemoveAt(i);
                    return;
                }
            }

        }
    }
}
