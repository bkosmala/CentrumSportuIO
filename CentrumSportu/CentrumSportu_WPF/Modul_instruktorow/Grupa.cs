using CentrumSportu_WPF.Modul_biletow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("Grupy")]
    public class Grupa
    {

        public string ID { get; private set; }

        public string Dyscyplina { get; private set; }

        public List<UczestnikZajec> Uczestincy { get; private set; }

        public uint  MinLiczebnosc { get; private set; }

        public uint MaxLiczebnosc { get; private set; }

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
            foreach (var item in Uczestincy)
            {
                if(item.ID==idUczestnika)
                {
                    Uczestincy.Remove(item);
                    return;
                }
            }
        }

        public void ZmienLiczbeUczestnikow(uint min,uint max)
        {
            MinLiczebnosc = min;
            MaxLiczebnosc = max;
        }

        public Grupa()
        {

        }
    }
}
