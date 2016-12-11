using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public abstract class WpisHarmonogram
    {
        public string ID { get; private set; }

        public DateTime DataRozpoczecia { get; private set; }

        public DateTime DataZakonczenia { get; private set; }

        public double DlugoscZajec { get; private set; }

        public ObiektSportowy ObiektSportowy { get; private set; }

        public WpisHarmonogram(DateTime dataRozp,DateTime dataZak,ObiektSportowy obiektSportowy)
        {
            DataRozpoczecia = dataRozp;
            DataZakonczenia = dataZak;
            ObiektSportowy = obiektSportowy;
        }

        public void ZmienDateRozpoczecia(DateTime data)
        {
            DataRozpoczecia = data;
        }

        public void ZmienDateZakonczenia(DateTime data)
        {
            DataZakonczenia = data;
        }

        public void ZmienObiektSportowy(ObiektSportowy obiekt)
        {
            ObiektSportowy = obiekt;
        }
    }
}
