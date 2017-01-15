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

        public Student(string imie, string nazwisko,KontoUzytkownika konto):base(imie, nazwisko)
        {
            Grupa = null;
            Profil = null;
            KontoUzytkownika = konto;
        }

        public Student(string imie, string nazwisko, KontoUzytkownika konto, string email, string telefon) : base(imie, nazwisko, email, telefon)
        {
            Grupa = null;
            Profil = null;
            KontoUzytkownika = konto;
            Bilety = new List<Bilet>();
        }

        public Student()
        {
            
        }

        public virtual Grupa Grupa { get;  set; }

        public virtual Profil Profil { get; set; }


        public void odejdzZGrupy()
        {
            
            Grupa = null;
           
          
        }


        public void dojdzDoGrupy(Grupa g)
        {

                Grupa = g;
            
        }




    }
}
