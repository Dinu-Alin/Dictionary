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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Word selectedWord = null;

        public HashSet<int> used;
        public Word SelectedWord
        {
            get
            {
                return selectedWord;
            }
            set
            {
                selectedWord = value;
            }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        public class Data
        {
            public static List<Word> getData()
            {
                List<Word> data = new List<Word>();

                foreach (var obj in MainWindow.Words)
                {
                    data.Add(obj);
                }

                return data;
            }
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new MainPage());
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new AdminPage());
        }

        private void GameClick(object sender, RoutedEventArgs e)
        {

            if (MainWindow.Words.Count() >= 5)
            {
                used = new HashSet<int>();

                for (; used.Count() < 5;)
                {
                    Random rnd = new Random();
                    used.Add(rnd.Next(MainWindow.Words.Count()));
                }

                Switch.SwitchIt(new GamePage(used));
            }
            else
            {

            }
        }

        // Generates a random number within a range.      

        //private void SearchClick(object sender, RoutedEventArgs e)
        //{
        //    if (SelectedWord != null)
        //    {
        //        Switch.SwitchIt(new WordPage(SelectedWord));
        //    }
        //    else
        //    {
        //        if(MainWindow.Words.Contains(SelectedWord))
        //        {
        //            //afis
        //        }
        //        else
        //        {
        //            //NOT FOUND
        //        }
        //    }
        //}
    }
}


//xmlns:MaterialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
//Style="{StaticResource MaterialDesignTextBox}"
//xmlns:MaterialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
