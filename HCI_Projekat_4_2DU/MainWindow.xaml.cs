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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Threading;

namespace HCI_Projekat_4_2DU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Vrsta> ListaVrsta { get; set; }
        public static ObservableCollection<Tip> ListaTipova { get;  set; }
        public static ObservableCollection<Etiketa> ListaEtiketa { get; set; }
        public static Point startPoint;
        public static Dictionary<string, List<double>> listaZaMapu;
        public static Boolean demoMode = false;

        public MainWindow()
        {
            DataContext = this;
            ListaVrsta = new ObservableCollection<Vrsta>();
            ListaTipova = new ObservableCollection<Tip>();
            ListaEtiketa = new ObservableCollection<Etiketa>();
            listaZaMapu = new Dictionary<string, List<double>>();

            RoutedCommand newCmd = new RoutedCommand();
            newCmd.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd, MenuItem_Click));

            RoutedCommand newCmd1 = new RoutedCommand();
            newCmd1.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd1, MenuItem_Click_1));

            RoutedCommand newCmd2 = new RoutedCommand();
            newCmd2.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd2, MenuItem_Click_2));


            

            RoutedCommand newCmd3 = new RoutedCommand();
            newCmd3.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd3, MenuItem_Click_3));

            RoutedCommand newCmd4 = new RoutedCommand();
            newCmd4.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd4, MenuItem_Click_5));

            RoutedCommand newCmd5 = new RoutedCommand();
            newCmd5.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd5, MenuItem_Click_4));



            RoutedCommand newCmd6 = new RoutedCommand();
            newCmd6.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(newCmd6, MenuItem_Click_8));

            RoutedCommand newCmd7 = new RoutedCommand();
            newCmd7.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(newCmd7, MenuItem_Click_7));

            RoutedCommand newCmd8 = new RoutedCommand();
            newCmd8.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(newCmd8, MenuItem_Click_6));



            RoutedCommand newCmd9 = new RoutedCommand();
            newCmd9.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd9, Demo_Begin));

            RoutedCommand newCmd10 = new RoutedCommand();
            newCmd10.InputGestures.Add(new KeyGesture(Key.F1));
            CommandBindings.Add(new CommandBinding(newCmd10, MenuItem_Click_9));


            ucitajEtikete();
            ucitajTipove();
            UcitajVrste();
            

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Closing += OnWindowClosing;
            InitializeComponent();
            UcitajKordinate();
            
        }

        private void UcitajVrste()
        {
            if (File.Exists("vrste.bin") && (new FileInfo("vrste.bin").Length != 0))
            {
                var formatter = new BinaryFormatter();
                FileStream stream = File.OpenRead("vrste.bin");
                var lista = (List<Vrsta>)formatter.Deserialize(stream);
                stream.Close();

                foreach (Vrsta item in lista)
                {
                    item.Ikonica = (BitmapImage) ByteToImage(item.Base64);
                    item.Datum = DateTime.Parse(item.DATUM);
                    ListaVrsta.Add(item);
                }
            }
        }

        private void UcitajKordinate()
        {
            if (File.Exists("kordinate.bin") && (new FileInfo("kordinate.bin").Length != 0))
            {
                var formatter = new BinaryFormatter();
                FileStream stream = File.OpenRead("kordinate.bin");
                listaZaMapu = (Dictionary<string, List<double>>)formatter.Deserialize(stream);
                Console.WriteLine("U LISTI IMA: " + listaZaMapu.Count);
                stream.Close();
                for (int i = 0; i < listaZaMapu.Count; i++)
                {
                    Console.WriteLine(listaZaMapu.Keys.ElementAt(i));
                }
                foreach(string key in listaZaMapu.Keys)
                {
                    foreach(Vrsta vrsta in ListaVrsta)
                    {
                        if (key.Equals(vrsta.Id))
                        {
                            Image imageControl = new Image();

                            imageControl = new Image() { Width = 60, Height = 50, Source = vrsta.Ikonica };
                            imageControl.ToolTip = " ID: " + vrsta.Id + "\n Ime: " + vrsta.Ime + "\n Tip: " + vrsta.Tip + "\n Ime tipa: " + vrsta.Tip.Ime;
                            imageControl.AllowDrop = false;
                            imageControl.MouseRightButtonDown += Image_RightClick;
                            Canvas.SetLeft(imageControl, listaZaMapu[key][0]);
                            Canvas.SetTop(imageControl, listaZaMapu[key][1]);
                            imageControl.MouseLeftButtonDown += imageControl_MouseLeftButtonDown;
                            this.DropList.Children.Add(imageControl);
                        }
                    }
                }
            }
        }

        private void ucitajEtikete()
        {
            if (File.Exists("etikete.bin") && (new FileInfo("etikete.bin").Length != 0))
            {
                var formatter = new BinaryFormatter();
                FileStream stream = File.OpenRead("etikete.bin");
                var lista = (List<Etiketa>)formatter.Deserialize(stream);
                stream.Close();

                foreach (Etiketa item in lista)
                {
                    item.Boja = (SolidColorBrush)new BrushConverter().ConvertFrom(item.BojaCuvanje);
                    ListaEtiketa.Add(item);
                }
            }
        }

        private void ucitajTipove()
        {
            if (File.Exists("tipovi.bin") && (new FileInfo("tipovi.bin").Length != 0))
            {
                var formatter = new BinaryFormatter();
                FileStream stream = File.OpenRead("tipovi.bin");
                var lista = (List<Tip>)formatter.Deserialize(stream);
                stream.Close();

                foreach (Tip item in lista)
                {
                    item.Ikonica = (BitmapImage) ByteToImage(item.Base64);
                    ListaTipova.Add(item);
                }
            }
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            PngBitmapEncoder png = new PngBitmapEncoder();
            List<Vrsta> listaR = ListaVrsta.ToList<Vrsta>();

            foreach (Vrsta item in listaR)
            {
                item.Base64 = ImageSourceToBytes(png, item.Ikonica);
                item.DATUM = item.Datum.ToString();
            }

            FileStream stream = File.Create("vrste.bin");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, listaR);
            stream.Close();
            ////////////////////////////////////////////////////////
            List<Etiketa> listaE = ListaEtiketa.ToList<Etiketa>();

            foreach (Etiketa item in listaE)
            {
                item.BojaCuvanje = item.Boja.Color.ToString();
            }

            stream = File.Create("etikete.bin");
            formatter = new BinaryFormatter();
            formatter.Serialize(stream, listaE);
            stream.Close();
            /////////////////////////////////////////////////////////
            stream = File.Create("kordinate.bin");
            formatter = new BinaryFormatter();
            formatter.Serialize(stream, listaZaMapu);
            stream.Close();
            ////////////////////////////////////////////////////////
            List<Tip> listaT = ListaTipova.ToList<Tip>();

            foreach (Tip t in listaT)
            {
                t.Base64 = ImageSourceToBytes(png, t.Ikonica);
            }

            stream = File.Create("tipovi.bin");
            formatter = new BinaryFormatter();
            formatter.Serialize(stream, listaT);
            stream.Close();
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            PngBitmapEncoder encoder2 = new PngBitmapEncoder();

            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder2.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder2.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VrsteDodaj dodajVrstu = new VrsteDodaj();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            TipDodaj dodajTip = new TipDodaj();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DodajEtiketu dodajEtiketu = new DodajEtiketu();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            VrstePrikaz vrstePrikaz = new VrstePrikaz();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            EtiketePrikaz etiketePrikaz = new EtiketePrikaz();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            TipoviPrikaz tipoviPrikaz = new TipoviPrikaz();
        }

        private void PretragaTipova_Click(object sender, RoutedEventArgs e)
        {
            TabelaTipova t = new TabelaTipova();
        }

        private void PretragaEtiketa_Click(object sender, RoutedEventArgs e)
        {
            TabelaEtiketa et = new TabelaEtiketa();
        }

        private void PretragaVrsta_Click(object sender, RoutedEventArgs e)
        {
            TabelaVrsta tv = new TabelaVrsta();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            IzmenaEtiketa ie = new IzmenaEtiketa();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            IzmenaTipa it = new IzmenaTipa();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            IzmenaVrsta iv = new IzmenaVrsta();
        }

        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           startPoint = e.GetPosition(null);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && (
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);
                Vrsta vrsta = null;
                // Find the data behind the ListViewItem
                if (listViewItem != null)
                {
                    vrsta = (Vrsta)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);
                } 
                else
                {
                    return;
                }
                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", vrsta);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);

            }
        }

        private static T FindAnchestor<T>(DependencyObject current)
    where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
        sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void updateCanvas()
        {
            foreach (var item in DropList.Children)
            {
                Image imageControl = (Image)item;
                Vrsta vrsta = (Vrsta)imageControl.DataContext;
                imageControl.ToolTip = " ID: " + vrsta.Id + "\n Ime: " + vrsta.Ime + "\n Tip: " + vrsta.Tip + "\n Ime tipa: " + vrsta.Tip.Ime;
                imageControl.Source = vrsta.Ikonica;

            }
        }

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            Image imageControl = new Image();

            if (e.Data.GetDataPresent("myFormat"))
            {
                Vrsta vrsta = e.Data.GetData("myFormat") as Vrsta;
                imageControl = new Image() { Width = 60, Height = 50, Source = vrsta.Ikonica, DataContext = vrsta };
                imageControl.ToolTip = " ID: " + vrsta.Id + "\n Ime: " + vrsta.Ime + "\n Tip: " + vrsta.Tip + "\n Ime tipa: " + vrsta.Tip.Ime;
                imageControl.AllowDrop = false;
                imageControl.MouseRightButtonDown += Image_RightClick;
                if (DropList.Children.Count > 0)
                {
                    foreach (Image i in DropList.Children)
                    {
                        string tooltip = (string)i.ToolTip;
                        string[] elements = tooltip.Split('\n');
                        string id = elements[0];
                        if (id.Contains(vrsta.Id))
                        {
                            MessageBox.Show("Zeljena vrsta se već nalazi na mapi!");
                            return;
                        }
                       
                        
                    }
                }

                //List<double> temp = new List<double>();
                //temp.Add(e.GetPosition(this.DropList).X - imageControl.Width / 2);
                //temp.Add(e.GetPosition(this.DropList).Y - imageControl.Height / 2);
                //listaZaMapu.Add(vrsta.Id, temp);
                //Console.WriteLine("KORDINATE UNETE U LISTU!\n");
                //Console.WriteLine("U listi ima trenutno:" + listaZaMapu.Count + "\n");

            }
            else
            {
                if ((e.Data.GetData(typeof(Image)) != null))
                {
                    Image image = e.Data.GetData(typeof(Image)) as Image;
                    imageControl = image;
                    if (this.DropList.Children.Contains(image))
                    {
                        this.DropList.Children.Remove(image);
                    }
                    
                }
            }

            Canvas.SetLeft(imageControl, e.GetPosition(this.DropList).X - imageControl.Width/2);
            Canvas.SetTop(imageControl, e.GetPosition(this.DropList).Y - imageControl.Height/2);
            string text = imageControl.ToolTip.ToString();
            string[] prvaPodela = text.Split('\n');  // " ID: id" 
            string[] drugaPodela = prvaPodela[0].Split(' ');
            if (listaZaMapu.ContainsKey(drugaPodela[2]))
            {
                listaZaMapu[drugaPodela[2]][0] = e.GetPosition(this.DropList).X - imageControl.Width / 2;
                listaZaMapu[drugaPodela[2]][1] = e.GetPosition(this.DropList).Y - imageControl.Height / 2;
            } else
            {
                List<double> temp = new List<double>();
                temp.Add(e.GetPosition(this.DropList).X - imageControl.Width / 2);
                temp.Add(e.GetPosition(this.DropList).Y - imageControl.Height / 2);
                listaZaMapu.Add(drugaPodela[2], temp);
            }



            imageControl.MouseLeftButtonDown += imageControl_MouseLeftButtonDown;
            this.DropList.Children.Add(imageControl);
        }

        void imageControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;
            DataObject data = new DataObject(typeof(Image), image);
            DragDrop.DoDragDrop(image, data, DragDropEffects.All);
        }

        private void Image_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                Image image = sender as Image;

                string tooltip = (string)image.ToolTip;
                string[] elements = tooltip.Split('\n');
                string id1 = elements[0];
                string[] elements1 = id1.Split(' ');
                string id = elements1[2];
                //Console.WriteLine("ID VRSTE JE:   " + id);
                ContextMenu contextMenu = new ContextMenu();

                MenuItem item1 = new MenuItem();

                item1.Header = "Detalji";

                int indexDetails = 0;

                for (int i = 0; i < ListaVrsta.Count; i++)
                {
                    if (id.Equals(ListaVrsta[i].Id))
                    {
                        indexDetails = i;
                        break;
                    }
                }

                item1.Click += delegate {
                    VrstePrikaz vp = new VrstePrikaz(indexDetails);
                };

                contextMenu.Items.Add(item1);

                MenuItem item2 = new MenuItem();

                item2.Header = "Izmeni";

                Vrsta zaIzmenu = null;

                foreach (Vrsta v in MainWindow.ListaVrsta)
                {

                    if (id.Equals(v.Id))
                    {
                        zaIzmenu = v;
                        break;
                    }
                }
                int indexIzmena = 0;

                for (int i = 0; i < ListaVrsta.Count; i++)
                {
                    if (id.Equals(ListaVrsta[i].Id))
                    {
                        indexIzmena = i;
                        break;
                    }
                }


                item2.Click += delegate {
                    IzmenaVrsta re = new IzmenaVrsta(indexIzmena);
                    image.ToolTip = " ID: " + zaIzmenu.Id + "\n Ime: " + zaIzmenu.Ime + "\n Tip: " + zaIzmenu.Tip + "\n Ime tipa: " + zaIzmenu.Tip.Ime;
                    image.Source = zaIzmenu.Ikonica;
                };

                contextMenu.Items.Add(item2);

                MenuItem item3 = new MenuItem();

                item3.Header = "Obriši";

                item3.Click += delegate {

                    for (int i = 0; i < DropList.Children.Count; i++)
                    {
                        Image img = (Image)DropList.Children[i];
                        if (img.Equals(image))
                        {
                            DropList.Children.RemoveAt(i);
                            string text = img.ToolTip.ToString();
                            string[] prvaPodela = text.Split('\n');  // " ID: id" 
                            string[] drugaPodela = prvaPodela[0].Split(' ');
                            listaZaMapu.Remove(drugaPodela[2]);
                        }
                    }

                };

                contextMenu.Items.Add(item3);

                image.ContextMenu = contextMenu;
                contextMenu.PlacementTarget = image;
                contextMenu.IsOpen = true;
            }
        }

        private void Demo_Begin(object sender, RoutedEventArgs e)
        {
            demoMode = true;
            VrsteDodaj dodajVrstu = null;
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                Point p = Vrsta1.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 100);


            });

            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                Vrsta1.IsSubmenuOpen = true;
            });


            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = Dodaj_vrstu.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 40);

            });
             


            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                dodajVrstu = new VrsteDodaj();
                Vrsta1.IsSubmenuOpen = false;
                Point p = dodajVrstu.textBox_ID.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;
                

                LinearSmoothMove(p, 100);
                System.Threading.Thread.Sleep(300);

                dodajVrstu.textBox_ID.Focus();
                p.X -= 30;

                LinearSmoothMove(p, 30);
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }

            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "I";
            });

            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id";
            });
            Console.WriteLine("Demo mod: " + demoMode.ToString());
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id ";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id v";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id vr";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id vrs";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id vrst";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_ID.Text = "Id vrste";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(200);
                Point p = dodajVrstu.textBox_IME.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 40);
                dodajVrstu.textBox_IME.Focus();
                System.Threading.Thread.Sleep(100);
                p.X -= 30;

                LinearSmoothMove(p, 30);

            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "I";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Im";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime ";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime v";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime vr";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime vrs";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime vrst";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_IME.Text = "Ime vrste";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.textBox_OPIS.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 40);
                dodajVrstu.textBox_OPIS.Focus();
                System.Threading.Thread.Sleep(100);
                p.X -= 30;

                LinearSmoothMove(p, 30);

            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "O";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Op";
                
            });


            if (!demoMode) return;
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opi";
            });


            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis ";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis v";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis vr";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis vrs";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis vrst";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_OPIS.Text = "Opis vrste";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.textBox_PRIHOD.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 80);
                dodajVrstu.textBox_PRIHOD.Focus();
                System.Threading.Thread.Sleep(100);
                p.X -= 30;

                LinearSmoothMove(p, 30);

            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_PRIHOD.Text = "6";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_PRIHOD.Text = "66";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                dodajVrstu.textBox_PRIHOD.Text = "666";
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.checkBox_IUCN.PointToScreen(new Point(0d, 0d));
                p.X += 5;
                p.Y += 5;

                LinearSmoothMove(p, 40);


            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);

                dodajVrstu.checkBox_IUCN.IsChecked = true;
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.checkBox_NASELJEN.PointToScreen(new Point(0d, 0d));
                p.X += 5;
                p.Y += 5;

                LinearSmoothMove(p, 40);

            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);

                dodajVrstu.checkBox_NASELJEN.IsChecked = true;
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.listaEtiketaXAML.PointToScreen(new Point(0d, 0d));
                p.X += 5;
                p.Y += 5;

                LinearSmoothMove(p, 40);

                


            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);

                dodajVrstu.listaEtiketaXAML.SelectedItems.Add(dodajVrstu.listaEtiketaXAML.Items[0]);
            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(300);
                Point p = dodajVrstu.button2.PointToScreen(new Point(0d, 0d));
                p.X += 20;
                p.Y += 10;

                LinearSmoothMove(p, 40);




            });
            if (!demoMode)
            {
                dodajVrstu.Close();
                return;
            }
            Dispatcher.Invoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(3000);

                dodajVrstu.Close();
            });

        }

        public void LinearSmoothMove(Point newPosition, int steps)
        {
            if (demoMode)
            {
                Point start = Win32.GetMousePosition();
                Point iterPoint = start;

                // Find the slope of the line segment defined by start and newPosition
                Point slope = new Point(newPosition.X - start.X, newPosition.Y - start.Y);

                // Divide by the number of steps
                slope.X = slope.X / steps;
                slope.Y = slope.Y / steps;

                // Move the mouse to each iterative point.
                for (int i = 0; i < steps; i++)
                {
                    if (!demoMode)
                    {
                        return;
                    }
                    iterPoint = new Point(iterPoint.X + slope.X, iterPoint.Y + slope.Y);
                    Win32.SetCursorPos((int)iterPoint.X, (int)iterPoint.Y);
                    Thread.Sleep(20);
                }

                // Move the mouse to the final destination.
                Win32.SetCursorPos((int)newPosition.X, (int)newPosition.Y);
            }
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            HelpViewer browser = new HelpViewer();
        }
    }
}

