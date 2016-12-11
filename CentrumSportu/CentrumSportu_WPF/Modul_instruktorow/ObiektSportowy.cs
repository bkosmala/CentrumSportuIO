using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public class ObiektSportowy
    {
        public string ID { get; private set; }

        public string Nazwa { get; private set; }

        public List<string> DostepneDyscypliny { get; private set; }

        public uint MaxUczestnikow { get; private set; }

        public uint IloscMiejscTrybuny { get; private set; }

        public ObiektSportowy(string id,string nazwa,List<string> dyscypliny,uint maxUczestnicy,uint trybuny)
        {
            ID = id;
            Nazwa = nazwa;
            DostepneDyscypliny = dyscypliny;
            MaxUczestnikow = maxUczestnicy;
            IloscMiejscTrybuny = trybuny;
        }

        public void DodajDyscypline(string dyscyplina)
        {
            DostepneDyscypliny.Add(dyscyplina);
        }

        public void UsunDyscypline(string dyscyplina)
        {
            DostepneDyscypliny.Remove(dyscyplina);
        }

        public void ZmienIloscUczestnikow(uint ilosc)
        {
            MaxUczestnikow = ilosc;
        }
    }
}
