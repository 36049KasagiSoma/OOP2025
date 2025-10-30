namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var groups = Library.Categories
                .GroupJoin(Library.Books,
                c => c.Id,
                b => b.CategoryId,
                (c, b) => new {
                    Category = c.Name,
                    Books = b,
                });

            foreach(var g in groups) {
                Console.WriteLine(g.Category);
                foreach (var b in g.Books) {
                    Console.WriteLine($"    {b.Title} ({b.PublishedYear})");
                }
            }
        }
    }
}
