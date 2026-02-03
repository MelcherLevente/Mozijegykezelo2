using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mozijegykezelo1.Model
{
    internal class Film
    {

        public Film(int id, string cim, string kategoria, string rendezo, int megjelenesEve, int hossz)
        {
            Id = id;
            Cim = cim;
            Kategoria = kategoria;
            Rendezo = rendezo;
            MegjelenesEve = megjelenesEve;
            Hossz = hossz;
        }
        public Film()
        {
        }

        public int Id { get; set; }
        public string Cim { get; set; }
        public string Kategoria { get; set; }
        public string Rendezo { get; set; }
        public int MegjelenesEve { get; set; }
        public int Hossz { get; set; }
    }
}
