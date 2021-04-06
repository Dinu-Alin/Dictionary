using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryV3
{
    public class Word:INotifyPropertyChanged
    {
        private String label;

        private String category;

        private String body;

        private String picture;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string proprtyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proprtyName));
        }

        public String Label
        {
            get => label;

            set
            {
                if(value != label)
                {
                    label = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Category
        {
            get => category;

            set
            {
                if (value != category)
                {
                    category = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Body
        {
            get => body;

            set
            {
                if (value != body)
                {
                    body = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Picture
        {
            get => picture;

            set
            {
                if (value != picture)
                {
                    picture = value;
                    OnPropertyChanged();
                }
            }
        }
        public Word()
        {
            Label = Category = Body = Picture = null;
        }

        public Word(string label, string category, string body)
        {
            Label = label;
            Category = category;
            Body = body;
            Picture = ImageManager.GetImage("no-image.jpg");
        }

        public Word(string label, string category, string body, string picture)
        {
            Label = label;
            Category = category;
            Body = body;
            Picture = picture;
        }


        public override string ToString()
        {
            return Label;
        }

        public override bool Equals(object obj)
        {
            return obj is Word &&
                   Label == (obj as Word).Label;
        }

        public override int GetHashCode()
        {
            return 981597221 + EqualityComparer<string>.Default.GetHashCode(Label);
        }
    }
}
