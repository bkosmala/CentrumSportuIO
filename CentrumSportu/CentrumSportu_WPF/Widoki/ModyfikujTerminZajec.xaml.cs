﻿using System;
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
    /// Logika interakcji dla klasy ModyfikujTerminZajec.xaml
    /// </summary>
    public partial class ModyfikujTerminZajec : Window
    {
        private WpisZajecia wpisZajecia;
        public bool czyModyfikowane = false;
        public ModyfikujTerminZajec(WpisZajecia wpis)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            wpisZajecia = wpis;
        }
    }
}
