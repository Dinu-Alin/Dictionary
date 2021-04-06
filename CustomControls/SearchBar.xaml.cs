using DictionaryV3.Menu;
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

namespace DictionaryV3.CustomControls
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        private Word selectedWord = null;

        private static Word NIL = new Word("Not Found!", "Try another word.", "Maybe we are too weak for your power!");

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
        public SearchBar()
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

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedWord = null;
            bool isFound = false;

            var boder = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Data.getData();

            string find = (sender as TextBox).Text;
            if (find.Length == 0)
            {
                resultStack.Children.Clear();
                resultStack.Background.Opacity = 0;
                boder.Visibility = Visibility.Collapsed;
            }
            else
            if (e.Key == Key.Enter)
            {
                SearchFunction();
            }
            if (e.Key == Key.Down) // The Arrow-Down key
            {
                textBox.Text = ((resultStack.Children[0] as Label).Content as Word).Label;
            }
            else
            {
                resultStack.Background.Opacity = 0.5;
                boder.Visibility = Visibility.Visible;
            }

            resultStack.Children.Clear();

            foreach (var obj in data)
            {
                if (obj.Label.Trim().ToLower().StartsWith(find.Trim().ToLower()))
                {
                    //obj.Label = obj.Label.Trim();
                   AddItem(obj);
                    int wordsMatching = resultStack.Children.Count;
                    if (wordsMatching <= 4)
                    {
                        resultStack.Height = ScrollViewer.Height = BorderAuto.Height = wordsMatching * 30;
                    }
                    isFound = true;
                }
            }

            if (!isFound)
            {
                resultStack.Background.Opacity = 0;
            }

        }

        public void AddItem(Word obj)
        {
            Label block = new Label();
            block.HorizontalContentAlignment = HorizontalAlignment.Center;
            //block.Background.Opacity = 50;
            //block.Padding = new Thickness(0, 0, 0, 0);
            //block.Background = new SolidColorBrush(Colors.WhiteSmoke);
            block.Content = obj;
            block.FontSize = 16;
            block.Height = 30;
            block.Foreground = new SolidColorBrush(Colors.Black);
            // block.Margin = new Thickness(1, 1, 1, 1);
            block.VerticalAlignment = VerticalAlignment.Center;
            block.HorizontalContentAlignment = HorizontalAlignment.Left;

            block.MouseLeftButtonDown += (sender, e) =>
            {
                Label lb = sender as Label;
                textBox.Text = lb.Content.ToString();
                SelectedWord = lb.Content as Word;
                lb.Background = new SolidColorBrush(Colors.Transparent);
            };

            block.MouseEnter += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.LightGray);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            block.MouseLeave += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.Transparent);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            resultStack.Children.Add(block);
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            //resultStack.Children.Clear();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Children.Clear();
            //resultStack.Background = new SolidColorBrush(Colors.Transparent);

            resultStack.Background.Opacity = 0;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Background = new SolidColorBrush(Colors.AntiqueWhite);
            resultStack.Background.Opacity = 0;
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            SearchFunction();
        }

        private void SearchFunction()
        {
            if (SelectedWord != null)
            {
                Switch.SwitchIt(new WordPage(SelectedWord));
            }
            else
            {
                bool isFound = false;
                var data = Data.getData();

                string temp = textBox.Text.Trim().ToLower();

                foreach (var obj in data)
                {
                    if (obj.Label.ToLower() == temp)
                    {
                        Switch.SwitchIt(new WordPage(obj));
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    Switch.SwitchIt(new WordPage(NIL));
                }
            }
        }
    }
}

