using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentrumSportu_WPF
{
    [Table("Pracownik")]
    public class Pracownik : Osoba
    {
        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public Pracownik(String imie, String nazwisko) : base(imie, nazwisko) { }
        public Pracownik() : base() { }
    }
}
