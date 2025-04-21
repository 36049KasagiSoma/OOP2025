namespace DistanceConverter {
    internal class Program {

        static readonly double oneFeedToMeter = 0.3048;

        static void Main(string[] args) {
#if true
            int min = args.Length >= 2 ? TryParceInt(args[1], 1) : 1;
            int max = args.Length >= 3 ? TryParceInt(args[2], 10) : 10;

            string type = args.Length > 0 ? args[0] : "-tom";

        


            if (type == "-tom") {
                FeetToMeter(min, max);
            } else if (type == "-tof") {
                MeterToFeed(min, max);
            } else {
                Console.WriteLine($"不明なオプション：{args[0]}");
            }


#else
            Maze maze = new Maze(30, 30);
            maze.CreateMaze();
            maze.Print();
#endif
        }


        /// <summary>
        /// string型変数を数値に変換することを試みます。
        /// </summary>
        /// <param name="_baseStr">変換元文字列</param>
        /// <param name="_failed">変換失敗時に返り値となる数値</param>
        /// <returns>変換した数値 または 指定された数値(失敗時：_failed)</returns>
        private static int TryParceInt(string _baseStr,int _failed) {
            if (int.TryParse(_baseStr, out int val)) return val;
            return _failed;
        }


        /// <summary>
        /// メートル値をフェード値に変換し、一覧を出力します。
        /// </summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        private static void MeterToFeed(int _min, int _max) {
            for (int meter = _min; meter <= _max; meter++) {
                int sp = _max.ToString().Length - meter.ToString().Length;
                double feet = MeterToFeet(meter);
                Console.WriteLine($"{fillSpace(sp)}{meter}m = {feet:0.0000}fr");
            }
        }

        /// <summary>
        /// フェード値をメートル値に変換し、一覧を出力します。
        /// </summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        private static void FeetToMeter(int _min, int _max) {
            for (int feet = _min; feet <= _max; feet++) {
                int sp = _max.ToString().Length - feet.ToString().Length;
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{fillSpace(sp)}{feet}fr = {meter:0.0000}m");
            }
        }

        /// <summary>
        /// フェード値をメートル値に変換します。
        /// </summary>
        /// <param name="_feet">変換元フェード値</param>
        /// <returns>メートル値</returns>
        static double FeetToMeter(int _feet) {
            return _feet * oneFeedToMeter;
        }

        /// <summary>
        /// メートル値をフェード値に変換します。
        /// </summary>
        /// <param name="_meter">変換元メートル値</param>
        /// <returns>フェード値</returns>
        static double MeterToFeet(int _meter) {
            return _meter / oneFeedToMeter;
        }

        static string fillSpace(int _cnt) {
            string rtn = "";
            for(int i = 0; i < _cnt; i++) {
                rtn += " ";
            }
            return rtn;
        }
    }
}
