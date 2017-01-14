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

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for DodawanieObiektuSportowego.xaml
    /// </summary>
    public partial class DodawanieObiektuSportowego : Window
    {
        private okno_administrator okno;
        public DodawanieObiektuSportowego(okno_administrator okno)
        {
            InitializeComponent();
            this.okno = okno;
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            okno.odśwież();
        }
    }
}
