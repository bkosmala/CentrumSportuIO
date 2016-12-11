using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF
{
    class Bilet
    {
        private int _id;
        private Zajecia _zajecia;  
        private bool _zaplacono;
  

        public Bilet(int id, ZajeciaOdbyte zajecia)
        {
            _zaplacono = false;
            _id = id;
            _zajecia = zajecia;
        }

        public int id
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
        

        public bool zaplacono
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
