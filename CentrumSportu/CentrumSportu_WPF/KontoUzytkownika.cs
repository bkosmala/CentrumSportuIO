using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    [Table("KontaUzytkownikow")]
    public class KontoUzytkownika
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Login { get;  set; }

        public string Haslo { get; private set; }

        public RodzajKonta TypKonta { get; private set; }

        public virtual Osoba Osoba { get; set; }

        public enum RodzajKonta
        {
            Student,
            Instruktor,
            Administrator,
            NieStudent
        };

        public KontoUzytkownika(string login, string haslo, RodzajKonta konto)
        {
            Login = login;
            Haslo = haslo;
            TypKonta = konto;
        }

        public KontoUzytkownika()
        {
            
        }
    }
}
