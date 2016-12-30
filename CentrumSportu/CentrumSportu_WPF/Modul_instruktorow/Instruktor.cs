using CentrumSportu_WPF.Modul_biletow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    [Table("Instruktorzy")]
    public class Instruktor :Osoba
    {
        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Zdjęcie { get; set; }

        public virtual ICollection<Dyscyplina> Dyscypliny { get; set; }

        public virtual ICollection<Grupa> Grupy { get; set; }

        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public virtual ICollection<WpisZajecia> WpisZajecia { get; set; }

        public Instruktor(string imie, string nazwisko,string email,string telefon,List<Dyscyplina> dyscypliny,KontoUzytkownika konto) : base(imie, nazwisko)
        {
            Email = email;
            Telefon = telefon;
            Dyscypliny = dyscypliny;
            Grupy = new List<Grupa>();
            KontoUzytkownika = konto;
        }

        public bool ZalozGrupe(Grupa grupa,WpisHarmonogram wpis)
        {
            //TO DO
            return true;
        }

        public Grupa PodgladGrupy(int id)
        {
            return Grupy.FirstOrDefault(i => i.Id == id);
        }

        public bool RozwiazGrupe(int id)
        {
            for (int i = 0; i < Grupy.Count; i++)
            {
                if (Grupy.ElementAt(i).Id == id)
                {
                    Grupy.ToList().RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool ZmienTerminZajec(int idWpisu,DateTime dataRozpoczecia,DateTime dataZakonczenia)
        {
            //TO DO
            return true;
        }

        /// <summary>
        /// Metoda bedzie wysylac email do wszystkich uczestnikow danej grupy
        /// </summary>
        public void WyslijWiadomoscDoUczestnikowDanejGrupy(int idGrupy,string wiadomosc)
        {
            //TO DO - zaimplementowac wysyalnie emaila
        }

        public UczestnikZajec PodgladProfiluUczestnika(int idUczestnika)
        {
            //TO DO
            return null;
        }

        public void DodajDyscypline(Dyscyplina dyscyplina)
        {
            Dyscypliny.Add(dyscyplina);
        }

        public void UsunDyscypline(Dyscyplina dyscyplina)
        {
            Dyscypliny.Remove(dyscyplina);
        }

        public void UsunUczestnikaZGrupy(int idGrupy,int idUczestnika)
        {
                foreach (var item in Grupy)
                {
                    if(item.Id==idGrupy)
                    {
                        item.UsunUczestnika(idUczestnika);
                        return;
                    }
                }          
        }

        public List<UczestnikZajec> PodgladUczestnikowGrupy(int idGrupy)
        {
            foreach (var item in Grupy)
            {
                if (item.Id == idGrupy)
                {
                    return item.Uczestincy.ToList();               
                }
            }
            return null;
        }

        public Instruktor() 
        {

        }


    }
}
