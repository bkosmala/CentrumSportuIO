using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("Studenci")]
    public class Student : UczestnikZajec
    {
        public virtual KontoUzytkownika KontoUzytkownika { get; set; }

        public Student(string id,string imie, string nazwisko,KontoUzytkownika konto):base(id,imie, nazwisko)
        {
            Grupa = null;
            Profil = null;
            KontoUzytkownika = konto;
        }

        public Student()
        {
            
        }

        public Grupa Grupa { get;  set; }

        public Profil Profil { get; set; }


        public void odejdzZGrupy()
        {
            Grupa = null;
            Grupa.UsunUczestnika(this.ID);
          
        }


        public void dojdzDoGrupy(Grupa g)
        {
            if (g.Uczestincy.Count < g.MaxLiczebnosc)
            {
                g.DodajUczestnika(this);
                Grupa = g;
            }
        }

        public void zmienGrupe(Grupa g)
        {
            if (g.Uczestincy.Count < g.MaxLiczebnosc)
            {
                odejdzZGrupy();
                g.DodajUczestnika(this);
                
            }
        }


    }
}
