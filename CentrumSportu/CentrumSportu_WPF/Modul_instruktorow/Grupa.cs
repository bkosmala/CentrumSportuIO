using CentrumSportu_WPF.Modul_biletow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("Grupy")]
    public class Grupa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  set; }

        public virtual Dyscyplina Dyscyplina { get; set; }

        public int  MinLiczebnosc { get; set; }

        public int MaxLiczebnosc { get; set; }

        public virtual Instruktor Instruktor { get; set; }

        public virtual ICollection<UczestnikZajec> Uczestincy { get; set; }

        public Grupa(Dyscyplina dyscyplina,int min,int max)
        {
            Dyscyplina = dyscyplina;
            MinLiczebnosc = min;
            MaxLiczebnosc = max;
            Uczestincy = new List<UczestnikZajec>();
        }

        public Grupa()
        {

        }

        public void DodajUczestnika(UczestnikZajec uczestnik)
        {
            Uczestincy.Add(uczestnik);
        }

        public void UsunUczestnika(int idUczestnika)
        {
            foreach (var item in Uczestincy)
            {
                if(item.Id==idUczestnika)
                {
                    Uczestincy.Remove(item);
                    return;
                }
            }
        }

        public void ZmienLiczbeUczestnikow(int min,int max)
        {
            MinLiczebnosc = min;
            MaxLiczebnosc = max;
        }
    }
}
