using System.Text.RegularExpressions;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            string text = File.ReadAllText("sample.txt");
            string newText = Regex.Replace(text,
                @"[vV]ersion\s*=\s*""v4\.0""",
                @"version=""v5.0""");
            Console.WriteLine(newText);
            File.WriteAllText("sample2.txt", newText);
        }
    }
}
