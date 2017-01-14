
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



    }
}
