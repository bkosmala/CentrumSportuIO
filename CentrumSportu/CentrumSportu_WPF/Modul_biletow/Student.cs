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
        private Grupa _grupa;
        private Profil _profil;

        public Student(string imie, string nazwisko):base(imie, nazwisko)
        {
            _grupa = null;
            _profil = new Profil();
        }


        public Grupa grupa
        {
            get
            {
                return _grupa;
            }

        }

        public Profil profil
        {
            get
            {
                return _profil;
            }

            set
            {
                _profil = value;
            }
        }


        public void odejdzZGrupy()
        {
            _grupa = null;
            _grupa.UsunUczestnika(this.ID);
          
        }


        public void dojdzDoGrupy(Grupa g)
        {
            if (g.Uczestincy.Count < g.MaxLiczebnosc)
            {
                g.DodajUczestnika(this);
                _grupa = g;
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
