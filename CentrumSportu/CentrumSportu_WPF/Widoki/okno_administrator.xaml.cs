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

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_administrator.xaml
    /// </summary>
    public partial class okno_administrator : Window
    {
        private Administrator administrator;
        private List<UczestnikZajec> niestudenci;
        private List<Administrator> administratorzy;
        private List<Student> studenci;
        private List<Instruktor> instruktorzy;

        public okno_administrator(Administrator _administrator)
        {
            InitializeComponent();
            administrator = _administrator;
            niestudenci = BazaMetody.ZwrocWszystkichNieStudentow();
            administratorzy = BazaMetody.ZwrocWszystkichAdministratorow();
            studenci = BazaMetody.ZwrocWszystkichStudentow();
            instruktorzy = BazaMetody.ZwrocWszystkichInstruktorow();

            // ---------------------------------------------------------------------------------- PROFIL
            this.imieTB.Text = administrator.Imie;
            this.nazwiskoTB.Text = administrator.Nazwisko;


            // ---------------------------------------------------------------------------------- NIE STUDENCI
            this.listaUzytkownicy.ItemsSource = niestudenci;



            // ---------------------------------------------------------------------------------- STUDENCI

            this.listaStudenci.ItemsSource = studenci;


            // ---------------------------------------------------------------------------------- INSTRUKTORZY

            this.listaInstruktorzy.ItemsSource = instruktorzy;


            // ---------------------------------------------------------------------------------- ADMINISTRATORZY

            this.listaAdministratorzy.ItemsSource = administratorzy;





    }

        private void DodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            dodawanieUzytkownika okno = new dodawanieUzytkownika();
            okno.Show();
        }

    }
}
