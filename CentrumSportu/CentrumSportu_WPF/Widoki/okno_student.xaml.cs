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
using CentrumSportu_WPF.Modul_biletow;
using System.Collections.ObjectModel;
using CentrumSportu_WPF.Baza_danych;

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_student.xaml
    /// </summary>
    public partial class okno_student : Window
    {
        private Student student;
        private ObservableCollection<Bilet> bilety;
        private ObservableCollection<ZajeciaOdbyte> zajeciaOdbyte;

        public okno_student(Student _student)
        {
            InitializeComponent();
            student = _student;
            imieStudentLabel.Content = student.Imie;
            nazwiskoStudentLabel.Content = student.Nazwisko;
            emailStudentLabel.Content = student.Email;
            telefonStudentLabel.Content = student.Telefon;
            zdjecieProfiloweStudentImage.Source = new BitmapImage(new Uri(student.Zdjecie));

        }

        private void WylogujStudentButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            okno.Show();
            this.Close();
        }
    }
}
