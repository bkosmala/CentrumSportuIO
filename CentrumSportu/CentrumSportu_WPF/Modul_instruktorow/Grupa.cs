using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public class Grupa
    {
        public string ID { get; set; }

        public List<UczestnikZajec> Uczestnicy { get; set; }

        public void UsunUczestnika(string idUczestnika)
        {
            //TO DO 
        }
    }
}
