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
using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
using System.Collections.ObjectModel;
using CentrumSportu_WPF.Modul_oferty;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_administrator.xaml
    /// </summary>
    public partial class okno_administrator : Window
    {

        private Administrator administrator;
        private ObservableCollection<UczestnikZajec> niestudenci;
        private ObservableCollection<Administrator> administratorzy;
        private ObservableCollection<Student> studenci;
        private ObservableCollection<Instruktor> instruktorzy;
        private ObservableCollection<ObiektSportowy> obiekty;
        private ObservableCollection<Przedmiot> przedmioty;

        public okno_administrator(Administrator _administrator)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            administrator = _administrator;
            niestudenci = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocWszystkichNieStudentow());
            administratorzy = new ObservableCollection<Administrator>(BazaMetody.ZwrocWszystkichAdministratorow());
            studenci = new ObservableCollection<Student>(BazaMetody.ZwrocWszystkichStudentow());
            instruktorzy = new ObservableCollection<Instruktor>(BazaMetody.ZwrocWszystkichInstruktorow());
            obiekty = new ObservableCollection<ObiektSportowy>(BazaMetody.ZwrocWszystkieObiektySportoweOferta());
            przedmioty = new ObservableCollection<Przedmiot>(BazaMetody.ZwrocWszystkiePrzedmioty());

            // ---------------------------------------------------------------------------------- PROFIL
            //this.imieTB.Text = administrator.Imie;
            //this.nazwiskoTB.Text = administrator.Nazwisko;
            Title = administrator.Imie + " " + administrator.Nazwisko;


            // ---------------------------------------------------------------------------------- NIE STUDENCI
            //this.listaUzytkownicy.ItemsSource = niestudenci;
            this.ZwykliUzytkownicyListView.ItemsSource = niestudenci;


            // ---------------------------------------------------------------------------------- STUDENCI

           // this.listaStudenci.ItemsSource = studenci;
            this.StudenciListView.ItemsSource = studenci;


            // ---------------------------------------------------------------------------------- INSTRUKTORZY

            //this.listaInstruktorzy.ItemsSource = instruktorzy;
            this.InstruktorzyListView.ItemsSource = instruktorzy;


            // ---------------------------------------------------------------------------------- ADMINISTRATORZY

            //this.listaAdministratorzy.ItemsSource = administratorzy;
            this.AdministratorzyListView.ItemsSource = administratorzy;


            ZwykliUzytkownicyListView.SelectedIndex = 0;
            StudenciListView.SelectedIndex = 0;
            InstruktorzyListView.SelectedIndex = 0;
            AdministratorzyListView.SelectedIndex = 0;

            this.ObiektySportoweListView.ItemsSource = obiekty;
            this.SprzetListView.ItemsSource = przedmioty;

        }

        private void DodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            dodawanieUzytkownika okno = new dodawanieUzytkownika(this);
            okno.Show();
        }


        public void odśwież()
        {
            studenci = new ObservableCollection<Student>(BazaMetody.ZwrocWszystkichStudentow());
            administratorzy = new ObservableCollection<Administrator>(BazaMetody.ZwrocWszystkichAdministratorow());
            niestudenci = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocWszystkichNieStudentow());
            instruktorzy = new ObservableCollection<Instruktor>(BazaMetody.ZwrocWszystkichInstruktorow());

            this.ZwykliUzytkownicyListView.ItemsSource = niestudenci;
            this.StudenciListView.ItemsSource = studenci;
            this.InstruktorzyListView.ItemsSource = instruktorzy;
            this.AdministratorzyListView.ItemsSource = administratorzy;
        }

        private void UsunU_Click(object sender, RoutedEventArgs e)
        {
            
           bool flaga = BazaMetody.UsunNieStudenta(niestudenci[ZwykliUzytkownicyListView.SelectedIndex]);

            if (flaga)
            {
                MessageBox.Show("Uzytkownik został usuniety");
                odśwież();
            }
            else
                MessageBox.Show("Wystąpił błąd");

        }
        private void UsunS_Click(object sender, RoutedEventArgs e)
        {
            
            bool flaga = BazaMetody.UsunStudenta(studenci[StudenciListView.SelectedIndex]);

            if (flaga)
            {
                MessageBox.Show("Uzytkownik został usuniety");
                odśwież();
            }
            else
                MessageBox.Show("Wystąpił błąd");

        }
        private void UsunI_Click(object sender, RoutedEventArgs e)
        {
            
            bool flaga = BazaMetody.UsunInstruktora(instruktorzy[InstruktorzyListView.SelectedIndex]);

            if (flaga)
            {
                MessageBox.Show("Uzytkownik został usuniety");
                odśwież();
            }
            else
                MessageBox.Show("Wystąpił błąd");

        }
        private void UsunA_Click(object sender, RoutedEventArgs e)
        {
            
            bool flaga = BazaMetody.UsunAdministratora(administratorzy[AdministratorzyListView.SelectedIndex]);

            if (flaga)
            {
                MessageBox.Show("Uzytkownik został usuniety");
                odśwież();
            }
            else
                MessageBox.Show("Wystąpił błąd");

        }
    }
}
