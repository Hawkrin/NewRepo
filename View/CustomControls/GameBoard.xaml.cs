using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using WpfTest.Objects;
using WpfTest.Utils;

namespace WpfTest.View.CustomControls {

    public partial class GameBoard : UserControl {
        public bool snakeDied = false;
        private int counter;
        public event EventHandler? CollisionDetected;
        public event EventHandler? RestartButtonClicked;
        private readonly Snake? snake;
        private readonly Fruit? fruit;
        public SharedViewModel ViewModel { get; set; }

        int gridRows = 25;
        int gridColumns = 25;

        public GameBoard() {

            ImageProcessor dictionaryBuilder = new ImageProcessor();

            InitializeComponent();
            SnakeCanvas.Loaded += SnakeCanvas_Loaded;

            double initialX = SnakeCanvas.Width / 2;
            double initialY = SnakeCanvas.Height / 2;
            int canvasWidth = (int)SnakeCanvas.Width;
            int canvasHeight = (int)SnakeCanvas.Height;
            double cellWidth = SnakeCanvas.Width / gridColumns;
            double cellHeight = SnakeCanvas.Height / gridRows;

            fruit = new Fruit(canvasWidth, canvasHeight, SnakeCanvas, dictionaryBuilder.fruitPNGFile);
            snake = new Snake(this, initialX, initialY, fruit, cellWidth, cellHeight, dictionaryBuilder.SnakeGifPaths, dictionaryBuilder.SnakeSegmentPNGPaths);

            KeyDown += snake.Snake_Movement; //reads input from keyboard
            Focusable = true;
            Loaded += (sender, e) => Keyboard.Focus(this);
        }

        public void OnCollisionDetected() {
            CollisionDetected?.Invoke(this, EventArgs.Empty);
            snakeDied = true;
            restartButton.Visibility = System.Windows.Visibility.Visible;
        }

        public void Score_Incrementer() {
            counter = (counter + 1) % 10000;
            ViewModel.Score = counter;
        }

        private void SnakeCanvas_Loaded(object sender, RoutedEventArgs e) {
            snake?.init_snake();
            fruit?.Fruit_Spawner();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e) {
            snakeDied = false;
            SnakeCanvas.Loaded += SnakeCanvas_Loaded;

            snake?.init_snake();
            Keyboard.Focus(this);

            restartButton.Visibility = System.Windows.Visibility.Collapsed;
            RestartButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
