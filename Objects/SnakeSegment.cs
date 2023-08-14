using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfTest.View.CustomControls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfTest.Objects {
    internal class SnakeSegment {
        public LinkedList<SnakeSegment> snakeBody = new LinkedList<SnakeSegment>();
        private GameBoard gameBoard;
        private int imageIndex = 0;
        Dictionary<string, string> SnakeSegmentPNGPaths;

        public UIElement? UIElement { get; set; } // Add this property
        public double X { get; set; }
        public double Y { get; set; }
        public double PrevX { get; set; } // Store the previous X position
        public double PrevY { get; set; } // Store the previous Y position
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public SnakeSegment(GameBoard parentGameBoard, Dictionary<string, string> snakeSegmentPNGPaths) {
            gameBoard = parentGameBoard;
            SnakeSegmentPNGPaths = snakeSegmentPNGPaths;
        }

        public void Update_SnakeSegments(double prevHeadX, double prevHeadY) {
            double prevSegmentX = prevHeadX;
            double prevSegmentY = prevHeadY;

            foreach (var segment in snakeBody) {
                double tempX = segment.PrevX; // Store the previous position
                double tempY = segment.PrevY;
                segment.PrevX = segment.X; // Update previous position
                segment.PrevY = segment.Y;
                segment.X = prevSegmentX;   // Update segment position
                segment.Y = prevSegmentY;

                prevSegmentX = tempX;
                prevSegmentY = tempY;

                Canvas.SetLeft(segment.UIElement, segment.X);
                Canvas.SetTop(segment.UIElement, segment.Y);
            }
        }

        public void Add_SnakeSegment(double x, double y) {
            Image image = new Image {
                Source = new BitmapImage(new Uri(SnakeSegmentPNGPaths.Values.ElementAt(imageIndex))),
                Width = 20,
                Height = 20,
            };

            // Increment the image index for the next segment
            imageIndex = (imageIndex + 1) % SnakeSegmentPNGPaths.Count;

            SnakeSegment? lastSegment = snakeBody.Last?.Value;
            if (lastSegment != null) {
                x = lastSegment.PrevX;
                y = lastSegment.PrevY;
            }

            SnakeSegment segment = new SnakeSegment(gameBoard, SnakeSegmentPNGPaths) {
                UIElement = image,
                X = x,
                Y = y,
                PrevX = x,
                PrevY = y
            };

            snakeBody.AddLast(segment);
            gameBoard?.SnakeCanvas.Children.Add(image);
        }
    }
}