using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentrumSportu_WPF.Modul_biletow
{
    [Table("ZajeciaOdbyte")]
    public class ZajeciaOdbyte
    {
        public string ID { get; set; }
        private Grupa _grupa;
        private string _obecnosc;
        private DateTime _data;

        public ZajeciaOdbyte(Grupa grupa, string obecnosc, DateTime data)
        {
            _grupa = grupa;
            _obecnosc = obecnosc;
            _data = data;
        }


        public Grupa grupa
        {
            get
            {
                return _grupa;
            }

            set
            {
                _grupa = value;
            }
        }

        public string obecnosc
        {
            get
            {
                return _obecnosc;
            }

            set
            {
                _obecnosc = value;
            }
        }

        public DateTime data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }


    }
}
