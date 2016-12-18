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

namespace CentrumSportu_WPF.Baza_danych
{
    public class CentrumDBInitializer :DropCreateDatabaseAlways<CentrumContext>
    {
        protected override void Seed(CentrumContext context)  
        {           
            Instruktor instruktor = new Instruktor("I1", "Piotr", "Kazmierczak","pkazmierczak@gmail.com","500600700", new List<string>() { "Pilka nozna" }, new KontoUzytkownika("kazik", "kazik", KontoUzytkownika.RodzajKonta.Instruktor));
            Student student = new Student("U1", "Rafał", "Lebioda", new KontoUzytkownika("lebioda", "lebioda", KontoUzytkownika.RodzajKonta.Student));
            Administrator administrator = new Administrator("A1", "Super", "Admin", new KontoUzytkownika("admin", "admin", KontoUzytkownika.RodzajKonta.Administrator));
            string path = AppDomain.CurrentDomain.BaseDirectory;
            instruktor.Zdjęcie = path + "../../Resources/instruktor_test.jpg";
            context.Instruktorzy.Add(instruktor);
            context.Studenci.Add(student);
            context.Administratorzy.Add(administrator);
            base.Seed(context);
        }
    }
}
