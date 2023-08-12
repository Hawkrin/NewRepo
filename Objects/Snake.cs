
using System;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using WpfTest.View.CustomControls;
using System.Diagnostics.Metrics;
using WpfTest.Objects;
using System.Xml.Linq;

public class Snake {
    public TranslateTransform snakeTransformation { get; } = new TranslateTransform();
    private int MOVEMENT_SPEED = 1; // Adjust the movement step as needed
    private int collisionThreshold = 8; // Adjust the accuracy of the fruits to be eaten
    private GameBoard? gameBoard;
    private SnakeSegment snakeSegment;
    private Fruit fruit;

    public Snake(GameBoard gameBoard, double initialX, double initialY) {
        this.gameBoard = gameBoard; // Assign the gameBoard reference
        snakeSegment = new SnakeSegment(gameBoard); // Pass the Snake instance
        fruit = new Fruit(gameBoard); // Pass the Fruit instance
        snakeSegment.CenterX = initialX;
        snakeSegment.CenterY = initialY;
    }

    private void Snake_Position() {
        double snakeHeadLeft = snakeTransformation.X;
        double snakeHeadTop = snakeTransformation.Y;
        double fruitLeft = fruit.fruitTransformation.X;
        double fruitTop = fruit.fruitTransformation.Y;

        if (snakeHeadLeft < 0 || snakeHeadLeft > 395 || snakeHeadTop < 0 || snakeHeadTop > 385) { gameBoard.OnCollisionDetected(); }

        if (Math.Abs(snakeHeadLeft - fruitLeft) <= collisionThreshold && Math.Abs(snakeHeadTop - fruitTop) <= collisionThreshold) {
            fruit.Fruit_Spawner();
            gameBoard?.ScoreIncrementedHandler(this, EventArgs.Empty);
            AddSnakeSegment(snakeHeadLeft, snakeHeadTop);
        }
    }

    private void UpdateSnakeSegments(double prevHeadX, double prevHeadY) {
        double prevSegmentX = prevHeadX;
        double prevSegmentY = prevHeadY;

        foreach (var segment in snakeSegment.snakeBody) {
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

    public void Snake_Movement(object sender, KeyEventArgs e) {
        if (!gameBoard.snakeDied) {
            double prevHeadX = snakeTransformation.X;
            double prevHeadY = snakeTransformation.Y;

            // Move snake's head
            switch (e.Key) {
                case Key.Left:
                    snakeTransformation.X -= MOVEMENT_SPEED;
                    break;
                case Key.Right:
                    snakeTransformation.X += MOVEMENT_SPEED;
                    break;
                case Key.Up:
                    snakeTransformation.Y -= MOVEMENT_SPEED;
                    break;
                case Key.Down:
                    snakeTransformation.Y += MOVEMENT_SPEED;
                    break;
            }
            UpdateSnakeSegments(prevHeadX, prevHeadY); // Update the positions of snake segments
        }

        Snake_Position();
    }

    private void AddSnakeSegment(double x, double y) {
        Ellipse ellipse = new Ellipse {
            Width = 10,
            Height = 10,
            Fill = Brushes.Green,
        };

        SnakeSegment? lastSegment = snakeSegment.snakeBody.Last?.Value;
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

        snakeSegment.snakeBody.AddLast(segment);
        gameBoard?.SnakeCanvas.Children.Add(ellipse);
    }

    private void SnakeSpawner() {

        int radius = 10;
        SolidColorBrush fillBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(50, 205, 50));

        Ellipse snake = new Ellipse {
            Width = radius - 1,
            Height = radius - 1,
            Fill = fillBrush
        };

        gameBoard?.SnakeCanvas.Children.Add(snake); //spawns the fruit
        snake.RenderTransform = snakeTransformation;

    }

    public void init_snake() {
        snakeTransformation.X = snakeSegment.CenterX;
        snakeTransformation.Y = snakeSegment.CenterY;

        SnakeSpawner();
    }

}




