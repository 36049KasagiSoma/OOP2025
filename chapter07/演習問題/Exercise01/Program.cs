
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            int[] numbers = [5, 10, 17, 9, 3, 21, 10, 40, 21, 3, 35];

            Console.WriteLine("--7.1.1----------");
            Exercise1(numbers);

            Console.WriteLine("--7.1.2----------");
            Exercise2(numbers);

            Console.WriteLine("--7.1.3----------");
            Exercise3(numbers);

            Console.WriteLine("--7.1.4----------");
            Exercise4(numbers);

            Console.WriteLine("--7.1.5----------");
            Exercise5(numbers);
        }

        private static void Exercise1(int[] numbers) {
            Console.WriteLine(numbers.Max());
        }

        private static void Exercise2(int[] numbers) {
            numbers[^2..].ToList().ForEach(Console.WriteLine);

            //[n..m] ... nからmまでを取り出す。
            //  ^n   ... 最後からn番目
            //参考:https://qiita.com/aramasa_ash/items/e3032f04cc0535529bb6
        }

        private static void Exercise3(int[] numbers) {
            numbers.ToList().ForEach(n => Console.WriteLine(n.ToString("000")));
        }

        private static void Exercise4(int[] numbers) {
            numbers.Order().Take(3).ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise5(int[] numbers) {
            Console.WriteLine(numbers.Distinct().Count(n => n > 10));
        }
    }
}
