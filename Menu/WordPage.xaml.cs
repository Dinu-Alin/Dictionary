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
    /// Interaction logic for WordPage.xaml
    /// </summary>
    public partial class WordPage : Page
    {
        private Word selectedWord;

        //private BitmapImage _imageOpenClose;

        //public BitmapImage ImageOpenClose
        //{
        //    get
        //    {
        //        return _imageOpenClose;
        //    }

        //    set
        //    {
        //        _imageOpenClose = value;
        //    }
        //}

        public Word SelectedWord
        {
            get => selectedWord;
            set
            {
                selectedWord = value;
            }
        }

        public WordPage(Word sender)
        {
            InitializeComponent();

            //ImageOpenClose = new BitmapImage(new Uri(sender.Picture));
            SelectedWord = sender;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new MainPage());
        }
    }
}
