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

        public okno_instruktor(Instruktor _instruktor)
        {
            InitializeComponent();
            instruktor = _instruktor;
            najblizszeZajecia = BazaMetody.ZwrocNajblizszeZajeciaDlaInstruktora(instruktor);
            grupy =new ObservableCollection<Grupa>(instruktor.Grupy); 
                   

            //PROFIL
            imie_textBlock.Text = instruktor.Imie;
            nazwisko_textBlock.Text = instruktor.Nazwisko;
            telefon_textBlock.Text = instruktor.Telefon;
            email_textBlock.Text = instruktor.Email;
            zdjecie_profilowe.Source=new BitmapImage(new Uri(instruktor.Zdjęcie));
            if (najblizszeZajecia != null)
            {
                
            }
            else
            {
                brakZajec_label.Visibility = Visibility.Visible;
            }

            //HARMONOGRAM
            GrupyComboBox.Items.Add("Wszystkie grupy");
            foreach (var item in grupy)
            {
                GrupyComboBox.Items.Add(item.Nazwa);
            }
            GrupyComboBox.SelectedIndex = 0;
            HarmonogramListView.ItemsSource = zajecia;
        }

        private void DodajGrupeButton_Click(object sender, RoutedEventArgs e)
        {
            TworzenieGrupyWindow okno=new TworzenieGrupyWindow(instruktor);
            okno.Show();
        }

        private void GrupyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GrupyComboBox.SelectedIndex == 0)
            {
                zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
            }
            else
            {
                string groupName = (string) GrupyComboBox.SelectedItem;
                int groupId = 0;
                foreach (var item in grupy)
                {
                    if (item.Nazwa == groupName)
                        groupId = item.Id;
                }
                zajecia= new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktoraiDanejGrupy(instruktor,groupId));
            }
            HarmonogramListView.ItemsSource = zajecia;

        }
    }
}
