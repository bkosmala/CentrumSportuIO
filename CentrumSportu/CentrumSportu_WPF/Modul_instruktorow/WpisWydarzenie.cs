using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("WpisyWydarzenia")]
    public class WpisWydarzenie : WpisHarmonogram
    {
        public Wydarzenie Wydarzenie { get; private set; }

        public WpisWydarzenie(DateTime dataRozp, DateTime dataZak, ObiektSportowy obiektSportowy,Wydarzenie wydarzenie) : base(dataRozp, dataZak, obiektSportowy)
        {
            Wydarzenie = wydarzenie;
        }
    }
}
