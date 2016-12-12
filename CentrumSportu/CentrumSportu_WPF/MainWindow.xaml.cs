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


            using (var ctx = new CentrumContext())
            {
                //ObiektSportowy obiekt = new ObiektSportowy("1","Hala", new List<string> {"piłka nożna" },12,5000);
                //ctx.ObiektySportowe.Add(obiekt);
                //ctx.SaveChanges();
                foreach (var item in ctx.ObiektySportowe)
                {
                    Console.WriteLine(item.Nazwa);
                }
            }
        }
    }
}
