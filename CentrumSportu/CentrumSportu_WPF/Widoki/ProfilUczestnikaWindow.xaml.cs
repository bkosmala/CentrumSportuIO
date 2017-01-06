using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_biletow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProfilUczestnikaWindow.xaml
    /// </summary>
    public partial class ProfilUczestnikaWindow : Window
    {
        private UczestnikZajec uczestnikZajec;
        private int idGrupyUczestnika;
        public ObservableCollection<UczestnikZajec> UczestnicyProfilUczestnikaCollection { get; set; }

        public ProfilUczestnikaWindow(UczestnikZajec uczestnikZajec, int idGrupyUczestnika)
        {
            
            this.uczestnikZajec = uczestnikZajec;
            this.idGrupyUczestnika = idGrupyUczestnika;
            InitializeComponent();
            imieLabel.Content = uczestnikZajec.Imie;
            nazwiskoLabel.Content = uczestnikZajec.Nazwisko;
            emailLabel.Content = uczestnikZajec.Email;
            telefonLabel.Content = uczestnikZajec.Telefon;
            zdjecieProfiloweImage.Source = new BitmapImage(new Uri(uczestnikZajec.Zdjecie));

        }

        private void anulujButton_Click(object sender, RoutedEventArgs e)
        {
            UczestnicyProfilUczestnikaCollection = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocUczestnikowZajecDlaDanejGrupy(idGrupyUczestnika));
            this.Close();
        }

        private void usunUczestnikaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("czy chcesz usunąć " + uczestnikZajec.Imie + " " + uczestnikZajec.Nazwisko + " z zajęć?", "Usuwanie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                UczestnicyProfilUczestnikaCollection = new ObservableCollection<UczestnikZajec>(BazaMetody.UsunUczestnikaZGrupy(idGrupyUczestnika, uczestnikZajec.Id));
                this.Close();
            }
        }
    }
}
