using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozijegykezelo1.Model
{
    internal class Jegy
    {
        private string _vevoNev;
        private string _filmCim;
        private string _vetitesIdopont;
        private string _szekSor;
        private int _szekSzam;

        public Jegy(string vevoNev, string filmCim, string vetitesIdopont, string szekSor, int szekSzam)
        {
            _vevoNev = vevoNev;
            _filmCim = filmCim;
            _vetitesIdopont = vetitesIdopont;
            _szekSor = szekSor;
            _szekSzam = szekSzam;
        }

        public Jegy()
        {

        }

        public string VevoNev { get => _vevoNev; set => _vevoNev = value; }
        public string FilmCim { get => _filmCim; set => _filmCim = value; }
        public string VetitesIdopont { get => _vetitesIdopont; set => _vetitesIdopont = value; }
        public string SzekSor { get => _szekSor; set => _szekSor = value; }
        public int SzekSzam { get => _szekSzam; set => _szekSzam = value; }

        public override string ToString()
        {
            return $"{VevoNev}, {FilmCim}, {VetitesIdopont}, {SzekSor} sor, {SzekSzam}. szék";

        }
    }
}
