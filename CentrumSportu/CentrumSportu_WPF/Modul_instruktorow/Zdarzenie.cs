using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public class Zdarzenie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public RodzajZdarzenia TypZdarzenia { get; set; }

        public string Message { get; set; }

        public virtual WpisZajecia WpisZajecia { get; set; }

        public virtual Instruktor Instruktor { get; set; }

        public enum RodzajZdarzenia
        {
            Zastepstwo,
            Komunikat
        };

        public Zdarzenie()
        {
            
        }

        public Zdarzenie(RodzajZdarzenia rodzaj, string message)
        {
            TypZdarzenia = rodzaj;
            Message = message;
        }
    }
}
