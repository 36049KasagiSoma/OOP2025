using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exercise01 {
    public class CountClass {
        private string _path;
        public CountClass(string path) {
            this._path = path;
        }

        public int CountClassStr() {
            int cnt = 0;
            
            using(StreamReader reader = new StreamReader(_path)) {
                string? line;
                while((line = reader.ReadLine()) != null) {
                    if(Regex.IsMatch(line, @"\sclass\s")) {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
    }
}
