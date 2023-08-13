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

namespace WpfTest.Objects {
    internal class SnakeSegment {
        public LinkedList<SnakeSegment> snakeBody = new LinkedList<SnakeSegment>();
        private GameBoard gameBoard;

        public UIElement? UIElement { get; set; } // Add this property
        public double X { get; set; }
        public double Y { get; set; }
        public double PrevX { get; set; } // Store the previous X position
        public double PrevY { get; set; } // Store the previous Y position
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public SnakeSegment(GameBoard parentGameBoard) {
            gameBoard = parentGameBoard;
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
            Ellipse ellipse = new Ellipse {
                Width = 20,
                Height = 20,
                Fill = Brushes.Green,
            };

            SnakeSegment? lastSegment = snakeBody.Last?.Value;
            if (lastSegment != null) {

                x = lastSegment.PrevX;
                y = lastSegment.PrevY;
            }

            SnakeSegment segment = new SnakeSegment(gameBoard) {
                UIElement = ellipse,
                X = x,
                Y = y,
                PrevX = x,
                PrevY = y
            };

            snakeBody.AddLast(segment);
            gameBoard?.SnakeCanvas.Children.Add(ellipse);
        }
    }
}
