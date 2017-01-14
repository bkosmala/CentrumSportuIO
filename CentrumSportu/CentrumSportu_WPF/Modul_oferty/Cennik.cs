using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Cennik")]
    public class Cennik
    {
        private Dictionary<string, float> znizki;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCennika { get; set; }
        public float CenaPodstawowa { get; set; }
        //public Dictionary<string,float> Znizki {
         //   get { return znizki; } }
        public string Waluta { get; set; }
        public string Nazwa { get; set; }
        //public DateTime DataUtworzenia { get; set; }
       // public DateTime DataModyfikacji { get; set; }
        public string Opis { get; set; }
        
        public Cennik()
        {

        }
        public Cennik(float cenaPodst, string waluta, string nazwa)
        {
            this.CenaPodstawowa = cenaPodst;
            this.Waluta = waluta;
            this.Nazwa = nazwa;
            //this.DataUtworzenia = dataUtw;

        }

        public bool DodajZnizke(string nazwa, float ile)
        {
             if (znizki.ContainsKey(nazwa))
            {
                znizki.Add(nazwa,ile);
                return true;
            }
             return false;
        }

        public void UsunZnizke(string nazwa)
        {
            znizki.Remove(nazwa);
        }
    }
}
