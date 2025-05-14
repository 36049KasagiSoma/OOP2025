namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("カウントしたい数値：");
            if (int.TryParse(Console.ReadLine(), out int num)) {
                Console.WriteLine(Count(num));
            } else {
                Console.WriteLine("無効な数値");
            }
        }

        static int Count(int num) {
            var numvers = new[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            var count = 0;
            foreach (var n in numvers) {
                if (n == num) {
                    count++;
                }
            }
            return count;
        }
    }
}
