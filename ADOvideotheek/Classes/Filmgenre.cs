
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOvideotheek
{
    public class Filmgenre
    {
        public int GenreNr { get; set; }
        public string GenreNaam { get; set; }
        public Filmgenre(int genrenr, string naam)
        {
            GenreNr = genrenr;
            GenreNaam = naam;
        }
        public override string ToString()
        {
            return this.GenreNaam;
        }
    }
}