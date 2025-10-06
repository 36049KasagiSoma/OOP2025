using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld {
    class ViewModel : BindableBase {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand<string>(
                (par) => GreetingMessage = par,
                (par) => GreetingMessage != par).
                ObservesProperty(() => GreetingMessage);


        }

        private string _greetingMessage = "HelloWorld";
        public string GreetingMessage {
            get => _greetingMessage;
            set => SetProperty(ref _greetingMessage, value);

        }

        public string NewMessage1 { get; } = "Guhe";
        public string NewMessage2 { get; } = "Nuwaaa";
        public DelegateCommand<string> ChangeMessageCommand { get; }
    }
}
