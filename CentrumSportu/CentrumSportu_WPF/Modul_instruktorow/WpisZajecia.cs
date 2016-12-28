﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("WpisyZajecia")]
    public class WpisZajecia : WpisHarmonogram
    {
        public virtual Instruktor Instruktor { get; set; }

        public virtual Grupa Grupa { get; set; }

        public virtual ObiektSportowy ObiektSportowy { get; set; }

        public WpisZajecia(DateTime dataRozp, DateTime dataZak, ObiektSportowy obiektSportowy,Instruktor instruktor,Grupa grupa) : base(dataRozp, dataZak)
        {
            Instruktor = instruktor;
            Grupa = grupa;
            ObiektSportowy = obiektSportowy;
        }

        public WpisZajecia()
        {

        }

        public void ZmienInstruktora(Instruktor instruktor)
        {
            Instruktor = instruktor;
        }      
    }
}
