using System.Text.RegularExpressions;

namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            var text = "private List<string> results = new List<string>();";
            var regEx = @"List<\w+>";
            bool isMatch = Regex.IsMatch(text,regEx);
            Console.WriteLine($"検索対象:{text}");
            Console.WriteLine($"検索内容:{regEx}");
            Console.Write("検索結果:");
            if (isMatch) {
                Console.WriteLine("見つかりました");
            } else {
                Console.WriteLine("見つかりません");
            }



        }
    }
}
