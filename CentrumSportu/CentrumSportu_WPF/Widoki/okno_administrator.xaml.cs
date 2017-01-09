﻿using System;
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

        public okno_administrator(Administrator _administrator)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            administrator = _administrator;
            niestudenci = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocWszystkichNieStudentow());
            administratorzy = new ObservableCollection<Administrator>(BazaMetody.ZwrocWszystkichAdministratorow());
            studenci = new ObservableCollection<Student>(BazaMetody.ZwrocWszystkichStudentow());
            instruktorzy = new ObservableCollection<Instruktor>(BazaMetody.ZwrocWszystkichInstruktorow());

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
            dodawanieUzytkownika okno = new dodawanieUzytkownika(this);
            okno.Show();
        }


        public void odśwież()
        {
            studenci = new ObservableCollection<Student>(BazaMetody.ZwrocWszystkichStudentow());
            administratorzy = new ObservableCollection<Administrator>(BazaMetody.ZwrocWszystkichAdministratorow());
            niestudenci = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocWszystkichNieStudentow());
            instruktorzy = new ObservableCollection<Instruktor>(BazaMetody.ZwrocWszystkichInstruktorow());

            this.listaUzytkownicy.ItemsSource = niestudenci;
            this.listaStudenci.ItemsSource = studenci;
            this.listaInstruktorzy.ItemsSource = instruktorzy;
            this.listaAdministratorzy.ItemsSource = administratorzy;
        }

        private void UsunU_Click(object sender, RoutedEventArgs e)
        {
           bool flaga = BazaMetody.UsunNieStudenta(niestudenci[listaUzytkownicy.SelectedIndex]);

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
            bool flaga = BazaMetody.UsunStudenta(studenci[listaStudenci.SelectedIndex]);

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
            bool flaga = BazaMetody.UsunInstruktora(instruktorzy[listaInstruktorzy.SelectedIndex]);

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
            bool flaga = BazaMetody.UsunAdministratora(administratorzy[listaAdministratorzy.SelectedIndex]);

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
