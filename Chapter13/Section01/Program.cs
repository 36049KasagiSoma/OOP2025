namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var books = Library.Books
                             .Join(Library.Categories,
                                b => b.CategoryId, c => c.Id,
                                (b, c) => new {b.Title, Category = c, b.PublishedYear});
            foreach (var b in books) {
                Console.WriteLine($"{b.Title}, {b.Category}, {b.PublishedYear}");
            }
        }
    }
}
