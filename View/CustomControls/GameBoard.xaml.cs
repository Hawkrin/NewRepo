using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfTest.Objects;

namespace WpfTest.View.CustomControls {

    public partial class GameBoard : UserControl {
        public bool snakeDied = false;
        private int counter;
        ScoreScreen ScoreScreenInstance = new ScoreScreen();
        public event EventHandler? CollisionDetected;
        public event EventHandler? RestartButtonClicked;
        private readonly Snake? snake;
        private readonly Fruit? fruit;

        public GameBoard() {
            InitializeComponent();
            SnakeCanvas.Loaded += SnakeCanvas_Loaded;

            double initialX = SnakeCanvas.Width / 2;
            double initialY = SnakeCanvas.Height / 2;

            snake = new Snake(this, initialX, initialY);
            fruit = new Fruit(this);

            KeyDown += snake.Snake_Movement; //reads input from keyboard
            Focusable = true;
            Loaded += (sender, e) => Keyboard.Focus(this);
        }

        public void OnCollisionDetected() {
            CollisionDetected?.Invoke(this, EventArgs.Empty);
            snakeDied = true;
            restartButton.Visibility = System.Windows.Visibility.Visible;
        }

        public void ScoreIncrementedHandler(object sender, EventArgs e) {
            counter = (counter + 1) % 10000;
            ScoreScreenInstance.UpdateCounterText(counter);
        }

        private void SnakeCanvas_Loaded(object sender, RoutedEventArgs e) {
            fruit.canvasWidth = (int)SnakeCanvas.ActualWidth;
            fruit.canvasHeight = (int)SnakeCanvas.ActualHeight;

            snake.init_snake();
            fruit.Fruit_Spawner();
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



