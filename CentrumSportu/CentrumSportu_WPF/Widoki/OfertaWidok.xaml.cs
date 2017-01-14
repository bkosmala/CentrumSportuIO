using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_oferty;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for OfertaWidok.xaml
    /// </summary>
    public partial class OfertaWidok : UserControl
    {
        public int fromWhere;
        // jeśli fromWhere = 1 - przejście od modułu biletów

        private ObservableCollection<Zajecia> listZajecia;
        private ObservableCollection<ObiektSportowy> listObiekty;
        private ObservableCollection<Wydarzenie> listWydarzenia;
        private ObservableCollection<Przedmiot> oferowanySprzet;
        private Zajecia selectedZajecia;
        private ObiektSportowy selectedObiekt;


        public OfertaWidok()
        {
            InitializeComponent();
            fromWhere = 0;
            listZajecia = new ObservableCollection<Zajecia>(BazaMetody.ZwrocWszystkieZajeciaOferta());
            //listWydarzenia = new ObservableCollection<Wydarzenie>();

            oferowanySprzet = new ObservableCollection<Przedmiot>(BazaMetody.ZwrocWszystkiePrzedmioty());

            listBoxZajecia.ItemsSource = listZajecia;
            listBoxSprzet.ItemsSource = oferowanySprzet;
            listBoxZajecia.SelectedIndex = 0;
            ZajeciaViewBox.DataContext = selectedZajecia;
            if (selectedZajecia.Dyscyplina != null) DyscyplinaLabel.Content = selectedZajecia.Dyscyplina.Nazwa;
            if (selectedZajecia.Dyscyplina != null)
            {
                var instruktorzy = BazaMetody.ZwrocListeInstruktorowDlaDanejDyscypliny(selectedZajecia.Dyscyplina);
                string napis = "";
                foreach (Instruktor ins in instruktorzy)
                { napis += ins.Imie + " " + ins.Nazwisko + "; "; }
                textBlockInstruktorzy.Text = napis;
            }
            if (selectedZajecia.CennikZajec != null) {
                var c = selectedZajecia.CennikZajec;
                textBlockCena.Text = c.CenaPodstawowa + c.Waluta + "/za godzinę";
            }
             else { textBlockCena.Text = ""; }

            listObiekty = new ObservableCollection<ObiektSportowy>(BazaMetody.ZwrocWszystkieObiektySportoweOferta());
            listBoxObiekty.ItemsSource = listObiekty;
            selectedObiekt = listObiekty.ElementAt(0);
            obiektyViewBox.DataContext = selectedObiekt;

            if (selectedObiekt.Zdjecie != null)
            {
                zdjecie_obiektu.Source = new BitmapImage(new Uri(selectedObiekt.Zdjecie));
            }
            zdjecie_obiektu.Visibility = System.Windows.Visibility.Hidden;

            //Wydarzenie a = new Wydarzenie("Koncert zespołu Beam");
            //listWydarzenia.Add(a);
            //listBoxObiekty.ItemsSource = listWydarzenia;
            //wydarzeniaViewBox.DataContext = listObiekty.ElementAt(0);
        }
        private void ListBoxZajecia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedZajecia = listZajecia.ElementAt(listBoxZajecia.SelectedIndex);
            ZajeciaViewBox.DataContext = selectedZajecia;
            if(selectedZajecia.Dyscyplina != null) DyscyplinaLabel.Content = selectedZajecia.Dyscyplina.Nazwa;
            if (selectedZajecia.Dyscyplina != null)
            {
                var instruktorzy = BazaMetody.ZwrocListeInstruktorowDlaDanejDyscypliny(selectedZajecia.Dyscyplina);
                string napis = "";
                foreach (Instruktor ins in instruktorzy)
                { napis += ins.Imie + " " + ins.Nazwisko + "; "; }
                textBlockInstruktorzy.Text = napis;
            }
            if (selectedZajecia.CennikZajec != null)
            {
                var c = selectedZajecia.CennikZajec;
                textBlockCena.Text = c.CenaPodstawowa + c.Waluta + "/za godzinę";
            }
            else { textBlockCena.Text = ""; }
        }

        private void ListBoxObiekty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedObiekt = listObiekty.ElementAt(listBoxObiekty.SelectedIndex);
            obiektyViewBox.DataContext = selectedObiekt;

            if (selectedObiekt.Zdjecie != null)
            {
                zdjecie_obiektu.Source = new BitmapImage(new Uri(selectedObiekt.Zdjecie));
            }
        }


        private void Tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tab.SelectedIndex==1)
            { zdjecie_obiektu.Visibility = System.Windows.Visibility.Visible; }
            else
            { zdjecie_obiektu.Visibility = System.Windows.Visibility.Hidden; }
        }

        private void ListBoxPrzedmiotySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Przedmiot selectedPrzedmiot = (listBoxSprzet.SelectedItem as Przedmiot);
            if (selectedPrzedmiot != null)
            {
                textBoxNazwaSprzetu.Text = selectedPrzedmiot.Nazwa;
            }
        }

        
    }
}
