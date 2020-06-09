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
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        public HelpViewer()
        {
            InitializeComponent();
            Uri u = new Uri(@"C:\Users\Vladimir\Desktop\HCI_Projekat\HCI_Projekat_4_2DU\HCI_Projekat_4_2DU\Help\PocetniProzor.html");
            
           
            wbHelp.Navigate(u);
            this.Show();
        }
    }
}
