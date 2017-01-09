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
using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using Xceed.Wpf.Toolkit;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for TworzenieGrupyWindow.xaml
    /// </summary>
    public partial class TworzenieGrupyWindow : Window
    {
        private Instruktor instruktor;
        private Grupa grupa;
        private List<ObiektSportowy> obiektySportowe;
        public bool isAdd = false;

        public TworzenieGrupyWindow(Instruktor _instruktor)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            instruktor = _instruktor;
            InitializeComponent();
            Init();

            
        }

        private void Init()
        {
            foreach (var item in instruktor.Dyscypliny)
            {
                DyscyplinaComboBox.Items.Add(item.Nazwa);
            }
            LiczebnoscMinUpDown.Minimum = 1;
            LiczebnoscMinUpDown.Value = 10;
            LiczebnoscMaxUpDown.Minimum = 10;
            LiczebnoscMaxUpDown.Value = 20;
            IloscTygodniUpDown.Maximum = 15;
            IloscTygodniUpDown.Minimum = 2;
            IloscZajecUpDown.Minimum = 1;
            IloscZajecUpDown.Maximum = 3;
            IloscZajecUpDown.Value = 1;
            DateTimePicker3.Visibility = Visibility.Hidden;
            DateTimePicker2.Visibility = Visibility.Hidden;
            LabelPicker2.Visibility = Visibility.Hidden;
            LabelPicker3.Visibility = Visibility.Hidden;

        }

        private void UtworzGrupeButton_Click(object sender, RoutedEventArgs e)
        {            
            try
            {


                Dyscyplina dyscyplina = instruktor.Dyscypliny.FirstOrDefault(x => x.Nazwa == DyscyplinaComboBox.Text);
                string nazwa = NazwaZajecTextBox.Text;
                ObiektSportowy obiekt = obiektySportowe.FirstOrDefault(x => x.Nazwa == ObiektSportowyComboBox.Text);
                int? liczebnoscMin = LiczebnoscMinUpDown.Value;
                int? liczebnoscMax = LiczebnoscMaxUpDown.Value;
                int dlugosc = Int32.Parse(DlugoscZajecTextBox.Text);
                Grupa grupa = new Grupa()
                {
                    Dyscyplina = dyscyplina,
                    Nazwa = nazwa,
                    Instruktor = instruktor,
                    MaxLiczebnosc = (int)liczebnoscMax,
                    MinLiczebnosc = (int)liczebnoscMin,
                    Uczestincy = new List<UczestnikZajec>()

                };
                if (JednorazoweCheckBox.IsChecked.Value == true)
                {
                    DateTime dataRozpoczecia = (DateTime)DateTimePicker1.Value;
                    DateTime dataZakonczenia = new DateTime(dataRozpoczecia.Year,dataRozpoczecia.Month,dataRozpoczecia.Day,dataRozpoczecia.Hour,dataRozpoczecia.Minute,dataRozpoczecia.Second);
                    dataZakonczenia=dataZakonczenia.AddMinutes(dlugosc);
                    if (BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia, dataZakonczenia))
                    {
                        WpisZajecia wpis=new WpisZajecia()
                        {
                            ObiektSportowy = obiekt,
                            Instruktor = instruktor,
                            Grupa = grupa,
                            DataRozpoczecia = dataRozpoczecia,
                            DataZakonczenia = dataZakonczenia,
                            DlugoscZajec = dlugosc
                        };
                        BazaMetody.DodajNowyTermin(wpis);
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Termin jest już zajety !!! Wybierz inny", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                }
                else
                {
                    int iloscZajecWTygodniu = (int)IloscZajecUpDown.Value;
                    int iloscTygodni = (int) IloscTygodniUpDown.Value;
                    bool czyWolnyTermin = true;
                    switch (iloscZajecWTygodniu)
                    {
                        case 1:
                            DateTime dataRozpoczecia = (DateTime)DateTimePicker1.Value;
                            DateTime dataZakonczenia = new DateTime(dataRozpoczecia.Year, dataRozpoczecia.Month, dataRozpoczecia.Day, dataRozpoczecia.Hour, dataRozpoczecia.Minute, dataRozpoczecia.Second);
                            dataZakonczenia=dataZakonczenia.AddMinutes(dlugosc);                           
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                czyWolnyTermin=BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia,
                                    dataZakonczenia);
                                if (czyWolnyTermin == false)
                                {
                                    Xceed.Wpf.Toolkit.MessageBox.Show("Termin jest już zajety !!! Wybierz inny", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                                dataRozpoczecia=dataRozpoczecia.AddDays(7);
                                dataRozpoczecia=dataZakonczenia.AddDays(7);
                            }
                            dataRozpoczecia = (DateTime)DateTimePicker1.Value;
                            dataZakonczenia = new DateTime(dataRozpoczecia.Year, dataRozpoczecia.Month, dataRozpoczecia.Day, dataRozpoczecia.Hour, dataRozpoczecia.Minute, dataRozpoczecia.Second);
                            dataZakonczenia=dataZakonczenia.AddMinutes(dlugosc);                          
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                WpisZajecia wpis = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia,
                                    DataZakonczenia = dataZakonczenia,
                                    DlugoscZajec = dlugosc
                                };
                                BazaMetody.DodajNowyTermin(wpis);
                                dataRozpoczecia= dataRozpoczecia.AddDays(7);
                                dataZakonczenia=dataZakonczenia.AddDays(7);
                            }                          
                            break;
                        case 2:
                            DateTime dataRozpoczecia1 = (DateTime)DateTimePicker1.Value;
                            DateTime dataZakonczenia1 = dataRozpoczecia1.AddMinutes(dlugosc);
                            DateTime dataRozpoczecia2 = (DateTime)DateTimePicker2.Value;
                            DateTime dataZakonczenia2 = dataRozpoczecia2.AddMinutes(dlugosc);
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                czyWolnyTermin = BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia1,
                                    dataZakonczenia1);
                                czyWolnyTermin = BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia2,
                                    dataZakonczenia2);
                                if (czyWolnyTermin == false)
                                {
                                    Xceed.Wpf.Toolkit.MessageBox.Show("Jeden lub więcej terminów jest już zajety !!! Wybierz inny", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                                dataRozpoczecia1 = dataRozpoczecia1.AddDays(7);
                                dataRozpoczecia1 = dataZakonczenia1.AddDays(7);
                                dataRozpoczecia2 = dataRozpoczecia2.AddDays(7);
                                dataRozpoczecia2 = dataZakonczenia2.AddDays(7);
                            }
                            dataRozpoczecia1 = (DateTime)DateTimePicker1.Value;
                            dataZakonczenia1 = dataRozpoczecia1.AddMinutes(dlugosc);
                            dataRozpoczecia2 = (DateTime)DateTimePicker2.Value;
                            dataZakonczenia2 = dataRozpoczecia2.AddMinutes(dlugosc);
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                WpisZajecia wpis1 = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia1,
                                    DataZakonczenia = dataZakonczenia1,
                                    DlugoscZajec = dlugosc
                                };
                                WpisZajecia wpis2 = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia2,
                                    DataZakonczenia = dataZakonczenia2,
                                    DlugoscZajec = dlugosc
                                };
                                BazaMetody.DodajNowyTermin(wpis1);
                                BazaMetody.DodajNowyTermin(wpis2);
                                dataRozpoczecia1 = dataRozpoczecia1.AddDays(7);
                                dataZakonczenia1 = dataZakonczenia1.AddDays(7);
                                dataRozpoczecia2 = dataRozpoczecia2.AddDays(7);
                                dataZakonczenia2 = dataZakonczenia2.AddDays(7);
                            }
                            break;
                        case 3:
                            dataRozpoczecia1 = (DateTime)DateTimePicker1.Value;
                            dataZakonczenia1 = dataRozpoczecia1.AddMinutes(dlugosc);
                            dataRozpoczecia2 = (DateTime)DateTimePicker2.Value;
                            dataZakonczenia2 = dataRozpoczecia2.AddMinutes(dlugosc);
                            DateTime dataRozpoczecia3 = (DateTime)DateTimePicker3.Value;
                            DateTime dataZakonczenia3 = dataRozpoczecia3.AddMinutes(dlugosc);
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                czyWolnyTermin = BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia1,
                                    dataZakonczenia1);
                                czyWolnyTermin = BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia2,
                                    dataZakonczenia2);
                                czyWolnyTermin = BazaMetody.SprawdzCzyJestWolnyTerminDlaDanegoObiektu(obiekt, dataRozpoczecia3,
                                    dataZakonczenia3);
                                if (czyWolnyTermin == false)
                                {
                                    Xceed.Wpf.Toolkit.MessageBox.Show("Jeden lub więcej terminów jest już zajety !!! Wybierz inny", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                                dataRozpoczecia1 = dataRozpoczecia1.AddDays(7);
                                dataRozpoczecia1 = dataZakonczenia1.AddDays(7);
                                dataRozpoczecia2 = dataRozpoczecia2.AddDays(7);
                                dataRozpoczecia2 = dataZakonczenia2.AddDays(7);
                                dataRozpoczecia3 = dataRozpoczecia3.AddDays(7);
                                dataRozpoczecia3 = dataZakonczenia3.AddDays(7);
                            }
                            dataRozpoczecia1 = (DateTime)DateTimePicker1.Value;
                            dataZakonczenia1 = dataRozpoczecia1.AddMinutes(dlugosc);
                            dataRozpoczecia2 = (DateTime)DateTimePicker2.Value;
                            dataZakonczenia2 = dataRozpoczecia2.AddMinutes(dlugosc);
                            dataRozpoczecia3 = (DateTime)DateTimePicker3.Value;
                            dataZakonczenia3 = dataRozpoczecia3.AddMinutes(dlugosc);
                            for (int i = 0; i < iloscTygodni; i++)
                            {
                                WpisZajecia wpis1 = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia1,
                                    DataZakonczenia = dataZakonczenia1,
                                    DlugoscZajec = dlugosc
                                };
                                WpisZajecia wpis2 = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia2,
                                    DataZakonczenia = dataZakonczenia2,
                                    DlugoscZajec = dlugosc
                                };
                                WpisZajecia wpis3 = new WpisZajecia()
                                {
                                    ObiektSportowy = obiekt,
                                    Instruktor = instruktor,
                                    Grupa = grupa,
                                    DataRozpoczecia = dataRozpoczecia3,
                                    DataZakonczenia = dataZakonczenia3,
                                    DlugoscZajec = dlugosc
                                };
                                BazaMetody.DodajNowyTermin(wpis1);
                                BazaMetody.DodajNowyTermin(wpis2);
                                BazaMetody.DodajNowyTermin(wpis3);
                                dataRozpoczecia1 = dataRozpoczecia1.AddDays(7);
                                dataZakonczenia1 = dataZakonczenia1.AddDays(7);
                                dataRozpoczecia2 = dataRozpoczecia2.AddDays(7);
                                dataZakonczenia2 = dataZakonczenia2.AddDays(7);
                                dataRozpoczecia3 = dataRozpoczecia3.AddDays(7);
                                dataZakonczenia3 = dataZakonczenia3.AddDays(7);
                            }
                            break;
                    }


                }
                isAdd = true;
                var okno = Xceed.Wpf.Toolkit.MessageBox.Show("Grupa utworzona !!!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                if (okno == MessageBoxResult.OK)
                {
                    this.Close();
                }              
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Wypełnij poprawnie wszystkie pola", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);               
            }


        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void JednorazoweCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IloscTygodniUpDown.IsEnabled = true;
            IloscZajecUpDown.IsEnabled = true; 
        }

        private void JednorazoweCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IloscTygodniUpDown.IsEnabled = false;
            IloscZajecUpDown.IsEnabled = false;
            DateTimePicker3.Visibility = Visibility.Hidden;
            DateTimePicker2.Visibility = Visibility.Hidden;
            LabelPicker2.Visibility= Visibility.Hidden;
            LabelPicker3.Visibility= Visibility.Hidden;
        }

        private void DyscyplinaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            var dyscyplina = instruktor.Dyscypliny.FirstOrDefault(x => x.Nazwa == (string)comboBox.SelectedValue);
            if (dyscyplina != null)
            {
                obiektySportowe = BazaMetody.ZwrocListeObiektowDlaDanejDyscypliny(dyscyplina);
                ObiektSportowyComboBox.Items.Clear();
                foreach (var item in obiektySportowe)
                {
                    ObiektSportowyComboBox.Items.Add(item.Nazwa);
                }
            }
        }

        private void IloscZajecUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown temp = (IntegerUpDown) sender;
            int? value = temp.Value;
            switch (value)
            {
                case 1:
                    DateTimePicker3.Visibility = Visibility.Hidden;
                    DateTimePicker2.Visibility = Visibility.Hidden;
                    LabelPicker2.Visibility = Visibility.Hidden;
                    LabelPicker3.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    DateTimePicker3.Visibility = Visibility.Hidden;
                    DateTimePicker2.Visibility = Visibility.Visible;
                    LabelPicker2.Visibility = Visibility.Visible;
                    LabelPicker3.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    DateTimePicker3.Visibility = Visibility.Visible;
                    DateTimePicker2.Visibility = Visibility.Visible;
                    LabelPicker2.Visibility = Visibility.Visible;
                    LabelPicker3.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
