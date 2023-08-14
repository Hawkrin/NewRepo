using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using WpfTest.View.CustomControls;

namespace WpfTest {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

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
