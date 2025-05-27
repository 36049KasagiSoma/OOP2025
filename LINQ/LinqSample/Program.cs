namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {
            var numbers = Enumerable.Range(1, 100);

            //合計
            Console.WriteLine("偶数合計:" + numbers.Where(n => n % 8 == 0).Sum());

            //==================================

            //Console.WriteLine("偶数合計:" + numbers.Sum(n => (n % 2 == 0 ? n : 0)));

            //Console.WriteLine("2倍偶数合計:" + numbers.Sum(n => (n % 2 == 0 ? n * 2 : 0)));



        }
    }
}
