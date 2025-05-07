namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            List<Sale> s = ReadSales(@"data\Sales.csv");
            Console.WriteLine(s.Count);
        }

        static List<Sale> ReadSales(string filePath) {
            //売り上げリスト
            List<Sale> sales = new List<Sale>();
            string[] lines = File.ReadAllLines(filePath);
            //各行をカンマ区切りで分割、Saleデータの作成、登録
            foreach (string line in lines) {
                string[] items = line.Split(',');
                Sale sale = new Sale {
                    ShopName = items[0],
                    ProductCategory = items[1],
                    Amount = int.Parse(items[2])
                };
                sales.Add(sale);
            }

            return sales;
        }
    }
}
