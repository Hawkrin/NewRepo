using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    }
}
