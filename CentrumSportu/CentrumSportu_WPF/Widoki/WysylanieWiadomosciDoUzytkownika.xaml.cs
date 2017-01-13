using CentrumSportu_WPF.Modul_biletow;
using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for WysylanieWiadomosciDoUzytkownika.xaml
    /// </summary>
    public partial class WysylanieWiadomosciDoUzytkownika : Window
    {
        private int port;
        private string login;
        private string haslo;
        static bool mailSent = false;

        private UczestnikZajec uczestnik;
        private Instruktor instruktor;
        public WysylanieWiadomosciDoUzytkownika(UczestnikZajec uczestnik, Instruktor instruktor)
        {
            InitializeComponent();
            this.uczestnik = uczestnik;
            this.instruktor = instruktor;
            this.Title = uczestnik.Imie + " " + uczestnik.Nazwisko;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.port = 587;
            this.login = "instruktortest";
            this.haslo = "testHaslo";
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WyslijWiadomoscButton_Click(object sender, RoutedEventArgs e)
        {
            WyslijWiadomosc("instruktortest@gmail.com", "rafallebioda@gmail.com");
            this.Close();
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //logger.Log(LogLevel.Info, "Email cancelled " + token);
                MessageBox.Show("Email anulowany" + token, "Wysyłanie");
            }
            if (e.Error != null)
            {
                //logger.Log(LogLevel.Info, "Email error " + token);
                MessageBox.Show("Błąd wysyłania" + token, "Wysyłanie");
            }
            else
            {
                //logger.Log(LogLevel.Info, "Email sent " + token);
                MessageBox.Show("Email wysłany" + token, "Wysyłanie");
            }
            mailSent = true;
        }

        private void WyslijWiadomosc(string from, string to)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = port;
            client.EnableSsl = true;
            MailAddress adresFrom = new MailAddress(from, instruktor.Imie + " " + instruktor.Nazwisko);
            MailAddress adresTo = new MailAddress(to, uczestnik.Imie + " " + uczestnik.Nazwisko);
            MailMessage msg = new MailMessage(adresFrom, adresTo);
            msg.Subject = "Wiadomość dla " + uczestnik.Imie + " " + uczestnik.Nazwisko;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = WiadomoscTextBox.Text;
            msg.BodyEncoding = Encoding.UTF8;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(login, haslo);
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userState = "send operation 123";
            client.SendAsync(msg, userState);
        }
    }
}
