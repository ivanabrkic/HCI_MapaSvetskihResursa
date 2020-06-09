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
using System.Collections.ObjectModel;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class VrsteDodaj : Window
    {
        public ObservableCollection<Tip> listaTipova { get; set; }
        public ObservableCollection<Etiketa> listaEtiketa { get; set; }
        public VrsteDodaj()
        {
            listaTipova = MainWindow.ListaTipova;
            listaEtiketa = MainWindow.ListaEtiketa;
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (validno())
            {
                Vrsta novaVrsta = new Vrsta();
                novaVrsta.Id = textBox_ID.Text;
                novaVrsta.Ime = textBox_IME.Text;
                novaVrsta.Opis = textBox_OPIS.Text;
                String statusU = "Default";
                switch (comboBox_UGROZENOST.SelectedIndex)
                {
                    case 0:
                        statusU = "Kriticno ugrozena";
                        break;
                    case 1:
                        statusU = "Ugrozena";
                        break;
                    case 2:
                        statusU = "Ranjiva";
                        break;
                    case 3:
                        statusU = "Zavisna od ocuvanja stanista";
                        break;
                    case 4:
                        statusU = "Blizu rizika";
                        break;
                    case 5:
                        statusU = "Najmanjeg rizika";
                        break;
                }
                novaVrsta.StatusU = statusU;
                novaVrsta.Tip = (Tip)comboBox_TIP.SelectedItem;
                if (checkBox_OPASNA.IsChecked.HasValue && checkBox_OPASNA.IsChecked.Value)
                    novaVrsta.Opasna = true;
                if (checkBox_IUCN.IsChecked.HasValue && checkBox_IUCN.IsChecked.Value)
                    novaVrsta.CrvenaLista = true;
                if (checkBox_NASELJEN.IsChecked.HasValue && checkBox_NASELJEN.IsChecked.Value)
                    novaVrsta.NaseljenRegion = true;
                String statusT = "Default";
                switch (comboBox_TURISTICKI.SelectedIndex)
                {
                    case 0:
                        statusT = "Izolovana";
                        break;
                    case 1:
                        statusT = "Delimicno habituirana";
                        break;
                    case 2:
                        statusT = "Habituirana";
                        break;
                }
                novaVrsta.Etikete = new ObservableCollection<Etiketa>();
                foreach (var item in listaEtiketaXAML.SelectedItems)
                {
                    novaVrsta.Etikete.Add((Etiketa)item);
                }
                novaVrsta.TStatus = statusT;
                novaVrsta.Datum = datePicker_DATUM.SelectedDate.Value.Date;
                novaVrsta.Ikonica = (BitmapImage)image_ikonica.Source;
                if (novaVrsta.Ikonica == null)
                {
                    novaVrsta.Ikonica = novaVrsta.Tip.Ikonica;
                }
                novaVrsta.Prihod = float.Parse(textBox_PRIHOD.Text);
                MainWindow.ListaVrsta.Add(novaVrsta);

                this.Close();
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
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
                image_ikonica.Source = bitmap;

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            image_ikonica.Source = null;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool validno()
        {
            foreach (char c in textBox_ID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID vrste ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }

            }
            if (String.IsNullOrWhiteSpace(textBox_IME.Text))
            {
                System.Windows.MessageBox.Show("Polje ime vrste ne sme biti prazno!");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox_PRIHOD.Text))
            {
                System.Windows.MessageBox.Show("Polje prihod ne sme biti prazno!");
                return false;
            }
            if (String.IsNullOrWhiteSpace(textBox_ID.Text))
            {
                System.Windows.MessageBox.Show("Polje ID vrste ne sme biti prazno!");
                return false;
            }
            foreach (char c in textBox_PRIHOD.Text)
            {
                if (char.IsLetter(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("Polje prihod ne sme da sadrži slovo ili simbol!");
                    return false;
                }
            }
            foreach (Vrsta v in MainWindow.ListaVrsta)
            {
                if (textBox_ID.Text.Equals(v.Id))
                {
                    System.Windows.MessageBox.Show("Uneti ID vrste već postoji!");
                    return false;
                }
            }

            return true;
        }

        private void textBox_ID_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBox_ID.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBox_ID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBox_ID.ToolTip = "ID vrste ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBox_ID.ClearValue(Border.BorderBrushProperty);
                    textBox_ID.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBox_ID.Text))
            {
                textBox_ID.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox_ID.ToolTip = "Polje ID vrste ne sme biti prazno!";
                return;
            }
            else
            {
                textBox_ID.ClearValue(Border.BorderBrushProperty);
                textBox_ID.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
            }

            foreach (Vrsta v in MainWindow.ListaVrsta)
            {
                if (textBox_ID.Text.Equals(v.Id))
                {
                    textBox_ID.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBox_ID.ToolTip = "Uneti ID vrste već postoji!";
                    return;
                }
                else
                {
                    textBox_ID.ClearValue(Border.BorderBrushProperty);
                    textBox_ID.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
                }
            }

        }

        private void textBox_IME_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox_IME.Text))
            {
                textBox_IME.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox_IME.ToolTip = "Polje ime vrste ne sme biti prazno!";
                return;
            }
            else
            {
                textBox_IME.ClearValue(Border.BorderBrushProperty);
                textBox_IME.ToolTip = "Ovde unesite ime ugrožene vrste.";
            }
        }

        private void textBox_PRIHOD_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox_PRIHOD.Text))
            {
                textBox_PRIHOD.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox_PRIHOD.ToolTip = "Polje prihod ne sme biti prazno!";
                return;
            }
            else
            {
                textBox_PRIHOD.ClearValue(Border.BorderBrushProperty);
                textBox_PRIHOD.ToolTip = "Ovde unesite godišnji prihod od ugrožene vrste.";
            }

            foreach (char c in textBox_PRIHOD.Text)
            {
                if (char.IsLetter(c) || char.IsSymbol(c))
                {
                    textBox_PRIHOD.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBox_PRIHOD.ToolTip = "Polje prihod ne sme da sadrži slovo ili simbol!";
                    return;
                }
                else
                {
                    textBox_PRIHOD.ClearValue(Border.BorderBrushProperty);
                    textBox_PRIHOD.ToolTip = "Ovde unesite godišnji prihod od ugrožene vrste.";
                }
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow.demoMode = false;
                // prevent child controls from handling this event as well
                //e.SuppressKeyPress = true;
            }
        }
    }
}
/*
 * <ComboBoxItem Content="Kriticno ugrozena"/>
            <ComboBoxItem Content="Ugrozena"/>
            <ComboBoxItem Content="Ranjiva"/>
            <ComboBoxItem Content="Zavisna od ocuvanja stanista"/>
            <ComboBoxItem Content="Blizu rizika"/>
            <ComboBoxItem Content="Najmanjeg rizika"/>

    */