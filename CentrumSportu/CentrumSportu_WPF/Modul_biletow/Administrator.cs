
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("Administratorzy")]
    public class Administrator : Pracownik
    {
        public Administrator(string imie, string nazwisko, KontoUzytkownika konto) : base(imie, nazwisko, konto)
        {
            
        }

        public Administrator()
        {
            
        }
        

        private void StworzAdministratora(string imie, string nazwisko)
        {
            //Instruktor a = new Instruktor(imie, nazwisko);
            //dodanie do listy?
        }

        private void StworzInstruktora(string imie, string nazwisko)
        {
            //Instruktor a = new Instruktor(imie, nazwisko);
            //dodanie do listy?
        }

        private void usunUzytkownika(Osoba o)
        {
            
        }

    }
}
