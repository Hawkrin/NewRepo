using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfTest.View.CustomControls;

namespace WpfTest.Objects {
    public class Fruit {
        private int canvasWidth;
        private int canvasHeight;
        private Canvas SnakeCanvas;
        public TranslateTransform fruitTransformation = new TranslateTransform();
        private string FruitPNGFile;

        public Fruit(int canvasWidth, int canvasHeight, Canvas SnakeCanvas, string fruitPNGfile) {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.SnakeCanvas = SnakeCanvas;
            this.FruitPNGFile = fruitPNGfile;
        }

        public void Fruit_Spawner() {
            Image image = new Image {
                Source = new BitmapImage(new Uri(FruitPNGFile)),
                Width = 20,
                Height = 20,
            };

            fruitTransformation.X = Random_Numbers(canvasWidth);
            fruitTransformation.Y = Random_Numbers(canvasHeight);
            image.RenderTransform = fruitTransformation;

            SnakeCanvas.Children.Add(image); //spawns the fruit
        }

        private double Random_Numbers(int number) {
            Random random = new Random();
            int radius = 20;
            int max = Math.Max(number - radius * 2, 0);
            return random.Next(0, max);
        }
    }
}
