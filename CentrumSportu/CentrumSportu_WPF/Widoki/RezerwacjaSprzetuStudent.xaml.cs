using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for WypozyczanieSprzetuStudent.xaml
    /// </summary>
    public partial class WypozyczanieSprzetuStudent : Window
    {
        private ObservableCollection<Przedmiot> dostepnePrzedmioty;
        private readonly Osoba klient;
        private okno_student oknoStudenta;
        private DateTime odDaty;
        private DateTime doDaty;

        public WypozyczanieSprzetuStudent(okno_student o, Osoba klient)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.klient = klient;
            oknoStudenta = o;

            dostepnePrzedmioty = new ObservableCollection<Przedmiot>(BazaMetody.ZwrocWszystkiePrzedmioty());

            InitializeControls();
        }

        private void InitializeControls()
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today);
            CalendarDateRange cdr2 = new CalendarDateRange(DateTime.Today.AddDays(22), DateTime.MaxValue);
            wyborDatyControl.BlackoutDates.Add(cdr);
            wyborDatyControl.BlackoutDates.Add(cdr2);
            startGodzinaControl.TimeInterval = new TimeSpan(0, 15, 0);
            startGodzinaControl.DefaultValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 7, 0, 0);
            startGodzinaControl.DisplayDefaultValueOnEmptyText = true;
            startGodzinaControl.EndTime = new TimeSpan(21, 0, 0);
            startGodzinaControl.StartTime = new TimeSpan(7, 0, 0);
            koniecGodzinaControl.TimeInterval = new TimeSpan(0, 15, 0);
            koniecGodzinaControl.DefaultValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
            koniecGodzinaControl.EndTime = new TimeSpan(22, 0, 0);
            koniecGodzinaControl.DisplayDefaultValueOnEmptyText = true;
            koniecGodzinaControl.StartTime = new TimeSpan(7, 15, 0);
            listView.ItemsSource = dostepnePrzedmioty;
            listView.IsEnabled = false;
            rezerwujButton.IsEnabled = false;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyPrzedmiot = (Przedmiot)listView.SelectedItem;
            if (wybranyPrzedmiot != null)
            {
                nazwaPrzedmiotuLabel.Content = wybranyPrzedmiot.Nazwa;
                idPrzedmiotuLabel.Content = wybranyPrzedmiot.Id;
            }
            
        }

        private void WyborDaty_SelectionDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (koniecGodzinaControl.Value != null && startGodzinaControl.Value != null)
            {
                aktywujKontrolki();
                SprawdzDostepnoscPrzedmiotow();
            }
        }

        private void StartGodzina_ValueChanged(Object sender, EventArgs e)
        {
            DateTime? ts = startGodzinaControl.Value;
            if (ts.HasValue)
            {
                koniecGodzinaControl.StartTime = ts.Value.TimeOfDay.Add(new TimeSpan(0, 15, 0));
                if (koniecGodzinaControl.Value != null && wyborDatyControl.SelectedDate != null)
                {
                    aktywujKontrolki();
                    SprawdzDostepnoscPrzedmiotow();
                }
            }            
            
        }

        private void KoniecGodzina_ValueChanged(object sender, EventArgs e)
        {
            DateTime? ts = koniecGodzinaControl.Value;
            if (ts.HasValue)
            {
                startGodzinaControl.EndTime = ts.Value.TimeOfDay.Add(new TimeSpan(0, -15, 0));
                if (startGodzinaControl.Value != null && wyborDatyControl.SelectedDate != null)
                {
                    aktywujKontrolki();
                    SprawdzDostepnoscPrzedmiotow();
                }
            }
        }

        private void SprawdzDostepnoscPrzedmiotow()
        {
            AktualizujPrzedzialCzasu();
            dostepnePrzedmioty.Clear();
            BazaMetody.ZwrocDostepnePrzedmiotyWTerminie(odDaty, doDaty).ForEach(dostepnePrzedmioty.Add);
        }

        private void AktualizujPrzedzialCzasu()
        {
            DateTime dataDzien = wyborDatyControl.SelectedDate.Value;
            DateTime godzinaStart = startGodzinaControl.Value.Value;
            DateTime godzinaKoniec = koniecGodzinaControl.Value.Value;
            odDaty = new DateTime(dataDzien.Year, dataDzien.Month, dataDzien.Day,
                                godzinaStart.Hour, godzinaStart.Minute, 0);
            doDaty = new DateTime(dataDzien.Year, dataDzien.Month, dataDzien.Day,
                                godzinaKoniec.Hour, godzinaKoniec.Minute, 0);
        }

        private void rezerwujButton_Click(object sender, RoutedEventArgs e)
        {
            var test = listView.SelectedItems;
            if (test.Count > 0)
            {
                WprowadzRezerwacje();                
            }
            else
            {
                MessageBox.Show("Wybierz Przedmiot/y!");
            }
        }

        private void WprowadzRezerwacje()
        {
            Rezerwacja rezerwacja = new Rezerwacja() { KlientId = klient.Id, OdDaty = odDaty, DoDaty = doDaty, Status = Rezerwacja.StatusRezerwacji.OCZEKUJACA };
            var rezerwowanePrzedmioty = listView.SelectedItems;
            foreach (Przedmiot item in rezerwowanePrzedmioty)
            {
                rezerwacja.RezerwujPrzedmiot(item);
            }
            
            BazaMetody.UtworzNowaRezerwacje(rezerwacja);
        }
        
        private void aktywujKontrolki()
        {
            listView.IsEnabled = true;
            rezerwujButton.IsEnabled = true;
        }
    }
}
