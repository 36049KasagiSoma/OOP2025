
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }

        private static void Exercise1(string text) {
            string work = text.ToUpper();
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (char c in work) {
                if ('A' <= c && c <= 'Z') {
                    if (dict.ContainsKey(c)) {
                        dict[c]++;
                    } else {
                        dict[c] = 1;
                    }
                }
            }

            dict = dict.OrderBy(p => p.Key).ToDictionary();

            foreach (var pair in dict) {
                Console.WriteLine($"\'{pair.Key}\':{pair.Value}");
            }
        }

        private static void Exercise2(string text) {
        }
    }
}
