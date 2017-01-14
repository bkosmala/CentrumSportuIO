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
    /// Logika interakcji dla klasy Okno_Dodaj_Termin.xaml
    /// </summary>
    public partial class Okno_Dodaj_Termin : Window
    {
        private Grupa grupa;
        private Instruktor instruktor;
        public bool czyDodano = false;

        public Okno_Dodaj_Termin(Instruktor inst, Grupa grupa)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            instruktor = inst;
            this.grupa = grupa;
            var temp = BazaMetody.ZwrocListeObiektowDlaDanejDyscypliny(this.grupa.Dyscyplina);
            foreach (var item in temp)
            {
                SalaComboBox.Items.Add(item.Nazwa);
            }
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var obiekty = BazaMetody.ZwrocListeObiektowDlaDanejDyscypliny(grupa.Dyscyplina);
                ObiektSportowy obiekt = obiekty.FirstOrDefault(x => x.Nazwa == SalaComboBox.Text);
                int dlugosc = Int32.Parse(DlugoscTextBox.Text);
                DateTime dataRozpoczecia = (DateTime)DateTimePicker.Value;
                DateTime dataZakonczenia = dataRozpoczecia.AddMinutes(dlugosc);
                if (BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia, dataZakonczenia))
                {
                    WpisZajecia wpis = new WpisZajecia()
                    {
                        ObiektSportowy = obiekt,
                        Instruktor = instruktor,
                        Grupa = null,
                        DataRozpoczecia = dataRozpoczecia,
                        DataZakonczenia = dataZakonczenia,
                        DlugoscZajec = dlugosc
                    };
                    BazaMetody.DodajNowyTerminDoInstniejacejGrupy(wpis,grupa);
                    czyDodano = true;
                    var okno = Xceed.Wpf.Toolkit.MessageBox.Show("Termin dodano !!!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (okno == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Termin jest już zajety !!! Wybierz inny", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
