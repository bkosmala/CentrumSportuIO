using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Properties;
using CentrumSportu_WPF.Modul_oferty;

namespace CentrumSportu_WPF.Baza_danych
{
    public class CentrumDBInitializer :DropCreateDatabaseAlways<CentrumContext>
    {
        protected override void Seed(CentrumContext context)  
        {      
            //Konta uzytkownikow
            KontoUzytkownika kontoInstruktor = new KontoUzytkownika("kazik", "kazik", KontoUzytkownika.RodzajKonta.Instruktor);
            KontoUzytkownika kontoAdministrator = new KontoUzytkownika("admin", "admin", KontoUzytkownika.RodzajKonta.Administrator);
            KontoUzytkownika kontoStudent= new KontoUzytkownika("lebioda", "lebioda", KontoUzytkownika.RodzajKonta.Student);

            KontoUzytkownika kontoInstruktor2 = new KontoUzytkownika("inst", "inst",
                KontoUzytkownika.RodzajKonta.Instruktor);

            //Dyscypliny
            List<Dyscyplina> dyscypliny=new List<Dyscyplina>()
            {
                new Dyscyplina("Piłka nożna"),
                new Dyscyplina("Piłka siatkowa"),
                new Dyscyplina("Piłka koszykowa")
            };


            //Instruktorzy
            Instruktor instruktor1= new Instruktor("Piotr", "Kazmierczak", "pkazmierczak@gmail.com", "500600700",dyscypliny,kontoInstruktor);
            string path = AppDomain.CurrentDomain.BaseDirectory;
            instruktor1.Zdjecie = path + "../../Images/instruktor_test.jpg";

            Instruktor instruktor2 = new Instruktor("Jan", "Kowalski", "kowalski@gmail.com", "999999999", new List<Dyscyplina>(), kontoInstruktor2);
            instruktor2.Zdjecie = path + "../../Images/instruktor_test1.jpg";
            instruktor2.WpisZajecia = new List<WpisZajecia>();


            //Studenci
            Student student1 = new Student("Rafał", "Lebioda", kontoStudent, "rafal.lebioda@gmail.com", "666666666");
            student1.Zdjecie = path + "../../Images/profilPhoto.jpg";


            //Administratorzy
            Administrator administrator1 = new Administrator("Super", "Admin",kontoAdministrator);

            //Obiekty sportowe
            ObiektSportowy obiekt1 = new ObiektSportowy("Sala główna", dyscypliny, 25, 100);
            ObiektSportowy obiekt2 = new ObiektSportowy("Sala główna 2", null, 25, 100);
            dyscypliny[1].ObiektySportowe.Add(obiekt2);
            dyscypliny[0].ObiektySportowe.Add(obiekt2);


            //UczestnicyZajec
            UczestnikZajec uczestnik1 = new UczestnikZajec("Jan", "Kowalski", "jan.kowalski@gmail.com", "608924351");
            UczestnikZajec uczestnik2 = new UczestnikZajec("Janusz", "Nowak", "janusz.nowak@gmail.com", "709261789");
            UczestnikZajec uczestnik3 = new UczestnikZajec("Rafał", "Lebioda", "rafal.lebioda@gmail.com", "666666666");
            uczestnik1.Zdjecie = path + "../../Images/profilPhoto.jpg";
            uczestnik2.Zdjecie = path + "../../Images/profilPhoto.jpg";
            uczestnik3.Zdjecie = path + "../../Images/profilPhoto.jpg";

            //Grupy
            Grupa grupa1 = new Grupa(dyscypliny[0], 5, 30,"Piłka nożna - grupa męska");
            Grupa grupa2 = new Grupa(dyscypliny[0], 5, 30, "Piłka nożna - grupa żeńska");
            Grupa grupa3 = new Grupa(dyscypliny[1], 5, 30, "Piłka siatkowa - grupa żeńska");

            grupa1.DodajUczestnika(uczestnik1);
            grupa1.DodajUczestnika(uczestnik2);
            grupa1.DodajUczestnika(uczestnik3);

            instruktor1.Grupy.Add(grupa1);
            instruktor1.Grupy.Add(grupa2);
            instruktor1.Grupy.Add(grupa3);

            //Cenniki - moduł oferta

            Cennik cennik1 = new Cennik(12, "zł", "Cennik - zajęcia grupowe, piłka nożna");
            Cennik cennik2 = new Cennik(13, "zł", "Cennik - zajęcia grupowe, koszykówka");
            Cennik cennik3 = new Cennik(15, "zł", "Cennik - zajęcia grupowe, siatkówka");

            //Zajecia - moduł oferta
            Zajecia zajecia1 = new Zajecia("grupowe", dyscypliny[0], "Trening piłki nożnej", "Dlaczego warto u nas trenować:\n - Trening prowadzony przez doświadczonych trenerów. \n - Własna metodologia szkolenia oparta na zachodnich wzorcach.\n - Możliwość korzystanie z bogatego wyposażenia Centrum.\n - Rozgrywka zespołowa na każdym treningu, przez ponad połowę czasu.  \n - I wiele innych :) ", cennik1);
            Zajecia zajecia2 = new Zajecia("grupowe", dyscypliny[2], "Trening koszykówki", "Rozgrywka zespołowa na każdym treningu", cennik2);
            Zajecia zajecia3 = new Zajecia("grupowe", dyscypliny[1], "Trening siatkówki", "Trening prowadzony przez doświadczonych trenerów.\n", cennik3);
            List<Zajecia> zajeciaOferta = new List<Zajecia>();

            zajeciaOferta.Add(zajecia1);
            zajeciaOferta.Add(zajecia2);
            zajeciaOferta.Add(zajecia3);

            //Zajecia
            List<WpisZajecia> zajecia = new List<WpisZajecia>
            {
                new WpisZajecia(new DateTime(2017,2,1,16,0,0), new DateTime(2017,2,1,18,0,0),obiekt1,instruktor1,grupa1,zajecia1 ),
                new WpisZajecia(new DateTime(2017,2,2,16,0,0), new DateTime(2017,2,2,18,0,0),obiekt1,instruktor1,grupa1,zajecia1 ),
                new WpisZajecia(new DateTime(2017,2,3,16,0,0), new DateTime(2017,2,3,18,0,0),obiekt1,instruktor1,grupa2,zajecia1 ),
                new WpisZajecia(new DateTime(2017,2,4,16,0,0), new DateTime(2017,2,4,18,0,0),obiekt1,instruktor1,grupa2,zajecia1 ),
                new WpisZajecia(new DateTime(2017,2,5,16,0,0), new DateTime(2017,2,5,18,0,0),obiekt1,instruktor1,grupa3,zajecia3 ),
                new WpisZajecia(new DateTime(2017,2,6,16,0,0), new DateTime(2017,2,6,18,0,0),obiekt1,instruktor1,grupa3,zajecia3 )
            };


            //Bilety
            Bilet b1 = new Bilet(zajecia[0], student1);
            student1.Bilety = new List<Bilet>();
            student1.Bilety.Add(b1);


            context.Bilety.Add(b1);
            




            //Rezerwacje - moduł oferta

            Rezerwacja rezerwacja1 = new Rezerwacja() { Klient = student1, OdDaty = DateTime.Now.AddHours(24), DoDaty = DateTime.Now.AddHours(25), Status = Rezerwacja.StatusRezerwacji.OCZEKUJACA };
            Rezerwacja rezerwacja2 = new Rezerwacja() { Klient = student1, OdDaty = DateTime.Now.AddHours(20), DoDaty = DateTime.Now.AddHours(23), Status = Rezerwacja.StatusRezerwacji.ANULOWANA };
            Rezerwacja rezerwacja3 = new Rezerwacja() { Klient = student1, OdDaty = DateTime.Now.AddHours(39), DoDaty = DateTime.Now.AddHours(41), Status = Rezerwacja.StatusRezerwacji.ZREALIZOWANA };


            Przedmiot przedmiot1 = new Przedmiot() { Nazwa = "paletka do tenisa stołowego", Dostepnosc = true };
            Przedmiot przedmiot2 = new Przedmiot() { Nazwa = "rakietka do squasha", Dostepnosc = true };
            Przedmiot przedmiot3 = new Przedmiot() { Nazwa = "rakietka do badmintona", Dostepnosc = true };

            rezerwacja1.RezerwujPrzedmiot(przedmiot1);
            rezerwacja1.RezerwujPrzedmiot(przedmiot2);
            rezerwacja2.RezerwujPrzedmiot(przedmiot3);
            rezerwacja3.RezerwujPrzedmiot(przedmiot1);

            context.Przedmioty.Add(przedmiot1);
            context.Przedmioty.Add(przedmiot2);
            context.Przedmioty.Add(przedmiot3);
            context.Rezerwacje.Add(rezerwacja1);
            context.Rezerwacje.Add(rezerwacja2);
            context.Rezerwacje.Add(rezerwacja3);
            

            context.Instruktorzy.Add(instruktor1);           
            

           
            context.Studenci.Add(student1);
            context.Administratorzy.Add(administrator1);
            foreach (var item in zajecia)
            {
                context.WpisyZajecia.Add(item);
            }
            foreach (var item in zajeciaOferta)
            {
                context.Zajecia.Add(item);
            }
            context.UczestnicyZajec.Add(uczestnik1);
            context.UczestnicyZajec.Add(uczestnik2);
            context.UczestnicyZajec.Add(uczestnik3);
            context.Cennik.Add(cennik1);
            context.Cennik.Add(cennik2);
            context.Cennik.Add(cennik3);
            context.SaveChanges();

            var d = new List<Dyscyplina>(dyscypliny.Take<Dyscyplina>(2));
            //var d = context.Dyscypliny.ToList();
            instruktor2.Dyscypliny = d;
            context.Instruktorzy.Add(instruktor2);
            base.Seed(context);


            
            
        }
    }
}
