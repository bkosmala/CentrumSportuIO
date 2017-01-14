using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
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
    /// Interaction logic for DodawanieNowegoSprzetuWindow.xaml
    /// </summary>
    public partial class DodawanieNowegoSprzetuWindow : Window
    {
        okno_administrator okno;
        public DodawanieNowegoSprzetuWindow(okno_administrator okno)
        {
            InitializeComponent();
            this.okno = okno;
            this.DostepnoscComboBox.Items.Add("Dostępny");
            this.DostepnoscComboBox.Items.Add("Niedostępny");
            this.DostepnoscComboBox.SelectedIndex = 0;
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            okno.odśwież();
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            Przedmiot przedmiot = new Przedmiot();
            string nazwa = this.NazwaSprzetuTextBox.Text;
            string dostepnosc = this.DostepnoscComboBox.Text;
            if (dostepnosc == "Dostępny")
            {
                przedmiot.Nazwa = nazwa;
                przedmiot.Dostepnosc = true;
            }
            else if (dostepnosc == "Niedostępny")
            {
                przedmiot.Nazwa = nazwa;
                przedmiot.Dostepnosc = false;
            }
            if (BazaMetody.DodajPrzedmiot(przedmiot))
            {
                MessageBox.Show("Dodano nowy przedmiot");
                okno.odśwież();
                this.Close();
            }
            else
            {
                MessageBox.Show("Błąd dodawania przedmiotu");
                okno.odśwież();
            }



        }
    }
}
