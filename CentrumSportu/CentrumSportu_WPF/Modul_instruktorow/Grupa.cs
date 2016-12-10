using CentrumSportu_WPF.Modul_biletow;
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

        public string Dyscyplina { get; set; }

        public List<UczestnikZajec> Uczestincy { get; set; }

        public uint  MinLiczebnosc { get; set; }

        public uint MaxLiczebnosc { get; set; }

        public Grupa(string dyscyplina,uint min,uint max)
        {
            Dyscyplina = dyscyplina;
            MinLiczebnosc = min;
            MaxLiczebnosc = max;
            Uczestincy = new List<UczestnikZajec>();
        }

        public void DodajUczestnika(UczestnikZajec uczestnik)
        {
            Uczestincy.Add(uczestnik);
        }

        public void UsunUczestnika(string idUczestnika)
        {
            
        }
    }
}
