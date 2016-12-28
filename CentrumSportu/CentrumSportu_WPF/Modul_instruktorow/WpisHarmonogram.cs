using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public abstract class WpisHarmonogram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  set; }

        public DateTime DataRozpoczecia { get; private set; }

        public DateTime DataZakonczenia { get; private set; }

        public double DlugoscZajec { get; private set; }    

        public WpisHarmonogram(DateTime dataRozp,DateTime dataZak)
        {
            DataRozpoczecia = dataRozp;
            DataZakonczenia = dataZak;
        }

        public void ZmienDateRozpoczecia(DateTime data)
        {
            DataRozpoczecia = data;
        }

        public void ZmienDateZakonczenia(DateTime data)
        {
            DataZakonczenia = data;
        }

        public WpisHarmonogram()
        {

        }
    }
}
