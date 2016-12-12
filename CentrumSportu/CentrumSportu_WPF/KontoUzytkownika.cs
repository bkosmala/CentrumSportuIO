﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    [Table("KontaUzytkownikow")]
    public class KontoUzytkownika
    {
        [Key]
        public string Login { get; private set; }

        public string Haslo { get; private set; }

        public RodzajKonta TypKonta { get; private set; }

        public enum RodzajKonta
        {
            Student,
            Instruktor,
            Administrator
        };

        public KontoUzytkownika(string login, string haslo, RodzajKonta konto)
        {
            Login = login;
            Haslo = haslo;
            TypKonta = konto;
        }

        public KontoUzytkownika()
        {
            
        }
    }
}