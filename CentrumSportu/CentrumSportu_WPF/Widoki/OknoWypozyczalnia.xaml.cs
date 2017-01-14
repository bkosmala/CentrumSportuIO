using System.Windows;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for OknoWypozyczalnia.xaml
    /// </summary>
    public partial class OknoWypozyczalnia : Window
    {
        private Pracownik pracownikWypozyczalni;

        public OknoWypozyczalnia(Pracownik pracownikWypozyczalni)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            this.pracownikWypozyczalni = pracownikWypozyczalni;
        }

        private void wylogujButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }
    }
}
