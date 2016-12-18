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

        public List<string> Dyscypliny { get; private set; }

        public virtual ICollection<Grupa> Grupy { get; set; }

        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public Instruktor(string id ,string imie, string nazwisko,string email,string telefon,List<string> dyscypliny,KontoUzytkownika konto) : base(id,imie, nazwisko)
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

        public Grupa PodgladGrupy(string id)
        {
            return Grupy.FirstOrDefault(i => i.ID == id);
        }

        public bool RozwiazGrupe(string id)
        {
            for (int i = 0; i < Grupy.Count; i++)
            {
                if (Grupy.ElementAt(i).ID == id)
                {
                    Grupy.ToList().RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public bool ZmienTerminZajec(string idWpisu,DateTime dataRozpoczecia,DateTime dataZakonczenia)
        {
            //TO DO
            return true;
        }

        /// <summary>
        /// Metoda bedzie wysylac email do wszystkich uczestnikow danej grupy
        /// </summary>
        public void WyslijWiadomoscDoUczestnikowDanejGrupy(string idGrupy,string wiadomosc)
        {
            //TO DO - zaimplementowac wysyalnie emaila
        }

        public UczestnikZajec PodgladProfiluUczestnika(string idUczestnika)
        {
            //TO DO
            return null;
        }

        public void DodajDyscypline(string nazwa)
        {
            Dyscypliny.Add(nazwa);
        }

        public void UsunDyscypline(string nazwa)
        {
            Dyscypliny.Remove(nazwa);
        }

        public void UsunUczestnikaZGrupy(string idGrupy,string idUczestnika)
        {
                foreach (var item in Grupy)
                {
                    if(item.ID==idGrupy)
                    {
                        item.UsunUczestnika(idUczestnika);
                        return;
                    }
                }          
        }

        public List<UczestnikZajec> PodgladUczestnikowGrupy(string idGrupy)
        {
            foreach (var item in Grupy)
            {
                if (item.ID == idGrupy)
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
