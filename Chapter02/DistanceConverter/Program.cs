namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィード・メートル対応表を出力するやつ
        static readonly double oneFeedToMeter = 0.3048;

        static void Main(string[] args) {
            int min = args.Length >= 2 ? TryParceInt(args[1], 1) : 1;
            int max = args.Length >= 3 ? TryParceInt(args[2], 10) : 10;

            string type = args.Length > 0 ? args[0] : "-tom";

            if (type == "-tom") {
                FeedConverter.FeetToMeter(min, max);
            } else if (type == "-tof") {
                FeedConverter.MeterToFeed(min, max);
            } else {
                Console.WriteLine($"不明なオプション：{args[0]}");
            }
        }

        /// <summary>string型変数を数値に変換することを試みます。</summary>
        /// <param name="_baseStr">変換元文字列</param>
        /// <param name="_failed">変換失敗時に返り値となる数値</param>
        /// <returns>変換した数値 または 指定された数値(失敗時：_failed)</returns>
        private static int TryParceInt(string _baseStr, int _failed) {
            if (int.TryParse(_baseStr, out int val)) return val;
            return _failed;
        }

    }
}
