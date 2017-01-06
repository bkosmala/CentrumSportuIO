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
            List<UczestnikZajec> temp = new List<UczestnikZajec>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Grupa grupa in data.Grupy)
                {
                    if (grupa.Id == idGrupy)
                    {
                        grupa.UsunUczestnika(idUczestnika);
                        temp.AddRange(grupa.Uczestincy);
                    }
                }
                data.SaveChanges();
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

        public static List<UczestnikZajec>ZwrocWszystkichNieStudentow()
        {
            
            using (CentrumContext data = new CentrumContext())
            {
                return data.UczestnicyZajec.Where(s => !data.Studenci.Where(es => es.Id == s.Id).Any()).ToList();
                
            }
        }

        public static List<Administrator> ZwrocWszystkichAdministratorow()
        {

            using (CentrumContext data = new CentrumContext())
            {
                return data.Administratorzy.ToList();

            }
        }
        public static List<Student> ZwrocWszystkichStudentow()
        {

            using (CentrumContext data = new CentrumContext())
            {
                return data.Studenci.ToList();

            }
        }
        public static List<Instruktor> ZwrocWszystkichInstruktorow()
        {

            using (CentrumContext data = new CentrumContext())
            {
                return data.Instruktorzy.ToList();

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

        public static bool DodajStudenta(Student K)
        {

            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    data.KontaUzytkownikow.Add(K.KontoUzytkownika);
                    data.Studenci.Add(K);
                    data.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }


        public static bool DodajAdministratora(Administrator K)
        {

            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    data.KontaUzytkownikow.Add(K.KontoUzytkownika);
                    data.Administratorzy.Add(K);
                    data.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool DodajUczestnika(UczestnikZajec K)
        {

            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    data.KontaUzytkownikow.Add(K.KontoUzytkownika);
                    data.UczestnicyZajec.Add(K);
                    data.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool DodajInstruktora(Instruktor K)
        {

            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    data.KontaUzytkownikow.Add(K.KontoUzytkownika);
                    data.Instruktorzy.Add(K);
                    data.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }
        
        
        
        
        public static bool UsunNieStudenta(UczestnikZajec K)
        {

            // to do

            return false;
        }

        public static bool UsunStudenta(Student K)
        {

            // to do

            return false;
        }


        public static bool UsunAdministratora(Administrator K)
        {

            // to do

            return false;
        }

        public static bool UsunInstruktora(Instruktor K)
        {

            // to do

            return false;
        }

        public static UczestnikZajec ZwrocWybranegoUczestnikaZajec(int idGrupy, int idUczestnika)
        {
            UczestnikZajec uczestnik = null;

            using (CentrumContext data = new CentrumContext())
            {
                foreach (Grupa grupa in data.Grupy)
                {
                    if (grupa.Id == idGrupy)
                        uczestnik = grupa.ZwrocWybranegoUczestnika(idUczestnika);
                }
            }
            return uczestnik;
        }

        public static List<UczestnikZajec> ZwrocWszystkichUczestnikowZajec()
        {
            List<UczestnikZajec> result = new List<UczestnikZajec>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Grupa grupa in data.Grupy)
                {
                     result.AddRange(grupa.Uczestincy);
                }
                return result;
            }
        }
    }
}
