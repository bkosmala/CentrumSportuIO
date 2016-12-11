using CentrumSportu_WPF.Modul_instruktorow;
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
            Database.SetInitializer<CentrumContext>(new DropCreateDatabaseIfModelChanges<CentrumContext>());
        }

        public DbSet<Instruktor> Instruktorzy { get; set; }
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<ObiektSportowy> ObiektySportowe { get; set; }
        public DbSet<WpisZajecia> WpisyZajecia { get; set; }
    }
}
