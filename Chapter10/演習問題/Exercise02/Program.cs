namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            LineNumAllocator lna = new LineNumAllocator("source.txt");
            lna.AddLineNumbers("output.txt");
        }
    }

    class LineNumAllocator {
        private string _path;
        public LineNumAllocator(string path) {
            this._path = path;
        }
        public void AddLineNumbers(string newPath) {
            string[]? lines = File.ReadAllLines(_path);
            int length = numLength(lines.Length);
            using (StreamWriter writer = new StreamWriter(newPath)) {
                for (int i = 0; i < lines.Length; i++) {
                    writer.WriteLine($"{num(i + 1, length)}:{lines[i]}");
                }
            }
        }

        private string num(int value, int length) {
            string str = value.ToString();
            while (str.Length < length) {
                str = " " + str;
            }
            return str;
        }

        private int numLength(int value) {
            return value.ToString().Length;
        }
    }
}
