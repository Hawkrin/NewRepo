using System;
using System.Windows;
using WpfTest.View;

namespace WpfTest {

    public partial class MainWindow : Window {
        private SharedViewModel sharedViewModel;
        public MainWindow() {
            InitializeComponent();
            sharedViewModel = new SharedViewModel();
            DataContext = sharedViewModel;
            gameBoard.ViewModel = sharedViewModel;

            gameBoard.RestartButtonClicked += GameBoard_RestartButtonClicked;
            gameBoard.Focus();
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
