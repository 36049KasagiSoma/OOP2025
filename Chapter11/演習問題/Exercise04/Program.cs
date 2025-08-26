using System.Text.RegularExpressions;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            string text = File.ReadAllText("sample.txt");
            string newText = Regex.Replace(text,
                @"(V|v)ersion\s*=\s*""v4.0""",
                @"version=""v5.0""");
            File.WriteAllText("sample.txt", newText);
        }
    }
}
