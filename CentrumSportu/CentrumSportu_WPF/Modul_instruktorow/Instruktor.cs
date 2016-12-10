using CentrumSportu_WPF.Modul_biletow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_instruktorow
{
    public class Instruktor :Osoba
    {      
        public List<string> Dyscypliny { get; private set; }

        public List<Grupa> Grupy { get; private set; }

        public Instruktor(string imie, string nazwisko,List<string> dyscypliny) : base(imie, nazwisko)
        {
            Dyscypliny = dyscypliny;
            Grupy = new List<Grupa>();
        }

        public bool ZalozGrupe(Grupa grupa,WpisHarmonogram wpis)
        {
            //TO DO
            return true;
        }

        public Grupa PodgladGrupy(string id)
        {
            return Grupy.Where(i => i.ID == id).FirstOrDefault();
        }

        public bool RozwiazGrupe(string id)
        {
            for (int i = 0; i < Grupy.Count; i++)
            {
                if (Grupy[i].ID == id)
                {
                    Grupy.RemoveAt(i);
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
                    return item.Uczestincy;               
                }
            }
            return null;
        }




    }
}
