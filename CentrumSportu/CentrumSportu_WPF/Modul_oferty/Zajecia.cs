using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Zajecia")]
    public class Zajecia
    {

        private List<Instruktor> instruktorzy;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Typ { get; set; }
        public virtual Dyscyplina Dyscyplina { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public virtual Cennik CennikZajec { get; set; }
        public List<Instruktor> Instruktorzy
        {
            get { return instruktorzy; }
        }
        public Zajecia()
        {

        }
        public Zajecia(string typ, Dyscyplina dyscyplina, string nazwa, string opis, Cennik cennik)
        {
            this.Typ = typ;
            this.Dyscyplina = dyscyplina;
            this.Nazwa = nazwa;
            this.Opis = opis;
            this.CennikZajec = cennik;

        }

        public void DodajInstruktora(Instruktor kto)
        {
            bool nieIstnieje = true;
            // sprawdzenie czy instruktor nie został już dodany
            foreach(Instruktor ins in instruktorzy)
            {
                if (ins.Id.Equals(kto.Id))
                {
                    nieIstnieje = false;
                }

            }

            if(nieIstnieje)
            {
                instruktorzy.Add(kto);
            }
        }

        public void UsunInstruktora(Instruktor kto)
        {
                instruktorzy.Remove(kto);
        }


    }
}
