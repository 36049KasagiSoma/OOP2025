namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {
            Product karinto = new Product(123, "かりんとう", 180);
            Product daihuku = new Product(234, "大福", 250);

            Console.WriteLine($"{karinto.Name}の税抜き価格は{karinto.Price}円です");
            Console.WriteLine($"{karinto.Name}の消費税額は{karinto.GetTax()}円です");
            Console.WriteLine($"{karinto.Name}の税込み価格は{karinto.GetPriceIncludingTax()}円です");

            Console.WriteLine();

            Console.WriteLine($"{daihuku.Name}の税抜き価格は{daihuku.Price}円です");
            Console.WriteLine($"{daihuku.Name}の消費税額は{daihuku.GetTax()}円です");
            Console.WriteLine($"{daihuku.Name}の税込み価格は{daihuku.GetPriceIncludingTax()}円です");
            
        }
    }
}
