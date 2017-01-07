using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_instruktorow;
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
    /// Interaction logic for RozwiazywanieGrupyWindow.xaml
    /// </summary>
    public partial class RozwiazywanieGrupyWindow : Window
    {
        private Grupa grupa;
        private Instruktor instruktor;
        public ObservableCollection<Grupa> Grupy { get; set; }
        public ObservableCollection<WpisZajecia> Zajecia { get; set; }

        public RozwiazywanieGrupyWindow(Grupa grupa, Instruktor instruktor)
        {
            InitializeComponent();
            this.grupa = grupa;
            this.instruktor = instruktor;
            this.Title = grupa.Nazwa;
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
    }
}
