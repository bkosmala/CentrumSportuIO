using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl-PL");
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            instruktor = _instruktor;
            RefreshData();

            //PROFIL
            imie_textBlock.Text = instruktor.Imie;
            nazwisko_textBlock.Text = instruktor.Nazwisko;
            telefon_textBlock.Text = instruktor.Telefon;
            email_textBlock.Text = instruktor.Email;
            iloscGrup_textBlock.Text = instruktor.Grupy.Count.ToString();
            if (instruktor.Zdjecie != null)
            {
                zdjecie_profilowe.Source = new BitmapImage(new Uri(instruktor.Zdjecie));
            }
            else
            {
                zdjecie_profilowe.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+"../../Images/profilPhoto.jpg"));
            }
            if (najblizszeZajecia != null)
            {
                brakZajec_label.Visibility = Visibility.Hidden;
                WrapPanel.Visibility = Visibility.Visible;
                GrupaTextBlock.Text = najblizszeZajecia.Grupa.Nazwa;
                SalaTextBlock.Text = najblizszeZajecia.ObiektSportowy.Nazwa;
                DataTextBlock.Text = najblizszeZajecia.DataRozpoczecia.ToString();
                DlugoscTextBlock.Text = najblizszeZajecia.DlugoscZajec.ToString();
            }
            else
            {
                brakZajec_label.Visibility = Visibility.Visible;
                WrapPanel.Visibility = Visibility.Hidden;
            }

            
        }

        

        private void RefreshData()
        {
            najblizszeZajecia = BazaMetody.ZwrocNajblizszeZajeciaDlaInstruktora(instruktor);
            grupy = new ObservableCollection<Grupa>(instruktor.Grupy);

            InformacjeOGrupachComboBox.Items.Clear();
            GrupyComboBox.Items.Clear();
            GrupyComboBox.Items.Add("Wszystkie grupy");
  
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

        private void WylogujButton_Click(object sender, RoutedEventArgs e)
        {           
            MainWindow okno=new MainWindow();
            okno.Show();
            this.Close();
        }

        private void RozwiazGrupeButton_Click(object sender, RoutedEventArgs e)
        {
            RozwiazywanieGrupyWindow okno = new RozwiazywanieGrupyWindow(BazaMetody.ZwrocGrupeDlaWybranegoInstruktora(instruktor.Id, InformacjeOGrupachComboBox.SelectedItem.ToString()), instruktor);
            okno.Show();
            okno.Closed += RozwiazywanieGrupyOkno_Closed;
        }

        private void RozwiazywanieGrupyOkno_Closed(object sender, EventArgs e)
        {
            RozwiazywanieGrupyWindow child = (RozwiazywanieGrupyWindow)sender;
            foreach (var item in grupy)
            {
                GrupyComboBox.Items.Remove(item.Nazwa);
                InformacjeOGrupachComboBox.Items.Remove(item.Nazwa);
            }
            foreach (var item in child.Grupy)
            {
                GrupyComboBox.Items.Add(item.Nazwa);
                InformacjeOGrupachComboBox.Items.Add(item.Nazwa);
            }
            HarmonogramListView.ItemsSource = child.Zajecia;
            GrupyComboBox.SelectedIndex = 0;
            InformacjeOGrupachComboBox.SelectedIndex = 0;
        }

        private void UsunMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WpisZajecia wpis = (WpisZajecia)HarmonogramListView.SelectedItem;
            if (wpis != null)
            {
                UsunTerminZajec okno = new UsunTerminZajec(wpis);
                okno.ShowDialog();
                if (okno.czyUsuniety == true)
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
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Zaznacz jakiś termin !!!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }          
        }

        private void ZastepstwoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WpisZajecia wpis = (WpisZajecia)HarmonogramListView.SelectedItem;
            if (wpis != null)
            {
                Okno_Zastepstwo okno =new Okno_Zastepstwo(wpis);
                okno.ShowDialog();
                if (okno.czyWybrano == true)
                {

                }
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Zaznacz jakiś termin !!!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ModyfikujMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WpisZajecia wpis = (WpisZajecia)HarmonogramListView.SelectedItem;
            if (wpis != null)
            {
                ModyfikujTerminZajec okno = new ModyfikujTerminZajec(wpis);
                okno.ShowDialog();
                if (okno.czyModyfikowane == true)
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
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Zaznacz jakiś termin !!!", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void WywolajZdarzenia()
        {
            if (instruktor.Zdarzenia.Count != 0)
            {
                var temp = instruktor.Zdarzenia.Where(x => x.TypZdarzenia == Zdarzenie.RodzajZdarzenia.Zastepstwo).ToList();
                foreach (var item in temp)
                {
                    var okno=Xceed.Wpf.Toolkit.MessageBox.Show("Prośba o zastępstwo :"+ Environment.NewLine+item.Message, "Zastepstwo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (okno == MessageBoxResult.Yes)
                    {
                        item.WpisZajecia.Instruktor = instruktor;
                        BazaMetody.AktualizujInstruktoraWpisuZajec(item.WpisZajecia);
                        zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
                        HarmonogramListView.ItemsSource = zajecia;

                    }
                    else
                    {
                        //Odesłanie zdarzenia ze zastepstwo zostalo odrzucone
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WywolajZdarzenia();
        }
    }
}
