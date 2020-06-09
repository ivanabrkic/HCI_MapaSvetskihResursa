using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HCI_Projekat_4_2DU
{
    [Serializable]
    public class Vrsta : INotifyPropertyChanged
    {
        private string ID;
        private string ime;
        private string opis;
        private Tip tip;
        private string statusU;
        private bool opasna;
        private bool crvenaLista;
        private bool naseljenRegion;
        private string tStatus;
        [NonSerialized]
        private BitmapImage ikonica;
        private float prihod;
        [NonSerialized]
        private DateTime datum;
        private ObservableCollection<Etiketa> etikete;

        public byte[] Base64;
        public string DATUM;
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

        public Tip Tip
        {
            get
            {
                return tip;
            }

            set
            {
                if (this.tip != value)
                {
                    this.tip = value;
                    this.NotifyPropertyChanged("Tip");
                }
            }
        }

        public string StatusU
        {
            get
            {
                return statusU;
            }

            set
            {
                if (this.statusU != value)
                {
                    this.statusU = value;
                    this.NotifyPropertyChanged("StatusU");
                }
            }
        }

        public bool Opasna
        {
            get
            {
                return opasna;
            }

            set
            {
                if(this.opasna != value)
                {
                    this.opasna = value;
                    this.NotifyPropertyChanged("Opasna");
                }
            }
        }

        public bool CrvenaLista
        {
            get
            {
                return crvenaLista;
            }

            set
            {
                if(this.crvenaLista != value)
                {
                    this.crvenaLista = value;
                    this.NotifyPropertyChanged("CrvenaLista");
                }
            }
        }

        public bool NaseljenRegion
        {
            get
            {
                return naseljenRegion;
            }

            set
            {
                if (this.naseljenRegion != value)
                {
                    this.naseljenRegion = value;
                    this.NotifyPropertyChanged("NaseljenRegion");
                }
            }
        }

        public string TStatus
        {
            get
            {
                return tStatus;
            }

            set
            {
                if (this.tStatus != value)
                {
                    this.tStatus = value;
                    this.NotifyPropertyChanged("TStatus");
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

        public float Prihod
        {
            get
            {
                return prihod;
            }

            set
            {
                if (this.prihod != value)
                {
                    this.prihod = value;
                    this.NotifyPropertyChanged("Prihod");
                }
            }
        }

        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                if (this.datum != value)
                {
                    this.datum = value;
                    this.NotifyPropertyChanged("Datum");
                }
            }
        }


        public ObservableCollection<Etiketa> Etikete
        {
            get
            {
                return etikete;
            }

            set
            {
                if (this.etikete != value)
                {
                    this.etikete = value;
                    this.NotifyPropertyChanged("Etikete");
                }
            }
        }



        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


    }
}
