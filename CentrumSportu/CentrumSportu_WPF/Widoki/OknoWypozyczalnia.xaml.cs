using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Linq;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for OknoWypozyczalnia.xaml
    /// </summary>
    public partial class OknoWypozyczalnia : Window
    {
        private Pracownik pracownikWypozyczalni;
        private ObservableCollection<Rezerwacja> rezerwacjeAktywne;
        private ObservableCollection<Rezerwacja> zakonczoneRezerwacje;

        public OknoWypozyczalnia(Pracownik pracownikWypozyczalni)
        {
            this.pracownikWypozyczalni = pracownikWypozyczalni;
            rezerwacjeAktywne = new ObservableCollection<Rezerwacja>();
            zakonczoneRezerwacje = new ObservableCollection<Rezerwacja>();

            var rezerwacjeAktywneTmp = BazaMetody.ZwrocRezerwacjeAktywne();
            foreach (var item in rezerwacjeAktywneTmp)
            {
                rezerwacjeAktywne.Add(item);
            }
            var rezerwacjeZakonczone = BazaMetody.ZwrocRezerwacjeWedlugStatusu(Rezerwacja.StatusRezerwacji.ZAKONCZONA);
            var rezerwacjeAnulowane = BazaMetody.ZwrocRezerwacjeWedlugStatusu(Rezerwacja.StatusRezerwacji.ANULOWANA);
            foreach (var item in rezerwacjeAnulowane)
            {
                zakonczoneRezerwacje.Add(item);
            }
            foreach(var item in rezerwacjeZakonczone)
            {
                zakonczoneRezerwacje.Add(item);
            }


            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            UzytkownikLabel.Content = pracownikWypozyczalni.Imie + " " + pracownikWypozyczalni.Nazwisko;
            rezerwacjeAktywneListView.ItemsSource = rezerwacjeAktywne;
            historiaRezerwacjiListView.ItemsSource = zakonczoneRezerwacje;
        }

        private void wylogujButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }

        private void rezerwacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void historiaRezerwacjiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zaznaczonaRezerwacja = historiaRezerwacjiListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                przedmiotyLabel.Content = String.Join(", ", zaznaczonaRezerwacja.Przedmioty.Select(p => p.Nazwa));
                if (zaznaczonaRezerwacja.Status == Rezerwacja.StatusRezerwacji.ZAKONCZONA)
                {
                    wydawcaSprzetuLabel.Content = zaznaczonaRezerwacja.Wypozyczenie.WydawcaSprzetu.Imie + " " + zaznaczonaRezerwacja.Wypozyczenie.WydawcaSprzetu.Nazwisko;
                }
            }
        }
    }
}
