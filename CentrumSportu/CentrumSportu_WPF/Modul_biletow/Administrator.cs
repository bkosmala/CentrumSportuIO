using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    class Instruktor : Osoba
    {

        public Instruktor(string imie, string nazwisko) : base(imie, nazwisko)
        {


        }

         private void StworzAdministratora(string imie, string nazwisko)
        {
            Instruktor a = new Instruktor(imie, nazwisko);
            //dodanie do listy?
        }

        private void StworzInstruktora(string imie, string nazwisko)
        {
            Instruktor a = new Instruktor(imie, nazwisko);
            //dodanie do listy?
        }

        private void usunUzytkownika(Osoba o)
        {
            
        }

    }
}
