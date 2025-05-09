using System.Diagnostics;

namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            var sales = new SalesCounter(@"data\Sales.csv");

            var amountsPerCategory = sales.GetPerStoreSales();
            
            foreach(var obj in amountsPerCategory) {
                Console.WriteLine($"{obj.Key} {obj.Value}");
            }
            
        }
       
    }
}
