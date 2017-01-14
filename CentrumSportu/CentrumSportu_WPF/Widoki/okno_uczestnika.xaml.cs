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
    /// Interaction logic for okno_uczestnika.xaml
    /// </summary>
    public partial class okno_uczestnika : Window
    {    
    private ObservableCollection<Bilet> bilety;
        private UczestnikZajec uczestnik;


    public okno_uczestnika(UczestnikZajec _uczestnik)
    {
        InitializeComponent();
        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        uczestnik = _uczestnik;
        imieStudentLabel.Content = uczestnik.Imie;
        nazwiskoStudentLabel.Content = uczestnik.Nazwisko;
        emailStudentLabel.Content = uczestnik.Email;
        telefonStudentLabel.Content = uczestnik.Telefon;
        zdjecieProfiloweStudentImage.Source = new BitmapImage(new Uri(uczestnik.Zdjecie));

        bilety = new ObservableCollection<Bilet>(BazaMetody.ZwrocBiletyUzytkownika(uczestnik));
        this.BiletyListView.ItemsSource = bilety;



    }

    private void WylogujStudentButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow okno = new MainWindow();
        okno.Show();
        this.Close();
    }


    private void BiletyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        Bilet bilet = (Bilet)BiletyListView.SelectedItem;
        if (bilet != null)
        {
            this.nazwaL.Content = bilet.Zajecia.Zajecia.Nazwa;
            this.opisL.Content = bilet.Zajecia.Zajecia.Opis;
            this.DyscyplinaL.Content = bilet.Zajecia.Zajecia.Dyscyplina.Nazwa;
            this.dlugoscL.Content = bilet.Zajecia.DlugoscZajec + " min";
        }
        else
        {
            this.nazwaL.Content = " ";
            this.opisL.Content = " ";
            this.DyscyplinaL.Content = " ";
            this.dlugoscL.Content = " ";
        }

        if (bilet.Zajecia.DataRozpoczecia > DateTime.Now)
            this.oddajBilet.IsEnabled = false;
        else
            this.oddajBilet.IsEnabled = true;

    }

    private void oddajBilet_Click(object sender, RoutedEventArgs e)
    {
        Bilet bilet = (Bilet)BiletyListView.SelectedItem;



        bool flaga = BazaMetody.UsunBilet(bilet);
        if (flaga)
        {
            MessageBox.Show("Bilet Zostal usuniety");
            bilet.Zajecia.Grupa.UsunUczestnika(uczestnik.Id);
            bilety = new ObservableCollection<Bilet>(BazaMetody.ZwrocBiletyUzytkownika(uczestnik));
            this.BiletyListView.ItemsSource = bilety;
        }

        else
            MessageBox.Show("Wystapil blad");





    }

    private void nowyBilet_Click(object sender, RoutedEventArgs e)
    {
        OfertaWidok widok = new OfertaWidok();
        //widok.fromWhere = 1;
        Switcher.Switch(widok);
    }
}
}
