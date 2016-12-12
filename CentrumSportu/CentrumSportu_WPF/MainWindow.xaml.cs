using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CentrumSportu_WPF.Widoki;

namespace CentrumSportu_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;


            using (var ctx = new CentrumContext())
            {
                KontoUzytkownika konto = new KontoUzytkownika("kazik", "kazik", KontoUzytkownika.RodzajKonta.Instruktor);
                //ctx.KontaUzytkownikow.Add(konto);
               // ctx.SaveChanges();
                Instruktor temp=new Instruktor("I1","Piotr","Kazmierczak",new List<string>() {"Pilka nozna"},konto);             
                //ctx.Instruktorzy.Add(temp);
                //ctx.SaveChanges();
            }
        }

        private void ZalogujBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string haslo = PasswordBox.Password;
            var Konto= BazaMetody.SprawdzLoginiHaslo(login, haslo);
            if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Instruktor)
            {
                Instruktor instruktor = BazaMetody.ZwrocInstruktora(Konto);
                if (instruktor != null)
                {
                    okno_instruktor okno = new okno_instruktor(instruktor);
                    okno.Show();
                    this.Close();
                }
            }
            else if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Administrator)
            {

            }
            else if (Konto.TypKonta == KontoUzytkownika.RodzajKonta.Student)
            {

            }
        }

        private void GoscBtn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new OfertaWidok());
        }
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

    }
}
