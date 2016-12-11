using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    public class Wydarzenie
    {
        public int IdWydarzenia { get; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Cennik CennikWydarzenia { get; set; }
        public string Kategoria { get; set; }
        public DateTime ProponowanyTerminRozp { get; set; }
        public DateTime ProponowanyTerminZak { get; set; }
    }
}
