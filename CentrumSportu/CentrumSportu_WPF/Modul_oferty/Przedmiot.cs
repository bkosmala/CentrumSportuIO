using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    [Table("Przedmioty")]
    public class Przedmiot
    {
        [Key]
        public int Id { get; set; }
        public String Nazwa { get; set; }
        public Boolean Dostepnosc { get; set; }

    }
}
