using System.Text;
using System.Text.RegularExpressions;

namespace Exercise05 {
    internal class Program {
        static void Main(string[] args) {
            string text = File.ReadAllText("sample.html");

            var tags = Regex.Matches(text, @"(</?[^>\s]*)(>|\s[^>]*)");

            string work = text;
            foreach (Match m in tags) {
                work = work.Substring(0, m.Index)
                    + m.Groups[1].Value.ToLower() + m.Groups[2].Value
                    + work.Substring(m.Index + m.Length, work.Length - (m.Index + m.Length));
            }

            File.WriteAllText("sampleOut.html", work);

        }
    }
}
