
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using WpfTest.View.CustomControls;
using WpfTest.Objects;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

public class Snake {
    public TranslateTransform snakeTransformation { get; } = new TranslateTransform();
    private GameBoard? gameBoard;
    private SnakeSegment? snakeSegment;
    private Fruit? fruit;
    private Dictionary<string, BitmapImage> gifDictionary = new Dictionary<string, BitmapImage>();
    private Image? snake;
    private bool isRightGifActive = true;
    private double cellWidth;
    private double cellHeight;
    Dictionary<string, string> SnakeGifPaths;

    public Snake(GameBoard gameBoard, double initialX, double initialY, Fruit fruit, double cellWidth, double cellHeight, Dictionary<string, string> SnakeGifPaths, Dictionary<string, string> SnakeSegmentPNGPaths) {

        this.gameBoard = gameBoard;
        this.SnakeGifPaths = SnakeGifPaths;
        this.fruit = fruit;
        this.cellWidth = cellWidth;
        this.cellHeight = cellHeight;

        snakeSegment = new SnakeSegment(gameBoard, SnakeSegmentPNGPaths);
        snakeTransformation.X = initialX;
        snakeTransformation.Y = initialY;
    }
    public void init_snake() {
      
        Prepare_Snake_Image();
        Snake_Spawner();
        fruit?.Fruit_Spawner();
    }

    public void Snake_Movement(object sender, KeyEventArgs e) {
        if (!gameBoard.snakeDied) {
            double prevHeadX = snakeTransformation.X;
            double prevHeadY = snakeTransformation.Y;

            // Move snake's head
            switch (e.Key) {
                case Key.Left:
                    snakeTransformation.X -= cellWidth;
                    isRightGifActive = false;
                    ImageBehavior.SetAnimatedSource(snake, gifDictionary["right"]);
                    break;
                case Key.Right:
                    snakeTransformation.X += cellWidth;
                    isRightGifActive = true;
                    ImageBehavior.SetAnimatedSource(snake, gifDictionary["left"]);
                    break;
                case Key.Up:
                    snakeTransformation.Y -= cellHeight;
                    ImageBehavior.SetAnimatedSource(snake, gifDictionary[isRightGifActive ? "rightUp" : "leftUp"]);
                    break;
                case Key.Down:
                    snakeTransformation.Y += cellHeight;
                    ImageBehavior.SetAnimatedSource(snake, gifDictionary[isRightGifActive ? "rightDown" : "leftDown"]);
                    break;
            }
            snakeSegment?.Update_SnakeSegments(prevHeadX, prevHeadY); // Update the positions of snake segments
        }
        Snake_Collision();
    }

    private void Prepare_Snake_Image() {
        foreach (var entry in SnakeGifPaths) {
            var gifImage = new BitmapImage();
            gifImage.BeginInit();
            gifImage.UriSource = new Uri(entry.Value);
            gifImage.EndInit();
            gifDictionary.Add(entry.Key, gifImage);
        }

        snake = new Image {
            Width = 20,
            Height = 20,
        };

        ImageBehavior.SetAnimatedSource(snake, gifDictionary["right"]);
    }

    private void Snake_Collision() {
        double snakeHeadLeft = snakeTransformation.X;
        double snakeHeadTop = snakeTransformation.Y;
        double fruitLeft = fruit.fruitTransformation.X;
        double fruitTop = fruit.fruitTransformation.Y;
        int collisionThreshold = 15;

        if (snakeHeadLeft < 0 || snakeHeadLeft > 395 || snakeHeadTop < 0 || snakeHeadTop > 385) { gameBoard?.OnCollisionDetected(); }

        if (Math.Abs(snakeHeadLeft - fruitLeft) <= collisionThreshold && Math.Abs(snakeHeadTop - fruitTop) <= collisionThreshold) {
            fruit.Fruit_Spawner();
            gameBoard?.Score_Incrementer();
            snakeSegment?.Add_SnakeSegment(snakeHeadLeft, snakeHeadTop);
        }
    }

    private void Snake_Spawner() {
        gameBoard?.SnakeCanvas.Children.Clear();

        gameBoard?.SnakeCanvas.Children.Add(snake);
        snake.RenderTransform = snakeTransformation;
    }

}




