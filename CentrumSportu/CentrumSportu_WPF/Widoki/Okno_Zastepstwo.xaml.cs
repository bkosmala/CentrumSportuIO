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
    /// Interaction logic for Okno_Zastepstwo.xaml
    /// </summary>
    public partial class Okno_Zastepstwo : Window
    {
        private WpisZajecia wpisZajecia;
        private List<Instruktor> Instruktorzy;
        public bool czyWybrano = false;

        public Okno_Zastepstwo(WpisZajecia wpis)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            wpisZajecia = wpis;
            Instruktorzy = BazaMetody.ZwrocWszystkichInstrutorowDlaDanejDyscypliny(wpisZajecia.Grupa.Dyscyplina);
            var temp = Instruktorzy.FirstOrDefault(x => x.Id == wpisZajecia.Instruktor.Id);
            Instruktorzy.Remove(temp);
            foreach (var item in Instruktorzy)
            {
                InstruktorzyComboBox.Items.Add(item);
            }

        }

        private void WyslijButton_Click(object sender, RoutedEventArgs e)
        {
            var instruktor = (Instruktor)InstruktorzyComboBox.SelectedItem;
            if (instruktor != null)
            {
                Zdarzenie zdarzenie = new Zdarzenie()
                {
                    TypZdarzenia = Zdarzenie.RodzajZdarzenia.Zastepstwo,
                    Message = "Prośba o zastepstwo w sprawie zajęć : " + wpisZajecia.DataRozpoczecia + " " + wpisZajecia.Grupa.Nazwa
                };
                instruktor.Zdarzenia.Add(zdarzenie);
                BazaMetody.AktualizujInstruktora(instruktor);
                Xceed.Wpf.Toolkit.MessageBox.Show("Prośba została wysłana !!!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Wybierz instruktora !!!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
