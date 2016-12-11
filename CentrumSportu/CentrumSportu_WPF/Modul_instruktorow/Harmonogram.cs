using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public class Harmonogram
    {
        public List<WpisHarmonogram> Wpisy { get; set; }

        public DateTime MinGodzina { get; set; }

        public DateTime MaxGodzina { get; set; }

        public Harmonogram(DateTime minGodzina,DateTime maxGodzina )
        {
            MinGodzina = minGodzina;
            MaxGodzina = maxGodzina;
        }

        public bool DodajWpis(WpisHarmonogram wpis)
        {
            //TO DO
            return true;
        }

        public bool UsunWpis(string idWpisu)
        {
            //TO DO
            return true;
        }
    }
}
