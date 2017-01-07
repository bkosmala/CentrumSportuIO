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
        private Zajecia _zajecia;  
        private bool _zaplacono;
  

        public Bilet(int id, Zajecia zajecia)
        {
            _zaplacono = false;
            _id = id;
            _zajecia = zajecia;
        }

        public Bilet(Zajecia zajecia)
        {
            this._zajecia = zajecia;
            _zaplacono = false;
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

        
        public Zajecia zajecia
        {
            get
            {
                return _zajecia;
            }

            set
            {
                _zajecia = value;
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
