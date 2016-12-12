using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Rezerwacje")]
    public class Rezerwacja
    {
        [Key]
        public int Id { get; set; }
        public DateTime OdDaty { get; private set; }
        public DateTime DoDaty { get; private set; }
        public Boolean CzyOplacone { get; private set; }
        public Wypozyczenie Wypozyczenie { get; private set; }
        public List<Przedmiot> Przedmioty { get; set; }

    }
}
