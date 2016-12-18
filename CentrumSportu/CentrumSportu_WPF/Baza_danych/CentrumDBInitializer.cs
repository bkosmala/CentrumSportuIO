using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Baza_danych
{
    public class CentrumDBInitializer :DropCreateDatabaseAlways<CentrumContext>
    {
        protected override void Seed(CentrumContext context)  
        {           
            Instruktor instruktor = new Instruktor("I1", "Piotr", "Kazmierczak", new List<string>() { "Pilka nozna" }, new KontoUzytkownika("kazik", "kazik", KontoUzytkownika.RodzajKonta.Instruktor));
            Student student = new Student("U1", "Rafał", "Lebioda", new KontoUzytkownika("lebioda", "lebioda", KontoUzytkownika.RodzajKonta.Student));
            Administrator administrator = new Administrator("A1", "Super", "Admin", new KontoUzytkownika("admin", "admin", KontoUzytkownika.RodzajKonta.Administrator));
            context.Instruktorzy.Add(instruktor);
            context.Studenci.Add(student);
            context.Administratorzy.Add(administrator);
            base.Seed(context);
        }
    }
}
