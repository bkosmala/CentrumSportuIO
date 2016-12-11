using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    class Rezerwacja
    {
        public int Id { get; }
        public DateTime OdDaty { get; private set; }
        public DateTime DoDaty { get; private set; }
        public Boolean CzyOplacone { get; private set; }
        public Wypozyczenie Wypozyczenie { get; private set; }
        public List<Przedmiot> Przedmioty { get; set; }

    }
}
