using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Rezerwacja")]
    public class Rezerwacja
    {
        public Rezerwacja()
        {
            this.Przedmioty = new HashSet<Przedmiot>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime OdDaty { get; private set; }
        public DateTime DoDaty { get; private set; }
        public Boolean CzyOplacone { get; private set; }
        
        public int? WypozyczenieId;
        public Wypozyczenie Wypozyczenie { get; private set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        [Range(1,3), Display(Name = "StatusRezerwacjiEnum")]
        public StatusRezerwacji status;

        public enum StatusRezerwacji
        {
            OCZEKUJACA = 1,
            ZREALIZOWANA = 2,
            ANULOWANA= 3
        };

        public void RezerwujPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Add(przedmiot);
        }

    }
}
