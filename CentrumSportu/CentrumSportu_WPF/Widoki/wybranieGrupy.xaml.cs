using CentrumSportu_WPF.Baza_danych;
using CentrumSportu_WPF.Modul_biletow;
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
using System.Windows.Shapes;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for wybranieGrupy.xaml
    /// </summary>
    public partial class wybranieGrupy : Window
    {
        private List<Grupa> grupy;
        private Student student;
        private okno_student okno;
        public wybranieGrupy(Student _student, okno_student o)
        {
            InitializeComponent();
            this.student = _student;
            grupy = BazaMetody.ZwrocWszystkieGrupy();
            this.grypyLV.ItemsSource = grupy;
            okno = o;


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            student.dojdzDoGrupy((Grupa)grypyLV.SelectedItem);
            okno.zmienG();
            BazaMetody.ZmienGrupe((Grupa)grypyLV.SelectedItem, student);
            this.Close();
        }


    }
}
