using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOvideotheek
{
    public class Film: INotifyPropertyChanged
    {
        public Film(int bandnr, string titel, Filmgenre genre, int invoorraad, int uitvoorraad, decimal prijs, int totaalverhuurd)
        {
            bandNrValue = bandnr;
            Titel = titel;
            Genre = genre;
            InVoorraad = invoorraad;
            UitVoorraad = uitvoorraad;
            Prijs = prijs;
            TotaalVerhuurd = totaalverhuurd;
            Changed = false;
            IsNewItem = false;
        }

        private int bandNrValue;

        public int BandNr
        {
            get { return bandNrValue; }
            internal set { bandNrValue = value;  }
        }

        private string titelValue;

        public string Titel
        {
            get { return titelValue; }
            set { titelValue = value; }
        }

        private Filmgenre genreValue;

        public Filmgenre Genre
        {
            get { return genreValue; }
            set { genreValue = value; }
        }

        private int inVoorraadValue;

        public int InVoorraad
        {
            get { return inVoorraadValue; }
            set { inVoorraadValue = value; Changed = true; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InVoorraad")); }
        }

        private int uitVoorraadValue;

        public int UitVoorraad
        {
            get { return uitVoorraadValue; }
            set { uitVoorraadValue = value; Changed = true; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UitVoorraad")); }
        }

        private decimal prijsValue;

        public decimal Prijs
        {
            get { return prijsValue; }
            set { prijsValue = value; }
        }

        private int totaalVerhuurdValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public int TotaalVerhuurd
        {
            get { return totaalVerhuurdValue; }
            set { totaalVerhuurdValue = value; Changed = true; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotaalVerhuurd")); }
        }

        public bool Changed { get; set; }

        public bool IsNewItem { get; set; }
    }
}