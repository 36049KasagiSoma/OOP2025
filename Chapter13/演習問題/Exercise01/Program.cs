
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            #region メソッドの呼出部分
            Console.WriteLine("(2)");
            Exercise1_2();
            Console.WriteLine();
            Console.WriteLine("(3)");
            Exercise1_3();
            Console.WriteLine();
            Console.WriteLine("(4)");
            Exercise1_4();
            Console.WriteLine();
            Console.WriteLine("(5)");
            Exercise1_5();
            Console.WriteLine();
            Console.WriteLine("(6)");
            Exercise1_6();
            Console.WriteLine();
            Console.WriteLine("(7)");
            Exercise1_7();
            Console.WriteLine();
            Console.WriteLine("(8)");
            Exercise1_8();
            Console.ReadLine();
            #endregion
        }

        private static void Exercise1_2() {
            Console.WriteLine(Library.Books.MaxBy(b => b.Price));
        }

        private static void Exercise1_3() {
            Library.Books.GroupBy(b => b.PublishedYear)
                .OrderBy(g => g.Key)
                .Select(x => $"{x.Key}:{x.Count()}")
                .ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise1_4() {
            Library.Books.OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price)
                .ToList().ForEach(b => Console.WriteLine($"{b.PublishedYear}年 {b.Price}円 {b.Title}"));
        }

        private static void Exercise1_5() {
            Library.Books.Where(b => b.PublishedYear == 2022)
                .Join(Library.Categories, b => b.CategoryId, c => c.Id, (b, c) => c.Name)
                .Distinct()
                .ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise1_6() {
            Library.Books
                .Join(Library.Categories, b => b.CategoryId, c => c.Id, (b, c) => new { Category = c.Name, Book = b })
                .GroupBy(g => g.Category)
                .OrderBy(g => g.Key)
                .ToList().ForEach(g => {
                    Console.WriteLine($"# {g.Key}");
                    g.ToList().ForEach(b => Console.WriteLine($"   {b.Book.Title}"));
                });
        }

        private static void Exercise1_7() {
            Library.Books
                .Join(Library.Categories, b => b.CategoryId, c => c.Id, (b, c) => new { Category = c.Name, Book = b })
                .Where(g => g.Category == "Development")
                .GroupBy(g => g.Book.PublishedYear)
                .OrderBy(g => g.Key)
                .ToList().ForEach(g => {
                    Console.WriteLine($"# {g.Key}");
                    g.ToList().ForEach(b => Console.WriteLine($"   {b.Book.Title}"));
                });
        }

        private static void Exercise1_8() {
            Library.Categories.GroupJoin(
                    Library.Books, c => c.Id,
                    b => b.CategoryId, (c, b) => new { Category = c.Name, Books = b }
                ).Where(g => g.Books.Count() >= 4)
                .ToList().ForEach(g => Console.WriteLine(g.Category));
        }


    }
}
