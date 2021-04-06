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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Word curr = null;

        private HashSet<int> sender;
        public GamePage(HashSet<int> sender)
        {
            InitializeComponent();

            this.sender = sender;
            //if(MainWindow.gameCount < )
            curr = MainWindow.Words[this.sender.First()];
            this.sender.Remove(this.sender.First());

            ++MainWindow.gameCount;

            if (MainWindow.gameCount > 1)
            {
                Prev.Visibility = Visibility.Hidden;
            }

            Finish.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Visible;


            Random rnd = new Random();
            int gameInput = rnd.Next(2);

            if (gameInput == 0)
            {
                BodyText.Text = curr.Body;
                sV.Visibility = Visibility.Visible;
            }
            else
            {
                if (curr.Picture != ImageManager.GetImage("no-image.jpg"))
                {
                    ImageHolder.Source = new BitmapImage(new Uri(curr.Picture));
                }
                else
                {
                    BodyText.Text = curr.Body;
                }
            }


        }

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
                this.resultStack.Children.Clear();
                resultStack.Background.Opacity = 0;
                boder.Visibility = Visibility.Collapsed;
            }
            else
            if (e.Key == Key.Enter)
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
                        resultStack.Height = this.ScrollViewer.Height = BorderAuto.Height = wordsMatching * 30;
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

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            //SelectedWord = null;
            //labelText.Text = textBox.Text;
            //CategoryText.Text = "";
            //BodyText.Text = "";
            int index = MainWindow.Words.IndexOf(new Word(textBox.Text, "", ""));

            if (index >= 0)
            {
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.gameCount == 0)
            {
                Switch.SwitchIt(new MainPage());
            }
            else
            {
                Prev.Visibility = Visibility.Hidden;
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new MainPage());
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {


            if (textBox.Text.Equals(curr.Label))
            {
                ++MainWindow.points;
                //Message.Text = "Congrats, you got one morep point!";
                Switch.SwitchIt(new NextPage(this.sender, true));
            }
            else
            {
                //Message.Text = "Wrong answear! The answear is:" + curr.Label;

                Switch.SwitchIt(new NextPage(this.sender, false, curr.Label));

            }
            
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            BodyText.Text = curr.Body;
            ImageHolder.Source = new BitmapImage(new Uri(curr.Picture));
        }
    }
}