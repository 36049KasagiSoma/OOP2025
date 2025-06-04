
using System.Collections;
using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";
            #region
            Console.WriteLine("=====6.3.1==========");
            Exercise1(text);

            Console.WriteLine();

            Console.WriteLine("=====6.3.2==========");
            Exercise2(text);

            Console.WriteLine();

            Console.WriteLine("=====6.3.3==========");
            Exercise3(text);

            Console.WriteLine();

            Console.WriteLine("=====6.3.4==========");
            Exercise4(text);

            Console.WriteLine();

            Console.WriteLine("=====6.3.5==========");
            Exercise5(text);

            Console.WriteLine();

            Console.WriteLine("=====6.3.99=========");
            Exercise6(text);
            #endregion

        }

        private static void Exercise1(string text) {
            Console.WriteLine("空白数:" + text.Count(Char.IsWhiteSpace));
        }

        private static void Exercise2(string text) {
            Console.WriteLine("前" + text);
            Console.WriteLine("後" + text.Replace("big", "small"));
        }

        private static void Exercise3(string text) {
            Console.WriteLine("前" + text);
            var words = text.Split(" ");
            Console.WriteLine("後" + new StringBuilder().AppendJoin(' ', words) + ".");

            ////stringばーじょん
            //Console.WriteLine(string.Join(' ', words) + ".");

            ////ちゃんとStringBuilderばーじょん
            //var sb = new StringBuilder();
            //for (int i = 0; i < words.Length; i++) {
            //    sb.Append(words[i]);
            //    if (i < words.Length - 1) sb.Append(' ');
            //}
            //sb.Append(".");
            //Console.WriteLine(sb);
        }

        private static void Exercise4(string text) {
            Console.WriteLine("単語数:" + text.Split(" ").Length);
        }

        private static void Exercise5(string text) {
            var filtedWords = text.Split(" ").Where(s => s.Length <= 4);
            filtedWords.ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise6(string text) {
            string tmp = text.ToLower();
            for (char ch = 'a'; ch <= 'z'; ch++) {
                Console.WriteLine($"{ch}:{tmp.Count(c => c == ch)}");
            }
        }

    }
}
