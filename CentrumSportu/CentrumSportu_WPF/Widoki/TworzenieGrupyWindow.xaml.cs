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
using CentrumSportu_WPF.Modul_instruktorow;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for TworzenieGrupyWindow.xaml
    /// </summary>
    public partial class TworzenieGrupyWindow : Window
    {
        private Instruktor instruktor;
        private Grupa grupa;
        public TworzenieGrupyWindow(Instruktor _instruktor)
        {
            instruktor = _instruktor;
            InitializeComponent();
            foreach (var item in instruktor.Dyscypliny)
            {
                DyscyplinaComboBox.Items.Add(item.Nazwa);
            }          
        }
    }
}
