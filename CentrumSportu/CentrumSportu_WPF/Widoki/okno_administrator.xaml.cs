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
using CentrumSportu_WPF.Modul_biletow;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_administrator.xaml
    /// </summary>
    public partial class okno_administrator : Window
    {
        private Administrator administrator;

        public okno_administrator(Administrator _administrator)
        {
            InitializeComponent();
            administrator = _administrator;
            this.textBox.Text = administrator.Imie + " " + administrator.Nazwisko;
        }
    }
}
