﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_oferty;
using System.Data.Entity.Core.Objects;

namespace CentrumSportu_WPF.Baza_danych
{
    public static class BazaMetody
    {
        public static List<Grupa> Usungrupe(int idInstruktor, int idgrupy)
        {
            List<Grupa> temp = new List<Grupa>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (WpisZajecia wpisZajecia in data.WpisyZajecia)
                {
                    if (wpisZajecia.Grupa.Id == idgrupy)
                    {
                        data.WpisyZajecia.Remove(wpisZajecia);
                    }
                }
                foreach (Instruktor instruktor in data.Instruktorzy)
                {
                    if (instruktor.Id == idInstruktor)
                    {
                        instruktor.UsunGrupe(idgrupy);
                        temp.AddRange(instruktor.Grupy);
                    }
                }
                data.SaveChanges();
                return temp;
            }
        }


        internal static List<Rezerwacja> ZwrocRezerwacjeWedlugStatusu(Rezerwacja.StatusRezerwacji status)
        {
            using (CentrumContext data = new CentrumContext())
            {
                return data.Rezerwacje.Include(x => x.Klient).Include(x => x.Przedmioty).Include(x => x.Wypozyczenie).Include(x => x.Wypozyczenie.WydawcaSprzetu).Where(x => x.Status == status).ToList();
            }
        }

        //takie, ktore juz moga zostac wypozyczone - REALIZOWANE
        internal static List<Rezerwacja> ZwrocRezerwacjeAktywne()
        {
            int maksRoznicaRozpoczeciaWypozyczenia = 10;
            using (CentrumContext data = new CentrumContext())
            {
                return data.Rezerwacje.Include("Przedmioty").Include(p => p.Wypozyczenie).Include(t => t.Klient)
                    .Where(x => Math.Abs(DbFunctions.DiffMinutes(x.OdDaty, DateTime.Now) ?? maksRoznicaRozpoczeciaWypozyczenia + 1) < maksRoznicaRozpoczeciaWypozyczenia
                            && x.Status == Rezerwacja.StatusRezerwacji.OCZEKUJACA).ToList();
            }
        }


        public static List<UczestnikZajec> UsunUczestnikaZGrupy(int idGrupy, int idUczestnika)
        {
            //TO DO
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


        internal static void AktualizujRezerwacje(Rezerwacja zaktualizowanaRezerwacja)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.Rezerwacje.AddOrUpdate(zaktualizowanaRezerwacja);
                var rezerwacjaPrzed = data.Rezerwacje.Include(p => p.Wypozyczenie).Where(p => p.Id == zaktualizowanaRezerwacja.Id).FirstOrDefault();
                rezerwacjaPrzed.Wypozyczenie = zaktualizowanaRezerwacja.Wypozyczenie;
                data.SaveChanges();
            }
        }

        public static KontoUzytkownika SprawdzLoginiHaslo(string login, string haslo)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in
                    data.KontaUzytkownikow)
                {
                    if (item.Login == login && item.Haslo == login)
                        return item;
                }
            }
            return null;
        }

        public static Instruktor ZwrocInstruktora(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Instruktorzy.Include("Dyscypliny").Include("Grupy").Include("Zdarzenia").Include("Zdarzenia.WpisZajecia.Instruktor"))
                {
                    if (item.KontoUzytkownika.Login == konto.Login)
                        return item;
                }
            }
            return null;
        }

        internal static Pracownik ZwrocPracownika(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Pracownicy)
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

        internal static List<Przedmiot> ZwrocDostepnePrzedmiotyWTerminie(DateTime startDate, DateTime endDate)
        {
            using (CentrumContext context = new CentrumContext())
            {
                var res = from przedmiot in context.Przedmioty
                          where przedmiot.Rezerwacje.All(r => r.Status == Rezerwacja.StatusRezerwacji.ANULOWANA ? true : (r.OdDaty > endDate || r.DoDaty < startDate))
                          select przedmiot;
                return res.ToList();
            }
        }

        internal static void AnulujRezerwacje(Rezerwacja r)
        {
            using (CentrumContext context = new CentrumContext())
            {
                context.Rezerwacje.AddOrUpdate(r);
                var rezerwacjaPrzed = context.Rezerwacje.FirstOrDefault(x => x.Id == r.Id);
                rezerwacjaPrzed.Status = Rezerwacja.StatusRezerwacji.ANULOWANA;
                context.SaveChanges();
            }
        }

        public static Student ZwrocStudenta(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Studenci.Include("Grupa"))
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
                    data.WpisyZajecia.Where(e => e.Instruktor.Id == instruktor.Id && e.DataRozpoczecia > DateTime.Now)
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy").Include("Grupa")
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
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy").Include("Grupa").Include("Grupa.Dyscyplina").Include("Instruktor").ToList();

                return result;
            }
        }

        public static List<WpisZajecia> ZwrocWszystkieZajeciaDlaInstruktoraiDanejGrupy(Instruktor instruktor, int idGrupy)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var result =
                    data.WpisyZajecia.Where(e => e.Instruktor.Id == instruktor.Id && e.DataRozpoczecia > DateTime.Now && e.Grupa.Id == idGrupy)
                        .OrderBy(e => e.DataRozpoczecia).Include("ObiektSportowy").Include("Grupa").Include("Grupa.Dyscyplina").Include("Instruktor").ToList();

                return result;
            }
        }

        internal static void UtworzNowaRezerwacje(Rezerwacja rezerwacja)
        {
            using (CentrumContext context = new CentrumContext())
            {
                context.Rezerwacje.Add(rezerwacja);
                context.SaveChanges();
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

        public static List<UczestnikZajec> ZwrocWszystkichNieStudentow()
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

            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    var entry = data.Entry(K);
                    if (entry.State == EntityState.Detached)
                        data.UczestnicyZajec.Attach(K);
                    data.UczestnicyZajec.Remove(K);
                    data.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static bool UsunStudenta(Student K)
        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {

                    foreach (Student s in data.Studenci)
                        if (s.Id == K.Id)
                        {
                            foreach (Bilet b in data.Bilety)
                            {
                                if (b.Uczestnik.Id == s.Id)
                                {
                                    data.Bilety.Remove(b);
                                }
                            }

                            data.Studenci.Remove(s);


                        }
                    data.SaveChanges();
                }
                catch(Exception e)
                {
                    return false;
                }

            }
            return true;
        }


        public static bool UsunAdministratora(Administrator K)
        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    var entry = data.Entry(K);
                    if (entry.State == EntityState.Detached)
                        data.Administratorzy.Attach(K);
                    data.Administratorzy.Remove(K);
                    data.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static bool UsunInstruktora(Instruktor K)
        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                  foreach(Instruktor i in data.Instruktorzy)
                        if(i.Id == K.Id)
                        {
                            i.Dyscypliny.Clear();
                            foreach(Grupa g in data.Grupy)                            
                                if(g.Instruktor.Id==i.Id)
                                {
                                    g.Uczestincy.Clear();
                                    g.Wpisy.Clear();
                                    data.Grupy.Remove(g);
                                }
                            data.Instruktorzy.Remove(i);

                }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }


        public static List<ObiektSportowy> ZwrocListeObiektowDlaDanejDyscypliny(Dyscyplina dyscyplina)
        {
            List<ObiektSportowy> lista = new List<ObiektSportowy>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.ObiektySportowe)
                {
                    foreach (var d in item.DostepneDyscypliny)
                    {
                        if (d.Nazwa == dyscyplina.Nazwa)
                        {
                            lista.Add(item);
                            break;
                        }
                    }
                }
            }
            return lista;
        }

        public static bool SprawdzCzyJestWolnyTerminDlaDanegoObiektu(ObiektSportowy obiekt, DateTime dataRozp, DateTime dataZakon)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var termin =
                    data.WpisyZajecia.FirstOrDefault(
                        x =>
                            x.ObiektSportowy.Nazwa == obiekt.Nazwa && ((dataRozp >= x.DataRozpoczecia &&
                            dataRozp < x.DataZakonczenia) || (dataZakon > x.DataRozpoczecia &&
                            dataZakon <= x.DataZakonczenia)));
                if (termin == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void DodajNowyTermin(WpisZajecia wpis)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.Instruktorzy.Attach(wpis.Instruktor);
                var temp = data.Dyscypliny.ToList();
                foreach (var item in data.Dyscypliny)
                {
                    data.Entry(item).State = EntityState.Detached;
                }
                if (data.Grupy.FirstOrDefault(x => x.Id == wpis.Grupa.Id) != null)
                {
                    data.Grupy.Attach(wpis.Grupa);
                }
                data.ObiektySportowe.Attach(wpis.ObiektSportowy);

                data.WpisyZajecia.Add(wpis);
                data.SaveChanges();
            }
        }

        public static void DodajNowyTerminDoInstniejacejGrupy(WpisZajecia wpis, Grupa grupa)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.ObiektySportowe.Attach(wpis.ObiektSportowy);
                foreach (var item in data.Dyscypliny)
                {
                    data.Entry(item).State = EntityState.Detached;
                }
                data.Instruktorzy.Attach(wpis.Instruktor);
                var temp = data.Grupy.FirstOrDefault(x => x.Id == grupa.Id);
                temp.Wpisy.Add(wpis);
                data.SaveChanges();
            }
        }

        public static List<Zajecia> ZwrocWszystkieZajeciaOferta()
        {
            using (CentrumContext data = new CentrumContext())
            {
                var query = (from z in data.Zajecia.Include("Dyscyplina").Include("CennikZajec")
                             select z).ToList();
                return query;
            }
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

        public static Grupa ZwrocGrupeDlaWybranegoInstruktora(int idInstruktora, string nazwaGrupy)
        {
            Grupa temp = null;

            using (CentrumContext data = new CentrumContext())
            {
                foreach (Instruktor instruktor in data.Instruktorzy.Include("Grupy.Dyscyplina"))
                {
                    if (instruktor.Id == idInstruktora)
                        temp = instruktor.PodgladGrupy(nazwaGrupy);
                }
            }
            return temp;
        }

        public static void UsunTerminZajec(int wpisId)
        {
            try
            {


                using (CentrumContext data = new CentrumContext())
                {
                    var result = data.WpisyZajecia.FirstOrDefault(x => x.Id == wpisId);
                    data.WpisyZajecia.Remove(result);
                    data.SaveChanges();
                }
            }
            catch(Exception)
            {

            }
        }

        //OFERTA
        public static List<Przedmiot> ZwrocWszystkiePrzedmioty()
        {
            using (CentrumContext context = new CentrumContext())
            {
                return context.Przedmioty.ToList();
            }
        }

        public static List<Rezerwacja> ZwrocRezerwacjeStudenta(int id)
        {
            using (CentrumContext context = new CentrumContext())
            {
                var rezerwacjeKlienta = context.Rezerwacje.Include("Przedmioty").Where(p => p.KlientId == id);
                return rezerwacjeKlienta.ToList();
            }
        }

        internal static List<Rezerwacja> PobierzRezerwacjeStudentaWedlugStatusu(int id, Rezerwacja.StatusRezerwacji status)
        {
            using (CentrumContext context = new CentrumContext())
            {
                return context.Rezerwacje.Include("Przedmioty").Where(r => r.Status == status && r.KlientId == id).ToList();
            }
        }

        public static List<Dyscyplina> ZwrocWszystkieDyscypliny()
        {
            using (CentrumContext data = new CentrumContext())
            {

                return data.Dyscypliny.ToList();


            }
        }

        public static List<ObiektSportowy> ZwrocWszystkieObiektySportoweOferta()
        {
            using (CentrumContext data = new CentrumContext())
            {
                var query = (from z in data.ObiektySportowe
                             select z).ToList();
                return query;
            }
        }

        public static void AktualizujWpisZajec(WpisZajecia wpis)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.WpisyZajecia.AddOrUpdate(wpis);
                var wpisStary = data.WpisyZajecia.FirstOrDefault(x => x.Id == wpis.Id);
                var obiekt = data.ObiektySportowe.FirstOrDefault(x => x.Id == wpis.ObiektSportowy.Id);
                wpisStary.ObiektSportowy = obiekt;
                data.SaveChanges();
            }
        }

        public static void AktualizujInstruktoraWpisuZajec(WpisZajecia wpis)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var wpisStary = data.WpisyZajecia.FirstOrDefault(x => x.Id == wpis.Id);
                var inst = data.Instruktorzy.FirstOrDefault(x => x.Id == wpis.Instruktor.Id);
                wpisStary.Instruktor = inst;
                data.SaveChanges();
            }
        }

        public static List<Instruktor> ZwrocWszystkichInstrutorowDlaDanejDyscypliny(Dyscyplina dyscyplina)
        {
            List<Instruktor> lista = new List<Instruktor>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Instruktorzy.Include("Zdarzenia"))
                {
                    var temp = item.Dyscypliny.FirstOrDefault(x => x.Nazwa == dyscyplina.Nazwa);
                    if (temp == null)
                        continue;
                    else
                        lista.Add(item);
                }
            }
            return lista;
        }

        public static void DodajZdarzenieDoInstruktora(Zdarzenie zdarzenie)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.WpisyZajecia.Attach(zdarzenie.WpisZajecia);
                foreach (var item in data.Dyscypliny)
                {
                    data.Entry(item).State = EntityState.Detached;
                }
                foreach (var item in data.Instruktorzy)
                {
                    data.Entry(item).State = EntityState.Detached;
                }
                data.Instruktorzy.Attach(zdarzenie.Instruktor);
                data.Zdarzenia.Add(zdarzenie);
                data.SaveChanges();
            }
        }

        public static void UsunZdarzeniaDlaInstruktora(Instruktor instruktor)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var inst = data.Instruktorzy.FirstOrDefault(x => x.Id == instruktor.Id);
                inst.Zdarzenia.Clear();
                data.SaveChanges();
            }
        }

        public static List<Instruktor> ZwrocListeInstruktorowDlaDanejDyscypliny(Dyscyplina dyscyplina)
        {
            List<Instruktor> lista = new List<Instruktor>();
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.Instruktorzy)
                {
                    foreach (var d in item.Dyscypliny)
                    {
                        if (d.Nazwa == dyscyplina.Nazwa)
                        {
                            lista.Add(item);
                            break;
                        }
                    }
                }
            }
            return lista;
        }



        public static List<Bilet> ZwrocBiletyUzytkownika(UczestnikZajec u)
        {
            using (CentrumContext data = new CentrumContext())
            {
                return data.Bilety.Include("Zajecia").Include("Zajecia.Zajecia").Include("Zajecia.Zajecia.Dyscyplina").Include("Zajecia.Grupa").Include("Zajecia.Grupa.Uczestincy").Where(r => r.Uczestnik.Id == u.Id).ToList();


            }
        }

        public static bool UsunBilet(Bilet bilet)
        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    var b = data.Bilety.FirstOrDefault(r => r.Id == bilet.Id);
                    data.Bilety.Remove(b);
                    data.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            return true;

        }

        

        public static bool DodajObiektSportowy(ObiektSportowy obiekt)

        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    List<Dyscyplina> temp = new List<Dyscyplina>();
                    foreach (Dyscyplina dyscyplina in obiekt.DostepneDyscypliny)
                    {
                        temp.Add(data.Dyscypliny.FirstOrDefault(x => x.Id == dyscyplina.Id));
                    }
                    obiekt.DostepneDyscypliny = temp;
                    data.ObiektySportowe.Add(obiekt);
                    data.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public static bool DodajPrzedmiot(Przedmiot przedmiot)
        {
            using (CentrumContext data = new CentrumContext())
            {
                try
                {
                    data.Przedmioty.Add(przedmiot);
                    data.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }
                return true;
            }


        public static List<Grupa> ZwrocWszystkieGrupy()
        {
            using (CentrumContext data = new CentrumContext())
            {
                List<Grupa> lista = new List<Grupa>();

                foreach(Grupa g in data.Grupy.Include("Uczestincy"))
                {
                    int licznik=0;
                    foreach(WpisZajecia wz in data.WpisyZajecia)
                    {
                        if (wz.Grupa.Id == g.Id)
                            licznik++;
                    }

                    if (licznik > 1 && g.MaxLiczebnosc > g.Uczestincy.Count + 1)
                        lista.Add(g);
                }

                return lista;
            }

        }


        public static List<WpisZajecia> ZwrocWszystkieGrupBiletowy()
        {
            using (CentrumContext data = new CentrumContext())
            {
                List<WpisZajecia> lista = new List<WpisZajecia>();

                foreach (Grupa g in data.Grupy)
                {
                    int licznik = 0;
                    foreach (WpisZajecia wz in data.WpisyZajecia)
                    {
                        if (wz.Grupa.Id == g.Id)
                            licznik++;
                    }

                    if (licznik == 1 && g.MaxLiczebnosc > g.Uczestincy.Count + 1)
                        foreach (WpisZajecia wz in data.WpisyZajecia.Include("Grupa").Include("Grupa.Dyscyplina"))
                        {
                            if (wz.Grupa.Id == g.Id)
                                lista.Add(wz);
                        }


                }

                return lista;
            }

        }

        public static void ZmienGrupe(Grupa g, Student s)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Student ss in data.Studenci)
                    if (ss.Id == s.Id)
                        foreach (Grupa gg in data.Grupy)
                            if (gg.Id == g.Id)
                            {
                                ss.Grupa = gg;
                                gg.Uczestincy.Add(ss);
                            }
                                

                data.SaveChanges();
            }
        }

        public static void OdejdzZGrupu(Student s, int g)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (Student ss in data.Studenci)
                    if (ss.Id == s.Id)
                    {
                        ss.Grupa = null;
                        foreach (Grupa gg in data.Grupy)
                            if (gg.Id == g)
                                gg.Uczestincy.Remove(ss);
                    }


                data.SaveChanges();
            }
        }




        public static void dodajBilet(UczestnikZajec u, int g)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (WpisZajecia wz in data.WpisyZajecia)
                    if (wz.Grupa.Id == g)
                    {
                        foreach(UczestnikZajec uu in data.UczestnicyZajec)
                        {
                            if(uu.Id==u.Id)
                            {
                                Bilet b = new Bilet(wz, uu);
                                data.Bilety.Add(b);

                            }
                        }

                    }
                data.SaveChanges();
            }
            
                  
        }

        public static UczestnikZajec ZwrocUczestnika(KontoUzytkownika konto)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (var item in data.UczestnicyZajec)
                {
                    if (item.KontoUzytkownika.Login == konto.Login)
                        return item;
                }
            }
            return null;
        }

        public static bool UsunPrzedmiot(Przedmiot przedmiot)
        {
            using (CentrumContext data = new CentrumContext())
            {
                var entry = data.Entry(przedmiot);
                if (entry.State == EntityState.Detached)
                    data.Przedmioty.Attach(przedmiot);
                data.Przedmioty.Remove(przedmiot);

                try
                {
                    foreach (Rezerwacja rezerwacja in data.Rezerwacje)
                    {
                        foreach (Przedmiot p in rezerwacja.Przedmioty)
                        {
                            if (przedmiot.Id == p.Id)
                            {
                                data.Rezerwacje.Remove(rezerwacja);
                            }
                            break;
                        }
                        break;
                    }
                    data.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public static Osoba ZwrocOsobe(int id)
        {
            using (CentrumContext data = new CentrumContext())
            {
                return data.Uzytkownicy.Where(p => p.Id == id).FirstOrDefault();
            }
        }

        public static void DodajWypozyczenie(Wypozyczenie noweWypozyczenie)
        {
            using (CentrumContext data = new CentrumContext())
            {
                data.Wypozyczenia.Add(noweWypozyczenie);
                data.SaveChanges();
            }
        }
        
        
        public static Zajecia zwrocZajeciaBiletu(Bilet b)
        {
            using (CentrumContext data = new CentrumContext())
            {
                Zajecia zz = new Zajecia();
                foreach(Bilet bb in data.Bilety)
                {
                    if (bb.Id == b.Id)
                    {
                        
                        foreach(WpisZajecia wz in data.WpisyZajecia)
                        {
                            if (bb.Zajecia.Id == wz.Id)
                            {
                                foreach (Grupa g in data.Grupy)
                                    if (g.Id == wz.Grupa.Id)
                                        foreach (Dyscyplina d in data.Dyscypliny)
                                            if (d.Id == g.Dyscyplina.Id)
                                                foreach (Zajecia z in data.Zajecia)
                                                    if (z.Dyscyplina.Nazwa == d.Nazwa)
                                                        return z;
                            }

                        }
                    }
                }
                return zz;
            }
        }


        public static void dodajdoGrypy(Grupa g, UczestnikZajec s)
        {
            using (CentrumContext data = new CentrumContext())
            {
                foreach (UczestnikZajec ss in data.UczestnicyZajec)
                    if (ss.Id == s.Id)
                        foreach (Grupa gg in data.Grupy)
                            if (gg.Id == g.Id)
                            {
                                
                                gg.Uczestincy.Add(ss);
                            }


                data.SaveChanges();
            }
        }



    }
}
