using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DictionaryV3
{
    public class ReadXML
    {
        public static void WriteWords(ObservableSet<Word> words)
        {

            XmlTextWriter writer = new XmlTextWriter(getXmlFile(), null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("words");
            if (words != null)
                foreach (var word in words)
                {
                    writer.WriteStartElement("word");

                    writer.WriteStartAttribute("label");
                    writer.WriteString(word.Label);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("category");
                    writer.WriteString(word.Category);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("body");
                    writer.WriteString(word.Body);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("picturePath");
                    writer.WriteString(word.Picture);
                    writer.WriteEndAttribute();

                    writer.WriteEndElement();
                }

            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
        }


        public static ObservableSet<Word> ReadWords()
        {
            ObservableSet<Word> result = new ObservableSet<Word>();
            XmlReader reader = XmlReader.Create(getXmlFile());


            if (reader.ReadToFollowing("word"))
            {
                do
                {

                    reader.MoveToAttribute("label");
                    string label = reader.ReadContentAsString();

                    //reader.MoveToAttribute("description");
                    //Category category = new Category(reader.ReadContentAsString());

                    reader.MoveToAttribute("category");
                    string category = reader.ReadContentAsString();

                    reader.MoveToAttribute("body");
                    string body = reader.ReadContentAsString();

                    reader.MoveToAttribute("picturePath");
                    string picturePath = reader.ReadContentAsString();

                    result.Add(new Word(label, category, body, picturePath));

                } while (reader.ReadToFollowing("word"));
            }
            reader.Close();

            return result;
        }

        private static string getXmlFile()
        {
            var filename = @"Words.xml";
            var resourcesFolder = ImageManager.GetResourcesFolder();
            var wordsFilePath = Path.Combine(resourcesFolder, filename);

            //ConsoleManager.Show();
            //Console.WriteLine(wordsFilePath);
            return wordsFilePath;
        }

    }
}
