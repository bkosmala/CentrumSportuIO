using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CentrumSportu_WPF
{
    public static class Switcher
    {
        public static MainWindow pageSwitcher;
        public static object lastWindowContent;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }
        public static void Switch(UserControl newPage, object lastPage)
        {
            pageSwitcher.Navigate(newPage);
            lastWindowContent = lastPage;
        }
    }


    /*
    Klasa ta używana jest do przełączania widoków dla głównego okna programu (MainWindow),
    (nie jest tworzone nowe okno, tylko podmieniana jest zawartość) 
    aby z niej korzystać najpierw należy utworzyć widok (Add > New Item > User Control (WPF) )
    następnie wykorzystać statyczną metodę Switch:

    Switcher.Switch(new NazwaUtworzonegoWidoku());

    np. aby spowodować przejście do utworzonego widoku po kliknięciu przycisku,
    należy wkleić powyższy kod do metody obsługującej przycisk i podmienić nazwę widoku
    */
}
