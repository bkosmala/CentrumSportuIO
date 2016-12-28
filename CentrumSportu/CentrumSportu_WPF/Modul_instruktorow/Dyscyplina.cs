using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("Dyscypliny")]
    public class Dyscyplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nazwa { get; set; }

        public Dyscyplina()
        {
            
        }

        public Dyscyplina(string nazwa)
        {
            Nazwa = nazwa;
        }
    }
}
