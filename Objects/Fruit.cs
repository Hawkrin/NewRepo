using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfTest.View.CustomControls;

namespace WpfTest.Objects {
    public class Fruit {
        public TranslateTransform fruitTransformation { get; } = new TranslateTransform();
        private GameBoard? gameBoard;
        public int canvasWidth;
        public int canvasHeight;

        public Fruit(GameBoard gameBoard) {
            this.gameBoard = gameBoard; // Assign the gameBoard reference
        }

        public void Fruit_Spawner() {

            Random random = new Random();

            int radius = 20;
            int maxX = Math.Max(canvasWidth - radius * 2, 0); // Ensure positive or zero value
            int maxY = Math.Max(canvasHeight - radius * 2, 0); // Ensure positive or zero value

            double x = random.Next(0, maxX);
            double y = random.Next(0, maxY);

            SolidColorBrush fillBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256)
            ));

            Ellipse ellipse = new Ellipse {
                Width = radius - 1,
                Height = radius - 1,
                Fill = fillBrush
            };

            // Use the fruitTransformation to set the position of the ellipse
            fruitTransformation.X = x;
            fruitTransformation.Y = y;
            ellipse.RenderTransform = fruitTransformation;

            gameBoard?.SnakeCanvas.Children.Add(ellipse); //spawns the fruit
        }
    }
}
