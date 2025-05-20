
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London",
                "Paris", "Berlin", "Canberra", "Hong Kong",
            };

            Console.WriteLine("***** 3.2.1 *****");
            Exercise2_1(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.2 *****");
            Exercise2_2(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.3 *****");
            Exercise2_3(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.4 *****");
            Exercise2_4(cities);
            Console.WriteLine();
        }

        private static void Exercise2_1(List<string> cities) {
            Console.WriteLine("都市名検索（空行で終了）");
            var searchText = Console.ReadLine();
            while (!string.IsNullOrEmpty(searchText)) {
                Console.WriteLine(cities.FindIndex(s => s.Equals(searchText)));
                searchText = Console.ReadLine();
            }
        }

        private static void Exercise2_2(List<string> cities) {
            Console.WriteLine("小文字の\'o\'が含まれている都市の数");
            Console.WriteLine(cities.Count(s => s.Contains('o')));
        }

        private static void Exercise2_3(List<string> cities) {
            Console.WriteLine("小文字の\'o\'が含まれている都市一覧");
            var list = cities.Where(s => s.Contains('o')).ToList();
            list.ForEach(Console.WriteLine);
        }

        private static void Exercise2_4(List<string> cities) {
            Console.WriteLine("\'B\'から始まる都市の文字数一覧");
            var countList = cities.Where(s => s[0].Equals('B')).Select(s => s.Length).ToList();
            countList.ForEach(Console.WriteLine);
        }
    }
}
