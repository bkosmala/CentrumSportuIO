using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_instruktor.xaml
    /// </summary>
    public partial class okno_instruktor : Window
    {
        private Instruktor instruktor;

        public okno_instruktor(Instruktor _instruktor)
        {
            InitializeComponent();
            instruktor = _instruktor;
        }
    }
}
