using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Przedmiot")]
    public class Przedmiot
    {
        public Przedmiot()
        {
            this.Rezerwacje = new HashSet<Rezerwacja>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(40, ErrorMessage = "Property 'Nazwa' cannot be longer than 40")]
        public String Nazwa { get; set; }
        public Boolean Dostepnosc { get; set; }
        public virtual ICollection<Rezerwacja> Rezerwacje { get; set; }
        public int? CennikId;
        public Cennik Cennik { get; set; }

    }
}
