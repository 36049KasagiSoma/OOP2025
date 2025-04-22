namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィード・メートル対応表を出力するやつ
        static readonly double oneFeetToMeter = 0.3048;

        static void Main(string[] args) {
            //入力文字列の数値変換（失敗時は min=1,max=10）
            int min = args.Length >= 2 ? TryParceInt(args[1], 1) : 1;
            int max = args.Length >= 3 ? TryParceInt(args[2], 10) : 10;

            //引数で「フィード→メートル」か「メートル→フィート」
            string type = args.Length > 0 ? args[0] : "-tom";

            if (type == "-tom") {
                FeetToMeter(min, max);
            } else if (type == "-tof") {
                MeterToFeet(min, max);
            } else {
                Console.WriteLine($"不明なオプション：{args[0]}");
            }
        }

        /// <summary>メートル値をフィード値に変換し、一覧を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        public static void MeterToFeet(int _min, int _max) {
            for (int meter = _min; meter <= _max; meter++) {
                int sp = _max.ToString().Length - meter.ToString().Length;
                double feet = FeetConverter.MeterToFeet(meter);
                Console.WriteLine($"{fillSpace(sp)}{meter}m = {feet:0.0000}fr");
            }
        }

        /// <summary>フィード値をメートル値に変換し、一覧を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        public static void FeetToMeter(int _min, int _max) {
            for (int feet = _min; feet <= _max; feet++) {
                int sp = _max.ToString().Length - feet.ToString().Length;
                double meter = FeetConverter.FeetToMeter(feet);
                Console.WriteLine($"{fillSpace(sp)}{feet}fr = {meter:0.0000}m");
            }
        }


        /// <summary>任意文字数の空白文字列を作成します。</summary>
        /// <param name="_length">文字数</param>
        /// <returns>引数で指定した文字数のスペース文字列</returns>
        private static string fillSpace(int _length) {
            string rtn = "";
            for (int i = 0; i < _length; i++) {
                rtn += " ";
            }
            return rtn;
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
