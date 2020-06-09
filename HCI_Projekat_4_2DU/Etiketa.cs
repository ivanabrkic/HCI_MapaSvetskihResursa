using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HCI_Projekat_4_2DU
{
    [Serializable]
    public class Etiketa : INotifyPropertyChanged
    {
        private string ID;
        public string Id
        {
            get
            {
                return ID;
            }

            set
            {
                if (this.ID != value)
                {
                    this.ID = value;
                    this.NotifyPropertyChanged("Id");
                }
            }
        }
        [field:NonSerialized]
        private SolidColorBrush boja;
        
        public String BojaCuvanje;

        public SolidColorBrush Boja
        {
            get
            {
                return boja;
            }

            set
            {
                this.boja = value;
                this.NotifyPropertyChanged("Boja");
            }
        }


        private string opis;

        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                opis = value;
                this.NotifyPropertyChanged("Opis");
            }
        }

        public override string ToString()
        {
            return ID;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
