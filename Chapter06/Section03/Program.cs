using System.Numerics;
using System.Text;

namespace Section03 {
    internal class Program {
        static void Main(string[] args) {
            var sb = new StringBuilder();
            foreach (var word in GetWorks()) {
                sb.Append(word);
            }

            Console.WriteLine(sb.ToString());

            string str = "";
            foreach (var word in GetWorks()) {
                str += word;
            }

            Console.WriteLine(str);
        }

        private static IEnumerable<string> GetWorks() {
            return ["Orange", "Lemon", "Strawberry"];
        }
    }
}
