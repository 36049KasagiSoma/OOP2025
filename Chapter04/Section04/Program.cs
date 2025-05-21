
namespace Section04 {
    internal class Program {
        static void Main(string[] args) {
            #region nullの判定

            string? name = null;
            if (name is null) {
                Console.WriteLine("nameはnullです");
            }

            #endregion

            #region null合体演算子
            string code = "12345";
            var message = GetMessage(code) ?? DefaultMessage();
            Console.WriteLine(message);
            #endregion

            #region null条件演算子
            //Sale? sale = new Sale {
            //    ShopName="新宿店",
            //    ProductCategory = "洋菓子",
            //    Amount=523100
            //};

            Sale? sale = null;
            int? amount = sale?.Amount;

            #endregion

            #region 値の入れ替え
            int a = 10;
            int b = 20;
            //(b, a) = (a, b);

            int tmp = a;
            a = b;
            b = tmp;

            Console.WriteLine($"a={a},b={b}");

            #endregion

            string? inputDara = Console.ReadLine();

            if(int.TryParse(inputDara, out var number)) {
                Console.WriteLine(number);
            } else {
                Console.WriteLine("エラー");
            }


            //try {
            //    int num = int.Parse(inputDara);
            //    Console.WriteLine(num);

            //} catch (FormatException e) {

            //    Console.WriteLine("フォーマットエラー:");
            //}catch(OverflowException e) {
            //    Console.WriteLine("オーバーフローエラー");
            //} finally {
            //    Console.WriteLine("処理完了");
            //}
            //Console.WriteLine("メソッド終了");

        }


        private static object? GetMessage(string code) {
            return code;
        }

        private static object? DefaultMessage() {
            return "DefaultMessage";
        }
    }
}
