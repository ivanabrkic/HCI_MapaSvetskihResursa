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
    /// Interaction logic for IzmenaVrsta.xaml
    /// </summary>
    public partial class IzmenaVrsta : Window
    {
        public ObservableCollection<Vrsta> listaVrstaIzmena { get; set; }
        public ObservableCollection<Tip> listaTipovaIzmena { get; set; }
        public ObservableCollection<Etiketa> listaEtiketaIzmena { get; set; }
        public List<String> stUgLista{ get; set; }
        public List<String> turStLista { get; set; }

        public IzmenaVrsta()
        {
            stUgLista = new List<string>();
            stUgLista.Add("Kriticno ugrozena");
            stUgLista.Add("Ugrozena");
            stUgLista.Add("Ranjiva");
            stUgLista.Add("Zavisna od ocuvanja stanista");
            stUgLista.Add("Blizu rizika");
            stUgLista.Add("Najmanjeg rizika");

            turStLista = new List<string>();
            turStLista.Add("Izolovana");
            turStLista.Add("Delimicno habituirana");
            turStLista.Add("Habituirana");


            listaVrstaIzmena = MainWindow.ListaVrsta;
            listaTipovaIzmena = MainWindow.ListaTipova;
            listaEtiketaIzmena = MainWindow.ListaEtiketa;
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
            this.ShowDialog();
        }
        public IzmenaVrsta(int index)
        {
            stUgLista = new List<string>();
            stUgLista.Add("Kriticno ugrozena");
            stUgLista.Add("Ugrozena");
            stUgLista.Add("Ranjiva");
            stUgLista.Add("Zavisna od ocuvanja stanista");
            stUgLista.Add("Blizu rizika");
            stUgLista.Add("Najmanjeg rizika");

            turStLista = new List<string>();
            turStLista.Add("Izolovana");
            turStLista.Add("Delimicno habituirana");
            turStLista.Add("Habituirana");


            listaVrstaIzmena = MainWindow.ListaVrsta;
            listaTipovaIzmena = MainWindow.ListaTipova;
            listaEtiketaIzmena = MainWindow.ListaEtiketa;
            this.DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
            listBox.SelectedIndex = index;
            this.ShowDialog();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Vrsta v = (Vrsta)listBox.SelectedItem;
            if (v != null)
            {
                if (comboBox_TIP != null)
                {
                    for (int i = 0; i < listaTipovaIzmena.Count; i++)
                    {
                        if (v.Tip.Id.Equals(listaTipovaIzmena[i].Id))
                        {
                            comboBox_TIP.SelectedIndex = i;
                        }
                    }

                    if (v.StatusU.Equals("Kriticno ugrozena"))
                    {
                        comboBox_UGROZENOST.SelectedIndex = 0;
                    }
                    else if (v.StatusU.Equals("Ugrozena"))
                    {
                        comboBox_UGROZENOST.SelectedIndex = 1;
                    }
                    else if (v.StatusU.Equals("Ranjiva"))
                    {
                        comboBox_UGROZENOST.SelectedIndex = 2;
                    }
                    else if (v.StatusU.Equals("Zavisna od ocuvanja stanista"))
                    {
                        comboBox_UGROZENOST.SelectedIndex = 3;
                    }
                    else if (v.StatusU.Equals("Blizu rizika"))
                    {
                        comboBox_UGROZENOST.SelectedIndex = 4;
                    }
                    else
                    {
                        comboBox_UGROZENOST.SelectedIndex = 5;
                    }
                }
            if (comboBox_TURISTICKI != null)
            {
                if (v.TStatus.Equals("Izolovana")) 
                {
                    comboBox_TURISTICKI.SelectedIndex = 0;
                }
                else if (v.TStatus.Equals("Delimicno habituirana"))
                {
                    comboBox_TURISTICKI.SelectedIndex = 1;
                }
                else
                {
                    comboBox_TURISTICKI.SelectedIndex = 2;
                }
            }

                if (listBox1 != null)
                {
                    listBox1.SelectedItems.Clear();
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        Etiketa temp = (Etiketa)listBox1.Items[i];
                        for (int k = 0; k < v.Etikete.Count; k++)
                        {
                            Etiketa temp2 = v.Etikete[k];
                            if (temp.Id.Equals(temp2.Id))
                            {
                                listBox1.SelectedItems.Add(listBox1.Items[i]);
                            }
                        }
                    }
                }
            }
        }

        private bool validno()
        {
            foreach (char c in textBlock.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    System.Windows.MessageBox.Show("ID vrste ne sme da sadrži simbol ili znak interpunkcije!");
                    return false;
                }

            }
            if (String.IsNullOrWhiteSpace(textBox_PRIHOD.Text))
            {
                System.Windows.MessageBox.Show("Polje prihod ne sme biti prazno!");
                return false;
            }

            if (String.IsNullOrWhiteSpace(textBlock1.Text))
            {
                System.Windows.MessageBox.Show("Polje ime vrste ne sme biti prazno!");
                return false;
            }

            if (String.IsNullOrWhiteSpace(textBlock.Text))
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

                foreach (Vrsta v in MainWindow.ListaVrsta)
                {
                    if (((Vrsta)listBox.SelectedItem).Id.Equals(textBlock.Text))
                    {
                        continue;
                    }
                    if (textBlock.Text.Equals(v.Id))
                    {
                        System.Windows.MessageBox.Show("Uneti ID vrste već postoji!");
                        return false;
                    }
                }

            }

            return true;
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((listBox.SelectedItem != null) && validno())
            {
                Vrsta temp = (Vrsta)listBox.SelectedItem;
                temp.Id = textBlock.Text;
                temp.Ime = textBlock1.Text;
                temp.Opis = textBlock2.Text;
                temp.Tip = (Tip)comboBox_TIP.SelectedItem;
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
                temp.StatusU = statusU;
                if (checkBox_OPASNA.IsChecked.HasValue && checkBox_OPASNA.IsChecked.Value)
                    temp.Opasna = true;
                if (checkBox_IUCN.IsChecked.HasValue && checkBox_IUCN.IsChecked.Value)
                    temp.CrvenaLista = true;
                if (checkBox_NASELJEN.IsChecked.HasValue && checkBox_NASELJEN.IsChecked.Value)
                    temp.NaseljenRegion = true;
                temp.Ikonica = (BitmapImage)image_ikonica.Source;
                if (temp.Ikonica == null)
                {
                    temp.Ikonica = temp.Tip.Ikonica;
                }
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

                temp.Etikete = new ObservableCollection<Etiketa>();
                foreach (var item in listBox1.SelectedItems)
                {
                    temp.Etikete.Add((Etiketa)item);
                }

                temp.TStatus = statusT;
                temp.Prihod = float.Parse(textBox_PRIHOD.Text);
                temp.Datum = datePicker_DATUM.SelectedDate.Value.Date;
                ((MainWindow)App.Current.MainWindow).updateCanvas();
                this.Close();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            image_ikonica.Source = null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
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

        private void textBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            foreach (char c in textBlock.Text)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    textBlock.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBlock.ToolTip = "ID vrste ne sme da sadrži simbol ili znak interpunkcije!";
                    return;
                }
                else
                {
                    textBlock.ClearValue(Border.BorderBrushProperty);
                    textBlock.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
                }
            }

            if (String.IsNullOrWhiteSpace(textBlock.Text))
            {
                textBlock.BorderBrush = System.Windows.Media.Brushes.Red;
                textBlock.ToolTip = "Polje ID vrste ne sme biti prazno!";
                return;
            }
            else
            {
                textBlock.ClearValue(Border.BorderBrushProperty);
                textBlock.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
            }

            foreach (Vrsta v in MainWindow.ListaVrsta)
            {
                if (((Vrsta)listBox.SelectedItem).Id.Equals(textBlock.Text))
                {
                    continue;
                }
                if (textBlock.Text.Equals(v.Id))
                {
                    textBlock.BorderBrush = System.Windows.Media.Brushes.Red;
                    textBlock.ToolTip = "Uneti ID vrste već postoji!";
                    return;
                }
                else
                {
                    textBlock.ClearValue(Border.BorderBrushProperty);
                    textBlock.ToolTip = "Ovde unesite jedinstveni ID ugrožene vrste.";
                }
            }

        }

        private void textBlock1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBlock1.Text))
            {
                textBlock1.BorderBrush = System.Windows.Media.Brushes.Red;
                textBlock1.ToolTip = "Polje ime vrste ne sme biti prazno!";
                return;
            }
            else
            {
                textBlock1.ClearValue(Border.BorderBrushProperty);
                textBlock1.ToolTip = "Ovde unesite ime ugrožene vrste.";
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Vrsta vrsta = (Vrsta)listBox.SelectedItem;
            
            //brisanje slike vrste sa canvasa

            foreach(Image im in ((MainWindow)App.Current.MainWindow).DropList.Children)
            {
                string temp = im.ToolTip.ToString();
                if (temp.Contains("ID: " + vrsta.Id))
                {
                    ((MainWindow)App.Current.MainWindow).DropList.Children.Remove(im);
                    break;
                }
            }
            /*
            for (int i = 0; i < MainWindow.ListaVrsta.Count; i++)
            {
                if (MainWindow.ListaVrsta[i].Id.Equals(vrsta.Id))
                {
                    MainWindow.ListaVrsta.RemoveAt(i);
                    break;
                }
            }*/
            listaVrstaIzmena.Remove(vrsta);
            MainWindow.ListaVrsta.Remove(vrsta);
        }
    }
}
