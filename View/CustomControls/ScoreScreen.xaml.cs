using System;
using System.Windows.Controls;

namespace WpfTest.View.CustomControls {
    
    public partial class ScoreScreen : UserControl {
        public delegate void ScoreIncrementedEventHandler(object sender, EventArgs e);

       
        public ScoreScreen() {
            InitializeComponent();
        }

      

    }
}
