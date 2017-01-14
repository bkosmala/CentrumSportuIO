
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
        



    }
}
