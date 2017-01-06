using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_oferty;

namespace CentrumSportu_WPF.Baza_danych
{
    public static class BazaMetody
    {
        public static bool UsunGrupe(string id)
        {
            // TO DO 
            return true;
        }

        public static List<UczestnikZajec> UsunUczestnikaZGrupy(int idGrupy,int idUczestnika)
        {
            //TO DO
            List<UczestnikZajec> temp = new List<UczestnikZajec>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Grupa grupa in data.Grupy)
                {
                    if(grupa.Id == idGrupy)
                    {
                        grupa.UsunUczestnika(idUczestnika);
                        temp.AddRange(grupa.Uczestincy);
                    }
                }
                return temp;
            }
        }

        public static KontoUzytkownika SprawdzLoginiHaslo(string login, string haslo)
        {
            using (CentrumContext data=new CentrumContext())
            {
                foreach (var item in data.KontaUzytkownikow)
                {
                    if (item.Login == login && item.Haslo == login)
                        return item;
                }
            }
            return null;
        }

        public static Instruktor ZwrocInstruktora(KontoUzytkownika konto)
        {
            using (CentrumContext data=new CentrumContext())
            {
                foreach (var item in data.Instruktorzy.Include("Dyscypliny").Include("Grupy"))
                {
                    if (item.KontoUzytkownika.Login == konto.Login)
                        return item;
                }
            }
            return null;
        }

        public static Administrator ZwrocAdministratora(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Administratorzy)
                {
                    if (item.KontoUzytkownika.Login == konto.Login)
                        return item;
                }
            }
            return null;
        }

        public static Student ZwrocStudenta(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Studenci)
                {
                    if (item.KontoUzytkownika.Login == konto.Login)
                        return item;
                }
            }
            return null;
        }

        public static WpisZajecia ZwrocNajblizszeZajeciaDlaInstruktora(Instruktor instruktor)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var result =
                    data.WpisyZajecia.Where(e=>e.Instruktor.Id==instruktor.Id && e.DataRozpoczecia > DateTime.Now)
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy")
                        .FirstOrDefault();
              
                return result;
            }
        }

        public static List<WpisZajecia> ZwrocWszystkieZajeciaDlaInstruktora(Instruktor instruktor)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var result =
                    data.WpisyZajecia.Where(e => e.Instruktor.Id == instruktor.Id && e.DataRozpoczecia > DateTime.Now)
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy").Include("Grupa").ToList();
                       
                return result;
            }
        }

        public static List<WpisZajecia> ZwrocWszystkieZajeciaDlaInstruktoraiDanejGrupy(Instruktor instruktor,int idGrupy)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var result =
                    data.WpisyZajecia.Where(e => e.Instruktor.Id == instruktor.Id && e.DataRozpoczecia > DateTime.Now && e.Grupa.Id==idGrupy)
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy").Include("Grupa").ToList();

                return result;
            }
        }

        public static List<UczestnikZajec> ZwrocUczestnikowZajecDlaDanejGrupy(int idGrupy)
        {
            List<UczestnikZajec> result = new List<UczestnikZajec>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Grupa grupa in data.Grupy)
                {
                    if (grupa.Id == idGrupy)
                        result.AddRange(grupa.Uczestincy);
                }
                return result;
            }
        }

        public static List<Zajecia> ZwrocWszystkieZajeciaOferta()
        {
            using (CentrumContext data = new CentrumContext())
            {
                var query = (from z in data.Zajecia
                             select z).ToList();
                return query;
            }
        }

    }
}
