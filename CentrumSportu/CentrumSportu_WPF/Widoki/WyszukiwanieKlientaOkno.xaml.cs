using CentrumSportu_WPF.Baza_danych;
using System;
using System.Windows;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for WyszukiwanieKlientaOkno.xaml
    /// </summary>
    public partial class WyszukiwanieKlientaOkno : Window
    {
        private Pracownik pracownik;

        public WyszukiwanieKlientaOkno(Pracownik pracownik)
        {
            this.pracownik = pracownik;
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
                    NoweWypozyczenieOkno okno = new NoweWypozyczenieOkno(osoba, pracownik);
                    this.Close();
                    okno.ShowDialog();
                }
            }   
        }
    }
}
