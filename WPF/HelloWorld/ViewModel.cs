using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld {
    class ViewModel:INotifyPropertyChanged {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand(() => {
                GreetingMessage = "Nuwaaaaa";
            });
        }

        private string _greetingMessage = "HelloWorld";
        public string GreetingMessage {
            get => _greetingMessage;
            set {
                if (_greetingMessage != value) {
                    _greetingMessage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GreetingMessage)));
                }
            }
        }
        public DelegateCommand ChangeMessageCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
