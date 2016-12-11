using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    class Profil
    {
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


        public void dodajZajecia(Zajecia zajecia)
        {
            _historia.Add(zajecia);
        }




    }
}
