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
        public DateTime OdDaty { get; set; }
        public DateTime DoDaty { get; set; }
        public Boolean CzyOplacone { get; private set; }
        
        public int? WypozyczenieId;
        public Wypozyczenie Wypozyczenie { get; private set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public int KlientId { get; set; }
        public Osoba Klient { get; set; }
        public StatusRezerwacji Status { get; set; }

        public enum StatusRezerwacji
        {
            OCZEKUJACA,
            ZREALIZOWANA,
            ANULOWANA
        };

        public void RezerwujPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Add(przedmiot);
        }

    }
}
