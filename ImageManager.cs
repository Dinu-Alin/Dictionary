using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryV3
{
    public static class ImageManager
    {
        public static string GetAppDirectory()
        {
            string runningPath = AppDomain.CurrentDomain.BaseDirectory;
            runningPath = Path.GetFullPath(Path.Combine(runningPath, @"..\..\"));
            //ConsoleManager.Show();
            //Console.WriteLine(runningPath);
            return runningPath;
        }
        public static string GetResourcesFolder()
        {
            string resourcesFolder = string.Format("{0}Resources\\", GetAppDirectory());
            //ConsoleManager.Show();
            //Console.WriteLine(resourcesFolder);

            return resourcesFolder;
        }

        public static string GetImagesFolder()
        {
            string imagesFolder = string.Format("{0}Images\\", GetResourcesFolder());
            //ConsoleManager.Show();
            //Console.WriteLine(imagesFolder);
            return imagesFolder;
        }

        public static string GetImage(string name)
        {
            string imagePath = string.Format("{0}" + name, GetImagesFolder());

            return imagePath;
        }
    }
}
