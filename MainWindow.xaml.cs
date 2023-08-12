using System;
using System.Windows;
using WpfTest.View.CustomControls;

namespace WpfTest {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            gameBoard.RestartButtonClicked += GameBoard_RestartButtonClicked;
        }

        /// Eventlistener for RestartButtonClicked
        private void GameBoard_RestartButtonClicked(object sender, EventArgs e) {
            GameOver.Visibility = Visibility.Collapsed;
        }

   
        /// Eventlistener for CollisionDetected
        private void GameBoard_CollisionDetected(object sender, EventArgs e) {
            GameOver.Visibility = Visibility.Visible;

        }



    }
}
