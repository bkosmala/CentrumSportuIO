using CentrumSportu_WPF.Modul_oferty;
using System.Collections.ObjectModel;
using System.Windows;
using System;
using CentrumSportu_WPF.Baza_danych;
using System.Collections.Generic;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for NoweWypozyczenieOkno.xaml
    /// </summary>
    public partial class NoweWypozyczenieOkno : Window
    {
        private ObservableCollection<Przedmiot> dostepnePrzedmioty;
        private Osoba osoba;
        private Pracownik pracownik;

        public NoweWypozyczenieOkno(Osoba osoba, Pracownik pracownik)
        {
            this.osoba = osoba;
            this.pracownik = pracownik;
            dostepnePrzedmioty = new ObservableCollection<Przedmiot>();

            InitializeComponent();
            initializeControls();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            RefreshData();
        }

        private void RefreshData()
        {
            dostepnePrzedmioty.Clear();
            var dostepnePrzedmiotyTmp = BazaMetody.ZwrocDostepnePrzedmiotyWTerminie(DateTime.Now, DateTime.Now.AddMinutes(slider.Value));
            foreach (var item in dostepnePrzedmiotyTmp)
            {
                dostepnePrzedmioty.Add(item);
            }
            listView.ItemsSource = dostepnePrzedmioty;
        }

        private void initializeControls()
        {
            slider.Value = 30;
            czasLabel.Content = slider.Value;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (czasLabel != null)
            {
                czasLabel.Content = slider.Value;
                RefreshData();
            }            
        }

        private void wypozyczButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            if (listView.SelectedItems.Count > 0)
            {
                var rezerwacja = StworzRezerwacjeWypozyczenie();
                BazaMetody.UtworzNowaRezerwacje(rezerwacja);
                this.Close();
            }
            else
            {
                MessageBox.Show("Wybierz Przedmiot/y!");
            }
        }

        private Rezerwacja StworzRezerwacjeWypozyczenie()
        {
            var przedmioty = listView.SelectedItems;
            Wypozyczenie noweWypozyczenie = new Wypozyczenie(DateTime.Now, pracownik);
            BazaMetody.DodajWypozyczenie(noweWypozyczenie);
            Rezerwacja nowaRezerwacja = new Rezerwacja()
            {
                OdDaty = DateTime.Now,
                DoDaty = DateTime.Now.AddMinutes(slider.Value),
                KlientId = osoba.Id,
                Status = Rezerwacja.StatusRezerwacji.REALIZOWANA,
                Wypozyczenie = noweWypozyczenie
            };
            foreach (Przedmiot item in przedmioty)
            {
                nowaRezerwacja.RezerwujPrzedmiot(item);
            }
            return nowaRezerwacja;
        }
    }
}
