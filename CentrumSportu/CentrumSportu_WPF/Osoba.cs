using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    public abstract class Osoba
    {
        public string ID { get; private set; }

        public string Imie { get; private set; }

        public string Nazwisko { get; private set; }

        public string Login { get; set; }

        public Osoba(string id ,string imie,string nazwisko, KontoUzytkownika konto)
        {
            ID = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Login = konto.Login;
        }

        public Osoba()
        {

        }

    }
}
