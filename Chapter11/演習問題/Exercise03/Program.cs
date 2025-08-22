using System.Text.RegularExpressions;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            string[] texts = [
                "Time is money.",
                "What time is it?",
                "It will take time.",
                "We reorganized the timetable.",
            ];

            foreach (string text in texts) {
                int index = GetWordIndex(text, "time");
                if (index >= 0) {
                    Console.WriteLine($"{text} : {index}");
                }
            }

        }

        static int GetWordIndex(string text, string word) {
            Regex regex = new Regex(@"\b" + word + @"\b", RegexOptions.IgnoreCase);
            Match match = regex.Match(text);
            if (match.Success) {
                return match.Index;
            }
            return -1;
        }
    }
}
