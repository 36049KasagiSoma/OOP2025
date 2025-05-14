namespace Section01 {
    internal class Program {

        public delegate bool Judgement(int value); //デリゲートの宣言

        static void Main(string[] args) {

            var numbers = new int[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            Judgement judge = IsOdd;
            Console.WriteLine(Count(numbers, judge));

        }

        static bool IsEven(int n) {
            return n % 2 == 0;
        }

        static bool IsOdd(int n) {
            return n % 2 != 0;
        }

        static int Count(int[] numvers, Judgement judge) {
            var count = 0;
            foreach (var n in numvers) {
                //引数で受け取ったメソッドを呼び出す
                if (judge(n)) {
                    count++;
                }
            }
            return count;
        }
    }
}
