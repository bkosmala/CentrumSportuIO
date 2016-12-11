using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
    public class ListaCennikow
    {
        private List<Cennik> cenniki;
        public List<Cennik> Cenniki
        {
            get { return cenniki; }
        }

        public bool DodajCennik(Cennik c)
        {
            bool nieIstnieje = true;
            // sprawdzenie czy cennik nie został już dodany
            foreach (Cennik element in cenniki)
            {
                if (element.IdCennika == c.IdCennika)
                {
                    nieIstnieje = false;
                }

            }

            if (nieIstnieje)
            {
                cenniki.Add(c);
                return true;
            }
            return false;
        }

        public void UsunCennik(Cennik c)
        {
            cenniki.Remove(c);
        }

        public List<Cennik> Wyszukaj(string nazwaCennika)
        {
            List<Cennik> wynik = new List<Cennik>();

            foreach (Cennik element in cenniki)
            {
                if (element.Nazwa.Equals(nazwaCennika))
                {
                    wynik.Add(element);
                }

            }
            return wynik;
        }

        public List<Cennik> Wyszukaj(int idCennika)
        {
            List<Cennik> wynik = new List<Cennik>();

            foreach (Cennik element in cenniki)
            {
                if (element.IdCennika == idCennika)
                {
                    wynik.Add(element);
                }

            }
            return wynik;
        }


    }
}
