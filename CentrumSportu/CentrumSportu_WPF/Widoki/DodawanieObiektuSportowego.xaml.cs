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
using System.Windows.Shapes;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for DodawanieObiektuSportowego.xaml
    /// </summary>
    public partial class DodawanieObiektuSportowego : Window
    {
        private okno_administrator okno;
        private List<Dyscyplina> dyscypliny;
        private List<DyscyplinaWybrane> wybory;
        public DodawanieObiektuSportowego(okno_administrator okno)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.okno = okno;
            dyscypliny = BazaMetody.ZwrocWszystkieDyscypliny();
            wybory = new List<DyscyplinaWybrane>();
            foreach (Dyscyplina d in dyscypliny)
            {
                wybory.Add(new DyscyplinaWybrane { IsSelected = false, dyscyplina = d });
            }
            this.listaDyscyplin.ItemsSource = this.wybory;
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            okno.odśwież();
        }

        private void DodajObiektButton_Click(object sender, RoutedEventArgs e)
        {
            ObiektSportowy obiekt = null;
            string nazwa = NazwaObiektuTextBox.Text;
            int maxUczestnikow = int.Parse(MaxUczestnikowTextBox.Text);
            int iloscMiejscTrybuny = int.Parse(PojemnosctrybunTextBox.Text);
            List<Dyscyplina> dyscyplinyObiektu = new List<Dyscyplina>();

            foreach (DyscyplinaWybrane d in this.wybory)
            {
                if (d.IsSelected)
                {
                    dyscyplinyObiektu.Add(d.dyscyplina);
                }
            }

            obiekt = new ObiektSportowy(nazwa, dyscyplinyObiektu, maxUczestnikow, iloscMiejscTrybuny);

            if (BazaMetody.DodajObiektSportowy(obiekt))
            {
                MessageBox.Show("Obiekt dodany");
                okno.odśwież();
                this.Close();
            }
            else
            {
                MessageBox.Show("Błąd dodawania obiektu sportowego");
                okno.odśwież();
            }
        }
    }
}
