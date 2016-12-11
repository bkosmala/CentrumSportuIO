using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Modul_oferty
{
    class Wypozyczenie
    {
        public int IdWypozyczenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime DataZwrotu { get; private set; }
        public Instruktor WydawcaSprzetu { get; set; }
        
    }
}
