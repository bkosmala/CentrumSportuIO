using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for OknoWypozyczalnia.xaml
    /// </summary>
    public partial class OknoWypozyczalnia : Window
    {
        private Pracownik pracownikWypozyczalni;
        private ObservableCollection<Rezerwacja> rezerwacjeAktywne;

        public OknoWypozyczalnia(Pracownik pracownikWypozyczalni)
        {
            this.pracownikWypozyczalni = pracownikWypozyczalni;
            rezerwacjeAktywne = new ObservableCollection<Rezerwacja>();
            var rezerwacjeAktywneTmp = BazaMetody.ZwrocRezerwacjeAktywne();
            foreach (var item in rezerwacjeAktywneTmp)
            {
                rezerwacjeAktywne.Add(item);
            }

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            UzytkownikLabel.Content = pracownikWypozyczalni.Imie + " " + pracownikWypozyczalni.Nazwisko;
            rezerwacjeAktywneListView.ItemsSource = rezerwacjeAktywne;
        }

        private void wylogujButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }

        private void rezerwacjeListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (rezerwacjeAktywneListView.SelectedItem != null)
            {

            }
            else
            {
                MessageBox.Show("Zaznacz rezerwacje.");
            }
        }
    }
}
