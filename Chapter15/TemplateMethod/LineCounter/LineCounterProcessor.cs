using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    public class LineCounterProcessor : TextProcessor {
        private int _cnt = 0;
        private string _word = string.Empty;

        protected override void Initialize(string fname) {
            _cnt = 0;
            Console.Write("検索文字列 > ");
            _word = Console.ReadLine()??string.Empty;
        }
        protected override void Execute(string line) {
            _cnt += Regex.Matches(line, @$".*{Regex.Escape(_word)}.*").Count();
        }
        protected override void Terminate() => Console.WriteLine($"{_cnt} 行");

        public int GetCount() => _cnt;
    }
}
