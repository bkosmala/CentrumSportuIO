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
using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Logika interakcji dla klasy ModyfikujTerminZajec.xaml
    /// </summary>
    public partial class ModyfikujTerminZajec : Window
    {
        private WpisZajecia wpisZajecia;
        private List<ObiektSportowy> obiektySportowe;
        public bool czyModyfikowane = false;

        public ModyfikujTerminZajec(WpisZajecia wpis)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            wpisZajecia = wpis;
            InstruktorTextBlock.Text = wpisZajecia.Instruktor.Imie + " " + wpisZajecia.Instruktor.Nazwisko;
            GrupaTextBlock.Text = wpisZajecia.Grupa.Nazwa;
            SalaTextBlock.Text = wpisZajecia.ObiektSportowy.Nazwa;
            DataRozpTextBlock.Text = wpisZajecia.DataRozpoczecia.ToString();
            DataZakonTextBlock.Text = wpisZajecia.DataZakonczenia.ToString();
            DlugoscTextBlock.Text = wpisZajecia.DlugoscZajec.ToString() + " " + "minut";
            obiektySportowe = BazaMetody.ZwrocListeObiektowDlaDanejDyscypliny(wpisZajecia.Grupa.Dyscyplina);
            ObiektyComboBox.Items.Clear();
            foreach (var item in obiektySportowe)
            {
                ObiektyComboBox.Items.Add(item.Nazwa);
            }
        }

        private void ZmienButton_Click(object sender, RoutedEventArgs e)
        {
            komunikatLabel.Visibility = Visibility.Hidden;
            try
            {
                int dlugosc = Int32.Parse(DlugoscTextBox.Text);
                DateTime dataRozp = (DateTime)DateTimePicker.Value;
                DateTime dataZakonczenia = dataRozp.AddMinutes(dlugosc);
                ObiektSportowy obiekt = obiektySportowe.FirstOrDefault(x => x.Nazwa == ObiektyComboBox.Text);
                if (obiekt != null)
                {
                    if (BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozp, dataZakonczenia))
                    {
                        wpisZajecia.DataRozpoczecia = dataRozp;
                        wpisZajecia.DataZakonczenia = dataZakonczenia;
                        wpisZajecia.DlugoscZajec = dlugosc;
                        wpisZajecia.ObiektSportowy = obiekt;
                        BazaMetody.AktualizujWpisZajec(wpisZajecia);
                        czyModyfikowane = true;
                        this.Close();
                    }
                    else
                    {
                        komunikatLabel.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
