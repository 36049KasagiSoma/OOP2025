using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    class LineToHalfNumberService : ITextFileService {
        public void Execute(string line) {
            string newLine = Regex.Replace(line, @"[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());
            Console.WriteLine(newLine);
        }

        public void Initialize(string fname) {
        }

        public void Terminate() {
        }
    }
}
