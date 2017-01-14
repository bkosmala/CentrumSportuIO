using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentrumSportu_WPF
{
    [Table("Pracownik")]
    public class Pracownik : Osoba
    {
        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public Pracownik(String imie, String nazwisko, KontoUzytkownika kontoUzytkownika) : base(imie, nazwisko)
        {
            KontoUzytkownika = kontoUzytkownika;
        }
        public Pracownik() : base() { }
    }
}
