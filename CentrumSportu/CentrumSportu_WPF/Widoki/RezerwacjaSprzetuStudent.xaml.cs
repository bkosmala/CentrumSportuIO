using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_oferty;
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
    /// Interaction logic for WypozyczanieSprzetuStudent.xaml
    /// </summary>
    public partial class WypozyczanieSprzetuStudent : Window
    {
        private ObservableCollection<Przedmiot> dostepnePrzedmioty;
       
        public WypozyczanieSprzetuStudent(okno_student o)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today);
            CalendarDateRange cdr2 = new CalendarDateRange(DateTime.Today.AddDays(22), DateTime.MaxValue);
            wyborDatyControl.BlackoutDates.Add(cdr);
            wyborDatyControl.BlackoutDates.Add(cdr2);
            startGodzinaControl.TimeInterval = new TimeSpan(0, 15, 0);
            //startGodzinaControl.DefaultValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 7, 0, 0);
            startGodzinaControl.EndTime = new TimeSpan(21, 0, 0);
            startGodzinaControl.StartTime = new TimeSpan(7, 0, 0);
            koniecGodzinaControl.TimeInterval = new TimeSpan(0, 15, 0);
            //koniecGodzinaControl.DefaultValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 22, 0, 0);
            koniecGodzinaControl.EndTime = new TimeSpan(22, 0, 0);
            koniecGodzinaControl.StartTime = new TimeSpan(7, 15, 0);
            

            dostepnePrzedmioty = new ObservableCollection<Przedmiot>(BazaMetody.ZwrocWszystkiePrzedmioty());
            listView.ItemsSource = dostepnePrzedmioty;
            listView.IsEnabled = false;
        }
        
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyPrzedmiot = (Przedmiot)listView.SelectedItem;
            if (wybranyPrzedmiot != null)
            {
                label3.Content = wybranyPrzedmiot.Nazwa;
                textBox.Text = wybranyPrzedmiot.Nazwa;
            }
            
        }

        private void wyborDaty_SelectionDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (koniecGodzinaControl.Value != null && startGodzinaControl.Value != null)
            {
                listView.IsEnabled = false;
                DateTime dataDzien = wyborDatyControl.SelectedDate.Value;
                DateTime godzinaStart = startGodzinaControl.Value.Value;
                DateTime godzinaKoniec = koniecGodzinaControl.Value.Value;
                DateTime startDate = new DateTime(dataDzien.Year, dataDzien.Month, dataDzien.Day,
                                    godzinaStart.Hour, godzinaStart.Minute, 0);
                DateTime endDate = new DateTime(dataDzien.Year, dataDzien.Month, dataDzien.Day,
                                    godzinaKoniec.Hour, godzinaKoniec.Minute, 0);

                dostepnePrzedmioty.Clear();
                BazaMetody.ZwrocDostepnePrzedmiotyWTerminie(startDate, endDate).ForEach(dostepnePrzedmioty.Add);
            }
        }
    }
}
