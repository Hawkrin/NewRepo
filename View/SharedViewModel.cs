using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.View {
    public class SharedViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _score;
 

        public int Score {
            get => _score;
            set {
                if (_score != value) {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
