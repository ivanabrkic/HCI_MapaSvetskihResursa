using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;

namespace HCI_Projekat_4_2DU
{
    [Serializable]
    public class Tip : INotifyPropertyChanged
    {
        private String ID;
        private String ime;
        private String opis;

        [field: NonSerialized]
        private BitmapImage ikonica;

        public byte[] Base64;


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
        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                if (this.ime != value)
                {
                    this.ime = value;
                    this.NotifyPropertyChanged("Ime");
                }
            }
        }

        public BitmapImage Ikonica
        {
            get
            {
                return ikonica;
            }

            set
            {
                if (this.ikonica != value)
                {
                    this.ikonica = value;
                    this.NotifyPropertyChanged("Ikonica");
                }
            }
        }
        public string Opis
        {
            get
            {
                return opis;
            }

            set
            {
                if (this.opis != value)
                {
                    this.opis = value;
                    this.NotifyPropertyChanged("Opis");
                }
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
