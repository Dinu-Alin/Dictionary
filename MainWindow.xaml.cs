using DictionaryV3.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DictionaryV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ObservableSet<Word> words;


        //
        public static int gameCount = 0;

        public static int points = 0;

        public static ObservableSet<Word> Words { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Words = new ObservableSet<Word>();

            Switch.pageSwitcher = this;
            Switch.SwitchIt(new MainPage());


            Words = ReadXML.ReadWords();

            DataContext = this;
        }

        public void Navigate(Page nextPage)
        {
            this.Content = nextPage;
        }

        protected override void OnClosed(EventArgs e)
        {
            ReadXML.WriteWords(Words);
            base.OnClosed(e);
        }
    }
}
