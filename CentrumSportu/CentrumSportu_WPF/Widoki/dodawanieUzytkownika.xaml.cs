using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_biletow;
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
using System.Windows.Shapes;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for dodawanieUzytkownika.xaml
    /// </summary>
    public partial class dodawanieUzytkownika : Window
    {
        okno_administrator okno;
        List<Dyscyplina> dyscypliny;
        List<DyscyplinaWybrane> wybory;


        public dodawanieUzytkownika(okno_administrator o)
        {

            InitializeComponent();
            typKontaCB.Items.Add("student");
            okno = o;
            typKontaCB.Items.Add("administrator");
            typKontaCB.Items.Add("zwykły użytkownik");
            typKontaCB.Items.Add("instruktor");
            dyscypliny = BazaMetody.ZwrocWszystkieDyscypliny();
            wybory = new List<DyscyplinaWybrane>();
            foreach (Dyscyplina d in dyscypliny)
            {
                wybory.Add(new DyscyplinaWybrane { IsSelected = false, dyscyplina = d });
            }
            this.listaDyscyplin.ItemsSource = wybory;

        }



        private void dodajUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            string imie = this.imieTB.Text;
            string nazwisko = this.nazwiskoTB.Text;
            string login = this.loginTB.Text;
            string haslo = this.hasloTB.Text;
            KontoUzytkownika.RodzajKonta rk = KontoUzytkownika.RodzajKonta.Student;
            string email = this.emailTB.Text;
            string telefon = this.telefonTB.Text;
            List<Dyscyplina> dyscyplinyInstruktora = new List<Dyscyplina>();


            foreach (DyscyplinaWybrane d in wybory)
            {
                if (d.IsSelected)
                {
                    dyscyplinyInstruktora.Add(d.dyscyplina);
                }

            }




            switch (this.typKontaCB.Text)
            {
                case "student":
                    rk = KontoUzytkownika.RodzajKonta.Student;
                    break;
                case "administrator":
                    rk = KontoUzytkownika.RodzajKonta.Administrator;
                    break;
                case "instruktor":
                    rk = KontoUzytkownika.RodzajKonta.Instruktor;
                    break;
                case "zwykły użytkownik":
                    rk = KontoUzytkownika.RodzajKonta.NieStudent;
                    break;
            }

            KontoUzytkownika konto = new KontoUzytkownika(login, haslo, rk);
            bool flaga = false;

            switch (this.typKontaCB.Text)
            {
                case "student":
                    Student o = new Student(imie, nazwisko, konto, email, telefon);
                    flaga = BazaMetody.DodajStudenta(o);

                    break;
                case "administrator":
                    Administrator a = new Administrator(imie, nazwisko, konto);
                    flaga = BazaMetody.DodajAdministratora(a);
                    break;
                case "instruktor":
                    Instruktor i = new Instruktor(imie, nazwisko, email, telefon, dyscyplinyInstruktora, konto);
                    flaga = BazaMetody.DodajInstruktora(i);

                    break;
                case "zwykły użytkownik":
                    UczestnikZajec u = new UczestnikZajec(imie, nazwisko,  konto, email, telefon);
                    flaga = BazaMetody.DodajUczestnika(u);
                    break;

            }

            if (flaga)
            {
                MessageBox.Show("Uzytkownik został dodany");
                okno.odśwież();
            }
            else
                MessageBox.Show("Wystąpił błąd");







        }
    }


    public class DyscyplinaWybrane
    {
        public bool IsSelected { get; set; }
        public Dyscyplina dyscyplina { get; set; }

    }




}
