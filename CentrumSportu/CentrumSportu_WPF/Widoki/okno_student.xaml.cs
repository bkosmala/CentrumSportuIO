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
using CentrumSportu_WPF.Modul_biletow;
using System.Collections.ObjectModel;
using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
using System.ComponentModel;


namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_student.xaml
    /// </summary>
    public partial class okno_student : Window
    {
        private Student student;
        private ObservableCollection<Bilet> bilety;
        private ObservableCollection<Rezerwacja> rezerwacje;
        
        public okno_student(Student _student)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            student = _student;
            imieStudentLabel.Content = student.Imie;
            nazwiskoStudentLabel.Content = student.Nazwisko;
            emailStudentLabel.Content = student.Email;
            telefonStudentLabel.Content = student.Telefon;
            if(!string.IsNullOrEmpty(student.Zdjecie))
                zdjecieProfiloweStudentImage.Source = new BitmapImage(new Uri(student.Zdjecie));

            rezerwacje = new ObservableCollection<Rezerwacja>(BazaMetody.ZwrocRezerwacjeStudenta(student.Id));
            
            rezerwacjeListView.ItemsSource = rezerwacje;
            comboBoxRezerwacje.SelectedIndex = 0;

            bilety = new ObservableCollection<Bilet>(BazaMetody.ZwrocBiletyUzytkownika(student));
            this.BiletyListView.ItemsSource = bilety;

            if (student.Grupa != null)
            {
                this.grupaL.Content = student.Grupa.Nazwa;
                this.znajdzGrupe.Visibility = Visibility.Hidden;
                
            }
            else
            {
                this.grupaL.Content = "brak grupy";
                this.odejdzzGrupy.Visibility = Visibility.Hidden;
            }
            


        }

        private void WylogujStudentButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }
        private void comboBoxRezerwacje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshRezerwacjeListView();      
        }

        private void rezerwacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (rezerwacjeListView.SelectedItems.Count < 1) return;
            if (((Rezerwacja)rezerwacjeListView.SelectedItem).Status == Rezerwacja.StatusRezerwacji.OCZEKUJACA)
            {
                anulujButton.IsEnabled = true;
            }
            else
            {
                anulujButton.IsEnabled = false;
            }
            var rezerwacjaTmp = (Rezerwacja)rezerwacjeListView.SelectedItems[0];
            textBlock1.Text = String.Empty;
            foreach (var item in rezerwacjaTmp.Przedmioty)
            {
                textBlock1.Text += item.Nazwa + "\n";
            }
        }

        private void WypozyczButton_Click(object sender, RoutedEventArgs e)
        {
            WypozyczanieSprzetuStudent okno = new WypozyczanieSprzetuStudent(this, this.student);
            okno.ShowDialog();
            RefreshRezerwacjeListView();
        }

        private void anulujButton_Click(object sender, RoutedEventArgs e)
        {
            var rezerwacja = (Rezerwacja)rezerwacjeListView.SelectedItem;
            rezerwacja.Status = Rezerwacja.StatusRezerwacji.ANULOWANA;
            BazaMetody.AnulujRezerwacje(rezerwacja);
            RefreshRezerwacjeListView();
        }

        private void RefreshRezerwacjeListView()
        {
            rezerwacje.Clear();
            if (comboBoxRezerwacje.SelectedIndex == 0)
            {
                BazaMetody.ZwrocRezerwacjeStudenta(student.Id).ForEach(rezerwacje.Add);
            }
            else
            {
                BazaMetody.PobierzRezerwacjeStudentaWedlugStatusu(student.Id, (Rezerwacja.StatusRezerwacji)comboBoxRezerwacje.SelectedIndex).ForEach(rezerwacje.Add);
            }
        }

        private void BiletyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Bilet bilet = (Bilet)BiletyListView.SelectedItem;

            if (bilet!=null)
            {
                Zajecia zz = BazaMetody.zwrocZajeciaBiletu(bilet);
                if (zz != null)
                {
                    this.nazwaL.Content = zz.Nazwa;
                    this.opisL.Content = zz.Opis;
                    //this.DyscyplinaL.Content = zz.Dyscyplina.Nazwa;
                    this.dlugoscL.Content = bilet.Zajecia.DlugoscZajec + " min";
                }
            }
            else
            {
                this.nazwaL.Content = " ";
                this.opisL.Content = " ";
                this.DyscyplinaL.Content = " ";
                this.dlugoscL.Content = " ";
            }

        }

        private void oddajBilet_Click(object sender, RoutedEventArgs e)
        {
            Bilet bilet = (Bilet)BiletyListView.SelectedItem;



            bool flaga = BazaMetody.UsunBilet(bilet);
            if (flaga)
            {
                MessageBox.Show("Bilet Zostal usuniety");
                bilet.Zajecia.Grupa.UsunUczestnika(student.Id);
                bilety = new ObservableCollection<Bilet>(BazaMetody.ZwrocBiletyUzytkownika(student));
                this.BiletyListView.ItemsSource = bilety;
            }

            else
                MessageBox.Show("Wystapil blad");
        }

        private void nowyBilet_Click(object sender, RoutedEventArgs e)
        {
            dodajBilet okno = new dodajBilet(this, student);
            okno.Show();
        }

        public void odsiwez()
        {
            bilety = new ObservableCollection<Bilet>(BazaMetody.ZwrocBiletyUzytkownika(student));
            this.BiletyListView.ItemsSource = bilety;
        }

        private void znajdzGrupe_Click(object sender, RoutedEventArgs e)
        {
            wybranieGrupy okno = new wybranieGrupy(student, this);
            okno.Show();
            

        }

        public void zmienG()
        {
            this.grupaL.Content = student.Grupa.Nazwa;
            this.odejdzzGrupy.Visibility = Visibility.Visible;
            this.znajdzGrupe.Visibility = Visibility.Hidden;
            
        }


        private void odejdzzGrupy_Click(object sender, RoutedEventArgs e)
        {
            BazaMetody.OdejdzZGrupu(student, student.Grupa.Id);
            student.odejdzZGrupy();
            this.grupaL.Content = student.Grupa;
            this.odejdzzGrupy.Visibility = Visibility.Hidden;
            this.znajdzGrupe.Visibility = Visibility.Visible;
            

        }
    }
}
