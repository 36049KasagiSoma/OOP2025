namespace Section01 {
    internal class Program {
        
        static void Main(string[] args) {

            var numbers = new int[] { 5, 3, 9, 6, 7, 5, 8, 1, 0, 5, 10, 4 };
            Console.WriteLine(Count(numbers, delegate(int n) { return n % 2 == 0; }));

        }


        static int Count(int[] numvers, Func<int,bool> judge) {
            var count = 0;
            foreach (var n in numvers) {
                //引数で受け取ったメソッドを呼び出す
                if (judge(n)) {
                    count++;
                }
            }
            return count;
        }

        /*
         * Actionデリゲート
         * 戻り値なし
         * 
         * Action<T>
         * 一つの引数（型はT）
         * 
         * 
         * Predicateデリゲート
         * 戻り値はbool
         * 
         * Predicate<T>
         * 一つの引数（型はT）
         * 
         * 
         * Funcデリゲート
         * 任意の戻り値（型はTResult）
         * 
         * Func<T,TResult>
         * 一つの引数（型はT）
         */
    }
}
