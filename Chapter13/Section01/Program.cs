namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var price = Library.Books.Where(b => b.CategoryId == 1).Max(b => b.Price);
            Console.WriteLine(price);
            Console.WriteLine();

            var book = Library.Books.Where(b => b.PublishedYear >= 2021).MinBy(b => b.Price);
            Console.WriteLine(book);
            Console.WriteLine();

            var avg = Library.Books.Average(b => b.Price);

            var books = Library.Books.Where(b => b.Price > avg);
            books.ToList().ForEach(Console.WriteLine);

        }
    }
}
