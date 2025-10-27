namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
           var selected = Library.Books.GroupBy(b => b.PublishedYear)
                .Select(g=>g.MaxBy(b=>b.Price))
                .OrderBy(b=>b!.PublishedYear);

            foreach (var b in selected) {
                Console.WriteLine($"{b!.PublishedYear}年 {b!.Title} ({b!.Price})");
            }

        }
    }
}
