using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Wydarzenia")]
    public class Wydarzenie
    {
        [Key]
        public int IdWydarzenia { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Cennik CennikWydarzenia { get; set; }
        public string Kategoria { get; set; }
        public DateTime ProponowanyTerminRozp { get; set; }
        public DateTime ProponowanyTerminZak { get; set; }
    }
}
