using System;
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
        private List<Bilet> _bilety;

        public UczestnikZajec(string id ,string imie, string nazwisko,KontoUzytkownika konto):base ( id,imie, nazwisko,konto)
        {
            _bilety = new List<Bilet>();
        }


        public List<Bilet> bilety
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
            // Zajecia: potrzebuje daty i liczby osob
        }

        public void zamienBilet(Bilet biletdoOddania, Bilet biletdoKupienia)
        {
            oddajBilet(biletdoOddania);
            kupBilet(biletdoKupienia);
        }


    }
}
