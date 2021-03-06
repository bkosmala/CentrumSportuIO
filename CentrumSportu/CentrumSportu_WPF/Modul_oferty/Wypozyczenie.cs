﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_instruktorow;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Wypozyczenie")]
    public class Wypozyczenie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdWypozyczenia { get; set; }
        public DateTime DataRozpoczecia { get; set; }
        public DateTime? DataZwrotu { get; set; }
        public int? WydawcaSprzetuId { get; set; }
        public Pracownik WydawcaSprzetu { get; set; }

        public Wypozyczenie() { }
        public Wypozyczenie(DateTime od, Pracownik pracownik)
        {
            DataRozpoczecia = od;
            WydawcaSprzetu = pracownik;
        }
        
    }
}
