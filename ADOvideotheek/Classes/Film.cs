using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOvideotheek
{
    public class Film
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
        }

        private int bandNrValue;

        public int BandNr
        {
            get { return bandNrValue; }
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
            set { inVoorraadValue = value; Changed = true; }
        }

        private int uitVoorraadValue;

        public int UitVoorraad
        {
            get { return uitVoorraadValue; }
            set { uitVoorraadValue = value; Changed = true; }
        }

        private decimal prijsValue;

        public decimal Prijs
        {
            get { return prijsValue; }
            set { prijsValue = value; }
        }

        private int totaalVerhuurdValue;

        public int TotaalVerhuurd
        {
            get { return totaalVerhuurdValue; }
            set { totaalVerhuurdValue = value; Changed = true; }
        }

        public bool Changed { get; set; }
    }
}