using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisomObservesSample {
    public class MainWindowViewModel : BindableBase {
        private string _input1 = "";
        private string _input2 = "";
        private string _result = "";

        public string Input1 { get => _input1; set => SetProperty(ref _input1, value); }
        public string Input2 { get => _input2; set => SetProperty(ref _input2, value); }
        public string Result { get => _result; set => SetProperty(ref _result, value); }

        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExcuteSum);
        }

        private void ExcuteSum() {
            if (int.TryParse(Input1, out int in1) && int.TryParse(Input2, out int in2)) {
                Result = (in1 + in2).ToString();
            } else {
                Result = "?";
                //new Thread(() => { 
                //    for (int i = 0; i < 1000; i++) {
                //        string jet = "";
                //        for (int j = 0; j < i % 10; j++) {
                //            jet += "～";
                //        }
                //        if (i % 2 == 0) {
                //            Result = jet + "＼(^o^)＼";
                //        } else {
                //            Result = jet + "／(^o^)／";
                //        }
                //        Thread.Sleep(200);
                //    }
                //}).Start();
            }
        }

        public DelegateCommand SumCommand { get; }

    }
}
