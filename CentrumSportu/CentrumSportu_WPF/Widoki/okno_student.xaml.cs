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

namespace CentrumSportu_WPF.Widoki
{
    /// <summary>
    /// Interaction logic for okno_student.xaml
    /// </summary>
    public partial class okno_student : Window
    {
        private Student student;

        public okno_student(Student _student)
        {
            InitializeComponent();
            student = _student;
            this.textBox.Text = student.Imie + " " + student.Nazwisko;
        }
    }
}
