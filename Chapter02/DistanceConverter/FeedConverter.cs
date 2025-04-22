using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    public static class FeedConverter {
        private const double ratio = 0.3048;

        /// <summary>メートル値をフィード値に変換し、一覧を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        public static void MeterToFeed(int _min, int _max) {
            for (int meter = _min; meter <= _max; meter++) {
                int sp = _max.ToString().Length - meter.ToString().Length;
                double feet = MeterToFeet(meter);
                Console.WriteLine($"{fillSpace(sp)}{meter}m = {feet:0.0000}fr");
            }
        }

        /// <summary>フィード値をメートル値に変換し、一覧を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        public static void FeetToMeter(int _min, int _max) {
            for (int feet = _min; feet <= _max; feet++) {
                int sp = _max.ToString().Length - feet.ToString().Length;
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{fillSpace(sp)}{feet}fr = {meter:0.0000}m");
            }
        }

        /// <summary>フィード値をメートル値に変換します。</summary>
        /// <param name="_feet">変換元フィード値</param>
        /// <returns>メートル値</returns>
        private static double FeetToMeter(int _feet) {
            return _feet * ratio;
        }

        /// <summary>メートル値をフィード値に変換します。</summary>
        /// <param name="_meter">変換元メートル値</param>
        /// <returns>フィード値</returns>
        private static double MeterToFeet(int _meter) {
            return _meter / ratio;
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
    }
}
