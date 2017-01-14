using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("ObiektySportowe")]
    public class ObiektSportowy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nazwa { get; set; }

        public virtual ICollection<Dyscyplina> DostepneDyscypliny { get; set; }

        public int MaxUczestnikow { get; set; }

        public int IloscMiejscTrybuny { get; set; }

        public string Opis { get; set; }

        public string Zdjecie { get; set; }

        public ObiektSportowy(string nazwa,List<Dyscyplina> dyscypliny,int maxUczestnicy,int trybuny )
        {
            Nazwa = nazwa;
            DostepneDyscypliny = dyscypliny;
            MaxUczestnikow = maxUczestnicy;
            IloscMiejscTrybuny = trybuny;
        }

        public ObiektSportowy()
        {

        }

        public void DodajDyscypline(Dyscyplina dyscyplina)
        {
            DostepneDyscypliny.Add(dyscyplina);
        }

        public void UsunDyscypline(Dyscyplina dyscyplina)
        {
            DostepneDyscypliny.Remove(dyscyplina);
        }

        public void ZmienIloscUczestnikow(int ilosc)
        {
            MaxUczestnikow = ilosc;
        }      
    }
}
