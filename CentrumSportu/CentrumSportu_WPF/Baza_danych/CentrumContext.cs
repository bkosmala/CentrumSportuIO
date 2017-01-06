using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Baza_danych
{
    public class CentrumContext : DbContext
    {
        public CentrumContext() : base("name=CetrumSportuDBCConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new CentrumDBInitializer());
        }

        public DbSet<Instruktor> Instruktorzy { get; set; }
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<ObiektSportowy> ObiektySportowe { get; set; }
        public DbSet<WpisZajecia> WpisyZajecia { get; set; }
        public DbSet<Student> Studenci { get; set; }
        public DbSet<WpisWydarzenie> WpisyWydarzenia { get; set; }
        public DbSet<Wydarzenie> Wydarzenia { get; set; }
        public DbSet<Cennik> Cennik { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
        public DbSet<KontoUzytkownika> KontaUzytkownikow { get; set; }
        public DbSet<Administrator> Administratorzy { get; set; }
        public DbSet<Dyscyplina> Dyscypliny { get; set; }
        public DbSet<UczestnikZajec> UczestnicyZajec { get; set; }
        public DbSet<Zajecia> Zajecia { get; set; }


    }
}
