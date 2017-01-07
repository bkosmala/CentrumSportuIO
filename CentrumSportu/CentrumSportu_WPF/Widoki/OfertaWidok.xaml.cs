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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for OfertaWidok.xaml
    /// </summary>
    public partial class OfertaWidok : UserControl
    {
        private ObservableCollection<Zajecia> listZajecia;
        private Zajecia selectedZajecia;
        public OfertaWidok()
        {
            InitializeComponent();
            listZajecia = new ObservableCollection<Zajecia>(BazaMetody.ZwrocWszystkieZajeciaOferta());

            listBoxZajecia.ItemsSource = listZajecia;
            listBoxZajecia.SelectedIndex = 0;
            ZajeciaViewBox.DataContext = selectedZajecia;
        }
        private void ListBoxZajecia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedZajecia = listZajecia.ElementAt(listBoxZajecia.SelectedIndex);
        }
        
    }
}
