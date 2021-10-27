using System;
using System.Drawing;

using MyRandom = Random.Random;

namespace Lab3
{
    class BinaryImage
    {
        private static int height = 10;
        private static int width = 10;

        public static void Image1(string path)
        {
            string fileName = path + "\\Г.png";

            var array = new byte[,]
            {
                {0,0,1,1,1,1,1,1,1,0 },
                {0,0,1,1,1,1,1,1,1,0 },
                {0,0,1,1,0,0,0,1,1,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 },
                {0,0,1,1,0,0,0,0,0,0 }
            };

            int size = (int)Math.Sqrt(array.Length);

            var image1 = new Bitmap(width, height);

            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i,j] == 1)
                    {
                        image1.SetPixel(j, i, Color.Black);
                    }
                    else
                    {
                        image1.SetPixel(j, i, Color.White);
                    }
                }
            }

            image1.Save(fileName);
        }

        public static void Image2(string path)
        {
            string fileName = path + "\\М.png";

            var array = new byte[,]
            {
                { 1,1,0,0,0,0,0,0,1,1},
                { 1,1,1,0,0,0,0,1,1,1},
                { 1,1,1,1,0,0,1,1,1,1},
                { 1,1,0,1,1,1,1,0,1,1},
                { 1,1,0,0,1,1,0,0,1,1},
                { 1,1,0,0,0,0,0,0,1,1},
                { 1,1,0,0,0,0,0,0,1,1},
                { 1,1,0,0,0,0,0,0,1,1},
                { 1,1,0,0,0,0,0,0,1,1},
                { 1,1,0,0,0,0,0,0,1,1}
            };

            int size = (int)Math.Sqrt(array.Length);

            var image1 = new Bitmap(width, height);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j] == 1)
                    {
                        image1.SetPixel(j, i, Color.Black);
                    }
                    else
                    {
                        image1.SetPixel(j, i, Color.White);
                    }
                }
            }

            image1.Save(fileName);
        }

        public static void Image3(string path)
        {
            string fileName = path + "\\У.png";

            var array = new byte[,]
            {
                { 0,0,1,1,0,0,0,1,1,0},
                { 0,0,1,1,1,0,0,1,1,0},
                { 0,0,0,1,1,0,0,1,1,0},
                { 0,0,0,0,1,1,1,1,0,0},
                { 0,0,0,0,1,1,1,1,0,0},
                { 0,0,0,0,0,1,1,1,0,0},
                { 0,0,0,0,0,0,1,1,0,0},
                { 0,0,0,0,0,0,1,1,0,0},
                { 0,0,1,1,1,1,1,1,0,0},
                { 0,0,1,1,1,1,1,1,0,0}
            };

            int size = (int)Math.Sqrt(array.Length);

            var image1 = new Bitmap(width, height);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j] == 1)
                    {
                        image1.SetPixel(j, i, Color.Black);
                    }
                    else
                    {
                        image1.SetPixel(j, i, Color.White);
                    }
                }
            }

            image1.Save(fileName);
        }

        public static State[] ImageToStates(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), $"{nameof(fileName)} cannot be null or empty");
            }

            var image = new Bitmap(fileName);

            State[] label = new State[image.Height * image.Width];

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var pixelColor = image.GetPixel(i, j);

                    int index = j * image.Width + i;

                    if (pixelColor == Color.FromArgb(0,0,0))
                    {
                        label[index] = State.UpperState;
                    }
                    else if (pixelColor == Color.FromArgb(255,255,255))
                    {
                        label[index] = State.LowerState;
                    }
                    else
                    {
                        throw new ArgumentException("The pixel of image can be white or black color");
                    }
                }
            }

            return label;
        }

        public static void StateToImage(string imageName, State[] array)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                throw new ArgumentNullException(nameof(imageName), $"{nameof(imageName)} cannot be null or empty");
            }

            if (array is null)
            {
                throw new ArgumentNullException(nameof(array), $"{nameof(array)} cannot be null");
            }

            int imageWidth = (int)Math.Sqrt(array.Length);
            int imageHeight = (int)Math.Sqrt(array.Length);

            var image = new Bitmap(imageWidth, imageHeight);

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    int index = i * imageHeight + j;

                    if (array[index] == State.UpperState)
                    {
                        image.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        image.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                    }
                }
            }

            image.Save(imageName);
        }

        public static int[,] GetW(State[][] labels)
        {
            if (labels is null)
            {
                throw new ArgumentNullException(nameof(labels), $"{nameof(labels)} cannot be null");
            }

            int[,] w = new int[labels[0].Length, labels[0].Length];

            for (int i = 0; i < labels[0].Length; i++)
            {
                for (int j = 0; j < labels[0].Length; j++)
                {
                    for (int indexArray = 0; indexArray < labels.Length; indexArray++)
                    {
                        if (labels[indexArray] is null)
                        {
                            throw new ArgumentNullException(nameof(labels), $"The row of {nameof(labels)} cannot be null");
                        }

                        if (i != j)
                        {
                            w[i, j] += (int)labels[indexArray][i] * (int)labels[indexArray][j];
                        }
                    }
                }
            }
            return w;
        }

        public static State[] GenerateNoisyImage(double noisy, State[] image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image cannot be null");
            }

            if (noisy < 0 || noisy > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(noisy), $"{nameof(noisy)} cannot be less than zero or greater than one");
            }

            var random = new MyRandom(noisy);

            var noisyImage = new State[image.Length];

            for (int i = 0; i < noisyImage.Length; i++)
            {
                int value = random.Next();

                if (value == 1)
                {
                    noisyImage[i] = image[i];
                }
                else
                {
                    noisyImage[i] = image[i] == State.LowerState ? State.UpperState : State.LowerState;
                }
            }

            return noisyImage;
        }

        public static Tuple<int, int> GetImageIndex(State[] noisyImage, State[][] images, int[,] w, int numberOfIteration)
        {
            if (noisyImage is null)
            {
                throw new ArgumentNullException(nameof(noisyImage), $"{nameof(noisyImage)} cannot be null");
            }

            if (w is null)
            {
                throw new ArgumentNullException(nameof(w), $"{nameof(w)} cannot be null");
            }

            if (numberOfIteration < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfIteration), $"{nameof(numberOfIteration)} cannot be less than zero");
            }

            if (images is null)
            {
                throw new ArgumentNullException(nameof(images), $"{nameof(images)} cannot be null");
            }

            int wWidth = (int)Math.Sqrt(w.Length);

            int index = -1;

            var newNoisyImage = new State[noisyImage.Length];

            var noisyImageCop = CopyState(noisyImage);

            int iteration;

            for (iteration = 0; iteration < numberOfIteration; iteration++)
            {
                for (int i = 0; i < newNoisyImage.Length; i++)
                {
                    int counter = 0;

                    for (int j = 0; j < wWidth; j++)
                    {
                        counter += (int)noisyImageCop[j] * w[i, j];
                    }
                    
                    if (counter > 0)
                    {
                        newNoisyImage[i] = State.UpperState;
                    }
                    else
                    {
                        newNoisyImage[i] = State.LowerState;
                    }
                }

                index = GetEqualImageIndex(newNoisyImage, images);

                if (index !=-1)
                {
                    break;
                }

                noisyImageCop = newNoisyImage;
            }

            return new Tuple<int, int>(index, iteration);
        }

        public static int GetEqualImageIndex(State[] newNoisyImage, State[][] images)
        {
            if (newNoisyImage is null)
            {
                throw new ArgumentNullException($"{nameof(newNoisyImage)} cannot be null", nameof(newNoisyImage));
            }

            if (images is null)
            {
                throw new ArgumentNullException($"{nameof(images)} cannot be null", nameof(images));
            }

            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] is null)
                {
                    throw new ArgumentNullException($"The row of {nameof(images)} cannot be null", nameof(images));
                }

                if (IsImagesEquals(newNoisyImage, images[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool IsImagesEquals(State[] firstImage, State[] secondImage)
        {
            if (firstImage is null)
            {
                throw new ArgumentNullException($"{nameof(firstImage)} cannot be null", nameof(firstImage));
            }

            if (secondImage is null)
            {
                throw new ArgumentNullException($"{nameof(secondImage)} cannot be null", nameof(secondImage));
            }

            if (firstImage.Length != secondImage.Length)
            {
                return false;
            }

            for (int i = 0; i < firstImage.Length; i++)
            {
                if (firstImage[i] != secondImage[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static State[] CopyState(State[] states)
        {
            if (states is null)
            {
                throw new ArgumentNullException(nameof(states), $"{nameof(states)} cannot be null");
            }

            var newStates = new State[states.Length];

            for (int i = 0; i < states.Length; i++)
            {
                newStates[i] = states[i];
            }

            return newStates;
        }
    }
}
