using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    public abstract class Osoba
    {
        public string ID { get;  set; }

        public string Imie { get; private set; }

        public string Nazwisko { get; private set; }

        public Osoba(string id ,string imie,string nazwisko)
        {
            ID = id;
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public Osoba()
        {

        }

    }
}
