﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("UczestnicyZajec")]
    public class UczestnikZajec : Osoba
    {
        private ICollection<Bilet> _bilety;
        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Zdjecie { get; set; }

        public UczestnikZajec(string imie, string nazwisko, KontoUzytkownika konto, string email, string telefon):base(imie, nazwisko)
        {
            _bilety = new List<Bilet>();
            KontoUzytkownika = konto;
            Email = email;
            Telefon = telefon;
        }

        public UczestnikZajec(string imie, string nazwisko, KontoUzytkownika konto):base(imie, nazwisko)
        {
            _bilety = new List<Bilet>();
            KontoUzytkownika = konto;
        }


        public UczestnikZajec(string imie, string nazwisko ):base (imie, nazwisko)
        {
            _bilety = new List<Bilet>();
        }

        public UczestnikZajec()
        {
            
        }


        public ICollection<Bilet> Bilety
        {
            get
            {
                return _bilety;
            }

            set
            {
                _bilety = value;
            }
        }


        public void kupBilet(Bilet b)
        {
            _bilety.Add(b);

        }

        public void oddajBilet(Bilet b)
        {
            
            _bilety.Remove(b);
            
        }

        public void zamienBilet(Bilet biletdoOddania, Bilet biletdoKupienia)
        {
            oddajBilet(biletdoOddania);
            kupBilet(biletdoKupienia);
        }

        public UczestnikZajec(string imie, string nazwisko, string email, string telefon) : base(imie, nazwisko)
        {
            _bilety = new List<Bilet>();
            this.Email = email;
            this.Telefon = telefon;
        }

        public Bilet ZwrocWybranyBilet(int idBilet)
        {
            return Bilety.First(e => e.Id == idBilet);
        }

    }
}
