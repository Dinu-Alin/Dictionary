using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryV3
{
    class Switch
    {
        public static MainWindow pageSwitcher;

        public static void SwitchIt(System.Windows.Controls.Page newPage)
        {
            pageSwitcher.Navigate(newPage);
        }
    }
}
