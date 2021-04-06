using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    /// 

    public partial class AdminPage : Page
    {
        private Word selectedWord;

        private string pathName = null;

        public Word SelectedWord
        {
            get => selectedWord;
            set
            {
                selectedWord = value;
            }
        }

        public AdminPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switch.SwitchIt(new MainPage());
        }

        private void ListMyItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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


        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Background = new SolidColorBrush(Colors.AntiqueWhite);
            resultStack.Background.Opacity = 0;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            resultStack.Children.Clear();

            resultStack.Background.Opacity = 0;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
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

                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            }
            else
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
                labelText.Text = SelectedWord.Label;
                CategoryText.Text = SelectedWord.Category;
                BodyText.Text = SelectedWord.Body;

                if (!SelectedWord.Picture.Equals(String.Empty))
                {
                    pathName = SelectedWord.Picture;
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(SelectedWord.Picture);
                    image.EndInit();
                    ImageHolder.Source = image;
                }

                lb.Background = new SolidColorBrush(Colors.Transparent);
            };

            block.MouseEnter += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.WhiteSmoke);
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

        private void labelText_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //SelectedWord = null;
            //labelText.Text = textBox.Text;
            //CategoryText.Text = "";
            //BodyText.Text = "";
            int index = MainWindow.Words.IndexOf(new Word(textBox.Text, "", ""));

            if (index >= 0)
            {
                labelText.Text = MainWindow.Words[index].Label;
                CategoryText.Text = MainWindow.Words[index].Category;
                BodyText.Text = MainWindow.Words[index].Body;

                pathName = MainWindow.Words[index].Picture;

                if (pathName != null && !pathName.Equals(string.Empty))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(pathName);
                    image.EndInit();
                    ImageHolder.Source = image;
                }
            }
            else
            {
                labelText.Text = "";
                CategoryText.Text = "";
                BodyText.Text = "";

                pathName = "";
                ImageHolder.Source = null;
            }

        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Words.Contains(SelectedWord))
            {
                if (labelText.Text != "" && CategoryText.Text != "" && BodyText.Text != "")
                {
                    if (pathName != null && !pathName.Equals(string.Empty))
                    {
                        string newPath = ImageManager.GetImagesFolder() + labelText.Text + ".jpg";

                        int index = MainWindow.Words.IndexOf(SelectedWord);

                        MainWindow.Words[index].Label = labelText.Text;
                        MainWindow.Words[index].Category = CategoryText.Text;
                        MainWindow.Words[index].Body = BodyText.Text;
                        MainWindow.Words[index].Picture = newPath;


                        //System.IO.File.Delete(newPath);

                        if (pathName != newPath)
                        {
                            System.IO.File.Copy(pathName, newPath, true);

                        }

                        textBox.Text = labelText.Text;
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = new Uri(pathName);
                        image.EndInit();
                        ImageHolder.Source = image;

                        Switch.SwitchIt(new AdminPage());

                    }
                    else
                    {

                        int index = MainWindow.Words.IndexOf(SelectedWord);

                        MainWindow.Words[index].Label = labelText.Text;
                        MainWindow.Words[index].Category = CategoryText.Text;
                        MainWindow.Words[index].Body = BodyText.Text;
                        MainWindow.Words[index].Picture = ImageManager.GetImage("no-image.jpg");

                        textBox.Text = labelText.Text;

                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = new Uri(MainWindow.Words[index].Picture);
                        image.EndInit();
                        ImageHolder.Source = image;

                        Switch.SwitchIt(new AdminPage());
                        //ImageHolder.Source = new BitmapImage(new Uri(MainWindow.Words[index].Picture));
                    }
                }

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Word newWord = null;
            selectedWord = new Word();
            if (labelText.Text != "" && CategoryText.Text != "" && BodyText.Text != "" && selectedWord.Label != labelText.Text)
            {
                if (pathName != null && !pathName.Equals(String.Empty))
                {
                    string newPath = ImageManager.GetImagesFolder() + labelText.Text + ".jpg";

                    newWord = new Word
                        (
                                labelText.Text,
                                CategoryText.Text,
                                BodyText.Text,
                                newPath
                        );

                    MainWindow.Words.Add(newWord);
                    selectedWord = null;


                    System.IO.File.Copy(pathName, newPath, true);
                    pathName = null;


                    Switch.SwitchIt(new AdminPage());
                    //textBox.Text = newWord.Label;
                }
                else
                {
                    newWord = new Word
                        (
                                labelText.Text,
                                CategoryText.Text,
                                BodyText.Text
                        );

                    MainWindow.Words.Add(newWord);
                    selectedWord = null;

                    textBox.Text = "";
                    labelText.Text = "";
                    CategoryText.Text = "";
                    BodyText.Text = "";
                    ImageHolder.Source = null;
                }

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = MainWindow.Words.IndexOf(new Word(textBox.Text, "", ""));

            if (index >= 0)
            {
                MainWindow.Words.RemoveAt(index);

                // ImageHolder.Source = new BitmapImage();


                if (SelectedWord != null && SelectedWord.Picture != ImageManager.GetImage("no-image.jpg"))
                    System.IO.File.Delete(SelectedWord.Picture);

                textBox.Text = "";
                labelText.Text = "";
                CategoryText.Text = "";
                BodyText.Text = "";
            }
            else
            {
                //Empty
            }
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\";
                openFileDialog.Filter = "(*.png)|*.png|(*.jpg)|*.jpg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            if (filePath != null && !filePath.Equals(string.Empty))
            {
                BitmapImage bi = new BitmapImage();

                bi.BeginInit();

                bi.CacheOption = BitmapCacheOption.OnLoad;

                bi.UriSource = new Uri(filePath);

                bi.EndInit();

                // Image image1 = new Image();

                ImageHolder.Source = bi;
            }

            pathName = filePath;
            //pathName = test + labelText.Text + ".jpg";

            //string test = ImageManager.GetImagesFolder();

            //System.IO.File.Copy(filePath, test + labelText.Text + ".jpg", true);

        }
    }
}


//Adeian 1 1 0 1 0 0 1 0 0 1 0 1 1 0 0 1 1 1 0 1 1 1 1 0
//Dinj   1 0 1 0 0 1 0 0 0 0 0 1 1 0 0 1 1 0 0 0 1 1 1 1
//Bakiu  1 0 1 1 0 0 1 0 0 0 0 1 1 0 0 1 1 0 0 0 1 1 0 1
//1 0 0 0 1 1 1 1 1 0 1 1 1 1 1 1 1 0 1 0 1 1 0 0 8 de 0 16 de 1