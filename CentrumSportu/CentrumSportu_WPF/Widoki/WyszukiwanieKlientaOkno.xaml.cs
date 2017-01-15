using CentrumSportu_WPF.Baza_danych;
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

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for WyszukiwanieKlientaOkno.xaml
    /// </summary>
    public partial class WyszukiwanieKlientaOkno : Window
    {
        public WyszukiwanieKlientaOkno()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void dalejButton_Click(object sender, RoutedEventArgs e)
        {
            var osoba = BazaMetody.ZwrocOsobe(idKlientaControl.Value ?? -1);
            String osobaTozsamosc = osoba.Id + ", " + osoba.Imie + " " + osoba.Nazwisko; 
            if (osoba != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(osobaTozsamosc + "?", "Tozsamosc klienta", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                }
            }   
        }
    }
}
