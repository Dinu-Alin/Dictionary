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

namespace DictionaryV3.Menu
{
    /// <summary>
    /// Interaction logic for NextPage.xaml
    /// </summary>
    public partial class NextPage : Page
    {

        private HashSet<int> sender;
        public NextPage(HashSet<int> sender, bool isPoint, string rightAnswear = null)
        {
            InitializeComponent();

            this.sender = sender;

            if (isPoint)
            {
                Message.Text = "Congrats, you got one morep point!";
            }
            else
            {
                Message.Text = "Wrong answear! The answear is:" + rightAnswear;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.gameCount == 5)
            {
                Switch.SwitchIt(new Finish(this.sender));
            }
            else
            {
                Switch.SwitchIt(new GamePage(this.sender));

            }
        }
    }
}
