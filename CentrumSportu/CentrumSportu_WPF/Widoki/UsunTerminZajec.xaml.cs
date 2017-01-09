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
    /// Logika interakcji dla klasy UsunTerminZajec.xaml
    /// </summary>
    public partial class UsunTerminZajec : Window
    {
        private WpisZajecia wpisZajecia;
        public bool czyUsuniety = false;
        public UsunTerminZajec(WpisZajecia wpis)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            wpisZajecia = wpis;
            InstruktorTextBlock.Text = wpisZajecia.Instruktor.Imie +" "+ wpisZajecia.Instruktor.Nazwisko;
            GrupaTextBlock.Text = wpisZajecia.Grupa.Nazwa;
            SalaTextBlock.Text = wpisZajecia.ObiektSportowy.Nazwa;
            DataRozpTextBlock.Text = wpisZajecia.DataRozpoczecia.ToString();
            DataZakonTextBlock.Text = wpisZajecia.DataZakonczenia.ToString();
            DlugoscTextBlock.Text = wpisZajecia.DlugoscZajec.ToString()+" "+"minut";
        }

        private void UsunButton_Click(object sender, RoutedEventArgs e)
        {
            BazaMetody.UsunTerminZajec(wpisZajecia.Id);
            czyUsuniety = true;
            Xceed.Wpf.Toolkit.MessageBox.Show("Usunieto poprawnie dany termin", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
