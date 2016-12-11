using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("Profile")]
    public class Profil
    {
        public string ID { get; set; }
        private int _ocena;
        private List<ZajeciaOdbyte> _historia;

        public Profil()
        {
            _ocena = 0;
            _historia = new List<ZajeciaOdbyte>();
        }


        public int ocena
        {
            get
            {
                return _ocena;
            }

            set
            {
                _ocena = value;
            }
        }

        public List<ZajeciaOdbyte> historia
        {
            get
            {
                return _historia;
            }

            set
            {
                _historia = value;
            }
        }


        //public void dodajZajecia(Zajecia zajecia)
        //{
        //    _historia.Add(zajecia);
        //}




    }
}
