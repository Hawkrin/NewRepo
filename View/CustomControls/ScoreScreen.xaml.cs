using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace WpfTest.View.CustomControls {
    
    public partial class ScoreScreen : UserControl, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public delegate void ScoreIncrementedEventHandler(object sender, EventArgs e);

       
        public ScoreScreen() {
            InitializeComponent();
        }

        public void UpdateCounterText(int value) {
            string formattedValue = value.ToString("D4");
            score.Text = formattedValue;
            Debug.WriteLine(score.Text);
            UpdateLayout();
        }

  






    }
}
