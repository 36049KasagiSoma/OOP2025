using System.Text.RegularExpressions;

namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            var strings = new string[] {
                "Microsoft Windows",
                "windows",
                "Windows Server",
                "Windows",
            };

            Regex regex = new Regex(@"^(W|w)indows$");
            var count = strings.Count(regex.IsMatch);

            Console.WriteLine($"{count}行と一致");

            // パターンと完全一致している行文字列を出力する
            strings.Where(s => regex.IsMatch(s))
                .ToList().ForEach(Console.WriteLine);

        }

    }
}
