using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Widoki;

namespace CentrumSportu_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Switcher.pageSwitcher = this;
        }

        private void ZalogujBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string haslo = PasswordBox.Password;
            var Konto= BazaMetody.SprawdzLoginiHaslo(login, haslo);
            if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.NieStudent)
            {
                UczestnikZajec uczestnik = BazaMetody.ZwrocUczestnika(Konto);
                if (uczestnik != null)
                {
                    okno_uczestnika okno = new okno_uczestnika(uczestnik);
                    okno.Show();
                    this.Close();
                }
            }

            if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Instruktor)
            {
                Instruktor instruktor = BazaMetody.ZwrocInstruktora(Konto);
                if (instruktor != null)
                {
                    okno_instruktor okno = new okno_instruktor(instruktor);
                    okno.Show();
                    this.Close();
                }
            }
            else if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Administrator)
            {
                Administrator administrator = BazaMetody.ZwrocAdministratora(Konto);
                if (administrator != null)
                {
                    okno_administrator okno = new okno_administrator(administrator);
                    okno.Show();
                    this.Close();
                }
            }
            else if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Student)
            {
                Student student = BazaMetody.ZwrocStudenta(Konto);
                if (student != null)
                {
                    okno_student okno = new okno_student(student);
                    okno.Show();
                    this.Close();
                }
            }
            else if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.PracownikWypozyczalni)
            {
                Pracownik pracownikWypozyczalni = BazaMetody.ZwrocPracownika(Konto);
                if (pracownikWypozyczalni != null)
                {
                    OknoWypozyczalnia okno = new OknoWypozyczalnia(pracownikWypozyczalni);
                    okno.Show();
                    this.Close();
                }
            }
        }

        private void GoscBtn_Click(object sender, RoutedEventArgs e)
        {
            OfertaWidok widok = new OfertaWidok();
            //widok.fromWhere = 0;
            Switcher.Switch(widok);
        }
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

    }
}
