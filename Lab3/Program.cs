using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab3
{
    class Program
    {
        readonly static string mainFolderPath = "E:\\ВМСиС\\7 семестр\\ЦОСиИ\\Lab3";
        readonly static string testingImageFolder = "TestingImages";
        readonly static string noisyImageFolder = "NoisyImages";

        private static void CreateDirectory(string path)
        {
            var dirTestInfo = new DirectoryInfo(path);

            if(!dirTestInfo.Exists)
            {
                dirTestInfo.Create();
            }
        }

        private static void CreateFile(string fileName, Dictionary<int,string> dictionary)
        {
            using (var file = new StreamWriter($"{mainFolderPath}\\{noisyImageFolder}\\{fileName}",false))
            {
                file.WriteLine("Noisy file name - source testing image");

                foreach (var valuePair in dictionary)
                {
                    file.WriteLine($"{valuePair.Key}.png - {valuePair.Value}");
                }

                file.Flush();
            }
        }
        static void Main(string[] args)
        {
            try
            {
                IFormatProvider formatProvider = new NumberFormatInfo { NumberDecimalSeparator = "." };

                CreateDirectory($"{mainFolderPath}\\{testingImageFolder}");
                CreateDirectory($"{mainFolderPath}\\{noisyImageFolder}");

                BinaryImage.Image1($"{mainFolderPath}\\{testingImageFolder}");
                BinaryImage.Image2($"{mainFolderPath}\\{testingImageFolder}");
                BinaryImage.Image3($"{mainFolderPath}\\{testingImageFolder}");

                Console.WriteLine("Введите значение вероятности шума для изображения:");

                double probability = Convert.ToDouble(Console.ReadLine(), formatProvider);

                CreateDirectory($"{mainFolderPath}\\{noisyImageFolder}\\{probability * 100}");

                int numberOfIteration = 10000;

                var images = new State[3][];

                string path0 = $"{mainFolderPath}\\{testingImageFolder}\\Г.png";
                string path1 = $"{mainFolderPath}\\{testingImageFolder}\\М.png";
                string path2 = $"{mainFolderPath}\\{testingImageFolder}\\У.png";

                var imageDictionary = new Dictionary<int, string>(3);

                imageDictionary.Add(0, path0);
                imageDictionary.Add(1, path1);
                imageDictionary.Add(2, path2);

                images[0] = BinaryImage.ImageToStates(path0);
                images[1] = BinaryImage.ImageToStates(path1);
                images[2] = BinaryImage.ImageToStates(path2);

                var w = BinaryImage.GetW(images);

                var noisyImageDictionary = new Dictionary<int, string>();
                var expectedDictionary = new Dictionary<int, string>();

                var rnd = new System.Random();

                for (int index = 0; index < 10; index++)
                {
                    string path = imageDictionary[rnd.Next(0, 3)];

                    expectedDictionary[index] = path;

                    var image = BinaryImage.ImageToStates(path);

                    var noisyImage = BinaryImage.GenerateNoisyImage(probability, image);

                    BinaryImage.StateToImage($"{mainFolderPath}\\{noisyImageFolder}\\{probability * 100}\\{index}.png", noisyImage);

                    var tuple = BinaryImage.GetImageIndex(noisyImage, images, w, numberOfIteration);

                    if (tuple.Item1 == -1)
                    {
                        Console.WriteLine("Image do not found");

                        noisyImageDictionary[index] = "Image do not found";
                    }
                    else
                    {
                        Console.WriteLine($"Image path: {imageDictionary[tuple.Item1]}");

                        noisyImageDictionary[index] = imageDictionary[tuple.Item1];
                    }
                }

                CreateFile($"{probability * 100}\\Actually.txt", noisyImageDictionary);
                CreateFile($"{probability * 100}\\Expected.txt", expectedDictionary);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc.Message);
            }
        }
    }
}
