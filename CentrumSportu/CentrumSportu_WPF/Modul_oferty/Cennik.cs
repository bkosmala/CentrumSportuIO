using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    public class Cennik
    {
        private Dictionary<string, float> znizki;


        public int IdCennika { get; }
        public float CenaPodstawowa { get; set; }
        public Dictionary<string,float> Znizki {
            get { return znizki; } }
        public string Waluta { get; set; }
        public string Nazwa { get; set; }
        public DateTime DataUtworzenia { get; }
        public DateTime DataModyfikacji { get; set; }
        public string Opis { get; set; }
    }
}
