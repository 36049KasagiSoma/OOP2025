using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWorld {
    class ViewModel:BindableBase {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand<string>((par) => {
                GreetingMessage = par;
            });
        }

        private string _greetingMessage = "HelloWorld";
        public string GreetingMessage {
            get => _greetingMessage;
            set {
                if(SetProperty(ref _greetingMessage, value)) {
                    CanChangeMessage = false;
                }
            }
        }

        private bool _canChangeMessage = true;
        public bool CanChangeMessage {
            get => _canChangeMessage;
            set => SetProperty(ref _canChangeMessage, value);
        }

        public string NewMessage1 { get;} ="Guhe";
        public string NewMessage2 { get;} = "Nuwaaa";
        public DelegateCommand<string> ChangeMessageCommand { get; }
    }
}
