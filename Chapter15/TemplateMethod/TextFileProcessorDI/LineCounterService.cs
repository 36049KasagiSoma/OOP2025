using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineCounterService : ITextFileService {
        private int _count;
        public void Execute(string line) {
            _count++;
        }

        public void Initialize(string fname) {
            _count = 0;
        }

        public void Terminate() {
            Console.WriteLine($"{_count} 行");
        }
    }
}
