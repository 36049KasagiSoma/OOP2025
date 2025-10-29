
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();


        }

        private static void Exercise1_2() {
            Console.WriteLine(Library.Books.MaxBy(b => b.Price));
        }

        private static void Exercise1_3() {
            Library.Books.GroupBy(b => b.PublishedYear)
                .Select(x => $"{x.Key}:{x.Count()}")
                .ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise1_4() {
            Library.Books.OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price)
                .ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise1_5() {
            Library.Books.Where(b => b.PublishedYear == 2022)
                .Join(Library.Categories, b => b.CategoryId, c => c.Id, (b, c) => c.Name)
                .Distinct()
                .ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise1_6() {
            Library.Books.GroupBy(b => b.CategoryId).ToList().ForEach(g => {
                Console.WriteLine($"# {Library.Categories.Where(c => c.Id == g.Key).First().Name}");
                g.ToList().ForEach(b => Console.WriteLine($"  {b.Title}"));
            });
        }

        private static void Exercise1_7() {
            Library.Books.Where(b => b.CategoryId
                    == Library.Categories.Where(c => c.Name == "Development").First().Id)
                .GroupBy(b => b.PublishedYear).ToList().ForEach(g => {
                    Console.WriteLine($"# {g.Key}");
                    g.ToList().ForEach(b => Console.WriteLine($"  {b.Title}"));
                }
            );
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
