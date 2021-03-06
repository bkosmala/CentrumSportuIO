﻿using CentrumSportu_WPF.Baza_danych;
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
    /// Interaction logic for dodajBilet.xaml
    /// </summary>
    public partial class dodajBilet1 : Window
    {
        private okno_uczestnika okno;
        private UczestnikZajec uczestnik;
        private List<WpisZajecia> bilety;

        public dodajBilet1(okno_uczestnika o, UczestnikZajec u)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.okno = o;
            this.uczestnik = u;
            InitializeComponent();
            bilety = BazaMetody.ZwrocWszystkieGrupBiletowy();
            this.biletyDostepne.ItemsSource = bilety; 

        }

        private void wybierzB_Click(object sender, RoutedEventArgs e)
        {
            WpisZajecia wpis = (WpisZajecia)biletyDostepne.SelectedItem;


            BazaMetody.dodajBilet(uczestnik, wpis.Grupa.Id);
            BazaMetody.dodajdoGrypy(wpis.Grupa, uczestnik);
                MessageBox.Show("Dodano bilet");
            okno.odsiwez();
            this.Close();

        }


    }
}
