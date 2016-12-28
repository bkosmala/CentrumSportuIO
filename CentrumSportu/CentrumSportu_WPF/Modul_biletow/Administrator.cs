
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("Administratorzy")]
    public class Administrator : Osoba
    {
        public Administrator(string imie, string nazwisko, KontoUzytkownika konto) : base(imie, nazwisko)
        {
            KontoUzytkownika = konto;

        }

        public Administrator()
        {
            
        }

        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

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
