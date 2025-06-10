using Exercise01;

namespace Exercise02 {
    public class Program {
        static void Main(string[] args) {
            // 5.2.1
            var ymCollection = new YearMonth[] {
                new YearMonth(1980, 1),
                new YearMonth(1990, 4),
                new YearMonth(1000, 7),
                new YearMonth(1010, 9),
                new YearMonth(1024, 12),
            };


            Console.WriteLine("=====5.2.2==========");
            Exercise2(ymCollection);
            Console.WriteLine();

            Console.WriteLine("=====5.2.4==========");
            Exercise4(ymCollection);
            Console.WriteLine();

            Console.WriteLine("=====5.2.5==========");
            Exercise5(ymCollection);
        }

        private static void Exercise2(YearMonth[] ymCollection) {
            ymCollection.ToList().ForEach(Console.WriteLine);
        }

        /// <summary>
        /// 与えられたYearMonth配列から、一番最初の21世紀のデータを返します。
        /// </summary>
        /// <param name="ymCollection">検索配列</param>
        /// <returns>最初の21世紀のデータ（見つからなかったらnull）</returns>
        private static YearMonth? FindFirst21C(YearMonth[] ymCollection) {
            foreach (YearMonth y in ymCollection) {
                if (y.Is21Century) return y;
            }
            return null;
        }

        private static void Exercise4(YearMonth[] ymCollection) {
            YearMonth? y = FindFirst21C(ymCollection);
            //Console.WriteLine(y is not null ? y.Year : "21世紀のデータはありません");

            //null合体演算子
            //Console.WriteLine(SafetyGetYear(y) ?? "21世紀のデータはありません");
        }

        /// <summary>
        /// YearMonthから安全に年を取得します。
        /// </summary>
        /// <param name="y">取得対象YearMonth</param>
        /// <returns>年（引数がnullの場合はnull）</returns>
        public static string? SafetyGetYear(YearMonth? y) {
            if (y == null) return null;
            return y.Year.ToString();
        }

        private static void Exercise5(YearMonth[] ymCollection) {
            YearMonth[] newYm = ymCollection.Select(y => y.AddOneMonth()).ToArray();

            Exercise2(newYm);
        }
    }

}
