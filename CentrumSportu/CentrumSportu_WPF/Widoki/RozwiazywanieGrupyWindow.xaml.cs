using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
    /// Interaction logic for RozwiazywanieGrupyWindow.xaml
    /// </summary>
    public partial class RozwiazywanieGrupyWindow : Window
    {
        private Grupa grupa;
        private Instruktor instruktor;
        public ObservableCollection<Grupa> Grupy { get; set; }
        public ObservableCollection<WpisZajecia> Zajecia { get; set; }

        //email
        private int port;
        private static bool mailSent = false;
        private string login;
        private string haslo;

        public RozwiazywanieGrupyWindow(Grupa grupa, Instruktor instruktor)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.grupa = grupa;
            this.instruktor = instruktor;
            this.Title = grupa.Nazwa;


            this.port = 587;
            this.login = "instruktortest";
            this.haslo = "testHaslo";
        }

        private void AnulujUsuniecieButton_Click(object sender, RoutedEventArgs e)
        {
            Grupy = new ObservableCollection<Grupa>(instruktor.Grupy);
            Zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
            this.Close();
        }

        private void PotwrdzUsuniecieButton_Click(object sender, RoutedEventArgs e)
        {
            int ile = instruktor.Grupy.Count;
            MessageBoxResult result = MessageBox.Show("Czy chcesz usunąć " + grupa.Nazwa + "?", "Usuwanie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Grupy = new ObservableCollection<Grupa>(BazaMetody.Usungrupe(instruktor.Id, grupa.Id));
                Zajecia = new ObservableCollection<WpisZajecia>(BazaMetody.ZwrocWszystkieZajeciaDlaInstruktora(instruktor));
                this.Close();
            }
        }

        private void send_button_Click(object sender, RoutedEventArgs e)
        {
            //string port = ConfigurationManager.AppSettings["port"].ToString();
            //SmtpClient client = new SmtpClient("smtp.gmail.com");
            //client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["ssl"]);
            //MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailAddressFrom"], ConfigurationManager.AppSettings["displayName"], System.Text.Encoding.UTF8);
            //MailAddress to = new MailAddress(ConfigurationManager.AppSettings["mailAddressTo"]);
            //MailMessage message = new MailMessage(from, to);
            //message.Attachments.Add(new Attachment(attachment_textblock.Text));
            //message.Body = message_textbox.Text;
            //message.BodyEncoding = System.Text.Encoding.UTF8;
            //message.Subject = ConfigurationManager.AppSettings["subject"];
            //message.SubjectEncoding = System.Text.Encoding.UTF8;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["password"]);
            //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            //string userState = "send operation 123";
            //client.SendAsync(message, userState);
            WyslijWiadomosc("instruktortest@gmail.com", "instruktortest@gmail.com");
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
            MailAddress adresTo = new MailAddress(to, "Uczestnicy grupy");
            MailMessage msg = new MailMessage(adresFrom, adresTo);
            msg.Subject = "Powód rozwiązania grupy " + grupa.Nazwa;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = PowodUsunieciaTextBox.Text;
            msg.BodyEncoding = Encoding.UTF8;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(login, haslo);
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userState = "send operation 123";
            client.SendAsync(msg, userState);
        }
    }
}
