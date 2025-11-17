using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    public class LineOutputService : ITextFileService {
        private int _count;
        private int _maxCount;
        public LineOutputService(int maxLineCount) {
            _maxCount = maxLineCount;
        }
        public void Execute(string line) {
            _count++;
            if (_count <= _maxCount)
                Console.WriteLine($"{GetLineNum(_count, _maxCount.ToString().Length)}|{line}");
        }

        public void Initialize(string fname) {
            _count = 0;
        }

        public void Terminate() {
        }

        private string GetLineNum(int num, int length) {
            StringBuilder space = new StringBuilder();
            while (num.ToString().Length + space.ToString().Length < length) {
                space.Append(' ');
            }
            return space.ToString() + num.ToString();
        }
    }
}
