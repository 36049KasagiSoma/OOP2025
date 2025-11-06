namespace Section05 {
    internal class Program {
        static void Main(string[] args) {
            var selected = Library.Books
                .AsParallel()
                .AsOrdered()
                .Where(b => b.Price > 500 && b.Price < 2000)
                .Select(b => new {b.Title});

            selected.ToList().ForEach(b=>Console.WriteLine(b.Title));
        }
    }
}
