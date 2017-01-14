using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("Bilety")]
    public class Bilet
    {
        private int _id;
        public virtual WpisZajecia Zajecia { get; set; }  
        private bool _zaplacono;
        

        
        public string Oplacone
        {
            get
            {
                if (_zaplacono)
                    return "opłacono";
                else
                    return "nie opłacono";
            }
        }

        public UczestnikZajec Uczestnik { get; set; }
  
        public Bilet()
        {

        }

        public Bilet(int id, WpisZajecia zajecia)
        {
            _zaplacono = false;
            _id = id;
            Zajecia = zajecia;
        }

        public Bilet(WpisZajecia zajecia, UczestnikZajec idU)
        {
            this.Zajecia = zajecia;
            _zaplacono = false;
            this.Uczestnik = idU;
        }

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        

        

        public bool Zaplacono
        {
            get
            {
                return _zaplacono;
            }

            set
            {
                _zaplacono = value;
            }
        }


        public void zaplac()
        {
            //przekierowanie do zewnetrzneg systemu platnosci?

        }

    }
}
