
using System.Text.RegularExpressions;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
        }

        private static void Pickup3DigitNumber(string filePath) {
            string text = File.ReadAllText(filePath);
            var matches = Regex.Matches(text, @"\d{3,}");
            foreach (Match m in matches) {
                Console.WriteLine($"Index = {m.Index,-6}, Length = {m.Length,-3}, Value = {m.Value}");
            }

        }
    }
}
