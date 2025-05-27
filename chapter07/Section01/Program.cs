namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var books = Books.GetBooks();


            //本の平均金額を表示
            Console.WriteLine("本の平均金額");
            Console.WriteLine(books.Average(b => b.Price) + " 円");

            Console.WriteLine();

            //本のページ合計を表示
            Console.WriteLine("本の合計ページ");
            Console.WriteLine(books.Sum(b => b.Pages) + " ページ");

            Console.WriteLine();

            //金額の安い書籍名と金額を表示
            Console.WriteLine("一番安い書籍");
            Console.WriteLine(books.OrderBy(b => b.Price).Select(b => b.Title + ", " + b.Price + "円").First());

            Console.WriteLine();

            //ページが多い書籍名とページ
            Console.WriteLine("一番ページが多い書籍");
            Console.WriteLine(books.OrderBy(b => b.Pages).Select(b => b.Title + ", " + b.Pages + "ページ").First());

            Console.WriteLine();

            //物語が含まれる書籍
            Console.WriteLine("「物語」が含まれる書籍");
            books.ForEach(b => {
                if (b.Title.Contains("物語")) Console.WriteLine(b.Title);
            });
        }
    }
}
