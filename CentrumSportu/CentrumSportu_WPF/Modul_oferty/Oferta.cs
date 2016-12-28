using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_oferty
{
   public class Oferta
    {
        private List<Wydarzenie> wydarzenia;
        private List<Zajecia> listaZajecia;
        private List<ObiektSportowy> obiekty;


        public List<Wydarzenie> Wydarzenia
        {
            get { return wydarzenia; }
        }
        public List<Zajecia> ListaZajecia
        {
            get { return listaZajecia; }
        }
        public List<ObiektSportowy> ListaObiekty
        {
            get { return obiekty; }
        }

        public bool DodajWydarzenie(Wydarzenie wyd)
        {
            bool nieIstnieje = true;
            foreach (Wydarzenie element in wydarzenia)
            {
                if (element.IdWydarzenia == wyd.IdWydarzenia)
                {
                    nieIstnieje = false;
                }

            }

            if (nieIstnieje)
            {
                wydarzenia.Add(wyd);
                return true;
            }
            return false;
        }

        public bool DodajZajecia(Zajecia zaj)
        {
            bool nieIstnieje = true;
            foreach (Zajecia element in listaZajecia)
            {
                if (element.Id == zaj.Id)
                {
                    nieIstnieje = false;
                }

            }

            if (nieIstnieje)
            {
                listaZajecia.Add(zaj);
                return true;
            }
            return false;
        }

        public bool DodajObiekt(ObiektSportowy o)
        {
            bool nieIstnieje = true;
            foreach (ObiektSportowy element in obiekty)
            {
                if (element.Id == o.Id)
                {
                    nieIstnieje = false;
                }

            }

            if (nieIstnieje)
            {
                obiekty.Add(o);
                return true;
            }
            return false;
        }

        public void UsunWydarzenie(Wydarzenie wyd)
        {
            wydarzenia.Remove(wyd);
        }

        public void UsunZajecia(Zajecia zaj)
        {
            listaZajecia.Remove(zaj);
        }

        public void UsunObiekt(ObiektSportowy o)
        {
            obiekty.Remove(o);
        }

        public Wydarzenie WyszukajWydarzenie(int idWyd)
        {

            foreach (Wydarzenie element in wydarzenia)
            {
                if (element.IdWydarzenia == idWyd)
                {
                    return element;
                }

            }
            return null;
        }

        public Zajecia WyszukajZajecia(int idZaj)
        {

            foreach (Zajecia element in listaZajecia)
            {
                if (element.Id == idZaj)
                {
                    return element;
                }

            }
            return null;
        }

        public ObiektSportowy WyszukajObiekt(int id)
        {

            foreach (ObiektSportowy element in obiekty)
            {
                if (element.Id == id)
                {
                    return element;
                }

            }
            return null;
        }
    }
}
