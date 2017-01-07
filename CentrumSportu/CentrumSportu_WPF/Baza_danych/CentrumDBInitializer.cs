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

            //Studenci
            Student student1 = new Student("Rafał", "Lebioda", kontoStudent, "rafal.lebioda@gmail.com", "666666666");
            student1.Zdjecie = path + "../../Images/profilPhoto.jpg";

            //Administratorzy
            Administrator administrator1 = new Administrator("Super", "Admin",kontoAdministrator);

            //Obiekty sportowe
            ObiektSportowy obiekt1 = new ObiektSportowy("Sala główna", dyscypliny, 25, 100);

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

            //Zajecia
            List<WpisZajecia> zajecia = new List<WpisZajecia>
            {
                new WpisZajecia(new DateTime(2017,2,1,16,0,0), new DateTime(2017,2,1,18,0,0),obiekt1,instruktor1,grupa1 ),
                new WpisZajecia(new DateTime(2017,2,2,16,0,0), new DateTime(2017,2,2,18,0,0),obiekt1,instruktor1,grupa1 ),
                new WpisZajecia(new DateTime(2017,2,3,16,0,0), new DateTime(2017,2,3,18,0,0),obiekt1,instruktor1,grupa2 ),
                new WpisZajecia(new DateTime(2017,2,4,16,0,0), new DateTime(2017,2,4,18,0,0),obiekt1,instruktor1,grupa2 ),
                new WpisZajecia(new DateTime(2017,2,5,16,0,0), new DateTime(2017,2,5,18,0,0),obiekt1,instruktor1,grupa3 ),
                new WpisZajecia(new DateTime(2017,2,6,16,0,0), new DateTime(2017,2,6,18,0,0),obiekt1,instruktor1,grupa3 )
            };

            //Zajecia - moduł oferta

            List<Zajecia> zajeciaOferta = new List<Zajecia>
            {
                new Zajecia("grupowe","Piłka nożna","Trening piłki nożnej","opis",null),
                new Zajecia("grupowe","Piłka koszykowa","Trening koszykówki","opis",null),
                new Zajecia("grupowe","Piłka siatkowa","Trening siatkówki","opis",null)

            };


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
            base.Seed(context);
        }
    }
}
