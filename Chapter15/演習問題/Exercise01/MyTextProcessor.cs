using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFileProcessor;

namespace Exercise01 {
    public class MyTextProcessor:TextProcessor {
        protected override void Execute(string line) {
            string newLine = Regex.Replace(line, @"[０-９]",
                p => ((char)(p.Value[0] - '０' + '0')).ToString());
            Console.WriteLine(newLine);
        }
    }
}
