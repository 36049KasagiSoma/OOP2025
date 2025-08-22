
using System.Text.RegularExpressions;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            string filePath = "sample.txt";
            Pickup3DigitNumber(filePath);
            Console.WriteLine("=========================");
            Pickup3Chars(filePath);
        }

        private static void Pickup3DigitNumber(string filePath) {
            string text = File.ReadAllText(filePath);
            var matches = Regex.Matches(text, @"\b\d{3,}\b");
            foreach (Match m in matches) {
                Console.WriteLine($"Index = {m.Index,-6}, Length = {m.Length,-3}, Value = {m.Value}");
            }
        }

        private static void Pickup3Chars(string filePath) {
            string text = File.ReadAllText(filePath);
            var matches = Regex.Matches(text, @"\b[a-zA-Z]{3,}\b");
            foreach (Match m in matches) {
                Console.WriteLine($"Index = {m.Index,-6}, Length = {m.Length,-3}, Value = {m.Value}");
            }
        }
    }
}
