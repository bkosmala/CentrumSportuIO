using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Baza_danych
{
    public static class BazaMetody
    {
        public static bool UsunGrupe(string id)
        {
            // TO DO 
            return true;
        }

        public static bool UsunUczestnikaZGrupy(string idGrupy,string idUczestnika)
        {
            //TO DO
            return true;
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
                foreach (var item in data.Instruktorzy)
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
    }
}
