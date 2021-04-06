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
    /// Interaction logic for Finish.xaml
    /// </summary>
    public partial class Finish : Page
    {
        private HashSet<int> sender;
        public Finish(HashSet<int> sender)
        {
            InitializeComponent();

            this.sender = sender;

            Message.Text = "Congrats, you finished the game! You have:" + MainWindow.points + " points!";

            this.sender.Clear();

            MainWindow.gameCount = 0;
            MainWindow.points = 0;


        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new MainPage());
        }
    }
}
