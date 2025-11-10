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

            using (StreamReader reader = new StreamReader(_path)) {
                string? line;
                while ((line = reader.ReadLine()) != null) {
                    if (Regex.IsMatch(line, @"\sclass\s")) {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

        public int CountClassStr2() {
            int cnt = 0;

            string[] line = File.ReadAllLines(_path);
            foreach (string str in line) {
                if (Regex.IsMatch(str, @"\sclass\s")) {
                    cnt++;
                }
            }

            return cnt;
        }

        public int CountClassStr3() {
            int cnt = 0;

            List<string> line = File.ReadLines(_path).ToList();
            foreach (string str in line) {
                if (Regex.IsMatch(str, @"\sclass\s")) {
                    cnt++;
                }
            }

            return cnt;
        }
    }
}
