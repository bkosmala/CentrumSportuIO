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
using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
using CentrumSportu_WPF.Modul_biletow;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_instruktor.xaml
    /// </summary>
    public partial class okno_instruktor : Window
    {
        private Instruktor instruktor;
        private WpisZajecia najblizszeZajecia;
        private ObservableCollection<Grupa> grupy;
        private ObservableCollection<WpisZajecia> zajecia;
        private ObservableCollection<UczestnikZajec> uczestnicyZajec;
        

        public okno_instruktor(Instruktor _instruktor)
        {
            InitializeComponent();
            instruktor = _instruktor;
            RefreshData();

            //PROFIL
            imie_textBlock.Text = instruktor.Imie;
            nazwisko_textBlock.Text = instruktor.Nazwisko;
            telefon_textBlock.Text = instruktor.Telefon;
            email_textBlock.Text = instruktor.Email;
            zdjecie_profilowe.Source = new BitmapImage(new Uri(instruktor.Zdjęcie));
            if (najblizszeZajecia != null)
            {

            }
            else
            {
                brakZajec_label.Visibility = Visibility.Visible;
            }

            //HARMONOGRAM
            
        }

        private void RefreshData()
        {
            najblizszeZajecia = BazaMetody.ZwrocNajblizszeZajeciaDlaInstruktora(instruktor);
            grupy = new ObservableCollection<Grupa>(instruktor.Grupy);

            InformacjeOGrupachComboBox.Items.Clear();
            GrupyComboBox.Items.Clear();
            GrupyComboBox.Items.Add("Wszystkie grupy");
            InformacjeOGrupachComboBox.Items.Add("Wszystkie grupy");
            foreach (var item in grupy)
            {
                GrupyComboBox.Items.Add(item.Nazwa);
                InformacjeOGrupachComboBox.Items.Add(item.Nazwa);
            }
            GrupyComboBox.SelectedIndex = 0;
            HarmonogramListView.ItemsSource = zajecia;
            InformacjeOGrupachComboBox.SelectedIndex = 0;

        }

        private void DodajGrupeButton_Click(object sender, RoutedEventArgs e)
        {
            TworzenieGrupyWindow okno = new TworzenieGrupyWindow(instruktor);
            okno.ShowDialog();
            if (okno.isAdd == true)
            {
                instruktor=BazaMetody.ZwrocInstruktora(instruktor.KontoUzytkownika);
                RefreshData();
            }
            
        }

        private void GrupyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GrupyComboBox.SelectedIndex == 0)
            {
                zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
            }
            else
            {
                string groupName = (string)GrupyComboBox.SelectedItem;
                int groupId = 0;
                foreach (var item in grupy)
                {
                    if (item.Nazwa == groupName)
                        groupId = item.Id;
                }
                zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktoraiDanejGrupy(instruktor, groupId));
            }
            HarmonogramListView.ItemsSource = zajecia;

        }

        private void InformacjeOGrupachComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InformacjeOGrupachComboBox.SelectedIndex == 0)
            {
                zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
            }
            else
            {
                string groupName = (string)InformacjeOGrupachComboBox.SelectedItem;
                int groupId = 0;
                foreach (var item in grupy)
                {
                    if (item.Nazwa == groupName)
                    {
                        groupId = item.Id;
                        IdgrupyLabel.Content = item.Id;
                        NazwagrupyLabel.Content = item.Nazwa;
                        minLiczebnoscLabel.Content = item.MinLiczebnosc;
                        maxLiczebnoscLabel.Content = item.MaxLiczebnosc;
                    }
                }
                zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktoraiDanejGrupy(instruktor, groupId));
                uczestnicyZajec = new ObservableCollection<UczestnikZajec>(BazaMetody.ZwrocUczestnikowZajecDlaDanejGrupy(groupId));
            }
            UczestnicyGrupyListView.ItemsSource = uczestnicyZajec;
        }

        private void UsunUczestnika(object sender, MouseButtonEventArgs e)
        {
            string groupName = (string)InformacjeOGrupachComboBox.SelectedItem;
            int groupId = 0;
            

            ListView tmp = (ListView)sender;
            UczestnikZajec uczestnik = (UczestnikZajec)tmp.SelectedItem;
            MessageBoxResult result = MessageBox.Show("czy chcesz usunąć " + uczestnik.Imie + " " + uczestnik.Nazwisko + " z zajęć?", "Usuwanie", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                foreach (var item in grupy)
                {
                    if (item.Nazwa == groupName)
                        groupId = item.Id;                   
                }
                uczestnicyZajec = new ObservableCollection<UczestnikZajec>(BazaMetody.UsunUczestnikaZGrupy(groupId, uczestnik.Id));
                UczestnicyGrupyListView.ItemsSource = uczestnicyZajec;
            }
        }

        private void WyswietlOknoProfiloweUczestnika(object sender, MouseButtonEventArgs e)
        {
            string groupName = (string)InformacjeOGrupachComboBox.SelectedItem;
            int groupId = 0;
            ListView tmp = (ListView)sender;
            UczestnikZajec uczestnik = (UczestnikZajec)tmp.SelectedItem;

            foreach (var item in grupy)
            {
                if (item.Nazwa == groupName)
                    groupId = item.Id;
            }
            ProfilUczestnikaWindow profilUCzestnikaWindow = new ProfilUczestnikaWindow(BazaMetody.ZwrocWybranegoUczestnikaZajec(groupId, uczestnik.Id), groupId);
            profilUCzestnikaWindow.Show();
            profilUCzestnikaWindow.Closed += profilOkno_closed;

        }

        private void profilOkno_closed(object sender, EventArgs e)
        {
            ProfilUczestnikaWindow child = (ProfilUczestnikaWindow)sender;
            UczestnicyGrupyListView.ItemsSource = child.UczestnicyProfilUczestnikaCollection;
        }



    }
}
