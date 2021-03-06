﻿using CentrumSportu_WPF.Modul_oferty;
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
        public virtual Wydarzenie Wydarzenie { get; set; }

        public virtual ObiektSportowy ObiektSportowy { get; set; }

        public WpisWydarzenie(DateTime dataRozp, DateTime dataZak, ObiektSportowy obiektSportowy,Wydarzenie wydarzenie) : base(dataRozp, dataZak)
        {
            Wydarzenie = wydarzenie;
            ObiektSportowy = obiektSportowy;
        }
    }
}
