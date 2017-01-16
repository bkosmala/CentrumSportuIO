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
        private ObservableCollection<Rezerwacja> realizowaneRezerwacje;
        private ObservableCollection<Rezerwacja> oczekujaceRezerwacje;

        public OknoWypozyczalnia(Pracownik pracownikWypozyczalni)
        {
            this.pracownikWypozyczalni = pracownikWypozyczalni;
            rezerwacjeAktywne = new ObservableCollection<Rezerwacja>();
            zakonczoneRezerwacje = new ObservableCollection<Rezerwacja>();
            realizowaneRezerwacje = new ObservableCollection<Rezerwacja>();
            oczekujaceRezerwacje = new ObservableCollection<Rezerwacja>();

            RefreshData();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            UzytkownikLabel.Content = pracownikWypozyczalni.Imie + " " + pracownikWypozyczalni.Nazwisko;
            rezerwacjeAktywneListView.ItemsSource = rezerwacjeAktywne;
            historiaRezerwacjiListView.ItemsSource = zakonczoneRezerwacje;
            realizowaneRezerwacjeListView.ItemsSource = realizowaneRezerwacje;
            oczekujaceRezerwacjeListView.ItemsSource = oczekujaceRezerwacje;
        }

        private void wylogujButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }

        private void rezerwacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zaznaczonaRezerwacja = rezerwacjeAktywneListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                przedmiotyLabel1.Content = String.Join(", ", zaznaczonaRezerwacja.Przedmioty.Select(p => p.Nazwa));
            }
        }

        private void wypozyczButton_Click(object sender, RoutedEventArgs e)
        {
            var zaznaczonaRezerwacja = rezerwacjeAktywneListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                zaznaczonaRezerwacja.Wypozyczenie = new Wypozyczenie(DateTime.Now, pracownikWypozyczalni);
                BazaMetody.DodajWypozyczenie(zaznaczonaRezerwacja.Wypozyczenie);
                zaznaczonaRezerwacja.Status = Rezerwacja.StatusRezerwacji.REALIZOWANA;
                foreach (var item in zaznaczonaRezerwacja.Przedmioty)
                {
                    item.Dostepnosc = false;
                }
                BazaMetody.AktualizujRezerwacje(zaznaczonaRezerwacja);
                RefreshData();
            }
            else
            {
                MessageBox.Show("Zaznacz rezerwacje.");
            }
        }

        private void RefreshData()
        {
            rezerwacjeAktywne.Clear();
            zakonczoneRezerwacje.Clear();
            realizowaneRezerwacje.Clear();
            oczekujaceRezerwacje.Clear();
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
            foreach (var item in rezerwacjeZakonczone)
            {
                zakonczoneRezerwacje.Add(item);
            }
            var realizowaneRezerwacjeTmp = BazaMetody.ZwrocRezerwacjeWedlugStatusu(Rezerwacja.StatusRezerwacji.REALIZOWANA);
            foreach (var item in realizowaneRezerwacjeTmp)
            {
                realizowaneRezerwacje.Add(item);
            }
            var oczekujaceRez = BazaMetody.ZwrocRezerwacjeWedlugStatusu(Rezerwacja.StatusRezerwacji.OCZEKUJACA);
            foreach (var item in oczekujaceRez)
            {
                oczekujaceRezerwacje.Add(item);
            }
        }

        private void historiaRezerwacjiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wydawcaSprzetuLabel.Content = "BRAK";
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

        private void przyjmijZwrotButton_Click(object sender, RoutedEventArgs e)
        {
            var zaznaczonaRezerwacja = realizowaneRezerwacjeListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                zaznaczonaRezerwacja.Status = Rezerwacja.StatusRezerwacji.ZAKONCZONA;
                foreach (var item in zaznaczonaRezerwacja.Przedmioty)
                {
                    item.Dostepnosc = true;
                }
                BazaMetody.AktualizujRezerwacje(zaznaczonaRezerwacja);
                RefreshData();
            } 
            else
            {
                MessageBox.Show("Wybierz rezerwacje.");
            }
        }

        private void noweWypozyczenieButton_Click(object sender, RoutedEventArgs e)
        {
            WyszukiwanieKlientaOkno okno = new WyszukiwanieKlientaOkno(pracownikWypozyczalni);
            okno.ShowDialog();
            RefreshData();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            przedmiotyLabel.Content = "";
        }

        private void realizowaneRezerwacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zaznaczonaRezerwacja = realizowaneRezerwacjeListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                przedmiotyLabel2.Content = String.Join(", ", zaznaczonaRezerwacja.Przedmioty.Select(p => p.Nazwa));
                if (zaznaczonaRezerwacja.Status == Rezerwacja.StatusRezerwacji.REALIZOWANA)
                {
                    wydawcaSprzetuLabel2.Content = zaznaczonaRezerwacja.Wypozyczenie.WydawcaSprzetu.Imie + " " + zaznaczonaRezerwacja.Wypozyczenie.WydawcaSprzetu.Nazwisko;
                }
            }
        }

        private void oczekujaceRezerwacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zaznaczonaRezerwacja = oczekujaceRezerwacjeListView.SelectedItem as Rezerwacja;
            if (zaznaczonaRezerwacja != null)
            {
                przedmiotyLabel4.Content = String.Join(", ", zaznaczonaRezerwacja.Przedmioty.Select(p => p.Nazwa));
            }
        }
    }
}
