using System.Diagnostics;

namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            SalesCounter sales = new SalesCounter(@"data\Sales.csv");

            IDictionary<string,int> amountsPerCategory = sales.GetPerStoreSales();
            
            foreach(KeyValuePair<string,int> obj in amountsPerCategory) {
                Console.WriteLine($"{obj.Key} {obj.Value}");
            }
            
        }
       
    }
}
