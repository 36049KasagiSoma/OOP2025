using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    public class LineCounterProcessor : TextProcessor {
        private int _cnt = 0;

        protected override void Initialize(string fname) => _cnt = 0;
        protected override void Execute(string line) => _cnt++;
        protected override void Terminate() => Console.WriteLine($"{_cnt} 行");

        public int GetCount() => _cnt;
    }
}
