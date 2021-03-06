﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    public abstract class Osoba
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  set; }

        public string Imie { get; private set; }

        public string Nazwisko { get; private set; }

        public Osoba(string imie,string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public Osoba()
        {

        }

    }
}
